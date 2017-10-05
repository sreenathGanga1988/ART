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
    public class FreightRequestMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMerchandiser/FreightRequestMasters
        public ActionResult Index()
        {
            return View(db.FreightRequestMasters.ToList());
        }

        // GET: ArtMVCMerchandiser/FreightRequestMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreightRequestMaster freightRequestMaster = db.FreightRequestMasters.Find(id);
            if (freightRequestMaster == null)
            {
                return HttpNotFound();
            }
            return View(freightRequestMaster);
        }

        // GET: ArtMVCMerchandiser/FreightRequestMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMerchandiser/FreightRequestMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FreightRequestID,FreightRequestNum,AddedBy,AddedDate,FromParty,ToParty,Shipper,Weight,ContentofPackage,DebitTo,Reason,Merchandiser,ForwarderDetails,ApproximateCharges,Remark,ApprovedBy,ApprovedDate,IsApproved,IsPosted")] FreightRequestMaster freightRequestMaster)
        {
            if (ModelState.IsValid)
            {
                db.FreightRequestMasters.Add(freightRequestMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(freightRequestMaster);
        }

        // GET: ArtMVCMerchandiser/FreightRequestMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreightRequestMaster freightRequestMaster = db.FreightRequestMasters.Find(id);
            if (freightRequestMaster == null)
            {
                return HttpNotFound();
            }
            return View(freightRequestMaster);
        }

        // POST: ArtMVCMerchandiser/FreightRequestMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FreightRequestID,FreightRequestNum,AddedBy,AddedDate,FromParty,ToParty,Shipper,Weight,ContentofPackage,DebitTo,Reason,Merchandiser,ForwarderDetails,ApproximateCharges,Remark,ApprovedBy,ApprovedDate,IsApproved,IsPosted")] FreightRequestMaster freightRequestMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(freightRequestMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(freightRequestMaster);
        }

        // GET: ArtMVCMerchandiser/FreightRequestMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreightRequestMaster freightRequestMaster = db.FreightRequestMasters.Find(id);
            if (freightRequestMaster == null)
            {
                return HttpNotFound();
            }
            return View(freightRequestMaster);
        }

        // POST: ArtMVCMerchandiser/FreightRequestMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            FreightRequestMaster freightRequestMaster = db.FreightRequestMasters.Find(id);
            db.FreightRequestMasters.Remove(freightRequestMaster);
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
