using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using Serilog;

namespace T4.PresentationLayer.Service
{
    public class MarketService : ServiceBase
    {
        
        public MarketService()
        {
            ServiceName = "MarketService";
            CanStop = true;
            CanPauseAndContinue = false;
            AutoLog = false;
        }
        
        protected override void OnStart(string[] args)
        {
            Log.Information("Starting service...");
            

        }

        protected override void OnStop()
        {
            /*base.OnStop();

            if (!_worker.IsBusy) return;

            EventLog.WriteEntry("Service: Worker cancelling ...", EventLogEntryType.Information);
            _worker.CancelAsync();
            Thread.Sleep(3000);

            EventLog.WriteEntry("Service: Worker dismissed ...", EventLogEntryType.Information);
            _worker = null;*/
        }
    }
}