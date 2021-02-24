using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using T5.Stuff;

namespace T5.Models
{
    public class SalesModel
    {
        

        public IEnumerable<Sale> Sales;
        public IEnumerable<Manager> Managers;
        public IEnumerable<Product> Products;
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
                // check if manager is initialized at all
                (manager == null || s.Manager.Equals(manager))
                // check if product is initialized at all
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
}