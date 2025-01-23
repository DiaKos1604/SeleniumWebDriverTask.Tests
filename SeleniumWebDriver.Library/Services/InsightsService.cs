using OpenQA.Selenium;
using SeleniumWebDriver.Business.Pages;
using SeleniumWebDriverTask.Core.Utilities;
using Serilog;

namespace SeleniumWebDriver.Business.Services
{
    public class InsightsService
    {
        private readonly InsightsPage _page;
        private readonly IWebDriver _driver;

        public InsightsService(IWebDriver driver, TimeSpan timeout, ILogger logger)
        {
            _driver = driver;
            _page = new InsightsPage(driver, timeout, logger);
        }

        public void MoveToSlider()
        {
            LoggerHelper.LogInformation("Moving to the slider.");
            var slider = _driver.FindElement(_page.SliderLocator);
            var actionsHelper = new ActionsHelper(_driver);
            actionsHelper.ScrollToElement(slider);
            LoggerHelper.LogInformation("Successfully moved to the slider.");
        }

        public void SwipeCarousel(int times)
        {
            LoggerHelper.LogInformation($"Swiping the carousel {times} times.");
            IWebElement carouselArrow = _page._waitHelper.WaitForElementToBeClickable(_page.CarouselArrowLocator);
            ActionsHelper actionHelper = new ActionsHelper(_driver);
            for (int i = 0; i < times; i++)
            {
                actionHelper.ClickElement(carouselArrow);
                LoggerHelper.LogInformation($"Swiped the carousel {i + 1} time(s).");
            }
            LoggerHelper.LogInformation("Finished swiping the carousel.");
        }

        public string GetArticleName()
        {
            LoggerHelper.LogInformation("Retrieving the article title.");
            _page._waitHelper.Until(() => ((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState").Equals("complete"));
            var articleName = _driver.FindElement(_page.ArticleNameLocator);
            string title = articleName.Text;
            LoggerHelper.LogInformation($"Retrieved the article title: {title}.");
            return title;
        }

        public void ClickReadMore()
        {
            LoggerHelper.LogInformation("Clicking the 'Read More' button.");
            IWebElement linkButton = _driver.FindElement(_page.ReadMoreButtonLocator);
            var jsHelper = new JavaScriptHelper(_driver);
            jsHelper.ClickElement(linkButton);
            LoggerHelper.LogInformation("Successfully clicked the 'Read More' button.");
        }

        public bool ValidateArticleName(string expectedName)
        {
            LoggerHelper.LogInformation("Validating the article title.");
            string currentArticleName = _driver.FindElement(_page.ArticleNameLocator).Text;
            bool isValid = currentArticleName.Equals(expectedName, StringComparison.OrdinalIgnoreCase);
            if (isValid)
            {
                LoggerHelper.LogInformation($"The article title: {currentArticleName} matches the expected title: {expectedName}.");
            }
            else
            {
                LoggerHelper.LogInformation($"The article title: {currentArticleName} does not match the expected title: {expectedName}.");
            }
            return isValid;
        }
    }
}