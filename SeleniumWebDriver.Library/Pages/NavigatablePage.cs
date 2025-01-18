using OpenQA.Selenium;
using Serilog;

namespace SeleniumWebDriver.Library.Pages
{
    public abstract class NavigatablePage : BasePage
    {
        protected NavigatablePage(IWebDriver driver, TimeSpan timeout, ILogger logger) : base(driver, timeout, logger)
        {
        }

        public abstract string Url { get; }
        public void GoTo()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}