using System;

namespace T5.ModelsLayer
{
    public class Sale
    {
        public int Id { get; set; }

        public virtual Client Client { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Product Product { get; set; }
        public DateTime DateTime { get; set; }


        public bool IsValid()
        {
            return Client.FirstName.Length <= 20 && Client.LastName.Length <= 20 && Manager.LastName.Length <= 20 && Product.Name.Length <= 20 && Product.Price >= 0;
        }
        
        public override string ToString()
        {
            return $"{Id}, Client {Client?.Id}, Manager {Manager?.Id}, {DateTime.Date}";
        }
    }
}