using OpenQA.Selenium;
using SeleniumWebDriver.Library.Pages;
using TasksWebDriver.Pages;

namespace SeleniumWebDriverTask.Tests
{
    public class Tests
    {
        public class AutomatedTests : WebDriverManager, IDisposable
        {
            private IWebDriver driver;

            public AutomatedTests()
            {
                driver = WebDriverManager.GetWebDriver();
            }

            private HomePage HomePage => new HomePage(driver, TimeSpan.FromSeconds(20));
            private CareersPage CareersPage => new CareersPage(driver, TimeSpan.FromSeconds(20));
            private MagnifierIconPage MagnifierIconPage => new MagnifierIconPage(driver, TimeSpan.FromSeconds(20));
            private AboutPage AboutPage => new AboutPage(driver, TimeSpan.FromSeconds(20));
            private InsightsPage InsightsPage => new InsightsPage(driver, TimeSpan.FromSeconds(20));

            [Fact]
            public void ValidateHomePage()
            {
                HomePage.GoTo();
                HomePage.ValidateNavigationElementsExist();
            }

            [Theory]
            [InlineData("C#")]
            public void ValidateJobSearch(string programmingLanguage)
            {
                HomePage.GoTo();
                HomePage.ClickCareersLink();

                CareersPage.SearchJob(programmingLanguage);
                CareersPage.GetDateLabel();
                CareersPage.GetViewAndApplyButtonForLatestob();

                var programingLangElement = CareersPage.IsProgrammingLangElementDisplayed(programmingLanguage);
                Assert.True(programingLangElement, $"Programming language {programmingLanguage} is not found");
            }

            [Theory]
            [InlineData("Cloud")]
            public void ValidateMagnifierIcon(string searchTerm)
            {
                HomePage.GoTo();
                MagnifierIconPage.Search(searchTerm);

                var searchResultsDisplayed = MagnifierIconPage.IsSearchResultsDisplayed(searchTerm);
                Assert.True(searchResultsDisplayed, "Search results are not displayed");
            }

            [Fact]
            public void IsFileDownloaded()
            {
                HomePage.GoTo();
                HomePage.ClickAboutLink();

                var isFileDownloaded = AboutPage.ValidateFileDownloaded("EPAM_Corporate_Overview_Q4_EOY.pdf");
                Assert.True(isFileDownloaded, "The expected file was not downloaded.");
            }

            [Fact]
            public void ValidateInsightsPage()
            {
                HomePage.GoTo();
                HomePage.ClickInsightsLink();

                InsightsPage.MoveToSlider();
                InsightsPage.SwipeCarousel(2);

                string articleTitle = InsightsPage.GetArticleName();
                InsightsPage.ClickReadMore();

                var isCorrectArticle = InsightsPage.ValidateArticleName(articleTitle);
                Assert.True(isCorrectArticle, "Article title does not match.");
            }

            public void Dispose()
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}