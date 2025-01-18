using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SeleniumWebDriverTask.Core.Utilities;

namespace SeleniumWebDriver.Library.Utilities
{
    public class BrowserFactory
    {
        public static IWebDriver CreateBrowser(string browserType, bool headless)
        {
            IWebDriver driver;

            LoggerHelper.LogInformation($"Attempting to create a new browser instance: {browserType}");
            try
            {
                switch (browserType.ToLower())
                {
                    case "chrome":
                        var chromeOptions = new ChromeOptions();
                        ConfigureCommonOptions(chromeOptions, headless);
                        driver = new ChromeDriver(chromeOptions);
                        break;

                    case "firefox":
                        var firefoxOptions = new FirefoxOptions();
                        ConfigureCommonOptions(firefoxOptions, headless);
                        driver = new FirefoxDriver(firefoxOptions);
                        break;

                    case "edge":
                        var edgeOptions = new EdgeOptions();
                        ConfigureCommonOptions(edgeOptions, headless);
                        driver = new EdgeDriver(edgeOptions);
                        break;


                    default:
                        LoggerHelper.LogInformation($"Browser type '{browserType}' is not supported.");
                        throw new ArgumentException($"Unsupported browser: {browserType}");
                }
            }

            catch (Exception ex)
            {
                LoggerHelper.LogError(ex, $"Failed to create browser instance: {browserType}");
                throw;
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
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
                LoggerHelper.LogInformation("Browser option added: --disable-gpu");

                options.AddArgument("--disable-software-rasterizer");
                LoggerHelper.LogInformation("Browser option added: --headless");

                options.AddArgument("--disable-popup-blocking");
                LoggerHelper.LogInformation("Browser option added: --disable-software-rasterizer");

                options.AddArgument("--no-sandbox");
                LoggerHelper.LogInformation("Browser option added: --no-sandbox");

                options.AddArgument("--disable-dev-shm-usage");
                LoggerHelper.LogInformation("Browser option added: --disable-dev-shm-usage");

                options.AddArgument("--disable-extensions");
                LoggerHelper.LogInformation("Browser option added: --disable-extensions");

                options.AddArgument("--window-size=1920x1080");
                LoggerHelper.LogInformation("Browser option added: --window-size=1920x1080");

                options.AddArgument("--incognito");
                LoggerHelper.LogInformation("Browser option added: --incognito");
            }
        }
    }
}