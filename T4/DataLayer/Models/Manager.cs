using System.Collections.Generic;

namespace T4.DataLayer.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        
        //public ICollection<Sale> Sales { get; set; }

        public Manager()
        {
            //Sales = new List<Sale>();
        }
    }
}