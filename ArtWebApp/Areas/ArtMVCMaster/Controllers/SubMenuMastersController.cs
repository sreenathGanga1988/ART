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
    public class SubMenuMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/SubMenuMasters
        public ActionResult Index()
        {
            var subMenuMasters = db.SubMenuMasters.Include(s => s.SubMenuMaster2);
            return View(subMenuMasters.ToList());
        }

        // GET: ArtMVCMaster/SubMenuMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenuMaster subMenuMaster = db.SubMenuMasters.Find(id);
            if (subMenuMaster == null)
            {
                return HttpNotFound();
            }
            return View(subMenuMaster);
        }

        // GET: ArtMVCMaster/SubMenuMasters/Create
        public ActionResult Create()
        {
            ViewBag.ParentID = new SelectList(db.SubMenuMasters, "Menu_PK", "MenuText");
            return View();
        }

        // POST: ArtMVCMaster/SubMenuMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Menu_PK,MenuText,MenuURL,ParentID,isEnable,IsNormal,Description,MVCURL,IsActive")] SubMenuMaster subMenuMaster)
        {
            if (ModelState.IsValid)
            {
                db.SubMenuMasters.Add(subMenuMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentID = new SelectList(db.SubMenuMasters, "Menu_PK", "MenuText", subMenuMaster.ParentID);
            return View(subMenuMaster);
        }

        // GET: ArtMVCMaster/SubMenuMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenuMaster subMenuMaster = db.SubMenuMasters.Find(id);
            if (subMenuMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentID = new SelectList(db.SubMenuMasters, "Menu_PK", "MenuText", subMenuMaster.ParentID);
            return View(subMenuMaster);
        }

        // POST: ArtMVCMaster/SubMenuMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Menu_PK,MenuText,MenuURL,ParentID,isEnable,IsNormal,Description,MVCURL,IsActive")] SubMenuMaster subMenuMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subMenuMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentID = new SelectList(db.SubMenuMasters, "Menu_PK", "MenuText", subMenuMaster.ParentID);
            return View(subMenuMaster);
        }

        // GET: ArtMVCMaster/SubMenuMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenuMaster subMenuMaster = db.SubMenuMasters.Find(id);
            if (subMenuMaster == null)
            {
                return HttpNotFound();
            }
            return View(subMenuMaster);
        }

        // POST: ArtMVCMaster/SubMenuMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            SubMenuMaster subMenuMaster = db.SubMenuMasters.Find(id);
            db.SubMenuMasters.Remove(subMenuMaster);
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
