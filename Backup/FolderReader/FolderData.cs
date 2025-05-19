using NLog;
using System.Text.Json.Serialization;

namespace BackupSolution.FolderReader
{
    public record MemoData(int NumChildFiles, int NumChildFolders, long ChildFileSize, DateTime UpdateTime)
    {
        public int NumChildFiles = NumChildFiles;
        public int NumChildFolders = NumChildFolders;
        public long ChildFileSize = ChildFileSize;
        public DateTime UpdateTime = DateTime.MaxValue;
    }

    public class FolderData
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private Dictionary<string, FolderData> _folders;
        private Dictionary<FolderData, MemoData> _memo;
        private Dictionary<string, FileData> _files = new();
        private FolderData _parent = null;
        private static readonly Lock _lockingObject = new(); //Problem with two different instantiations of FolderData trees. Too frequent a lock is held
        public void Reset()
        {
            _memo.Clear();
        }

        public bool TryGetFileData(string fileName, out FileData fileData)
        {
            return _files.TryGetValue(fileName, out fileData);
        }

        public FolderData GetOrCreateFolderData(string root, string path, FolderData parent, out bool found)
        {
            path = path.EndsWith('\\') ? path : path + "\\";
            var combinedPath = Path.Combine(root, path);
            lock (_lockingObject)
            {
                if (_folders.TryGetValue(combinedPath, out var fd))
                {
                    _logger.Log(LogLevel.Info, $"_folders Found {combinedPath}");
                    found = true;
                    return fd;
                }

                _logger.Log(LogLevel.Info, $"_folders not found {combinedPath}, return new folder with [{root}, {path}]");
                var folder = new FolderData(root, path, _folders, _memo)
                {
                    _parent = parent,
                };
                parent?.Folders?.Add(folder);
                found = false;
                return folder;
            }
        }

        public bool TryGetFolderData(string folderName, out FolderData fd)
        {
            folderName = folderName.EndsWith('\\') ? folderName : folderName + "\\";
            var root = Path.GetPathRoot(folderName);
            var path = Path.GetRelativePath(root, folderName);
            var result = _folders.TryGetValue(Path.Combine(root, path), out fd);
            _logger.Log(LogLevel.Info, $"TryGetFolderData[{folderName},{root},{path}] => {result}");
            return result;
        }

        private FolderData(string root, string relativePath, Dictionary<string, FolderData> folders, Dictionary<FolderData, MemoData> memo)
        {
            _folders = folders;
            _memo = memo;
            var combinedPath = Path.Combine(root, relativePath);
            Root = root;
            RelativePath = relativePath;
            _logger.Log(LogLevel.Info, $"Add[{root},{relativePath},{combinedPath}]");
            _folders[combinedPath] = this;
        }

        public bool AddOrReplaceFile(string fileName, FileData fileData, bool cloneMd5)
        {
            var tokens = fileName.Split(Path.DirectorySeparatorChar);
            var firstPath = tokens[0];
            if (tokens.Length == 1)
            {
                var toReplace = Files.FirstOrDefault(x => x.FileName == fileData.FileName);
                if (toReplace != null)
                {
                    toReplace.FileSize = fileData.FileSize;
                    toReplace.FileName = fileData.FileName;
                    toReplace.LastWriteTime = fileData.LastWriteTime;
                    toReplace.MD5Hash = cloneMd5 ? fileData.MD5Hash : string.Empty;
                }
                else
                {
                    var fd = new FileData
                    {
                        FileSize = fileData.FileSize,
                        FileName = fileData.FileName,
                        FolderData = this,
                        LastWriteTime = fileData.LastWriteTime
                    };
                    if (cloneMd5)
                    {
                        fd.MD5Hash = fileData.MD5Hash;
                    }

                    Files.Add(fd);
                }
                return true;
            }

            var targetFolder = Path.Combine(FolderName, firstPath) + Path.DirectorySeparatorChar;
            var myFolder = GetOrCreateFolderData(this.Root, Path.GetRelativePath(Root, targetFolder), this, out _);
            return myFolder.AddOrReplaceFile(Path.GetRelativePath(firstPath, fileName), fileData, cloneMd5);
        }

        public FolderData()
        {

        }

        public FolderData(string root, string relativePath) : this(root, relativePath, new(), new()) { }

        public void Init()
        {
            _folders = new Dictionary<string, FolderData>();
            _memo = new Dictionary<FolderData, MemoData>();
            _logger.Log(LogLevel.Info, $"Init base[{Root},{RelativePath}]");
            _folders.Add(Path.Combine(Root, RelativePath), this);
            RecursiveInit();
            _files.Clear();
            foreach (var file in EnumerateOverAllFiles())
            {
                _files[file.FullPath] = file;
            }
        }

        private void Init(FolderData parent, Dictionary<string, FolderData> folders, Dictionary<FolderData, MemoData> memo)
        {
            _folders = folders;
            _memo = memo;
            _parent = parent;
            _logger.Log(LogLevel.Info, $"Init[{Root},{RelativePath}]");
            _folders.Add(Path.Combine(Root, RelativePath), this);
            RecursiveInit();
        }

