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
    public class UserProfileMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/UserProfileMasters
        public ActionResult Index()
        {
            return View(db.UserProfileMasters.ToList());
        }

        // GET: ArtMVCMaster/UserProfileMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfileMaster userProfileMaster = db.UserProfileMasters.Find(id);
            if (userProfileMaster == null)
            {
                return HttpNotFound();
            }
            return View(userProfileMaster);
        }

        // GET: ArtMVCMaster/UserProfileMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMaster/UserProfileMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserProfile_Pk,UserProfileName,UserProfileCode,Description,IsActive,isActiveProfile")] UserProfileMaster userProfileMaster)
        {
            if (ModelState.IsValid)
            {
                db.UserProfileMasters.Add(userProfileMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userProfileMaster);
        }

        // GET: ArtMVCMaster/UserProfileMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfileMaster userProfileMaster = db.UserProfileMasters.Find(id);
            if (userProfileMaster == null)
            {
                return HttpNotFound();
            }
            return View(userProfileMaster);
        }

        // POST: ArtMVCMaster/UserProfileMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserProfile_Pk,UserProfileName,UserProfileCode,Description,IsActive,isActiveProfile")] UserProfileMaster userProfileMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userProfileMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userProfileMaster);
        }

        // GET: ArtMVCMaster/UserProfileMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfileMaster userProfileMaster = db.UserProfileMasters.Find(id);
            if (userProfileMaster == null)
            {
                return HttpNotFound();
            }
            return View(userProfileMaster);
        }

        // POST: ArtMVCMaster/UserProfileMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            UserProfileMaster userProfileMaster = db.UserProfileMasters.Find(id);
            db.UserProfileMasters.Remove(userProfileMaster);
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
