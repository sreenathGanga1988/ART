using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.ITAdministrator.Controllers
{
    public class DeviceMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ITAdministrator/DeviceMasters
        public ActionResult Index()
        {
            var deviceMasters = db.DeviceMasters.Include(d => d.DeviceType1).OrderBy(u=>u.Manufacturer).Include(d => d.LocationMaster);
            return View(deviceMasters.ToList());
        }

        // GET: ITAdministrator/DeviceMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceMaster deviceMaster = db.DeviceMasters.Find(id);
            if (deviceMaster == null)
            {
                return HttpNotFound();
            }
            return View(deviceMaster);
        }

        // GET: ITAdministrator/DeviceMasters/Create
        public ActionResult Create()
        {
            ViewBag.DeviceType = new SelectList(db.DeviceTypes, "DeviceTypeID", "DeviceTypeName");
            ViewBag.Location_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName");
            return View();
        }

        // POST: ITAdministrator/DeviceMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NodeID,MacID,DeviceName,IPAddress,Username,Password,Manufacturer,DeviceType,Location_PK,IsActive,Remark,SerialNumber,OSRef")] DeviceMaster deviceMaster)
        {
            if (ModelState.IsValid)
            {
                db.DeviceMasters.Add(deviceMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceType = new SelectList(db.DeviceTypes, "DeviceTypeID", "DeviceTypeName", deviceMaster.DeviceType);
            ViewBag.Location_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName", deviceMaster.Location_PK);
            return View(deviceMaster);
        }

        // GET: ITAdministrator/DeviceMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceMaster deviceMaster = db.DeviceMasters.Find(id);
            if (deviceMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeviceType = new SelectList(db.DeviceTypes, "DeviceTypeID", "DeviceTypeName", deviceMaster.DeviceType);
            ViewBag.Location_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName", deviceMaster.Location_PK);
            return View(deviceMaster);
        }

        // POST: ITAdministrator/DeviceMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NodeID,MacID,DeviceName,IPAddress,Username,Password,Manufacturer,DeviceType,Location_PK,IsActive,Remark,SerialNumber,OSRef")] DeviceMaster deviceMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deviceMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceType = new SelectList(db.DeviceTypes, "DeviceTypeID", "DeviceTypeName", deviceMaster.DeviceType);
            ViewBag.Location_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName", deviceMaster.Location_PK);
            return View(deviceMaster);
        }

        // GET: ITAdministrator/DeviceMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceMaster deviceMaster = db.DeviceMasters.Find(id);
            if (deviceMaster == null)
            {
                return HttpNotFound();
            }
            return View(deviceMaster);
        }

        // POST: ITAdministrator/DeviceMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            DeviceMaster deviceMaster = db.DeviceMasters.Find(id);
            db.DeviceMasters.Remove(deviceMaster);
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
