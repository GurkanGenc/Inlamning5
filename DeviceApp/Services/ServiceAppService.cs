using Microsoft.Azure.Devices;
using System;
using System.Threading.Tasks;

namespace DeviceApp.Services
{
    public class ServiceAppService
    {
        private ServiceClient serviceClient;

        public ServiceAppService(string connectionstring)
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionstring);
        }

        // deviceId here is "ConsoleApp" and methodName is "SetTelemetryInterval".
        // This method is same thing with "Call Method on Device" tab in the Device Explorer app.
        public async Task<CloudToDeviceMethodResult> InvokeMethod(string deviceName, string methodName, string payload)
        {
            var methodInvocation = new CloudToDeviceMethod(methodName);
            methodInvocation.SetPayloadJson(payload);

            var response = await serviceClient.InvokeDeviceMethodAsync(deviceName, methodInvocation);
            Console.WriteLine($"Response Status: {response.Status}");
            Console.WriteLine(response.GetPayloadAsJson());
            return response;
        }
    }
}
