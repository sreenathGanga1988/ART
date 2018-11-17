using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

using ArtWebApp.DataModels;
using ArtWebApp.DBTransaction.ReportTransactions;

using System.Data;
using ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel;

namespace ArtWebApp.Areas.ArtMVCMerchandiser.Controllers
{
    public class EXPADNController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew();
        // GET: ArtMVCMerchandiser/EXPADN
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SupplierPODetailsIndex()
        {
            ViewBag.Supplier_PK = new SelectList(enty.SupplierMasters.ToList(), "Supplier_PK", "SupplierName");
            ViewBag.AtcID = new SelectList(enty.AtcMasters.Where(u => u.IsShipmentCompleted == "Y" && u.IsMCRDone != "Y").ToList(), "AtcId", "AtcNum");
            return View();
        }


        


        public class MerchantRepo
        {

            public DataTable GETEXPWISE(DateTime fromdate, DateTime todate)
            {
                DataTable dt = new DataTable();






                using (SqlCommand cmd = new SqlCommand(@"GetADNWISE_SP"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fromdate", fromdate);
                    cmd.Parameters.AddWithValue("@todate", todate);
                  
                    dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
                }


                return dt;
            }
            public DataTable GETSPOEXPWISE(DateTime fromdate, DateTime todate)
            {
                DataTable dt = new DataTable();






                using (SqlCommand cmd = new SqlCommand(@"GetStockADNWISE_SP"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fromdate", fromdate);
                    cmd.Parameters.AddWithValue("@todate", todate);

                    dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
                }


                return dt;
            }
            public DataTable GetSupplierwisePodetails(int @Supplier_PK,DateTime fromdate, DateTime todate)
            {
                DataTable dt = new DataTable();






                using (SqlCommand cmd = new SqlCommand(@"GetSupplierwisePODetails_SP"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fromdate", fromdate);
                    cmd.Parameters.AddWithValue("@todate", todate);
                    cmd.Parameters.AddWithValue("@Supplier_PK", @Supplier_PK);
                  
                    dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
                }


                return dt;
            }

        }

            [HttpGet]
        public PartialViewResult GETEXPWISE(DateTime fromdate, DateTime todate)
        {
            EXPADNReportModel model = new EXPADNReportModel();
            MerchantRepo MerchantReportRepo = new MerchantRepo();
            DataTable dt = MerchantReportRepo.GETEXPWISE(fromdate, todate);
            model.AsqData = dt;

            model.ReportName = "Report Between  " + fromdate.ToShortDateString() + " && " + todate.ToShortDateString();


            return PartialView("EXPADN_P", model);
        }
        [HttpGet]
        public PartialViewResult GETSPOEXPWISE(DateTime fromdate, DateTime todate)
        {
            EXPADNReportModel model = new EXPADNReportModel();
            MerchantRepo MerchantReportRepo = new MerchantRepo();
            DataTable dt = MerchantReportRepo.GETSPOEXPWISE(fromdate, todate);
            model.AsqData = dt;

            model.ReportName = "Report Between  " + fromdate.ToShortDateString() + " && " + todate.ToShortDateString();


            return PartialView("EXPADN_P", model);
        }
        [HttpGet]
        public PartialViewResult GetSupplierwisePODetails(int supplier_pk,DateTime fromdate, DateTime todate)
        {
            EXPADNReportModel model = new EXPADNReportModel();
            MerchantRepo MerchantReportRepo = new MerchantRepo();
            DataTable dt = MerchantReportRepo.GetSupplierwisePodetails (supplier_pk, fromdate, todate);
            model.AsqData = dt;

            model.ReportName = "Report Between  " + fromdate.ToShortDateString() + " && " + todate.ToShortDateString();


            return PartialView("EXPADN_P", model);
        }
    }
}