using OpenQA.Selenium;

namespace SeleniumWebDriverTask.Core.Utilities
{
    public class ScreenshotMaker
    {
        private readonly IWebDriver driver;

        public ScreenshotMaker(IWebDriver driver)
        {
            this.driver = driver;
        }

        public static void TakeBrowserScreenshot(ITakesScreenshot driver, string description)
        {
            if (driver is ITakesScreenshot takesScreenshot)
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff");
                var screenshotsDirectory = Path.Combine(Environment.CurrentDirectory, "Screenshots");
                Directory.CreateDirectory(screenshotsDirectory);
                var screenshotPath = Path.Combine(screenshotsDirectory, $"{description}_{timestamp}.png");

                try
                {
                    takesScreenshot.GetScreenshot().SaveAsFile(screenshotPath);
                    LoggerHelper.LogInformation($"Screenshot taken: {screenshotPath}.");
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError(ex, $"Failed to take screenshot.");
                }
            }
            else
            { 
                LoggerHelper.LogInformation("Driver does not support taking screenshots.");
            }
        }
    }
}