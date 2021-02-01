using System.Collections.Generic;

namespace T4.DataLayer.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public ICollection<Sale> Sales { get; set; }

        public Client()
        {
            Sales = new List<Sale>();
        }
    }
}