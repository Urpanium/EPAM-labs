using System.Configuration;
using Serilog;
using T4.PresentationLayer;
using T4.PresentationLayer.Service;

namespace T4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string directoryPath = ConfigurationManager.AppSettings["directoryPath"];
            string logFileName = ConfigurationManager.AppSettings["logFileName"];

            string logPath = directoryPath + "\\" + logFileName;
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(logPath)
                .WriteTo.Console()
                .CreateLogger();
            Log.Information("Creating client");
            ConsoleClient client = new ConsoleClient(directoryPath);
            //ServiceClient client = new ServiceClient(directoryPath);
        }
    }
}