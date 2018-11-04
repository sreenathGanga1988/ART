using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using ArtWebApp.Areas.Inventory.ViewModel;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.Inventory.Controllers
{
    public class MCRController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew();
        // GET: Inventory/MCR
        public ActionResult Index()
        {
            ViewBag.AtcID = new SelectList(enty.AtcMasters.Where(u=> u.IsShipmentCompleted=="Y" && u.IsMCRDone!="Y").ToList(), "AtcId", "AtcNum");
            ViewBag.Locid = new SelectList(enty.LocationMasters.Where(u=> u.LocType=="W").ToList(), "location_pk", "locationname");
            ViewBag.ToLocid = new SelectList(enty.LocationMasters.Where(u => u.LocType == "W").ToList(), "location_pk", "locationname");
            return View();
        }

        public ActionResult EditIndex()
        {
            ViewBag.AtcID = new SelectList(enty.AtcMasters.Where(u=> u.IsShipmentCompleted=="Y" && u.IsMCRDone=="Y" ).ToList(), "AtcId", "AtcNum");
            ViewBag.Locid = new SelectList(enty.LocationMasters.Where(u=> u.LocType=="W").ToList(), "location_pk", "locationname");
            ViewBag.Mcr_pk = new SelectList(enty.MCR_Master.Where(u => u.IsTransfer == "N").ToList(), "MCR_Pk", "MCR_no");
            return View();
        }
        public ActionResult MCRRollIndex()
        {
            ViewBag.Mcr_pk = new SelectList(enty.MCR_Master.Where(u => u.IsTransfer == "N").ToList(), "MCR_Pk", "MCR_no");
            return View();
        }
        public ActionResult TransferIndex()
        {            
            ViewBag.Mcr_pk= new SelectList(enty.MCR_Master.Where(u=> u.IsTransfer == "N").ToList(), "MCR_Pk", "MCR_no");
            return View();
        }

        public ActionResult ReceiveIndex()
        {
            ViewBag.Mcr_pk = new SelectList(enty.MCR_Master.Where(u => u.IsTransfer == "Y"  && u.IsReceived=="N").ToList(), "MCR_Pk", "MCR_no");
            return View();
        }
        public ActionResult ApproveIndex()
        {
            ViewBag.AtcID = new SelectList(enty.AtcMasters.Where(u => u.IsShipmentCompleted == "Y" && u.IsMCRDone == "Y").ToList(), "AtcId", "AtcNum");
            ViewBag.Locid = new SelectList(enty.LocationMasters.Where(u => u.LocType == "W").ToList(), "location_pk", "locationname");
            ViewBag.Mcr_pk = new SelectList(enty.MCR_Master.Where(u => u.IsReceived == "Y" && u.IsApproved=="N").ToList(), "MCR_Pk", "MCR_no");
            return View();
        }

        public ActionResult ConfirmIndex()
        {
            ViewBag.AtcID = new SelectList(enty.AtcMasters.Where(u => u.IsShipmentCompleted == "Y" && u.IsMCRDone == "Y").ToList(), "AtcId", "AtcNum");
            ViewBag.Locid = new SelectList(enty.LocationMasters.Where(u => u.LocType == "W").ToList(), "location_pk", "locationname");
            ViewBag.Mcr_pk = new SelectList(enty.MCR_Master.Where(u => u.IsApproved == "Y" && u.IsConfirmed== "N").ToList(), "MCR_Pk", "MCR_no");
            return View();
        }
        [HttpGet]
        public JsonResult GetATCList(int Id)
        {
            //int locationpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());

            
            SelectList atclist = new SelectList(enty.MCRDetails.Where(f =>f.Location_pk==Id).Distinct().ToList(), "AtcId", "AtcNum");
        
            JsonResult jsd = Json(atclist, JsonRequestBehavior.AllowGet);

            return jsd;

        }


        [HttpGet]
        public PartialViewResult GetATCwiseRollDetail(int mcrpk,int atcid)
        {

            AtcwiseFabricInventory atcwiseFabricInventory = new AtcwiseFabricInventory();
            ArrayList popaklist = new ArrayList();
            DataTable rolldt = new DataTable();
            InventoryRepo inventoryRepo = new InventoryRepo();
            int locid = 0;
            int tolocid = 0;
            
            var q = from mcr in enty.MCR_Master where mcr.MCR_Pk == mcrpk select mcr;
            foreach(var element in q)
            {
                locid = int.Parse(element.Location_pk.ToString());
                atcid= int.Parse(element.Atc_Id.ToString());
                tolocid= int.Parse(element.ToLocation_pk .ToString());
            }
            DataTable dt = inventoryRepo.GetATCwiseFabricInventory(locid, atcid, tolocid);            
            if (dt != null)
            {                
                    foreach (DataRow row in dt.Rows)
                    {
                        int invitem_pk = int.Parse(row["InventoryItem_PK"].ToString());

                        popaklist.Add(invitem_pk);
                    }
                    string conditionatc = " and ( ";

                    for (int i = 0; i < popaklist.Count; i++)
                    {
                        if (i == 0)
                        {
                            conditionatc = conditionatc + " InventoryMaster.InventoryItem_PK =" + popaklist[i].ToString().Trim() + "";
                        }

                    else
                    {
                        conditionatc = conditionatc + " or InventoryMaster.InventoryItem_PK =" + popaklist[i].ToString().Trim() + "";
                    }
                }
                    conditionatc = conditionatc + ")";
                    if (conditionatc == "and()")
                    {
                        conditionatc = "";
                    }


                    rolldt = inventoryRepo.getFabricRollofAItemPK(conditionatc, locid, mcrpk);

            }



                if (dt != null )
                {
                atcwiseFabricInventory.rolldetails = rolldt;                    
                }
                else
                {
                    TempData["shortMessage"] = "MCR AlreadyGenerated";
                }
                return PartialView("MCRRollDetails_P", atcwiseFabricInventory);
            
        }

        [HttpGet]
        public PartialViewResult GetATCwiseFabricInventory(int locid, int atcid, int Tolocid)
        {

            AtcwiseFabricInventory atcwiseFabricInventory = new AtcwiseFabricInventory();
            ArrayList popaklist = new ArrayList();
            DataTable rolldt = new DataTable();
            InventoryRepo inventoryRepo = new InventoryRepo();
            DataTable dt = inventoryRepo.GetATCwiseFabricInventory(locid, atcid,Tolocid);
            dt.Columns.Add("AlterUOM");
            dt.Columns.Add("UOMQty");            
            DataTable trimdt = inventoryRepo.GetATCwiseTrimsInventory(locid, atcid,Tolocid);
            


                if (dt != null && trimdt != null)
                {
                foreach (DataRow row in dt.Rows)
                {
                    String UomCode = row["UomCode"].ToString();
                    
                    if(UomCode == "KGS"){
                        row["AlterUOM"] = "YDS";
                        row["UOMQty"] = 100;
                    }
                    else if(UomCode == "YDS")
                    {
                        row["AlterUOM"] = "KGS";
                        row["UOMQty"] = 200;
                    }

                }
                    atcwiseFabricInventory.InventoryDetails = dt;
                
                    atcwiseFabricInventory.TrimsInventoryDetails = trimdt;
                }
                else
                {
                    TempData["shortMessage"] = "MCR AlreadyGenerated";
                }
                return PartialView("MCRDetails_P", atcwiseFabricInventory);
            
        }
        [HttpGet]
        public PartialViewResult GetRollDetails(int Mcr_pk)
        {

            AtcwiseFabricInventory atcwiseFabricInventory = new AtcwiseFabricInventory();
            ArrayList popaklist = new ArrayList();
            DataTable rolldt = new DataTable();
            InventoryRepo inventoryRepo = new InventoryRepo();
            int locid = 0;

            var q = from mcrdet in enty.MCRDetails where mcrdet.Mcr_pk == Mcr_pk select mcrdet;
            foreach(var element in q)
            {            
                int invitem_pk = int.Parse(element.InventoryItem_pk.ToString());
                popaklist.Add(invitem_pk);
                locid = int.Parse(element.Location_pk.ToString());
            }
                string conditionatc = " and ( ";

                for (int i = 0; i < popaklist.Count; i++)
                {
                    if (i == 0)
                    {
                        conditionatc = conditionatc + " InventoryMaster.InventoryItem_PK =" + popaklist[i].ToString().Trim() + "";
                    }

                    else
                    {
                        conditionatc = conditionatc + " or InventoryMaster.InventoryItem_PK =" + popaklist[i].ToString().Trim() + "";
                    }
                }
                conditionatc = conditionatc + ")";
                if (conditionatc == "and()")
                {
                    conditionatc = "";
                }


                rolldt = inventoryRepo.getFabricRollofAItemPK(conditionatc, locid, Mcr_pk);

            

            if (rolldt != null)
            {
                atcwiseFabricInventory.rolldetails = rolldt;
            }
            else
            {
                TempData["shortMessage"] = "MCR AlreadyGenerated";
            }
            return PartialView("MCRRollDetails_P", atcwiseFabricInventory);

        }

        [HttpGet]
        public PartialViewResult GetMCRDetails(int Mcr_pk)
        {
            int atcid = 0;
            int locid = 0;
            GetMcrInventory  getMcrInventory = new GetMcrInventory();

            InventoryRepo inventoryRepo = new InventoryRepo();
            var q = from mcr in enty.MCR_Master where mcr.MCR_Pk == Mcr_pk select mcr;
            foreach(var element in q)
            {
                atcid = int.Parse(element.Atc_Id.ToString());
                locid= int.Parse(element.Location_pk.ToString());
            }
            DataTable dt = inventoryRepo.FabricInventoryEdit (locid,atcid,  Mcr_pk);
            DataTable trimdt = inventoryRepo.TrimsInventoryEdit(locid,atcid, Mcr_pk);
            dt.Columns.Add("AlterUOM");
            dt.Columns.Add("UOMQty");
            if (dt != null && trimdt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    String UomCode = row["UomCode"].ToString();
                    if (UomCode == "KGS")
                    {
                        row["AlterUOM"] = "YDS";
                        row["UOMQty"] = 100;
                    }
                    else if (UomCode == "YDS")
                    {
                        row["AlterUOM"] = "KGS";
                        row["UOMQty"] = 200;
                    }

                }
                getMcrInventory.InventoryDetails = dt;
                getMcrInventory.TrimsInventoryDetails = trimdt;
            }
            else
            {
                TempData["shortMessage"] = "MCR AlreadyGenerated";
            }
            return PartialView("MCREdit_P", getMcrInventory);
        }
        [HttpGet]
        public PartialViewResult GetTransferMCRDetails(int mcr_pk)
        {

            GetMcrInventory getMcrInventory = new GetMcrInventory();

            InventoryRepo inventoryRepo = new InventoryRepo();
            DataTable dt = inventoryRepo.MCRTransferFabricInventory(mcr_pk);
            DataTable trimdt = inventoryRepo.MCRTransferTrimsInventory(mcr_pk);

            if (dt != null && trimdt != null)
            {
                getMcrInventory.InventoryDetails = dt;
                getMcrInventory.TrimsInventoryDetails = trimdt;
            }
            else
            {
                TempData["shortMessage"] = "MCR AlreadyGenerated";
            }
            return PartialView("TransferDetails_P", getMcrInventory);
        }

        [HttpGet]
        public PartialViewResult GetTransferMCRRollDetails(int mcr_pk)
        {

            GetMcrInventory getMcrInventory = new GetMcrInventory();

            InventoryRepo inventoryRepo = new InventoryRepo();
            DataTable dt = inventoryRepo.MCRTransferRollInventory(mcr_pk);

            if (dt != null)
            {
                getMcrInventory.rolldetails = dt;
            }
            else
            {
                TempData["shortMessage"] = "MCR AlreadyGenerated";
            }
            return PartialView("TransferRollDetails_P", getMcrInventory);
        }


        [HttpGet]
        public PartialViewResult GetReceiveMCRDetails(int mcr_pk)
        {

            GetMcrInventory getMcrInventory = new GetMcrInventory();

            InventoryRepo inventoryRepo = new InventoryRepo();
            DataTable dt = inventoryRepo.MCRTransferFabricInventory(mcr_pk);
            DataTable trimdt = inventoryRepo.MCRTransferTrimsInventory(mcr_pk);

            if (dt != null && trimdt != null)
            {
                getMcrInventory.InventoryDetails = dt;
                getMcrInventory.TrimsInventoryDetails = trimdt;
            }
            else
            {
                TempData["shortMessage"] = "MCR AlreadyGenerated";
            }
            return PartialView("ReceiveDetails_P", getMcrInventory);
        }
        [HttpGet]
        public PartialViewResult GetReceiveMCRRollDetails(int mcr_pk)
        {

            GetMcrInventory getMcrInventory = new GetMcrInventory();

            InventoryRepo inventoryRepo = new InventoryRepo();
            DataTable dt = inventoryRepo.MCRTransferRollInventory(mcr_pk);

            if (dt != null)
            {
                getMcrInventory.rolldetails = dt;
            }
            else
            {
                TempData["shortMessage"] = "MCR AlreadyGenerated";
            }
            return PartialView("ReceiveRollDetails_P", getMcrInventory);
        }
        [HttpGet]
        public PartialViewResult LoadMCRDetails(int Mcr_pk)
        {

            GetMcrInventory getMcrInventory = new GetMcrInventory();

            InventoryRepo inventoryRepo = new InventoryRepo();
            DataTable dt = inventoryRepo.MCRFabricInventory(Mcr_pk);
            DataTable trimdt = inventoryRepo.MCRTrimsInventory(Mcr_pk);

            if (dt != null && trimdt != null)
            {
                getMcrInventory.InventoryDetails = dt;
                getMcrInventory.TrimsInventoryDetails = trimdt;
            }
            else
            {
                TempData["shortMessage"] = "MCR AlreadyGenerated";
            }
            return PartialView("ApproveView_P", getMcrInventory);
        }

        [HttpGet]
        public PartialViewResult GetApprovedMCR(int Mcr_pk)
        {

            GetMcrInventory getMcrInventory = new GetMcrInventory();

            InventoryRepo inventoryRepo = new InventoryRepo();
            DataTable dt = inventoryRepo.ApprovedMCRFabricInventory(Mcr_pk);
            DataTable trimdt = inventoryRepo.ApprovedMCRTrimsInventory(Mcr_pk);

            if (dt != null && trimdt != null)
            {
                getMcrInventory.InventoryDetails = dt;
                getMcrInventory.TrimsInventoryDetails = trimdt;
            }
            else
            {
                TempData["shortMessage"] = "MCR AlreadyGenerated";
            }
            return PartialView("GetApprovedMCR_P", getMcrInventory);
        }

        [HttpPost]
        public JsonResult Create(List<FabricInventoryList> things)
        {            
            string  status = "";
            int atcid = 0;
            int inventory_Pk = 0;
            int location = 0;
            int mcrpk = 0;
            string Donum = "";
            int ToLocid = 0;
            try
            {
                foreach (FabricInventoryList fablist in things)
                {
                    atcid = int.Parse(fablist.AtcId.ToString());
                    inventory_Pk = int.Parse(fablist.InventoryItem_PK.ToString());
                    location = int.Parse(fablist.Location.ToString());
                    ToLocid = int.Parse(fablist.ToLocid.ToString());


                    if (!enty.MCR_Master.Any(f => f.Atc_Id == atcid && f.Location_pk == location))
                    {
                        MCR_Master mCR_Master = new MCR_Master();
                        mCR_Master.Atc_Id = atcid;
                        mCR_Master.Location_pk = location;
                        mCR_Master.AddedDate = DateTime.Now;
                        mCR_Master.AddedBy = HttpContext.Session["Username"].ToString();
                        mCR_Master.IsReceived = "N";
                        mCR_Master.IsTransfer = "N";
                        mCR_Master.IsApproved = "N";
                        mCR_Master.IsConfirmed= "N";
                        mCR_Master.ToLocation_pk = ToLocid;
                        enty.MCR_Master.Add(mCR_Master);
                        enty.SaveChanges();
                        Donum = mCR_Master.MCR_no = "MCR" + mCR_Master.MCR_Pk.ToString().PadLeft(6, '0');
                        mcrpk= int.Parse(mCR_Master.MCR_Pk.ToString()); 
                    }
                    if (!enty.MCRDetails.Any(f=>f.Atcid ==atcid && f.Location_pk==location && f.InventoryItem_pk==inventory_Pk))
                    {
                        MCRDetail mCRDetail = new MCRDetail();
                        mCRDetail.InventoryItem_pk = int.Parse(fablist.InventoryItem_PK.ToString());
                        mCRDetail.DeliveredQty = Decimal.Parse(fablist.DeliveredQty.ToString());                            
                        mCRDetail.Atcid = int.Parse(fablist.AtcId.ToString());
                        mCRDetail.Location_pk = int.Parse(fablist.Location.ToString());
                        mCRDetail.ItemColor = fablist.ItemColor;
                        mCRDetail.Onhandqty = Decimal.Parse(fablist.OnhandQty.ToString());
                        mCRDetail.PhysicalQty = Decimal.Parse(fablist.PhysicalQty.ToString());
                        mCRDetail.ReceivedQty = Decimal.Parse(fablist.ReceivedQty.ToString());
                        mCRDetail.RMNum = fablist.RMNum;
                        mCRDetail.SupplierColor = fablist.SupplierColor;
                        mCRDetail.UOM = fablist.UomCode;
                        mCRDetail.DiffQty = Decimal.Parse(fablist.DiffQty.ToString());
                        mCRDetail.Description = fablist.Description;
                        mCRDetail.type = fablist.Type;
                        mCRDetail.AddedDate = DateTime.Now;
                        mCRDetail.Addedby= HttpContext.Session["Username"].ToString();
                        mCRDetail.ActualCU_Rate = Decimal.Parse(fablist.ActualCURate.ToString());
                        mCRDetail.CU_Rate = Decimal.Parse(fablist.CURate.ToString());
                        mCRDetail.Template_pk= int.Parse(fablist.Template_Pk.ToString());
                        mCRDetail.Skudet_pk= int.Parse(fablist.Skudet_Pk.ToString());
                        
                        if (fablist.Type == "F")
                        {
                            mCRDetail.AlterUOM_qty = Decimal.Parse(fablist.AlterUOM_Qty.ToString());
                            mCRDetail.AlterUOM = fablist.AlterUOM;
                            mCRDetail.Packages = fablist.Packages;
                        }
                        else
                        {
                            mCRDetail.AlterUOM_qty = 0;

                        }
                        mCRDetail.Mcr_pk = mcrpk;
                        enty.MCRDetails.Add(mCRDetail);
                        enty.SaveChanges();
                    }
                    else
                    {

                        var q = from mcrdetails in enty.MCRDetails
                                where mcrdetails.Atcid == atcid & mcrdetails.Location_pk == location && mcrdetails.InventoryItem_pk == inventory_Pk
                                select mcrdetails;
                        foreach(var element in q)
                        {
                            element.PhysicalQty = Decimal.Parse(fablist.PhysicalQty.ToString());
                            element.DiffQty = Decimal.Parse(fablist.DiffQty.ToString());
                        }
                    }
                    }
                //}
                enty.SaveChanges();
                var atc = from atcmaster in enty.AtcMasters where atcmaster.AtcId == atcid select atcmaster;
                foreach(var element in atc)
                {
                    element.IsMCRDone = "Y";                    
                }
                enty.SaveChanges();
            }
            catch (Exception exp)
            {
                status = "MCR Not Generated " ;
                throw;
            }
            status = "MCR Generated Successfully " + Donum ;
                        
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult UpdateRolls(List<rolllist > things)
        {
            string status = "";
            int atcid = 0;
            int roll_pk = 0;
            int location = 0;
            int mcrpk = 0;
            int inv_pk = 0;
            int mrndet_pk = 0;
            string Rollnum = "";
            decimal ayard = 0;
            try
            {
                foreach (rolllist  roll in things)
                {
                   
                    roll_pk= int.Parse(roll.Roll_pk.ToString());
                    mcrpk= int.Parse(roll.MCR_pk.ToString());
                    inv_pk =int.Parse(roll.InventoryItem_Pk .ToString());
                    var q = from mcr in enty.MCR_Master where mcr.MCR_Pk == mcrpk select mcr;
                    foreach(var ele in q)
                    {
                        location = int.Parse(ele.Location_pk.ToString());
                    }
                    

                    var r = from rolldetails in enty.FabricRollmasters
                            where rolldetails.Roll_PK == roll_pk
                            select rolldetails;
                    foreach(var element in r)
                    {
                        Rollnum = element.RollNum;
                        mrndet_pk = int.Parse (element.MRnDet_PK.ToString ());
                        ayard = int.Parse (element.AYard.ToString ());
                    }
                    if (!enty.MCRRollDetails .Any(f => f.Roll_pk  == roll_pk  && f.Location_pk == location))
                    {
                        MCRRollDetail mCRRollDetail = new MCRRollDetail();
                        mCRRollDetail.Roll_pk = roll_pk;
                        mCRRollDetail.Mcr_pk = mcrpk ;
                        mCRRollDetail.RollNum  = Rollnum;
                        mCRRollDetail.Inventoryitem_pk  = inv_pk;
                        mCRRollDetail.Mrndet_pk  = mrndet_pk ;
                        mCRRollDetail.Location_pk  = location ;
                        mCRRollDetail.AddedBy = HttpContext.Session["Username"].ToString();
                        mCRRollDetail.AddedDate = DateTime.Now;
                        mCRRollDetail.IsReceived = "N";
                        mCRRollDetail.IsConfirm= "N";
                        mCRRollDetail.AYard = ayard;
                        enty.MCRRollDetails.Add(mCRRollDetail);

                    }
                }

                enty.SaveChanges();
            }
            catch (Exception exp)
            {
                status = "MCR Not Generated ";
                throw;
            }
            status = "Roll Details Updated Successfully ";

            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult Edit(List<FabricInventoryList> things)
        {
            string status = "";
            int atcid = 0;
            int inventory_Pk = 0;
            int location = 0;
            int mcrpk = 0;
            string Donum = "";
            try
            {
                foreach (FabricInventoryList fablist in things)
                {
                    atcid = int.Parse(fablist.AtcId.ToString());
                    inventory_Pk = int.Parse(fablist.InventoryItem_PK.ToString());
                    location = int.Parse(fablist.Location.ToString());
                    mcrpk = int.Parse(fablist.MCRDetails_Pk.ToString());

                    if (!enty.MCRDetails.Any(f => f.Atcid == atcid && f.Location_pk == location && f.InventoryItem_pk == inventory_Pk))
                    {
                        MCRDetail mCRDetail = new MCRDetail();
                        mCRDetail.InventoryItem_pk = int.Parse(fablist.InventoryItem_PK.ToString());
                        mCRDetail.DeliveredQty = Decimal.Parse(fablist.DeliveredQty.ToString());
                        mCRDetail.Atcid = int.Parse(fablist.AtcId.ToString());
                        mCRDetail.Location_pk = int.Parse(fablist.Location.ToString());
                        mCRDetail.ItemColor = fablist.ItemColor;
                        mCRDetail.Onhandqty = Decimal.Parse(fablist.OnhandQty.ToString());
                        mCRDetail.PhysicalQty = Decimal.Parse(fablist.PhysicalQty.ToString());
                        mCRDetail.ReceivedQty = Decimal.Parse(fablist.ReceivedQty.ToString());
                        mCRDetail.RMNum = fablist.RMNum;
                        mCRDetail.SupplierColor = fablist.SupplierColor;
                        mCRDetail.UOM = fablist.UomCode;
                        mCRDetail.DiffQty = Decimal.Parse(fablist.DiffQty.ToString());
                        mCRDetail.Description = fablist.Description;
                        mCRDetail.type = fablist.Type;
                        mCRDetail.AddedDate = DateTime.Now;
                        mCRDetail.Addedby = HttpContext.Session["Username"].ToString();
                        mCRDetail.ActualCU_Rate = Decimal.Parse(fablist.ActualCURate.ToString());
                        mCRDetail.CU_Rate = Decimal.Parse(fablist.CURate.ToString());
                        mCRDetail.Template_pk = int.Parse(fablist.Template_Pk.ToString());
                        mCRDetail.Skudet_pk = int.Parse(fablist.Skudet_Pk.ToString());

                        if (fablist.Type == "F")
                        {
                            mCRDetail.AlterUOM_qty = Decimal.Parse(fablist.AlterUOM_Qty.ToString());
                            mCRDetail.AlterUOM = fablist.AlterUOM;
                            mCRDetail.Packages = fablist.Packages;
                        }
                        else
                        {
                            mCRDetail.AlterUOM_qty = 0;

                        }
                        mCRDetail.Mcr_pk = mcrpk;
                        enty.MCRDetails.Add(mCRDetail);
                        enty.SaveChanges();
                    }
                    else
                    {

                        var q = from mcrdetails in enty.MCRDetails
                                where mcrdetails.Atcid == atcid & mcrdetails.Location_pk == location && mcrdetails.InventoryItem_pk == inventory_Pk
                                select mcrdetails;
                        foreach (var element in q)
                        {
                            element.PhysicalQty = Decimal.Parse(fablist.PhysicalQty.ToString());
                            element.DiffQty = Decimal.Parse(fablist.DiffQty.ToString());
                        }
                    }
                }
                //}
                enty.SaveChanges();
               
            }
            catch (Exception exp)
            {
                status = "MCR Not Generated ";
                throw;
            }
            status = "MCR Updated Successfully ";

            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult UpdateReceive(List<FabricInventoryList> things)
        {
            bool status = false;
            int mcrpk = 0;
            int mcrdet_pk = 0;

            try
            {
                foreach (FabricInventoryList fablist in things)
                {

                    mcrdet_pk = int.Parse(fablist.MCRDetails_Pk.ToString());

                    var q = from mcrdetails in enty.MCRDetails
                            where mcrdetails.McrDetails_pk == mcrdet_pk
                            select mcrdetails;
                    foreach (var element in q)
                    {
                        element.ActualReceive = Decimal.Parse(fablist.PhysicalQty.ToString());
                        element.ActualDiffQty = Decimal.Parse(fablist.DiffQty.ToString());
                        mcrpk = int.Parse(element.Mcr_pk.ToString());

                    }

                }
                
            }
            catch (Exception exp)
            {

                throw;
            }
            status = true;

            return new JsonResult { Data = new { status = status } };
        }
        public JsonResult Receive(List<FabricInventoryList> things)
        {
            bool status = false;
            int mcrpk = 0;
            int mcrdet_pk = 0;

            try
            {
                foreach (FabricInventoryList fablist in things)
                {

                    mcrdet_pk = int.Parse(fablist.MCRDetails_Pk.ToString());

                    var q = from mcrdetails in enty.MCRDetails
                            where mcrdetails.McrDetails_pk == mcrdet_pk
                            select mcrdetails;
                    foreach (var element in q)
                    {
                        element.ActualReceive  = Decimal.Parse(fablist.PhysicalQty.ToString());
                        element.ActualDiffQty  = Decimal.Parse(fablist.DiffQty.ToString());
                        mcrpk = int.Parse(element.Mcr_pk.ToString());

                    }

                }
                var q1 = from mcr in enty.MCR_Master where mcr.MCR_Pk == mcrpk select mcr;
                foreach(var e1 in q1)
                {
                    e1.IsReceived = "Y";
                    e1.ReceivedDate= DateTime.Now;
                    e1.ReceivedBy = HttpContext.Session["Username"].ToString();

                }
                enty.SaveChanges();
            }
            catch (Exception exp)
            {

                throw;
            }
            status = true;

            return new JsonResult { Data = new { status = status } };
        }

        public JsonResult UpdateReceiveRolls(List<rolllist> things)
        {
            string status = "";
            int atcid = 0;
            int roll_pk = 0;
            int location = 0;
            int mcrpk = 0;
            int inv_pk = 0;
            int mrndet_pk = 0;
            string Rollnum = "";
            decimal RYard = 0;
            decimal Mcr_kg = 0;
            try
            {
                foreach (rolllist roll in things)
                {

                    roll_pk = int.Parse(roll.Roll_pk.ToString());
                    mcrpk = int.Parse(roll.MCR_pk.ToString());
                    inv_pk = int.Parse(roll.InventoryItem_Pk.ToString());
                    RYard = int.Parse(roll.RYard.ToString());
                    Mcr_kg = int.Parse(roll.Mcr_kg.ToString());
                    

                    var r = from mroll in enty.MCRRollDetails where mroll.Mcr_pk == mcrpk && mroll.Roll_pk ==roll_pk select mroll;
                    foreach(var element in r)
                    {
                        element.IsReceived = "Y";
                        element.McrYard = RYard;
                        element.Mcr_kg = Mcr_kg;
                    }
                   
                }

                enty.SaveChanges();
                status = "Roll Details Updated Successfully ";
            }
            catch (Exception exp)
            {
                status = exp + "Roll not Updated Generated ";
                throw;
            }
            

            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult Transfer(List<FabricInventoryList> things)
        {
            bool status = false;
            int mcrpk = 0;
            try
            {
                foreach (FabricInventoryList fablist in things)
                {

                    mcrpk = int.Parse(fablist.MCRDetails_Pk.ToString());

                    var q = from mcrmas in enty.MCR_Master
                            where mcrmas.MCR_Pk== mcrpk
                            select mcrmas;
                    foreach (var element in q)
                    {
                        element.IsTransfer = "Y";
                        element.TransferDate= DateTime.Now;
                        element.TransferBy = HttpContext.Session["Username"].ToString();

                    }

                }
               

                enty.SaveChanges();
            }
            catch (Exception exp)
            {

                throw;
            }
            status = true;

            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult Confirm(List<FabricInventoryList> things)
        {
            bool status = false;
            int mcrpk = 0;
            int mcrdet_pk = 0;
            int atcid = 0;
            int locid = 0;
            int tolocid = 0;
            try
            {
                foreach (FabricInventoryList fablist in things)
                {

                    mcrdet_pk = int.Parse(fablist.MCRDetails_Pk.ToString());

                    var q = from mcrdetails in enty.MCRDetails
                            where mcrdetails.McrDetails_pk == mcrdet_pk
                            select mcrdetails;
                    foreach (var element in q)
                    {
                        element.ActualCU_Rate = Decimal.Parse(fablist.ActualCURate.ToString());
                        //element.CU_Rate= Decimal.Parse(fablist.CURate.ToString());
                        element.CU_Rate= Decimal.Parse(fablist.ActualCURate.ToString());
                        atcid = int.Parse(element.Atcid.ToString());
                        locid = int.Parse(element.Location_pk.ToString());                        
                        mcrpk = int.Parse(element.Mcr_pk.ToString());
                     
                    }

                }

                var q1 = from mcr in enty.MCR_Master where mcr.MCR_Pk == mcrpk select mcr;
                foreach (var e1 in q1)
                {
                    e1.IsConfirmed = "Y";
                    e1.ConfirmDate = DateTime.Now;
                    e1.ConfirmBy= HttpContext.Session["Username"].ToString();
                    tolocid = int.Parse(e1.ToLocation_pk.ToString());
                }
                enty.SaveChanges();
                if (locid == 13)
                {
                    transferQty(things, atcid, locid);
                }
                else
                {
                    transferQty(things, atcid, tolocid);
                }
                

            }
            catch (Exception exp)
            {

                throw;
            }
            status = true;

            return new JsonResult { Data = new { status = status } };
        }


        public void transferQty(List<FabricInventoryList> things,int atcid,int location_pk)
        {
            int mrndet_pk = 0;

            int podet_pk = 0;
            int uom_pk = 0;
            decimal curate = 0;
            int skudetpk = 0;
            int mcrpk = 0;
            String Construction = "";
            String Composition = "";
            String ItemColor = "";
            String ItemSize = "";
            String TransferNumber = "";
            String MRNNum = "";

            int templete_PK = 0;
            int MCRDetails_Pk = 0;
            int InventoryItem_PK = 0;
            Decimal NewUnitprice = 0;
            Decimal receivedQty = 0;
            Decimal OnhandQty = 0;
            var locationmame = "";

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var loc = from locmaster in enty.LocationMasters
                          where locmaster.Location_PK == location_pk
                          select locmaster;
                foreach(var Location in loc)
                {
                    locationmame = Location.LocationPrefix;   
                }
                TransferNumber = "MCR" + locationmame + atcid;
                StockPOMaster spomstr = new StockPOMaster();
                spomstr.Supplier_Pk = 1113;
                spomstr.DeliveryDate = DateTime.Now;
                spomstr.DeliveryTerms_Pk = 1;
                spomstr.DeliveryMethod_Pk = 1;
                spomstr.PaymentTermID = 3;
                spomstr.PO_value = 0;
                spomstr.Location_PK = location_pk;
                spomstr.CurrencyID = 18;
                spomstr.Remark = "Po against MCR Closing";
                spomstr.AddedBy = "Admin";
                spomstr.AddedDate = DateTime.Now;
                spomstr.IsApproved = "Y";
                spomstr.IsDeleted = "N";
                spomstr.SPONum = TransferNumber;
                enty.StockPOMasters.Add(spomstr);
                enty.SaveChanges();

                StockMrnMaster smrnmstrdb = new StockMrnMaster();
                smrnmstrdb.DoNumber = TransferNumber;
                smrnmstrdb.SMrnNum = TransferNumber;
                smrnmstrdb.AddedDate = DateTime.Now;
                smrnmstrdb.SPo_PK = spomstr.SPO_Pk;
                smrnmstrdb.AddedBY = "Admin";

                smrnmstrdb.Location_Pk = location_pk;
                smrnmstrdb.SReciept_Pk = 30122;
                enty.StockMrnMasters.Add(smrnmstrdb);
                enty.SaveChanges();


                foreach (FabricInventoryList trdet in things)
                {
                    MCRDetails_Pk = int.Parse(trdet.MCRDetails_Pk.ToString());
                    var q = from mcrdetails in enty.MCRDetails
                            where mcrdetails.McrDetails_pk == MCRDetails_Pk
                            select mcrdetails;
                    foreach(var conmcr in q)
                    {
                        conmcr.IsConfirm = "Y";
                        conmcr.ConfirmedBy = HttpContext.Session["Username"].ToString(); 
                        conmcr.ConfirmedDate = DateTime.Now.Date;
                        mcrpk = int.Parse(conmcr.Mcr_pk.ToString());
                    }
                    foreach (var mcr in q)
                    {
                        templete_PK = int.Parse(mcr.Template_pk.ToString());
                        NewUnitprice = Decimal.Parse(mcr.CU_Rate.ToString());
                        receivedQty = Decimal.Parse(mcr.ActualReceive.ToString());
                        OnhandQty = Decimal.Parse(mcr.Onhandqty.ToString());
                        InventoryItem_PK = int.Parse(mcr.InventoryItem_pk.ToString());                        
                    }
                    

                    var existinginventory = from invitem in enty.InventoryMasters
                                            where invitem.InventoryItem_PK == InventoryItem_PK
                                            select invitem;

                    foreach (var invitemdetail in existinginventory)
                    {

                        invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + OnhandQty;
                        invitemdetail.OnhandQty = invitemdetail.OnhandQty - OnhandQty;

                        uom_pk = int.Parse(invitemdetail.Uom_Pk.ToString());
                        skudetpk = int.Parse(invitemdetail.SkuDet_Pk.ToString());
                    }


                    var templatedata = from skudet in enty.SkuRawmaterialDetails
                                       join skumstr in enty.SkuRawMaterialMasters on skudet.Sku_PK equals skumstr.Sku_Pk
                                       where skudet.SkuDet_PK == skudetpk
                                       select new { skumstr.Construction, skumstr.Composition, skudet.ItemColor, skudet.ItemSize };

                    foreach (var elementtemplatedata in templatedata)
                    {
                        Construction = elementtemplatedata.Construction == null ? "" : elementtemplatedata.Construction.ToString();
                        Composition = elementtemplatedata.Composition == null ? "" : elementtemplatedata.Composition.ToString();
                        ItemColor = elementtemplatedata.ItemColor == null ? "" : elementtemplatedata.ItemColor.ToString();
                        ItemSize = elementtemplatedata.ItemSize == null ? "" : elementtemplatedata.ItemSize.ToString();
                    }
                    StockPODetail spodetal = new StockPODetail();
                    spodetal.SPO_PK = spomstr.SPO_Pk;
                    spodetal.Template_PK = templete_PK;

                    spodetal.Unitprice = NewUnitprice;
                    spodetal.POQty = receivedQty;
                    spodetal.Uom_PK = uom_pk;
                    spodetal.CUrate = NewUnitprice;
                    spodetal.Composition = Composition;
                    spodetal.Construct = Construction;
                    spodetal.TemplateColor = ItemColor;
                    spodetal.TemplateSize = ItemSize;
                    enty.StockPODetails.Add(spodetal);
                    enty.SaveChanges();

                    StockMRNDetail smrndetdb = new StockMRNDetail();
                    smrndetdb.SMRN_Pk = smrnmstrdb.SMrn_PK;
                    smrndetdb.SPODetails_PK = spodetal.SPODetails_PK;
                    smrndetdb.SPO_PK = spomstr.SPO_Pk;
                    smrndetdb.ReceivedQty = receivedQty;
                    smrndetdb.Unitprice = NewUnitprice;
                    smrndetdb.Uom_PK = uom_pk;
                    smrndetdb.ExtraQty = 0;
                    enty.StockMRNDetails.Add(smrndetdb);

                    enty.SaveChanges();

                    StockInventoryMaster sinvmstr = new StockInventoryMaster();

                    sinvmstr.SMRNDet_Pk = smrndetdb.SMRNDet_Pk;
                    sinvmstr.SPODetails_PK = spodetal.SPODetails_PK;
                    sinvmstr.Template_PK = templete_PK;
                    sinvmstr.OnHandQty = receivedQty;
                    sinvmstr.ReceivedQty = receivedQty;
                    sinvmstr.DeliveredQty = 0;
                    sinvmstr.Unitprice = smrndetdb.Unitprice;
                    sinvmstr.Composition = Composition;
                    sinvmstr.Construct = Construction;
                    sinvmstr.TemplateColor = ItemColor;
                    sinvmstr.TemplateSize = ItemSize;

                    sinvmstr.Uom_PK = uom_pk;
                    sinvmstr.CuRate = smrndetdb.Unitprice;
                    sinvmstr.ReceivedVia = "GTR";
                    sinvmstr.Location_Pk = smrnmstrdb.Location_Pk;
                    sinvmstr.Refnum = smrnmstrdb.SMrnNum;
                    sinvmstr.AddedDate = DateTime.Now.Date;
                    sinvmstr.ParentRef = trdet.RMNum;
                    enty.StockInventoryMasters.Add(sinvmstr);
                    enty.SaveChanges();


                }
                var r = from mcrroll in enty.MCRRollDetails where mcrroll.Mcr_pk == mcrpk && mcrroll.IsReceived == "Y" select mcrroll;
                foreach(var rolldet in r)
                {
                    RollInventoryMaster rollInventory = new RollInventoryMaster();
                    rollInventory.Roll_PK = rolldet.Roll_pk;
                    rollInventory.Location_Pk = location_pk;
                    rollInventory.DocumentNum = TransferNumber;
                    rollInventory.AddedVia = "MCR";
                    rollInventory.IsPresent = "Y";
                    rollInventory.AddedBy = "Admin";
                    rollInventory.Addeddate = DateTime.Now;
                    enty.RollInventoryMasters.Add(rollInventory);
                    rolldet.IsConfirm = "Y";
                    enty.SaveChanges();
                    var f = from fab in enty.FabricRollmasters where fab.Roll_PK == rolldet.Roll_pk select fab;
                    foreach(var fabdet in f)
                    {
                        fabdet.IsDelivered = "N";
                        enty.SaveChanges();
                    }
                    
                        
                }
            }
        }


        [HttpPost]
        public JsonResult ApproveMCR(List<FabricInventoryList> things)
        {
            bool status = false;
            int mcrpk = 0;
            int mcrdet_pk = 0;
            try
            {
                foreach (FabricInventoryList fablist in things)
                {

                    mcrdet_pk = int.Parse(fablist.MCRDetails_Pk.ToString());

                    var q = from mcrdetails in enty.MCRDetails
                            where mcrdetails.McrDetails_pk == mcrdet_pk
                            select mcrdetails;
                    foreach (var element in q)
                    {

                        element.ApprovedDate= DateTime.Now;
                        element.ApprovedBy= HttpContext.Session["Username"].ToString();
                        element.IsApproved = "Y";
                        mcrpk = int.Parse(element.Mcr_pk.ToString());  
                    }

                }
                var q1 = from mcr in enty.MCR_Master where mcr.MCR_Pk == mcrpk select mcr;
                foreach(var e1 in q1)
                {
                    e1.IsApproved = "Y";
                    e1.ApprovedDate = DateTime.Now;
                    e1.ApprovedBy = HttpContext.Session["Username"].ToString();
                }
                enty.SaveChanges();
            }
            catch (Exception exp)
            {

                throw;
            }
            status = true;

            return new JsonResult { Data = new { status = status } };
        }

       



    }
}   