using OpenQA.Selenium;

namespace SeleniumWebDriverTask.Business.Pages
{
    public class ServicesSectionPage : BasePage
    {
        public ServicesSectionPage(IWebDriver driver) : base(driver)
        {
        }

        public By AILocator => By.XPath("//a[contains(@class, 'button-ui-23') and contains(normalize-space(./span/span[@class='button__content button__content--desktop']), 'Artificial Intelligence')]");

        public By VideoElement = By.ClassName("background-video__container");

        public By ResponsibleAILinkLocator => By.XPath("//a[contains(@class, 'button-ui-23')]//span[contains(text(),'Responsible AI')]");
        public By GenerativeAILinkLocator => By.XPath("//a[contains(@class, 'button-ui-23')]//span[contains(text(), 'Generative AI')]");
        public By TitleNameLocator => By.CssSelector(".museo-sans-500.gradient-text");
        public By OurRelatedForGenerativeLocator => By.CssSelector("div:nth-child(7) section:nth-child(1) div:nth-child(2) div:nth-child(2) div:nth-child(1) p:nth-child(1) span:nth-child(1) span:nth-child(1)");
        public By OurRelatedForResponsibleLocator => By.CssSelector("div:nth-child(8) section:nth-child(1) div:nth-child(2) div:nth-child(2) div:nth-child(1) p:nth-child(1) span:nth-child(1) span:nth-child(1)");   
    }
}