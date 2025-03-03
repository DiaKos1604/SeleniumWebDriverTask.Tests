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
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"BeforeScenario on Thread ID: {threadId}");

            var browserType = ConfigurationHelper.GetBrowserType();
            var headless = ConfigurationHelper.GetHeadlessOption();
            var driver = WebDriverManager.Instance().GetWebDriver(browserType, headless);

            _scenarioContext["WebDriver"] = driver;
            _scenarioContext.Set(driver, typeof(IWebDriver).FullName);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"AfterScenario on Thread ID: {threadId}");

            if (_scenarioContext["WebDriver"] is IWebDriver driver)
            {
                driver.Quit();
            }
            else
            {
                LoggerHelper.LogWarning("WebDriver was not found in the scenario context.");
            }
        }
    }
}