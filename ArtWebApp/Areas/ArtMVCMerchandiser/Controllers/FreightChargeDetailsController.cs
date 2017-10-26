using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.ArtMVCMerchandiser.Controllers
{
    public class FreightChargeDetailsController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMerchandiser/FreightChargeDetails
        public ActionResult Index()
        {
            var freightChargeDetails = db.FreightChargeDetails.Include(f => f.FreightRequestMaster);
            return View(freightChargeDetails.ToList());
        }

        // GET: ArtMVCMerchandiser/FreightChargeDetails/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreightChargeDetail freightChargeDetail = db.FreightChargeDetails.Find(id);
            if (freightChargeDetail == null)
            {
                return HttpNotFound();
            }
            return View(freightChargeDetail);
        }

        // GET: ArtMVCMerchandiser/FreightChargeDetails/Create
        public ActionResult Create()
        {
            ViewBag.FreightRequestID = new SelectList(db.FreightRequestMasters, "FreightRequestID", "FreightRequestNum");
            return View();
        }

        // POST: ArtMVCMerchandiser/FreightChargeDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FreightReqDetID,AtcID,FreightCharge,FreightRequestID")] FreightChargeDetail freightChargeDetail)
        {
            if (ModelState.IsValid)
            {
                db.FreightChargeDetails.Add(freightChargeDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FreightRequestID = new SelectList(db.FreightRequestMasters, "FreightRequestID", "FreightRequestNum", freightChargeDetail.FreightRequestID);
            return View(freightChargeDetail);
        }

        // GET: ArtMVCMerchandiser/FreightChargeDetails/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreightChargeDetail freightChargeDetail = db.FreightChargeDetails.Find(id);
            if (freightChargeDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.FreightRequestID = new SelectList(db.FreightRequestMasters, "FreightRequestID", "FreightRequestNum", freightChargeDetail.FreightRequestID);
            return View(freightChargeDetail);
        }

        // POST: ArtMVCMerchandiser/FreightChargeDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FreightReqDetID,AtcID,FreightCharge,FreightRequestID")] FreightChargeDetail freightChargeDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(freightChargeDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FreightRequestID = new SelectList(db.FreightRequestMasters, "FreightRequestID", "FreightRequestNum", freightChargeDetail.FreightRequestID);
            return View(freightChargeDetail);
        }

        // GET: ArtMVCMerchandiser/FreightChargeDetails/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreightChargeDetail freightChargeDetail = db.FreightChargeDetails.Find(id);
            if (freightChargeDetail == null)
            {
                return HttpNotFound();
            }
            return View(freightChargeDetail);
        }

        // POST: ArtMVCMerchandiser/FreightChargeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            FreightChargeDetail freightChargeDetail = db.FreightChargeDetails.Find(id);
            db.FreightChargeDetails.Remove(freightChargeDetail);
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
