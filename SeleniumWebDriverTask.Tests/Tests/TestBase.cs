﻿using OpenQA.Selenium;
using SeleniumWebDriverTask.Business.Pages;
using SeleniumWebDriverTask.Core.Utilities;

namespace SeleniumWebDriverTask.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected IWebDriver Driver;

        protected HomePage _homePage;
        protected CareersPage _careersPage;
        protected MagnifierIconPage _magnifierIconPage;
        protected AboutPage _aboutPage;
        protected InsightsPage _insightsPage;
        protected ServicesSectionPage _servicesSectionPage;

        protected TestBase(WebDriverManager webDriverManager)
        {
            Driver = webDriverManager.GetWebDriver(
                    ConfigurationHelper.GetBrowserType(),
                    ConfigurationHelper.GetHeadlessOption());
            LoggerHelper.LogInformation("TestBase initialized.");

            _homePage = new HomePage(Driver);
            _careersPage = new CareersPage(Driver);
            _magnifierIconPage = new MagnifierIconPage(Driver);
            _aboutPage = new AboutPage(Driver);
            _insightsPage = new InsightsPage(Driver);
            _servicesSectionPage = new ServicesSectionPage(Driver);
        }

        public void Dispose()
        {
            LoggerHelper.LogInformation("Disposing TestBase and quitting WebDriver.");
            WebDriverManager.Instance().QuitDriver();

            LoggerHelper.CloseAndFlush();
            LoggerHelper.LogInformation("WebDriver quit and resources cleaned up.");
        }
    }
}