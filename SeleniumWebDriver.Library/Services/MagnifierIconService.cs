using OpenQA.Selenium;
using SeleniumWebDriver.Business.Pages;
using SeleniumWebDriverTask.Core.Utilities;
using Serilog;

namespace SeleniumWebDriver.Business.Services
{
    public class MagnifierIconService
    {
        private readonly MagnifierIconPage _page;
        private readonly IWebDriver _driver;

        public MagnifierIconService(IWebDriver driver, TimeSpan timeout, ILogger logger)
        {
            _driver = driver;
            _page = new MagnifierIconPage(driver, timeout, logger);
        }

        public void Search(string searchTerm)
        {
            LoggerHelper.LogInformation($"Starting search for term: {searchTerm}.");

            _page._waitHelper.WaitForPageLoad(_driver);
            _page._waitHelper.WaitForElementToBeClickable(_page.MagnifierIconLocator).Click();
            LoggerHelper.LogInformation("Clicking the magnifier icon.");

            _page._waitHelper.WaitForPageLoad(_driver);
            _page._waitHelper.WaitForPageLoad(_driver);
            _page._waitHelper.WaitForElementToBeClickable(_page.SearchInputLocator).SendKeys(searchTerm);
            LoggerHelper.LogInformation($"Entering search term: '{searchTerm}' in the search input.");

            _page._waitHelper.WaitForPageLoad(_driver);
            _page._waitHelper.WaitForElementToBeClickable(_page.FindButtonLocator).Click();
            LoggerHelper.LogInformation("Clicking the 'Find' button.");

            _page._waitHelper.WaitForPageLoad(_driver);
            LoggerHelper.LogInformation($"Search for '{searchTerm}' completed successfully.");
        }

        public bool IsSearchResultsDisplayed(string searchTerm)
        {
            LoggerHelper.LogInformation($"Checking if search results for '{searchTerm}' are displayed on the search page.");
            try
            {
                LoggerHelper.LogInformation($"Search results for '{searchTerm}' are displayed.");
                var searchResults = _page._waitHelper.WaitForElementsToBePresent(By.XPath($"//*[contains(text(), '{searchTerm}')]"));
                return searchResults.Count > 0;
            }
            catch (WebDriverTimeoutException)
            {
                LoggerHelper.LogInformation($"No search results found for '{searchTerm}'.");
                return false;
            }
        }
    }
}