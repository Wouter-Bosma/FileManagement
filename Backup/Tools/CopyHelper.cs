using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backup.Copier;
using BackupSolution;
using BackupSolution.FolderReader;

namespace Backup.Tools
{
    enum CopySetting
    {
        NoOverwrite,
        OverwriteAll,
        OverwriteChangedSourceWriteTime,
        OverwriteChangedHash,
    }
    internal static class CopyHelper
    {
        private static readonly Lock _synchronizationLock = new();
        public static async Task CopyFromSourceToTarget(CopyData data, CopySetting setting, bool cloneHashOnCopy)
        {
            if (data.isFolderSource)
            {
                var valid = Configuration.Instance.GetFolderData(true).TryGetFolderData(data.sourceData, out var sourceFolder);
                valid = Configuration.Instance.GetFolderData(false).TryGetFolderData(data.DestinationFolder, out var targetFolder) && valid;
                if (!valid)
                {
                    return;
                }
                await CopyFromSourceToTarget(sourceFolder, targetFolder);
            }
            else
            {
                var valid = Configuration.Instance.GetFolderData(true).TryGetFileData(data.sourceData, out var sourceFile);
                valid = Configuration.Instance.GetFolderData(false).TryGetFolderData(data.DestinationFolder, out var targetFolder) && valid;
                if (!valid)
                {
                    return;
                }
                await CopyFromSourceToTarget(sourceFile, targetFolder);
            }
        }

        private static async Task CopyFromSourceToTarget(FolderData sourceFolder, FolderData targetFolder)
        {
            var sourceFolderFiles = sourceFolder.EnumerateOverAllFiles().ToDictionary(x => x.GetRelativePath(sourceFolder.FolderName), x => x);
            var targetFolderFiles = targetFolder.EnumerateOverAllFiles().ToDictionary(x => x.GetRelativePath(targetFolder.FolderName), x => x);
            var toCopy = new Dictionary<string, FileData>();
            foreach (var kvp in sourceFolderFiles)
            {
                if (!targetFolderFiles.ContainsKey(kvp.Key))
                {
                    toCopy[kvp.Key] = kvp.Value;
                }
            }

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 3
            };

            int filesCopied = 0;
            Parallel.ForEach(toCopy, options, async void (kvp) =>
            {
                var targetFileName = Path.Combine(targetFolder.FolderName, kvp.Key);
                Directory.CreateDirectory(Path.GetDirectoryName(targetFileName));
                try
                {
                    await Task.Run(() => File.Copy(kvp.Value.FullPath, targetFileName));
                }
                catch (Exception)
                {
                    return;
                }

                Interlocked.Increment(ref filesCopied);
                lock (_synchronizationLock)
                {
                    targetFolder.AddOrReplaceFile(kvp.Key, kvp.Value);
                }
            });

            Configuration.Instance.GetFolderData(false).Init();
        }

        private static async Task CopyFromSourceToTarget(FileData sourceFile, FolderData targetFolder)
        {
            FileData? fileFound = targetFolder.Files.FirstOrDefault(x => string.Equals(x.FileName, sourceFile.FileName, StringComparison.InvariantCultureIgnoreCase));

            var copyFile = false;
            
            if (fileFound == null)
            {
                copyFile = true;
            }
            else
            {
                copyFile = fileFound.FileSize != sourceFile.FileSize;
            }

            if (!copyFile)
            {
                return;
            }
            var targetFileName = Path.Combine(targetFolder.FolderName, sourceFile.FileName);
            await Task.Run(() => File.Copy(sourceFile.FullPath, targetFileName, true));
            var myFile = ReadFiles.ReadFileFunc(targetFileName);
            lock (_synchronizationLock)
            {
                if (fileFound != null)
                {
                    targetFolder.Files.Remove(fileFound);
                }

                targetFolder.Files.Add(myFile);
            }
            //Select if file needs to be copied
            //Copy file
        }
    }
}
