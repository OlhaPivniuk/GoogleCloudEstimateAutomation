﻿using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace GoogleCloudEstimateAutomation.Extensions
{
    public static class WebDriverExtensions
    {
        private const string OptionXPath = "//span[text() = '{0}']/ancestor::li[@role = 'option']";

        public static void SetDropDownValue(this IWebDriver driver, IWebElement dropdown, string value)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            dropdown.Click();
            wait.Until(_ => { return dropdown.GetAttribute("aria-expanded") == "true"; });

            IWebElement option = driver.FindElement(By.XPath(string.Format(OptionXPath, value)));

            driver.JsClick(option);
            wait.Until(_ => { return dropdown.GetAttribute("aria-expanded") == "false"; });
        }

        public static void JsClick(this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", element);
        }

    }

}
