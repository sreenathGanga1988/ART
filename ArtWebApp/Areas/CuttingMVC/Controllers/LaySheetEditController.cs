using ArtWebApp.BLL.ProductionBLL;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.CuttingMVC.Controllers

{
    public class LaySheetEditController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: LaySheetEdit
        public ActionResult Index()
        {
            
            ViewBag.LaySheet_PK = new SelectList(db.LaySheetMasters.Where (u=>u.IsApproved=="N"), "LaySheet_PK", "LaySheetNum");
            return View();
        }
      
        public PartialViewResult Details(decimal id)
        {
            if (id == null)
            {
               
            }
            LaySheetMaster laySheetMaster = db.LaySheetMasters.Find(id);
            if (laySheetMaster == null)
            {
                
            }
            return PartialView("_LaysheetDetails", laySheetMaster);
            //    return PartialView("Details", laySheetMaster);


        }


        public ActionResult DeleteRoll(int rollid)
        {
            LaysheetMasterData lymstrdata = new LaysheetMasterData();
            lymstrdata.DeletelaysheetdetailRoll(rollid);
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}