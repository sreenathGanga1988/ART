using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.Inventory.Controllers
{
    public class DeliveryReceiptController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();
        // GET: Inventory/DeliveryReceipt
        public ActionResult DeliveryReceipt()
        {
            int lockpk= int.Parse( HttpContext.Session["UserLoc_pk"].ToString());
            ViewBag.DoPK = new SelectList(db.DeliveryOrderMasters.Where(u=>u.ToLocation_PK==1), "BuyerID", "BuyerName");

            return View();
        }
    }
}