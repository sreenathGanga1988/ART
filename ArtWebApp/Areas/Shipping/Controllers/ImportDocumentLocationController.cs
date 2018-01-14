using ArtWebApp.Areas.Shipping.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.Shipping.Controllers
{
    public class ImportDocumentLocationController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: Shipping/ImportDocumentLocation
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AssignOffLoadLocation(int id)
        {
           
            ShippingRepo shippingRepo = new ShippingRepo();
            ImportViewModelMaster importViewModelMaster = shippingRepo.loadIMPreport(id);
            ViewBag.Location_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName");

            return View(importViewModelMaster);
        }

        [HttpPost]
        public ActionResult AssignOffLoadLocation(ImportViewModelMaster importViewModelMaster)
        {
            ShippingRepo shippingRepo = new ShippingRepo();

            shippingRepo.InsertShippingDocumentlocation(importViewModelMaster);

            return RedirectToAction("AssignOffLoadLocation", importViewModelMaster.ID);
        }
    }
}