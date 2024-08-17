using GoogleCloudEstimateAutomation.Drivers;
using GoogleCloudEstimateAutomation.Models;
using GoogleCloudEstimateAutomation.Pages; 
using GoogleCloudEstimateAutomation.Utilities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;

namespace GoogleCloudEstimateAutomation.Tests
{
    [TestFixture]
    public sealed class GoogleCloudCalculatorTests : IDisposable
    {
        private readonly IWebDriver webDriver;
        private readonly ComputeInstanceConfig generalPurposeConfiguration;
        private readonly ScreenshotUtility screenshotHelper;
        private bool testFailed = true;
        private readonly IConfigurationRoot configuration;

        public GoogleCloudCalculatorTests()
        {
            var env = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT");
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .Build();

            webDriver = Drivers.WebDriverManager.GetDriver(configuration["browser"]!);
            webDriver.Url = configuration["baseURL"];

            generalPurposeConfiguration = TestDataUtility.Get(configuration["testData"] ?? "FreeGeneralPurposeConfig.json");

            screenshotHelper = new ScreenshotUtility(webDriver);
        }

        public void Dispose()
        {
            if (testFailed)
            {
                string filePath = configuration["screenshotsPath"];
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                Directory.CreateDirectory(filePath);
                screenshotHelper.TakeScreenshot(Path.Combine(filePath, $"failure_{timestamp}.png"));
            }

            Drivers.WebDriverManager.CloseDriver();
        }

        private void ConfigureProducts()
        {
            var pricingCalculatorPage = new PricingCalculatorPage(webDriver);

            pricingCalculatorPage.ClickAdd();
            pricingCalculatorPage.WaitForAddToEstimateFrame();
            pricingCalculatorPage.ClickComputeEngine();

            var computeEnginePage = new ComputeEnginePage(webDriver);
            computeEnginePage.WaitForPageLoaded();

            computeEnginePage.EnterNumberOfInstances(generalPurposeConfiguration.NumberOfInstances);
            computeEnginePage.SetSoftware(generalPurposeConfiguration.Software);

            computeEnginePage.SetProvisioningModel();
            computeEnginePage.SetMachineFamily(generalPurposeConfiguration.MachineFamily);
            computeEnginePage.SetSeries(generalPurposeConfiguration.Series);
            computeEnginePage.SetMachineType(generalPurposeConfiguration.MashineType);

            computeEnginePage.AddGPU();
            computeEnginePage.WaitUntilCostUpdated();

            computeEnginePage.SetGpuModel(generalPurposeConfiguration.GpuType);
            computeEnginePage.SetGpuNumber(generalPurposeConfiguration.GpusNumber);

            computeEnginePage.SetLocalSSD(generalPurposeConfiguration.LocalSSD);

            computeEnginePage.SetRegion(generalPurposeConfiguration.Region);
            computeEnginePage.WaitUntilCostUpdated();

            computeEnginePage.ShareClick();
            computeEnginePage.WaitForShareFrame();
            computeEnginePage.OpenEstimatedCost();
        }

        [Test]
        public void CostEstimateTableTest()
        {
            ConfigureProducts();

            webDriver.SwitchTo().Window(webDriver.WindowHandles[1]);
            var summaryPage = new CostEstimateSummaryPage(webDriver);
            summaryPage.WaitForPageLoaded();

            Assert.AreEqual(generalPurposeConfiguration.NumberOfInstances, summaryPage.GetItemValue("Number of Instances"));
            Assert.AreEqual(generalPurposeConfiguration.Software, summaryPage.GetItemValue("Operating System / Software"));
            Assert.That(summaryPage.GetItemValue("Machine type"), Contains.Substring(generalPurposeConfiguration.MashineType));
            Assert.AreEqual(generalPurposeConfiguration.GpuType, summaryPage.GetItemValue("GPU Model"));
            Assert.AreEqual(generalPurposeConfiguration.GpusNumber, summaryPage.GetItemValue("Number of GPUs"));
            Assert.AreEqual(generalPurposeConfiguration.LocalSSD, summaryPage.GetItemValue("Local SSD"));
            Assert.AreEqual(generalPurposeConfiguration.Region, summaryPage.GetItemValue("Region"));

            testFailed = false;
        }
    }
}
