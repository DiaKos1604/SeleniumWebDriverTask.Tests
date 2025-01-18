using Microsoft.Extensions.Configuration;
using Serilog;

namespace SeleniumWebDriverTask.Core.Utilities
{
    public static class LoggerHelper
    {
        public static ILogger Logger;

        static LoggerHelper()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public static void LogError(Exception ex, string message)
        {
            Logger.Error(ex, message);

        }

        public static void LogInformation(string message)
        {
            Logger.Information(message);
        }
    }
}