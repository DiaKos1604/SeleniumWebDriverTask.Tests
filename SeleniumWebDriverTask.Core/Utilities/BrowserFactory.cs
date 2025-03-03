using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace SeleniumWebDriverTask.Core.Utilities
{
    public class BrowserFactory
    {
        public static IWebDriver CreateBrowser(string browserType, bool headless)
        {
            LoggerHelper.LogInformation($"Attempting to create a new browser instance: {browserType}");

            return browserType.ToLower() switch
            {
                "chrome" => CreateChromeDriver(headless),
                "firefox" => CreateFirefoxDriver(headless),
                "edge" => CreateEdgeDriver(headless),
                _ => throw new ArgumentException($"Unsupported browser: {browserType}")
            };
        }

        private static IWebDriver CreateChromeDriver(bool headless)
        {
            var options = new ChromeOptions();
            ConfigureCommonOptions(options, headless);
            return new ChromeDriver(options);
        }

        private static IWebDriver CreateFirefoxDriver(bool headless)
        {
            var options = new FirefoxOptions();
            ConfigureCommonOptions(options, headless);
            return new FirefoxDriver(options);
        }

        private static IWebDriver CreateEdgeDriver(bool headless)
        {
            var options = new EdgeOptions();
            ConfigureCommonOptions(options, headless);
            return new EdgeDriver(options);
        }

        private static void ConfigureCommonOptions(dynamic options, bool headless)
        {
            LoggerHelper.LogInformation("Configuring browser options.");

            options.AddArgument("--start-maximized");
            LoggerHelper.LogInformation("Browser option added: --start-maximized");

            if (headless)
            {
                options.AddArgument("--headless");
                LoggerHelper.LogInformation("Browser option added: --headless");

                options.AddArgument("--disable-gpu");
                options.AddArgument("--disable-software-rasterizer");
                options.AddArgument("--disable-popup-blocking");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--window-size=1920x1080");
                options.AddArgument("--incognito");
                options.AddArgument("--disable-dev-shm-usage");

                LoggerHelper.LogInformation("All headless options configured.");
            }
        }
    }
}