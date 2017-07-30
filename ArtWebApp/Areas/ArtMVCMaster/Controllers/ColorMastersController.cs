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
    public class ColorMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/ColorMasters
        public ActionResult Index()
        {
            return View(db.ColorMasters.ToList());
        }

        // GET: ArtMVCMaster/ColorMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorMaster colorMaster = db.ColorMasters.Find(id);
            if (colorMaster == null)
            {
                return HttpNotFound();
            }
            return View(colorMaster);
        }

        // GET: ArtMVCMaster/ColorMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMaster/ColorMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ColorId,ColorCode,ColorName")] ColorMaster colorMaster)
        {
            if (ModelState.IsValid)
            {
                db.ColorMasters.Add(colorMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(colorMaster);
        }

        // GET: ArtMVCMaster/ColorMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorMaster colorMaster = db.ColorMasters.Find(id);
            if (colorMaster == null)
            {
                return HttpNotFound();
            }
            return View(colorMaster);
        }

        // POST: ArtMVCMaster/ColorMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ColorId,ColorCode,ColorName")] ColorMaster colorMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colorMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(colorMaster);
        }

        // GET: ArtMVCMaster/ColorMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorMaster colorMaster = db.ColorMasters.Find(id);
            if (colorMaster == null)
            {
                return HttpNotFound();
            }
            return View(colorMaster);
        }

        // POST: ArtMVCMaster/ColorMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ColorMaster colorMaster = db.ColorMasters.Find(id);
            db.ColorMasters.Remove(colorMaster);
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
