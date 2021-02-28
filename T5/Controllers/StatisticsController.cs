using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Serilog;
using T5.DataAccesLayer;
using T5.Models;
using T5.ModelsLayer;
using Filter = T5.Models.Filter;

namespace T5.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly Worker _worker = new Worker();
        private const int PageSize = 25;

        public ActionResult Index(int page = 1)
        {
            SalesModel model = new SalesModel
            {
                Sales = _worker.GetAllSales(),
                Managers = _worker.GetAllManagers()
            };

            PageInfo pageInfo = new PageInfo
                {PageNumber = page, PageSize = PageSize, ItemsCount = model.Sales.Count()};

            model.PageInfo = pageInfo;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PageNumberButtonGroup", pageInfo);
            }

            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditSale(int? id)
        {
            try
            {
                Sale sale = _worker.GetById(id);
                if (sale == null)
                {
                    return HttpNotFound();
                }

                return View(sale);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("EditSale")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult EditPost(Sale sale)
        {
            try
            {
                if (sale != null && !sale.IsValid())
                {
                    throw new ArgumentException();
                }

                _worker.Edit(sale);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteSale(int id)
        {
            try
            {
                if (id < 0)
                {
                    throw new ArgumentException();
                }
                _worker.RemoveById(id);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult UpdatePageInfo(Filter filter)
        {
            IEnumerable<Sale> sales = _worker.GetAllSales(filter);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = filter.PageNumber,
                PageSize = PageSize,
                ItemsCount = sales.Count(),
            };
            return PartialView("_PageNumberButtonGroup", pageInfo);
        }

        public ActionResult UpdateSalesTable(Filter filter)
        {
            IEnumerable<Sale> sales = _worker.GetAllSales(filter);
            sales = PageSales(sales, filter.PageNumber);
            return PartialView("_SalesTable", sales);
        }


        public ActionResult GetChart()
        {
            List<string> managers = _worker.GetAllManagers().Select(m => m.LastName).ToList();
            List<int> sales = new List<int>();
            for (int i = 0; i < managers.Count; i++)
            {
                string lastName = managers[i];
                sales.Add(_worker.GetAllSales().Where(s => s.Manager.LastName.Equals(lastName))
                    .ToList()
                    .Count);
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


        private static IEnumerable<Sale> PageSales(IEnumerable<Sale> sales, int page)
        {
            return sales.Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}