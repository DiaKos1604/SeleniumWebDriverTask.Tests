using OpenQA.Selenium;
using Serilog;

namespace SeleniumWebDriver.Business.Pages
{
    public class MagnifierIconPage : BasePage
    {
        public MagnifierIconPage(IWebDriver driver) : base(driver)
        {
        }

        public By MagnifierIconLocator => By.XPath("//button[@class='header-search__button header__icon' and @aria-expanded='false']");
        public By SearchInputLocator => By.Name("q");
        public By FindButtonLocator => By.ClassName("custom-search-button");
        public By SearchResultsHeaderLocator => By.XPath("//h1[contains(@class, 'search-results-header')]");

    }
}