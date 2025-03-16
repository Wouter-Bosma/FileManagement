using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackupSolution.FolderReader;

namespace BackupSolution.Serialization
{
    internal interface ISerialization
    {
        public Task SerializeAsync(string endpoint, FolderData fd);
        public Task<FolderData> DeserializeAsync(string endpoint);
    }
}
