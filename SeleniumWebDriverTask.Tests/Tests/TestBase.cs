using OpenQA.Selenium;
using SeleniumWebDriver.Library.Utilities;
using SeleniumWebDriverTask.Core.Utilities;
using Serilog;

namespace SeleniumWebDriverTask.Tests.Tests
{
    public class TestBase : IDisposable
    {
        protected IWebDriver driver;
        protected ILogger logger = Log.Logger;

        public TestBase()
        {
            try
            {
                string browserType = ConfigurationHelper.GetBrowserType();
                bool headless = ConfigurationHelper.GetHeadlessOption();
                driver = BrowserFactory.CreateBrowser(browserType, headless);
                LoggerHelper.LogInformation("TestBase initialized");
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(ex, "TestBase");
                throw;
            }
        }

        public void Dispose()
        {
            WebDriverManager.QuitDriver();
            logger.Information("WebDriver quit and resources cleaned up");
            Log.CloseAndFlush();
        }
    }
}