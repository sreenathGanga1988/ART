using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ArtWebApp.Areas.Inventory.ViewModel;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.Inventory.Controllers
{
    public class RackAllocationController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew();
        // GET: Inventory/RackAllocation
        public ActionResult RackAllocationIndex()
        {
            ViewBag.Mrn_PK = new SelectList(enty.MrnMasters, "Mrn_PK", "MrnNum");
            ViewBag.Rack_PK= new SelectList(enty.RackMasters, "Rack_PK", "Rack_name");
            return View();
        }
        

        [HttpGet]
        public PartialViewResult GetMRNDetails(int mrn_pk,int Rack_PK)
        {
            AllocateRack allocateRack = new AllocateRack();
            InventoryRepo inventoryRepo = new InventoryRepo();
            DataTable dt= inventoryRepo.GetMrnDetailsforAllocation(mrn_pk, Rack_PK);
            
            if (dt != null)
            {
            
                allocateRack.GetMrnlist = dt;
            }

            return PartialView("TrimsRackAllocation_P", allocateRack);
        }
        [HttpPost]
        public ActionResult ApproveRequest(List<Approvedata> things)
        {
            bool status = false;
            InventoryRepo inventoryRepo = new InventoryRepo();
            //inventoryRepo.LockCM(things);
            foreach(Approvedata item in things)
            {
                var inv = from inventory in enty.InventoryMasters
                          where inventory.InventoryItem_PK == int.Parse(item.InventoryItem_PK.ToString())
                          select inventory;

                foreach ( var element in inv)
                {
                    RackInventoryMaster rackInventoryMaster = new RackInventoryMaster();
                    rackInventoryMaster.Inventoryitem_PK = element.InventoryItem_PK;
                    rackInventoryMaster.MrnDet_Pk = element.MrnDet_PK;
                    rackInventoryMaster.SkuDet_PK = element.SkuDet_Pk;
                    rackInventoryMaster.PoDet_PK = element.PoDet_PK;
                    rackInventoryMaster.OnhandQty = element.OnhandQty;
                    rackInventoryMaster.ReceivedVia = element.ReceivedVia;
                    rackInventoryMaster.Location_PK = element.Location_PK;
                    rackInventoryMaster.Rack_PK = int.Parse(item.Rack_PK.ToString());
                    rackInventoryMaster.RefNum = element.Refnum;
                    rackInventoryMaster.IsLast = "Y";
                    enty.RackInventoryMasters.Add(rackInventoryMaster);
                    var mrn = from mrndet in enty.MrnDetails
                              where mrndet.MrnDet_PK == element.MrnDet_PK
                              select mrndet;
                    foreach(var mrndet in mrn)
                    {
                        mrndet.IsRackAllocateDone = "Y";
                        enty.SaveChanges();
                    }
                              
                }
                
            }
            enty.SaveChanges();

            
            return Json(new { ok = true, newurl = Url.Action("RackAllocationIndex") }, JsonRequestBehavior.AllowGet);
            


        }
    }
}