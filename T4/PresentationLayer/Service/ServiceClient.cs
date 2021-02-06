using System.ServiceProcess;

namespace T4.PresentationLayer.Service
{
    public class ServiceClient
    {
        public ServiceClient()
        {
            ServiceBase.Run(new MarketService());
        }
    }
}