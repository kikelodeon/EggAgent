using System;
using System.Collections.Generic;
using System.Diagnostics;

class PackageManager
{
    public string Name { get; }
    public string Description { get; }
    public PackageManager(string name, string description)
    {
        Name = name;
        Description = description;

    }
    public static List<PackageManager> GetAvailables()
    {
        List<PackageManager> availablePackageManagers = new List<PackageManager>();

        if (CommandAvailable("apt-get"))
        {
            availablePackageManagers.Add(new PackageManager("apt-get", "(Debian/Ubuntu)"));
        }
        if (CommandAvailable("dnf"))
        {
            availablePackageManagers.Add(new PackageManager("dnf", "(Fedora)"));
        }
        if (CommandAvailable("zypper"))
        {
            availablePackageManagers.Add(new PackageManager("zypper", "(openSUSE)"));
        }
        if (CommandAvailable("yum"))
        {
            availablePackageManagers.Add(new PackageManager("yum", "(RHEL/CentOS)"));
        }
        if (CommandAvailable("pacman"))
        {
            availablePackageManagers.Add(new PackageManager("pacman", "(Arch Linux)"));
        }
        if (CommandAvailable("apk"))
        {
            availablePackageManagers.Add(new PackageManager("apk", "(Alpine Linux)"));
        }
        if (CommandAvailable("eopkg"))
        {
            availablePackageManagers.Add(new PackageManager("eopkg", "(Solus)"));
        }
        if (CommandAvailable("xbps-install"))
        {
            availablePackageManagers.Add(new PackageManager("xbps-install", "(Void Linux)"));
        }
        if (CommandAvailable("brew"))
        {
            availablePackageManagers.Add(new PackageManager("brew", "(macOS)"));
        }
        if (CommandAvailable("port"))
        {
            availablePackageManagers.Add(new PackageManager("port", "(macOS)"));
        }

        return availablePackageManagers;
    }


    public static bool InstallPackage(string packageName)
    {
        var packageManagers = GetAvailables();
        if (packageManagers.Count == 0)
        {
            Console.WriteLine("No available package managers found in your system.");
            return false;
        }

        foreach (var packageManager in packageManagers)
        {
            Console.WriteLine($"Trying to install '{packageName}' using '{packageManager.Name}'...");

            if (RunShellCommand($"sudo {packageManager.Name} install -y {packageName}", print: true))
            {
                Console.WriteLine($"Package '{packageName}' installed successfully using '{packageManager.Name}'.");
                return true;
            }
            else
            {
                Console.WriteLine($"Failed to install '{packageName}' using '{packageManager.Name}'.");
            }
        }

        Console.WriteLine($"Failed to install '{packageName}' with any available package manager.");
        return false;
    }


    private static bool CommandAvailable(string command)
    {
        try
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

            // Check if the command is executable
            process.StandardInput.WriteLine($"command -v {command} && type {command}");
            process.StandardInput.Close();

            // Wait for the command to finish
            process.WaitForExit();

            // Return true if the exit code is 0 (success), otherwise false
            return process.ExitCode == 0;
        }
        catch
        {
            return false;
        }
    }

    private static bool RunShellCommand(string command, bool print = false)
    {
        try
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = $"-c \"{command}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using (var process = new Process { StartInfo = processStartInfo })
            {
                process.Start();

                // Read the standard output and error streams
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                // Wait for the command to finish
                process.WaitForExit();

                // Print output if requested
                if (print)
                {
                    Console.WriteLine(output);
                    Console.WriteLine(error);
                }

                // Return true if the exit code is 0 (success), otherwise false
                return process.ExitCode == 0;
            }
        }
        catch
        {
            // Handle exceptions (e.g., process start failure)
            // You can log the exception or take appropriate action based on your requirements
            return false;
        }
    }
}
