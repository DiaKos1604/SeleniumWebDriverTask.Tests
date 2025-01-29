using OpenQA.Selenium;
using SeleniumWebDriver.Business.Pages;
using SeleniumWebDriver.Business.Services;
using TechTalk.SpecFlow;

namespace SeleniumWebDriverTask.Spec.Steps
{
    [Binding]
    public class HomeSteps : BaseSteps
    {
        private readonly HomeService _homeService;
        private readonly NavigationService _navigationService;

        public HomeSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            var driver = _scenarioContext.Get<IWebDriver>(typeof(IWebDriver).FullName);
            _homeService = new HomeService(driver);
            _navigationService = new NavigationService(driver);
        }

        [Given(@"The user is on the EPAM home page")]
        public void GivenTheUserIsOnTheEPAMHomePage()
        {
            _navigationService.GoToPage(HomePage.Url);
        }

        [Then(@"The user validate navigation to main tabs")]
        public void ThenTheUserValidateNavigationToMainTabs()
        {
            _homeService.ValidateNavigationElementsExist();
        }
    }
}