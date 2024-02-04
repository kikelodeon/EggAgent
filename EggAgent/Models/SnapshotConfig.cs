using Newtonsoft.Json;
namespace EggAgent
{
    public class SnapshotConfig
    {
        private string filePath { get; set; } = null!;
        public string GetFilePath()
        {
            return filePath;
        }
        void SetFilePath(string filePath)
        {
            this.filePath = filePath;
        }
        public static string GetConfigPathLocation()
        {
            return Path.Combine("/", ApplicationConfig.RootAppFolder, ApplicationConfig.ApplicationName, ApplicationConfig.ApplicationDataFolderName, ApplicationConfig.SnapshotConfigurationsFolderName);
        }

        public static List<SnapshotConfig> GetAllConfigFilesList()
        {
            string configPath = GetConfigPathLocation();
            if (!Directory.Exists(configPath))
            {
                Console.WriteLine($"Config directory '{configPath}' not found.");
                return new();
            }
            string[] files = Directory.GetFiles(configPath);
            List<SnapshotConfig> configs = files.Select(file => Load(file)).ToList();
            return configs;
        }
        private string _name = null!;
        public string Name
        {
            get => _name;
            set
            {
                if (value.Length > 20)
                {
                    value = value.Substring(0, 20);
                    Logger.Warning(nameof(value) + "cannot exceed 20 characters.");
                }
                _name = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
        public BackupConfiguration Backup { get; set; } = null!;
        public List<SnapshotResult> Log { get; set; } = null!;
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Name);
        }
        public static SnapshotConfig Load(string filePath)
        {
            string configJson = File.ReadAllText(filePath);

            try
            {
                SnapshotConfig? config = System.Text.Json.JsonSerializer.Deserialize<SnapshotConfig>(configJson);

                if (config == null || !config.IsValid())
                {
                    throw new Exception("Invalid or incomplete configuration file.");
                }
                config.SetFilePath(filePath);
                return config;
            }
            catch (System.Text.Json.JsonException ex)
            {
                throw new Exception($"Error parsing configuration file: {ex.Message}");
            }
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public void Save(string filePath)
        {
            SetFilePath(filePath);
            string json = ToJson();
            File.WriteAllText(filePath, json);
        }
        public void Remove(bool removeSnapshotsAlso =false)
        {
            if(removeSnapshotsAlso)
            {
                //TODO: REMOVE SNAPSHOTS
            }
            File.Delete(GetFilePath());
        }
    }
    public class SnapshotResult
    {
        public bool Success;
        public DateTime StartTime;
        public DateTime EndTime;
        public string? ErrorMessage;
    }
    public class BackupConfiguration
    {
        public string Source { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public List<string> Options { get; set; } = null!;
        public override string ToString()
        {
            // Override ToString to provide a formatted string representation of DDNSConfig
            return $"Source={Source},Source={Destination},Options={string.Join(", ", Options)}";
        }
    }
}
