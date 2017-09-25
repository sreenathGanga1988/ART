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
    public class LocationGroupDetailsController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/LocationGroupDetails
        public ActionResult Index()
        {
            var locationGroupDetails = db.LocationGroupDetails.Include(l => l.LocationGroupMaster).Include(l => l.LocationMaster);
            return View(locationGroupDetails.ToList());
        }

        // GET: ArtMVCMaster/LocationGroupDetails/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationGroupDetail locationGroupDetail = db.LocationGroupDetails.Find(id);
            if (locationGroupDetail == null)
            {
                return HttpNotFound();
            }
            return View(locationGroupDetail);
        }

        // GET: ArtMVCMaster/LocationGroupDetails/Create
        public ActionResult Create()
        {
            ViewBag.LocationGroupID = new SelectList(db.LocationGroupMasters, "LocationGroupID", "GroupName");
            ViewBag.Loication_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName");
            return View();
        }

        // POST: ArtMVCMaster/LocationGroupDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationDetailID,Loication_PK,AddedDate,AddedBy,LocationGroupID")] LocationGroupDetail locationGroupDetail)
        {
            if (ModelState.IsValid)
            {
                db.LocationGroupDetails.Add(locationGroupDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationGroupID = new SelectList(db.LocationGroupMasters, "LocationGroupID", "GroupName", locationGroupDetail.LocationGroupID);
            ViewBag.Loication_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName", locationGroupDetail.Loication_PK);
            return View(locationGroupDetail);
        }

        // GET: ArtMVCMaster/LocationGroupDetails/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationGroupDetail locationGroupDetail = db.LocationGroupDetails.Find(id);
            if (locationGroupDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationGroupID = new SelectList(db.LocationGroupMasters, "LocationGroupID", "GroupName", locationGroupDetail.LocationGroupID);
            ViewBag.Loication_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName", locationGroupDetail.Loication_PK);
            return View(locationGroupDetail);
        }

        // POST: ArtMVCMaster/LocationGroupDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationDetailID,Loication_PK,AddedDate,AddedBy,LocationGroupID")] LocationGroupDetail locationGroupDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationGroupDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationGroupID = new SelectList(db.LocationGroupMasters, "LocationGroupID", "GroupName", locationGroupDetail.LocationGroupID);
            ViewBag.Loication_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName", locationGroupDetail.Loication_PK);
            return View(locationGroupDetail);
        }

        // GET: ArtMVCMaster/LocationGroupDetails/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationGroupDetail locationGroupDetail = db.LocationGroupDetails.Find(id);
            if (locationGroupDetail == null)
            {
                return HttpNotFound();
            }
            return View(locationGroupDetail);
        }

        // POST: ArtMVCMaster/LocationGroupDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            LocationGroupDetail locationGroupDetail = db.LocationGroupDetails.Find(id);
            db.LocationGroupDetails.Remove(locationGroupDetail);
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
