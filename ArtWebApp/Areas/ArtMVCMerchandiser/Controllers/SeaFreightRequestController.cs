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
    public class SeaFreightRequestController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCMerchandiser/SeaFreightRequests
        public ActionResult Index()
        {
            return View(db.FreightRequestMasters.Where(u=>u.ShipementType=="Sea").ToList());
        }

        // GET: ArtMVCMerchandiser/SeaFreightRequests/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreightRequestMaster freightRequestMaster = db.FreightRequestMasters.Find(id);
            if (freightRequestMaster == null)
            {
                return HttpNotFound();
            }
            return View(freightRequestMaster);
        }

        // GET: ArtMVCMerchandiser/SeaFreightRequests/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtMVCMerchandiser/FreightRequestMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(FreightRequestMasterViewModel order)
        {

            bool status = false;
            FreightChargeRepo freightChargeRepo = new FreightChargeRepo();
            order.ShipmentType = "Sea";
            string reqnum = freightChargeRepo.InsertFreightCharges(order);
            status = true;


            return new JsonResult { Data = new { status = status } };
        }
        [HttpGet]
        public JsonResult GetATcList()
        {


            SelectList atclist = new SelectList(db.AtcMasters.Where(u => u.IsCompleted == "N" && u.IsClosed == "N"), "AtcID", "AtcNum");


            JsonResult jsd = Json(atclist, JsonRequestBehavior.AllowGet);

            return jsd;

        }


        [HttpGet]
        public JsonResult GetAllowedFreightCharge(int id = 0)
        {

            DBTransaction.CostingTransaction costtrans = new DBTransaction.CostingTransaction();

            Decimal allowedvalue = 0;

            allowedvalue = costtrans.GetAllowedFreightCharges(id);



            var finalvalue = new { success = true, allowedvalue = allowedvalue.ToString() };
            return Json(finalvalue, JsonRequestBehavior.AllowGet);
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