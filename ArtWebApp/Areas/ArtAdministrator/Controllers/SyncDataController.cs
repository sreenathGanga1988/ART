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


    }
}