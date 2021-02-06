using System;
using System.IO;
using Serilog;
using T4.BusinessLogicLayer;

namespace T4.PresentationLayer
{
    public class ConsoleClient
    {
        public ConsoleClient(string directoryPath)
        {
            try
            {
                Log.Information("Starting...");
                FileHandler fileHandler = new FileHandler();
                FileWatcher fileWatcher = new FileWatcher(directoryPath, fileHandler);
                DatabaseManager databaseManager = new DatabaseManager();

                using (Worker worker = new Worker(databaseManager, fileHandler, fileWatcher))
                {
                    worker.Start();
                    Log.Information("Started. Press any key to stop");
                    Console.ReadKey();
                    worker.Stop();
                    Log.CloseAndFlush();
                }
            }
            catch (Exception e)
            {
                Log.Error($"Some shit happened: {e}");
            }
        }
    }
}