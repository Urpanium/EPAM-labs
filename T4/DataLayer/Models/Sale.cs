using System;

namespace T4.DataLayer.Models
{
    public class Sale
    {
        public int Price { get; set; }
        public int ClientId { get; set; }
        public int ManagerId { get; set; }
        //???
        public Client Client { get; set; }
        public Manager Manager { get; set; }
        
        public DateTime DateTime { get; set; }
        
    }
}