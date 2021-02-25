using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using T5.Models;
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
            var sales = _context.Sales
                //.Where(s => DateTime.Compare())
                .OrderBy(s => s.DateTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
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
                        DateTime = s.DateTime,
                        DateTimeString = s.DateTime.ToString(CultureInfo.InvariantCulture)
                    }
                ).ToArray();
            PageInfo pageInfo = new PageInfo
                {PageNumber = page, PageSize = pageSize, ItemsCount = _context.Sales.ToList().Count};
            return PartialView("_PageNumberButtonGroup", pageInfo);
        }

        public ActionResult UpdateSalesTable(string jsonData)
        {
            return PartialView("_SalesTable");
        }

        public JsonResult UpdateSales(string jsonData)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int page = serializer.Deserialize<int>(jsonData);
            //TODO: get values from view

            /*Manager manager;
            DateTime fromDateTime;
            DateTime toDateTime;*/
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
                        //TODO: check if it can be replace by Client = s.Client etc.
                        Client = new Client
                        {
                            Id = s.Client.Id, FirstName = s.Client.FirstName, LastName = s.Client.LastName
                        },
                        Manager = new Manager {Id = s.Manager.Id, LastName = s.Manager.LastName},
                        Product = new Product {Id = s.Product.Id, Name = s.Product.Name,},
                        DateTime = s.DateTime,
                        DateTimeString = s.DateTime.ToString(CultureInfo.InvariantCulture)
                    }
                ).ToArray();
            return Json(sales, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChart()
        {
            List<Manager> managersList = _context.Managers.ToList();
            List<string> managers = new List<string>();
            List<int> sales = new List<int>();

            for (int i = 0; i < managersList.Count; i++)
            {
                Manager manager = managersList[i];
                managers.Add(manager.LastName);
                sales.Add(0);
                sales[sales.Count - 1] += _context.Sales.Where(s => s.Manager.LastName.Equals(manager.LastName))
                    .ToList()
                    .Count;
            }

            byte[] chart = new Chart(width: 800, height: 600)
                .AddTitle("Managers Efficiency")
                .AddSeries(
                    name: "Employee",
                    xValue: managers,
                    yValues: sales)
                .GetBytes();
            return File(chart, "image/png");
        }
    }
}