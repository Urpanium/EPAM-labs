using System.ServiceProcess;

namespace T4.PresentationLayer.Service
{
    public class ServiceClient
    {
        public ServiceClient(string directoryPath)
        {
            ServiceBase.Run(new MarketService(directoryPath));
        }
    }
}