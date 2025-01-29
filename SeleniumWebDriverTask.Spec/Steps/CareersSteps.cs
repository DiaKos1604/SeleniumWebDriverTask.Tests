using OpenQA.Selenium;
using SeleniumWebDriver.Business.Services;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumWebDriverTask.Spec.Steps
{
    [Binding]
    public class CareersSteps : BaseSteps
    {
        private readonly CareersService _careersService;
        private readonly HomeService _homeService;

        public CareersSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            var driver = _scenarioContext.Get<IWebDriver>(typeof(IWebDriver).FullName);
            _homeService = new HomeService(driver);
            _careersService = new CareersService(driver);
        }

        [When(@"The user navigates to the Careers page")]
        public void WhenTheUserNavigatesToTheCareersPage()
        {
            _homeService.ClickCareersLink();
        }

        [Then("The user clicks the 'Find Your Dream Job' link")]
        public void ThenTheUserClicksTheFindYourDreamJobLink()
        {
            _careersService.ClickFindYourDreamJobLink();
        }

        [Then(@"The user enters ""(.*)"" in the search field")]
        public void ThenTheUserEntersProgrammingLanguageInTheSearchField(string programmingLanguage)
        {
            _careersService.SearchJob(programmingLanguage);
        }

        [Then(@"The user clicks 'Sort by Date'")]
        public void ThenTheUserClicksSortByDate()
        {
            _careersService.SelectSearchByDate();
        }

        [When(@"The user clicks on the first job posting")]
        public void WhenTheUserClicksOnTheFirstJobPosting()
        {
            _careersService.ClickViewAndApplyForLatestJob();
        }

        [Then(@"The job description should contain ""(.*)""")]
        public void ThenTheJobDescriptionShouldContain(string programmingLanguage)
        {
            bool isDisplayed = _careersService.IsProgrammingLangElementDisplayed(programmingLanguage);
            Assert.True(isDisplayed, $"The job description did not contain the programming language '{programmingLanguage}'.");
        }
    }
}