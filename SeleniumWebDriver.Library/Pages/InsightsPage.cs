using OpenQA.Selenium;
using Serilog;

namespace SeleniumWebDriver.Business.Pages
{
    public class InsightsPage : BasePage
    {
        public InsightsPage(IWebDriver driver) : base(driver)
        {
        }

        public By SliderLocator => By.CssSelector(".slider-ui-23.media-content");
        public By CarouselArrowLocator => By.CssSelector(".slider__right-arrow.slider-navigation-arrow");
        public By ArticleNameLocator => By.CssSelector(".museo-sans-light");
        public By ReadMoreButtonLocator => By.CssSelector(".slider-ui-23.media-content a.custom-link.link-with-bottom-arrow.color-link.body-text.font-900.slider-cta-link");
}}