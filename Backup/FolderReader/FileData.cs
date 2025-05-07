using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackupSolution.FolderReader
{
    public class FileData
    {
        private readonly Lock _fileDataLock = new(); //Todo: ReaderWriteLockSlim?
        private string _md5Hash = string.Empty;
        private string _fileName = string.Empty;
        private long _fileSize = 0;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        [JsonIgnore] public FolderData? FolderData { get; set; } = null;
        [JsonIgnore] public string FullPath => Path.Combine(FolderData == null ? string.Empty : FolderData.FolderName, FileName);
        [JsonIgnore] public string FullPathWithMd5 => $"{FullPath} - {MD5Hash}";

        public string GetRelativePath(string basePath)
        {
            var tempBasePath = basePath.ToLower().TrimEnd(Path.DirectorySeparatorChar);
            var fullPath = FullPath;
            if (FullPath.ToLower().StartsWith(tempBasePath))
            {
                return fullPath.Substring(tempBasePath.Length + 1);
            }

            return fullPath;
        }

        public string FileName
        {
            get { lock(_fileDataLock) {return _fileName;} }
            set { lock (_fileDataLock) { _fileName = value; } } // Needs to be public for serialization
        }

        public long FileSize
        {
            get { lock (_fileDataLock) { return _fileSize; } }
            set { lock (_fileDataLock) { _fileSize = value; } }
        }

        public string MD5Hash
        {
            get { lock (_fileDataLock) { return _md5Hash; } }
            set { lock (_fileDataLock) { _md5Hash = value; } }
        }

        public DateTime LastWriteTime { get; set; }
        
        public override bool Equals(object other)
        {
            return Equals(other as FileData);
        }

        public bool Equals(FileData? other)
        {
            if (other == null)
            {
                return false;
            }

            return other.FileName == FileName && other.FileSize == FileSize && other.LastWriteTime == LastWriteTime;
        }

        public async ValueTask<bool> CalculateMd5Hash(bool force = false)
        {
            return await CalculateMd5Hash(CancellationToken.None, force);
        }

        public async ValueTask<bool> CalculateMd5Hash(CancellationToken ct, bool force = false)
        {
            _logger.Log(LogLevel.Info, $"Calculate hash for {FullPath} with Force=={force}");
            if (!force && !string.IsNullOrEmpty(MD5Hash))
            {
                return false;
            }
            try
            {
                await using var stream = File.OpenRead(FullPath); //TODO: Is await using useful?
                using var md5 = MD5.Create();
                var x = await md5.ComputeHashAsync(stream, ct);
                if (ct.IsCancellationRequested)
                {
                    return false;
                }
                MD5Hash = ToHex(x);
            }
            catch
            {
                MD5Hash = string.Empty;
            }

            return true;
        }

        public string ToHex(byte[] bytes)
        {
            return string.Concat(bytes.Select(b => b.ToString("X2")));
        }
    }
}
