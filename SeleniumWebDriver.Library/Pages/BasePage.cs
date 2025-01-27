using OpenQA.Selenium;
using SeleniumWebDriverTask.Core.Utilities;
using Serilog;

namespace SeleniumWebDriver.Business.Pages
{
    public abstract class BasePage
    {
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

        protected readonly IWebDriver _driver;
        public readonly WaitHelper _waitHelper;

        protected BasePage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _waitHelper = new WaitHelper(driver, DefaultTimeout);
        }

        public virtual void WaitForPageLoad()
        {
            Log.Logger.Information("Waiting for page load...");
            _waitHelper.WaitForPageLoad();
        }
    }
}