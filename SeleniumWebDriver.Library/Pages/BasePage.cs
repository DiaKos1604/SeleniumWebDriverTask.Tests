using OpenQA.Selenium;
using TasksWebDriver.Utilities;

namespace SeleniumWebDriver.Library.Pages
{
    public abstract class BasePage
    {
        internal readonly IWebDriver driver;
        internal readonly WaitHelper waitHelper;

        public BasePage(IWebDriver driver, TimeSpan timeout)
        {
            this.driver = driver;
            this.waitHelper = new WaitHelper(driver, timeout);
        }
    }
}