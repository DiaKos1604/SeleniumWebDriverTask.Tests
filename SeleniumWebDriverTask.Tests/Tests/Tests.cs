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
            _navigationService = new NavigationService(Driver, Logger);
        }

        [Fact]
        public void ValidateHomePage()
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateHomePage)}.");
            _navigationService.GoToPage(HomePage.Url);

            var homeService = new HomeService(Driver, TimeSpan.FromSeconds(30), Logger);
            homeService.ValidateNavigationElementsExist();
        }

        [Theory]
        [InlineData("C#")]
        public void ValidateJobSearch(string programmingLanguage)
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateJobSearch)}.");
            _navigationService.GoToPage(HomePage.Url);

            var homeService = new HomeService(Driver, TimeSpan.FromSeconds(30), Logger);
            homeService.ClickCareersLink();

            var careersService = new CareersService(Driver, TimeSpan.FromSeconds(30), Logger);
            careersService.SearchJob(programmingLanguage);
            careersService.GetDateLabel();
            careersService.GetViewAndApplyButtonForLatestob();

            var programingLangElement = careersService.IsProgrammingLangElementDisplayed(programmingLanguage);
            Assert.True(programingLangElement, $"The programming language for element {programmingLanguage} is not displayed on the Careers page.");
        }

        [Theory]
        [InlineData("Cloud")]
        public void ValidateMagnifierIcon(string searchTerm)
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateMagnifierIcon)}.");
            _navigationService.GoToPage(HomePage.Url);

            var magnifierIconService = new MagnifierIconService(Driver, TimeSpan.FromSeconds(30), Logger);
            magnifierIconService.Search(searchTerm);

            var searchResultsDisplayed = magnifierIconService.IsSearchResultsDisplayed(searchTerm);
            Assert.True(searchResultsDisplayed, $"Search results for '{searchTerm}' are not displayed on the search page.");
        }

        [Fact]
        public void IsFileDownloaded()
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(IsFileDownloaded)}.");
            _navigationService.GoToPage(HomePage.Url);

            var homeService = new HomeService(Driver, TimeSpan.FromSeconds(30), Logger);
            homeService.ClickAboutLink();

            var aboutService = new AboutService(Driver, TimeSpan.FromSeconds(30), Logger);
            aboutService.ClickDownloadButton();

            var isFileDownloaded = aboutService.ValidateFileDownloaded("EPAM_Corporate_Overview_Q4_EOY.pdf");
            Assert.True(isFileDownloaded, "The expected file 'EPAM_Corporate_Overview_Q4_EOY.pdf' was not downloaded.");
        }

        [Fact]
        public void ValidateInsightsPage()
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateInsightsPage)}.");
            _navigationService.GoToPage(HomePage.Url);

            var homeService = new HomeService(Driver, TimeSpan.FromSeconds(30), Logger);
            homeService.ClickInsightsLink();

            var insightsService = new InsightsService(Driver, TimeSpan.FromSeconds(30), Logger);
            insightsService.MoveToSlider();
            insightsService.SwipeCarousel(2);

            string articleTitle = insightsService.GetArticleName();

            insightsService.ClickReadMore();

            var isCorrectArticle = insightsService.ValidateArticleName(articleTitle);
            Assert.True(isCorrectArticle, $"The article title: {articleTitle} not matches the previously noted title.");
        }
    }
}