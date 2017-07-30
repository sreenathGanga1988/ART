using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVC.Controllers
{
    public class ApproveLaysheetController : Controller
    {

        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: ArtMVC/ApproveLaysheet
        public ActionResult ApproveLaysheet()
        {

            ViewBag.AtcID = new SelectList(db.AtcMasters.Where(o => o.IsClosed == "N"), "AtcId", "AtcNum");
            return View();
        }



        [HttpGet]
        public JsonResult PopulateOurStyle(int Id = 0)
        {


            SelectList ourstyleitem = new SelectList(db.AtcDetails.Where(o => o.AtcId == Id), "OurStyleID", "OurStyle");

            
            JsonResult jsd = Json(ourstyleitem, JsonRequestBehavior.AllowGet);

            return jsd;
         
        }







    }
}