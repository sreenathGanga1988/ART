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
    public class SupplierTypemastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/SupplierTypemasters
        public ActionResult Index()
        {
            return View(db.SupplierTypemasters.ToList());
        }

        // GET: ArtMVCMaster/SupplierTypemasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierTypemaster supplierTypemaster = db.SupplierTypemasters.Find(id);
            if (supplierTypemaster == null)
            {
                return HttpNotFound();
            }
            return View(supplierTypemaster);
        }

        // GET: ArtMVCMaster/SupplierTypemasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMaster/SupplierTypemasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierTypeID,SupplierTypeName,TypeCode")] SupplierTypemaster supplierTypemaster)
        {
            if (ModelState.IsValid)
            {
                db.SupplierTypemasters.Add(supplierTypemaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplierTypemaster);
        }

        // GET: ArtMVCMaster/SupplierTypemasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierTypemaster supplierTypemaster = db.SupplierTypemasters.Find(id);
            if (supplierTypemaster == null)
            {
                return HttpNotFound();
            }
            return View(supplierTypemaster);
        }

        // POST: ArtMVCMaster/SupplierTypemasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierTypeID,SupplierTypeName,TypeCode")] SupplierTypemaster supplierTypemaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierTypemaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplierTypemaster);
        }

        // GET: ArtMVCMaster/SupplierTypemasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierTypemaster supplierTypemaster = db.SupplierTypemasters.Find(id);
            if (supplierTypemaster == null)
            {
                return HttpNotFound();
            }
            return View(supplierTypemaster);
        }

        // POST: ArtMVCMaster/SupplierTypemasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            SupplierTypemaster supplierTypemaster = db.SupplierTypemasters.Find(id);
            db.SupplierTypemasters.Remove(supplierTypemaster);
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
