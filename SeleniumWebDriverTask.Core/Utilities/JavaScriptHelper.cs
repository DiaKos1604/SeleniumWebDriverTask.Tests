using OpenQA.Selenium;

namespace SeleniumWebDriverTask.Core.Utilities
{
    public class JavaScriptHelper
    {
        private readonly IWebDriver _driver;
        private readonly IJavaScriptExecutor _jsExecutor;

        public JavaScriptHelper(IWebDriver driver)
        {
            _driver = driver;
            _jsExecutor = (IJavaScriptExecutor)driver;
        }

        public void ClickElement(IWebElement element)
        {
            _jsExecutor.ExecuteScript("arguments[0].click();", element);
        }

        public string GetPageTitle()
        {
            return (string)_jsExecutor.ExecuteScript("return document.title;");
        }

        public void ScrollToElement(IWebElement element)
        {
            _jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
    }
}