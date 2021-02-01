using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using T4.DataLayer.Models.CSV;


namespace T4.BusinessLogicLayer //DataAccess???
{
    public class CsvParser
    {
        public static List<Sale> Parse(string filePath)
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    List<Sale> sales = csvReader.GetRecords<Sale>().ToList();
                    return sales;
                }
            }
        }
    }
}