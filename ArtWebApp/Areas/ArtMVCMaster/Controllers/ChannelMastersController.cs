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
    public class ChannelMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/ChannelMasters
        public ActionResult Index()
        {
            return View(db.ChannelMasters.ToList());
        }

        // GET: ArtMVCMaster/ChannelMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChannelMaster channelMaster = db.ChannelMasters.Find(id);
            if (channelMaster == null)
            {
                return HttpNotFound();
            }
            return View(channelMaster);
        }

        // GET: ArtMVCMaster/ChannelMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMaster/ChannelMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChannelID,ChannelName")] ChannelMaster channelMaster)
        {
            if (ModelState.IsValid)
            {
                db.ChannelMasters.Add(channelMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(channelMaster);
        }

        // GET: ArtMVCMaster/ChannelMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChannelMaster channelMaster = db.ChannelMasters.Find(id);
            if (channelMaster == null)
            {
                return HttpNotFound();
            }
            return View(channelMaster);
        }

        // POST: ArtMVCMaster/ChannelMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChannelID,ChannelName")] ChannelMaster channelMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(channelMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(channelMaster);
        }

        // GET: ArtMVCMaster/ChannelMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChannelMaster channelMaster = db.ChannelMasters.Find(id);
            if (channelMaster == null)
            {
                return HttpNotFound();
            }
            return View(channelMaster);
        }

        // POST: ArtMVCMaster/ChannelMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ChannelMaster channelMaster = db.ChannelMasters.Find(id);
            db.ChannelMasters.Remove(channelMaster);
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
