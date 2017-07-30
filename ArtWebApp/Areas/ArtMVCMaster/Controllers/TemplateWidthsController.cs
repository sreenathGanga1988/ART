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
    public class TemplateWidthsController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/TemplateWidths
        public ActionResult Index()
        {
            var templateWidths = db.TemplateWidths.Include(t => t.Template_Master);
            return View(templateWidths.ToList());
        }

        // GET: ArtMVCMaster/TemplateWidths/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateWidth templateWidth = db.TemplateWidths.Find(id);
            if (templateWidth == null)
            {
                return HttpNotFound();
            }
            return View(templateWidth);
        }

        // GET: ArtMVCMaster/TemplateWidths/Create
        public ActionResult Create()
        {
            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "TemplateCode");
            return View();
        }

        // POST: ArtMVCMaster/TemplateWidths/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TemplateWidth_Pk,Template_Pk,Width")] TemplateWidth templateWidth)
        {
            if (ModelState.IsValid)
            {
                db.TemplateWidths.Add(templateWidth);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateWidth.Template_Pk);
            return View(templateWidth);
        }

        // GET: ArtMVCMaster/TemplateWidths/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateWidth templateWidth = db.TemplateWidths.Find(id);
            if (templateWidth == null)
            {
                return HttpNotFound();
            }
            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateWidth.Template_Pk);
            return View(templateWidth);
        }

        // POST: ArtMVCMaster/TemplateWidths/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TemplateWidth_Pk,Template_Pk,Width")] TemplateWidth templateWidth)
        {
            if (ModelState.IsValid)
            {
                db.Entry(templateWidth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateWidth.Template_Pk);
            return View(templateWidth);
        }

        // GET: ArtMVCMaster/TemplateWidths/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateWidth templateWidth = db.TemplateWidths.Find(id);
            if (templateWidth == null)
            {
                return HttpNotFound();
            }
            return View(templateWidth);
        }

        // POST: ArtMVCMaster/TemplateWidths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TemplateWidth templateWidth = db.TemplateWidths.Find(id);
            db.TemplateWidths.Remove(templateWidth);
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
