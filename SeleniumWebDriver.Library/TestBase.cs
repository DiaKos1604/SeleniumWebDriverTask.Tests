using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

public class TestBase : IDisposable
{
    protected readonly IWebDriver driver;
    protected readonly WebDriverWait wait;

    public TestBase()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        driver = new ChromeDriver(options);
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public void Dispose()
    {
        driver.Quit();
    }
}