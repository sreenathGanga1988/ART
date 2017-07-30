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
    public class CutTypeMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/CutTypeMasters
        public ActionResult Index()
        {
            return View(db.CutTypeMasters.ToList());
        }

        // GET: ArtMVCMaster/CutTypeMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CutTypeMaster cutTypeMaster = db.CutTypeMasters.Find(id);
            if (cutTypeMaster == null)
            {
                return HttpNotFound();
            }
            return View(cutTypeMaster);
        }

        // GET: ArtMVCMaster/CutTypeMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMaster/CutTypeMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CutTypeID,CutType,IsActive,AddedBy,AddedDate")] CutTypeMaster cutTypeMaster)
        {
            if (ModelState.IsValid)
            {
                db.CutTypeMasters.Add(cutTypeMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cutTypeMaster);
        }

        // GET: ArtMVCMaster/CutTypeMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CutTypeMaster cutTypeMaster = db.CutTypeMasters.Find(id);
            if (cutTypeMaster == null)
            {
                return HttpNotFound();
            }
            return View(cutTypeMaster);
        }

        // POST: ArtMVCMaster/CutTypeMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CutTypeID,CutType,IsActive,AddedBy,AddedDate")] CutTypeMaster cutTypeMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cutTypeMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cutTypeMaster);
        }

        // GET: ArtMVCMaster/CutTypeMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CutTypeMaster cutTypeMaster = db.CutTypeMasters.Find(id);
            if (cutTypeMaster == null)
            {
                return HttpNotFound();
            }
            return View(cutTypeMaster);
        }

        // POST: ArtMVCMaster/CutTypeMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            CutTypeMaster cutTypeMaster = db.CutTypeMasters.Find(id);
            db.CutTypeMasters.Remove(cutTypeMaster);
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
