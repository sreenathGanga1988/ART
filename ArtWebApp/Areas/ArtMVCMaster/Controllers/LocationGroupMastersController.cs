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
    public class LocationGroupMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/LocationGroupMasters
        public ActionResult Index()
        {
            var locationGroupMasters = db.LocationGroupMasters.Include(l => l.LocationMaster);
            return View(locationGroupMasters.ToList());
        }

        // GET: ArtMVCMaster/LocationGroupMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationGroupMaster locationGroupMaster = db.LocationGroupMasters.Find(id);
            if (locationGroupMaster == null)
            {
                return HttpNotFound();
            }
            return View(locationGroupMaster);
        }

        // GET: ArtMVCMaster/LocationGroupMasters/Create
        public ActionResult Create()
        {
            ViewBag.MasterLocationID = new SelectList(db.LocationMasters, "Location_PK", "LocationName");
            return View();
        }

        // POST: ArtMVCMaster/LocationGroupMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationGroupID,MasterLocationID,AddedBy,AddedDate,Description,GroupName")] LocationGroupMaster locationGroupMaster)
        {
            if (ModelState.IsValid)
            {
                db.LocationGroupMasters.Add(locationGroupMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MasterLocationID = new SelectList(db.LocationMasters, "Location_PK", "LocationName", locationGroupMaster.MasterLocationID);
            return View(locationGroupMaster);
        }

        // GET: ArtMVCMaster/LocationGroupMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationGroupMaster locationGroupMaster = db.LocationGroupMasters.Find(id);
            if (locationGroupMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterLocationID = new SelectList(db.LocationMasters, "Location_PK", "LocationName", locationGroupMaster.MasterLocationID);
            return View(locationGroupMaster);
        }

        // POST: ArtMVCMaster/LocationGroupMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationGroupID,MasterLocationID,AddedBy,AddedDate,Description,GroupName")] LocationGroupMaster locationGroupMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationGroupMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MasterLocationID = new SelectList(db.LocationMasters, "Location_PK", "LocationName", locationGroupMaster.MasterLocationID);
            return View(locationGroupMaster);
        }

        // GET: ArtMVCMaster/LocationGroupMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationGroupMaster locationGroupMaster = db.LocationGroupMasters.Find(id);
            if (locationGroupMaster == null)
            {
                return HttpNotFound();
            }
            return View(locationGroupMaster);
        }

        // POST: ArtMVCMaster/LocationGroupMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            LocationGroupMaster locationGroupMaster = db.LocationGroupMasters.Find(id);
            db.LocationGroupMasters.Remove(locationGroupMaster);
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
