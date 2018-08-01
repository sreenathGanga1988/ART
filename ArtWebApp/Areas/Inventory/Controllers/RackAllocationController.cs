using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ArtWebApp.Areas.Inventory.ViewModel;
using ArtWebApp.DataModels;
using System.Data.Entity.Validation;

namespace ArtWebApp.Areas.Inventory.Controllers
{
    public class RackAllocationController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew();
        // GET: Inventory/RackAllocation
        public ActionResult RackAllocationIndex()
        {
            int locpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            ViewBag.Mrn_PK = new SelectList(enty.MrnMasters, "Mrn_PK", "MrnNum");
            ViewBag.Rack_PK= new SelectList(enty.RackMasters.Where (u=> u.Rack_type == "Trims" && u.Location_pk == locpk).ToList(), "Rack_PK", "Rack_name");
            return View();
        }
        public ActionResult RollRackAllocationIndex()
        {
            int locpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            ViewBag.Mrn_PK = new SelectList(enty.MrnMasters, "Mrn_PK", "MrnNum");
            ViewBag.Rack_PK = new SelectList(enty.RackMasters.Where(u => u.Rack_type == "Fabric" && u.Location_pk == locpk).ToList(), "Rack_PK", "Rack_name");
            return View();
        }
        public ActionResult TrimsRackShuffleIndex()
        {
            int locpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            ViewBag.Rack_PK = new SelectList(enty.RackMasters.Where(u => u.Rack_type == "Trims" && u.Location_pk == locpk).ToList(), "Rack_PK", "Rack_name");            

            return View();
        }
        public ActionResult RollRackShuffleIndex()
        {
            int locpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            ViewBag.Atcid = new SelectList(enty.AtcMasters, "Atcid", "AtcNum");
            ViewBag.Rack_PK = new SelectList(enty.RackMasters.Where(u => u.Rack_type == "Fabric" && u.Location_pk == locpk).ToList(), "Rack_PK", "Rack_name");            

            return View();
        }

        [HttpGet]
        public PartialViewResult GetDetailsRackShuffle(int Rack_Pk)
        {
            int locpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            AllocateRack allocateRack = new AllocateRack();
            ViewBag.New_Rack_PK = new SelectList(enty.RackMasters.Where(u => u.Rack_type == "Trims" && u.Location_pk == locpk).ToList(), "Rack_PK", "Rack_name");
            InventoryRepo inv = new InventoryRepo();
            int locp = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            DataTable dt = inv.GetDetailsRackShuffle(Rack_Pk);
            allocateRack.GetMrnlist = dt;
            return PartialView("TrimsRackShuffle_P",allocateRack);
        }
        [HttpGet]
        public PartialViewResult GetRollDetailsRackShuffle(int Atcid,int Rack_Pk)
        {
            int locpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            AllocateRack allocateRack = new AllocateRack();
            ViewBag.New_Rack_PK = new SelectList(enty.RackMasters.Where(u => u.Rack_type == "Fabric" && u.Location_pk == locpk).ToList(), "Rack_PK", "Rack_name");
            InventoryRepo inv = new InventoryRepo();
            int locp = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            DataTable dt = inv.GetRollDetailsRackShuffle(locpk,Atcid,Rack_Pk);
            allocateRack.GetMrnlist = dt;
            return PartialView("RollRackShuffle_P", allocateRack);
        }

        [HttpGet]
        public PartialViewResult GetMRNDetails(int mrn_pk,int Rack_PK)
        {
            AllocateRack allocateRack = new AllocateRack();
            InventoryRepo inventoryRepo = new InventoryRepo();
            int locpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            DataTable dt= inventoryRepo.GetMrnDetailsforAllocation(mrn_pk, Rack_PK, locpk);
            
            if (dt != null)
            {
                
                allocateRack.GetMrnlist = dt;
            }

            return PartialView("TrimsRackAllocation_P", allocateRack);
        }
        [HttpGet]
        public PartialViewResult GetRollMRNDetails(int mrn_pk, int Rack_PK)
        {
            int locpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            AllocateRack allocateRack = new AllocateRack();
            InventoryRepo inventoryRepo = new InventoryRepo();
            DataTable dt = inventoryRepo.GetRollMrnDetailsforAllocation(mrn_pk, Rack_PK, locpk);

            if (dt != null)
            {

                allocateRack.GetMrnlist = dt;
            }

            return PartialView("RollRackAllocation_P", allocateRack);
        }

