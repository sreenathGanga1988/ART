using ArtWebApp.Areas.Inventory.ViewModel;
using ArtWebApp.Areas.ProductionMVC;
using ArtWebApp.Areas.ProductionMVC.Viewmodel;
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
            ViewBag.Location_pk = new SelectList(db.LocationMasters, "Location_PK", "LocationName");
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
        public PartialViewResult GetRollTracker(int supplierdock_pk, int Skudetpk, int RollPk,int cutplanPk,string Docnum)
        {
            InventoryReportDataModel model = new InventoryReportDataModel();
            InventoryReportRepo productionReportRepo = new InventoryReportRepo();

            DataTable dt = productionReportRepo.GetRollTrackData(supplierdock_pk, Skudetpk, RollPk, cutplanPk,Docnum);
            model.AsqData = dt;
            model.ReportName = "BE Report";
            return PartialView("InventoryRollView", model);
        }


        [HttpGet]
        public PartialViewResult GetGstockData(int location_pk)
        {
            InventoryReportDataModel model = new InventoryReportDataModel();
            InventoryReportRepo productionReportRepo = new InventoryReportRepo();

            DataTable dt = productionReportRepo.GetGstockData(location_pk);
            model.AsqData = dt;
            model.ReportName = "Gstock Onhand Report";
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




        [HttpGet]
        public PartialViewResult GetRollYardLocation(int AtcID)
        {
            InventoryReportDataModel model = new InventoryReportDataModel();
            InventoryReportRepo productionReportRepo = new InventoryReportRepo();

            DataTable dt = productionReportRepo.GetLocationWiseRoll(AtcID);
            model.AsqData = dt;
            model.ReportName = "Roll Yardage  Allocation ";
            return PartialView("InventoryRollView", model);
        }


        public PartialViewResult GetInventoryBetweenDate(DateTime fromdate, DateTime todate, String Id)
        {
            InventoryReportDataModel model = new InventoryReportDataModel();
            InventoryReportRepo productionReportRepo = new InventoryReportRepo();
            DataTable dt = productionReportRepo.GetInventoryTransferNotesBetweenDate(fromdate, todate, Id);
            model.AsqData = dt;

            model.ReportName = "Transfer Note Report Between  " + fromdate.ToShortDateString() + " && " + todate.ToShortDateString();


            return PartialView("InventoryRollView", model);
        }

        [HttpGet]
        public PartialViewResult GetRoReport()
        {
            InventoryReportDataModel model = new InventoryReportDataModel();
            InventoryReportRepo productionReportRepo = new InventoryReportRepo();
            DataTable dt = productionReportRepo.GetRoReport();
            model.AsqData = dt;

            model.ReportName = "RO Report" ;


            return PartialView("InventoryRollView", model);
        }

        [HttpGet]
        public PartialViewResult GetPurchasesfromSupplier()
        {
            InventoryReportDataModel model = new InventoryReportDataModel();
            DBTransaction.ReportTransactions.AccountReportrans acctrn = new DBTransaction.ReportTransactions.AccountReportrans();

            DataTable dt = acctrn.GetPurchasesfromSupplier();
            model.AsqData = dt;

           model.ReportName = "Purchase of Supplier ";
            return PartialView("InventoryRollView", model);
        }

        public PartialViewResult GetStockRoReport()
        {
            InventoryReportDataModel model = new InventoryReportDataModel();
            InventoryReportRepo productionReportRepo = new InventoryReportRepo();
            DataTable dt = productionReportRepo.GetStockRoReport();
            model.AsqData = dt;

            model.ReportName = "SRO Report" ;


            return PartialView("InventoryRollView", model);
        }

        [HttpGet]
        public PartialViewResult GetCriticalAtc(int Id)
        {
            InventoryReportDataModel model = new InventoryReportDataModel();
            ProductionReportRepo productionReportRepo = new ProductionReportRepo();
            DataTable dt = productionReportRepo.GetCriticalAtc(Id);
            model.AsqData = dt;

           model.ReportName = "Critical Report";


            return PartialView("InventoryRollView", model);
        }



    }
   

}