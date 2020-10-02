using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApp.Services
{
    public class DeviceService
    {
        private DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=Inlamning3-iothub.azure-devices.net;DeviceId=ConsoleApp;SharedAccessKey=ptX988yL+vj8vS/Z4KQzewzdmmF4IJGyD2SLXCjCtgY=", TransportType.Mqtt);
        private int telemetryInterval = 5;
        private Random random = new Random();

        public DeviceService()
        {
            // Registering the method.
            deviceClient.SetMethodHandlerAsync("SetTelemetryInterval", SetTelemetryInterval, null);
        }

        public Task<MethodResponse> SetTelemetryInterval(MethodRequest request, object userContext)
        {
            var payload = Encoding.UTF8.GetString(request.Data).Replace("\"", "");

            // TryParse means we test if the value is an int
            if (Int32.TryParse(payload, out telemetryInterval))
            {
                    Console.WriteLine($"Interval set to: {telemetryInterval} seconds.");
                    string json = "{\"result\": \"Executed direct method: " + request.Name + "\"}";
                    return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(json), 200));
            }
            else
            {
                string json = "{\"result\": \"Method not implemented\"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(json), 501));
            }
        }

        public async Task SendMessageAsync()
        {
            while (true)
            {
                double temp = 10 + random.NextDouble() * 30;
                double hum = 40 + random.NextDouble() * 40;

                var data = new
                {
                    temperature = temp,
                    humidity = hum
                };

                var json = JsonConvert.SerializeObject(data);
                var payload = new Message(Encoding.UTF8.GetBytes(json)); // "payload" is a variable that has a type of an object!

                await deviceClient.SendEventAsync(payload);
                Console.WriteLine($"Message sent: {json}");

                await Task.Delay(telemetryInterval * 1000);
            }
        }
    }
}
