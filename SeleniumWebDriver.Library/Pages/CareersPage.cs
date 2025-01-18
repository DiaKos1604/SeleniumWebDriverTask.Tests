using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Serilog;

namespace SeleniumWebDriver.Library.Pages
{
    public class CareersPage : EpamMainPage
    {
        public CareersPage(IWebDriver driver, TimeSpan timeout, ILogger logger) : base(driver, timeout, logger)
        {
        }

        public IWebElement FindJobLink => waitHelper.WaitForElementToBeClickable(By.PartialLinkText("Find Your"));
        public IWebElement KeywordsField => waitHelper.WaitForElementToBeClickable(By.Id("new_form_job_search-keyword"));
        public IWebElement LocationField => waitHelper.WaitForElementToBeClickable(By.ClassName("select2-selection__rendered"));
        public IWebElement ChoiceLocation => waitHelper.WaitForElementToBeClickable(By.CssSelector("li.select2-results__option"));
        public IWebElement RemoteOption => waitHelper.WaitForElementToBeClickable(By.XPath("//label[@for='id-e5369b1e-5de0-3bf9-b805-e8ab3dc54483-remote']"));
        public IWebElement FindButton => waitHelper.WaitForElementToBeClickable(By.XPath("//button[contains(normalize-space(text()), 'Find')]"));

        public void SearchJob(string programmingLanguage)
        {
            new Actions(driver).Click(FindJobLink).Perform();
            KeywordsField.Clear();
            KeywordsField.SendKeys(programmingLanguage);
            LocationField.Click();
            ChoiceLocation.Click();
            RemoteOption.Click();
            FindButton.Click();
        }

        public void GetDateLabel()
        {
            IWebElement dateElement = waitHelper.WaitForElementToBeClickable(By.CssSelector("label[for='sort-time']"));
            new Actions(driver).Click(dateElement).Perform();
        }

        public void GetViewAndApplyButtonForLatestob()
        {
            waitHelper.WaitForPageLoad(driver);
            IWebElement latestElement = waitHelper.WaitForElementToBeClickable(By.XPath("//ul[@class='search-result__list']/li[position()=1]/descendant::a[text()='View and apply']"));
            new Actions(driver).Click(latestElement).Perform();
        }

        public bool IsProgrammingLangElementDisplayed(string programmingLanguage)
        {
            var programmingLangElement = waitHelper.WaitForElementsToBePresent(By.XPath($"//*[contains(text(), '{programmingLanguage}')]"));
            return programmingLangElement != null;
        }
    }
}