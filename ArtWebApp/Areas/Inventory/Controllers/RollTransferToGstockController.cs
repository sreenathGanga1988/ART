using ArtWebApp.Areas.Inventory.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.Inventory.Controllers
{
    public class RollTransferToGstockController : Controller
    {
        ArtEntitiesnew db = new ArtEntitiesnew();
        // GET: Inventory/RollTransferToGstock
        [HttpGet]
        public ActionResult RollTransferToGstock()
        {

            int locationpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            ViewBag.Id = new SelectList(db.TransferToGstockMasters.Where(U=>U.Location_Pk== locationpk), "TransferToGSTock_PK", "TransferNumber");
               
           
            return View();
        }
        [HttpPost]
        public ActionResult RollTransferToGstock(RollTransfertoGstockModelMaster rollTransfertoGstockModelMaster)
        {

            RollTransfertoGstockRepo rollTransfertoGstockRepo = new RollTransfertoGstockRepo();

            rollTransfertoGstockRepo.InsertGstockRoll(rollTransfertoGstockModelMaster);
            TempData["shortMessage"] = "Roll Added Sucessfully" ;
            return RedirectToAction("RollTransferToGstock");
        }




        [HttpGet]
        public JsonResult PopulateFabric(int Id = 0)
        {
            RollTransfertoGstockRepo rollTransfertoGstockRepo = new RollTransfertoGstockRepo();
            DataTable dt = rollTransfertoGstockRepo.GetfabricinsideTransferToGstock(Id);
            SelectList fabriclist = MVCControls.DataTabletoSelectList("SkuDet_PK", "itemDescription", dt, "Select A Fabric");
         


            JsonResult jsd = Json(fabriclist, JsonRequestBehavior.AllowGet);

            return jsd;

        }

        [HttpGet]
        public PartialViewResult GetRollView(int SkuDet_PK)
        {
            int locationpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            RollTransfertoGstockRepo rollTransfertoGstockRepo = new RollTransfertoGstockRepo();
            RollTransfertoGstockModelMaster model = rollTransfertoGstockRepo.GetRollTransfertoGstockModelMasterData(locationpk, SkuDet_PK);
        
            return PartialView("ToGstockRollview", model);
        }
    }
}