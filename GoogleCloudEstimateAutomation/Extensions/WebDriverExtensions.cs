using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace GoogleCloudEstimateAutomation.Extensions
{
    public static class WebDriverExtensions
    {
        public static void SetDropDownValue(this IWebDriver driver, IWebElement dropdown, string value)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            dropdown.Click();
            wait.Until(_ => { return dropdown.GetAttribute("aria-expanded") == "true"; });

            IWebElement option = driver.FindElement(By.XPath($"//span[text() = '{value}']/ancestor::li[@role = 'option']"));

            driver.JsClick(option);
            wait.Until(_ => { return dropdown.GetAttribute("aria-expanded") == "false"; });
        }

        public static void JsClick(this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", element);
        }

    }

}
