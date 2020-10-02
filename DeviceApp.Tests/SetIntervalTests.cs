using Xunit;
using DeviceApp.Services;

namespace DeviceApp.Tests
{
    public class SetIntervalTests
    {
        [Theory]
        [InlineData("ConsoleApp", "SetTelemetryInterval", "5", "200")] // accepted
        [InlineData("ConsoleApp", "GetInterval", "5", "501")] // not implemented
        
        public void SetInterval_ShouldSetTelemetryInterval(string deviceName, string methodName, string payload, string expected)
        {
            var connectionString = "HostName=Inlamning3-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=t6ZRawYie7F8lN3iQGm3jnmPac6NJRAWphc4+JRLAtg=";
            ServiceAppService service = new ServiceAppService(connectionString);
            var response = service.InvokeMethod(deviceName, methodName, payload);

            Assert.Equal(expected, response.Result.Status.ToString());
        }
    }
}
