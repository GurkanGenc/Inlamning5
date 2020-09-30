using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApp.Services
{
    public static class DeviceService
    {
        private static DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=Inlamning3-iothub.azure-devices.net;DeviceId=ConsoleApp;SharedAccessKey=ptX988yL+vj8vS/Z4KQzewzdmmF4IJGyD2SLXCjCtgY=", TransportType.Mqtt);
        private static int telemetryInterval = 5; // It is second as unit.
        private static Random rnd = new Random();

        public static Task<MethodResponse> SetTelemetryInterval(MethodRequest request, object userContext)
        {
            var payload = Encoding.UTF8.GetString(request.Data).Replace("\"", "");

            // TryParse means we test if the value is an int
            if (Int32.TryParse(payload, out telemetryInterval))
            {
                Console.WriteLine($"Interval set to: {telemetryInterval} seconds.");

                // We can write direct as a json. { "result": "Executed direct method: SetTelemetryInterval" }
                string json = "{\"result\": \"Executed direct method: " + request.Name + "\"}";

                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(json), 200));
            }
            else
            {
                string json = "{\"result\": \"Method not implemented\"}";

                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(json), 501));
            }
        }

        //public static async Task SendMessageAsync()
        //{
        //    while (true)
        //    {
        //        double temp = 10 + rnd.NextDouble() * 30;
        //        double hum = 40 + rnd.NextDouble() * 40;

        //        var data = new // "payload" is a variable that has a type as an object!
        //        {
        //            temperature = temp,
        //            humidity = hum
        //        };

        //        var json = JsonConvert.SerializeObject(data);
        //        var payload = new Message(Encoding.UTF8.GetBytes(json));

        //        await deviceClient.SendEventAsync(payload);
        //        Console.WriteLine($"Message sent: {json}");

        //        await Task.Delay(telemetryInterval * 1000);
        //    }
        //}
    }
}
