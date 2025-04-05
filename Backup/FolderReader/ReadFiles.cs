using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupSolution.FolderReader;

internal class ReadFiles
{ 
    private static Logger _logger = LogManager.GetCurrentClassLogger();
    private static Lock lockingObject = new Lock();

    public static async Task<FolderData> ReadFilesRecursive(string folderName, FolderData rootFolder)
    {
        _logger.Log(LogLevel.Info, $"ReadFilesRecursive {folderName} on {rootFolder.FolderName}");
        return await ReadFilesRecursive(folderName, rootFolder, rootFolder);
    }

    public static async Task<FolderData> ReadFilesRecursive(string folderName, FolderData rootFolder, FolderData parent)
    {
        folderName = folderName.EndsWith('\\') ? folderName : folderName + "\\";
        var root = Path.GetPathRoot(folderName);
        var path = Path.GetRelativePath(root, folderName);
        var result = rootFolder.GetOrCreateFolderData(root, path, parent);
        
        IEnumerable<string> folders;
        try
        {
            folders = Directory.EnumerateDirectories(folderName);
        }
        catch (Exception ex)
        {
            folders = new List<string>();
        }

        await Parallel.ForEachAsync(folders, new ParallelOptions(), async (folder, _) =>
        {
            var item = await ReadFilesRecursive(folder, rootFolder, result);
            lock (lockingObject)
            {
                if (!result.Folders.Any(x => x.CompareFolderName(item)))
                {
                    result.Folders.Add(item);
                }
            }
        });

        try
        {
            foreach (var file in Directory.EnumerateFiles(folderName))
            {
                var item = ReadFile(file);
                lock (lockingObject)
                {
                    var existingFileData = result.Files.FirstOrDefault(x => x.FileName.Equals(item.FileName, StringComparison.CurrentCultureIgnoreCase));
                    if (existingFileData == null)
                    {
                        result.Files.Add(item);
                    }
                    else
                    {
                        if (!existingFileData.Equals(item))
                        {
                            existingFileData.MD5Hash = string.Empty;
                        }
                    }
                }
            }
        }
        catch
        {

        }
        return result;
    }

    private static FileData ReadFileFunc(string fileName)
    {
        var fi = new FileInfo(fileName);
        var result = new FileData()
        {
            FileName = Path.GetFileName(fileName),
            FileSize = fi.Length,
            LastWriteTime = fi.LastWriteTime,
        };
        return result;
    }

    private static FileData ReadFile(string fileName)
    {
        return ReadFileFunc(fileName);
    }
}

