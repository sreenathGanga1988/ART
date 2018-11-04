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
            return View(db.LabRequestMasters.Where(u=> u.IsPosted==null) .ToList());

        }
        public ActionResult Edit(decimal id)
        {
            ViewBag.SupplierPK = new SelectList(db.SupplierMasters, "Supplier_PK", "SupplierName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabRequestMaster labRequestMaster = db.LabRequestMasters.Find(id);
            if (labRequestMaster == null)
            {
                return HttpNotFound();
            }
            //return View((labRequestMaster));
            return View(calculateAllowedValuesofFreight(labRequestMaster));
        }


        [HttpPost]
        public JsonResult Edit(LabChargeMasterViewModel order)
        {

            bool status = false;
            FreightChargeRepo freightChargeRepo = new FreightChargeRepo();

            string reqnum = freightChargeRepo.UpdateLabCharge(order);

            status = true;


            return new JsonResult { Data = new { status = status, Reqnum = reqnum } };
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

        public LabRequestMaster calculateAllowedValuesofFreight(LabRequestMaster labRequestMaster)
        {
            Decimal allowedvalue = 0;
            Decimal alreadyused = 0;
            decimal balance = 0;

            FreightChargeRepo freightChargeRepo = new FreightChargeRepo();
            foreach (LabChargeDetail freightChargeDetail in labRequestMaster.LabChargeDetails)
            {
                LabChargeDetail freightChargeDetailnew = freightChargeRepo.GetAllowedLabCharges(freightChargeDetail);
                freightChargeDetail.AllowedValue = freightChargeDetailnew.AllowedValue;
                freightChargeDetail.UsedValue = freightChargeDetailnew.UsedValue;
                freightChargeDetail.BalanceValue = freightChargeDetailnew.BalanceValue;

                allowedvalue += Decimal.Parse(freightChargeDetailnew.AllowedValue.ToString());
                alreadyused += Decimal.Parse(freightChargeDetailnew.UsedValue.ToString());
                balance += Decimal.Parse(freightChargeDetailnew.BalanceValue.ToString());
            }



            return labRequestMaster;
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