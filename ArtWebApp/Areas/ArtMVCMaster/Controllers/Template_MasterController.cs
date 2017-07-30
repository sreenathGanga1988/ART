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
    public class Template_MasterController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/Template_Master
        public ActionResult Index()
        {
            var template_Master = db.Template_Master.Include(t => t.ItemGroupMaster).Include(t => t.UOMMaster);
            return View(template_Master.ToList());
        }

        // GET: ArtMVCMaster/Template_Master/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template_Master template_Master = db.Template_Master.Find(id);
            if (template_Master == null)
            {
                return HttpNotFound();
            }
            return View(template_Master);
        }

        // GET: ArtMVCMaster/Template_Master/Create
        public ActionResult Create()
        {
            ViewBag.ItemGroup_PK = new SelectList(db.ItemGroupMasters, "ItemGroupID", "ItemGroupName");
            ViewBag.Uom_PK = new SelectList(db.UOMMasters, "Uom_PK", "UomCode");
            return View();
        }

        // POST: ArtMVCMaster/Template_Master/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Template_PK,TemplateCode,Description,ItemGroup_PK,HCCode,Uom_PK,Wastage,IsItemColor,IsItemSize,IsStock,IsEnabled")] Template_Master template_Master)
        {
            if (ModelState.IsValid)
            {
                template_Master.AddedBy = Session["Username"].ToString();
                template_Master.AddedDate = DateTime.Now;
                db.Template_Master.Add(template_Master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemGroup_PK = new SelectList(db.ItemGroupMasters, "ItemGroupID", "ItemGroupName", template_Master.ItemGroup_PK);
            ViewBag.Uom_PK = new SelectList(db.UOMMasters, "Uom_PK", "UomCode", template_Master.Uom_PK);
            return View(template_Master);
        }

        // GET: ArtMVCMaster/Template_Master/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template_Master template_Master = db.Template_Master.Find(id);
            if (template_Master == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemGroup_PK = new SelectList(db.ItemGroupMasters, "ItemGroupID", "ItemGroupName", template_Master.ItemGroup_PK);
            ViewBag.Uom_PK = new SelectList(db.UOMMasters, "Uom_PK", "UomCode", template_Master.Uom_PK);
            return View(template_Master);
        }

        // POST: ArtMVCMaster/Template_Master/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Template_PK,TemplateCode,Description,ItemGroup_PK,HCCode,Uom_PK,Wastage,IsItemColor,IsItemSize,IsStock,IsEnabled")] Template_Master template_Master)
        {
            if (ModelState.IsValid)
            {
                template_Master.AddedBy = Session["Username"].ToString();
                template_Master.AddedDate = DateTime.Now;
                db.Entry(template_Master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemGroup_PK = new SelectList(db.ItemGroupMasters, "ItemGroupID", "ItemGroupName", template_Master.ItemGroup_PK);
            ViewBag.Uom_PK = new SelectList(db.UOMMasters, "Uom_PK", "UomCode", template_Master.Uom_PK);
            return View(template_Master);
        }

        // GET: ArtMVCMaster/Template_Master/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template_Master template_Master = db.Template_Master.Find(id);
            if (template_Master == null)
            {
                return HttpNotFound();
            }
            return View(template_Master);
        }

        // POST: ArtMVCMaster/Template_Master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Template_Master template_Master = db.Template_Master.Find(id);
            db.Template_Master.Remove(template_Master);
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
