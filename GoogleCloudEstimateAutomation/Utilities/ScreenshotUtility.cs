using OpenQA.Selenium;

namespace GoogleCloudEstimateAutomation.Utilities
{
    public class ScreenshotUtility
    {
        private readonly IWebDriver driver;

        public ScreenshotUtility(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void TakeScreenshot(string filePath)
        {
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(filePath);
        }
    }
}
