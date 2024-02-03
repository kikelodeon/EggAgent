using Serilog;
using Serilog.Events;
using System;

namespace EggBackup
{
    public class Logger
    {
        private static readonly ILogger _logger;

        static Logger()
        {
            _logger = new LoggerConfiguration()
                .CreateLogger();
        }

        public static void Log(string message)
        {
            _logger.Information($"[{DateTime.Now}] {message}");
        }

        public static void LogWarning(string message)
        {
            _logger.Warning($"{DateTime.Now:HH:mm:ss} - WARNING - {message}");
        }
        public static void LogSuccess(string message)
        {
            _logger.Warning($"{DateTime.Now:HH:mm:ss} - SUCCESS - {message}");
        }

        public static void LogCritical(string message)
        {
            _logger.Fatal($"{DateTime.Now:HH:mm:ss} - CRITICAL - {message}");
        }
    }
}
