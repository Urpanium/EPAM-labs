using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using Serilog;
using T4.BusinessLogicLayer;
using T4.DataAccessLayer;

namespace T4.PresentationLayer.Service
{
    public partial class MarketService: ServiceBase
    {
        private Worker _worker;
        private readonly string _directoryPath;
        public MarketService(string directoryPath)
        {
            InitializeComponent();
            CanStop = true;
            ServiceName = "MarketService";
            CanStop = true;
            CanPauseAndContinue = false;
            AutoLog = false;
            
            _directoryPath = directoryPath;
        }
        
        protected override void OnStart(string[] args)
        {
            try
            {
                Log.Information("Starting service...");
                FileHandler fileHandler = new FileHandler();
                FileWatcher fileWatcher = new FileWatcher(_directoryPath, fileHandler);
                DatabaseManager databaseManager = new DatabaseManager();
                
                _worker = new Worker(databaseManager, fileHandler, fileWatcher);
                _worker.Start();
                Log.Information("Started");
            }
            catch (Exception e)
            {
                Log.Error($"Some shit happened: {e}");
            }
        }

        protected override void OnStop()
        {
            try
            {
                _worker.Stop();
                _worker.Dispose();
                Thread.Sleep(3000);
                Log.Information("Stopped service successfully");
            }
            catch (Exception e)
            {
                Log.Error($"Error while stopping service: {e}");
            }
        }
    }
}