        [HttpPost]
        public JsonResult RollAllocate(List<Approvedata> things)
        {
            bool status = false;
            InventoryRepo inventoryRepo = new InventoryRepo();
            foreach (Approvedata roll in things)
            {
                int rollpk = int.Parse(roll.InventoryItem_PK.ToString());
                int rackpk = int.Parse(roll.Rack_PK.ToString());
                var q = from fab in enty.FabricRollmasters
                        where fab.Roll_PK == rollpk
                        select fab;
                foreach (var element in q)
                {
                    element.RackAllocated = "Y";
                }
                var rin = from rollinv in enty.RollInventoryMasters where rollinv.Roll_PK == rollpk select rollinv;
                foreach (var r in rin)
                {
                    r.Rack_PK = rackpk;
                }

                try
                {
                    enty.SaveChanges();
                    status = true;
                }
                catch (Exception exp)
                {
                    status = false;
                    throw;
                }

            }
            return new JsonResult { Data = new { status = status } }; ;
        }

        [HttpPost]
        public JsonResult RollShuffleAllocate(List<Approvedata> things)
        {
            bool status = false;
            InventoryRepo inventoryRepo = new InventoryRepo();
            foreach(Approvedata roll in things)
            {
                int rollpk = int.Parse(roll.InventoryItem_PK.ToString());
                int rackpk = int.Parse(roll.NewRack_PK.ToString());
                
                var rin = from rollinv in enty.RollInventoryMasters where rollinv.Roll_PK == rollpk select rollinv;
                foreach (var r in rin)
                {
                    r.Rack_PK = rackpk;                    
                }

                try
                {
                    enty.SaveChanges();
                    status = true;
                }
                catch (Exception exp)
                {
                    status = false;
                    throw;
                }

            }
            return new JsonResult { Data = new { status = status } }; ;
        }

