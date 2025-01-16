using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace SeleniumWebDriver.Library.Utilities
{
    public class BrowserFactory
    {
        public static IWebDriver CreateBrowser(string browserType, bool headless)
        {
            IWebDriver driver;

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
                    throw new ArgumentException($"Unsupported browser: {browserType}");
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }
        private static void ConfigureCommonOptions(dynamic options, bool headless)
        {
            options.AddArgument("--start-maximized");
            if (headless)
            {
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920x1080");
            }
        }

    }
}