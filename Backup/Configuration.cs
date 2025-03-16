using System.Text.Json;
using BackupSolution.FolderReader;

namespace BackupSolution;
class Configuration
{
    public List<string> Folders { get; set; } = new();

    public void Save(FolderData sourceData)
    {
        File.WriteAllText(@"M:\Config.json", JsonSerializer.Serialize(this));
        File.WriteAllText(@"M:\SourceData.json", JsonSerializer.Serialize(sourceData));
    }

    public void Load(ref FolderData sourceData)
    {
        if (File.Exists(@"M:\Config.json"))
        {
            var myConfig = JsonSerializer.Deserialize<Configuration>(File.ReadAllText(@"M:\Config.json"));
            if (myConfig != null)
            {
                this.Folders = myConfig.Folders;
            }
        }

        if (File.Exists(@"M:\SourceData.json"))
        {
            var temp = JsonSerializer.Deserialize<FolderData>(File.ReadAllText(@"M:\SourceData.json"));
            if (temp != null)
            {
                temp.Init();
                sourceData = temp;
            }
            else
            {
                sourceData = new FolderData("\\\\", string.Empty);
            }
        }
        else
        {
            sourceData = new FolderData("\\\\", string.Empty);
        }
    }
}
