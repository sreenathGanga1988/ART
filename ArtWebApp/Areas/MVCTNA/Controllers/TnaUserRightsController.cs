using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.MVCTNA
{
    public class TnaUserRightsController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: MVCTNA/TnaUserRights
        public ActionResult Index()
        {
            var tnaUserRights = db.TnaUserRights.Include(t => t.UserMaster);
            return View(tnaUserRights.ToList());
        }

        // GET: MVCTNA/TnaUserRights/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TnaUserRight tnaUserRight = db.TnaUserRights.Find(id);
            if (tnaUserRight == null)
            {
                return HttpNotFound();
            }
            return View(tnaUserRight);
        }

        // GET: MVCTNA/TnaUserRights/Create
        public ActionResult Create()
        {
            ViewBag.User_PK = new SelectList(db.UserMasters, "User_PK", "UserName");
            return View();
        }

        // POST: MVCTNA/TnaUserRights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TNAUserRightID,IsPPSumbissionDate,IsPPApprovalDate,IsSampleYardagesDate,IsGradedPatternDate,IsBulkFabricDate,IsSewingTrimDate,IsSizeSetDate,IsPPMeetingDate,IsFC1Date,IsFinalMarkerDate,IsFactoryPlannedPCDDate,IsMerchandisingPCDDate,IsInputDate,IsPackingTrimDate,User_PK,IsSystemFile,IsShrinkage,IsSystemFile")] TnaUserRight tnaUserRight)
        {
            if (ModelState.IsValid)
            {
                db.TnaUserRights.Add(tnaUserRight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_PK = new SelectList(db.UserMasters, "User_PK", "UserName", tnaUserRight.User_PK);
            return View(tnaUserRight);
        }

        // GET: MVCTNA/TnaUserRights/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TnaUserRight tnaUserRight = db.TnaUserRights.Find(id);
            if (tnaUserRight == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_PK = new SelectList(db.UserMasters, "User_PK", "UserName", tnaUserRight.User_PK);
            return View(tnaUserRight);
        }

        // POST: MVCTNA/TnaUserRights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TNAUserRightID,IsPPSumbissionDate,IsPPApprovalDate,IsSampleYardagesDate,IsGradedPatternDate,IsBulkFabricDate,IsSewingTrimDate,IsSizeSetDate,IsPPMeetingDate,IsFC1Date,IsFinalMarkerDate,IsFactoryPlannedPCDDate,IsMerchandisingPCDDate,IsInputDate,IsPackingTrimDate,User_PK,IsSystemFile")] TnaUserRight tnaUserRight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tnaUserRight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_PK = new SelectList(db.UserMasters, "User_PK", "UserName", tnaUserRight.User_PK);
            return View(tnaUserRight);
        }

        // GET: MVCTNA/TnaUserRights/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TnaUserRight tnaUserRight = db.TnaUserRights.Find(id);
            if (tnaUserRight == null)
            {
                return HttpNotFound();
            }
            return View(tnaUserRight);
        }

        // POST: MVCTNA/TnaUserRights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TnaUserRight tnaUserRight = db.TnaUserRights.Find(id);
            db.TnaUserRights.Remove(tnaUserRight);
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
