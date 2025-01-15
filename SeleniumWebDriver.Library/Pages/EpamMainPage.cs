using OpenQA.Selenium;

namespace SeleniumWebDriver.Library.Pages
{
    public abstract class EpamMainPage : NavigatablePage
    {
        protected EpamMainPage(IWebDriver driver, TimeSpan timeout) : base(driver, timeout)
        {
        }

        public override string Url => "https://www.epam.com/";
    }
}