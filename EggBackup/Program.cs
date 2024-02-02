using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using EggBackup;

class Program
{
   static void Main(string[] args)
    {
        const string mutexName = "Global\\EggBackupMutex";

        using (Mutex mutex = new Mutex(true, mutexName, out bool createdNew))
        {
            if (args.Length == 0 || args.Length == 1 && args[0].Equals("--help", StringComparison.OrdinalIgnoreCase))
            {
                HelpCommand.Execute();
                return;
            }
            if (!createdNew)
            {
                Console.WriteLine("Another instance of EggBackup is already running. Exiting...");
                return;
            }

            string configFolderPath = args[0];

            try
            {
                Backup.Run();
            }
            catch (Exception ex)
            {
                Logger.LogCritical($"Error: {ex.Message}");
                Environment.Exit(1);
            }
        }
    }
}