using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace SeleniumWebDriverTask.Tests
{
    public class Tests
    {
        public class AutomatedTests : TestBase, IDisposable
        {

            [Theory]
            [InlineData("C#")]
            public void TestCase1(string programingLanguage)
            {
                driver.Navigate().GoToUrl("https://www.epam.com/");

                var careersLink = driver.FindElement(By.LinkText("Careers"));
                careersLink.Click();

                var findJobLink = driver.FindElement(By.PartialLinkText("Find Your"));
                findJobLink.Click();


                var cookieButton = wait.Until(d => d.FindElement(By.Id("onetrust-accept-btn-handler")));
                cookieButton.Click();

                var keywordsField = driver.FindElement(By.Id(("new_form_job_search-keyword")));
                keywordsField.Clear();
                keywordsField.SendKeys(programingLanguage);

                var locationField = driver.FindElement(By.ClassName(("select2-selection__rendered")));
                locationField.Click();
                var choiceLocation = driver.FindElement(By.CssSelector("li.select2-results__option"));
                choiceLocation.Click();

                var remoteOption = driver.FindElement(By.XPath("//label[@for='id-e5369b1e-5de0-3bf9-b805-e8ab3dc54483-remote']"));
                remoteOption.Click();

                var findButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(normalize-space(text()), 'Find')]")));
                findButton.Click();
                Thread.Sleep(2000);

                IWebElement GetViewAndApplyButtonForLatestob = wait.Until(d => d.FindElement(By.XPath("//ul[@class='search-result__list']/li[position()=1]/descendant::a[text()='View and apply']")));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].scrollIntoView(true);", GetViewAndApplyButtonForLatestob);

                var programmingLangElement = driver.FindElement(By.XPath($"//*[contains(text(), '{programingLanguage}')]"));
                Assert.NotNull(programmingLangElement);

            }

            [Theory]
            [InlineData("Cloud")]
            public void TestCase2(string searchTerm)
            {
                driver.Navigate().GoToUrl("https://www.epam.com/");

               
                var magnifierIcon = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("header-search__button")));
                wait.Until(d => magnifierIcon.Displayed && magnifierIcon.Enabled);
                magnifierIcon.Click();

                var searchInput = wait.Until(d => d.FindElement(By.Name("q")));
                wait.Until(d => searchInput.Displayed && searchInput.Enabled);
                searchInput.SendKeys(searchTerm);

                var findButton = driver.FindElement(By.ClassName("custom-search-button"));
                findButton.Click();
                Thread.Sleep(2000);

                Assert.True(IsSearchResultsDisplayed(searchTerm));

            }
            
            public bool IsSearchResultsDisplayed(string searchTerm)
            {
                try
                {
                    var searchResults = wait.Until(d => d.FindElements(By.XPath($"//*[contains(text(), '{searchTerm}')]")));
                    return searchResults.Count > 0;
                }
                catch (WebDriverTimeoutException)
                {
                    return false;
                }
            }
        }
    }
}