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
    public class BuyerMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/BuyerMasters
        public ActionResult Index()
        {
            var buyerMasters = db.BuyerMasters.Include(b => b.CountryMaster).Include(b => b.CurrencyMaster).Include(b => b.PaymentModeMaster);
            return View(buyerMasters.ToList());
        }

        // GET: ArtMVCMaster/BuyerMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerMaster buyerMaster = db.BuyerMasters.Find(id);
            if (buyerMaster == null)
            {
                return HttpNotFound();
            }
            return View(buyerMaster);
        }

        // GET: ArtMVCMaster/BuyerMasters/Create
        public ActionResult Create()
        {
            ViewBag.CountryCode = new SelectList(db.CountryMasters, "CountryID", "ShortName");
            ViewBag.CurrencyCode = new SelectList(db.CurrencyMasters, "CurrencyID", "CurrencyCode");
            ViewBag.PaymentModeCode = new SelectList(db.PaymentModeMasters, "PaymentModeID", "PaymentModeCode");
            return View();
        }

        // POST: ArtMVCMaster/BuyerMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuyerID,BuyerName,Prefix,Adress,Telephone,Fax,Email,ContactPerson,CountryCode,CurrencyCode,PaymentModeCode,Department,Agent,Allowance,AccountCode,Debitaccid,Br,partner_id,IsActive")] BuyerMaster buyerMaster)
        {
            if (ModelState.IsValid)
            {
                db.BuyerMasters.Add(buyerMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryCode = new SelectList(db.CountryMasters, "CountryID", "ShortName", buyerMaster.CountryCode);
            ViewBag.CurrencyCode = new SelectList(db.CurrencyMasters, "CurrencyID", "CurrencyCode", buyerMaster.CurrencyCode);
            ViewBag.PaymentModeCode = new SelectList(db.PaymentModeMasters, "PaymentModeID", "PaymentModeCode", buyerMaster.PaymentModeCode);
            return View(buyerMaster);
        }

        // GET: ArtMVCMaster/BuyerMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerMaster buyerMaster = db.BuyerMasters.Find(id);
            if (buyerMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryCode = new SelectList(db.CountryMasters, "CountryID", "ShortName", buyerMaster.CountryCode);
            ViewBag.CurrencyCode = new SelectList(db.CurrencyMasters, "CurrencyID", "CurrencyCode", buyerMaster.CurrencyCode);
            ViewBag.PaymentModeCode = new SelectList(db.PaymentModeMasters, "PaymentModeID", "PaymentModeCode", buyerMaster.PaymentModeCode);
            return View(buyerMaster);
        }

        // POST: ArtMVCMaster/BuyerMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuyerID,BuyerName,Prefix,Adress,Telephone,Fax,Email,ContactPerson,CountryCode,CurrencyCode,PaymentModeCode,Department,Agent,Allowance,AccountCode,Debitaccid,Br,partner_id,IsActive")] BuyerMaster buyerMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buyerMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryCode = new SelectList(db.CountryMasters, "CountryID", "ShortName", buyerMaster.CountryCode);
            ViewBag.CurrencyCode = new SelectList(db.CurrencyMasters, "CurrencyID", "CurrencyCode", buyerMaster.CurrencyCode);
            ViewBag.PaymentModeCode = new SelectList(db.PaymentModeMasters, "PaymentModeID", "PaymentModeCode", buyerMaster.PaymentModeCode);
            return View(buyerMaster);
        }

        // GET: ArtMVCMaster/BuyerMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerMaster buyerMaster = db.BuyerMasters.Find(id);
            if (buyerMaster == null)
            {
                return HttpNotFound();
            }
            return View(buyerMaster);
        }

        // POST: ArtMVCMaster/BuyerMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            BuyerMaster buyerMaster = db.BuyerMasters.Find(id);
            db.BuyerMasters.Remove(buyerMaster);
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
