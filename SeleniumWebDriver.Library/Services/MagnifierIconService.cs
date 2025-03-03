using OpenQA.Selenium;
using SeleniumWebDriverTask.Business.Pages;
using SeleniumWebDriverTask.Core.Utilities;

namespace SeleniumWebDriverTask.Business.Services
{
    public class MagnifierIconService
    {
        private readonly MagnifierIconPage _page;
        private readonly IWebDriver _driver;

        public MagnifierIconService(IWebDriver driver)
        {
            _driver = driver;
            _page = new MagnifierIconPage(driver);
        }

        private void ClickElement(By locator, string elementName)
        {
            LoggerHelper.LogInformation($"Clicking '{elementName}' element.");

            var element = _page._waitHelper.WaitForElementToBeClickable(locator);
            element.Click();

            LoggerHelper.LogInformation($"Successfully clicked '{elementName}' element.");
        }

        public void EnterSearchTerm(string searchTerm)
        {
            _page._waitHelper.WaitForPageLoad();
            var searchInput = _page._waitHelper.WaitForElementToBeClickable(_page.SearchInputLocator);
            searchInput.Clear();
            searchInput.SendKeys(searchTerm);

            LoggerHelper.LogInformation($"Search term '{searchTerm}' entered successfully.");
        }

        public void ClickFindButton()
        {
            ClickElement(_page.FindButtonLocator, "Find Button");

            LoggerHelper.LogInformation($"Search completed successfully.");
        }

        public bool IsSearchResultsDisplayed(string searchTerm)
        {
            LoggerHelper.LogInformation($"Validating if search results for '{searchTerm}' are displayed.");
            try
            {
                var searchResults = _page._waitHelper.WaitForElementsToBePresent(By.XPath($"//*[contains(text(), '{searchTerm}')]"));
                bool isDisplayed = searchResults.Count > 0;

                LoggerHelper.LogInformation(isDisplayed
                    ? $"Search results for '{searchTerm}' are displayed."
                    : $"No search results found for '{searchTerm}'.");
                return isDisplayed;
            }
            catch (WebDriverTimeoutException ex)
            {
                LoggerHelper.LogWarning($"Timeout while waiting for search results for '{searchTerm}': {ex.Message}");
                return false;
            }
        }
    }
}