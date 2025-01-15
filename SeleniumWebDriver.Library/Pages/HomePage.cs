using OpenQA.Selenium;

namespace SeleniumWebDriver.Library.Pages
{
    public class HomePage : EpamMainPage
    {
        public HomePage(IWebDriver driver, TimeSpan timeout) : base(driver, timeout)
        {
        }

        public void ClickCareersLink() => waitHelper.WaitForElementToBeClickable(By.LinkText("Careers")).Click();
        public void ClickMagnifierIcon() => waitHelper.WaitForElementToBeClickable(By.ClassName("header-search__button")).Click();
        public void ClickAboutLink() => waitHelper.WaitForElementToBeClickable(By.LinkText("About")).Click();
        public void ClickInsightsLink() => waitHelper.WaitForElementToBeClickable(By.LinkText("Insights")).Click();

        public void ValidateNavigationElementsExist()
        {
            waitHelper.WaitForPageLoad(driver);
            ClickCareersLink();
            waitHelper.WaitForPageLoad(driver);
            ClickMagnifierIcon();
            waitHelper.WaitForPageLoad(driver);
            ClickAboutLink();
            waitHelper.WaitForPageLoad(driver);
            ClickInsightsLink();
        }
    }
}