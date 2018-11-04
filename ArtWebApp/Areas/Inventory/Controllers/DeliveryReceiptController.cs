using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ArtWebApp.Areas.Inventory.ViewModel;

namespace ArtWebApp.Areas.Inventory.Controllers
{
    public class DeliveryReceiptController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();
        static String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        // GET: Inventory/DeliveryReceipt
        public ActionResult DeliveryReceipt()
        {
            int lockpk= int.Parse( HttpContext.Session["UserLoc_pk"].ToString());
            ViewBag.DoPK = new SelectList(db.DeliveryOrderMasters.Where(u=>u.ToLocation_PK==1), "BuyerID", "BuyerName");            
            ViewBag.Atcid = new SelectList(db.AtcMasters.Where(u => u.IsShipmentCompleted != "Y" && u.IsMCRDone != "Y").ToList(), "Atcid", "AtcNum");
            ViewBag.DOMethod= new SelectList(db.DeliveryMethodMasters.ToList(), "Deliverymethod_Pk", "DeliveryMethod");

            return View();
        }

        public ActionResult DeliveryOrderWarehouse()
        {
            int lockpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            ViewBag.DoPK = new SelectList(db.DeliveryOrderMasters.Where(u => u.ToLocation_PK == 1), "BuyerID", "BuyerName");
            ViewBag.Atcid = new SelectList(db.AtcMasters.Where(u => u.IsShipmentCompleted != "Y" && u.IsMCRDone != "Y").ToList(), "Atcid", "AtcNum");
            ViewBag.DOMethod = new SelectList(db.DeliveryMethodMasters.ToList(), "Deliverymethod_Pk", "DeliveryMethod");
            ViewBag.toLocid = new SelectList(db.LocationMasters.Where(u => u.LocType == "W").ToList(), "location_pk", "locationname");
            return View();
        }

        [HttpGet]
        public JsonResult Get_Factory( int Id = 0)
        {
            int locationpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            SelectList Factlist = Fillfactory(locationpk);
            JsonResult jsd = Json(Factlist, JsonRequestBehavior.AllowGet);
            return jsd;
        }

        public static SelectList Fillfactory(int locpk)
        {
            DataTable dt = InventoryRepo.GetFactoryDetails(locpk);

            SelectList skulist = Areas.MVCControls.DataTabletoSelectList("Location_PK", "LocationName", dt, "Select");

            return skulist;

        }

        [HttpGet]
        public PartialViewResult GetItemDetails(int atcid,DateTime dodate, string container,int toloc,string boe,string deliverymethod, string dotype)
        {

            int locationpk = int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            DeliveryReceipt deliveryReceipt = new DeliveryReceipt();
            
            DataTable dt1 = new DataTable();
            InventoryRepo inventoryRepo = new InventoryRepo();
            
            DataTable dt = inventoryRepo.GetDeliveryItemDetails(locationpk, atcid);
            
            dt1 = inventoryRepo.GetFabricStockDetails(locationpk, atcid);
            
            deliveryReceipt.AtcId = atcid;
            deliveryReceipt.DoDate = dodate;
            deliveryReceipt.containerno = container;
            deliveryReceipt.toloc = toloc;
            deliveryReceipt.boe = boe;
            deliveryReceipt.deliverymethod = deliverymethod;
            if (dt != null )
            {
                deliveryReceipt.DeliveryDetails= dt;
            }
            if (dt1 != null)
            {
                deliveryReceipt.DeliverFabricDetails = dt1;
            }
            else
            {
                TempData["shortMessage"] = "MCR AlreadyGenerated";
            }
            return PartialView("DeliveryReceipt_P", deliveryReceipt);
        }

