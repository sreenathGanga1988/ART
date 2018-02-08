using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.CuttingMVC.Controllers
{
    public class FabricMissingsController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: CuttingMVC/FabricMissings
        public ActionResult Index()
        {
            return View(db.FabricMissings.Where(u=>u.IsLevel1Approved=="N").ToList());
        }

        // GET: CuttingMVC/FabricMissings/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FabricMissing fabricMissing = db.FabricMissings.Find(id);
            if (fabricMissing == null)
            {
                return HttpNotFound();
            }
            return View(fabricMissing);
        }

        // GET: CuttingMVC/FabricMissings/Details/5
        public ActionResult Approve(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FabricMissing fabricMissing = db.FabricMissings.Find(id);
            if (fabricMissing == null)
            {
                return HttpNotFound();
            }
            else
            {
                fabricMissing.Level1ApprovedBY = HttpContext.Session["Username"].ToString();
                fabricMissing.Level1ApprovedDate = DateTime.Now;
                fabricMissing.IsLevel1Approved = "Y";

                db.Entry(fabricMissing).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Error"] = " Approved";
            }
          
            return RedirectToAction("Index");
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
