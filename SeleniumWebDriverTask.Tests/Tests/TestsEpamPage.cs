using SeleniumWebDriver.Business.Pages;
using SeleniumWebDriver.Business.Services;
using SeleniumWebDriverTask.Core.Utilities;

namespace SeleniumWebDriverTask.Tests.Tests
{
    public class TestsEpamPage : TestBase
    {
        private readonly NavigationService _navigationService;
        public TestsEpamPage() : base(WebDriverManager.Instance())
        {
            _navigationService = new NavigationService(Driver);
        }

        [Fact]
        public void ValidateHomePage()
        {
            AssertionHelper.HandleAssert(() =>
            {
                LoggerHelper.LogInformation($"Starting test: {nameof(ValidateHomePage)}.");
                _navigationService.GoToPage(HomePage.Url);

                var homeService = new HomeService(Driver);
                homeService.ValidateNavigationElementsExist();

                _navigationService.GoToPage(HomePage.Url);

                Assert.Equal(HomePage.Url, Driver.Url);
            }, Driver);
        }

        [Theory]
        [InlineData("C#")]
        public void ValidateJobSearch(string programmingLanguage)
        {
            AssertionHelper.HandleAssert(() =>
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

                Assert.True(programingLangElement, $"The programming language for element {programmingLanguage} is not displayed on the Careers page.");
            }, Driver);
        }

        [Theory]
        [InlineData("Cloud")]
        public void ValidateMagnifierIcon(string searchTerm)
        {
            AssertionHelper.HandleAssert(() =>
            {
                LoggerHelper.LogInformation($"Starting test: {nameof(ValidateMagnifierIcon)}.");
                _navigationService.GoToPage(HomePage.Url);

                var magnifierIconService = new MagnifierIconService(Driver);
                magnifierIconService.Search(searchTerm);

                var searchResultsDisplayed = magnifierIconService.IsSearchResultsDisplayed(searchTerm);

                Assert.True(searchResultsDisplayed, $"Search results for '{searchTerm}' are not displayed on the search page.");
            }, Driver);
        }

        [Fact]
        public void IsFileDownloaded()
        {
            AssertionHelper.HandleAssert(() =>
            {
                LoggerHelper.LogInformation($"Starting test: {nameof(IsFileDownloaded)}.");
                _navigationService.GoToPage(HomePage.Url);

                var homeService = new HomeService(Driver);
                homeService.ClickAboutLink();

                var aboutService = new AboutService(Driver);
                aboutService.ClickDownloadButton();

                var expectedFileName = "EPAM_Corporate_Overview_Q4_EOY.pdf";
                var actualFileDownloaded = aboutService.ValidateFileDownloaded(expectedFileName);

                Assert.True(actualFileDownloaded, $"The expected file '{expectedFileName}' was not downloaded.");
            }, Driver);
        }

        [Fact]
        public void ValidateInsightsPage()
        {
            AssertionHelper.HandleAssert(() =>
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

                Assert.True(isCorrectArticle, $"The article title: {articleTitle} not matches the previously noted title.");
            }, Driver);
        }

        [Theory]
        [InlineData("Generative AI")]
        [InlineData("Responsible AI")]
        public void ValidateNavigationToGenerativeAI(string serviceCategory)
        {
            AssertionHelper.HandleAssert(() =>
            {
                LoggerHelper.LogInformation($"Starting test: {nameof(ValidateNavigationToGenerativeAI)}.");

                _navigationService.GoToPage(HomePage.Url);
                var homeService = new HomeService(Driver);
                homeService.ClickServicesLink();

                var servicesSectionService = new ServicesSectionService(Driver);
                servicesSectionService.MoveToAILink();
                servicesSectionService.StopVideo();
                servicesSectionService.SelectCategory(serviceCategory);

                var isCorrectTitle = servicesSectionService.ValidatePageTitle(serviceCategory);
                Assert.True(isCorrectTitle, $"The page title does not match the expected title: {serviceCategory}");

                var isOurRelatedSectionDisplayed = servicesSectionService.ValidateOurRelatedExpertiseSectionIsDisplayed();
                Assert.True(isOurRelatedSectionDisplayed, "The 'Our Related Expertise' section is not displayed on the page.");
            }, Driver);
        }
    }
}