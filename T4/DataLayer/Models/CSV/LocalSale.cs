using System;

namespace T4.DataLayer.Models.CSV
{
    // local sale structure stored in CVS's
    public class LocalSale
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }

        public int ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}