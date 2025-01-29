using OpenQA.Selenium;
using SeleniumWebDriver.Business.Pages;
using SeleniumWebDriverTask.Core.Utilities;

namespace SeleniumWebDriver.Business.Services
{
    public class ServicesSectionService
    {
        private readonly ServicesSectionPage _page;
        private readonly IWebDriver _driver;
        private string? _lastSelectedCategory;

        public ServicesSectionService(IWebDriver driver)
        {
            _driver = driver;
            _page = new ServicesSectionPage(driver);
        }

        public void MoveToAILink()
        {
            LoggerHelper.LogInformation($"Move to Artificial Intelligence link.");

            _page._waitHelper.WaitForPageLoad();
            IWebElement webElement = _page._waitHelper.WaitForElementToBeVisible(_page.AILocator);
            var jsHelper = new JavaScriptHelper(_driver);
            jsHelper.ClickElement(webElement);
        }

        public void StopVideo()
        {
            _page.WaitForPageLoad();

            IWebElement pauseVideo = _page._waitHelper.WaitForElementToBeClickable(_page.VideoElement);
            var jsHelper = new JavaScriptHelper(_driver);
            jsHelper.PauseElement(pauseVideo);
        }

        public void SelectCategory(string serviceCategory)
        {
            LoggerHelper.LogInformation($"Selecting service category: {serviceCategory}.");
            _lastSelectedCategory = serviceCategory.ToLower();
            By categoryLocator = _lastSelectedCategory switch
            {
                "responsible ai" => _page.ResponsibleAILinkLocator,
                "generative ai" => _page.GenerativeAILinkLocator,
                _ => throw new ArgumentException($"Invalid service category: {serviceCategory}"),
            };

            _page.WaitForPageLoad();

            IWebElement categoryElement = _page._waitHelper.WaitForElementToBeClickable(categoryLocator);
            var jsHelper = new JavaScriptHelper(_driver);
            jsHelper.ScrollToElement(categoryElement);
            jsHelper.ClickElement(categoryElement);

            LoggerHelper.LogInformation($"Successfully selected the service category: {serviceCategory}.");
        }

        public bool ValidatePageTitle(string expectedTitle)
        {
            LoggerHelper.LogInformation($"Validating the page title: '{expectedTitle}'.");

            var actualTitle = _page._waitHelper.WaitForElementToBeVisible(_page.TitleNameLocator).Text;
            bool isValid = actualTitle.Equals(expectedTitle, StringComparison.OrdinalIgnoreCase);

            if (isValid)
            {
                LoggerHelper.LogInformation($"The article title: {actualTitle} matches the expected title: {expectedTitle}.");
            }
            else
            {
                LoggerHelper.LogInformation($"The article title: {actualTitle} does not match the expected title: {expectedTitle}.");
            }
            return isValid;
        }

        public bool ValidateOurRelatedExpertiseSectionIsDisplayed()
        {
            try
            {
                _page.WaitForPageLoad();
                By locator = _lastSelectedCategory switch
                {
                    "responsible ai" => _page.OurRelatedForResponsibleLocator,
                    "generative ai" => _page.OurRelatedForGenerativeLocator,
                    _ => throw new InvalidOperationException($"Unable to determine the correct locator, because no valid category was selected previously or category selection was not performed."),
                };

                var element = _page._waitHelper.WaitForElementToBeVisible(locator);

                LoggerHelper.LogInformation("'Our Related Expertise' section validation successful.");
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                LoggerHelper.LogInformation("'Our Related Expertise' section is not displayed.");
                return false;
            }
        }
    }
}