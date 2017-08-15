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
    public class DepartmentMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/DepartmentMasters
        public ActionResult Index()
        {
            return View(db.DepartmentMasters.ToList());
        }

        // GET: ArtMVCMaster/DepartmentMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentMaster departmentMaster = db.DepartmentMasters.Find(id);
            if (departmentMaster == null)
            {
                return HttpNotFound();
            }
            return View(departmentMaster);
        }

        // GET: ArtMVCMaster/DepartmentMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMaster/DepartmentMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Deapartment_PK,DepartmentName")] DepartmentMaster departmentMaster)
        {
            if (ModelState.IsValid)
            {
                db.DepartmentMasters.Add(departmentMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departmentMaster);
        }

        // GET: ArtMVCMaster/DepartmentMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentMaster departmentMaster = db.DepartmentMasters.Find(id);
            if (departmentMaster == null)
            {
                return HttpNotFound();
            }
            return View(departmentMaster);
        }

        // POST: ArtMVCMaster/DepartmentMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Deapartment_PK,DepartmentName")] DepartmentMaster departmentMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departmentMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departmentMaster);
        }

        // GET: ArtMVCMaster/DepartmentMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentMaster departmentMaster = db.DepartmentMasters.Find(id);
            if (departmentMaster == null)
            {
                return HttpNotFound();
            }
            return View(departmentMaster);
        }

        // POST: ArtMVCMaster/DepartmentMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            DepartmentMaster departmentMaster = db.DepartmentMasters.Find(id);
            db.DepartmentMasters.Remove(departmentMaster);
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
