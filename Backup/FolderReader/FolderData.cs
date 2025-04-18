﻿using NLog;
using System.IO;
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
        private FolderData _parent = null;
        private static readonly Lock _lockingObject = new();
        public void Reset()
        {
            _memo.Clear();
        }

        public FolderData GetOrCreateFolderData(string root, string path, FolderData parent)
        {
            var combinedPath = Path.Combine(root, path);
            lock (_lockingObject)
            {
                if (_folders.TryGetValue(combinedPath, out var fd))
                {
                    _logger.Log(LogLevel.Info, $"_folders Found {combinedPath}");
                    return fd;
                }

                _logger.Log(LogLevel.Info, $"_folders not found {combinedPath}, return new folder with [{root}, {path}]");
                var folder = new FolderData(root, path, _folders, _memo)
                {
                    _parent = parent,
                };
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
        
        public string Root { get; set; }
        public string RelativePath { get; set; }

        public List<FolderData> Folders { get; set; } = new List<FolderData>();
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
                if (folderName.StartsWith(data.FolderName))
                {
                    var returnValue = data.FindFolder(folderName);
                    lastFoundFolder = returnValue;
                    lastLookedFolder = folderName;
                    return returnValue;
                }
            }

            return null;
        }
    }
}
