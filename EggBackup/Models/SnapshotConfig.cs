namespace EggBackup
{
    public class EggBackupSnapshotConfig
    {
        public string SnapshotRoot { get; set; } =null!;
        public int NoCreateRoot { get; set; }
        public string CmdCp { get; set; } =null!;
        public string CmdRm { get; set; } =null!;
        public string CmdRsync { get; set; } =null!;
        public string CmdSsh { get; set; } =null!;
        public string CmdLogger { get; set; } =null!;
        public string CmdDu { get; set; } =null!;
        public Dictionary<string, int> Interval { get; set; } =null!;
        public int Verbose { get; set; }
        public int LogLevel { get; set; }
        public string LogFile { get; set; } =null!;
        public string LockFile { get; set; } =null!;
        public string SshArgs { get; set; } =null!;
        public List<string> Include { get; set; } =null!;
        public List<string> Exclude { get; set; } =null!;
        public string IncludeFile { get; set; } =null!;
        public string ExcludeFile { get; set; } =null!;
        public BackupConfiguration Backup { get; set; } =null!;

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

    public class BackupConfiguration
    {
        public string Source { get; set; } =null!;
        public string Destination { get; set; } =null!;
        public List<string> Options { get; set; } =null!;
    }
}
