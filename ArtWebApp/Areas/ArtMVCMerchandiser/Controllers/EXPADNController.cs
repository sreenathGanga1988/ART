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
        // GET: ArtMVCMerchandiser/EXPADN
        public ActionResult Index()
        {
            return View();
        }




        ////[HttpGet]
        ////public PartialViewResult GETCSFA(DateTime fromdate, DateTime todate, int Id)
        ////{
        ////    ReportDataModel model = new ReportDataModel();
        ////    ProductionReportRepo productionReportRepo = new ProductionReportRepo();
        ////    DataTable dt = productionReportRepo.GetCSFANew(fromdate, todate, Id);
        ////    model.AsqData = dt;

        ////    model.ReportName = "CSFA Report";


        ////    return PartialView("CSFAReportViewer", model);
        ////}

       





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
    }
}