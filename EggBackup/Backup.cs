namespace EggBackup
{
    public class Backup
    {
        public static void Run(SnapshotConfig config)
        {
            Console.WriteLine("Processing: " + config.ToString());
        }
    }
}
