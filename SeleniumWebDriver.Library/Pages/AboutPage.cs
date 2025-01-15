using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumWebDriver.Library.Pages;

namespace TasksWebDriver.Pages
{
    public class AboutPage : EpamMainPage
    {
        public AboutPage(IWebDriver driver, TimeSpan timeout) : base(driver, timeout)
        {
        }

        public void ClickDownloadButton()
        { 
            IWebElement downloadButton = waitHelper.WaitForElementToBeClickable(By.CssSelector("a.button-ui-23.btn-focusable[href*='EPAM_Corporate_Overview_Q4_EOY.pdf']"));
            waitHelper.WaitForPageLoad(driver);
            new Actions(driver).Click(downloadButton).Perform();
        }

        public bool ValidateFileDownloaded(string fileName)
        {
            string downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string filePath = Path.Combine(downloadPath, fileName);

            return waitHelper.Until(() => File.Exists(filePath));
        }
    }
}