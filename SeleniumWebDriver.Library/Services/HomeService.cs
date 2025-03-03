using OpenQA.Selenium;
using SeleniumWebDriverTask.Business.Pages;
using SeleniumWebDriverTask.Core.Utilities;

namespace SeleniumWebDriverTask.Business.Services
{
    public class HomeService
    {
        private readonly HomePage _page;
        private readonly IWebDriver _driver;

        public HomeService(IWebDriver driver)
        {
            _page = new HomePage(driver);
            _driver = driver;
        }

        private void ClickElement(By locator, string elementName)
        {
            LoggerHelper.LogInformation($"Clicking '{elementName}' element on {nameof(HomePage)}.");
            _page._waitHelper.WaitForElementToBeClickable(locator).Click();
            _page._waitHelper.WaitForPageLoad();
        }

        public void ClickCareersLink() => ClickElement(_page.CareersLinkLocator, "Careers");

        public void ClickMagnifierIcon() => ClickElement(_page.MagnifierIconLocator, "Magnifier Icon");

        public void ClickAboutLink() => ClickElement(_page.AboutLinkLocator, "About");

        public void ClickInsightsLink() => ClickElement(_page.InsightsLinkLocator, "Insights");

        public void ClickServicesLink() => ClickElement(_page.ServicesLinkLocator, "Services");

        public void ValidateNavigationElementsExist()
        {
            LoggerHelper.LogInformation($"Validating navigation elements on {nameof(HomePage)}.");

            ClickCareersLink();
            ClickMagnifierIcon();
            ClickAboutLink();
            ClickInsightsLink();
            ClickServicesLink();

            LoggerHelper.LogInformation($"Validation of navigation elements on {nameof(HomePage)} is complete.");
        }
    }
}