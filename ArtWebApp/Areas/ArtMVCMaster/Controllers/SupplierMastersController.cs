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
    public class SupplierMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/SupplierMasters
        public ActionResult Index()
        {
            var supplierMasters = db.SupplierMasters.Include(s => s.CountryMaster).Include(s => s.CurrencyMaster).Include(s => s.PaymentModeMaster).Include(s => s.PaymentTermMaster);
            return View(supplierMasters.ToList());
        }

        // GET: ArtMVCMaster/SupplierMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierMaster supplierMaster = db.SupplierMasters.Find(id);
            if (supplierMaster == null)
            {
                return HttpNotFound();
            }
            return View(supplierMaster);
        }

        // GET: ArtMVCMaster/SupplierMasters/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.CountryMasters, "CountryID", "ShortName");
            ViewBag.CurrencyID = new SelectList(db.CurrencyMasters, "CurrencyID", "CurrencyCode");
            ViewBag.PaymentModeID = new SelectList(db.PaymentModeMasters, "PaymentModeID", "PaymentModeCode");
            ViewBag.PaymentTermID = new SelectList(db.PaymentTermMasters, "PaymentTermID", "PaymentTermCode");
            return View();
        }

        // POST: ArtMVCMaster/SupplierMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Supplier_PK,SupplierName,SupplierPrefix,SupplierAddress,SupplierType,Telephone,Email,Fax,ContactPerson,CurrencyID,CountryID,PaymentModeID,PaymentTermID,IsActive,partner_id,IsPogiven,AddedBY,AddedDate")] SupplierMaster supplierMaster)
        {
            if (ModelState.IsValid)
            {
                db.SupplierMasters.Add(supplierMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.CountryMasters, "CountryID", "ShortName", supplierMaster.CountryID);
            ViewBag.CurrencyID = new SelectList(db.CurrencyMasters, "CurrencyID", "CurrencyCode", supplierMaster.CurrencyID);
            ViewBag.PaymentModeID = new SelectList(db.PaymentModeMasters, "PaymentModeID", "PaymentModeCode", supplierMaster.PaymentModeID);
            ViewBag.PaymentTermID = new SelectList(db.PaymentTermMasters, "PaymentTermID", "PaymentTermCode", supplierMaster.PaymentTermID);
            return View(supplierMaster);
        }

        // GET: ArtMVCMaster/SupplierMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierMaster supplierMaster = db.SupplierMasters.Find(id);
            if (supplierMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.CountryMasters, "CountryID", "ShortName", supplierMaster.CountryID);
            ViewBag.CurrencyID = new SelectList(db.CurrencyMasters, "CurrencyID", "CurrencyCode", supplierMaster.CurrencyID);
            ViewBag.PaymentModeID = new SelectList(db.PaymentModeMasters, "PaymentModeID", "PaymentModeCode", supplierMaster.PaymentModeID);
            ViewBag.PaymentTermID = new SelectList(db.PaymentTermMasters, "PaymentTermID", "PaymentTermCode", supplierMaster.PaymentTermID);
            return View(supplierMaster);
        }

        // POST: ArtMVCMaster/SupplierMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Supplier_PK,SupplierName,SupplierPrefix,SupplierAddress,SupplierType,Telephone,Email,Fax,ContactPerson,CurrencyID,CountryID,PaymentModeID,PaymentTermID,IsActive,partner_id,IsPogiven,AddedBY,AddedDate")] SupplierMaster supplierMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.CountryMasters, "CountryID", "ShortName", supplierMaster.CountryID);
            ViewBag.CurrencyID = new SelectList(db.CurrencyMasters, "CurrencyID", "CurrencyCode", supplierMaster.CurrencyID);
            ViewBag.PaymentModeID = new SelectList(db.PaymentModeMasters, "PaymentModeID", "PaymentModeCode", supplierMaster.PaymentModeID);
            ViewBag.PaymentTermID = new SelectList(db.PaymentTermMasters, "PaymentTermID", "PaymentTermCode", supplierMaster.PaymentTermID);
            return View(supplierMaster);
        }

        // GET: ArtMVCMaster/SupplierMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierMaster supplierMaster = db.SupplierMasters.Find(id);
            if (supplierMaster == null)
            {
                return HttpNotFound();
            }
            return View(supplierMaster);
        }

        // POST: ArtMVCMaster/SupplierMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            SupplierMaster supplierMaster = db.SupplierMasters.Find(id);
            db.SupplierMasters.Remove(supplierMaster);
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
