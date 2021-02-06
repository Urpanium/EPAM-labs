using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Serilog;
using T4.DataLayer.Models;
using T4.DataLayer.Models.CSV;

namespace T4.BusinessLogicLayer
{
    public class FileHandler
    {
        public delegate void FileSaleHandler(IEnumerable<Sale> sales);

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
                        return sales;
                    }
                }
            }
            catch (ArgumentException e)
            {
                Log.Error($"Invalid directory path: {e.Message}");
            }

            return null;
        }

        public void OnDirectoryContentChanged(object sender, FileSystemEventArgs eventArgs)
        {
            string fileName = eventArgs.Name;
            string path = eventArgs.FullPath;
            string managerSurname = GetManagerLastNameFromFileName(fileName);
            // csv sales
            IEnumerable<LocalSale> localSales = ParseFile(path);
            ICollection<Sale> dbSales = new List<Sale>();
            foreach (var localSale in localSales)
            {
                Sale sale = MakeDbSale(localSale, managerSurname);
                dbSales.Add(sale);
            }
            // pass sales to database manager
            
            OnSalesReadyEvent.Invoke(dbSales);
        }

        public Sale MakeDbSale(LocalSale localSale, string managerLastName)
        {
            Sale sale = new Sale();

            sale.DateTime = localSale.DateTime;

            sale.Client = new Client(localSale.ClientFirstName, localSale.ClientLastName);
            sale.Manager = new Manager(managerLastName);
            return sale;
        }
        
        
        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }
        
        public string GetManagerLastNameFromFileName(string fileName)
        {
            return fileName.Split('_')[0];
        }

        public DateTime GetDateTimeFromFileName(string fileName) //why?
        {
            return DateTime.ParseExact(fileName.Split('_')[1], "ddMMyyyy", CultureInfo.InvariantCulture);
        }
    }
}