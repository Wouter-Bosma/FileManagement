using BackupSolution.FolderReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackupSolution.Serialization
{
    internal class DatabaseSerializer : ISerialization
    {

        public async Task SerializeAsync(string endpoint, FolderData fd)
        {
            string jsonString = JsonSerializer.Serialize(fd);
            await File.WriteAllTextAsync(endpoint, jsonString);
        }

        public async Task<FolderData> DeserializeAsync(string endpoint)
        {
            var intermediate = await File.ReadAllTextAsync(endpoint);
            return JsonSerializer.Deserialize<FolderData>(intermediate);

            /*
            var intermediate = await File.ReadAllTextAsync(endpoint);
            return string.IsNullOrEmpty(intermediate)
                ? new FolderData()
                : JsonSerializer.Deserialize<FolderData>(intermediate) ?? new FolderData();*/
        }
    }
}
