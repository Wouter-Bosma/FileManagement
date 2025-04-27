using System.Text.Json;
using System.Text.Json.Serialization;
using Backup;
using Backup.Copier;
using BackupSolution.FolderReader;
using NLog;

namespace BackupSolution;
public class Configuration
{
    private static Logger _logger = LogManager.GetCurrentClassLogger();
    private FolderData _sourceData;
    private CopyData _copyData;
    private FolderData _targetData;
    private static Configuration _config = null;

    public List<string> Folders(bool sourceFolders)
    {
        return sourceFolders ? _sourceFolders.Folders : _targetFolders.Folders;
    }

    [JsonIgnore]
    private SelectedFoldersConfiguration _sourceFolders;
    [JsonIgnore]
    private SelectedFoldersConfiguration _targetFolders;

    [JsonIgnore] public FolderData SourceData => _sourceData;
    [JsonIgnore] public FolderData TargetData => _targetData;

    private static string _configurationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FileManager");
    [JsonIgnore]
    public string MainConfigurationFileName => Path.Combine(_configurationFolder, $"{Environment.MachineName}.Config.json");
    [JsonIgnore]
    public string TargetFoldersConfigurationFileName => Path.Combine(_configurationFolder, $"{Environment.MachineName}.TargetFoldersConfig.json");
    [JsonIgnore]
    public string SourceConfigurationFileName => Path.Combine(_configurationFolder, $"{Environment.MachineName}.SourceData.json");
    [JsonIgnore]
    public string CopyConfigurationFileName => Path.Combine(_configurationFolder, $"{Environment.MachineName}.CopyData.json");
    [JsonIgnore]
    public string TargetConfigurationFileName => Path.Combine(_configurationFolder, $"{Environment.MachineName}.TargetData.json");



    public static Configuration Instance => _config ?? (_config = new Configuration(true));

    public Configuration(bool init = false)
    {
        if (init)
        {
            CreateTargetFolder();
            Load();
            _sourceData ??= new FolderData("\\\\", string.Empty);
            _targetData ??= new FolderData("\\\\", string.Empty);
        }
    }

    private void CreateTargetFolder()
    {
        Directory.CreateDirectory(_configurationFolder);
    }

    public void Save()
    {
        _logger.Log(LogLevel.Info, "Saving config");
        File.WriteAllText(MainConfigurationFileName, JsonSerializer.Serialize(_sourceFolders));
        File.WriteAllText(TargetFoldersConfigurationFileName, JsonSerializer.Serialize(_targetFolders));
        File.WriteAllText(SourceConfigurationFileName, JsonSerializer.Serialize(_sourceData));
        File.WriteAllText(CopyConfigurationFileName, JsonSerializer.Serialize(_copyData));
        File.WriteAllText(TargetConfigurationFileName, JsonSerializer.Serialize(_targetData));
    }

    public void Load()
    {
        _logger.Log(LogLevel.Info, "Loading config");
        SelectedFoldersConfiguration? myConfig;
        if (File.Exists(MainConfigurationFileName) && (myConfig = JsonSerializer.Deserialize<SelectedFoldersConfiguration>(File.ReadAllText(MainConfigurationFileName))) != null)
        {
            _sourceFolders = myConfig;
        }
        else
        {
            _sourceFolders = new SelectedFoldersConfiguration();
        }
        if (File.Exists(TargetFoldersConfigurationFileName) && (myConfig = JsonSerializer.Deserialize<SelectedFoldersConfiguration>(File.ReadAllText(TargetFoldersConfigurationFileName))) != null)
        {
            _targetFolders = myConfig;
        }
        else
        {
            _targetFolders = new SelectedFoldersConfiguration();
        }

        FolderData? temp = null;
        if (File.Exists(SourceConfigurationFileName) && (temp = JsonSerializer.Deserialize<FolderData>(File.ReadAllText(SourceConfigurationFileName))) != null)
        {
            temp.Init();
            _sourceData = temp;
        }
        else
        {
            _sourceData = new FolderData(@"\\", "");
        }
        if (File.Exists(TargetConfigurationFileName) && (temp = JsonSerializer.Deserialize<FolderData>(File.ReadAllText(TargetConfigurationFileName))) != null)
        {
            temp.Init();
            _targetData = temp;
        }
        else
        {
            _targetData = new FolderData(@"\\","");
        }
        _logger.Log(LogLevel.Info, "Loading config finished");
    }
}
