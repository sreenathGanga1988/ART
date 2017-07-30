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
    public class TemplateWeightsController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/TemplateWeights
        public ActionResult Index()
        {
            var templateWeights = db.TemplateWeights.Include(t => t.Template_Master);
            return View(templateWeights.ToList());
        }

        // GET: ArtMVCMaster/TemplateWeights/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateWeight templateWeight = db.TemplateWeights.Find(id);
            if (templateWeight == null)
            {
                return HttpNotFound();
            }
            return View(templateWeight);
        }

        // GET: ArtMVCMaster/TemplateWeights/Create
        public ActionResult Create()
        {
            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "TemplateCode");
            return View();
        }

        // POST: ArtMVCMaster/TemplateWeights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TemplateWeight_Pk,Template_Pk,Weight")] TemplateWeight templateWeight)
        {
            if (ModelState.IsValid)
            {
                db.TemplateWeights.Add(templateWeight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateWeight.Template_Pk);
            return View(templateWeight);
        }

        // GET: ArtMVCMaster/TemplateWeights/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateWeight templateWeight = db.TemplateWeights.Find(id);
            if (templateWeight == null)
            {
                return HttpNotFound();
            }
            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateWeight.Template_Pk);
            return View(templateWeight);
        }

        // POST: ArtMVCMaster/TemplateWeights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TemplateWeight_Pk,Template_Pk,Weight")] TemplateWeight templateWeight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(templateWeight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Template_Pk = new SelectList(db.Template_Master, "Template_PK", "TemplateCode", templateWeight.Template_Pk);
            return View(templateWeight);
        }

        // GET: ArtMVCMaster/TemplateWeights/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemplateWeight templateWeight = db.TemplateWeights.Find(id);
            if (templateWeight == null)
            {
                return HttpNotFound();
            }
            return View(templateWeight);
        }

        // POST: ArtMVCMaster/TemplateWeights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TemplateWeight templateWeight = db.TemplateWeights.Find(id);
            db.TemplateWeights.Remove(templateWeight);
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
