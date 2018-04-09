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
    public class BuyerDestinationMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/BuyerDestinationMasters
        public ActionResult Index()
        {
            var buyerDestinationMasters = db.BuyerDestinationMasters.Include(b => b.BuyerMaster);
            return View(buyerDestinationMasters.ToList());
        }

        // GET: ArtMVCMaster/BuyerDestinationMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerDestinationMaster buyerDestinationMaster = db.BuyerDestinationMasters.Find(id);
            if (buyerDestinationMaster == null)
            {
                return HttpNotFound();
            }
            return View(buyerDestinationMaster);
        }

        // GET: ArtMVCMaster/BuyerDestinationMasters/Create
        public ActionResult Create()
        {
            ViewBag.BuyerID = new SelectList(db.BuyerMasters, "BuyerID", "BuyerName");
            return View();
        }

        // POST: ArtMVCMaster/BuyerDestinationMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuyerDestination_PK,BuyerDestination,BuyerID")] BuyerDestinationMaster buyerDestinationMaster)
        {
            if (ModelState.IsValid)
            {
                db.BuyerDestinationMasters.Add(buyerDestinationMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuyerID = new SelectList(db.BuyerMasters, "BuyerID", "BuyerName", buyerDestinationMaster.BuyerID);
            return View(buyerDestinationMaster);
        }

        // GET: ArtMVCMaster/BuyerDestinationMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerDestinationMaster buyerDestinationMaster = db.BuyerDestinationMasters.Find(id);
            if (buyerDestinationMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuyerID = new SelectList(db.BuyerMasters, "BuyerID", "BuyerName", buyerDestinationMaster.BuyerID);
            return View(buyerDestinationMaster);
        }

        // POST: ArtMVCMaster/BuyerDestinationMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuyerDestination_PK,BuyerDestination,BuyerID")] BuyerDestinationMaster buyerDestinationMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buyerDestinationMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuyerID = new SelectList(db.BuyerMasters, "BuyerID", "BuyerName", buyerDestinationMaster.BuyerID);
            return View(buyerDestinationMaster);
        }

        // GET: ArtMVCMaster/BuyerDestinationMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerDestinationMaster buyerDestinationMaster = db.BuyerDestinationMasters.Find(id);
            if (buyerDestinationMaster == null)
            {
                return HttpNotFound();
            }
            return View(buyerDestinationMaster);
        }

        // POST: ArtMVCMaster/BuyerDestinationMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            BuyerDestinationMaster buyerDestinationMaster = db.BuyerDestinationMasters.Find(id);
            db.BuyerDestinationMasters.Remove(buyerDestinationMaster);
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
