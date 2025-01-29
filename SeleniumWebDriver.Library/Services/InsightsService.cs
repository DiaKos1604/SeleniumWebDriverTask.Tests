using OpenQA.Selenium;
using SeleniumWebDriver.Business.Pages;
using SeleniumWebDriverTask.Core.Utilities;

namespace SeleniumWebDriver.Business.Services
{
    public class InsightsService
    {
        private readonly InsightsPage _page;
        private readonly IWebDriver _driver;

        public InsightsService(IWebDriver driver)
        {
            _driver = driver;
            _page = new InsightsPage(driver);
        }

        private IWebElement WaitForElement(By locator, string elementName)
        {
            LoggerHelper.LogInformation($"Waiting for '{elementName}' element.");
            return _page._waitHelper.WaitForElementToBeClickable(locator);
        }

        public void MoveToSlider()
        {
            LoggerHelper.LogInformation("Moving to the slider.");
            var slider = WaitForElement(_page.SliderLocator, "Slider");
            new ActionsHelper(_driver).ScrollToElement(slider);
        }

        public void SwipeCarousel(int times)
        {
            LoggerHelper.LogInformation($"Swiping the carousel {times} time(s).");
            var carouselArrow = WaitForElement(_page.CarouselArrowLocator, "Carousel Arrow");
            var actionsHelper = new ActionsHelper(_driver);

            for (int i = 0; i < times; i++)
            {
                actionsHelper.ClickElement(carouselArrow);
                LoggerHelper.LogInformation($"Swiped the carousel {i + 1} time(s).");
            }
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
            string currentArticleName = GetArticleName();
            bool isValid = currentArticleName.Equals(expectedName, StringComparison.OrdinalIgnoreCase);

            LoggerHelper.LogInformation(isValid
                ? $"The article title '{currentArticleName}' matches the expected title '{expectedName}'."
                : $"The article title '{currentArticleName}' does not match the expected title '{expectedName}'.");

            return isValid;
        }
    }
}