using OpenQA.Selenium;

namespace SeleniumWebDriverTask.Core.Utilities
{
    public class ScreenshotMaker
    {
        private readonly IWebDriver _driver;

        public ScreenshotMaker(IWebDriver driver)
        {
            _driver = driver;
        }

        public static void TakeBrowserScreenshot(ITakesScreenshot driver, string description)
        {
            if (driver is ITakesScreenshot takesScreenshot)
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff");
                var screenshotsDirectory = Path.Combine(Environment.CurrentDirectory, "Screenshots");
                Directory.CreateDirectory(screenshotsDirectory);
                var screenshotPath = Path.Combine(screenshotsDirectory, $"{description}_{timestamp}.png");


                takesScreenshot.GetScreenshot().SaveAsFile(screenshotPath);
                LoggerHelper.LogInformation($"Screenshot taken: {screenshotPath}.");

                LoggerHelper.LogError($"Failed to take screenshot.");
            }
            else
            {
                LoggerHelper.LogInformation("Driver does not support taking screenshots.");
            }
        }
    }
}