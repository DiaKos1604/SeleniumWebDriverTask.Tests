using OpenQA.Selenium;

namespace SeleniumWebDriverTask.Core.Utilities
{
    public static class AssertionHelper
    {
        public static void HandleAssert(Action assert, IWebDriver driver)
        {
            try
            {
                assert.Invoke();
            }
            catch (Exception e)
            {
                LoggerHelper.LogInformation($"Exception: {e}.");
                ScreenshotMaker.TakeBrowserScreenshot((ITakesScreenshot)driver, "TestFailure");
                throw;
            }
        }
    }
}
