using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.MVCTNA.Controllers
{
    public class ProductionTNAsController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: MVCTNA/ProductionTNAs
        public ActionResult Index()
        {
            var productionTNAs = db.ProductionTNAs.Include(p => p.AtcDetail).Include(p => p.LocationMaster);
            return View(productionTNAs.ToList());
        }

        // GET: MVCTNA/ProductionTNAs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionTNA productionTNA = db.ProductionTNAs.Find(id);
            if (productionTNA == null)
            {
                return HttpNotFound();
            }
            return View(productionTNA);
        }

        // GET: MVCTNA/ProductionTNAs/Create
        public ActionResult Create()
        {
            ViewBag.OurStyleID = new SelectList(db.AtcDetails, "OurStyleID", "OurStyle");
            ViewBag.Location_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName");
            return View();
        }

        // POST: MVCTNA/ProductionTNAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductionTNID,OurStyleID,Location_PK,PPSumbissionDate,PPApprovalDate,SampleYardagesDate,GradedPatternDate,BulkFabricDate,SewingTrimDate,SizeSetDate,PPMeetingDate,FC1Date,FinalMarkerDate,FactoryPlannedPCDDate,MerchandisingPCDDate,InputDate,PackingTrimDate")] ProductionTNA productionTNA)
        {
            if (ModelState.IsValid)
            {
                db.ProductionTNAs.Add(productionTNA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OurStyleID = new SelectList(db.AtcDetails, "OurStyleID", "OurStyle", productionTNA.OurStyleID);
            ViewBag.Location_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName", productionTNA.Location_PK);
            return View(productionTNA);
        }

        // GET: MVCTNA/ProductionTNAs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionTNA productionTNA = db.ProductionTNAs.Find(id);
            if (productionTNA == null)
            {
                return HttpNotFound();
            }
            ViewBag.OurStyleID = new SelectList(db.AtcDetails, "OurStyleID", "OurStyle", productionTNA.OurStyleID);
            ViewBag.Location_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName", productionTNA.Location_PK);
            return View(productionTNA);
        }

        // POST: MVCTNA/ProductionTNAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductionTNID,OurStyleID,Location_PK,PPSumbissionDate,PPApprovalDate,SampleYardagesDate,GradedPatternDate,BulkFabricDate,SewingTrimDate,SizeSetDate,PPMeetingDate,FC1Date,FinalMarkerDate,FactoryPlannedPCDDate,MerchandisingPCDDate,InputDate,PackingTrimDate")] ProductionTNA productionTNA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productionTNA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OurStyleID = new SelectList(db.AtcDetails, "OurStyleID", "OurStyle", productionTNA.OurStyleID);
            ViewBag.Location_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName", productionTNA.Location_PK);
            return View(productionTNA);
        }

        // GET: MVCTNA/ProductionTNAs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionTNA productionTNA = db.ProductionTNAs.Find(id);
            if (productionTNA == null)
            {
                return HttpNotFound();
            }
            return View(productionTNA);
        }

        // POST: MVCTNA/ProductionTNAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ProductionTNA productionTNA = db.ProductionTNAs.Find(id);
            db.ProductionTNAs.Remove(productionTNA);
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
