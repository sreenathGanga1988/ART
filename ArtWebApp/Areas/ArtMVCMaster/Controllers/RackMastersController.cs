using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;
using System.Data.SqlClient;


namespace ArtWebApp.Areas.ArtMVCMaster.Controllers
{
    public class RackMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/RackMasters
        public ActionResult Index()
        {
            var rackMasters = db.RackMasters.Include(r => r.LocationMaster).Where(u=> u.IsActive =="Y");
            return View(rackMasters.ToList());
        }

        // GET: ArtMVCMaster/RackMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RackMaster rackMaster = db.RackMasters.Find(id);
            if (rackMaster == null)
            {
                return HttpNotFound();
            }
            return View(rackMaster);
        }

        // GET: ArtMVCMaster/RackMasters/Create
        public ActionResult Create()
        {
            ViewBag.Location_pk = new SelectList(db.LocationMasters.Where(u => u.LocType == "W").ToList(), "Location_PK", "LocationName");
            return View();
        }

        // POST: ArtMVCMaster/RackMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Rack_pk,Location_pk,Rack_name,Rack_type,AddedBy,AddedDate,IsActive")] RackMaster rackMaster)
        {
            if (ModelState.IsValid)
            {
                RackMaster rackMaster1 = new RackMaster();
                rackMaster1.Location_pk = rackMaster.Location_pk;
                rackMaster1.Rack_name = rackMaster.Rack_name;
                rackMaster1.Rack_type = rackMaster.Rack_type;
                rackMaster1.AddedBy= HttpContext.Session["Username"].ToString();
                rackMaster1.AddedDate = DateTime.Now;
                rackMaster1.IsActive = "Y";
                db.RackMasters.Add(rackMaster1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Location_pk = new SelectList(db.LocationMasters.Where(u => u.LocType == "W").ToList(), "Location_PK", "LocationName", rackMaster.Location_pk);
            return View(rackMaster);
        }

        // GET: ArtMVCMaster/RackMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RackMaster rackMaster = db.RackMasters.Find(id);
            if (rackMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.Location_pk = new SelectList(db.LocationMasters.Where(u => u.LocType == "W").ToList(), "Location_PK", "LocationName", rackMaster.Location_pk);
            return View(rackMaster);
        }

        // POST: ArtMVCMaster/RackMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Rack_pk,Location_pk,Rack_name,Rack_type,AddedBy,AddedDate,IsActive")] RackMaster rackMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rackMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Location_pk = new SelectList(db.LocationMasters.Where(u => u.LocType == "W").ToList(), "Location_PK", "LocationName", rackMaster.Location_pk);
            return View(rackMaster);
        }

        // GET: ArtMVCMaster/RackMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RackMaster rackMaster = db.RackMasters.Find(id);
            if (rackMaster == null)
            {
                return HttpNotFound();
            }
            return View(rackMaster);
        }

        // POST: ArtMVCMaster/RackMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            RackMaster rackMaster = db.RackMasters.Find(id);
            db.RackMasters.Remove(rackMaster);
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
