using ArtWebApp.Areas.Shipping.ViewModel;
using ArtWebApp.DataModels;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.Shipping.Controllers
{
    public class GateReceiptController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: Shipping/GateReceipt
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GateINIndex()
        {
            return View(db.ShippingDocumentMasters.Where(u =>u.IsReceived == "N" && u.IsDelivered == "Y" && u.IsAssigned == "Y").ToList().OrderByDescending(u => u.ShipingDoc_PK));


        }

        [HttpGet]
        public ActionResult GateIN(int id)
        {
            ShippingDocumentMaster shippingDocument = db.ShippingDocumentMasters.Find(id);
            return View(shippingDocument);
        }
        [HttpPost]
        public ActionResult GateIN(ShippingDocumentMaster shippingDocument)
        {
            ShippingRepo shippingRepo = new ShippingRepo();
           shippingRepo.gateIn(int.Parse( shippingDocument.ShipingDoc_PK.ToString()), Decimal.Parse(shippingDocument.DeliveredPackage.ToString()));         
            return RedirectToAction("GateINIndex");
        }


        [HttpGet]
        public ActionResult GateOutIndex()
        {
            String locid = HttpContext.Session["UserLoc_pk"].ToString();
            return View(db.ShippingDocumentMasters.Where(u =>  u.LastReceivedLocationPK == locid && u.IsReceived == "Y" && u.IsDelivered == "N" && u.IsAssigned == "Y").ToList());

        }

        [HttpGet]
        public ActionResult GateOut(int id)
        {
            String locid = HttpContext.Session["UserLoc_pk"].ToString();
            ViewBag.Locid = new SelectList(db.LocationMasters, "location_pk", "locationname");
            ShippingDocumentMaster shippingDocument = db.ShippingDocumentMasters.Where(u=>u.ShipingDoc_PK==id && u.LastReceivedLocationPK== locid && u.IsDelivered=="N").FirstOrDefault();
            return View(shippingDocument);
        }
        //[HttpPost]
        //public ActionResult GateOut(ShippingDocumentMaster shippingDocument)
        //{
        //    ShippingRepo shippingRepo = new ShippingRepo();
        //    shippingRepo.gateOut(int.Parse(shippingDocument.ShipingDoc_PK.ToString()), decimal.Parse(shippingDocument.DeliveredPackage.ToString()),shippingDocument.SetLocation, decimal.Parse(shippingDocument.ToLoc_Pk.ToString()));
        //    return RedirectToAction("GateOutIndex");
        //}
        [HttpGet]
        public ActionResult UpdateGateOut(int shippingdoc_pk,decimal qty, int locid, Boolean setlocation)
        {
            ShippingDocumentMaster shippingDocumentMaster = new ShippingDocumentMaster();
            ShippingRepo shippingRepo = new ShippingRepo();
            shippingRepo.gateOut(shippingdoc_pk, qty,setlocation,decimal.Parse(locid.ToString()));
            return RedirectToAction("GateOutIndex");
        }

        [HttpGet]
        public ActionResult GateReceipt(int id)
        {
           
            ShippingRepo shippingRepo = new ShippingRepo();
           ImportViewModelMaster importViewModelMaster = shippingRepo.loadIMPreport(id);
            importViewModelMaster.ID = id;
            String idstrng = id.ToString();

        String locid= HttpContext.Session["UserLoc_pk"].ToString();

            ViewBag.OtherLocationlist= importViewModelMaster.ImportViewModels.Where(u => u.Location_PK != locid).ToList();

            importViewModelMaster.ImportViewModels = importViewModelMaster.ImportViewModels.Where(u => u.Location_PK == locid).ToList();

            return View(importViewModelMaster);
        }


        [HttpGet]
        public ActionResult PendingGateReceipt(int id)
        {

            ShippingRepo shippingRepo = new ShippingRepo();
            ImportViewModelMaster importViewModelMaster = shippingRepo.loadIMPreport(id);
            String idstrng = id.ToString();
            importViewModelMaster.ID = id;
            importViewModelMaster.ImportViewModels = importViewModelMaster.ImportViewModels.Where(u => u.isReceived == "N").ToList();




            List<String> LocationID = importViewModelMaster.ImportViewModels.Select(u => u.Location_PK).Distinct().ToList();


            ViewBag.Location_PKs = LocationID;


            return View(importViewModelMaster);
        }
        public ActionResult Print(decimal id)
        {
            var report = new Rotativa.MVC.ActionAsPdf("PendingGateReceipt", new { id = id });
            return report;

        }
        [HttpPost]
        public ActionResult GateReceipt(ImportViewModelMaster importViewModelMaster)
        {
            ShippingRepo shippingRepo = new ShippingRepo();

            shippingRepo.ReceiveDocumentlocation(importViewModelMaster);

            return RedirectToAction("GateReceipt", importViewModelMaster.ID);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Accept")]
        public ActionResult Accept(ImportViewModelMaster importViewModelMaster)
        {
            ShippingRepo shippingRepo = new ShippingRepo();

            shippingRepo.ReceiveDocumentlocation(importViewModelMaster);

            return RedirectToAction("GateReceipt", importViewModelMaster.ID);
        }
      
    }
}