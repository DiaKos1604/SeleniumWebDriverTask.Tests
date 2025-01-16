using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace TasksWebDriver.Utilities
{
    public class WaitHelper
    {
        private readonly WebDriverWait wait;

        public WaitHelper(IWebDriver driver, TimeSpan timeout)
        {
            wait = new WebDriverWait(driver, timeout);
        }

        public IWebElement WaitForElementToBeClickable(By locator)
        {
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public bool WaitForPageLoad(IWebDriver driver)
        {
            return wait.Until(d =>
            {
                var js = (IJavaScriptExecutor)d;
                return js.ExecuteScript("return document.readyState").ToString() == "complete";
            });
        }

        public bool Until(Func<bool> condition)
        {
            return wait.Until(_ => condition());
        }

        public ReadOnlyCollection<IWebElement> WaitForElementsToBePresent(By locator)
        {
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }
    }
}