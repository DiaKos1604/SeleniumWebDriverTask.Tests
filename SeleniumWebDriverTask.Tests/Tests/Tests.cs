using SeleniumWebDriver.Library.Pages;
using SeleniumWebDriverTask.Core.Utilities;
using TasksWebDriver.Pages;

namespace SeleniumWebDriverTask.Tests.Tests
{
    public class Tests
    {
        public class AutomatedTests : TestBase
        {
            private HomePage HomePage => new HomePage(driver, TimeSpan.FromSeconds(20), logger);
            private CareersPage CareersPage => new CareersPage(driver, TimeSpan.FromSeconds(20), logger);
            private MagnifierIconPage MagnifierIconPage => new MagnifierIconPage(driver, TimeSpan.FromSeconds(20), logger);
            private AboutPage AboutPage => new AboutPage(driver, TimeSpan.FromSeconds(20), logger);
            private InsightsPage InsightsPage => new InsightsPage(driver, TimeSpan.FromSeconds(20), logger);

            [Fact]
            public void ValidateHomePage()
            {
                LoggerHelper.LogInformation($"Starting test: {nameof(ValidateHomePage)}.");

                try
                {
                    HomePage.GoTo();
                    LoggerHelper.LogInformation($"Navigated to {nameof(HomePage)}.");

                    HomePage.ValidateNavigationElementsExist();
                    LoggerHelper.LogInformation($"Validated navigation elements on {nameof(HomePage)}.");
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError(ex, $"Error occurred in {nameof(HomePage)} while processing navigation to elements on page.");
                    throw;
                }
            }

            [Theory]
            [InlineData("C#")]
            public void ValidateJobSearch(string programmingLanguage)
            {
                LoggerHelper.LogInformation($"Starting test: {nameof(ValidateJobSearch)}.");

                try
                {
                    HomePage.GoTo();
                    LoggerHelper.LogInformation($"Navigated to {nameof(HomePage)}.");
                    
                    HomePage.ClickCareersLink();
                    LoggerHelper.LogInformation($"Clicked the Careers link on the {nameof(HomePage)}.");


                    CareersPage.SearchJob(programmingLanguage);
                    LoggerHelper.LogInformation($"Searched for jobs with programming language: {programmingLanguage}.");

                    CareersPage.GetDateLabel();
                    LoggerHelper.LogInformation("Selecting a search by date.");

                    CareersPage.GetViewAndApplyButtonForLatestob();
                    LoggerHelper.LogInformation("Retrieved the View and Apply button for the latest job.");

                    var programingLangElement = CareersPage.IsProgrammingLangElementDisplayed(programmingLanguage);
                    Assert.True(programingLangElement, $"The programming language for element {programmingLanguage} is not displayed on the Careers page.");
                    LoggerHelper.LogInformation($"The programmin language for element {programmingLanguage} is displayed on the Careers page.");
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError(ex, $"Error occurred in {nameof(ValidateJobSearch)} while processing programming language: {programmingLanguage}.");
                    throw;
                }
            }

            [Theory]
            [InlineData("Cloud")]
            public void ValidateMagnifierIcon(string searchTerm)
            {
                LoggerHelper.LogInformation($"Starting test: {nameof(ValidateMagnifierIcon)}.");

                try
                {
                    HomePage.GoTo();
                    LoggerHelper.LogInformation($"Navigated to {nameof(HomePage)}.");

                    MagnifierIconPage.Search(searchTerm);
                    LoggerHelper.LogInformation($"Clicked and entered the search term: {searchTerm} in the magnifier icon.");

                    var searchResultsDisplayed = MagnifierIconPage.IsSearchResultsDisplayed(searchTerm);
                    Assert.True(searchResultsDisplayed, $"Search results for '{searchTerm}' are not displayed on the search page.");
                    LoggerHelper.LogInformation($"Search results for '{searchTerm}' are displayed on the search page.");

                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError(ex, $"Error occurred in {nameof(ValidateMagnifierIcon)} while processing the search term: {searchTerm}.");
                    throw;
                }
            }

            [Fact]
            public void IsFileDownloaded()
            {
                LoggerHelper.LogInformation($"Starting test: {nameof(IsFileDownloaded)}.");

                try
                {
                    HomePage.GoTo();
                    LoggerHelper.LogInformation($"Navigated to {nameof(HomePage)}.");

                    HomePage.ClickAboutLink();
                    LoggerHelper.LogInformation($"Clicked the About link on the {nameof(HomePage)}.");

                    var isFileDownloaded = AboutPage.ValidateFileDownloaded("EPAM_Corporate_Overview_Q4_EOY.pdf");
                    Assert.True(isFileDownloaded, "The expected file 'EPAM_Corporate_Overview_Q4_EOY.pdf' was not downloaded.");
                    LoggerHelper.LogInformation("The expected file 'EPAM_Corporate_Overview_Q4_EOY.pdf' was downloaded.");
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError(ex, $"Error occurred in {nameof(IsFileDownloaded)} while attempting to download the file 'EPAM_Corporate_Overview_Q4_EOY.pdf'.");
                    throw;
                }
            }

            [Fact]
            public void ValidateInsightsPage()
            {
                LoggerHelper.LogInformation($"Starting test: {nameof(ValidateInsightsPage)}.");

                try
                {
                    HomePage.GoTo();
                    LoggerHelper.LogInformation($"Navigated to {nameof(HomePage)}.");

                    HomePage.ClickInsightsLink();
                    LoggerHelper.LogInformation($"Clicked the Insights link on the {nameof(HomePage)}.");

                    InsightsPage.MoveToSlider();
                    LoggerHelper.LogInformation("Moved to the slider and clicked it.");

                    InsightsPage.SwipeCarousel(2);
                    LoggerHelper.LogInformation("Swiped the slider twice.");

                    string articleTitle = InsightsPage.GetArticleName();
                    LoggerHelper.LogInformation($"Get the article title: {articleTitle}.");

                    InsightsPage.ClickReadMore();
                    LoggerHelper.LogInformation($"Clicked the Read More button.");

                    var isCorrectArticle = InsightsPage.ValidateArticleName(articleTitle);
                    Assert.True(isCorrectArticle, $"The article title: {articleTitle} not matches the previously noted title.");
                    LoggerHelper.LogInformation($"The article title: {articleTitle} matches the previously noted title.");
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogError(ex, $"Error occurred in {nameof(ValidateInsightsPage)} while validating the article title.");
                    throw;
                }
            }
        }
    }
}