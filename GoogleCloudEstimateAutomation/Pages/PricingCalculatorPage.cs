using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace GoogleCloudEstimateAutomation.Pages
{
    public class PricingCalculatorPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private const string AddToEstimateButtonXPath = "//span[text()='Add to estimate']/ancestor::button";
        private const string ComputeEngineButtonXPath = "//*[text()='Compute Engine']/parent::div";

        public PricingCalculatorPage(IWebDriver webDriver)
        {
            _driver = webDriver;
            _wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
        }

        private IWebElement AddToEstimateButton => _driver.FindElement(By.XPath(AddToEstimateButtonXPath));
        private IWebElement ComputeEngineButton => _driver.FindElement(By.XPath(ComputeEngineButtonXPath));


        public void ClickAddToEstimate() => AddToEstimateButton.Click();

        public void SelectComputeEngine() => ComputeEngineButton.Click();

        public void WaitForEstimateFrameToLoad() => _wait.Until(_ => ComputeEngineButton.Displayed);

    }
}
