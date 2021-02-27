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
        private readonly int pageSize = 25;

        public ActionResult Index(int page = 1)
        {
            //TODO: move to config

            // make model
            SalesModel model = new SalesModel
            {
                Sales = _context.Sales
                    .ToList()
                    .Select(
                        s => new Sale
                        {
                            Id = s.Id,
                            //TODO: check if it can be replace by Client = s.Client etc.
                            Client = new Client
                            {
                                Id = s.Client.Id, FirstName = s.Client.FirstName, LastName = s.Client.LastName
                            },
                            Manager = new Manager {Id = s.Manager.Id, LastName = s.Manager.LastName},
                            Product = new Product {Id = s.Product.Id, Name = s.Product.Name,},
                            DateTime = s.DateTime
                        }
                    ),
                //Sales = salesForPage,
                Managers = _context.Managers,
                Clients = _context.Clients,
                Products = _context.Products
            };
            PageInfo pageInfo = new PageInfo
                {PageNumber = page, PageSize = pageSize, ItemsCount = _context.Sales.ToList().Count};
            model.PageInfo = pageInfo;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PageNumberButtonGroup", pageInfo);
            }

            return View(model);
            //return Json(_context.Sales.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePageInfo(string jsonData)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int page = serializer.Deserialize<int>(jsonData);
            PageInfo pageInfo = new PageInfo
                {PageNumber = page, PageSize = pageSize, ItemsCount = _context.Sales.ToList().Count};
            return PartialView("_PageNumberButtonGroup", pageInfo);
        }

        public ActionResult UpdateSalesTable(string jsonData)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int page = serializer.Deserialize<int>(jsonData);
            var sales = _context.Sales
                //.Where(s => DateTime.Compare())
                .OrderBy(s => s.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
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
                        Product = new Product {Id = s.Product.Id, Name = s.Product.Name},
                        DateTime = s.DateTime,
                    }
                ).ToArray();
            return PartialView("_SalesTable", sales);
        }

        


        public ActionResult GetChart()
        {
            //TODO: rewrite
            List<string> managers = _context.Managers.Select(m => m.LastName).ToList();
            List<int> sales = new List<int>();
            for (int i = 0; i < managers.Count; i++)
            {
                string lastName = managers[i];
                managers.Add(lastName);
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