using System;
using System.Collections.Generic;
namespace EggAgent
{
    class ListCommand
    {

        public static void Execute()
        {
            Console.WriteLine("Running --list command ...");
            List<SnapshotConfig> configs = SnapshotConfig.GetAllConfigFilesList();
            if (configs.Count > 0)
            {
                for (int j = 0; j < configs.Count; j++)
                {
                   Console.WriteLine(configs[j].ToJson());
                }
                Console.WriteLine($"Displaying {configs.Count} results.");
            }
            else
            {
                Console.WriteLine($"No results to display");
            }
        }
    }
}