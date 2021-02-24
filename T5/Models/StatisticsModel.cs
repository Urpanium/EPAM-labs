using System.Collections.Generic;
using T5.Stuff;

namespace T5.Models
{
    public class StatisticsModel
    {
        public IEnumerable<Sale> Sales { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Manager> Managers { get; set; }
    }
}