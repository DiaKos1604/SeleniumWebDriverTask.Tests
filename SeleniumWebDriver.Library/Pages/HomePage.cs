using OpenQA.Selenium;
using Serilog;

namespace SeleniumWebDriver.Business.Pages
{
    public class HomePage : BasePage
    {
        public const string Url = "https://www.epam.com/";

        public HomePage(IWebDriver driver, TimeSpan timeout, ILogger logger) : base(driver, timeout, logger)
        {
        }

        public By CareersLinkLocator => By.LinkText("Careers");
        public By MagnifierIconLocator => By.ClassName("header-search__button");
        public By AboutLinkLocator => By.LinkText("About");
        public By InsightsLinkLocator => By.LinkText("Insights");
    }
}