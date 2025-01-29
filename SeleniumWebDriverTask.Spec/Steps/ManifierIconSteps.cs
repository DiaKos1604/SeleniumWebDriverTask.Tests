using OpenQA.Selenium;
using SeleniumWebDriver.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumWebDriverTask.Spec.Steps
{
    [Binding]
    public class ManifierIconSteps : BaseSteps
    {
        private readonly MagnifierIconService _magnifierIconService;
        private readonly HomeService _homeService;

        public ManifierIconSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
            var driver = _scenarioContext.Get<IWebDriver>(typeof(IWebDriver).FullName);
            _homeService = new HomeService(driver);
            _magnifierIconService = new MagnifierIconService(driver);
        }

        [When(@"The user navigates to the Magnifier icon page")]
        public void WhenTheUserNavigatesToTheMagnifierIconPage()
        {
            _homeService.ClickMagnifierIcon();
        }

        [Then(@"The user enters in the search field ""(.*)""")]
        public void ThenTheUserEntersInTheSearchFieldSearchTerm(string serchTerm)
        {
            _magnifierIconService.EnterSearchTerm(serchTerm);
        }

        [Then(@"The user clicks the 'Find' button")]
        public void ThenTheUserClicksTheFindButton()
        {
            _magnifierIconService.ClickFindButton();
        }

        [Then(@"The searching results should contain ""(.*)""")]
        public void ThenTheSearchingResultsShouldContain(string searchTerm)
        {
            var searchResultsDisplayed = _magnifierIconService.IsSearchResultsDisplayed(searchTerm);

            Assert.True(searchResultsDisplayed, $"Search results for '{searchTerm}' are not displayed on the search page.");
        }
    }
}