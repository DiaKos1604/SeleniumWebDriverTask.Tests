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
        private bool testFailed;

        public TestBase()
        {
            try
            {
                string browserType = ConfigurationHelper.GetBrowserType();
                bool headless = ConfigurationHelper.GetHeadlessOption();
                driver = BrowserFactory.CreateBrowser(browserType, headless);
                LoggerHelper.LogInformation("TestBase initialized.");
            }
            catch (Exception ex)
            {
                MarkTestAsFailed();
                LoggerHelper.LogError(ex, "TestBase Initialization failed.");
                throw;
            }
        }

        public void MarkTestAsFailed()
        {
            testFailed = true;
            ScreenshotMaker.TakeBrowserScreenshot((ITakesScreenshot)driver, "TestFailure");
        }

        public void Dispose()
        {
            try
            {
                WebDriverManager.QuitDriver();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Failed to quit WebDriver properly.");
            }

            logger.Information("WebDriver quit and resources cleaned up.");
            LoggerHelper.CloseAndFlush();
        }
    }
}