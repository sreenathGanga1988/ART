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
        public JsonResult UpdateTTL()

        {
            bool status = false;
            ArtAdministrator.ArtAdministratorRepo.UpDateTTL();
            status = true;
            JsonResult jsd = Json(new { status = status }, JsonRequestBehavior.AllowGet);
            return jsd;
        }


        [HttpGet]
        public JsonResult UpdateCostperMinute()

        {
            bool status = false;
            ArtAdministrator.ArtAdministratorRepo.UpdateCostperminute();
            status = true;
            JsonResult jsd = Json(new { status = status }, JsonRequestBehavior.AllowGet);
            return jsd;
        }

        [HttpGet]
        public JsonResult BackUp()

        {
            bool status = false;
         String msg= ArtAdministrator.ArtAdministratorRepo.BackUpDB();
            status = true;
            JsonResult jsd = Json(new { status = status }, JsonRequestBehavior.AllowGet);
            return jsd;
        }


    }
}