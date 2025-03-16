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
        [JsonIgnore] public FolderData? FolderData { get; set; } = null;
        [JsonIgnore] public string FullPath => Path.Combine(FolderData == null ? string.Empty : FolderData.FolderName, FileName);
        [JsonIgnore] public string FullPathWithMd5 => $"{FullPath} - {MD5Hash}";
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string MD5Hash { get; set; } = string.Empty;

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
            if (!force && !string.IsNullOrEmpty(MD5Hash))
            {
                return false;
            }
            try
            {
                await using var stream = File.OpenRead(FullPath);
                using var md5 = MD5.Create();
                var x = await md5.ComputeHashAsync(stream);
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
