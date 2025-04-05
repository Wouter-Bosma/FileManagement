using Backup.Copier;
using BackupSolution.FolderReader;
using System.Text.Json;
using System.Text.Json.Serialization;
using NLog;

namespace BackupSolution;
class Configuration
{
    private static Logger _logger = LogManager.GetCurrentClassLogger();
    private FolderData sourceData;
    private CopyData copyData;
    private FolderData targetData;
    [JsonIgnore]
    public FolderData Root => sourceData;

    private static string _configurationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FileManager");
    [JsonIgnore]
    public string MainConfigurationFileName => Path.Combine(_configurationFolder, $"{Environment.MachineName}.Config.json");
    [JsonIgnore]
    public string SourceConfigurationFileName => Path.Combine(_configurationFolder, $"{Environment.MachineName}.SourceData.json");
    [JsonIgnore]
    public string CopyConfigurationFileName => Path.Combine(_configurationFolder, $"{Environment.MachineName}.CopyData.json");
    [JsonIgnore]
    public string TargetConfigurationFileName => Path.Combine(_configurationFolder, $"{Environment.MachineName}.TargetData.json");

    public List<string> Folders { get; set; } = new();

    public Configuration()
    {
        CreateTargetFolder();
    }

    private void CreateTargetFolder()
    {
        Directory.CreateDirectory(_configurationFolder);
    }

    public void Save()
    {
        _logger.Log(LogLevel.Info, "Saving config");
        File.WriteAllText(MainConfigurationFileName, JsonSerializer.Serialize(this));
        File.WriteAllText(SourceConfigurationFileName, JsonSerializer.Serialize(sourceData));
        File.WriteAllText(CopyConfigurationFileName, JsonSerializer.Serialize(copyData));
        File.WriteAllText(CopyConfigurationFileName, JsonSerializer.Serialize(targetData));
    }

    public void Load()
    {
        _logger.Log(LogLevel.Info, "Loading config");
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
        if (File.Exists(TargetConfigurationFileName) && (temp = JsonSerializer.Deserialize<FolderData>(File.ReadAllText(TargetConfigurationFileName))) != null)
        {
            temp.Init();
            targetData = temp;
        }
        else
        {
            targetData = new FolderData("\\\\", string.Empty);
        }
        _logger.Log(LogLevel.Info, "Loading config finished");
    }
}
