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
    public class CityMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/CityMasters
        public ActionResult Index()
        {
            var cityMasters = db.CityMasters.Include(c => c.CountryMaster);
            return View(cityMasters.ToList());
        }

        // GET: ArtMVCMaster/CityMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityMaster cityMaster = db.CityMasters.Find(id);
            if (cityMaster == null)
            {
                return HttpNotFound();
            }
            return View(cityMaster);
        }

        // GET: ArtMVCMaster/CityMasters/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.CountryMasters, "CountryID", "ShortName");
            return View();
        }

        // POST: ArtMVCMaster/CityMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CityID,CityName,CountryID")] CityMaster cityMaster)
        {
            if (ModelState.IsValid)
            {
                db.CityMasters.Add(cityMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.CountryMasters, "CountryID", "ShortName", cityMaster.CountryID);
            return View(cityMaster);
        }

        // GET: ArtMVCMaster/CityMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityMaster cityMaster = db.CityMasters.Find(id);
            if (cityMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.CountryMasters, "CountryID", "ShortName", cityMaster.CountryID);
            return View(cityMaster);
        }

        // POST: ArtMVCMaster/CityMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CityID,CityName,CountryID")] CityMaster cityMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cityMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.CountryMasters, "CountryID", "ShortName", cityMaster.CountryID);
            return View(cityMaster);
        }

        // GET: ArtMVCMaster/CityMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityMaster cityMaster = db.CityMasters.Find(id);
            if (cityMaster == null)
            {
                return HttpNotFound();
            }
            return View(cityMaster);
        }

        // POST: ArtMVCMaster/CityMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            CityMaster cityMaster = db.CityMasters.Find(id);
            db.CityMasters.Remove(cityMaster);
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
