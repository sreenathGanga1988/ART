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
    public class ItemGroupMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/ItemGroupMasters
        public ActionResult Index()
        {
            return View(db.ItemGroupMasters.ToList());
        }

        // GET: ArtMVCMaster/ItemGroupMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemGroupMaster itemGroupMaster = db.ItemGroupMasters.Find(id);
            if (itemGroupMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemGroupMaster);
        }

        // GET: ArtMVCMaster/ItemGroupMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMaster/ItemGroupMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemGroupID,ItemGroupName,ItemGroupDescription")] ItemGroupMaster itemGroupMaster)
        {
            if (ModelState.IsValid)
            {
                db.ItemGroupMasters.Add(itemGroupMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itemGroupMaster);
        }

        // GET: ArtMVCMaster/ItemGroupMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemGroupMaster itemGroupMaster = db.ItemGroupMasters.Find(id);
            if (itemGroupMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemGroupMaster);
        }

        // POST: ArtMVCMaster/ItemGroupMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemGroupID,ItemGroupName,ItemGroupDescription")] ItemGroupMaster itemGroupMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemGroupMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itemGroupMaster);
        }

        // GET: ArtMVCMaster/ItemGroupMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemGroupMaster itemGroupMaster = db.ItemGroupMasters.Find(id);
            if (itemGroupMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemGroupMaster);
        }

        // POST: ArtMVCMaster/ItemGroupMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ItemGroupMaster itemGroupMaster = db.ItemGroupMasters.Find(id);
            db.ItemGroupMasters.Remove(itemGroupMaster);
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
