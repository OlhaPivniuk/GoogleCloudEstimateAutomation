﻿using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using GoogleCloudEstimateAutomation.Extensions;

namespace GoogleCloudEstimateAutomation.Pages
{
    public class ComputeEnginePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        const string DropdownXpath = "//*[text()='{0}']/ancestor::div[@role = 'combobox']";

        public ComputeEnginePage(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
        }

        private IWebElement NumberOfInstancesInput => driver.FindElement(By.CssSelector("input#c13[type = 'number']"));
        private IWebElement SoftwareDropDown => driver.FindElement(By.XPath(string.Format(DropdownXpath, "Operating System / Software")));
        private IWebElement ProvisioningModelButton => driver.FindElement(By.XPath("//input[@value = 'regular']/parent::div"));
        private IWebElement MachineFamilyDropDown => driver.FindElement(By.XPath(string.Format(DropdownXpath, "Machine Family")));
        private IWebElement SeriesDropDown => driver.FindElement(By.XPath(string.Format(DropdownXpath, "Series")));
        private IWebElement MachineTypeDropDown => driver.FindElement(By.XPath(string.Format(DropdownXpath, "Machine type")));
        private IWebElement AddGPUsButton => driver.FindElement(By.XPath("//button[@aria-label = 'Add GPUs' and @role = 'switch']"));
        private IWebElement GPUModelDropDown => driver.FindElement(By.XPath(string.Format(DropdownXpath, "GPU Model")));
        private IWebElement GpuNumberDropDown => driver.FindElement(By.XPath(string.Format(DropdownXpath, "Number of GPUs")));
        private IWebElement LocalSsdDropDown => driver.FindElement(By.XPath(string.Format(DropdownXpath, "Local SSD")));
        private IWebElement RegionDropDown => driver.FindElement(By.XPath(string.Format(DropdownXpath, "Region")));
        private IWebElement ShareLink => driver.FindElement(By.XPath("//*[text()='Share']/ancestor::button"));
        private IWebElement TotalCost => driver.FindElement(By.XPath("//*[text()='Estimated cost']/parent::div/descendant::label"));
        private IWebElement EstimateDialog => driver.FindElement(By.XPath("//*[@role = 'dialog' and @aria-label = 'Share Estimate Dialog']"));
        private IWebElement OpenEstimateCost => EstimateDialog.FindElement(By.LinkText("Open estimate summary"));
        private IWebElement ComputeEngineHeader => driver.FindElement(By.XPath("//h1[text() = 'Compute Engine']"));
        private IWebElement ShareEstimateHeader => driver.FindElement(By.XPath("//h3[text() = 'Share Estimate']"));

        public void EnterNumberOfInstances(string number)
        {
            NumberOfInstancesInput.Clear();
            NumberOfInstancesInput.SendKeys(number);
        }

        public void SetSoftware(string software) => driver.SetDropDownValue(SoftwareDropDown, software);

        public void SetProvisioningModel() => ProvisioningModelButton.Click();

        public void SetMachineFamily(string machineFamily) => driver.SetDropDownValue(MachineFamilyDropDown, machineFamily);

        public void SetSeries(string series) => driver.SetDropDownValue(SeriesDropDown, series);

        public void SetMachineType(string machineType) => driver.SetDropDownValue(MachineTypeDropDown, machineType);

        public void AddGPU() => driver.JsClick(AddGPUsButton);

        public void SetGpuModel(string gpuType) => driver.SetDropDownValue(GPUModelDropDown, gpuType);

        public void SetGpuNumber(string gpuNumber) => driver.SetDropDownValue(GpuNumberDropDown, gpuNumber);

        public void SetLocalSSD(string localSSD) => driver.SetDropDownValue(LocalSsdDropDown, localSSD);

        public void SetRegion(string region) => driver.SetDropDownValue(RegionDropDown, region);

        public void ShareClick()
        {
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", ShareLink);
            js.ExecuteScript("arguments[0].click();", ShareLink);
        }


        public void OpenEstimatedCost() => OpenEstimateCost.Click();

        public void WaitUntilCostUpdated()
        {
            var cost = TotalCost.Text;
            wait.Until(_ => !string.Equals(TotalCost.Text, cost, StringComparison.InvariantCulture));         
        }

        public void WaitForPageLoaded() => wait.Until(_ => ComputeEngineHeader.Displayed);

        public void WaitForShareFrame() => wait.Until(_ => ShareEstimateHeader.Displayed);

    }
}
