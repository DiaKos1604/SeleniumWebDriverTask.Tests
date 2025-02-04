using OpenQA.Selenium;
using SeleniumWebDriverTask.Business.Pages;
using SeleniumWebDriverTask.Core.Utilities;

namespace SeleniumWebDriverTask.Business.Services
{
    public class CareersService
    {
        private readonly CareersPage _page;
        private readonly IWebDriver _driver;

        public CareersService(IWebDriver driver)
        {
            _driver = driver;
            _page = new CareersPage(driver);
        }
        public IWebElement KeywordsField => _page._waitHelper.WaitForElementToBeClickable(_page.KeywordsFieldLocator);

        private void ClickElement(By locator, string elementName)
        {
            LoggerHelper.LogInformation($"Attempting to click '{elementName}'.");
            var element = _page._waitHelper.WaitForElementToBeClickable(locator);
            new ActionsHelper(_driver).ClickElement(element);
            LoggerHelper.LogInformation($"Successfully clicked '{elementName}'.");
        }

        private void EnterText(IWebElement element, string text, string elementName)
        {
            LoggerHelper.LogInformation($"Entering '{text}' into '{elementName}'.");
            element.Clear();
            element.SendKeys(text);
            LoggerHelper.LogInformation($"Successfully entered '{text}' into '{elementName}'.");
        }

        public void ClickFindYourDreamJobLink()
        {
            ClickElement(_page.FindJobLinkLocator, "Find Your Dream Job Link");
        }

        public void SearchJob(string programmingLanguage)
        {
            LoggerHelper.LogInformation($"Starting job search for '{programmingLanguage}'.");
           
            EnterText(KeywordsField, programmingLanguage, "Keywords Field");
            ClickElement(_page.LocationFieldLocator, "Location Field");
            ClickElement(_page.ChoiceLocationLocator, "Choice Location");
            ClickElement(_page.RemoteOptionLocator, "Remote Option");
            ClickElement(_page.FindButtonLocator, "Find Button");

            LoggerHelper.LogInformation("Job search completed.");
        }

        public void SelectSearchByDate()
        {
            ClickElement(By.CssSelector("label[for='sort-time']"), "Search by Date Option");
        }

        public void ClickViewAndApplyForLatestJob()
        {
            LoggerHelper.LogInformation("Attempting to click 'View and Apply' for the latest job.");
            _page._waitHelper.WaitForPageLoad();

            ClickElement(By.XPath("//ul[@class='search-result__list']/li[position()=1]/descendant::a[text()='View and apply']"),
                "View and Apply Button for Latest Job");
        }

        public bool IsProgrammingLangElementDisplayed(string programmingLanguage)
        {
            LoggerHelper.LogInformation($"Checking for the presence of '{programmingLanguage}' on the Careers page.");

            var elements = _page._waitHelper.WaitForElementsToBePresent(By.XPath($"//*[contains(text(), '{programmingLanguage}')]"));
            bool isDisplayed = elements.Any();

            LoggerHelper.LogInformation($"Programming language '{programmingLanguage}' is {(isDisplayed ? "displayed" : "not displayed")} on the Careers page.");

            return isDisplayed;
        }
    }
}