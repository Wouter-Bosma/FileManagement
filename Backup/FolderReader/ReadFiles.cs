using NLog;
using System.Collections.Concurrent;

namespace BackupSolution.FolderReader;

internal class ReadFiles
{ 
    private static Logger _logger = LogManager.GetCurrentClassLogger();
    private static Lock lockingObject = new Lock();

    public static async Task<FolderData> ReadFilesRecursive(string folderName, FolderData rootFolder, bool refreshData)
    {
        _logger.Log(LogLevel.Info, $"ReadFilesRecursive {folderName} on {rootFolder.FolderName}");
        return await ReadFilesRecursive(folderName, rootFolder, rootFolder, refreshData);
    }

    public static async Task<FolderData> ReadFilesRecursive(string folderName, FolderData rootFolder, FolderData parent, bool refreshData = false)
    {
        folderName = folderName.EndsWith('\\') ? folderName : folderName + "\\";
        var root = Path.GetPathRoot(folderName);
        var path = Path.GetRelativePath(root, folderName);
        var result = rootFolder.GetOrCreateFolderData(root, path, parent, out bool found);
        _logger.Log(LogLevel.Info, $"Initial folder data found = {root}-{path}-{parent}-{result.Folders.Count}");
        if (found && !refreshData)
        {
            return result;
        }
        
        IEnumerable<string> folders;
        try
        {
            folders = Directory.EnumerateDirectories(folderName);
        }
        catch (Exception ex)
        {
            folders = new List<string>();
        }

        Dictionary<string, FolderData> existingFolders;
        lock (lockingObject)
        {
            existingFolders = result.Folders.ToDictionary(x => x.FolderName.TrimEnd(Path.DirectorySeparatorChar), x => x);
        }

        var mySetOfFolders = new ConcurrentDictionary<string, byte>();
        await Parallel.ForEachAsync(folders, new ParallelOptions(), async (folder, _) =>
        {
            mySetOfFolders.TryAdd(folder.TrimEnd(Path.DirectorySeparatorChar), 0);
            if (!refreshData && result.Folders.Any(f => f.FolderName.TrimEnd(Path.DirectorySeparatorChar) == folder.TrimEnd(Path.DirectorySeparatorChar)))
            {
                return;
            }
            var item = await ReadFilesRecursive(folder, rootFolder, result, refreshData);
            lock (lockingObject)
            {
                if (!result.Folders.Any(x => x.CompareFolderName(item)))
                {
                    result.Folders.Add(item);
                }
            }
        });
        bool reset = false;

        foreach (var folder in existingFolders.Where(x => mySetOfFolders.ContainsKey(x.Key)))
        {
            lock (lockingObject)
            {
                reset = result.RemoveFolder(folder.Value) || reset;
            }
        }

        try
        {
            Dictionary<string, FileData> preExistingFiles;
            lock (lockingObject)
            {
                preExistingFiles = result.Files.ToDictionary(x => x.FileName, x=> x);
            }
            _logger.Log(LogLevel.Info, $"Before parsing files in folder = {root}-{path}-{parent}-{result.Folders.Count}");

            var filesFound = new HashSet<string>();
            foreach (var file in Directory.EnumerateFiles(folderName))
            {
                var item = ReadFile(file);
                filesFound.Add(item.FileName);
                lock (lockingObject)
                {
                    var existingFileData = result.Files.FirstOrDefault(x => x.FileName.Equals(item.FileName, StringComparison.CurrentCultureIgnoreCase));
                    if (existingFileData == null)
                    {
                        result.Files.Add(item);
                    }
                    else if (refreshData && !existingFileData.Equals(item))
                    {
                        existingFileData.MD5Hash = string.Empty;
                    }
                }
            }

            foreach (var file in preExistingFiles.Where(file => !filesFound.Contains(file.Key)))
            {
                lock (lockingObject)
                {
                    reset = result.Files.Remove(file.Value) || reset;
                }
            }

            if (reset)
            {
                lock (lockingObject)
                {
                    result.Reset();
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

