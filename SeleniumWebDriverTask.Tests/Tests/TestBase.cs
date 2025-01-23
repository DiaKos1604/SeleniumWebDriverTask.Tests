using OpenQA.Selenium;
using SeleniumWebDriver.Business.Pages;
using SeleniumWebDriverTask.Core.Utilities;
using Serilog;

namespace SeleniumWebDriverTask.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected IWebDriver Driver;
        protected ILogger Logger = Log.Logger;

        protected HomePage _homePage;
        protected CareersPage _careersPage;
        protected MagnifierIconPage _magnifierIconPage;
        protected AboutPage _aboutPage;
        protected InsightsPage _insightsPage;

        protected TestBase(WebDriverManager webDriverManager)
        {
            Driver = webDriverManager.GetWebDriver(
                    ConfigurationHelper.GetBrowserType(),
                    ConfigurationHelper.GetHeadlessOption());
            LoggerHelper.LogInformation("TestBase initialized.");

            _homePage = new HomePage(Driver, TimeSpan.FromSeconds(20), Logger);
            _careersPage = new CareersPage(Driver, TimeSpan.FromSeconds(20), Logger);
            _magnifierIconPage = new MagnifierIconPage(Driver, TimeSpan.FromSeconds(20), Logger);
            _aboutPage = new AboutPage(Driver, TimeSpan.FromSeconds(20), Logger);
            _insightsPage = new InsightsPage(Driver, TimeSpan.FromSeconds(20), Logger);
        }

        public void Dispose()
        {
            ScreenshotMaker.TakeBrowserScreenshot((ITakesScreenshot)Driver, "TestFailure");

            LoggerHelper.LogInformation("Disposing TestBase and quitting WebDriver.");
            WebDriverManager.Instance().QuitDriver();

            LoggerHelper.CloseAndFlush();
            LoggerHelper.LogInformation("WebDriver quit and resources cleaned up.");
        }
    }
}