        [HttpPost]
        public JsonResult ApproveRequest(List<Approvedata> things)
        {
            bool status = false;
            InventoryRepo inventoryRepo = new InventoryRepo();
            //inventoryRepo.LockCM(things);
            foreach(Approvedata item in things)
            {
                int inv_pk = int.Parse(item.InventoryItem_PK.ToString());
                int rack_pk= int.Parse(item.Rack_PK.ToString());
                var inv = from inventory in enty.InventoryMasters
                          where inventory.InventoryItem_PK == inv_pk
                          select inventory;                

                foreach ( var element in inv)
                {
                    if (!enty.RackInventoryMasters.Any(f=> f.Inventoryitem_PK==element.InventoryItem_PK && f.Rack_PK == rack_pk))
                    {
                        RackInventoryMaster rackInventoryMaster = new RackInventoryMaster();
                        rackInventoryMaster.Inventoryitem_PK = element.InventoryItem_PK;
                        rackInventoryMaster.MrnDet_Pk = element.MrnDet_PK;
                        rackInventoryMaster.SkuDet_PK = element.SkuDet_Pk;
                        rackInventoryMaster.PoDet_PK = element.PoDet_PK;
                        rackInventoryMaster.OnhandQty = Decimal.Parse(item.AllocateQty.ToString());
                        rackInventoryMaster.MrnQty = Decimal.Parse(item.mrnqty.ToString());
                        rackInventoryMaster.ReceivedQty = Decimal.Parse(item.AllocateQty.ToString());
                        rackInventoryMaster.BalanceQty = Decimal.Parse(item.BalanceQty.ToString()) - Decimal.Parse(item.AllocateQty.ToString());
                        rackInventoryMaster.ReceivedVia = element.ReceivedVia;
                        rackInventoryMaster.Location_PK = element.Location_PK;
                        rackInventoryMaster.Rack_PK = int.Parse(item.Rack_PK.ToString());
                        rackInventoryMaster.RefNum = element.Refnum;
                        rackInventoryMaster.DeliveredQty = 0;
                        rackInventoryMaster.IsLast = "Y";
                        enty.RackInventoryMasters.Add(rackInventoryMaster);
                        //try
                        //{
                        //    enty.SaveChanges();
                        //}
                        //catch (Exception e)
                        //{
                        //    Console.WriteLine(String.Concat(e.StackTrace, e.Message));

                        //    if (e.InnerException != null)
                        //    {
                        //        Console.WriteLine("Inner Exception");
                        //        Console.WriteLine(String.Concat(e.InnerException.StackTrace,
                        //        e.InnerException.Message));
                        //    }
                        //    throw;
                        //}
                        if ((Decimal.Parse(item.BalanceQty.ToString()) - Decimal.Parse(item.AllocateQty.ToString())) == 0)
                        {
                            var mrn = from mrndet in enty.MrnDetails
                                      where mrndet.MrnDet_PK == element.MrnDet_PK
                                      select mrndet;
                            foreach (var mrndet in mrn)
                            {
                                mrndet.IsRackAllocateDone = "Y";
                                
                            }
                        }

                    }
                    else
                    {

                        var q = from rack in enty.RackInventoryMasters where rack.Inventoryitem_PK == element.InventoryItem_PK && rack.Rack_PK == rack_pk select rack;
                        foreach(var rackinv in q)
                        {
                            rackinv.ReceivedQty+= Decimal.Parse(item.AllocateQty.ToString());
                            rackinv.OnhandQty+= Decimal.Parse(item.AllocateQty.ToString());
                            rackinv.BalanceQty-= Decimal.Parse(item.AllocateQty.ToString());
                        }
                    }                    
                              
                }
                
            }
            try
            {
                enty.SaveChanges();
                status = true;
            }
            catch (Exception exp)
            {
                status = false;
                throw;
            }
            
            return new JsonResult { Data = new { status = status } };
            
            //return this.Json(new { updateddata = things }, JsonRequestBehavior.AllowGet);



        }
        [HttpPost]
        public JsonResult ApproveShuffleRequest(List<Approvedata> things)
        {
            bool status = false;
            InventoryRepo inventoryRepo = new InventoryRepo();
            //inventoryRepo.LockCM(things);
            foreach (Approvedata item in things)
            {
                int inv_pk = int.Parse(item.InventoryItem_PK.ToString());
                int rack_pk = int.Parse(item.Rack_PK.ToString());
                int newrack = int.Parse(item.NewRack_PK.ToString());
                var inv = from inventory in enty.InventoryMasters
                          where inventory.InventoryItem_PK == inv_pk
                          select inventory;

                foreach (var element in inv)
                {
                    if (!enty.RackInventoryMasters.Any(f => f.Inventoryitem_PK == element.InventoryItem_PK && f.Rack_PK ==newrack ))
                    {
                        RackInventoryMaster rackInventoryMaster = new RackInventoryMaster();
                        rackInventoryMaster.Inventoryitem_PK = element.InventoryItem_PK;
                        rackInventoryMaster.MrnDet_Pk = element.MrnDet_PK;
                        rackInventoryMaster.SkuDet_PK = element.SkuDet_Pk;
                        rackInventoryMaster.PoDet_PK = element.PoDet_PK;
                        rackInventoryMaster.OnhandQty = Decimal.Parse(item.AllocateQty.ToString());
                        rackInventoryMaster.MrnQty = Decimal.Parse(element.ReceivedQty.ToString());
                        rackInventoryMaster.ReceivedQty = Decimal.Parse(item.AllocateQty.ToString());
                        rackInventoryMaster.BalanceQty = 0;
                        rackInventoryMaster.ReceivedVia = "Rack Shuffle";
                        rackInventoryMaster.Location_PK = element.Location_PK;
                        rackInventoryMaster.Rack_PK = int.Parse(item.NewRack_PK.ToString());
                        rackInventoryMaster.RefNum = element.Refnum;
                        rackInventoryMaster.DeliveredQty = 0;
                        rackInventoryMaster.IsLast = "Y";
                        enty.RackInventoryMasters.Add(rackInventoryMaster);
                        var q1 = from rack in enty.RackInventoryMasters where rack.Inventoryitem_PK == element.InventoryItem_PK && rack.Rack_PK == rack_pk select rack;
                        foreach (var rackinv in q1)
                        {
                            rackinv.DeliveredQty += Decimal.Parse(item.AllocateQty.ToString());
                            rackinv.OnhandQty -= Decimal.Parse(item.AllocateQty.ToString());
                        }
                    }
                    else
                    {
                        var q = from rack in enty.RackInventoryMasters where rack.Inventoryitem_PK == element.InventoryItem_PK && rack.Rack_PK == newrack select rack;
                        foreach (var rackinv in q)
                        {
                            rackinv.ReceivedQty += Decimal.Parse(item.AllocateQty.ToString());
                            rackinv.OnhandQty += Decimal.Parse(item.AllocateQty.ToString());                            
                        }
                        var q2 = from rack in enty.RackInventoryMasters where rack.Inventoryitem_PK == element.InventoryItem_PK && rack.Rack_PK == rack_pk select rack;
                        foreach (var rackinv in q2)
                        {
                            rackinv.DeliveredQty += Decimal.Parse(item.AllocateQty.ToString());
                            rackinv.OnhandQty -= Decimal.Parse(item.AllocateQty.ToString());
                        }
                    }

                }

            }
            try
            {
                enty.SaveChanges();
                status = true;
            }
            catch (Exception exp)
            {
                status = false;
                throw;
            }

            return new JsonResult { Data = new { status = status } };

            //return this.Json(new { updateddata = things }, JsonRequestBehavior.AllowGet);



        }
    }
}