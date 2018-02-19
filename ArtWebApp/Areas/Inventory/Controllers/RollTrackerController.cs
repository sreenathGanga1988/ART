using ArtWebApp.Areas.Inventory.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.Inventory.Controllers
{
    public class RollTrackerController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();
        // GET: Inventory/RollTracker
        public ActionResult Index()
        {
            ViewBag.AtcID = new SelectList(db.AtcMasters, "AtcId", "AtcNum");
            List<SelectListItem> MonthList = new List<SelectListItem>()
    {   new SelectListItem() {Text="TotalAlloc", Value="TotalAlloc"},
        new SelectListItem() {Text="All", Value="All"},
        new SelectListItem() {Text="January", Value="January"},
        new SelectListItem() { Text="February", Value="February"},
        new SelectListItem() { Text="March", Value="March"},
        new SelectListItem() { Text="April", Value="April"},
        new SelectListItem() { Text="May", Value="May"},
        new SelectListItem() { Text="June", Value="June"},
        new SelectListItem() { Text="July", Value="July"},
        new SelectListItem() { Text="August", Value="August"},
        new SelectListItem() { Text="September", Value="September"},
        new SelectListItem() { Text="October", Value="October"},
         new SelectListItem() { Text="November", Value="November"},
        new SelectListItem() { Text="December", Value="December"},

    }; ViewBag.MonthList = MonthList;
            return View();
        }



        [HttpGet]
        public PartialViewResult GetRollTracker(int supplierdock_pk, int Skudetpk, int RollPk,int cutplanPk)
        {
            InventoryReportDataModel model = new InventoryReportDataModel();
            InventoryReportRepo productionReportRepo = new InventoryReportRepo();

            DataTable dt = productionReportRepo.GetRollTrackData(supplierdock_pk, Skudetpk, RollPk, cutplanPk);
            model.AsqData = dt;
            model.ReportName = "BE Report";
            return PartialView("InventoryRollView", model);
        }

        [HttpGet]
        public PartialViewResult GetDoBetweenDate(DateTime fromdate, DateTime todate, string dotype)
        {
            InventoryReportDataModel model = new InventoryReportDataModel();
            InventoryReportRepo productionReportRepo = new InventoryReportRepo();
            DataTable dt = productionReportRepo.GetDObetweenDate(fromdate, todate, dotype);
            model.AsqData = dt;

            model.ReportName = dotype + "Report Between  " + fromdate.ToShortDateString() + " && " + todate.ToShortDateString();


            return PartialView("InventoryRollView", model);
        }
    }
}