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
    public class MarkerDirectionMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/MarkerDirectionMasters
        public ActionResult Index()
        {
            return View(db.MarkerDirectionMasters.ToList());
        }

        // GET: ArtMVCMaster/MarkerDirectionMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkerDirectionMaster markerDirectionMaster = db.MarkerDirectionMasters.Find(id);
            if (markerDirectionMaster == null)
            {
                return HttpNotFound();
            }
            return View(markerDirectionMaster);
        }

        // GET: ArtMVCMaster/MarkerDirectionMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMaster/MarkerDirectionMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarkerDirectionID,MarkerDirection,IsActive")] MarkerDirectionMaster markerDirectionMaster)
        {
            if (ModelState.IsValid)
            {
                db.MarkerDirectionMasters.Add(markerDirectionMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(markerDirectionMaster);
        }

        // GET: ArtMVCMaster/MarkerDirectionMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkerDirectionMaster markerDirectionMaster = db.MarkerDirectionMasters.Find(id);
            if (markerDirectionMaster == null)
            {
                return HttpNotFound();
            }
            return View(markerDirectionMaster);
        }

        // POST: ArtMVCMaster/MarkerDirectionMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarkerDirectionID,MarkerDirection,IsActive")] MarkerDirectionMaster markerDirectionMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(markerDirectionMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(markerDirectionMaster);
        }

        // GET: ArtMVCMaster/MarkerDirectionMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkerDirectionMaster markerDirectionMaster = db.MarkerDirectionMasters.Find(id);
            if (markerDirectionMaster == null)
            {
                return HttpNotFound();
            }
            return View(markerDirectionMaster);
        }

        // POST: ArtMVCMaster/MarkerDirectionMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            MarkerDirectionMaster markerDirectionMaster = db.MarkerDirectionMasters.Find(id);
            db.MarkerDirectionMasters.Remove(markerDirectionMaster);
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
