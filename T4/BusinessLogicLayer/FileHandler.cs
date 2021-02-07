using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Serilog;
using T4.DataLayer.Models;
using T4.DataLayer.Models.CSV;

namespace T4.BusinessLogicLayer
{
    public class FileHandler
    {
        public delegate void FileSaleHandler(IEnumerable<Sale> sales, Manager manager);

        public event FileSaleHandler OnSalesReadyEvent;

        private IEnumerable<LocalSale> ParseFile(string path)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        List<LocalSale> sales = csvReader.GetRecords<LocalSale>().ToList();
                        if (sales.Count == 0)
                        {
                            Log.Error($"No records where read from file {path}");
                        }
                        /*StringBuilder sb = new StringBuilder();
                        foreach (var sale in sales)
                        {
                            sb.Append(sale + "\n");
                        }
                        Log.Information($"Ghost local sales: {sb}");*/
                        return sales;
                    }
                }
            }
            catch (ArgumentException e)
            {
                Log.Error($"Invalid directory path: {e.Message}");
            }
            catch (Exception e)
            {
                Log.Error($"Error while parsing {new FileInfo(path).Name} ({path}): {e}");
            }

            return null;
        }

        public void OnDirectoryContentChanged(object sender, FileSystemEventArgs eventArgs)
        {
            Log.Information("Directory content changed. Processing...");
            Task.Factory.StartNew(() =>
            {
                try
                {
                    string fileName = eventArgs.Name;
                    string path = eventArgs.FullPath;
                    string managerLastName = GetManagerLastNameFromFileName(fileName);

                    FileInfo fileInfo = new FileInfo(path);

                    bool printed = false;
                    while (IsFileBusy(fileInfo))
                    {
                        if (!printed)
                        {
                            Log.Information($"{fileName} is busy. Waiting...");
                            printed = true;
                        }
                        // nice weather
                    }

                    if (printed)
                    {
                        Log.Information($"{fileName} is not busy anymore. Continuing...");
                    }

                    // csv sales
                    IEnumerable<LocalSale> localSales = ParseFile(path);
                    // db sales
                    ICollection<Sale> dbSales = new List<Sale>();
                    Manager manager = new Manager(managerLastName);
                    foreach (var localSale in localSales)
                    {
                        Sale sale = MakeDbSale(localSale, manager);
                        dbSales.Add(sale);
                    }

                    // pass sales and manager to database manager
                    OnSalesReadyEvent?.Invoke(dbSales, manager);
                }
                catch (Exception e)
                {
                    Log.Error($"Error while processing {eventArgs.Name} ({eventArgs.FullPath}): {e}");
                }
            });
        }

        private static Sale MakeDbSale(LocalSale localSale, Manager manager)
        {
            Sale sale = new Sale
            {
                DateTime = DateTime.ParseExact(localSale.DateTime, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Client = new Client(localSale.ClientFirstName, localSale.ClientLastName),
                Manager = manager,
                Product = new Product(localSale.ProductName, localSale.ProductPrice)
            };
            return sale;
        }

        private static bool IsFileBusy(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }

        private static string GetManagerLastNameFromFileName(string fileName)
        {
            return fileName.Split('_')[0];
        }

    }
}