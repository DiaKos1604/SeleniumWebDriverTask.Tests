using OpenQA.Selenium;
using Serilog;

namespace SeleniumWebDriver.Library.Pages
{
    public abstract class EpamMainPage : NavigatablePage
    {
        protected EpamMainPage(IWebDriver driver, TimeSpan timeout, ILogger logger) : base(driver, timeout, logger)
        {
        }

        public override string Url => "https://www.epam.com/";
    }
}