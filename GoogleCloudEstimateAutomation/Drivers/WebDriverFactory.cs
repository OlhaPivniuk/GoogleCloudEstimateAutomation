using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace GoogleCloudEstimateAutomation.Drivers
{
    public class WebDriverFactory
    {
        private static IWebDriver? _driver;

        private WebDriverFactory() { }

        public static IWebDriver GetDriver(string browserType)
        {
            if (_driver == null)
            {
                switch (browserType)
                {
                    case "firefox":
                        {
                            new DriverManager().SetUpDriver(new FirefoxConfig());
                            _driver = new FirefoxDriver();
                            break;
                        }
                    default:
                        {
                            new DriverManager().SetUpDriver(new ChromeConfig());
                            _driver = new ChromeDriver();
                            break;
                        }
                }

                _driver.Manage().Window.Maximize();
            }

            return _driver;
        }

        public static void QuitDriver()
        {
            _driver?.Quit();
            _driver = null;
        }
    }

}
