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
    internal static class CopyHelper
    {
        public static void CopyFromSourceToTarget(CopyData data)
        {
            if (data.isFolderSource)
            {
                var valid = Configuration.Instance.GetFolderData(true).TryGetFolderData(data.sourceData, out var sourceFolder);
                valid = Configuration.Instance.GetFolderData(false).TryGetFolderData(data.DestinationFolder, out var targetFolder) && valid;
                if (!valid)
                {
                    return;
                }
                CopyFromSourceToTarget(sourceFolder, targetFolder);
            }
            else
            {
                var valid = Configuration.Instance.GetFolderData(true).TryGetFileData(data.sourceData, out var sourceFile);
                valid = Configuration.Instance.GetFolderData(false).TryGetFolderData(data.DestinationFolder, out var targetFolder) && valid;
                if (!valid)
                {
                    return;
                }
                CopyFromSourceToTarget(sourceFile, targetFolder);
            }
        }

        private static void CopyFromSourceToTarget(FolderData sourceFolder, FolderData targetFolder)
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

            foreach (var kvp in toCopy)
            {
                var targetFileName = Path.Combine(targetFolder.FolderName, kvp.Key);
                Directory.CreateDirectory(Path.GetDirectoryName(targetFileName));
                File.Copy(kvp.Value.FullPath, targetFileName, true);
                targetFolder.AddOrReplaceFile(kvp.Key, kvp.Value);
                //Todo: Add newly added file to the folderdata hierachy
            }
            Configuration.Instance.GetFolderData(false).Init();
        }

        private static void CopyFromSourceToTarget(FileData sourceFile, FolderData targetFolder)
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
            File.Copy(sourceFile.FullPath, targetFileName);
            var myFile = ReadFiles.ReadFileFunc(targetFileName);
            if (fileFound != null)
            {
                targetFolder.Files.Remove(fileFound);
            }
            targetFolder.Files.Add(myFile);
            //Select if file needs to be copied
            //Copy file
        }
    }
}
