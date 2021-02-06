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

        public IEnumerable<LocalSale> ParseFile(string path)
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
                Log.Error($"Unknown error in FileHandler.ParseFile: {e}");
            }

            return null;
        }

        public void OnDirectoryContentChanged(object sender, FileSystemEventArgs eventArgs)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    string fileName = eventArgs.Name;
                    string path = eventArgs.FullPath;
                    string managerLastName = GetManagerLastNameFromFileName(fileName);

                    FileInfo fileInfo = new FileInfo(path);

                    while (IsFileBusy(fileInfo))
                    {
                        // nice weather huh
                    }

                    // csv sales
                    IEnumerable<LocalSale> localSales = ParseFile(path);
                    // db sales
                    ICollection<Sale> dbSales = new List<Sale>();
                    foreach (var localSale in localSales)
                    {
                        Sale sale = MakeDbSale(localSale, managerLastName);
                        dbSales.Add(sale);
                    }

                    // pass sales and manager to database manager
                    Manager manager = new Manager(managerLastName);
                    OnSalesReadyEvent?.Invoke(dbSales, manager);
                }
                catch (Exception e)
                {
                    Log.Error($"Error while processing {eventArgs.Name} ({eventArgs.FullPath}): {e}");
                }
            });
        }

        public Sale MakeDbSale(LocalSale localSale, string managerLastName)
        {
            Sale sale = new Sale();
            sale.DateTime = localSale.DateTime;
            sale.Client = new Client(localSale.ClientFirstName, localSale.ClientLastName);
            sale.Manager = new Manager(managerLastName);
            return sale;
        }

        private bool IsFileBusy(FileInfo file)
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


        private string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        private string GetManagerLastNameFromFileName(string fileName)
        {
            return fileName.Split('_')[0];
        }

        private DateTime GetDateTimeFromFileName(string fileName) //why?
        {
            return DateTime.ParseExact(fileName.Split('_')[1], "ddMMyyyy", CultureInfo.InvariantCulture);
        }
    }
}