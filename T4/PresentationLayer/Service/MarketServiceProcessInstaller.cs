using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace T4.PresentationLayer.Service
{
    [RunInstaller(true)]
    public partial class MarketServiceProcessInstaller : Installer
    {

        public MarketServiceProcessInstaller()
        {
            var serviceProcessInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem,
                Password = null,
                Username = null
            };

            var serviceInstaller = new ServiceInstaller
            {
                DisplayName = "MarketService",
                ServiceName = "MarketService",
                StartType = ServiceStartMode.Manual
            };

            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}