using Microsoft.Azure.Devices;
using System;
using System.Threading.Tasks;

namespace ServiceApp
{
    class Program
    {
        private static ServiceClient serviceClient = ServiceClient.CreateFromConnectionString("HostName=Inlamning3-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=t6ZRawYie7F8lN3iQGm3jnmPac6NJRAWphc4+JRLAtg=");

        static void Main(string[] args)
        {
            Task.Delay(5000).Wait(); // We don't need this. It is just to see what's going on.

            InvokeMethod("ConsoleApp", "SetTelemetryInterval", "10").GetAwaiter();

            Console.ReadKey();
        }

        // deviceId here is "ConsoleApp" and methodName is "SetTelemetryInterval".
        // This method is same thing with "Call Method on Device" tab in the Device Explorer app.
        static async Task InvokeMethod(string deviceId, string methodName, string payload)
        {
            var methodInvocation = new CloudToDeviceMethod(methodName) { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.SetPayloadJson(payload);

            var response = await serviceClient.InvokeDeviceMethodAsync(deviceId, methodInvocation);
            Console.WriteLine($"Response Status: {response.Status}");
            Console.WriteLine(response.GetPayloadAsJson());
        }
    }
}
