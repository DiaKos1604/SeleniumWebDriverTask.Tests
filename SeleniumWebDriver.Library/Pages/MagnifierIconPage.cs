using OpenQA.Selenium;

namespace SeleniumWebDriver.Business.Pages
{
    public class MagnifierIconPage : BasePage
    {
        public MagnifierIconPage(IWebDriver driver) : base(driver)
        {
        }

        public By SearchInputLocator => By.Name("q");
        public By FindButtonLocator => By.ClassName("custom-search-button");
    }
}