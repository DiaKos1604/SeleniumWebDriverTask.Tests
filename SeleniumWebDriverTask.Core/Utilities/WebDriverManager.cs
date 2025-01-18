using OpenQA.Selenium;
using SeleniumWebDriver.Library.Utilities;
using SeleniumWebDriverTask.Core.Utilities;

public class WebDriverManager
{ 
    private static IWebDriver? driver;
    private static readonly object lockObject = new object();
    public static IWebDriver GetDriver(string browserType, bool headless)
    {
        if (driver == null)
        {
            lock (lockObject)
            {
                if (driver == null)
                {
                    driver = BrowserFactory.CreateBrowser(browserType, headless);
                }
            }
        }
        return driver;
    }
    public static void QuitDriver()
    {
        if (driver != null)
        {
            LoggerHelper.LogInformation("Attempting to close the browser.");
            try
            {
                driver.Quit();
                LoggerHelper.LogInformation("Browser closed successfully.");
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(ex, "Failed to close the browser properly.");
            }
            finally
            {
                driver = null;
                LoggerHelper.LogInformation("Driver instance set to null.");
            }
        }
    }
}