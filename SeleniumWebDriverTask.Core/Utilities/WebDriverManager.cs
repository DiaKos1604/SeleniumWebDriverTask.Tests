using OpenQA.Selenium;

namespace SeleniumWebDriverTask.Core.Utilities
{
    public class WebDriverManager
    {
        private WebDriverManager() { }

        private static WebDriverManager? _instance;
        private static readonly object _lock = new object();
        private static readonly ThreadLocal<IWebDriver> _driver = new ThreadLocal<IWebDriver>();

        public static WebDriverManager Instance()
        {

            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new WebDriverManager();
                    }
                }
            }
            return _instance;
        }

        public IWebDriver GetWebDriver(string browserType, bool headless)
        {
            if (_driver.Value != null)
            {
                _driver.Value.Quit();
                _driver.Value.Dispose();
            }

            _driver.Value = BrowserFactory.CreateBrowser(browserType, headless);
            return _driver.Value;
        }

        public void QuitDriver()
        {
            if (_driver.Value != null)
            {
                LoggerHelper.LogInformation("Attempting to close the browser.");

                _driver.Value.Quit();
                LoggerHelper.LogInformation("Browser closed successfully.");

                _driver.Value.Dispose();
                LoggerHelper.LogInformation("Driver instance is dispose.");
            }
        }
    }
}