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
        public static async Task CopyFromSourceToTarget(CopyData data, CopySetting setting, bool cloneHashOnCopy, CopyInfo copyInfo)
        {
            if (data.isFolderSource)
            {
                var valid = Configuration.Instance.GetFolderData(true).TryGetFolderData(data.sourceData, out var sourceFolder);
                var targetChildPath = sourceFolder.LastChildPath;
                valid = valid && !string.IsNullOrEmpty(targetChildPath);
                valid = Configuration.Instance.GetFolderData(false).TryGetFolderData(data.DestinationFolder, out var targetFolder) && valid;
                targetFolder = targetFolder.FindOrCreateChildFolder(targetChildPath);
                
                if (!valid)
                {
                    return;
                }
                await CopyFromSourceToTarget(sourceFolder, targetFolder, setting, cloneHashOnCopy, copyInfo);
            }
            else
            {
                var valid = Configuration.Instance.GetFolderData(true).TryGetFileData(data.sourceData, out var sourceFile);
                valid = Configuration.Instance.GetFolderData(false).TryGetFolderData(data.DestinationFolder, out var targetFolder) && valid;
                if (!valid)
                {
                    return;
                }
                await CopyFromSourceToTarget(sourceFile, targetFolder, setting, cloneHashOnCopy, copyInfo);
            }
        }

        private static async Task ExecuteCopy(FolderData targetFolder, FileData sourceFile, string fileName, CopyInfo copyInfo, bool cloneHashOnCopy)
        {
            var targetFileName = Path.Combine(targetFolder.FolderName, fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(targetFileName));
            try
            {
                copyInfo.FileStart(sourceFile.FullPath);
                //await Task.Run(() => FileHelper.Copy(sourceFile.FullPath, targetFileName, true));
                await FileHelper.CopyAsync(sourceFile.FullPath, targetFileName, true);
                copyInfo.FileFinish(sourceFile.FullPath);
            }
            catch (Exception)
            {
                return;
            }

            lock (_synchronizationLock)
            {
                targetFolder.AddOrReplaceFile(fileName, sourceFile, cloneHashOnCopy);
            }
        }

        private static async Task CopyFromSourceToTarget(FolderData sourceFolder, FolderData targetFolder, CopySetting setting, bool cloneHashOnCopy, CopyInfo copyInfo)
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
            copyInfo.FilesToCopy = toCopy.Count;
            var copyTasks = new List<Task>();
            foreach(var kvp in toCopy)
            {
                copyTasks.Add(ExecuteCopy(targetFolder, kvp.Value, kvp.Key, copyInfo, cloneHashOnCopy));
                if (copyTasks.Count > 2)
                {
                    var task = await Task.WhenAny(copyTasks);
                    copyTasks.Remove(task);
                }
            }

            await Task.WhenAll(copyTasks);

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

        private static async Task CopyFromSourceToTarget(FileData sourceFile, FolderData targetFolder, CopySetting setting, bool cloneHashOnCopy, CopyInfo copyInfo)
        {
            FileData? fileFound = targetFolder.Files.FirstOrDefault(x => string.Equals(x.FileName, sourceFile.FileName, StringComparison.InvariantCultureIgnoreCase));

            var copyFile = ReplaceFile(sourceFile, fileFound, setting);

            if (!copyFile)
            {
                return;
            }

            copyInfo.FilesToCopy = 1;
            copyInfo.FileStart(sourceFile.FullPath);
            var targetFileName = Path.Combine(targetFolder.FolderName, sourceFile.FileName);
            //await Task.Run(() => FileHelper.Copy(sourceFile.FullPath, targetFileName, true));
            await FileHelper.CopyAsync(sourceFile.FullPath, targetFileName, true); 
            copyInfo.FileFinish(sourceFile.FullPath);
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
