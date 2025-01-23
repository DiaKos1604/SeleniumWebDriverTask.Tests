using OpenQA.Selenium;
using Serilog;

namespace SeleniumWebDriver.Business.Pages
{
    public class MagnifierIconPage : BasePage
    {
        public MagnifierIconPage(IWebDriver driver, TimeSpan timeout, ILogger logger) : base(driver, timeout, logger)
        {
        }

        public By MagnifierIconLocator => By.XPath("//button[@class='header-search__button header__icon' and @aria-expanded='false']");
        public By SearchInputLocator => By.Name("q");
        public By FindButtonLocator => By.ClassName("custom-search-button");
    }
}