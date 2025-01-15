using OpenQA.Selenium;

namespace SeleniumWebDriver.Library.Pages
{
    public abstract class NavigatablePage : BasePage
    {
        protected NavigatablePage(IWebDriver driver, TimeSpan timeout) : base(driver, timeout)
        {
        }

        public abstract string Url { get; }
        public void GoTo() => driver.Navigate().GoToUrl(Url);
    }
}