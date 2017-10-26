using ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel;
using ArtWebApp.Areas.Repository;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVCMerchandiser.Controllers
{
    public class LabChargeRequestController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();
        // GET: ArtMVCMerchandiser/LabChargeRequest
        public ActionResult Index()
        {
            return View(db.LabRequestMasters.ToList());
           
        }

        public ActionResult Create()
        {
            ViewBag.SupplierPK = new SelectList(db.SupplierMasters, "Supplier_PK", "SupplierName");

            return View();
        }

        [HttpPost]
        public JsonResult Create(LabChargeMasterViewModel order)
        {

            bool status = false;
            FreightChargeRepo freightChargeRepo = new FreightChargeRepo();
            string reqnum = freightChargeRepo.InsertLabCharges(order);
            status = true;


            return new JsonResult { Data = new { status = status } };
        }

        // GET: ArtMVCMerchandiser/FreightRequestMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabRequestMaster freightRequestMaster = db.LabRequestMasters.Find(id);
            if (freightRequestMaster == null)
            {
                return HttpNotFound();
            }
            return View(freightRequestMaster);
        }



    }
}