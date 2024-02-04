namespace EggAgent
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
                string file = Path.Combine(SnapshotConfig.GetConfigPathLocation(), config.Name + ".json"); 

                if (File.Exists(file))
                {
                    Console.WriteLine($"Backup file with name '{config.Name}' already exist.");
                    return;
                }

                string? path = Path.GetDirectoryName(file);

                if(path!=null)
                    Directory.CreateDirectory(path);
                
                config.Save(file);
                Console.WriteLine($"Backup configuration {config.Name} added successfully.");
                return;
            }
            else
            {
               Console.WriteLine($"Backup configuration has not been added");
               return;
            }
        }
    }
}