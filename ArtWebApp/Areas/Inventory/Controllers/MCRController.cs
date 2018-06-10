using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return View();
        }

        public ActionResult EditIndex()
        {
            ViewBag.AtcID = new SelectList(enty.AtcMasters.Where(u=> u.IsShipmentCompleted=="Y" && u.IsMCRDone!="Y").ToList(), "AtcId", "AtcNum");
            ViewBag.Locid = new SelectList(enty.LocationMasters.Where(u=> u.LocType=="W").ToList(), "location_pk", "locationname");
            return View();
        }
        public ActionResult ApproveIndex()
        {
            ViewBag.AtcID = new SelectList(enty.AtcMasters.Where(u => u.IsShipmentCompleted == "Y" && u.IsMCRDone != "Y").ToList(), "AtcId", "AtcNum");
            ViewBag.Locid = new SelectList(enty.LocationMasters.Where(u => u.LocType == "W").ToList(), "location_pk", "locationname");
            return View();
        }

        public ActionResult ConfirmIndex()
        {
            ViewBag.AtcID = new SelectList(enty.AtcMasters.Where(u=> u.IsShipmentCompleted=="Y" && u.IsMCRDone!="Y").ToList(), "AtcId", "AtcNum");
            ViewBag.Locid = new SelectList(enty.LocationMasters.Where(u=> u.LocType=="W").ToList(), "location_pk", "locationname");
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
        public PartialViewResult GetATCwiseFabricInventory(int locid, int atcid)
        {

            AtcwiseFabricInventory atcwiseFabricInventory = new AtcwiseFabricInventory();
            
            InventoryRepo inventoryRepo = new InventoryRepo();            
            DataTable dt = inventoryRepo.GetATCwiseFabricInventory(locid, atcid);
            DataTable trimdt = inventoryRepo.GetATCwiseTrimsInventory(locid, atcid);
            
            
            if (dt!=null && trimdt != null)
            {
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
        public PartialViewResult GetMCRDetails(int locid, int atcid)
        {

            GetMcrInventory  getMcrInventory = new GetMcrInventory();

            InventoryRepo inventoryRepo = new InventoryRepo();
            DataTable dt = inventoryRepo.MCRFabricInventory(locid, atcid);
            DataTable trimdt = inventoryRepo.MCRTrimsInventory(locid, atcid);

            if (dt != null && trimdt != null)
            {
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
        public PartialViewResult LoadMCRDetails(int locid, int atcid)
        {

            GetMcrInventory getMcrInventory = new GetMcrInventory();

            InventoryRepo inventoryRepo = new InventoryRepo();
            DataTable dt = inventoryRepo.MCRFabricInventory(locid, atcid);
            DataTable trimdt = inventoryRepo.MCRTrimsInventory(locid, atcid);

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
        public PartialViewResult GetApprovedMCR(int locid, int atcid)
        {

            GetMcrInventory getMcrInventory = new GetMcrInventory();

            InventoryRepo inventoryRepo = new InventoryRepo();
            DataTable dt = inventoryRepo.ApprovedMCRFabricInventory(locid, atcid);
            DataTable trimdt = inventoryRepo.ApprovedMCRTrimsInventory(locid, atcid);

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
            bool status = false;
            int atcid = 0;
            int inventory_Pk = 0;
            int location = 0;
            string Donum = "";
            try
            {
                foreach (FabricInventoryList fablist in things)
                {
                    atcid = int.Parse(fablist.AtcId.ToString());
                    inventory_Pk= int.Parse(fablist.InventoryItem_PK.ToString());
                    location = int.Parse(fablist.Location.ToString());
                   

                    //if(!enty.MCR_Master.Any(f=> f.Atc_Id==atcid && f.Location_pk==location))
                    //{
                    //    MCR_Master mCR_Master = new MCR_Master();
                    //    mCR_Master.Atc_Id = atcid;
                    //    mCR_Master.Location_pk = location;
                    //    mCR_Master.AddedDate = DateTime.Now;
                    //    mCR_Master.AddedBy = HttpContext.Session["Username"].ToString();
                    //    enty.MCR_Master.Add(mCR_Master);
                    //    enty.SaveChanges();
                    //    Donum = mCR_Master.MCR_no = "MCR" + mCR_Master.MCR_Pk.ToString().PadLeft(6, '0');
                   
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



                        enty.MCRDetails.Add(mCRDetail);
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
                    enty.SaveChanges();
                }
            }
            catch (Exception exp)
            {

                throw;
            }
            status = true;
                        
            return new JsonResult { Data = new { status = status } };
        }


        [HttpPost]
        public JsonResult Edit(List<FabricInventoryList> things)
        {
            bool status = false;
            int mcrpk = 0;
            try
            {
                foreach (FabricInventoryList fablist in things)
                {                   
                    
                    mcrpk= int.Parse(fablist.MCRDetails_Pk.ToString());                    

                        var q = from mcrdetails in enty.MCRDetails
                                where mcrdetails.McrDetails_pk == mcrpk
                                select mcrdetails;
                        foreach (var element in q)
                        {
                            element.PhysicalQty = Decimal.Parse(fablist.PhysicalQty.ToString());
                            element.DiffQty = Decimal.Parse(fablist.DiffQty.ToString());
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
            int atcid = 0;
            int locid = 0;
            try
            {
                foreach (FabricInventoryList fablist in things)
                {

                    mcrpk = int.Parse(fablist.MCRDetails_Pk.ToString());

                    var q = from mcrdetails in enty.MCRDetails
                            where mcrdetails.McrDetails_pk == mcrpk
                            select mcrdetails;
                    foreach (var element in q)
                    {
                        element.ActualCU_Rate = Decimal.Parse(fablist.ActualCURate.ToString());
                        //element.CU_Rate= Decimal.Parse(fablist.CURate.ToString());
                        element.CU_Rate= Decimal.Parse(fablist.ActualCURate.ToString());
                        atcid = int.Parse(element.Atcid.ToString());
                        locid = int.Parse(element.Location_pk.ToString());
                     
                    }

                }
                enty.SaveChanges();
                transferQty(things,atcid, locid);

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

            String Construction = "";
            String Composition = "";
            String ItemColor = "";
            String ItemSize = "";
            String TransferNumber = "";
            

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
                    foreach(var mcr in q)
                    {
                        templete_PK = int.Parse(mcr.Template_pk.ToString());
                        NewUnitprice = Decimal.Parse(mcr.CU_Rate.ToString());
                        receivedQty = Decimal.Parse(mcr.PhysicalQty.ToString());
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
            }
        }


        [HttpPost]
        public JsonResult ApproveMCR(List<FabricInventoryList> things)
        {
            bool status = false;
            int mcrpk = 0;
            try
            {
                foreach (FabricInventoryList fablist in things)
                {

                    mcrpk = int.Parse(fablist.MCRDetails_Pk.ToString());

                    var q = from mcrdetails in enty.MCRDetails
                            where mcrdetails.McrDetails_pk == mcrpk
                            select mcrdetails;
                    foreach (var element in q)
                    {

                        element.ApprovedDate= DateTime.Now;
                        element.ApprovedBy= HttpContext.Session["Username"].ToString();
                        element.IsApproved = "Y";
                       
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

       



    }
}   