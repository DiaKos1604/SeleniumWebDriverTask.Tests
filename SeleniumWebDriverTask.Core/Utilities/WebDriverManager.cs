using OpenQA.Selenium;

namespace SeleniumWebDriverTask.Core.Utilities
{
    public class WebDriverManager
    {
        private WebDriverManager() { }

        private static WebDriverManager? _instance;
        private static readonly object _lock = new object();
        private IWebDriver? _webDriver;
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
            if (_webDriver != null)
            {
                LoggerHelper.LogInformation("WebDriver instance already exists. Returning the existing instance.");
                return _webDriver;
            }

            lock (_lock)
            {
                if (_webDriver == null)
                {
                    LoggerHelper.LogInformation("Creating new WebDriver instance.");
                    _webDriver = BrowserFactory.CreateBrowser(browserType, headless);
                }
            }
            return _webDriver;
        }

        public void QuitDriver()
        {
            if (_webDriver != null)
            {
                LoggerHelper.LogInformation("Attempting to close the browser.");

                try
                {
                    _webDriver.Quit();
                    LoggerHelper.LogInformation("Browser closed successfully.");
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError(ex, "Failed to close the browser properly.");
                }
                finally
                {
                    _webDriver.Dispose();
                    _webDriver = null;
                    LoggerHelper.LogInformation("Driver instance set to null.");
                }
            }
        }
    }
}