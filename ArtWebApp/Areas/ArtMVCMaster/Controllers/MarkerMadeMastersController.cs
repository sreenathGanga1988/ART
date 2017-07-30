using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.ArtMVCMaster.Controllers
{
    public class MarkerMadeMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/MarkerMadeMasters
        public ActionResult Index()
        {
            return View(db.MarkerMadeMasters.ToList());
        }

        // GET: ArtMVCMaster/MarkerMadeMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkerMadeMaster markerMadeMaster = db.MarkerMadeMasters.Find(id);
            if (markerMadeMaster == null)
            {
                return HttpNotFound();
            }
            return View(markerMadeMaster);
        }

        // GET: ArtMVCMaster/MarkerMadeMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMaster/MarkerMadeMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CuttingMarkerMadeID,MarkerMade,IsActive")] MarkerMadeMaster markerMadeMaster)
        {
            if (ModelState.IsValid)
            {
                db.MarkerMadeMasters.Add(markerMadeMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(markerMadeMaster);
        }

        // GET: ArtMVCMaster/MarkerMadeMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkerMadeMaster markerMadeMaster = db.MarkerMadeMasters.Find(id);
            if (markerMadeMaster == null)
            {
                return HttpNotFound();
            }
            return View(markerMadeMaster);
        }

        // POST: ArtMVCMaster/MarkerMadeMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CuttingMarkerMadeID,MarkerMade,IsActive")] MarkerMadeMaster markerMadeMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(markerMadeMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(markerMadeMaster);
        }

        // GET: ArtMVCMaster/MarkerMadeMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkerMadeMaster markerMadeMaster = db.MarkerMadeMasters.Find(id);
            if (markerMadeMaster == null)
            {
                return HttpNotFound();
            }
            return View(markerMadeMaster);
        }

        // POST: ArtMVCMaster/MarkerMadeMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            MarkerMadeMaster markerMadeMaster = db.MarkerMadeMasters.Find(id);
            db.MarkerMadeMasters.Remove(markerMadeMaster);
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
