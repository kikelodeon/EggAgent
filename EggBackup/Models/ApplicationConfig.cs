namespace EggBackup
{
    public class ApplicationConfig
    {
        public static EggBackupSnapshotConfig? LoadConfiguration(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<EggBackupSnapshotConfig>(json);
        }
        public void Save(string filePath)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
