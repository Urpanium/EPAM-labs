using System;
using System.Collections.Generic;
using System.Linq;

namespace T5.Models
{
    public class SalesModel
    {
        

        public IEnumerable<Sale> Sales;
        public IEnumerable<Manager> Managers;
        public IEnumerable<Product> Product;
        public IEnumerable<Client> Clients;

        public SalesModel()
        {
        }

        public SalesModel(IEnumerable<Sale> sales)
        {
            Sales = sales;
        }

        public SalesModel GetFilteredSalesModel(Manager manager, DateTime fromDateTime, DateTime toDateTime,
            Product product)
        {
            //TODO: make separate class for this?
                
            SalesModel model = new SalesModel();
            model.Sales = Sales.Where(s =>
                (manager == null || s.Manager.Equals(manager))
                && (product == null || s.Product.Equals(product))
                // if sale DateTime is later than fromDateTime
                // check if fromDateTime is initialized at all
                && (fromDateTime.Equals(DateTime.MinValue) || DateTime.Compare(fromDateTime, s.DateTime) >= 0)
                // if sale DateTime is earlier than toDateTime
                // check if toFromDate is initialized at all
                && (toDateTime.Equals(DateTime.MinValue) || DateTime.Compare(toDateTime, s.DateTime) <= 0));
            return model;
        }
    }


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

    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Client(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }

    public class Manager
    {
        public int Id { get; set; }
        public string LastName { get; set; }


        public Manager()
        {
        }

        public Manager(string lastName)
        {
            LastName = lastName;
        }

        public override string ToString()
        {
            return LastName;
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product()
        {
        }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}