        [HttpPost]
        public JsonResult CreateDO(List<DeliveryDetails> things )
        {
            bool status = false;
            string Donum = "";
            string shotmes = "";
            int atcid = 0;
            string dodate="";
            string container = "";
            string boe = "";
            int toloc = 0;
            string dotype = "";
            
            foreach (DeliveryDetails master in things)
            {
                atcid = master.AtcId;
                dodate = master.DoDate;
                container = master.containerno;
                boe = master.boe;
                toloc = master.toloc;
                dotype = master.dotype;
            }
            DeliveryOrderMaster deliveryOrderMaster = new DeliveryOrderMaster();
            deliveryOrderMaster.AtcID = atcid;
            deliveryOrderMaster.BoeNum = boe;
            deliveryOrderMaster.ContainerNumber = container;
            deliveryOrderMaster.DeliveryDate = DateTime.Parse(dodate.ToString());
            deliveryOrderMaster.DoType = dotype;
            deliveryOrderMaster.AddedDate = DateTime.Now;
            deliveryOrderMaster.AddedBy = HttpContext.Session["Username"].ToString();
            deliveryOrderMaster.FromLocation_PK= int.Parse(HttpContext.Session["UserLoc_pk"].ToString());
            deliveryOrderMaster.ToLocation_PK = toloc;
            db.DeliveryOrderMasters.Add(deliveryOrderMaster);
            db.SaveChanges();
            Donum = deliveryOrderMaster.DONum = CodeGenerator.GetUniqueCode(dotype, HttpContext.Session["lOC_Code"].ToString().Trim(), int.Parse(deliveryOrderMaster.DO_PK.ToString()));

            foreach(DeliveryDetails item in things)
            {               
                DeliveryOrderDetail deliveryOrderDetail = new DeliveryOrderDetail();
                deliveryOrderDetail.DO_PK = deliveryOrderMaster.DO_PK;
                deliveryOrderDetail.InventoryItem_PK = item.InventoryItem_pk;
                deliveryOrderDetail.DeliveryQty = item.DeliveryQty;
                db.DeliveryOrderDetails.Add(deliveryOrderDetail);
                db.SaveChanges();

                int mrndet_pk = 0;
                int podet_pk = 0;
                int skudetPK = 0;
                decimal curate = 0;
                int uom_pk = 0;
                //Reduce goods in Rack Inventory
                var ra = from rack in db.RackInventoryMasters where rack.RackInventory_PK == item.RackInventory_PK select rack;
                foreach(var rackinv in ra)
                {
                    rackinv.DeliveredQty = rackinv.DeliveredQty + item.DeliveryQty;
                    rackinv.OnhandQty = rackinv.OnhandQty - item.DeliveryQty;
                }
                // Reduce Goods in Inventory
                var q = from invitem in db.InventoryMasters
                        where invitem.InventoryItem_PK == item.InventoryItem_pk
                        select invitem;
                foreach (var invitemdetail in q)
                {

                    invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + item.DeliveryQty;
                    invitemdetail.OnhandQty = invitemdetail.OnhandQty - item.DeliveryQty;

                    mrndet_pk = int.Parse(invitemdetail.MrnDet_PK.ToString());
                    podet_pk = int.Parse(invitemdetail.PoDet_PK.ToString());
                    skudetPK = int.Parse(invitemdetail.SkuDet_Pk.ToString());
                    curate = decimal.Parse(invitemdetail.CURate.ToString());
                    uom_pk = int.Parse(invitemdetail.Uom_Pk.ToString());
                }
                if (dotype == "WF")
                {
                    InventoryMaster invmstr = new InventoryMaster();
                    invmstr.MrnDet_PK = mrndet_pk;
                    invmstr.PoDet_PK = podet_pk;
                    invmstr.SkuDet_Pk = skudetPK;
                    invmstr.ReceivedQty = item.DeliveryQty;
                    invmstr.OnhandQty = item.DeliveryQty;
                    invmstr.DeliveredQty = 0;
                    invmstr.ReceivedVia = "FR";
                    invmstr.Location_PK = toloc;
                    invmstr.CURate = curate;
                    invmstr.AddedDate = DateTime.Now.Date;
                    invmstr.Uom_Pk = uom_pk;
                    invmstr.Refnum = deliveryOrderMaster.DONum;
                    db.InventoryMasters.Add(invmstr);
                }
                if (dotype == "WW")
                {
                    //Add goods to transit
                    GoodsInTransit gtn = new GoodsInTransit();
                    gtn.InventoryItem_PK = item.InventoryItem_pk; ;
                    gtn.DO_PK = deliveryOrderMaster.DO_PK;
                    gtn.TransitQty = item.DeliveryQty;
                    db.GoodsInTransits.Add(gtn);
                    db.SaveChanges();
                }

            }
            
            try
            {
                db.SaveChanges();
                status = true;
                shotmes = "Delivery Order# " + Donum + "  Created Successfully";
            }
            catch (Exception exp)
            {
                shotmes = "Delivery Order not created";
                status = false;
                throw;

            }


            return new JsonResult { Data = new { status = shotmes } };
        }
    }
}