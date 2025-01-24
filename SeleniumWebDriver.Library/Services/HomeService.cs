using OpenQA.Selenium;
using SeleniumWebDriver.Business.Pages;
using SeleniumWebDriverTask.Core.Utilities;
using Serilog;

namespace SeleniumWebDriver.Business.Services
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

        public void ClickCareersLink()
        {
            LoggerHelper.LogInformation($"Clicking 'Careers' link on {nameof(HomePage)}.");
            _page._waitHelper.WaitForElementToBeClickable(_page.CareersLinkLocator).Click();
        }

        public void ClickMagnifierIcon()
        {
            LoggerHelper.LogInformation($"Clicking magnifier icon on {nameof(HomePage)}.");
            _page._waitHelper.WaitForElementToBeClickable(_page.MagnifierIconLocator).Click();
        }

        public void ClickAboutLink()
        {
            LoggerHelper.LogInformation($"Clicking 'About' link on {nameof(HomePage)}.");
            _page._waitHelper.WaitForElementToBeClickable(_page.AboutLinkLocator).Click();
        }

        public void ClickInsightsLink()
        {
            LoggerHelper.LogInformation($"Clicking 'Insights' link on {nameof(HomePage)}.");
            _page._waitHelper.WaitForElementToBeClickable(_page.InsightsLinkLocator).Click();
        }

        public void ValidateNavigationElementsExist()
        {
            LoggerHelper.LogInformation($"Validating navigation elements on {nameof(HomePage)}.");

            ClickCareersLink();
            _page._waitHelper.WaitForPageLoad(_driver);

            ClickMagnifierIcon();
            _page._waitHelper.WaitForPageLoad(_driver);

            ClickAboutLink();
            _page._waitHelper.WaitForPageLoad(_driver);

            ClickInsightsLink();
            _page._waitHelper.WaitForPageLoad(_driver);

            LoggerHelper.LogInformation($"Validation of navigation elements on {nameof(HomePage)} is complete.");
        }
    }
}