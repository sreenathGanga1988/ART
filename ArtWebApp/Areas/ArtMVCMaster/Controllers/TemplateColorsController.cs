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
    public class TemplateColorsController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/TemplateColors
        public ActionResult Index()
        {
            var templateColors = db.TemplateColors.Include(t => t.Template_Master);
            return View(templateColors.ToList());
        }

        // GET: ArtMVCMaster/TemplateColors/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateColor templateColor = db.TemplateColors.Find(id);
            if (templateColor == null)
            {
                return HttpNotFound();
            }
            return View(templateColor);
        }

        // GET: ArtMVCMaster/TemplateColors/Create
        public ActionResult Create()
        {
            ViewBag.Template_PK = new SelectList(db.Template_Master, "Template_PK", "TemplateCode");
            return View();
        }

        // POST: ArtMVCMaster/TemplateColors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TemplateColor_PK,Template_PK,TemplateColor1")] TemplateColor templateColor)
        {
            if (ModelState.IsValid)
            {
                db.TemplateColors.Add(templateColor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Template_PK = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateColor.Template_PK);
            return View(templateColor);
        }

        // GET: ArtMVCMaster/TemplateColors/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateColor templateColor = db.TemplateColors.Find(id);
            if (templateColor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Template_PK = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateColor.Template_PK);
            return View(templateColor);
        }

        // POST: ArtMVCMaster/TemplateColors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TemplateColor_PK,Template_PK,TemplateColor1")] TemplateColor templateColor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(templateColor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Template_PK = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateColor.Template_PK);
            return View(templateColor);
        }

        // GET: ArtMVCMaster/TemplateColors/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateColor templateColor = db.TemplateColors.Find(id);
            if (templateColor == null)
            {
                return HttpNotFound();
            }
            return View(templateColor);
        }

        // POST: ArtMVCMaster/TemplateColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TemplateColor templateColor = db.TemplateColors.Find(id);
            db.TemplateColors.Remove(templateColor);
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
