using OpenQA.Selenium;
using SeleniumWebDriverTask.Core.Utilities;
using Serilog;

namespace SeleniumWebDriver.Business.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver _driver;
        public readonly WaitHelper _waitHelper;
        protected readonly ILogger _logger;

        protected BasePage(IWebDriver driver, TimeSpan timeout, ILogger logger)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _waitHelper = new WaitHelper(driver, timeout, logger);
        }

        public virtual void WaitForPageLoad()
        {
            _waitHelper.WaitForPageLoad(_driver);
        }
    }
}