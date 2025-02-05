using OpenQA.Selenium;
using SeleniumWebDriverTask.Business.Services;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumWebDriverTask.Spec.Steps
{
    [Binding]
    public class InsightsSteps : BaseSteps
    {
        private readonly InsightsService _insightsService;
        private readonly HomeService _homeService;

        public InsightsSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            var driver = _scenarioContext.Get<IWebDriver>(typeof(IWebDriver).FullName);
            _homeService = new HomeService(driver);
            _insightsService = new InsightsService(driver);
        }

        [When(@"The user navigates to the Insights page")]
        public void WhenTheUserNavigatesToTheInsightsPage()
        {
            _homeService.ClickInsightsLink();
        }

        [When(@"The user moves to the slider$")]
        public void WhenTheUserMovesToTheSlider()
        {
            _insightsService.MoveToSlider();
        }

        [When(@"The user swipes the carousel 2 times")]
        public void WhenTheUserSwipesTheCarousel()
        {
            _insightsService.SwipeCarousel(2);
        }

        [Then(@"The user clicks the 'Read More' button$")]
        public void ThenTheUserClicksTheReadMoreButton()
        {
            _insightsService.ClickReadMore();
        }

        [Then(@"The user should see an article titled")]
        public void ThenTheUserShouldSeeAnArticleTitled()
        {
            var articleTitle = _insightsService.GetArticleName();
            var isCorrectArticle = _insightsService.ValidateArticleName(articleTitle);

            Assert.True(isCorrectArticle, $"The article title: {articleTitle} not matches the previously noted title.");
        }
    }
}