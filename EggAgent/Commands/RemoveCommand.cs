namespace EggAgent
{
    class RemoveCommand
    {
        public static void Execute(string name)
        {
            List<SnapshotConfig> configs = SnapshotConfig.GetAllConfigFilesList();
            SnapshotConfig? config = configs.Find(x => x.Name == name);

            if (config == null)
            {
                Console.WriteLine($"Backup '{config}' does not exist. Use --list to check existing backups");
                return;
            }
            Console.WriteLine($"Are you sure you want to remove the backup configuration '{name}'?");
            Console.Write("Enter 'yes' to confirm, or 'no' to cancel: ");
            string? userResponse = Console.ReadLine();

            if (string.Equals(userResponse, "yes", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Do you want to remove existing snapshots of this backup config? (yes/no): ");
                string? removeSnapshotsResponse = Console.ReadLine();

                if (string.Equals(removeSnapshotsResponse, "yes", StringComparison.OrdinalIgnoreCase))
                {
                    // Remove both the configuration and existing snapshots.
                    Console.WriteLine("Removing both the configuration and existing snapshots..");
                    config.Remove(true);
                }
                else if (string.Equals(removeSnapshotsResponse, "no", StringComparison.OrdinalIgnoreCase))
                {
                    // Remove only the configuration, keep existing snapshots.
                     Console.WriteLine("Removing only the configuration, keep existing snapshots..");
                   config.Remove(false);
                }
                else
                {
                    Console.WriteLine("Invalid response. Cancelling removal.");
                }
            }
            else if (string.Equals(userResponse, "no", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Cancelling removal.");
            }
            else
            {
                Console.WriteLine("Invalid response. Cancelling removal.");
            }
        }
    }
}