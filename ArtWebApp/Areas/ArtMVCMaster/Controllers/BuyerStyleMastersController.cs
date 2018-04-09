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
    public class BuyerStyleMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/BuyerStyleMasters
        public ActionResult Index()
        {
            var buyerStyleMasters = db.BuyerStyleMasters.Include(b => b.BuyerMaster);
            return View(buyerStyleMasters.ToList());
        }

        // GET: ArtMVCMaster/BuyerStyleMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerStyleMaster buyerStyleMaster = db.BuyerStyleMasters.Find(id);
            if (buyerStyleMaster == null)
            {
                return HttpNotFound();
            }
            return View(buyerStyleMaster);
        }

        // GET: ArtMVCMaster/BuyerStyleMasters/Create
        public ActionResult Create()
        {
            ViewBag.BuyerID = new SelectList(db.BuyerMasters, "BuyerID", "BuyerName");
            return View();
        }

        // POST: ArtMVCMaster/BuyerStyleMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuyerStyleID,BuyerStyle,BuyerID")] BuyerStyleMaster buyerStyleMaster)
        {
            if (ModelState.IsValid)
            {
                db.BuyerStyleMasters.Add(buyerStyleMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuyerID = new SelectList(db.BuyerMasters, "BuyerID", "BuyerName", buyerStyleMaster.BuyerID);
            return View(buyerStyleMaster);
        }

        // GET: ArtMVCMaster/BuyerStyleMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerStyleMaster buyerStyleMaster = db.BuyerStyleMasters.Find(id);
            if (buyerStyleMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuyerID = new SelectList(db.BuyerMasters, "BuyerID", "BuyerName", buyerStyleMaster.BuyerID);
            return View(buyerStyleMaster);
        }

        // POST: ArtMVCMaster/BuyerStyleMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuyerStyleID,BuyerStyle,BuyerID")] BuyerStyleMaster buyerStyleMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buyerStyleMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuyerID = new SelectList(db.BuyerMasters, "BuyerID", "BuyerName", buyerStyleMaster.BuyerID);
            return View(buyerStyleMaster);
        }

        // GET: ArtMVCMaster/BuyerStyleMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerStyleMaster buyerStyleMaster = db.BuyerStyleMasters.Find(id);
            if (buyerStyleMaster == null)
            {
                return HttpNotFound();
            }
            return View(buyerStyleMaster);
        }

        // POST: ArtMVCMaster/BuyerStyleMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            BuyerStyleMaster buyerStyleMaster = db.BuyerStyleMasters.Find(id);
            db.BuyerStyleMasters.Remove(buyerStyleMaster);
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
