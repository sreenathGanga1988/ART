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
    public class SizeMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/SizeMasters
        public ActionResult Index()
        {
            return View(db.SizeMasters.ToList());
        }

        // GET: ArtMVCMaster/SizeMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeMaster sizeMaster = db.SizeMasters.Find(id);
            if (sizeMaster == null)
            {
                return HttpNotFound();
            }
            return View(sizeMaster);
        }

        // GET: ArtMVCMaster/SizeMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMaster/SizeMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SizeID,SizeCode,SizeName")] SizeMaster sizeMaster)
        {
            if (ModelState.IsValid)
            {
                db.SizeMasters.Add(sizeMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sizeMaster);
        }

        // GET: ArtMVCMaster/SizeMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeMaster sizeMaster = db.SizeMasters.Find(id);
            if (sizeMaster == null)
            {
                return HttpNotFound();
            }
            return View(sizeMaster);
        }

        // POST: ArtMVCMaster/SizeMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SizeID,SizeCode,SizeName")] SizeMaster sizeMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sizeMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sizeMaster);
        }

        // GET: ArtMVCMaster/SizeMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeMaster sizeMaster = db.SizeMasters.Find(id);
            if (sizeMaster == null)
            {
                return HttpNotFound();
            }
            return View(sizeMaster);
        }

        // POST: ArtMVCMaster/SizeMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            SizeMaster sizeMaster = db.SizeMasters.Find(id);
            db.SizeMasters.Remove(sizeMaster);
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
