using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.ArtMVCGPO.Controllers
{
    public class MrnFileUploadsController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCGPO/MrnFileUploads
        public ActionResult Index()
        {
            return View(db.MrnFileUploads.ToList());
        }

        // GET: ArtMVCGPO/MrnFileUploads/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MrnFileUpload mrnFileUpload = db.MrnFileUploads.Find(id);
            if (mrnFileUpload == null)
            {
                return HttpNotFound();
            }
            return View(mrnFileUpload);
        }

        // GET: ArtMVCGPO/MrnFileUploads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCGPO/MrnFileUploads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FileUploadID,Mrn_PK,StringLength,MrnType,AddedBy,AddedDate")] MrnFileUpload mrnFileUpload)
        {
            if (ModelState.IsValid)
            {
                db.MrnFileUploads.Add(mrnFileUpload);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mrnFileUpload);
        }

        // GET: ArtMVCGPO/MrnFileUploads/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MrnFileUpload mrnFileUpload = db.MrnFileUploads.Find(id);
            if (mrnFileUpload == null)
            {
                return HttpNotFound();
            }
            return View(mrnFileUpload);
        }

        // POST: ArtMVCGPO/MrnFileUploads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FileUploadID,Mrn_PK,StringLength,MrnType,AddedBy,AddedDate")] MrnFileUpload mrnFileUpload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mrnFileUpload).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mrnFileUpload);
        }

        // GET: ArtMVCGPO/MrnFileUploads/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MrnFileUpload mrnFileUpload = db.MrnFileUploads.Find(id);
            if (mrnFileUpload == null)
            {
                return HttpNotFound();
            }
            return View(mrnFileUpload);
        }

        // POST: ArtMVCGPO/MrnFileUploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            MrnFileUpload mrnFileUpload = db.MrnFileUploads.Find(id);
            db.MrnFileUploads.Remove(mrnFileUpload);
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
