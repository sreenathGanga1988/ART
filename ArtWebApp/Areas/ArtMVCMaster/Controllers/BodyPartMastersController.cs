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
    public class BodyPartMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/BodyPartMasters
        public ActionResult Index()
        {
            return View(db.BodyPartMasters.ToList());
        }

        // GET: ArtMVCMaster/BodyPartMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BodyPartMaster bodyPartMaster = db.BodyPartMasters.Find(id);
            if (bodyPartMaster == null)
            {
                return HttpNotFound();
            }
            return View(bodyPartMaster);
        }

        // GET: ArtMVCMaster/BodyPartMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMaster/BodyPartMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BodyPart_PK,BodyPartName,IsActive")] BodyPartMaster bodyPartMaster)
        {
            if (ModelState.IsValid)
            {
                db.BodyPartMasters.Add(bodyPartMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bodyPartMaster);
        }

        // GET: ArtMVCMaster/BodyPartMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BodyPartMaster bodyPartMaster = db.BodyPartMasters.Find(id);
            if (bodyPartMaster == null)
            {
                return HttpNotFound();
            }
            return View(bodyPartMaster);
        }

        // POST: ArtMVCMaster/BodyPartMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BodyPart_PK,BodyPartName,IsActive")] BodyPartMaster bodyPartMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bodyPartMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bodyPartMaster);
        }

        // GET: ArtMVCMaster/BodyPartMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BodyPartMaster bodyPartMaster = db.BodyPartMasters.Find(id);
            if (bodyPartMaster == null)
            {
                return HttpNotFound();
            }
            return View(bodyPartMaster);
        }

        // POST: ArtMVCMaster/BodyPartMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            BodyPartMaster bodyPartMaster = db.BodyPartMasters.Find(id);
            db.BodyPartMasters.Remove(bodyPartMaster);
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
