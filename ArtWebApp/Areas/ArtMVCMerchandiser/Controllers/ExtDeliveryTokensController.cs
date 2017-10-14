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
    public class ExtDeliveryTokensController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMerchandiser/ExtDeliveryTokens
        public ActionResult Index()
        {
            var extDeliveryTokens = db.ExtDeliveryTokens.Include(e => e.AtcMaster).Include(e => e.LocationMaster).Include(e => e.LocationMaster1).Include(e => e.SkuRawmaterialDetail).Include(e => e.ItemGroupMaster);
            return View(extDeliveryTokens.ToList());
        }

        // GET: ArtMVCMerchandiser/ExtDeliveryTokens/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtDeliveryToken extDeliveryToken = db.ExtDeliveryTokens.Find(id);
            if (extDeliveryToken == null)
            {
                return HttpNotFound();
            }
            return View(extDeliveryToken);
        }

        // GET: ArtMVCMerchandiser/ExtDeliveryTokens/Create
        public ActionResult Create()
        {
            ViewBag.AtcID = new SelectList(db.AtcMasters, "AtcId", "AtcNum");
            ViewBag.Fromlocation = new SelectList(db.LocationMasters, "Location_PK", "LocationName");
            ViewBag.ToLocation = new SelectList(db.LocationMasters, "Location_PK", "LocationName");
            ViewBag.SkuDet_PK = new SelectList(db.SkuRawmaterialDetails, "SkuDet_PK", "ColorCode");
            ViewBag.Itemgroup = new SelectList(db.ItemGroupMasters, "ItemGroupID", "ItemGroupName");
            return View();
        }

        // POST: ArtMVCMerchandiser/ExtDeliveryTokens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExtDeliveryTokenID,SkuDet_PK,ItemName,Fromlocation,ToLocation,Qty,AddedBY,AddedDate,DeliveredQty,BalanceToDeliver,IsApproved,ApprovedDate,ApprovedBy,Isdeleted,DeletedDate,DeletedBy,AtcID,Itemgroup,SKU")] ExtDeliveryToken extDeliveryToken)
        {
            if (ModelState.IsValid)
            {
                db.ExtDeliveryTokens.Add(extDeliveryToken);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AtcID = new SelectList(db.AtcMasters, "AtcId", "AtcNum", extDeliveryToken.AtcID);
            ViewBag.Fromlocation = new SelectList(db.LocationMasters, "Location_PK", "LocationName", extDeliveryToken.Fromlocation);
            ViewBag.ToLocation = new SelectList(db.LocationMasters, "Location_PK", "LocationName", extDeliveryToken.ToLocation);
            ViewBag.SkuDet_PK = new SelectList(db.SkuRawmaterialDetails, "SkuDet_PK", "ColorCode", extDeliveryToken.SkuDet_PK);
            ViewBag.Itemgroup = new SelectList(db.ItemGroupMasters, "ItemGroupID", "ItemGroupName", extDeliveryToken.Itemgroup);
            return View(extDeliveryToken);
        }

        // GET: ArtMVCMerchandiser/ExtDeliveryTokens/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtDeliveryToken extDeliveryToken = db.ExtDeliveryTokens.Find(id);
            if (extDeliveryToken == null)
            {
                return HttpNotFound();
            }
            ViewBag.AtcID = new SelectList(db.AtcMasters, "AtcId", "AtcNum", extDeliveryToken.AtcID);
            ViewBag.Fromlocation = new SelectList(db.LocationMasters, "Location_PK", "LocationName", extDeliveryToken.Fromlocation);
            ViewBag.ToLocation = new SelectList(db.LocationMasters, "Location_PK", "LocationName", extDeliveryToken.ToLocation);
            ViewBag.SkuDet_PK = new SelectList(db.SkuRawmaterialDetails, "SkuDet_PK", "ColorCode", extDeliveryToken.SkuDet_PK);
            ViewBag.Itemgroup = new SelectList(db.ItemGroupMasters, "ItemGroupID", "ItemGroupName", extDeliveryToken.Itemgroup);
            return View(extDeliveryToken);
        }

        // POST: ArtMVCMerchandiser/ExtDeliveryTokens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExtDeliveryTokenID,SkuDet_PK,ItemName,Fromlocation,ToLocation,Qty,AddedBY,AddedDate,DeliveredQty,BalanceToDeliver,IsApproved,ApprovedDate,ApprovedBy,Isdeleted,DeletedDate,DeletedBy,AtcID,Itemgroup,SKU")] ExtDeliveryToken extDeliveryToken)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extDeliveryToken).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AtcID = new SelectList(db.AtcMasters, "AtcId", "AtcNum", extDeliveryToken.AtcID);
            ViewBag.Fromlocation = new SelectList(db.LocationMasters, "Location_PK", "LocationName", extDeliveryToken.Fromlocation);
            ViewBag.ToLocation = new SelectList(db.LocationMasters, "Location_PK", "LocationName", extDeliveryToken.ToLocation);
            ViewBag.SkuDet_PK = new SelectList(db.SkuRawmaterialDetails, "SkuDet_PK", "ColorCode", extDeliveryToken.SkuDet_PK);
            ViewBag.Itemgroup = new SelectList(db.ItemGroupMasters, "ItemGroupID", "ItemGroupName", extDeliveryToken.Itemgroup);
            return View(extDeliveryToken);
        }

        // GET: ArtMVCMerchandiser/ExtDeliveryTokens/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtDeliveryToken extDeliveryToken = db.ExtDeliveryTokens.Find(id);
            if (extDeliveryToken == null)
            {
                return HttpNotFound();
            }
            return View(extDeliveryToken);
        }

        // POST: ArtMVCMerchandiser/ExtDeliveryTokens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ExtDeliveryToken extDeliveryToken = db.ExtDeliveryTokens.Find(id);
            db.ExtDeliveryTokens.Remove(extDeliveryToken);
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
