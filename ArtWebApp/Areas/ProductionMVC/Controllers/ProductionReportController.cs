using ArtWebApp.Areas.ProductionMVC.Viewmodel;
using ArtWebApp.DataModels;
using ArtWebApp.DBTransaction.ReportTransactions;
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
        public PartialViewResult GetRatioRequestDetailView(String Month = "All")
        {
            ReportDataModel model = new ReportDataModel();
            ProductionReportRepo productionReportRepo = new ProductionReportRepo();

            model = productionReportRepo.GetBEpercentRatioReport(Month);
            model.ReportName = "BE Report";
            return PartialView("ProductionReportViewer", model);
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
        public PartialViewResult GetAtcDetails()
        {
            ReportDataModel model = new ReportDataModel();
            ProductionReportsTrans acctrn = new ProductionReportsTrans();

            DataTable dt = acctrn.GetAtcDetails();
            model.AsqData = dt;

            model.ReportName = "AtcDetails ";
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
        public PartialViewResult GETADNWISE(DateTime fromdate, DateTime todate)
        {
            ReportDataModel model = new ReportDataModel();
            ProductionReportRepo productionReportRepo = new ProductionReportRepo();
            DataTable dt = productionReportRepo.GETADNWISE(fromdate, todate);
            model.AsqData = dt;

            model.ReportName = "Report Between  " + fromdate.ToShortDateString() + " && " + todate.ToShortDateString();


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

        [HttpGet]
        public PartialViewResult GetJobContractRequestAll(int Id)
        {
            ReportDataModel model = new ReportDataModel();
            ProductionReportRepo productionReportRepo = new ProductionReportRepo();
            DataTable dt = productionReportRepo.GetJobContractMaster(Id);
            model.AsqData = dt;

            model.ReportName = "Job Contract Report";


            return PartialView("ProductionReportViewer", model);
        }


        [HttpGet]
        public PartialViewResult GETCSFA(DateTime fromdate, DateTime todate, int Id)
        {
            ReportDataModel model = new ReportDataModel();
            ProductionReportRepo productionReportRepo = new ProductionReportRepo();
            DataTable dt = productionReportRepo.GetCSFANew(fromdate, todate,Id);
            model.AsqData = dt;

            model.ReportName = "CSFA Report";


            return PartialView("CSFAReportViewer", model);
        }



        [HttpGet]
        public PartialViewResult GETCSFABreakUP(DateTime fromdate, DateTime todate, int Id,String Type)
        {
            ReportDataModel model = new ReportDataModel();
            ProductionReportRepo productionReportRepo = new ProductionReportRepo();
            DataTable dt = productionReportRepo.GetCSFAWithAtcAndRatio(fromdate, todate, Id,Type);
            model.AsqData = dt;

            model.ReportName = "CSFA Report";


            return PartialView("CSFAReportViewer", model);
        }









        [HttpGet]
        public JsonResult UpdateCSFA(DateTime fromdate, DateTime todate)

        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool status = false;
            ArtAdministrator.ArtAdministratorRepo.UpdateCSFA(fromdate,todate);
            status = true;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            JsonResult jsd = Json(new { status = status, filename = elapsedMs }, JsonRequestBehavior.AllowGet);
            return jsd;
        }


    }
}