using GoogleCloudEstimateAutomation.Models;
using GoogleCloudEstimateAutomation.Utilities;

namespace GoogleCloudEstimateAutomation.Services
{
    public static class ComputeInstanceConfigFactory
    {
        public static ComputeInstanceConfig CreateGeneralPurposeConfiguration()
        {
            return TestDataUtility.Get("Data.json");
        }
    }
}
