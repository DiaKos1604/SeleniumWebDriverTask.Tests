using OpenQA.Selenium;

namespace SeleniumWebDriver.Library.Pages
{
    public class MagnifierIconPage : EpamMainPage
    {
        public MagnifierIconPage(IWebDriver driver, TimeSpan timeout) : base(driver, timeout)
        {
        }

        public IWebElement MagnifierIcon => waitHelper.WaitForElementToBeClickable(By.XPath("//button[@class='header-search__button header__icon' and @aria-expanded='false']"));
        public IWebElement SearchInput => waitHelper.WaitForElementToBeClickable(By.Name("q"));
        public IWebElement FindButton => waitHelper.WaitForElementToBeClickable(By.ClassName("custom-search-button"));

        public void Search(string searchTerm)
        {
            waitHelper.WaitForPageLoad(driver);
            MagnifierIcon.Click();
            waitHelper.WaitForPageLoad(driver);
            SearchInput.SendKeys(searchTerm);
            waitHelper.WaitForPageLoad(driver);
            FindButton.Click();
        }

        public bool IsSearchResultsDisplayed(string searchTerm)
        {
            try
            {
                var searchResults = waitHelper.WaitForElementsToBePresent(By.XPath($"//*[contains(text(), '{searchTerm}')]"));
                return searchResults.Count > 0;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}