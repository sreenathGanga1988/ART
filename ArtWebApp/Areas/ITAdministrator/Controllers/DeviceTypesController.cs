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
    public class DeviceTypesController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ITAdministrator/DeviceTypes
        public ActionResult Index()
        {
            return View(db.DeviceTypes.ToList());
        }

        // GET: ITAdministrator/DeviceTypes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceType deviceType = db.DeviceTypes.Find(id);
            if (deviceType == null)
            {
                return HttpNotFound();
            }
            return View(deviceType);
        }

        // GET: ITAdministrator/DeviceTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ITAdministrator/DeviceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeviceTypeID,DeviceTypeName,IsActive")] DeviceType deviceType)
        {
            if (ModelState.IsValid)
            {
                db.DeviceTypes.Add(deviceType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deviceType);
        }

        // GET: ITAdministrator/DeviceTypes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceType deviceType = db.DeviceTypes.Find(id);
            if (deviceType == null)
            {
                return HttpNotFound();
            }
            return View(deviceType);
        }

        // POST: ITAdministrator/DeviceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceTypeID,DeviceTypeName,IsActive")] DeviceType deviceType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deviceType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deviceType);
        }

        // GET: ITAdministrator/DeviceTypes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceType deviceType = db.DeviceTypes.Find(id);
            if (deviceType == null)
            {
                return HttpNotFound();
            }
            return View(deviceType);
        }

        // POST: ITAdministrator/DeviceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            DeviceType deviceType = db.DeviceTypes.Find(id);
            db.DeviceTypes.Remove(deviceType);
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
