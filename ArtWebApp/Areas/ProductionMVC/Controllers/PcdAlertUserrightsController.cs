using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.ProductionMVC.Controllers
{
    public class PcdAlertUserrightsController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ProductionMVC/PcdAlertUserrights
        public ActionResult Index()
        {
            var pcdAlertUserrights = db.PcdAlertUserrights.Include(p => p.LocationMaster).Include(p => p.UserMaster);
            return View(pcdAlertUserrights.ToList());
        }

        // GET: ProductionMVC/PcdAlertUserrights/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PcdAlertUserright pcdAlertUserright = db.PcdAlertUserrights.Find(id);
            if (pcdAlertUserright == null)
            {
                return HttpNotFound();
            }
            return View(pcdAlertUserright);
        }

        // GET: ProductionMVC/PcdAlertUserrights/Create
        public ActionResult Create()
        {
            ViewBag.Location_pk = new SelectList(db.LocationMasters, "Location_PK", "LocationName");
            ViewBag.User_pk = new SelectList(db.UserMasters, "User_PK", "UserName");
            return View();
        }

        // POST: ProductionMVC/PcdAlertUserrights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pcduser_pk,Is_Cut_Start_date,Is_Approval_status,Is_Sewing_Material_Issue,Is_BO_remarks_sewing,Is_Sewing_Bo_plan_accept,Is_Sewing_action,Is_Packing_Material_Issue,Is_BO_remarks_Packing,Is_Packing_bo_plan_accept,Is_Packing_action,User_pk,Location_pk")] PcdAlertUserright pcdAlertUserright)
        {
            if (ModelState.IsValid)
            {
                db.PcdAlertUserrights.Add(pcdAlertUserright);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Location_pk = new SelectList(db.LocationMasters, "Location_PK", "LocationName", pcdAlertUserright.Location_pk);
            ViewBag.User_pk = new SelectList(db.UserMasters, "User_PK", "UserName", pcdAlertUserright.User_pk);
            return View(pcdAlertUserright);
        }

        // GET: ProductionMVC/PcdAlertUserrights/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PcdAlertUserright pcdAlertUserright = db.PcdAlertUserrights.Find(id);
            if (pcdAlertUserright == null)
            {
                return HttpNotFound();
            }
            ViewBag.Location_pk = new SelectList(db.LocationMasters, "Location_PK", "LocationName", pcdAlertUserright.Location_pk);
            ViewBag.User_pk = new SelectList(db.UserMasters, "User_PK", "UserName", pcdAlertUserright.User_pk);
            return View(pcdAlertUserright);
        }

        // POST: ProductionMVC/PcdAlertUserrights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pcduser_pk,Is_Cut_Start_date,Is_Approval_status,Is_Sewing_Material_Issue,Is_BO_remarks_sewing,Is_Sewing_Bo_plan_accept,Is_Sewing_action,Is_Packing_Material_Issue,Is_BO_remarks_Packing,Is_Packing_bo_plan_accept,Is_Packing_action,User_pk,Location_pk")] PcdAlertUserright pcdAlertUserright)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pcdAlertUserright).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Location_pk = new SelectList(db.LocationMasters, "Location_PK", "LocationName", pcdAlertUserright.Location_pk);
            ViewBag.User_pk = new SelectList(db.UserMasters, "User_PK", "UserName", pcdAlertUserright.User_pk);
            return View(pcdAlertUserright);
        }

        // GET: ProductionMVC/PcdAlertUserrights/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PcdAlertUserright pcdAlertUserright = db.PcdAlertUserrights.Find(id);
            if (pcdAlertUserright == null)
            {
                return HttpNotFound();
            }
            return View(pcdAlertUserright);
        }

        // POST: ProductionMVC/PcdAlertUserrights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PcdAlertUserright pcdAlertUserright = db.PcdAlertUserrights.Find(id);
            db.PcdAlertUserrights.Remove(pcdAlertUserright);
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
