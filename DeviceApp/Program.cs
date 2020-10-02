using DeviceApp.Services;

namespace DeviceApp  // This is an IoT device
{
    class Program
    {
        static void Main(string[] args)
        {
            DeviceService service = new DeviceService();
            service.SendMessageAsync().Wait();
        }

    }
}