using OpenQA.Selenium;
using SeleniumWebDriverTask.Core.Utilities;
using TechTalk.SpecFlow;

namespace SeleniumWebDriverTask.Spec.Hooks
{
    [Binding]
    public class Scenario
    {
        private readonly ScenarioContext _scenarioContext;

        public Scenario(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            try
            {
                var browserType = ConfigurationHelper.GetBrowserType();
                var headless = ConfigurationHelper.GetHeadlessOption();
                var driver = WebDriverManager.Instance().GetWebDriver(browserType, headless);
                _scenarioContext["WebDriver"] = driver;
                _scenarioContext.Set(driver, typeof(IWebDriver).FullName);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(ex, "Error when initializing the web driver.");
                throw;
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext["WebDriver"] is IWebDriver)
            {
                WebDriverManager.Instance().QuitDriver();
            }
            else
            {
                LoggerHelper.LogWarning("WebDriver was not found in the scenario context.");
            }
        }
    }
}