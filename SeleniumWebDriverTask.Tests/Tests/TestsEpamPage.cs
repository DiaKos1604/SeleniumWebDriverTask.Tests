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
            _navigationService = new NavigationService(Driver);
        }

        [Fact]
        public void ValidateHomePage()
        {
            LoggerHelper.LogInformation($"Starting test: {nameof(ValidateHomePage)}.");
            _navigationService.GoToPage(HomePage.Url);

            var homeService = new HomeService(Driver);
            homeService.ValidateNavigationElementsExist();
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
            _navigationService.GoToPage(HomePage.Url);

            var magnifierIconService = new MagnifierIconService(Driver);
            magnifierIconService.EnterSearchTerm(searchTerm);
            magnifierIconService.ClickFindButton();

            var searchResultsDisplayed = magnifierIconService.IsSearchResultsDisplayed(searchTerm);

            Assert.True(searchResultsDisplayed, $"Search results for '{searchTerm}' are not displayed on the search page.");
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

            Assert.True(actualFileDownloaded, $"The expected file '{expectedFileName}' was not downloaded.");
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

            Assert.True(isCorrectArticle, $"The article title: {articleTitle} not matches the previously noted title.");
        }

        [Theory]
        [InlineData("Generative AI")]
        [InlineData("Responsible AI")]
        public void ValidateNavigationToGenerativeAI(string serviceCategory)
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
        }
    }
}