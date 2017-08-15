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
    public class UserMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMaster/UserMasters
        public ActionResult Index()
        {

          if(Session["UserProfile_Pk"].ToString().Trim()== "11")
            {
                var userMasters = db.UserMasters.Include(u => u.DepartmentMaster).Include(u => u.LocationMaster).Include(u => u.UserProfileMaster);
                return View(userMasters.ToList());
            }
            else
            {

                int userpk = int.Parse(Session["User_PK"].ToString());
                var userMasters = db.UserMasters.Include(u => u.DepartmentMaster).Where (u=>u.User_PK==userpk).Include(u => u.LocationMaster).Include(u => u.UserProfileMaster);
                return View(userMasters.ToList());
            }
            
           
        }

        // GET: ArtMVCMaster/UserMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }

        // GET: ArtMVCMaster/UserMasters/Create
        public ActionResult Create()
        {
            ViewBag.Department_PK = new SelectList(db.DepartmentMasters, "Deapartment_PK", "DepartmentName");
            ViewBag.UserLoc_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName");
            ViewBag.UserProfile_Pk = new SelectList(db.UserProfileMasters, "UserProfile_Pk", "UserProfileName");
            return View();
        }

        // POST: ArtMVCMaster/UserMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_PK,UserName,Password,UserLoc_PK,PassCode,IsAdmin,LastLogin,UserProfile_Pk,Department_PK,EmailId,MobileNum,PssWrd,LastPassWordDate,IsActiveUser,IsDeleteduser,IsLockedUser,IsVerified,ITVerified,AllowOutSideAction,AddedBy,AddedDate")] UserMaster userMaster)
        {
            if (ModelState.IsValid)
            {
                userMaster.AddedBy = Session["Username"].ToString();
                userMaster.AddedDate = DateTime.Now;
                userMaster.LastPassWordDate = DateTime.Now;
                db.UserMasters.Add(userMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Department_PK = new SelectList(db.DepartmentMasters, "Deapartment_PK", "DepartmentName", userMaster.Department_PK);
            ViewBag.UserLoc_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName", userMaster.UserLoc_PK);
            ViewBag.UserProfile_Pk = new SelectList(db.UserProfileMasters, "UserProfile_Pk", "UserProfileName", userMaster.UserProfile_Pk);
            return View(userMaster);
        }

        // GET: ArtMVCMaster/UserMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.Department_PK = new SelectList(db.DepartmentMasters, "Deapartment_PK", "DepartmentName", userMaster.Department_PK);
            ViewBag.UserLoc_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName", userMaster.UserLoc_PK);
            ViewBag.UserProfile_Pk = new SelectList(db.UserProfileMasters, "UserProfile_Pk", "UserProfileName", userMaster.UserProfile_Pk);
            return View(userMaster);
        }

        // POST: ArtMVCMaster/UserMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_PK,UserName,Password,UserLoc_PK,PassCode,IsAdmin,LastLogin,UserProfile_Pk,Department_PK,EmailId,MobileNum,PssWrd,LastPassWordDate,IsActiveUser,IsDeleteduser,IsLockedUser,IsVerified,ITVerified,AllowOutSideAction,AddedBy,AddedDate")] UserMaster userMaster)
        {
            if (ModelState.IsValid)
            {
                userMaster.AddedBy = Session["Username"].ToString();
                userMaster.AddedDate = DateTime.Now;
                userMaster.LastPassWordDate = DateTime.Now;
                db.Entry(userMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Department_PK = new SelectList(db.DepartmentMasters, "Deapartment_PK", "DepartmentName", userMaster.Department_PK);
            ViewBag.UserLoc_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName", userMaster.UserLoc_PK);
            ViewBag.UserProfile_Pk = new SelectList(db.UserProfileMasters, "UserProfile_Pk", "UserProfileName", userMaster.UserProfile_Pk);
            return View(userMaster);
        }

        // GET: ArtMVCMaster/UserMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }

        // POST: ArtMVCMaster/UserMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            UserMaster userMaster = db.UserMasters.Find(id);
            db.UserMasters.Remove(userMaster);
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
