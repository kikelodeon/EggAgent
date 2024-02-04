using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using EggAgent;



class Program
{
    static void Main(string[] args)
    {
        const string mutexName = "Global\\EggAgentMutex";
        using (Mutex mutex = new Mutex(true, mutexName, out bool createdNew))
        {
            if (args.Length == 0 || args.Length == 1 && (args[0].Equals("-h", StringComparison.OrdinalIgnoreCase) || args[0].Equals("--help", StringComparison.OrdinalIgnoreCase)))
            {
                HelpCommand.Execute();
                return;
            }

            if (args.Length == 1 && (args[0].Equals("-l", StringComparison.OrdinalIgnoreCase) || args[0].Equals("--list", StringComparison.OrdinalIgnoreCase)))
            {
                ListCommand.Execute();
                return;
            }

            if ((args.Length == 1 || args.Length == 2) && (args[0].Equals("-a", StringComparison.OrdinalIgnoreCase) || args[0].Equals("--add", StringComparison.OrdinalIgnoreCase)))
            {
                if (args.Length == 2 && !string.IsNullOrEmpty(args[1]))
                {
                    AddCommand.Execute(args[1]);
                    return;
                }
                else
                {
                    Console.WriteLine("Select the path of the backup config that you want to add");
                    return;
                }
            }

            if ((args.Length == 1 || args.Length == 2) && (args[0].Equals("-r", StringComparison.OrdinalIgnoreCase) || args[0].Equals("--remove", StringComparison.OrdinalIgnoreCase)))
            {
                if (args.Length == 2 && !string.IsNullOrEmpty(args[1]))
                {
                    RemoveCommand.Execute(args[1]);
                    return;
                }
                else
                {
                    Console.WriteLine("Select the name of the backup config that you want to remove");
                    return;
                }
            }
            if (!createdNew)
            {
                Console.WriteLine("Another instance of EggAgent is already running. Exiting...");
                return;
            }

            //No actions defined or unknown command.
            Console.WriteLine("Unknown command.");
            Console.WriteLine("Run --help for see options");
            return;

        }
    }
}