using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace GoogleCloudEstimateAutomation.Pages
{
    public class CostEstimateSummaryPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private const string CostItemValueXPath = "//span[text() = '{0}']/following-sibling::span";
        private const string PageLoadedXPath = "//h4[text() = 'Cost Estimate Summary']";

        public CostEstimateSummaryPage(IWebDriver webDriver)
        {
            _driver = webDriver;
            _wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
        }

        public void Navigate(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public string GetItemValue(string costItem)
        {
            var element = _driver.FindElement(By.XPath(string.Format(CostItemValueXPath, costItem)));
            return element?.Text ?? string.Empty;
        }

        public void WaitForPageToLoad()
        {
            _wait.Until(driver => driver.FindElement(By.XPath(PageLoadedXPath)).Displayed);
        }
    }
}
