using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog;
using System.Collections.ObjectModel;

namespace SeleniumWebDriverTask.Core.Utilities
{
    public class WaitHelper
    {
        private readonly WebDriverWait _wait;
        private ILogger Logger;

        public WaitHelper(IWebDriver driver, TimeSpan timeout, ILogger logger)
        {
            _wait = new WebDriverWait(driver, timeout);
            this.Logger = logger;
        }

        public IWebElement WaitForElementToBeClickable(By locator)
        {
            Logger.Information($"Waiting for element {locator} to be clickable.");
            try
            {
                return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to wait for element {locator} to be clickable.");
                throw;
            }
        }

        public bool WaitForPageLoad(IWebDriver driver)
        {
            Logger.Information("Waiting for page to load completely.");
            try
            {
                return _wait.Until(d =>
                {
                    var js = (IJavaScriptExecutor)d;
                    return js.ExecuteScript("return document.readyState").ToString() == "complete";
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to wait for the page to load.");
                throw;
            }
        }

        public bool Until(Func<bool> condition)
        {
            Logger.Information("Waiting for a custom condition.");

            try
            {
                return _wait.Until(_ => condition());
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to wait for the custom condition.");
                throw;
            }
        }

        public ReadOnlyCollection<IWebElement> WaitForElementsToBePresent(By locator)
        {
            Logger.Information($"Waiting for elements {locator} to be present.");

            try
            {
                return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to wait for elements {locator} to be present.");
                throw;
            }
        }
    }
}