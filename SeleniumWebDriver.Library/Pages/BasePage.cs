using OpenQA.Selenium;
using Serilog;
using TasksWebDriver.Utilities;

namespace SeleniumWebDriver.Library.Pages
{
    public abstract class BasePage
    {
        internal readonly IWebDriver driver;
        internal readonly WaitHelper waitHelper;
        private ILogger logger;

        public BasePage(IWebDriver driver, TimeSpan timeout, ILogger logger)
        {
            this.driver = driver;
            this.logger = logger;
            waitHelper = new WaitHelper(driver, timeout, logger);
        }
    }
}