using ArtWebApp.Areas.Shipping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.Shipping.Controllers
{
    public class GateReceiptController : Controller
    {
        // GET: Shipping/GateReceipt
        public ActionResult Index()
        {
            return View();
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