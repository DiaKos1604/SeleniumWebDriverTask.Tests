using OpenQA.Selenium;
using SeleniumWebDriverTask.Core.Utilities;
using Serilog;
using System.Configuration;

namespace SeleniumWebDriverTask.Business.Services
{
    public class NavigationService
    {
        private readonly IWebDriver _driver;

        public NavigationService(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public void GoToPage()
        {
            string url = ConfigurationHelper.GetApplicationUrl();

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }

            Log.Logger.Information($"Navigating to {url}");
            _driver.Navigate().GoToUrl(url);
        }
    }
}