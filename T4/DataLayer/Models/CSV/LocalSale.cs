namespace T4.DataLayer.Models.CSV
{
    // local sale structure stored in CVS's
    public class LocalSale
    {
        public int Id { get; set; }
        public string DateTime { get; set; }

        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }

        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}