        private void RecursiveInit()
        {
            foreach (var fd in Folders)
            {
                fd.Init(this, _folders, _memo);
            }
        }

        public string ManualFolderName { get; set; } = string.Empty;

        [JsonIgnore]
        public string FolderName
        {
            get => string.IsNullOrEmpty(ManualFolderName) ? Path.Combine(Root, RelativePath) : ManualFolderName;
            set => ManualFolderName = value;
        }

        [JsonIgnore]
        public string LastChildPath
        {
            get
            {
                var tokens = FolderName.Split(Path.DirectorySeparatorChar);
                if (tokens?.Length > 0)
                {
                    var lastChild = tokens.Last(x => !string.IsNullOrEmpty(x));
                    return lastChild;
                }

                return string.Empty;
            }
        }

        public FolderData FindOrCreateChildFolder(string targetFolder)
        {
            var child = Folders.FirstOrDefault(x => x.LastChildPath == targetFolder);
            if (child != null)
            {
                return child;
            }

            var newTarget = Path.Combine(RelativePath, targetFolder);
            var fullPath = Path.Combine(Root, newTarget);
            Directory.CreateDirectory(fullPath);
            return GetOrCreateFolderData(Root, newTarget, this, out _);
        }

        public string Root { get; set; }
        public string RelativePath { get; set; }

        public HashSet<FolderData> Folders { get; set; } = new HashSet<FolderData>();
        public List<FileData> Files { get; set; } = new List<FileData>();
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public bool CompareFolderName(FolderData fd)
        {
            return string.Equals(fd.FolderName, FolderName, StringComparison.CurrentCultureIgnoreCase);
        }

        public IEnumerable<FileData> EnumerateOverAllFiles()
        {
            foreach (var file in Files.OrderBy(x => x.FileName))
            {
                file.FolderData = this;
                yield return file;
            }

            foreach (var file in Folders.OrderBy(x => x.FolderName).SelectMany(x => x.EnumerateOverAllFiles()))
            {
                yield return file;
            }
        }

        [JsonIgnore]
        public DateTime OldestUpdateTime
        {
            get
            {
                if (_memo.TryGetValue(this, out var data))
                {
                    return data.UpdateTime;
                }

                data = GetMemoData();
                _memo[this] = data;
                return data.UpdateTime;
            }
        }
        [JsonIgnore]
        public int ChildFolders
        {
            get
            {
                if (_memo.TryGetValue(this, out var data))
                {
                    return data.NumChildFolders;
                }

                data = GetMemoData();
                _memo[this] = data;
                return data.NumChildFolders;
            }
        }
        [JsonIgnore]
        public int ChildFiles
        {
            get
            {
                if (_memo.TryGetValue(this, out var data))
                {
                    return data.NumChildFiles;
                }

                data = GetMemoData();
                _memo[this] = data;
                return data.NumChildFiles;
            }
        }
        [JsonIgnore]
        public long ChildFileSize
        {
            get
            {
                if (_memo.TryGetValue(this, out var data))
                {
                    return data.ChildFileSize;
                }

                data = GetMemoData();
                _memo[this] = data;
                return data.ChildFileSize;
            }
        }

        private MemoData GetMemoData()
        {
            if (_memo.TryGetValue(this, out var result))
            {
                return result;
            }
            result = new MemoData(Files.Count, Folders.Count, Files.Sum(x => x.FileSize), UpdateTime);

            foreach (var folder in Folders)
            {
                var temp = folder.GetMemoData();
                result.NumChildFolders += temp.NumChildFolders;
                result.NumChildFiles += temp.NumChildFiles;
                result.ChildFileSize += temp.ChildFileSize;
                if (temp.UpdateTime < result.UpdateTime)
                {
                    result.UpdateTime = temp.UpdateTime;
                }
            }

            _memo[this] = result;
            return result;
        }

        private string lastLookedFolder = string.Empty;
        private FolderData? lastFoundFolder = null;
        public FolderData? FindFolder(string folderName)
        {
            if (lastLookedFolder == folderName)
            {
                return lastFoundFolder;
            }

            if (folderName == FolderName)
            {
                lastLookedFolder = folderName;
                lastFoundFolder = this;
                return this;
            }

            foreach (var data in Folders)
            {
                if (folderName.TrimEnd(Path.DirectorySeparatorChar).StartsWith(data.FolderName.TrimEnd(Path.DirectorySeparatorChar)))
                {
                    var returnValue = data;
                    lastFoundFolder = returnValue;
                    lastLookedFolder = folderName;
                    return returnValue;
                }
            }

            return null;
        }

        public bool RemoveFolder(FolderData fd)
        {
            Reset();
            bool result = false;
            var keyToRemove = _folders.FirstOrDefault(x => x.Value == fd).Key;
            if (!string.IsNullOrEmpty(keyToRemove))
            {
                result = _folders.Remove(keyToRemove);
            }
            result = Folders.Remove(fd) && result;
            return result;
        }
    }
}
