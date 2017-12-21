using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.CuttingMVC
{
    public class CutPlanMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: CuttingMVC/CutPlanMasters
        public ActionResult Index()
        {
            var cutPlanMasters = db.CutPlanMasters.Include(c => c.AtcDetail).Include(c => c.LocationMaster).Include(c => c.SkuRawmaterialDetail);
            return View(cutPlanMasters.ToList());
        }

        // GET: CuttingMVC/CutPlanMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CutPlanMaster cutPlanMaster = db.CutPlanMasters.Find(id);
            if (cutPlanMaster == null)
            {
                return HttpNotFound();
            }
            return View(cutPlanMaster);
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
