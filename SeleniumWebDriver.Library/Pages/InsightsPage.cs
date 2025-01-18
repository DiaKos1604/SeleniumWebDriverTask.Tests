using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumWebDriver.Library.Pages;
using Serilog;

namespace TasksWebDriver.Pages
{
    public class InsightsPage : EpamMainPage
    {
        public InsightsPage(IWebDriver driver, TimeSpan timeout, ILogger logger) : base(driver, timeout, logger)
        {
        }

        public void MoveToSlider()
        {
            var slider = driver.FindElement(By.CssSelector(".slider-ui-23.media-content"));
            new Actions(driver).ScrollToElement(slider).Perform();
        }

        public void SwipeCarousel(int times)
        {
            IWebElement carouselArrow = waitHelper.WaitForElementToBeClickable(By.CssSelector(".slider__right-arrow.slider-navigation-arrow"));
            Actions actions = new Actions(driver);
            for (int i = 0; i < times; i++)
            {
                actions.Click(carouselArrow).Perform();
            }
        }

        public string GetArticleName()
        {
            waitHelper.Until(() => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            var articleName = driver.FindElement(By.CssSelector(".museo-sans-light"));
            return articleName.Text;
        }

        public void ClickReadMore()
        {
            IWebElement linkButton = driver.FindElement(By.CssSelector(".slider-ui-23.media-content a.custom-link.link-with-bottom-arrow.color-link.body-text.font-900.slider-cta-link"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", linkButton);
        }

        public bool ValidateArticleName(string expectedName)
        {
            var currentArticleName = driver.FindElement((By.CssSelector(".museo-sans-light"))).Text;
            return currentArticleName.Equals(expectedName, StringComparison.OrdinalIgnoreCase);
        }
    }
}