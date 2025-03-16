using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupSolution.FolderReader
{
    public class FileData
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
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
    }
}
