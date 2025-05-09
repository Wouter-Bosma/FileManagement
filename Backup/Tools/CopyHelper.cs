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
                await CopyFromSourceToTarget(sourceFolder, targetFolder, setting, cloneHashOnCopy);
            }
            else
            {
                var valid = Configuration.Instance.GetFolderData(true).TryGetFileData(data.sourceData, out var sourceFile);
                valid = Configuration.Instance.GetFolderData(false).TryGetFolderData(data.DestinationFolder, out var targetFolder) && valid;
                if (!valid)
                {
                    return;
                }
                await CopyFromSourceToTarget(sourceFile, targetFolder, setting, cloneHashOnCopy);
            }
        }

        private static async Task CopyFromSourceToTarget(FolderData sourceFolder, FolderData targetFolder, CopySetting setting, bool cloneHashOnCopy)
        {
            var sourceFolderFiles = sourceFolder.EnumerateOverAllFiles().ToDictionary(x => x.GetRelativePath(sourceFolder.FolderName), x => x);
            var targetFolderFiles = targetFolder.EnumerateOverAllFiles().ToDictionary(x => x.GetRelativePath(targetFolder.FolderName), x => x);
            var toCopy = new Dictionary<string, FileData>();
            foreach (var kvp in sourceFolderFiles)
            {
                if (!targetFolderFiles.TryGetValue(kvp.Key, out var data) || ReplaceFile(kvp.Value, data, setting))
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
                    await Task.Run(() => File.Copy(kvp.Value.FullPath, targetFileName, true));
                }
                catch (Exception)
                {
                    return;
                }

                Interlocked.Increment(ref filesCopied);
                lock (_synchronizationLock)
                {
                    targetFolder.AddOrReplaceFile(kvp.Key, kvp.Value, cloneHashOnCopy);
                }
            });

            Configuration.Instance.GetFolderData(false).Init();
        }

        private static bool ReplaceFile(FileData sourceFile, FileData? targetFile, CopySetting setting)
        {
            if (targetFile == null)
            {
                return true;
            }
            var replace = false;
            if (setting == CopySetting.OverwriteAll || targetFile.FileSize != sourceFile.FileSize)
            {
                replace = true;
            }
            else if (setting == CopySetting.OverwriteChangedHash)
            {
                if (!string.IsNullOrEmpty(sourceFile.MD5Hash) && !string.IsNullOrEmpty(targetFile.MD5Hash))
                {
                    replace = sourceFile.MD5Hash != targetFile.MD5Hash;
                }
            }
            else if (setting == CopySetting.OverwriteChangedSourceWriteTime)
            {
                replace = sourceFile.LastWriteTime != targetFile.LastWriteTime || sourceFile.FileSize != targetFile.FileSize;
            }
            return replace;
        }

        private static async Task CopyFromSourceToTarget(FileData sourceFile, FolderData targetFolder, CopySetting setting, bool cloneHashOnCopy)
        {
            FileData? fileFound = targetFolder.Files.FirstOrDefault(x => string.Equals(x.FileName, sourceFile.FileName, StringComparison.InvariantCultureIgnoreCase));

            var copyFile = ReplaceFile(sourceFile, fileFound, setting);

            if (!copyFile)
            {
                return;
            }
            var targetFileName = Path.Combine(targetFolder.FolderName, sourceFile.FileName);
            await Task.Run(() => File.Copy(sourceFile.FullPath, targetFileName, true));
            var myFile = ReadFiles.ReadFileFunc(targetFileName);
            if (cloneHashOnCopy)
            {
                myFile.MD5Hash = sourceFile.MD5Hash;
            }
            myFile.LastWriteTime = sourceFile.LastWriteTime;
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
