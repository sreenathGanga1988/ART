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
    public class PatternNameBanksController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: CuttingMVC/PatternNameBanks
        public ActionResult Index()
        {
            var patternNameBanks = db.PatternNameBanks.Include(p => p.AtcDetail).Include(p => p.LocationMaster).Include(p => p.SkuRawmaterialDetail);
            return View(patternNameBanks.ToList());
        }

        // GET: CuttingMVC/PatternNameBanks/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatternNameBank patternNameBank = db.PatternNameBanks.Find(id);
            if (patternNameBank == null)
            {
                return HttpNotFound();
            }
            return View(patternNameBank);
        }

        // GET: CuttingMVC/PatternNameBanks/Create
        public ActionResult Create()
        {
            ViewBag.OurStyleID = new SelectList(db.AtcDetails, "OurStyleID", "OurStyle");
            ViewBag.Location_Pk = new SelectList(db.LocationMasters, "Location_PK", "LocationName");
            ViewBag.Skudetpk = new SelectList(db.SkuRawmaterialDetails, "SkuDet_PK", "ColorCode");
            return View();
        }

        // POST: CuttingMVC/PatternNameBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatternID,Skudetpk,OurStyleID,Shrinkage,PatternName,ReferncePatternName,Location_Pk,AddedBy,AddedDate")] PatternNameBank patternNameBank)
        {
            if (ModelState.IsValid)
            {
                db.PatternNameBanks.Add(patternNameBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OurStyleID = new SelectList(db.AtcDetails, "OurStyleID", "OurStyle", patternNameBank.OurStyleID);
            ViewBag.Location_Pk = new SelectList(db.LocationMasters, "Location_PK", "LocationName", patternNameBank.Location_Pk);
            ViewBag.Skudetpk = new SelectList(db.SkuRawmaterialDetails, "SkuDet_PK", "ColorCode", patternNameBank.Skudetpk);
            return View(patternNameBank);
        }

        // GET: CuttingMVC/PatternNameBanks/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatternNameBank patternNameBank = db.PatternNameBanks.Find(id);
            if (patternNameBank == null)
            {
                return HttpNotFound();
            }
            ViewBag.OurStyleID = new SelectList(db.AtcDetails, "OurStyleID", "OurStyle", patternNameBank.OurStyleID);
            ViewBag.Location_Pk = new SelectList(db.LocationMasters, "Location_PK", "LocationName", patternNameBank.Location_Pk);
            ViewBag.Skudetpk = new SelectList(db.SkuRawmaterialDetails, "SkuDet_PK", "ColorCode", patternNameBank.Skudetpk);
            return View(patternNameBank);
        }

        // POST: CuttingMVC/PatternNameBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatternID,Skudetpk,OurStyleID,Shrinkage,PatternName,ReferncePatternName,Location_Pk,AddedBy,AddedDate")] PatternNameBank patternNameBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patternNameBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OurStyleID = new SelectList(db.AtcDetails, "OurStyleID", "OurStyle", patternNameBank.OurStyleID);
            ViewBag.Location_Pk = new SelectList(db.LocationMasters, "Location_PK", "LocationName", patternNameBank.Location_Pk);
            ViewBag.Skudetpk = new SelectList(db.SkuRawmaterialDetails, "SkuDet_PK", "ColorCode", patternNameBank.Skudetpk);
            return View(patternNameBank);
        }

        // GET: CuttingMVC/PatternNameBanks/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatternNameBank patternNameBank = db.PatternNameBanks.Find(id);
            if (patternNameBank == null)
            {
                return HttpNotFound();
            }
            return View(patternNameBank);
        }

        // POST: CuttingMVC/PatternNameBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PatternNameBank patternNameBank = db.PatternNameBanks.Find(id);
            db.PatternNameBanks.Remove(patternNameBank);
            db.SaveChanges();
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
