using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

namespace SeleniumWebDriverTask.Tests
{
    public class Tests
    {
        public class AutomatedTests : TestBase, IDisposable
        {

            [Theory]
            [MemberData(nameof(GetProgrammingLanguages))]
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

                var findButton = driver.FindElement(By.XPath("//button[contains(normalize-space(text()), 'Find')]"));
                findButton.Click();

                int previousCount = 0;
                wait.Until(d =>
                {
                    var elements = d.FindElements(By.XPath("//ul[@class='search-result__list']/li"));
                    previousCount = elements.Count;
                    return previousCount > 0;
                });

                while (true)
                {
                    try
                    {
                        var viewMoreButton = wait.Until(d =>
                        {
                            var buttons = d.FindElements(By.XPath("//a[normalize-space(text())='View More']"));
                            return buttons.FirstOrDefault(b => b.Displayed);
                        });

                        if (viewMoreButton != null)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", viewMoreButton);

                            wait.Until(d =>
                            {
                                var currentCount = d.FindElements(By.XPath("//ul[@class='search-result__list']/li")).Count;
                                return currentCount > previousCount;
                            });
                        }
                        previousCount = driver.FindElements(By.XPath("//ul[@class='search-result__list']/li")).Count;
                    }
                    catch (WebDriverTimeoutException)
                    {
                        break;
                    }
                    catch (NoSuchElementException)
                    {
                        break;
                    }
                }


                var lastElemet = wait.Until(d => d.FindElement(By.XPath("//ul[@class='search-result__list']/li[last()]")));
                Actions actions = new Actions(driver);
                actions.MoveToElement(lastElemet).Perform();


                var viewAndApplyButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//ul[@class='search-result__list']/li[last()]/descendant::a[text()='View and apply']")));
                Assert.NotNull(viewAndApplyButton);

                var programmingLangElement = driver.FindElement(By.XPath($"//*[contains(text(), {programingLanguage})]"));
                Assert.NotNull(programmingLangElement);
            }
            public static IEnumerable<object[]> GetProgrammingLanguages()
            {
                yield return new object[] { "C#" };
                yield return new object[] { "Java" };
                yield return new object[] { "Python" };
                yield return new object[] { "JavaScript" };
                yield return new object[] { "TypeScript" };
                yield return new object[] { "C++" };
                yield return new object[] { "C" };
                yield return new object[] { "Ruby" };
                yield return new object[] { "Swift" };
                yield return new object[] { "Kotlin" };
                yield return new object[] { "Go" };
                yield return new object[] { "Rust" };
                yield return new object[] { "PHP" };
                yield return new object[] { "Perl" };
                yield return new object[] { "Scala" };
                yield return new object[] { "Objective-C" };
                yield return new object[] { "Haskell" };
                yield return new object[] { "Dart" };
                yield return new object[] { "R" };
                yield return new object[] { "MATLAB" };
                yield return new object[] { "Shell" };
                yield return new object[] { "PowerShell" };
                yield return new object[] { "Visual Basic" };
                yield return new object[] { "Assembly" };
                yield return new object[] { "SQL" };
                yield return new object[] { "Groovy" };
                yield return new object[] { "Lua" };
                yield return new object[] { "Elixir" };
                yield return new object[] { "Erlang" };
                yield return new object[] { "F#" };
                yield return new object[] { "Prolog" };
                yield return new object[] { "COBOL" };
                yield return new object[] { "Ada" };
                yield return new object[] { "Fortran" };
                yield return new object[] { "Delphi" };
                yield return new object[] { "Lisp" };
                yield return new object[] { "Pascal" };
                yield return new object[] { "Scheme" };
            }


            [Theory]
            [InlineData("Cloud")]
            [InlineData("Automation")]
            [InlineData("BLOCKCHAIN")]
            public void TestCase2(string searchTerm)
            {
                driver.Navigate().GoToUrl("https://www.epam.com/");

                var magnifierIcon = driver.FindElement(By.ClassName("header-search__button"));
                magnifierIcon.Click();

                var searchInput = driver.FindElement(By.Name("q"));
                searchInput.Clear();
                searchInput.SendKeys(searchTerm);

                var findButton = driver.FindElement(By.ClassName("custom-search-button"));
                findButton.Click();

                var links = driver.FindElements(By.TagName("a"));
                foreach (var link in links)
                {
                    Assert.Contains(searchTerm, link.Text, StringComparison.OrdinalIgnoreCase);
                }
            }

        }
    }
}