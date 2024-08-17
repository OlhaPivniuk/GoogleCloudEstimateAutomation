using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace GoogleCloudEstimateAutomation.Drivers
{
    public class WebDriverManager
    {
        private static IWebDriver? driver;

        private WebDriverManager() { }

        public static IWebDriver GetDriver(string browser)
        {
            if (driver == null)
            {
                switch (browser)
                {
                    case "firefox":
                        {
                            new DriverManager().SetUpDriver(new FirefoxConfig());
                            driver = new FirefoxDriver();
                            break;
                        }
                    default:
                        {
                            new DriverManager().SetUpDriver(new ChromeConfig());
                            driver = new ChromeDriver();
                            break;
                        }
                }

                driver.Manage().Window.Maximize();
            }

            return driver;
        }

        public static void CloseDriver()
        {
            driver?.Quit();
        }
    }

}
