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
    public class TemplateCompositionsController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/TemplateCompositions
        public ActionResult Index()
        {
            var templateCompositions = db.TemplateCompositions.Include(t => t.Template_Master);
            return View(templateCompositions.ToList());
        }

        // GET: ArtMVCMaster/TemplateCompositions/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateComposition templateComposition = db.TemplateCompositions.Find(id);
            if (templateComposition == null)
            {
                return HttpNotFound();
            }
            return View(templateComposition);
        }

        // GET: ArtMVCMaster/TemplateCompositions/Create
        public ActionResult Create()
        {
            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "Description");
            return View();
        }

        // POST: ArtMVCMaster/TemplateCompositions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TemplateCom_Pk,Template_Pk,Composition")] TemplateComposition templateComposition)
        {
            if (ModelState.IsValid)
            {
                db.TemplateCompositions.Add(templateComposition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "Description", templateComposition.Template_Pk);
            return View(templateComposition);
        }

        // GET: ArtMVCMaster/TemplateCompositions/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateComposition templateComposition = db.TemplateCompositions.Find(id);
            if (templateComposition == null)
            {
                return HttpNotFound();
            }
            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "Description", templateComposition.Template_Pk);
            return View(templateComposition);
        }

        // POST: ArtMVCMaster/TemplateCompositions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TemplateCom_Pk,Template_Pk,Composition")] TemplateComposition templateComposition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(templateComposition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateComposition.Template_Pk);
            return View(templateComposition);
        }

        // GET: ArtMVCMaster/TemplateCompositions/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateComposition templateComposition = db.TemplateCompositions.Find(id);
            if (templateComposition == null)
            {
                return HttpNotFound();
            }
            return View(templateComposition);
        }

        // POST: ArtMVCMaster/TemplateCompositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TemplateComposition templateComposition = db.TemplateCompositions.Find(id);
            db.TemplateCompositions.Remove(templateComposition);
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
