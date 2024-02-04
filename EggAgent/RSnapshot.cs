using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
namespace EggAgent
{
    public class RSnapshot
    {
        static void CheckInstalled()
        {
            if (!IsRsnapshotInstalled())
            {
                Console.WriteLine("rsnapshot is not installed. Installing...");

                InstallRsnapshot();


            }
            else
            {
                Console.WriteLine("rsnapshot is already installed.");
            }
        }


        static void InstallRsnapshot()
        {
            if (IsLinuxOrMacOS())
            {
                bool successUpdate = RunShellCommand("sudo apt-get update", print: true);

                if (successUpdate)
                {
                    bool successInstall = RunShellCommand("sudo apt-get install -y rsnapshot", print: true);

                    if (successInstall)
                    {
                        Console.WriteLine("rsnapshot installed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to install rsnapshot.");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to update package lists. rsnapshot installation aborted.");
                }
            }
            else if (IsWindows())
            {
                // Add Windows installation commands if needed
            }
            Console.WriteLine("rsnapshot installed successfully.");
        }

        static bool IsRsnapshotInstalled()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();

            // Check if rsnapshot is installed
            process.StandardInput.WriteLine("command -v rsnapshot");
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return !string.IsNullOrEmpty(result);
        }

        static bool IsLinuxOrMacOS()
        {
            int platformID = (int)Environment.OSVersion.Platform;
            return platformID == 4 || platformID == 6 || platformID == 128;
        }

        static bool IsWindows()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
        }


        static bool RunShellCommand(string command, bool print = false)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();

            // Run the shell command
            process.StandardInput.WriteLine(command);
            process.StandardInput.Close();

            // Wait for the command to finish
            process.WaitForExit();

            // Print the output if requested
            if (print)
            {
                string output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
            }

            // Return true if the exit code is 0 (success), otherwise false
            return process.ExitCode == 0;
        }
    }
}
