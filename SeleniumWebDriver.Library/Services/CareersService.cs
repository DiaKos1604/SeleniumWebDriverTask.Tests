using OpenQA.Selenium;
using SeleniumWebDriver.Business.Pages;
using SeleniumWebDriverTask.Core.Utilities;
using Serilog;

namespace SeleniumWebDriver.Business.Services
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

        public IWebElement FindJobLink => _page._waitHelper.WaitForElementToBeClickable(_page.FindJobLinkLocator);
        public IWebElement KeywordsField => _page._waitHelper.WaitForElementToBeClickable(_page.KeywordsFieldLocator);
        public IWebElement LocationField => _page._waitHelper.WaitForElementToBeClickable(_page.LocationFieldLocator);
        public IWebElement ChoiceLocation => _page._waitHelper.WaitForElementToBeClickable(_page.ChoiceLocationLocator);
        public IWebElement RemoteOption => _page._waitHelper.WaitForElementToBeClickable(_page.RemoteOptionLocator);
        public IWebElement FindButton => _page._waitHelper.WaitForElementToBeClickable(_page.FindButtonLocator);

        public void SearchJob(string programmingLanguage)
        {
            LoggerHelper.LogInformation($"Initiating job search for programming language: {programmingLanguage}.");

            new ActionsHelper(_driver).ClickElement(FindJobLink);
            LoggerHelper.LogInformation("Clicked on the 'Find Your Job' link.");

            KeywordsField.Clear();
            KeywordsField.SendKeys(programmingLanguage);
            LocationField.Click();
            LoggerHelper.LogInformation($"Entered programming language: {programmingLanguage} in the keywords field.");

            ChoiceLocation.Click();
            LoggerHelper.LogInformation("Selected the location from the dropdown.");

            RemoteOption.Click();
            LoggerHelper.LogInformation("Selected the 'Remote' option.");

            FindButton.Click();
            LoggerHelper.LogInformation("Clicked the 'Find' button to search for jobs.");
        }

        public void GetDateLabel()
        {
            LoggerHelper.LogInformation("Attempting to select the 'Search by Date' option.");

            var dateElement = _page._waitHelper.WaitForElementToBeClickable(By.CssSelector("label[for='sort-time']"));
            new ActionsHelper(_driver).ClickElement(dateElement);
            LoggerHelper.LogInformation("'Search by Date' option selected successfully.");
        }

        public void GetViewAndApplyButtonForLatestob()
        {
            LoggerHelper.LogInformation("Retrieving the 'View and Apply' button for the latest job.");

            _page._waitHelper.WaitForPageLoad(_driver);
            var latestElement = _page._waitHelper.WaitForElementToBeClickable(By.XPath("//ul[@class='search-result__list']/li[position()=1]/descendant::a[text()='View and apply']"));
            new ActionsHelper(_driver).ClickElement(latestElement);
            LoggerHelper.LogInformation("'View and Apply' button for the latest job clicked successfully.");
        }

        public bool IsProgrammingLangElementDisplayed(string programmingLanguage)
        {
            LoggerHelper.LogInformation($"Checking if programming language '{programmingLanguage}' is displayed on the Careers page.");

            var programmingLangElements = _page._waitHelper.WaitForElementsToBePresent(By.XPath($"//*[contains(text(), '{programmingLanguage}')]"));
            bool isDisplayed = programmingLangElements.Any();
            LoggerHelper.LogInformation($"Programming language '{programmingLanguage}' is {(isDisplayed ? "displayed" : "not displayed")} on the Careers page.");

            return isDisplayed;
        }
    }
}