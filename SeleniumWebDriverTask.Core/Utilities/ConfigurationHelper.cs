using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.BiDi.Communication;

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
            return Configuration["BrowserType"] ?? "chrome";
        }

        public static bool GetHeadlessOption()
        {
            string headlessConfig = Configuration["Headless"] ?? "false";
            return bool.TryParse(headlessConfig, out bool headless) && headless;
        }
    }
}