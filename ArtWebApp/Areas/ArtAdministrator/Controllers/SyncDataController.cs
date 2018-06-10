using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtAdministrator.Controllers
{
    public class SyncDataController : Controller
    {
     
        // GET: ArtAdministrator/SyncData
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UpdateData()
        {


         return   RedirectToAction("Index");
        }


        
            [HttpGet]
        public JsonResult UpDateLocation()

        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool status = false;
            ArtAdministrator.ArtAdministratorRepo.UpDateLocation();
            status = true;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            JsonResult jsd = Json(new { status = status, filename = elapsedMs }, JsonRequestBehavior.AllowGet);
            return jsd;
        }


        [HttpGet]
        public JsonResult UpdateTTL()

        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool status = false;
            ArtAdministrator.ArtAdministratorRepo.UpDateTTL();
            status = true;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            JsonResult jsd = Json(new { status = status, filename = elapsedMs }, JsonRequestBehavior.AllowGet);
            return jsd;
        }


        [HttpGet]
        public JsonResult UpdateCostperMinute()

        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool status = false;
            ArtAdministrator.ArtAdministratorRepo.UpdateCostperminute();
            status = true;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            JsonResult jsd = Json(new { status = status, filename = elapsedMs }, JsonRequestBehavior.AllowGet);
            return jsd;
        }


        [HttpGet]
        public JsonResult UpDateJobContracttokenya()

        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool status = false;
            ArtAdministrator.ArtAdministratorRepo.UpdateJobcontractTokenya();
            status = true;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            JsonResult jsd = Json(new { status = status, filename = elapsedMs }, JsonRequestBehavior.AllowGet);
            return jsd;
        }
        [HttpGet]
        public JsonResult UpDateJobContractOptionaltokenya()

        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool status = false;
            ArtAdministrator.ArtAdministratorRepo.UpdateJobcontractOptionalTokenya();
            status = true;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            JsonResult jsd = Json(new { status = status, filename = elapsedMs }, JsonRequestBehavior.AllowGet);
            return jsd;
        }




        [HttpGet]
        public JsonResult UpdateInputlineData()

        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool status = false;
            ArtAdministrator.ArtAdministratorRepo.UpdateUpdateInputlineData();
            status = true;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            JsonResult jsd = Json(new { status = status, filename = elapsedMs }, JsonRequestBehavior.AllowGet);
            return jsd;
        }



        [HttpGet]
        public JsonResult UpdateCSFA()

        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool status = false;
          ArtAdministrator.ArtAdministratorRepo.UpdateCSFA();
            status = true;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            JsonResult jsd = Json(new { status = status, filename = elapsedMs }, JsonRequestBehavior.AllowGet);
            return jsd;
        }


        [HttpGet]
        public JsonResult UpdateExtraReq()

        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool status = false;
            ArtAdministrator.ArtAdministratorRepo.UpdateExtraReq();
            status = true;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            JsonResult jsd = Json(new { status = status, filename = elapsedMs }, JsonRequestBehavior.AllowGet);
            return jsd;
        }














        [HttpGet]
        public JsonResult UpdatePL()

        {


            var watch = System.Diagnostics.Stopwatch.StartNew();



            bool status = false;
            ArtAdministrator.ArtAdministratorRepo.UpdatePL();
            status = true;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            JsonResult jsd = Json(new { status = status, filename = elapsedMs }, JsonRequestBehavior.AllowGet);
            return jsd;
        }















        [HttpGet]
        public JsonResult BackUp()

        {
            bool status = false;
         String msg= ArtAdministrator.ArtAdministratorRepo.BackUpDB();
            status = true;
            JsonResult jsd = Json(new { status = status,filename=msg }, JsonRequestBehavior.AllowGet);
            return jsd;
        }

        [HttpGet]
        public JsonResult ZipBackUP(string filename)

        {
            bool status = false;
            String msg = ArtAdministrator.ArtAdministratorRepo.ZipBackUP(filename);
            status = true;
            JsonResult jsd = Json(new { status = status, filename = msg }, JsonRequestBehavior.AllowGet);
            return jsd;
        }


       
    }
}