using OpenQA.Selenium;
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
                driver = WebDriverManager.GetDriver("edge", true);
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