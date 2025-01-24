using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;

namespace SeleniumWebDriverTask.Core.Utilities
{
    public class ActionsHelper
    {
        private readonly IWebDriver _driver;
        private readonly Actions _actions;

        public ActionsHelper(IWebDriver driver)
        {
            _driver = driver;
            _actions = new Actions(driver);
        }

        public void ClickElement(IWebElement element)
        {
            _actions.Click(element).Perform();
        }

        public void ScrollToElement(IWebElement element)
        {
            _actions.MoveToElement(element).Perform();
        }
    }
}