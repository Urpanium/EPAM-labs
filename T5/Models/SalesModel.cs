using System.Collections.Generic;
using T5.ModelsLayer;

namespace T5.Models
{
    public class SalesModel
    {
        public IEnumerable<Sale> Sales { get; set; }
        
        public IEnumerable<Manager> Managers { get; set; }
        public PageInfo PageInfo { get; set; }

    }
}