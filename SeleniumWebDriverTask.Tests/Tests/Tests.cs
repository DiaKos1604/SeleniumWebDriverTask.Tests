using OpenQA.Selenium;
using SeleniumWebDriver.Business.Pages;
using SeleniumWebDriver.Business.Services;
using SeleniumWebDriverTask.Core.Utilities;

namespace SeleniumWebDriverTask.Tests
{
    public class Tests : TestBase
    {
        private readonly NavigationService _navigationService;
        public Tests() : base(WebDriverManager.Instance())
        {
            _navigationService = new NavigationService(Driver);
        }

        [Fact]
        public void ValidateHomePage()
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateHomePage)}.");
            _navigationService.GoToPage(HomePage.Url);

            var homeService = new HomeService(Driver);
            homeService.ValidateNavigationElementsExist();

             _navigationService.GoToPage(HomePage.Url);
            AssertionHelper.HandleAssert(() =>
            {
                Assert.Equal(HomePage.Url, Driver.Url);
            }, Driver);
        }

        [Theory]
        [InlineData("C#")]
        public void ValidateJobSearch(string programmingLanguage)
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateJobSearch)}.");
            _navigationService.GoToPage(HomePage.Url);

            var homeService = new HomeService(Driver);
            homeService.ClickCareersLink();

            var careersService = new CareersService(Driver);
            careersService.SearchJob(programmingLanguage);
            careersService.GetDateLabel();
            careersService.GetViewAndApplyButtonForLatestob();

            var programingLangElement = careersService.IsProgrammingLangElementDisplayed(programmingLanguage);
            AssertionHelper.HandleAssert(() =>
            {
                Assert.True(programingLangElement, $"The programming language for element {programmingLanguage} is not displayed on the Careers page.");
            }, Driver);
        }

        [Theory]
        [InlineData("Cloud")]
        public void ValidateMagnifierIcon(string searchTerm)
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateMagnifierIcon)}.");
            _navigationService.GoToPage(HomePage.Url);

            var magnifierIconService = new MagnifierIconService(Driver);
            magnifierIconService.Search(searchTerm);

            var searchResultsDisplayed = magnifierIconService.IsSearchResultsDisplayed(searchTerm);
            AssertionHelper.HandleAssert(() =>
            {
                Assert.True(searchResultsDisplayed, $"Search results for '{searchTerm}' are not displayed on the search page.");
            }, Driver);
        }

        [Fact]
        public void IsFileDownloaded()
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(IsFileDownloaded)}.");
            _navigationService.GoToPage(HomePage.Url);

            var homeService = new HomeService(Driver);
            homeService.ClickAboutLink();

            var aboutService = new AboutService(Driver);
            aboutService.ClickDownloadButton();

            var expectedFileName = "EPAM_Corporate_Overview_Q4_EOY.pdf";
            var actualFileDownloaded = aboutService.ValidateFileDownloaded(expectedFileName);
            AssertionHelper.HandleAssert(() =>
            {
                Assert.True(actualFileDownloaded, $"The expected file '{expectedFileName}' was not downloaded.");
            }, Driver);
        }

        [Fact]
        public void ValidateInsightsPage()
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateInsightsPage)}.");
            _navigationService.GoToPage(HomePage.Url);

            var homeService = new HomeService(Driver);
            homeService.ClickInsightsLink();

            var insightsService = new InsightsService(Driver);
            insightsService.MoveToSlider();
            insightsService.SwipeCarousel(2);

            string articleTitle = insightsService.GetArticleName();

            insightsService.ClickReadMore();

            var isCorrectArticle = insightsService.ValidateArticleName(articleTitle);
            AssertionHelper.HandleAssert(() =>
            {
                Assert.True(isCorrectArticle, $"The article title: {articleTitle} not matches the previously noted title.");
            }, Driver);
        }
    }
}