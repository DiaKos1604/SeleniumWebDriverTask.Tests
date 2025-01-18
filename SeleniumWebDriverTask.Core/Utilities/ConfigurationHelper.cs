using Microsoft.Extensions.Configuration;

namespace SeleniumWebDriverTask.Core.Utilities
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot Configuration { get; }

        static ConfigurationHelper()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            LoggerHelper.LogInformation("Configuration loaded from appsettings.json");
        }

        public static string GetBrowserType()
        {
            string browserType = Configuration["BrowserType"] ?? "chrome";
            LoggerHelper.LogDebug($"Browser type retrieved from configuration: {browserType}");
            return browserType;
        }

        public static bool GetHeadlessOption()
        {
            string headlessConfig = Configuration["Headless"] ?? "false";
            bool headless = false;
            if (bool.TryParse(headlessConfig, out headless))
            {
                LoggerHelper.LogDebug($"Headless option parsed successfully: {headless}");
            }
            else
            {
                LoggerHelper.LogWarning($"Failed to parse headless option from configuration: {headlessConfig}");
            }
            return headless;
        }
    }
}