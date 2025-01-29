using OpenQA.Selenium;
using SeleniumWebDriver.Business.Services;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumWebDriverTask.Spec.Steps
{
    [Binding]
    public class ServicesSectionSteps : BaseSteps
    {
        private readonly ServicesSectionService _servicesSectionService;
        private readonly HomeService _homeService;

        public ServicesSectionSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            var driver = _scenarioContext.Get<IWebDriver>(typeof(IWebDriver).FullName);
            _homeService = new HomeService(driver);
            _servicesSectionService = new ServicesSectionService(driver);
        }

        [When(@"The user navigates to the Services page")]
        public void WhenTheUserNavigatesToTheServicesPage()
        {
            _homeService.ClickServicesLink();
        }

        [When(@"The user moves to the Artificial Intelligence link")]
        public void WhenTheUserMovesToTheArtificialIntelligenceLink()
        {
            _servicesSectionService.MoveToAILink();
        }

        [When(@"The user selects ""(.*)"" category")]
        public void WhenTheUserSelectsCategory(string serviceCategory)
        {
            _servicesSectionService.StopVideo();
            _servicesSectionService.SelectCategory(serviceCategory);
        }

        [Then(@"The ""(.*)"" section should be displayed for ""(.*)""")]
        public void ThenTheSectionShouldBeDisplayedFor(string sectionName, string serviceCategory)
        {
            var isCorrectTitle = _servicesSectionService.ValidatePageTitle(serviceCategory);
            Assert.True(isCorrectTitle, $"The page title does not match the expected title: {serviceCategory}");

            var isOurRelatedSectionDisplayed = _servicesSectionService.ValidateOurRelatedExpertiseSectionIsDisplayed();
            Assert.True(isOurRelatedSectionDisplayed, "The 'Our Related Expertise' section is not displayed on the page.");
        }
    }
}