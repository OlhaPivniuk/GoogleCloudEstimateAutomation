using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using GoogleCloudEstimateAutomation.Extensions;

namespace GoogleCloudEstimateAutomation.Pages
{
    public class ComputeEnginePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private const string DropdownXPath = "//*[text()='{0}']/ancestor::div[@role = 'combobox']";
        private const string ProvisioningModelXPath = "//input[@value = 'regular']/parent::div";
        private const string AddGPUsButtonXPath = "//button[@aria-label = 'Add GPUs' and @role = 'switch']";
        private const string ShareLinkXPath = "//*[text()='Share']/ancestor::button";
        private const string TotalCostXPath = "//*[text()='Estimated cost']/parent::div/descendant::label";
        private const string EstimateDialogXPath = "//*[@role = 'dialog' and @aria-label = 'Share Estimate Dialog']";
        private const string ComputeEngineHeaderXPath = "//h1[text() = 'Compute Engine']";
        private const string ShareEstimateHeaderXPath = "//h3[text() = 'Share Estimate']";

        public ComputeEnginePage(IWebDriver webDriver)
        {
            _driver = webDriver;
            _wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
        }

        private IWebElement NumberOfInstancesInput => _driver.FindElement(By.CssSelector("input#c13[type = 'number']"));
        private IWebElement SoftwareDropDown => _driver.FindElement(By.XPath(string.Format(DropdownXPath, "Operating System / Software")));
        private IWebElement ProvisioningModelButton => _driver.FindElement(By.XPath(ProvisioningModelXPath));
        private IWebElement MachineFamilyDropDown => _driver.FindElement(By.XPath(string.Format(DropdownXPath, "Machine Family")));
        private IWebElement SeriesDropDown => _driver.FindElement(By.XPath(string.Format(DropdownXPath, "Series")));
        private IWebElement MachineTypeDropDown => _driver.FindElement(By.XPath(string.Format(DropdownXPath, "Machine type")));
        private IWebElement AddGPUsButton => _driver.FindElement(By.XPath(AddGPUsButtonXPath));
        private IWebElement GPUModelDropDown => _driver.FindElement(By.XPath(string.Format(DropdownXPath, "GPU Model")));
        private IWebElement GpuNumberDropDown => _driver.FindElement(By.XPath(string.Format(DropdownXPath, "Number of GPUs")));
        private IWebElement LocalSsdDropDown => _driver.FindElement(By.XPath(string.Format(DropdownXPath, "Local SSD")));
        private IWebElement RegionDropDown => _driver.FindElement(By.XPath(string.Format(DropdownXPath, "Region")));
        private IWebElement ShareLink => _driver.FindElement(By.XPath(ShareLinkXPath));
        private IWebElement TotalCost => _driver.FindElement(By.XPath(TotalCostXPath));
        private IWebElement EstimateDialog => _driver.FindElement(By.XPath(EstimateDialogXPath));
        private IWebElement OpenEstimateCost => EstimateDialog.FindElement(By.LinkText("Open estimate summary"));
        private IWebElement ComputeEngineHeader => _driver.FindElement(By.XPath(ComputeEngineHeaderXPath));
        private IWebElement ShareEstimateHeader => _driver.FindElement(By.XPath(ShareEstimateHeaderXPath));

        public void EnterNumberOfInstances(string number)
        {
            NumberOfInstancesInput.Clear();
            NumberOfInstancesInput.SendKeys(number);
        }

        public void SelectSoftware(string software) => _driver.SetDropDownValue(SoftwareDropDown, software);

        public void SelectProvisioningModel() => ProvisioningModelButton.Click();

        public void SelectMachineFamily(string machineFamily) => _driver.SetDropDownValue(MachineFamilyDropDown, machineFamily);

        public void SelectSeries(string series) => _driver.SetDropDownValue(SeriesDropDown, series);

        public void SelectMachineType(string machineType) => _driver.SetDropDownValue(MachineTypeDropDown, machineType);

        public void AddGPU() => _driver.JsClick(AddGPUsButton);

        public void SelectGpuModel(string gpuType) => _driver.SetDropDownValue(GPUModelDropDown, gpuType);

        public void SelectGpuNumber(string gpuNumber) => _driver.SetDropDownValue(GpuNumberDropDown, gpuNumber);

        public void SelectLocalSSD(string localSSD) => _driver.SetDropDownValue(LocalSsdDropDown, localSSD);

        public void SelectRegion(string region) => _driver.SetDropDownValue(RegionDropDown, region);

        public void ClickShare()
        {
            var js = (IJavaScriptExecutor) _driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", ShareLink);
            js.ExecuteScript("arguments[0].click();", ShareLink);
        }


        public void OpenEstimateSummary() => OpenEstimateCost.Click();

        public void WaitUntilCostIsUpdated()
        {
            var cost = TotalCost.Text;
            _wait.Until(_ => !string.Equals(TotalCost.Text, cost, StringComparison.InvariantCulture));         
        }

        public void WaitForPageToLoad() => _wait.Until(_ => ComputeEngineHeader.Displayed);

        public void WaitForShareFrame() => _wait.Until(_ => ShareEstimateHeader.Displayed);
    }
}
