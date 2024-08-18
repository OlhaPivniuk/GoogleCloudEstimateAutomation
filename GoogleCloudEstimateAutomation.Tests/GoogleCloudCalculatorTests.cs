using GoogleCloudEstimateAutomation.Drivers;
using GoogleCloudEstimateAutomation.Models;
using GoogleCloudEstimateAutomation.Pages;
using GoogleCloudEstimateAutomation.Utilities;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace GoogleCloudEstimateAutomation.Tests
{
    [TestFixture]
    public sealed class GoogleCloudCalculatorTests
    {
        private IWebDriver _driver;
        private ComputeInstanceConfig _instanceConfig;
        private ScreenshotUtility _screenshotUtility;
        private IConfigurationRoot _configuration;
        private CostEstimateSummaryPage _summaryPage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string environment = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "Development";
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            _driver = WebDriverFactory.GetDriver(_configuration["browser"]!);
            _driver.Url = _configuration["baseURL"];

            _instanceConfig = TestDataUtility.Get(_configuration["TestDataFile"] ?? "FreeGeneralPurposeConfig.json");

            _screenshotUtility = new ScreenshotUtility(_driver);

            ConfigureComputeEngine();

            _driver.SwitchTo().Window(_driver.WindowHandles[1]);
            _summaryPage = new CostEstimateSummaryPage(_driver);
            _summaryPage.WaitForPageToLoad();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                string screenshotPath = _configuration["screenshotsPath"];
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                Directory.CreateDirectory(screenshotPath);
                _screenshotUtility.TakeScreenshot(Path.Combine(screenshotPath, $"failure_{timestamp}.png"));
            }

            WebDriverFactory.QuitDriver();
        }

        private void ConfigureComputeEngine()
        {
            var pricingCalculatorPage = new PricingCalculatorPage(_driver);

            pricingCalculatorPage.ClickAddToEstimate();
            pricingCalculatorPage.WaitForEstimateFrameToLoad();
            pricingCalculatorPage.SelectComputeEngine();

            var computeEnginePage = new ComputeEnginePage(_driver);
            computeEnginePage.WaitForPageToLoad();

            computeEnginePage.EnterNumberOfInstances(_instanceConfig.NumberOfInstances);
            computeEnginePage.SelectSoftware(_instanceConfig.Software);

            computeEnginePage.SelectProvisioningModel();
            computeEnginePage.SelectMachineFamily(_instanceConfig.MachineFamily);
            computeEnginePage.SelectSeries(_instanceConfig.Series);
            computeEnginePage.SelectMachineType(_instanceConfig.MashineType);

            computeEnginePage.AddGPU();
            computeEnginePage.WaitUntilCostIsUpdated();

            computeEnginePage.SelectGpuModel(_instanceConfig.GpuType);
            computeEnginePage.SelectGpuNumber(_instanceConfig.GpusNumber);

            computeEnginePage.SelectLocalSSD(_instanceConfig.LocalSSD);

            computeEnginePage.SelectRegion(_instanceConfig.Region);
            computeEnginePage.WaitUntilCostIsUpdated();

            computeEnginePage.ClickShare();
            computeEnginePage.WaitForShareFrame();
            computeEnginePage.OpenEstimateSummary();
        }

        [Test]
        public void VerifyNumberOfInstances()
        {
            Assert.That(_summaryPage.GetItemValue("Number of Instances"), Is.EqualTo(_instanceConfig.NumberOfInstances));
        }

        [Test]
        public void VerifySoftware()
        {
            Assert.That(_summaryPage.GetItemValue("Operating System / Software"), Is.EqualTo(_instanceConfig.Software));
        }

        [Test]
        public void VerifyMachineType()
        {
            Assert.That(_summaryPage.GetItemValue("Machine type"), Does.Contain(_instanceConfig.MashineType));
        }

        [Test]
        public void VerifyGpuModel()
        {
            Assert.That(_summaryPage.GetItemValue("GPU Model"), Is.EqualTo(_instanceConfig.GpuType));
        }

        [Test]
        public void VerifyGpuNumber()
        {
            Assert.That(_summaryPage.GetItemValue("Number of GPUs"), Is.EqualTo(_instanceConfig.GpusNumber));
        }

        [Test]
        public void VerifyLocalSSD()
        {
            Assert.That(_summaryPage.GetItemValue("Local SSD"), Is.EqualTo(_instanceConfig.LocalSSD));
        }

        [Test]
        public void VerifyRegion()
        {
            Assert.That(_summaryPage.GetItemValue("Region"), Is.EqualTo(_instanceConfig.Region));
        }
    }
}
