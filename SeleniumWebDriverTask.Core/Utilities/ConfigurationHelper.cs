using Microsoft.Extensions.Configuration;
using RestSharp;

namespace SeleniumWebDriverTask.Core.Utilities
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot Configuration { get; }

        static ConfigurationHelper()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
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

        public static string GetApiBaseUrl()
        {
            string baseUrl = Configuration["AppSettings:BaseUrl"] ?? throw new InvalidOperationException("BaseUrl is not configured in appsettings.json.");

            if (string.IsNullOrEmpty(baseUrl))
            {
                LoggerHelper.LogError("BaseUrl is not configured in appsettings.json.");
                throw new InvalidOperationException("BaseUrl is required in appsettings.json.");
            }

            LoggerHelper.LogInformation($"API Base URL retrieved from configuration: {baseUrl}");
            return baseUrl;
        }
    }
}