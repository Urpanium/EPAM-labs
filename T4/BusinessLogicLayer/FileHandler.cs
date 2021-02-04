using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using CsvHelper;
using Serilog;
using T4.DataLayer.Models.CSV;

namespace T4.BusinessLogicLayer
{
    public class FileHandler
    {

        public IEnumerable<Sale> ParseFile(string path)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        List<Sale> sales = csvReader.GetRecords<Sale>().ToList();
                        return sales;
                    }
                }
            }
            catch (Exception e)
            {
                //Log.Error();
            }

            return null;
        }

        public void OnDirectoryContentChanged(object sender, FileSystemEventArgs eventArgs)
        {
            
        }

        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }
        
        public string GetManagerLastNameFromFileName(string fileName)
        {
            return fileName.Split('_')[0];
        }

        public DateTime GetDateTimeFromFileName(string fileName)
        {
            return DateTime.ParseExact(fileName.Split('_')[1], "ddMMyyyy", CultureInfo.InvariantCulture);
        }
    }
}