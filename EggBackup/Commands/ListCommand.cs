using System;
using System.Collections.Generic;
namespace EggBackup
{
    class ListCommand
    {

        public static void Execute()
        {
            List<SnapshotConfig> configs = SnapshotConfig.GetAllConfigFilesList();
            if (configs.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"Name \\t Path");
                Console.ResetColor();
                Console.WriteLine();
                for (int j = 0; j < configs.Count; j++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(configs[j].Name);
                    Console.Write($" \\t");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(configs[j].GetFilePath());
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
        }
    }
}