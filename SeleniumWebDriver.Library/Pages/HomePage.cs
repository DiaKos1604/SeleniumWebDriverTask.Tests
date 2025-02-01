using OpenQA.Selenium;
using Serilog;

namespace SeleniumWebDriverTask.Business.Pages
{
    public class HomePage : BasePage
    {
        public const string Url = "https://www.epam.com/";

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public By CareersLinkLocator => By.LinkText("Careers");
        public By MagnifierIconLocator => By.ClassName("header-search__button");
        public By AboutLinkLocator => By.LinkText("About");
        public By InsightsLinkLocator => By.LinkText("Insights");
        public By ServicesLinkLocator => By.LinkText("Services");
    }
}