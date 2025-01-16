using OpenQA.Selenium;
using SeleniumWebDriver.Library.Utilities;

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
            driver.Quit();
            driver = null;
        }
    }
}