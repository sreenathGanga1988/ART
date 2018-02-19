using ArtWebApp.Areas.ProductionMVC.Viewmodel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ProductionMVC.Controllers
{
    public class ProductionReportController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();
        // GET: ProductionMVC/ProductionReport
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
        public PartialViewResult GetRequestDetailView(String Month="All")
        {
            ReportDataModel model = new ReportDataModel();
            ProductionReportRepo productionReportRepo = new ProductionReportRepo();
          
            model = productionReportRepo.GetBEpercentReport(Month);
            model.ReportName = "BE Report";
            return PartialView("BEProductionReportViewer", model);
        }

        [HttpGet]
        public PartialViewResult GetCostingData(int Id)
        {
            ReportDataModel model = new ReportDataModel();
            DBTransaction.ReportTransactions.AccountReportrans acctrn = new DBTransaction.ReportTransactions.AccountReportrans();

            DataTable dt = acctrn.GetCostingData(Id);
            model.AsqData = dt;
          
            model.ReportName = "Costing Report";
            return PartialView("ProductionReportViewer", model);
        }

        [HttpGet]
        public PartialViewResult GetPurchasesfromSupplier()
        {
            ReportDataModel model = new ReportDataModel();
            DBTransaction.ReportTransactions.AccountReportrans acctrn = new DBTransaction.ReportTransactions.AccountReportrans();

            DataTable dt = acctrn.GetPurchasesfromSupplier();
            model.AsqData = dt;

            model.ReportName = "Purchase of Supplier ";
            return PartialView("ProductionReportViewer", model);
        }

        [HttpGet]
        public PartialViewResult GetShipmentofAtc(int Id)
        {
            ReportDataModel model = new ReportDataModel();
            ProductionReportRepo productionReportRepo = new ProductionReportRepo();
            DataTable dt = productionReportRepo.GetShipmentofAtc(Id);
            model.AsqData = dt;
        
            model.ReportName = "Shipment Report";

           
            return PartialView("ProductionReportViewer", model);
        }



        [HttpGet]
        public PartialViewResult GetDoBetweenDate(DateTime fromdate,DateTime todate,string dotype)
        {
            ReportDataModel model = new ReportDataModel();
            ProductionReportRepo productionReportRepo = new ProductionReportRepo();
            DataTable dt = productionReportRepo.GetDObetweenDate(fromdate, todate, dotype);
            model.AsqData = dt;

            model.ReportName = dotype + "Report Between  "+ fromdate.ToShortDateString() + " && "+ todate.ToShortDateString();


            return PartialView("ProductionReportViewer", model);
        }



        [HttpGet]
        public PartialViewResult GetRejectionRequest(int Id)
        {
            ReportDataModel model = new ReportDataModel();
            ProductionReportRepo productionReportRepo = new ProductionReportRepo();
            DataTable dt = productionReportRepo.GetRejectionRequest(Id);
            model.AsqData = dt;

            model.ReportName = "Rejection Request Report";


            return PartialView("ProductionReportViewer", model);
        }

      





    }
}