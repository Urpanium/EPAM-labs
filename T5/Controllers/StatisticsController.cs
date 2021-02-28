using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using T5.Models;
using T5.Service;
using T5.Stuff;

namespace T5.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private const int PageSize = 25;

        private int _lastPagesCount;

        public ActionResult Index(int page = 1)
        {
            SalesModel model = new SalesModel
            {
                Sales = _context.Sales
                    .ToList()
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .Select(
                        s => new Sale
                        {
                            Id = s.Id,
                            Client = new Client
                            {
                                Id = s.Client.Id, FirstName = s.Client.FirstName, LastName = s.Client.LastName
                            },
                            Manager = new Manager {Id = s.Manager.Id, LastName = s.Manager.LastName},
                            Product = new Product {Id = s.Product.Id, Name = s.Product.Name},
                            DateTime = s.DateTime
                        }
                    ),
                Managers = _context.Managers,
                Clients = _context.Clients,
                Products = _context.Products
            };
            //_lastPagesCount = model.Sales.Count();
            PageInfo pageInfo = new PageInfo
                {PageNumber = page, PageSize = PageSize, ItemsCount = model.Sales.Count()};
            model.PageInfo = pageInfo;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PageNumberButtonGroup", pageInfo);
            }

            return View(model);
        }

        public ActionResult UpdatePageInfo(string jsonData)
        {
            Console.WriteLine($"JSON DATA: '{jsonData}'");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int page = serializer.Deserialize<int>(jsonData);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = PageSize,
                ItemsCount = _context.Sales.Count(),
            };
            return PartialView("_PageNumberButtonGroup", pageInfo);
        }

        public ActionResult UpdateSalesTable(FilterModel filter)
        {
            Console.WriteLine("UpdateSalesTable called");
            Console.WriteLine($"FILTER: {filter}");

            DateTime fromDateTime = DateTime.ParseExact(filter.FromDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime toDateTime = DateTime.ParseExact(filter.ToDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            int page = filter.PageNumber;
            IEnumerable<Sale> sales = GetSales(filter.ManagerLastName, fromDateTime, toDateTime, page);
            return PartialView("_SalesTable", sales);
        }

        private IEnumerable<Sale> GetSales(string managerLastName, DateTime fromDateTime, DateTime toDateTime, int page)
        {
            var sales = _context.Sales
                .OrderBy(s => s.Id)
                .Where(s => (s.DateTime.Equals(DateTime.MinValue) || DateTime.Compare(s.DateTime, fromDateTime) >= 0)
                            && (s.DateTime.Equals(DateTime.MinValue) || DateTime.Compare(s.DateTime, toDateTime) <= 0)
                            && (managerLastName.Equals("All managers") || s.Manager.LastName.Equals(managerLastName)))
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList()
                .Select(
                    s => new Sale
                    {
                        Id = s.Id,
                        Client = new Client
                        {
                            Id = s.Client.Id, FirstName = s.Client.FirstName, LastName = s.Client.LastName
                        },
                        Manager = new Manager {Id = s.Manager.Id, LastName = s.Manager.LastName},
                        Product = new Product {Id = s.Product.Id, Name = s.Product.Name,},
                        DateTime = s.DateTime,
                    }
                );
            _lastPagesCount = sales.Count();
            return sales;
        }

        public ActionResult GetChart()
        {
            List<string> managers = _context.Managers.Select(m => m.LastName).ToList();
            List<int> sales = new List<int>();
            for (int i = 0; i < managers.Count; i++)
            {
                string lastName = managers[i];
                sales.Add(0);
                sales[sales.Count - 1] += _context.Sales.Where(s => s.Manager.LastName.Equals(lastName))
                    .ToList()
                    .Count;
            }

            byte[] chart = new Chart(800, 600)
                .AddTitle("Managers Efficiency")
                .AddSeries(
                    name: "Manager",
                    xValue: managers,
                    yValues: sales)
                .GetBytes();
            return File(chart, "image/png");
        }
    }
}