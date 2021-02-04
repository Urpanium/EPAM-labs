using System.Configuration;
using System.IO;
using Serilog;

namespace T4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string directoryName = ConfigurationManager.AppSettings["directoryName"];
            string logFileName = ConfigurationManager.AppSettings["logFileName"];

            string directoryPath = Directory.GetCurrentDirectory() + "/" + directoryName;
            string logPath = Directory.GetCurrentDirectory() + "/" + logFileName;
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(logPath)
                .WriteTo.Console()
                .CreateLogger();


            /*using (DatabaseContext db = new DatabaseContext())
            {
                Manager manager = new Manager {LastName = "ururu"};
                db.Managers.Add(manager);
                Sale sale = new Sale {DateTime = DateTime.Today};   
                db.Sales.Add(sale);
                Client client = new Client();
                db.Clients.Add(client);

                db.Products.Add(new Product());


                db.SaveChanges();
            }*/
        }
    }
}