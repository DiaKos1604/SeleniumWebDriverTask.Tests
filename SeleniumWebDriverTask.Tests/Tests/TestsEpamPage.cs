using SeleniumWebDriverTask.Business.Pages;
using SeleniumWebDriverTask.Business.Services;
using SeleniumWebDriverTask.Core.Utilities;

namespace SeleniumWebDriverTask.Tests.Tests
{
    public class TestsEpamPage : TestBase
    {
        private readonly NavigationService _navigationService;
        public TestsEpamPage() : base(WebDriverManager.Instance())
        {
            _navigationService = new NavigationService(_driver);
        }

        [Fact]
        public void ValidateHomePage()
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateHomePage)}.");
            _navigationService.GoToPage();

            var homeService = new HomeService(_driver);
            homeService.ValidateNavigationElementsExist();
        }

        [Theory]
        [InlineData("C#")]
        public void ValidateJobSearch(string programmingLanguage)
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateJobSearch)}.");
            _navigationService.GoToPage();

            var homeService = new HomeService(_driver);
            homeService.ClickCareersLink();

            var careersService = new CareersService(_driver);
            careersService.ClickFindYourDreamJobLink();
            careersService.SearchJob(programmingLanguage);
            careersService.SelectSearchByDate();
            careersService.ClickViewAndApplyForLatestJob();

            var programingLangElement = careersService.IsProgrammingLangElementDisplayed(programmingLanguage);

            Assert.True(programingLangElement, $"The programming language for element {programmingLanguage} is not displayed on the Careers page.");
        }

        [Theory]
        [InlineData("Cloud")]
        public void ValidateMagnifierIcon(string searchTerm)
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateMagnifierIcon)}.");
            _navigationService.GoToPage();

            var magnifierIconService = new MagnifierIconService(_driver);
            magnifierIconService.ClickMagnifierIcon();
            magnifierIconService.EnterSearchTerm(searchTerm);
            magnifierIconService.ClickFindButton();

            var searchResultsDisplayed = magnifierIconService.IsSearchResultsDisplayed(searchTerm);

            Assert.True(searchResultsDisplayed, $"Search results for '{searchTerm}' are not displayed on the search page.");
        }

        [Fact]
        public void IsFileDownloaded()
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(IsFileDownloaded)}.");
            _navigationService.GoToPage();

            var homeService = new HomeService(_driver);
            homeService.ClickAboutLink();

            var aboutService = new AboutService(_driver);
            aboutService.ClickDownloadButton();

            var expectedFileName = "EPAM_Corporate_Overview_Q4_EOY.pdf";
            var actualFileDownloaded = aboutService.ValidateFileDownloaded(expectedFileName);

            Assert.True(actualFileDownloaded, $"The expected file '{expectedFileName}' was not downloaded.");
        }

        [Fact]
        public void ValidateInsightsPage()
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateInsightsPage)}.");
            _navigationService.GoToPage();

            var homeService = new HomeService(_driver);
            homeService.ClickInsightsLink();

            var insightsService = new InsightsService(_driver);
            insightsService.MoveToSlider();
            insightsService.SwipeCarousel(2);

            string articleTitle = insightsService.GetArticleName();

            insightsService.ClickReadMore();

            var isCorrectArticle = insightsService.ValidateArticleName(articleTitle);

            Assert.True(isCorrectArticle, $"The article title: {articleTitle} not matches the previously noted title.");
        }

        [Theory]
        [InlineData("Generative AI")]
        [InlineData("Responsible AI")]
        public void ValidateNavigationToGenerativeAI(string serviceCategory)
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateNavigationToGenerativeAI)}.");

            _navigationService.GoToPage();
            var homeService = new HomeService(_driver);
            homeService.ClickServicesLink();

            var servicesSectionService = new ServicesSectionService(_driver);
            servicesSectionService.MoveToAILink();
            servicesSectionService.StopVideo();
            servicesSectionService.SelectCategory(serviceCategory);

            var isCorrectTitle = servicesSectionService.ValidatePageTitle(serviceCategory);
            Assert.True(isCorrectTitle, $"The page title does not match the expected title: {serviceCategory}");

            var isOurRelatedSectionDisplayed = servicesSectionService.ValidateOurRelatedExpertiseSectionIsDisplayed();
            Assert.True(isOurRelatedSectionDisplayed, "The 'Our Related Expertise' section is not displayed on the page.");
        }
    }
}