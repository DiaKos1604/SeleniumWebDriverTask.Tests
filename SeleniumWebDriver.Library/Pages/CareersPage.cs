﻿using OpenQA.Selenium;
using Serilog;

namespace SeleniumWebDriver.Business.Pages
{
    public class CareersPage : BasePage
    {
        public CareersPage(IWebDriver driver, TimeSpan timeout, ILogger logger) : base(driver, timeout, logger)
        {
        }

        public By FindJobLinkLocator => By.PartialLinkText("Find Your");
        public By KeywordsFieldLocator => By.Id("new_form_job_search-keyword");
        public By LocationFieldLocator => By.ClassName("select2-selection__rendered");
        public By ChoiceLocationLocator => By.CssSelector("li.select2-results__option");
        public By RemoteOptionLocator => By.XPath("//label[@for='id-e5369b1e-5de0-3bf9-b805-e8ab3dc54483-remote']");
        public By FindButtonLocator => By.XPath("//button[contains(normalize-space(text()), 'Find')]");
    }
}