using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace SeleniumWebDriverTask.Spec.Steps
{
    public class BaseSteps
    {
        protected readonly IWebDriver _driver;
        protected readonly ScenarioContext _scenarioContext;

        public BaseSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            if (!_scenarioContext.ContainsKey("WebDriver"))
            {
                throw new InvalidOperationException("WebDriver instance is missing from ScenarioContext.");
            }

            _driver = _scenarioContext["WebDriver"] as IWebDriver
                      ?? throw new InvalidOperationException("WebDriver instance could not be cast.");
        }
    }
}