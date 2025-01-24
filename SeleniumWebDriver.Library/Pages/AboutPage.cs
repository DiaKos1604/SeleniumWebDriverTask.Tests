using OpenQA.Selenium;
using Serilog;

namespace SeleniumWebDriver.Business.Pages
{
    public class AboutPage : BasePage
    {
        public AboutPage(IWebDriver driver) : base(driver)
        {
        }

        public By DownloadButtonLocator => By.CssSelector("a.button-ui-23.btn-focusable[href*='EPAM_Corporate_Overview_Q4_EOY.pdf']");
    }
}