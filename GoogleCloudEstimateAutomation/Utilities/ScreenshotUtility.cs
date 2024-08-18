using OpenQA.Selenium;

namespace GoogleCloudEstimateAutomation.Utilities
{
    public class ScreenshotUtility
    {
        private readonly IWebDriver _driver;

        public ScreenshotUtility(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void TakeScreenshot(string filePath)
        {
            Screenshot screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            screenshot.SaveAsFile(filePath);
        }
    }
}
