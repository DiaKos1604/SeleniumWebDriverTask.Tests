using OpenQA.Selenium;

namespace SeleniumWebDriverTask.Core.Utilities
{
    public class ScreenshotMaker()
    {
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
            }
            else
            {
                LoggerHelper.LogInformation("Driver does not support taking screenshots.");
            }
        }
    }
}