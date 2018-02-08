using ArtWebApp.Areas.CuttingMVC.Models;
using ArtWebApp.Areas.Inventory.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.Inventory.Controllers
{
    public class RollPropertyChangeController : Controller
    {
        // GET: Inventory/RollPropertyChange
        public ActionResult Index()

        {

           

         
            
            return View();
        }



        [HttpGet]
        public PartialViewResult GetRollView(int id)
        {
            InventoryRepo inventoryRepo = new InventoryRepo();
            RollPropertyViewModelMaster rollPropertyViewModelMaster = inventoryRepo.GetRollData(id);
           
          
            return PartialView("RollPropertyView", rollPropertyViewModelMaster);
        }


        [HttpPost]
        public ActionResult RollPropertyChage(RollPropertyJson Model)
        {

            InventoryRepo inventoryRepo = new InventoryRepo();



            inventoryRepo.insertRollProperty(Model);

            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult ApproveRolltransfer()

        {
            int locationpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            InventoryRepo inventoryRepo = new InventoryRepo();
           RollPropertApprovalModelMaster rollPropertApprovalModelMaster = inventoryRepo.GetRollPropertApprovalModelMasterData(locationpk);
            

                return View(rollPropertApprovalModelMaster);
        }

        [HttpGet]
        public ActionResult Cancel(decimal id)

        {
            InventoryRepo inventoryRepo = new InventoryRepo();

            inventoryRepo.CancelRollPropertyChange(int.Parse(id.ToString()));

            return RedirectToAction("ApproveRolltransfer");
        }
        public ActionResult Approve(decimal id)
        {
           
            InventoryRepo inventoryRepo = new InventoryRepo();

            inventoryRepo.ApproveRollPropertyChange(int.Parse(id.ToString()));

          return  RedirectToAction("ApproveRolltransfer");
        }

    }
}