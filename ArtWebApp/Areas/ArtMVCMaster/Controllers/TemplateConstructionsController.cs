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
    public class TemplateConstructionsController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/TemplateConstructions
        public ActionResult Index()
        {
            var templateConstructions = db.TemplateConstructions.Include(t => t.Template_Master);
            return View(templateConstructions.ToList());
        }

        // GET: ArtMVCMaster/TemplateConstructions/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateConstruction templateConstruction = db.TemplateConstructions.Find(id);
            if (templateConstruction == null)
            {
                return HttpNotFound();
            }
            return View(templateConstruction);
        }

        // GET: ArtMVCMaster/TemplateConstructions/Create
        public ActionResult Create()
        {
            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "TemplateCode");
            return View();
        }

        // POST: ArtMVCMaster/TemplateConstructions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TemplateCon_Pk,Template_Pk,Construct")] TemplateConstruction templateConstruction)
        {
            if (ModelState.IsValid)
            {
                db.TemplateConstructions.Add(templateConstruction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateConstruction.Template_Pk);
            return View(templateConstruction);
        }

        // GET: ArtMVCMaster/TemplateConstructions/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateConstruction templateConstruction = db.TemplateConstructions.Find(id);
            if (templateConstruction == null)
            {
                return HttpNotFound();
            }
            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateConstruction.Template_Pk);
            return View(templateConstruction);
        }

        // POST: ArtMVCMaster/TemplateConstructions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TemplateCon_Pk,Template_Pk,Construct")] TemplateConstruction templateConstruction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(templateConstruction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateConstruction.Template_Pk);
            return View(templateConstruction);
        }

        // GET: ArtMVCMaster/TemplateConstructions/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateConstruction templateConstruction = db.TemplateConstructions.Find(id);
            if (templateConstruction == null)
            {
                return HttpNotFound();
            }
            return View(templateConstruction);
        }

        // POST: ArtMVCMaster/TemplateConstructions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TemplateConstruction templateConstruction = db.TemplateConstructions.Find(id);
            db.TemplateConstructions.Remove(templateConstruction);
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
