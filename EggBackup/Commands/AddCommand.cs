using System;
using System.Collections.Generic;
namespace EggBackup
{
    class AddCommand
    {
        public static void Execute(string pathToConfigFileToAdd)
        {
           
            if (!File.Exists(pathToConfigFileToAdd))
            {
                Console.WriteLine($"File '{pathToConfigFileToAdd}' does not exist.");
                return;
            }

            SnapshotConfig config = SnapshotConfig.Load(pathToConfigFileToAdd);

            if (config.IsValid())
            {
                string filePath = SnapshotConfig.GetConfigPathLocation(), config.Name + "-configuration.json";
                
                config.Save(Path.Combine(SnapshotConfig.GetConfigPathLocation(), config.Name + "-configuration.json"));
                Console.WriteLine($"Backup configuration {config.Name} added successfully.");
            }
            else
            {
               Console.WriteLine($"Backup configuration has not been added");
            }
        }
    }
}