using OpenQA.Selenium;
using Serilog;

public class NavigationService
{
    private readonly IWebDriver _driver;
    private readonly ILogger _logger;

    public NavigationService(IWebDriver driver, ILogger logger)
    {
        _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void GoToPage(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("URL cannot be null or empty.", nameof(url));
        }
        _logger.Information($"Navigating to {url}");
        _driver.Navigate().GoToUrl(url);
    }
}