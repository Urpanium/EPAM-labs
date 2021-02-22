using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using T5.Models;

namespace T5.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sales()
        {
            ViewBag.Message = "KAVO";
            SalesModel model = new SalesModel();
            model.Managers = context.Managers;

            return View(model);
        }

        public ActionResult GetChart()
        {
            //TODO: add filter to this shit
            List<Manager> managersList = context.Managers.ToList();
            List<string> managers = new List<string>();
            List<int> sales = new List<int>();
            Console.WriteLine($"GET CHART CALLED {managers.Count} {sales.Count} {managersList.Count}");
            
            for (int i = 0; i < managersList.Count; i++)
            {
                Manager manager = managersList[i];
                Console.WriteLine($"FOR INDEX: {i}");
                managers.Add(manager.LastName);
                sales.Add(context.Sales.Where(s => s.Manager.LastName.Equals(manager.LastName)).ToList().Count);
            }

            byte[] chart = new Chart(width: 800, height: 600)
                .AddTitle("Employee's Efficiency")
                .AddSeries(
                    name: "Employee",
                    xValue: managers,
                    yValues: sales)
                //xValue: new[] {"Peter", "Andrew", "Julie", "Mary", "Dave"},
                //yValues: new[] {2, 6, 4, 5, 3})
                .GetBytes();
            Console.WriteLine($"GET CHART CALLED {managers.Count} {sales.Count}");
            return File(chart, "image/png");
        }


        public ActionResult UpdateChart()
        {
            // Return the contents of the Stream to the client
            return base.File("~/Content/chart", "png");
        }
    }
}