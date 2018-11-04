using ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel;
using ArtWebApp.Areas.Repository;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVCMerchandiser.Controllers
{
    public class StockPOFreightChargeController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();
        // GET: ArtMVCMerchandiser/StockPOFreightCharge
        public ActionResult Index()
        {
            return View(db.StockFreightRequestMasters.Where(u => u.IsPosted == null && u.IsDeleted=="N").ToList());
        }

        // GET: ArtMVCMerchandiser/SeaFreightRequests/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockFreightRequestMaster sfreightRequestMaster = db.StockFreightRequestMasters.Find(id);
            if (sfreightRequestMaster == null)
            {
                return HttpNotFound();
            }
            return View(sfreightRequestMaster);
        }

        // GET: ArtMVCMerchandiser/SeaFreightRequests/Create
        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> ShipementType = new List<SelectListItem>();

            ShipementType.Add(new SelectListItem { Text = "Sea", Value = "Sea" });
            ShipementType.Add(new SelectListItem { Text = "Air", Value = "Air" });
            ShipementType.Add(new SelectListItem { Text = "Courier", Value = "Courier" });


            ViewBag.ShipementType = ShipementType;
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
          
            string reqnum = freightChargeRepo.InsertStocFreightCharges(order);
            status = true;


            return new JsonResult { Data = new { status = status } };
        }
        [HttpGet]
        public JsonResult GetPOList()
        {


            SelectList atclist = new SelectList(db.StockPOMasters.Where(u => u.IsApproved == "Y" ), "SPO_Pk", "SPONum");


            JsonResult jsd = Json(atclist, JsonRequestBehavior.AllowGet);

            return jsd;

        }


        [HttpGet]
        public JsonResult GetItemDescription(int id = 0)
        {

            BLL.ProcurementBLL.StockPODetailsdata spdetdata = new BLL.ProcurementBLL.StockPODetailsdata();

            DataTable dt = spdetdata.GetSpoItemList(id);

            SelectList Mylist = MVCControls.DataTabletoSelectList("SPODetails_PK", "Item", dt, "");



           
            return Json(Mylist, JsonRequestBehavior.AllowGet);
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