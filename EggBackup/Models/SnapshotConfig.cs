using System.Text.Json;

namespace EggBackup
{
    public class SnapshotConfig
    {
        private string filePath {get;set;} = null!;
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
        public string Name { get; set; } = null!;
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
                SnapshotConfig? config = JsonSerializer.Deserialize<SnapshotConfig>(configJson);

                if (config == null || !config.IsValid())
                {
                    throw new Exception("Invalid or incomplete configuration file.");
                }
                config.SetFilePath(filePath);
                return config;
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error parsing configuration file: {ex.Message}");
            }
        }
        public override string ToString()
        {
            // Override ToString to provide a formatted string representation of DDNSConfig
            return $"Name={Name}, ......" + Backup.ToString(); ;
        }

        public void Save(string filePath)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
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
