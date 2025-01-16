using OpenQA.Selenium;

namespace SeleniumWebDriverTask.Tests.Tests
{
    public class TestBase : IDisposable
    {

        protected IWebDriver driver;

        public TestBase()
        {
            driver = WebDriverManager.GetDriver("edge", true);
        }

        public void Dispose()
        {
            WebDriverManager.QuitDriver();
        }
    }
}
