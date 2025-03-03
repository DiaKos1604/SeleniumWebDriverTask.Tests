using OpenQA.Selenium;
using SeleniumWebDriverTask.Business.Pages;
using SeleniumWebDriverTask.Core.Utilities;

namespace SeleniumWebDriverTask.Business.Services
{
    public class AboutService
    {
        private readonly AboutPage _page;
        private readonly IWebDriver _driver;

        public AboutService(IWebDriver driver)
        {
            _driver = driver;
            _page = new AboutPage(driver);
        }


        public void ClickDownloadButton()
        {
            LoggerHelper.LogInformation("Attempting to click the download button in 'EPAM at a Glance' section.");

            _page._waitHelper.WaitForPageLoad();
            IWebElement downloadButton = _page._waitHelper.WaitForElementToBeClickable(_page.DownloadButtonLocator);
            
            new ActionsHelper(_driver).ClickElement(downloadButton);
            LoggerHelper.LogInformation("Successfully clicked the download button.");
        }

        public bool ValidateFileDownloaded(string fileName)
        {
            LoggerHelper.LogInformation($"Checking if the file '{fileName}' was downloaded successfully.");

            string downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string filePath = Path.Combine(downloadPath, fileName);

            bool isFileDownloaded = _page._waitHelper.Until(() => File.Exists(filePath));
            if (isFileDownloaded)
            {
                LoggerHelper.LogInformation($"File '{fileName}' was successfully found in the Downloads folder.");
            }
            else
            {
                LoggerHelper.LogWarning($"File '{fileName}' was not found in the Downloads folder after waiting.");
            }

            return isFileDownloaded;
        }
    }
}