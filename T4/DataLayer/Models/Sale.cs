using System;

namespace T4.DataLayer.Models
{
    public class Sale
    {
        public int Id { get; set; }
        
        public Client Client { get; set; }
        public Manager Manager { get; set; }
        public Product Product { get; set; }
        
        public DateTime DateTime { get; set; }

        public override string ToString()
        {
            return $"{Id}, Client {Client?.Id}, Manager {Manager?.Id}, {DateTime.Date}";
        }
    }
}