using GoogleCloudEstimateAutomation.Models;
using System.Text.Json;

namespace GoogleCloudEstimateAutomation.Utilities
{
    public static class TestDataUtility
    {
        public static ComputeInstanceConfig Get(string fileName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", fileName);
            string jsonString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<ComputeInstanceConfig>(jsonString) ?? new();
        }
    }
}
