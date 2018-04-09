using ArtWebApp.Areas.ProductionMVC.Viewmodel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ProductionMVC.Controllers
{
    public class PcdAlertController : Controller
    {
        ArtEntitiesnew db = new ArtEntitiesnew();
        // GET: ProductionMVC/PcdAlert
        public ActionResult Index()
        {
            int userid = int.Parse(HttpContext.Session["User_PK"].ToString());


            try
            {
                ViewBag.Userright = db.PcdAlertUserrights.Where(u => u.User_pk == userid).First();
            }
            catch (Exception)
            {
                ViewBag.Userright = null;


            }

            ViewBag.Weeknum = new SelectList(db.YearWeekMasters, "Week_Pk", "Week_no");
            ViewBag.Atcid = new SelectList(db.AtcMasters, "AtcId", "AtcNum");
            ViewBag.Location_pk= new SelectList(db.LocationMasters.Where(u=>u.LocType=="F"), "Location_PK", "LocationName");
            return View();
        }
        
        public ActionResult PcdPackingView()
        {
            int userid = int.Parse(HttpContext.Session["User_PK"].ToString());
            try
            {
                ViewBag.Userright = db.PcdAlertUserrights.Where(u => u.User_pk == userid).First();
            }
            catch (Exception)
            {
                ViewBag.Userright = null;


            }
            ViewBag.Weeknum = new SelectList(db.YearWeekMasters, "Week_Pk", "Week_no");
            ViewBag.Atcid = new SelectList(db.AtcMasters, "AtcId", "AtcNum");
            ViewBag.Location_pk = new SelectList(db.LocationMasters.Where(u => u.LocType == "F"), "Location_PK", "LocationName");
            return View();
        }

        public ActionResult PcdAlertReport()
        {
            ViewBag.Weeknum = new SelectList(db.YearWeekMasters, "Week_Pk", "Week_no");
            ViewBag.Atcid = new SelectList(db.AtcMasters, "AtcId", "AtcNum");
            ViewBag.Location_pk = new SelectList(db.LocationMasters.Where(u => u.LocType == "F"), "Location_PK", "LocationName");
            return View();
        }



        [HttpPost]
        public JsonResult SubmitDetails(PcdAlertModel things)
        {
            bool status = false;

            PcdAlertRepo prepo = new PcdAlertRepo();
         String msg=   prepo.insertpcdaLERT(things);

            return new JsonResult { Data = new { status = status,msge=msg } };
        }
        [HttpPost]
        public JsonResult SubmitDetailsPacking(PcdAlertModel things)
        {
            bool status = false;

            PcdAlertRepo prepo = new PcdAlertRepo();
            String msg = prepo.insertpcdalertPacking(things);

            return new JsonResult { Data = new { status = status, msge = msg } };
        }

        [HttpGet]
        public PartialViewResult getalreadyadded(DateTime weekno,int locationpk)
        {
            List<PcdAlertModel> things = new List<PcdAlertModel>();
            PcdAlertRepo prepo = new PcdAlertRepo();

            things = prepo.get_pcdalert(weekno, locationpk);
            return PartialView("PcdAlertPartialView", things);
        }
        [HttpGet]
        public PartialViewResult getalreadyadded_packing(DateTime weekno, int locationpk)
        {
            List<PcdAlertModel> things = new List<PcdAlertModel>();
            PcdAlertRepo prepo = new PcdAlertRepo();

            things = prepo.get_pcdalert_packing(weekno, locationpk);
            return PartialView("PcdAlertPartialPacking", things);
        }
        [HttpGet]
        public PartialViewResult GetPCDSewingDetails(DateTime weekno, int locationpk)
        {
            List<PcdAlertModel> things = new List<PcdAlertModel>();
            PcdAlertRepo prepo = new PcdAlertRepo();

            things = prepo.GetPCDSewingDetails(weekno, locationpk);
            return PartialView("SewingPCDReportP", things);
        }
        [HttpGet]
        public PartialViewResult GetPCDPackingDetails(DateTime weekno, int locationpk)
        {
            List<PcdAlertModel> things = new List<PcdAlertModel>();
            PcdAlertRepo prepo = new PcdAlertRepo();

            things = prepo.GetPCDPackingDetails(weekno, locationpk);
            return PartialView("PackingPCDReportP", things);
        }
    }
}