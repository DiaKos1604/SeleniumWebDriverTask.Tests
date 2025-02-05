using OpenQA.Selenium;
using SeleniumWebDriverTask.Business.Pages;
using SeleniumWebDriverTask.Core.Utilities;

namespace SeleniumWebDriverTask.Tests.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected IWebDriver _driver;

        protected HomePage _homePage;
        protected CareersPage _careersPage;
        protected MagnifierIconPage _magnifierIconPage;
        protected AboutPage _aboutPage;
        protected InsightsPage _insightsPage;
        protected ServicesSectionPage _servicesSectionPage;

        protected TestBase(WebDriverManager webDriverManager)
        {
            _driver = webDriverManager.GetWebDriver(
                    ConfigurationHelper.GetBrowserType(),
                    ConfigurationHelper.GetHeadlessOption());
            LoggerHelper.LogInformation("TestBase initialized.");

            _homePage = new HomePage(_driver);
            _careersPage = new CareersPage(_driver);
            _magnifierIconPage = new MagnifierIconPage(_driver);
            _aboutPage = new AboutPage(_driver);
            _insightsPage = new InsightsPage(_driver);
            _servicesSectionPage = new ServicesSectionPage(_driver);
        }

        public void Dispose()
        {
            if (XunitContext.Context?.TestException != null)
            {
                LoggerHelper.LogInformation("Test failed. Taking screenshot.");
                ScreenshotMaker.TakeBrowserScreenshot((ITakesScreenshot)_driver, "TestFailure");
            }

            LoggerHelper.LogInformation("Disposing TestBase and quitting WebDriver.");
            WebDriverManager.Instance().QuitDriver();

            LoggerHelper.CloseAndFlush();
            GC.SuppressFinalize(this);
            LoggerHelper.LogInformation("WebDriver quit and resources cleaned up.");
        }
    }
}