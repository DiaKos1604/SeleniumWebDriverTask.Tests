using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class WebDriverManager
{ 
    public static IWebDriver GetWebDriver(bool headless = true)
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");

        if (headless)
        {
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920x1080");
        }

        return new ChromeDriver(options);
    }
}