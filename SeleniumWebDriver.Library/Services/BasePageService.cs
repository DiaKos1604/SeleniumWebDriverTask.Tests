﻿using SeleniumWebDriverTask.Business.Pages;

namespace SeleniumWebDriverTask.Business.Services
{
    public class BasePageService<T> where T : BasePage
    {
        private readonly T _page;

        public BasePageService(T page)
        {
            _page = page ?? throw new ArgumentNullException(nameof(page));
        }

        public void EnsurePageIsLoaded()
        {
            _page.WaitForPageLoad();
        }
    }
}