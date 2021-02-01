using System;

namespace T4.DataLayer.Models.CSV
{
    // local sale structure stored in CVS's
    public class Sale
    {
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }
}