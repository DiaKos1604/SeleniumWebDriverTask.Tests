using OpenQA.Selenium;
using Serilog;

namespace SeleniumWebDriver.Business.Pages
{
    public class AboutPage : BasePage
    {
        public AboutPage(IWebDriver driver, TimeSpan timeout, ILogger logger) : base(driver, timeout, logger)
        {
        }

        public By DownloadButtonLocator => By.CssSelector("a.button-ui-23.btn-focusable[href*='EPAM_Corporate_Overview_Q4_EOY.pdf']");
    }
}