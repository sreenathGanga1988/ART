using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.CuttingMVC.Controllers
{
    public class SubConExtraRequestsController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: CuttingMVC/SubConExtraRequests
        public ActionResult Index()
        {
           
            var subConExtraRequests = db.SubConExtraRequests.Include(s => s.AtcMaster).Include(s => s.CutOrderMaster).Include(s => s.ExtraRequestReasonMaster);

            return View(subConExtraRequests.ToList());
        }

        // GET: CuttingMVC/SubConExtraRequests/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubConExtraRequest subConExtraRequest = db.SubConExtraRequests.Find(id);
            if (subConExtraRequest == null)
            {
                return HttpNotFound();
            }
            return View(subConExtraRequest);
        }

        // GET: CuttingMVC/SubConExtraRequests/Create
        public ActionResult Create()
        {
            ViewBag.AtcID = new SelectList(db.AtcMasters, "AtcId", "AtcNum");
            ViewBag.CutOrderID = new SelectList(db.CutOrderMasters, "CutID", "Cut_NO");
            ViewBag.ExtraReason_Pk = new SelectList(db.ExtraRequestReasonMasters, "ExtraReason_Pk", "ExtraReason");
            return View();
        }

        // POST: CuttingMVC/SubConExtraRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExtraRequestID,AtcID,CutOrderID,RequestQty,IsApproved,AddedBy,AddedDate,ApprovedBy,ApprovedDate,IsDeleted,DeletedBy,ExtraReason_Pk")] SubConExtraRequest subConExtraRequest)
        {
            subConExtraRequest.AddedBy = HttpContext.Session["Username"].ToString();
            subConExtraRequest.IsApproved = "N";
           
            subConExtraRequest.AddedDate = DateTime.Now;
               subConExtraRequest.ApprovedBy = "";
            subConExtraRequest.ApprovedDate = null;
              subConExtraRequest.IsDeleted = "N";
            subConExtraRequest.DeletedBy = "";



            if (ModelState.IsValid)
            {
                db.SubConExtraRequests.Add(subConExtraRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AtcID = new SelectList(db.AtcMasters, "AtcId", "AtcNum", subConExtraRequest.AtcID);
            ViewBag.CutOrderID = new SelectList(db.CutOrderMasters, "CutID", "Cut_NO", subConExtraRequest.CutOrderID);
            return View(subConExtraRequest);
        }

        // GET: CuttingMVC/SubConExtraRequests/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubConExtraRequest subConExtraRequest = db.SubConExtraRequests.Find(id);
            if (subConExtraRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.AtcID = new SelectList(db.AtcMasters, "AtcId", "AtcNum", subConExtraRequest.AtcID);
            ViewBag.CutOrderID = new SelectList(db.CutOrderMasters, "CutID", "Cut_NO", subConExtraRequest.CutOrderID);
            return View(subConExtraRequest);
        }

        // POST: CuttingMVC/SubConExtraRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExtraRequestID,AtcID,CutOrderID,RequestQty,IsApproved,AddedBy,AddedDate,ApprovedBy,ApprovedDate,IsDeleted,DeletedBy")] SubConExtraRequest subConExtraRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subConExtraRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AtcID = new SelectList(db.AtcMasters, "AtcId", "AtcNum", subConExtraRequest.AtcID);
            ViewBag.CutOrderID = new SelectList(db.CutOrderMasters, "CutID", "Cut_NO", subConExtraRequest.CutOrderID);
            return View(subConExtraRequest);
        }

        // GET: CuttingMVC/SubConExtraRequests/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubConExtraRequest subConExtraRequest = db.SubConExtraRequests.Find(id);
            if (subConExtraRequest == null)
            {
                return HttpNotFound();
            }
            return View(subConExtraRequest);
        }

        // POST: CuttingMVC/SubConExtraRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            SubConExtraRequest subConExtraRequest = db.SubConExtraRequests.Find(id);
            db.SubConExtraRequests.Remove(subConExtraRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public JsonResult PopulateCutorder(int Id = 0)
        {


            SelectList cutorderlist = new SelectList(db.CutOrderMasters.Where(o => o.AtcID == Id), "CutID", "Cut_NO");


            JsonResult jsd = Json(cutorderlist, JsonRequestBehavior.AllowGet);

            return jsd;

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
