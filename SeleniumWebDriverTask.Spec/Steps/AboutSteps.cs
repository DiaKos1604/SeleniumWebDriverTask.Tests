using OpenQA.Selenium;
using SeleniumWebDriver.Business.Services;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumWebDriverTask.Spec.Steps
{
    [Binding]
    public class AboutSteps : BaseSteps
    {
        private readonly AboutService _aboutService;
        private readonly HomeService _homeService;

        public AboutSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            var driver = _scenarioContext.Get<IWebDriver>(typeof(IWebDriver).FullName);
            _homeService = new HomeService(driver);
            _aboutService = new AboutService(driver);
        }

        [When(@"The user navigates to the About page")]
        public void WhenTheUserNavigatesToTheAboutPage()
        {
            _homeService.ClickAboutLink();
        }

        [When(@"The user clicks the download button in the ""EPAM at a Glance"" section")]
        public void WhenTheUserClicksTheDownloadButton()
        {
            _aboutService.ClickDownloadButton();
        }

        [Then(@"The file ""(.*)"" should be successfully downloaded")]
        public void ThenTheFileShouldBeSuccessfullyDownloaded(string fileName)
        {
            _aboutService.ValidateFileDownloaded(fileName);
        }

        [Then(@"The downloaded file should match the expected content and format")]
        public void ThenTheDownloadedFileShouldMatchTheExpectedContentAndFormat()
        {
            var expectedFileName = "EPAM_Corporate_Overview_Q4_EOY.pdf";
            var actualFileDownloaded = _aboutService.ValidateFileDownloaded(expectedFileName);

            Assert.True(actualFileDownloaded, $"The expected file '{expectedFileName}' was not downloaded.");
        }
    }
}