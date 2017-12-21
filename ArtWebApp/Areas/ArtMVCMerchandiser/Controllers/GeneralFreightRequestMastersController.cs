using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.ArtMVCMerchandiser.Controllers
{
    public class GeneralFreightRequestMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMerchandiser/GeneralFreightRequestMasters
        public ActionResult Index()
        {
            return View(db.GeneralFreightRequestMasters.ToList());
        }

        // GET: ArtMVCMerchandiser/GeneralFreightRequestMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralFreightRequestMaster generalFreightRequestMaster = db.GeneralFreightRequestMasters.Find(id);
            if (generalFreightRequestMaster == null)
            {
                return HttpNotFound();
            }
            return View(generalFreightRequestMaster);
        }

        // GET: ArtMVCMerchandiser/GeneralFreightRequestMasters/Create
        public ActionResult Create()
        {
            List<SelectListItem> ShipementType = new List<SelectListItem>();

            ShipementType.Add(new SelectListItem { Text = "Sea", Value = "Sea" });
            ShipementType.Add(new SelectListItem { Text = "Air", Value = "Air" });
            ShipementType.Add(new SelectListItem { Text = "Courier", Value = "Courier" });


            ViewBag.ShipementType = ShipementType;
            return View();
        }

        // POST: ArtMVCMerchandiser/GeneralFreightRequestMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GeneralFreightRequestID,GeneralFreightRequestNum,AddedBy,AddedDate,FromParty,ToParty,Shipper,Weight,ContentofPackage,DebitTo,Reason,Merchandiser,ForwarderDetails,ApproximateCharges,Remark,ApprovedBy,ApprovedDate,IsApproved,IsPosted,ShipementType")] GeneralFreightRequestMaster generalFreightRequestMaster)
        {
            if (ModelState.IsValid)
            {
                generalFreightRequestMaster.IsApproved = "N";
                generalFreightRequestMaster.IsPosted = "N";
                generalFreightRequestMaster.AddedBy = HttpContext.Session["Username"].ToString();
                generalFreightRequestMaster.AddedDate = DateTime.Now;
                db.GeneralFreightRequestMasters.Add(generalFreightRequestMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(generalFreightRequestMaster);
        }

        // GET: ArtMVCMerchandiser/GeneralFreightRequestMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralFreightRequestMaster generalFreightRequestMaster = db.GeneralFreightRequestMasters.Find(id);
            if (generalFreightRequestMaster == null)
            {
                return HttpNotFound();
            }
            return View(generalFreightRequestMaster);
        }

        // POST: ArtMVCMerchandiser/GeneralFreightRequestMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GeneralFreightRequestID,GeneralFreightRequestNum,AddedBy,AddedDate,FromParty,ToParty,Shipper,Weight,ContentofPackage,DebitTo,Reason,Merchandiser,ForwarderDetails,ApproximateCharges,Remark,ApprovedBy,ApprovedDate,IsApproved,IsPosted,ShipementType")] GeneralFreightRequestMaster generalFreightRequestMaster)
        {
            if (ModelState.IsValid)
            {
                generalFreightRequestMaster.IsApproved = "N";
                generalFreightRequestMaster.IsPosted = "N";
                generalFreightRequestMaster.AddedBy = HttpContext.Session["Username"].ToString();
                generalFreightRequestMaster.AddedDate = DateTime.Now;
                db.Entry(generalFreightRequestMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(generalFreightRequestMaster);
        }

        // GET: ArtMVCMerchandiser/GeneralFreightRequestMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralFreightRequestMaster generalFreightRequestMaster = db.GeneralFreightRequestMasters.Find(id);
            if (generalFreightRequestMaster == null)
            {
                return HttpNotFound();
            }
            return View(generalFreightRequestMaster);
        }

        // POST: ArtMVCMerchandiser/GeneralFreightRequestMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            GeneralFreightRequestMaster generalFreightRequestMaster = db.GeneralFreightRequestMasters.Find(id);
            db.GeneralFreightRequestMasters.Remove(generalFreightRequestMaster);
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
