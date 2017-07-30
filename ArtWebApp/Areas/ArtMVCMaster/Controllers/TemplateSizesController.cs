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
    public class TemplateSizesController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/TemplateSizes
        public ActionResult Index()
        {
            var templateSizes = db.TemplateSizes.Include(t => t.Template_Master);
            return View(templateSizes.ToList());
        }

        // GET: ArtMVCMaster/TemplateSizes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateSize templateSize = db.TemplateSizes.Find(id);
            if (templateSize == null)
            {
                return HttpNotFound();
            }
            return View(templateSize);
        }

        // GET: ArtMVCMaster/TemplateSizes/Create
        public ActionResult Create()
        {
            ViewBag.Template_PK = new SelectList(db.Template_Master, "Template_PK", "TemplateCode");
            return View();
        }

        // POST: ArtMVCMaster/TemplateSizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TemplateSize_PK,Template_PK,TemplateSize1")] TemplateSize templateSize)
        {
            if (ModelState.IsValid)
            {
                db.TemplateSizes.Add(templateSize);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Template_PK = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateSize.Template_PK);
            return View(templateSize);
        }

        // GET: ArtMVCMaster/TemplateSizes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateSize templateSize = db.TemplateSizes.Find(id);
            if (templateSize == null)
            {
                return HttpNotFound();
            }
            ViewBag.Template_PK = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateSize.Template_PK);
            return View(templateSize);
        }

        // POST: ArtMVCMaster/TemplateSizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TemplateSize_PK,Template_PK,TemplateSize1")] TemplateSize templateSize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(templateSize).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Template_PK = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateSize.Template_PK);
            return View(templateSize);
        }

        // GET: ArtMVCMaster/TemplateSizes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateSize templateSize = db.TemplateSizes.Find(id);
            if (templateSize == null)
            {
                return HttpNotFound();
            }
            return View(templateSize);
        }

        // POST: ArtMVCMaster/TemplateSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TemplateSize templateSize = db.TemplateSizes.Find(id);
            db.TemplateSizes.Remove(templateSize);
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
