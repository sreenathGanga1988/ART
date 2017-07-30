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
    public class GarmentCategoriesController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/GarmentCategories
        public ActionResult Index()
        {
            return View(db.GarmentCategories.ToList());
        }

        // GET: ArtMVCMaster/GarmentCategories/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GarmentCategory garmentCategory = db.GarmentCategories.Find(id);
            if (garmentCategory == null)
            {
                return HttpNotFound();
            }
            return View(garmentCategory);
        }

        // GET: ArtMVCMaster/GarmentCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMaster/GarmentCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName")] GarmentCategory garmentCategory)
        {
            if (ModelState.IsValid)
            {
                db.GarmentCategories.Add(garmentCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(garmentCategory);
        }

        // GET: ArtMVCMaster/GarmentCategories/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GarmentCategory garmentCategory = db.GarmentCategories.Find(id);
            if (garmentCategory == null)
            {
                return HttpNotFound();
            }
            return View(garmentCategory);
        }

        // POST: ArtMVCMaster/GarmentCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName")] GarmentCategory garmentCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(garmentCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(garmentCategory);
        }

        // GET: ArtMVCMaster/GarmentCategories/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GarmentCategory garmentCategory = db.GarmentCategories.Find(id);
            if (garmentCategory == null)
            {
                return HttpNotFound();
            }
            return View(garmentCategory);
        }

        // POST: ArtMVCMaster/GarmentCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            GarmentCategory garmentCategory = db.GarmentCategories.Find(id);
            db.GarmentCategories.Remove(garmentCategory);
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
