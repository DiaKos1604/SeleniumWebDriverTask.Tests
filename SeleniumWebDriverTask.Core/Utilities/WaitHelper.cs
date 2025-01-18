using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog;
using System.Collections.ObjectModel;

namespace TasksWebDriver.Utilities
{
    public class WaitHelper
    {
        private readonly WebDriverWait wait;
        private ILogger logger;

        public WaitHelper(IWebDriver driver, TimeSpan timeout, ILogger logger)
        {
            wait = new WebDriverWait(driver, timeout);
            this.logger = logger;
        }

        public IWebElement WaitForElementToBeClickable(By locator)
        {
            logger.Information($"Waiting for element {locator} to be clickable.");
            try
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Failed to wait for element {locator} to be clickable.");
                throw;
            }
        }

        public bool WaitForPageLoad(IWebDriver driver)
        {
            logger.Information("Waiting for page to load completely.");
            try
            {
                return wait.Until(d =>
                {
                    var js = (IJavaScriptExecutor)d;
                    return js.ExecuteScript("return document.readyState").ToString() == "complete";
                });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Failed to wait for the page to load.");
                throw;
            }
        }

        public bool Until(Func<bool> condition)
        {
            logger.Information("Waiting for a custom condition.");

            try
            {
                return wait.Until(_ => condition());
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Failed to wait for the custom condition.");
                throw;
            }
        }

        public ReadOnlyCollection<IWebElement> WaitForElementsToBePresent(By locator)
        {
            logger.Information($"Waiting for elements {locator} to be present.");

            try
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Failed to wait for elements {locator} to be present.");
                throw;
            }
        }
    }
}