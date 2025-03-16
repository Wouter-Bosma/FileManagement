using System.Text.Json;
using System.Text.Json.Serialization;
using BackupSolution.FolderReader;
using Microsoft.Win32;

namespace BackupSolution;
class Configuration
{
    private static string _configurationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FileManager");
    [JsonIgnore]
    public string MainConfigurationFileName => Path.Combine(_configurationFolder, "Config.json");
    [JsonIgnore]
    public string SourceConfigurationFileName => Path.Combine(_configurationFolder, "SourceData.json");
    
    public List<string> Folders { get; set; } = new();

    public Configuration()
    {
        CreateTargetFolder();
    }

    private void CreateTargetFolder()
    {
        Directory.CreateDirectory(_configurationFolder);
    }

    public void Save(FolderData sourceData)
    {
        File.WriteAllText(MainConfigurationFileName, JsonSerializer.Serialize(this));
        File.WriteAllText(SourceConfigurationFileName, JsonSerializer.Serialize(sourceData));
    }

    public void Load(ref FolderData sourceData)
    {
        if (File.Exists(MainConfigurationFileName))
        {
            var myConfig = JsonSerializer.Deserialize<Configuration>(File.ReadAllText(MainConfigurationFileName));
            if (myConfig != null)
            {
                this.Folders = myConfig.Folders;
            }
        }

        FolderData? temp = null;
        if (File.Exists(SourceConfigurationFileName) && (temp = JsonSerializer.Deserialize<FolderData>(File.ReadAllText(SourceConfigurationFileName))) != null)
        {
            temp.Init();
            sourceData = temp;
        }
        else
        {
            sourceData = new FolderData("\\\\", string.Empty);
        }
    }
}
