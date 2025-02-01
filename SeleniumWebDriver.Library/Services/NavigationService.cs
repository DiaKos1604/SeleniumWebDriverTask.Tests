using OpenQA.Selenium;
using Serilog;

namespace SeleniumWebDriverTask.Business.Services
{
    public class NavigationService
    {
        private readonly IWebDriver _driver;

        public NavigationService(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public void GoToPage(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }

            Log.Logger.Information($"Navigating to {url}");
            _driver.Navigate().GoToUrl(url);
        }
    }
}