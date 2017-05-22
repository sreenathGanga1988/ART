using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.InventoryBLL
{
    public class DeliveryOrder
    {
        public DeliveryOrderMasterData Domstrdata { get; set; }
        public List<DeliveryOrderDetailsData> DeliveryOrderDetailsDataCollection { get; set; }

        public DataTable DeliveryRollDetailsData { get; set; }
        /// <summary>
        /// Delivery Order given from one warehouse to another
        /// </summary>
        /// <param name="Dodata"></param>
        /// <returns></returns>
        public String  insertWarehouseDO(DeliveryOrder Dodata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                DeliveryOrderMaster domstr = new DeliveryOrderMaster();
                domstr.AtcID = Dodata.Domstrdata.AtcID; 
                domstr.AddedDate = DateTime.Now;
                domstr.AddedBy = Dodata.Domstrdata.AddedBy; 
                domstr.ToLocation_PK = Dodata.Domstrdata.ToLocation_PK ;
                domstr.FromLocation_PK = Dodata.Domstrdata.FromLocation_PK;
                domstr.DoType = Dodata.Domstrdata.DoType;
                domstr.BoeNum = Dodata.Domstrdata.BoeNum;
                domstr.ContainerNumber = Dodata.Domstrdata.ContainerNumber;
                domstr.DeliveryDate = Dodata.Domstrdata.DeliveryDate;
                domstr.ExportContainer = Dodata.Domstrdata.ExportContainer;
                
                enty.DeliveryOrderMasters.Add(domstr);


                enty.SaveChanges();

                Donum = domstr.DONum = CodeGenerator.GetUniqueCode("DOWW", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(domstr.DO_PK.ToString()));

              


                foreach (DeliveryOrderDetailsData di in Dodata.DeliveryOrderDetailsDataCollection)
                {
                       //Add the delivery details
                        DeliveryOrderDetail dlvrdet = new DeliveryOrderDetail();
                        dlvrdet.DO_PK = domstr.DO_PK;
                        dlvrdet.InventoryItem_PK = di.InventoryItem_PK;
                        dlvrdet.DeliveryQty =di.DeliveryQty;

                        enty.DeliveryOrderDetails.Add(dlvrdet);


                        // Reduce Goods in Inventory
                        var q = from invitem in enty.InventoryMasters
                                where invitem.InventoryItem_PK == di.InventoryItem_PK
                                select invitem;

                        foreach (var invitemdetail in q)
                        {

                            invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + di.DeliveryQty;
                            invitemdetail.OnhandQty = invitemdetail.OnhandQty - di.DeliveryQty;

                        }
                        //Add goods to transit
                        GoodsInTransit gtn = new GoodsInTransit();
                        gtn.InventoryItem_PK = di.InventoryItem_PK;;
                        gtn.DO_PK = domstr.DO_PK;
                        gtn.TransitQty = di.DeliveryQty;
                        enty.GoodsInTransits.Add(gtn);
                        enty.SaveChanges();


                    }
               enty.SaveChanges();

                }


            return Donum;
            }

        /// <summary>
        /// Give Do to the factory and Make Auto Reciept of the DO
        /// </summary>
        /// <param name="Dodata"></param>
        /// <returns></returns>
        public String insertFactoryDO(DeliveryOrder Dodata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                DeliveryOrderMaster domstr = new DeliveryOrderMaster();
                domstr.AtcID = Dodata.Domstrdata.AtcID;
                domstr.AddedDate = DateTime.Now;
                domstr.AddedBy = Dodata.Domstrdata.AddedBy;
                domstr.ToLocation_PK = Dodata.Domstrdata.ToLocation_PK;
                domstr.FromLocation_PK = Dodata.Domstrdata.FromLocation_PK;
                domstr.DoType = Dodata.Domstrdata.DoType;
                domstr.BoeNum = Dodata.Domstrdata.BoeNum;
                domstr.ContainerNumber = Dodata.Domstrdata.ContainerNumber;
                domstr.DeliveryDate = Dodata.Domstrdata.DeliveryDate;
                enty.DeliveryOrderMasters.Add(domstr);


                enty.SaveChanges();

                Donum = domstr.DONum = CodeGenerator.GetUniqueCode("WF", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(domstr.DO_PK.ToString()));




                foreach (DeliveryOrderDetailsData di in Dodata.DeliveryOrderDetailsDataCollection)
                {
                    //Add the delivery details
                    DeliveryOrderDetail dlvrdet = new DeliveryOrderDetail();
                    dlvrdet.DO_PK = domstr.DO_PK;
                    dlvrdet.InventoryItem_PK = di.InventoryItem_PK;
                    dlvrdet.DeliveryQty = di.DeliveryQty;

                    enty.DeliveryOrderDetails.Add(dlvrdet);

                    enty.SaveChanges();
                   
                    int mrndet_pk = 0;
                    int podet_pk = 0;
                    int skudetPK = 0;
                    decimal curate = 0;
                    int uom_pk = 0;
                    // Reduce Goods in Inventory
                    var q = from invitem in enty.InventoryMasters
                            where invitem.InventoryItem_PK == di.InventoryItem_PK
                            select invitem;

                    foreach (var invitemdetail in q)
                    {

                        invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + di.DeliveryQty;
                        invitemdetail.OnhandQty = invitemdetail.OnhandQty - di.DeliveryQty;
                    
                        mrndet_pk =int.Parse ( invitemdetail.MrnDet_PK.ToString ());
                        podet_pk =int.Parse (invitemdetail.PoDet_PK.ToString ());
                        skudetPK = int.Parse(invitemdetail.SkuDet_Pk.ToString());
                        curate = decimal.Parse(invitemdetail.CURate.ToString());
                        uom_pk = int.Parse(invitemdetail.Uom_Pk.ToString());
                    }
                    //Add goods to transit
                    InventoryMaster invmstr = new InventoryMaster();
                    invmstr.MrnDet_PK = mrndet_pk;
                    invmstr.PoDet_PK = podet_pk;
                    invmstr.SkuDet_Pk = skudetPK;
                    invmstr.ReceivedQty = di.DeliveryQty;
                    invmstr.OnhandQty = di.DeliveryQty;
                    invmstr.DeliveredQty = 0;
                    invmstr.ReceivedVia = "FR";
                    invmstr.Location_PK = Dodata.Domstrdata.ToLocation_PK;
                    invmstr.CURate = curate;
                    invmstr.AddedDate = DateTime.Now.Date;
                    invmstr.Uom_Pk = uom_pk;
                    invmstr.Refnum = domstr.DONum;
                    enty.InventoryMasters.Add(invmstr);


                }
                enty.SaveChanges();

            }


            return Donum;
        }


        /// <summary>
        /// Transfer Fabric from One warehouse to factory based on Cut order
        /// </summary>
        /// <param name="Dodata"></param>
        /// <returns></returns>
        public String insertFactoryFabricDO(DeliveryOrder Dodata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                DeliveryOrderMaster domstr = new DeliveryOrderMaster();
                domstr.AtcID = Dodata.Domstrdata.AtcID;
                domstr.AddedDate = DateTime.Now;
                domstr.AddedBy = Dodata.Domstrdata.AddedBy;
                domstr.ToLocation_PK = Dodata.Domstrdata.ToLocation_PK;
                domstr.FromLocation_PK = Dodata.Domstrdata.FromLocation_PK;
                domstr.DoType = Dodata.Domstrdata.DoType;
                domstr.BoeNum = Dodata.Domstrdata.BoeNum;
                domstr.ContainerNumber = Dodata.Domstrdata.ContainerNumber;
                domstr.DeliveryDate = Dodata.Domstrdata.DeliveryDate;
                enty.DeliveryOrderMasters.Add(domstr);


                enty.SaveChanges();

                Donum = domstr.DONum = CodeGenerator.GetUniqueCode("WF", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(domstr.DO_PK.ToString()));




                foreach (DeliveryOrderDetailsData di in Dodata.DeliveryOrderDetailsDataCollection)
                {
                    //Add the delivery details
                    DeliveryOrderDetail dlvrdet = new DeliveryOrderDetail();
                    dlvrdet.DO_PK = domstr.DO_PK;
                    dlvrdet.InventoryItem_PK = di.InventoryItem_PK;
                    dlvrdet.DeliveryQty = di.DeliveryQty;

                    enty.DeliveryOrderDetails.Add(dlvrdet);

                    enty.SaveChanges();

                    int mrndet_pk = 0;
                    int podet_pk = 0;
                    int skudetPK = 0;
                    decimal curate = 0;
                    int uom_pk = 0;
                    // Reduce Goods in Inventory
                    var q = from invitem in enty.InventoryMasters
                            where invitem.InventoryItem_PK == di.InventoryItem_PK
                            select invitem;

                    foreach (var invitemdetail in q)
                    {

                        invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + di.DeliveryQty;
                        invitemdetail.OnhandQty = invitemdetail.OnhandQty - di.DeliveryQty;

                        mrndet_pk = int.Parse(invitemdetail.MrnDet_PK.ToString());
                        podet_pk = int.Parse(invitemdetail.PoDet_PK.ToString());
                        skudetPK = int.Parse(invitemdetail.SkuDet_Pk.ToString());
                        curate = decimal.Parse(invitemdetail.CURate.ToString());
                        uom_pk = int.Parse(invitemdetail.Uom_Pk.ToString());
                    }
                    //Add goods to transit
                    InventoryMaster invmstr = new InventoryMaster();
                    invmstr.MrnDet_PK = mrndet_pk;
                    invmstr.PoDet_PK = podet_pk;
                    invmstr.SkuDet_Pk = skudetPK;
                    invmstr.ReceivedQty = di.DeliveryQty;
                    invmstr.OnhandQty = di.DeliveryQty;
                    invmstr.DeliveredQty = 0;
                    invmstr.ReceivedVia = "FR";
                    invmstr.Location_PK = Dodata.Domstrdata.ToLocation_PK;
                    invmstr.CURate = curate;
                    invmstr.AddedDate = DateTime.Now.Date;
                    invmstr.Uom_Pk = uom_pk;
                    invmstr.Refnum = domstr.DONum;
                    enty.InventoryMasters.Add(invmstr);




                    CutOrderDO ctordrdo = new CutOrderDO();
                    ctordrdo.CutID = di.Cutid;
                    ctordrdo.Skudet_PK = skudetPK;
                    ctordrdo.DoDet_Pk = dlvrdet.DODet_PK;
                    ctordrdo.DeliveryQty = dlvrdet.DeliveryQty;
                    enty.CutOrderDOes.Add(ctordrdo);




                }
                enty.SaveChanges();

            }


            return Donum;
        }





        /// <summary>
        /// Transfer Fabric from One warehouse to factory based on Cut order With Rolldata
        /// </summary>
        /// <param name="Dodata"></param>
        /// <returns></returns>
        public String insertFactoryFabricROLLDO(DeliveryOrder Dodata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                DeliveryOrderMaster domstr = new DeliveryOrderMaster();
                domstr.AtcID = Dodata.Domstrdata.AtcID;
                domstr.AddedDate = DateTime.Now;
                domstr.AddedBy = Dodata.Domstrdata.AddedBy;
                domstr.ToLocation_PK = Dodata.Domstrdata.ToLocation_PK;
                domstr.FromLocation_PK = Dodata.Domstrdata.FromLocation_PK;
                domstr.DoType = Dodata.Domstrdata.DoType;
                domstr.BoeNum = Dodata.Domstrdata.BoeNum;
                domstr.ContainerNumber = Dodata.Domstrdata.ContainerNumber;
                domstr.DeliveryDate = Dodata.Domstrdata.DeliveryDate;
                enty.DeliveryOrderMasters.Add(domstr);


                enty.SaveChanges();

                Donum = domstr.DONum = CodeGenerator.GetUniqueCode("WF", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(domstr.DO_PK.ToString()));




                foreach (DeliveryOrderDetailsData di in Dodata.DeliveryOrderDetailsDataCollection)
                {
                    //Add the delivery details
                    DeliveryOrderDetail dlvrdet = new DeliveryOrderDetail();
                    dlvrdet.DO_PK = domstr.DO_PK;
                    dlvrdet.InventoryItem_PK = di.InventoryItem_PK;
                    dlvrdet.DeliveryQty = di.DeliveryQty;

                    enty.DeliveryOrderDetails.Add(dlvrdet);

                    enty.SaveChanges();

                    int mrndet_pk = 0;
                    int podet_pk = 0;
                    int skudetPK = 0;
                    decimal curate = 0;
                    int uom_pk = 0;
                    // Reduce Goods in Inventory
                    var q = from invitem in enty.InventoryMasters
                            where invitem.InventoryItem_PK == di.InventoryItem_PK
                            select invitem;

                    foreach (var invitemdetail in q)
                    {

                        invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + di.DeliveryQty;
                        invitemdetail.OnhandQty = invitemdetail.OnhandQty - di.DeliveryQty;

                        mrndet_pk = int.Parse(invitemdetail.MrnDet_PK.ToString());
                        podet_pk = int.Parse(invitemdetail.PoDet_PK.ToString());
                        skudetPK = int.Parse(invitemdetail.SkuDet_Pk.ToString());
                        curate = decimal.Parse(invitemdetail.CURate.ToString());
                        uom_pk = int.Parse(invitemdetail.Uom_Pk.ToString());
                    }












                    //Add goods to transit
                    InventoryMaster invmstr = new InventoryMaster();
                    invmstr.MrnDet_PK = mrndet_pk;
                    invmstr.PoDet_PK = podet_pk;
                    invmstr.SkuDet_Pk = skudetPK;
                    invmstr.ReceivedQty = di.DeliveryQty;
                    invmstr.OnhandQty = di.DeliveryQty;
                    invmstr.DeliveredQty = 0;
                    invmstr.ReceivedVia = "FR";
                    invmstr.Location_PK = Dodata.Domstrdata.ToLocation_PK;
                    invmstr.CURate = curate;
                    invmstr.AddedDate = DateTime.Now.Date;
                    invmstr.Uom_Pk = uom_pk;
                    invmstr.Refnum = domstr.DONum;
                    enty.InventoryMasters.Add(invmstr);




                    CutOrderDO ctordrdo = new CutOrderDO();
                    ctordrdo.CutID = di.Cutid;
                    ctordrdo.Skudet_PK = skudetPK;
                    ctordrdo.DoDet_Pk = dlvrdet.DODet_PK;
                    ctordrdo.DeliveryQty = dlvrdet.DeliveryQty;
                    enty.CutOrderDOes.Add(ctordrdo);


                    enty.SaveChanges();
                    DataTable dt = Dodata.DeliveryRollDetailsData;

                     dt = dt.Select("InventoryItem_PK="+di.InventoryItem_PK.ToString ()).CopyToDataTable();


                    for (int i=0;i<dt.Rows.Count;i++)
                    {
                        DORollDetail dorolldet = new DataModels.DORollDetail();

                        dorolldet.CutID = di.Cutid;
                        dorolldet.Roll_PK =int.Parse(dt.Rows[i]["roll_Pk"].ToString ());
                        dorolldet.DODet_PK = dlvrdet.DODet_PK;
                        dorolldet.DO_PK = domstr.DO_PK;
                        enty.DORollDetails.Add(dorolldet);


                        var m = from invitem in enty.FabricRollmasters
                                where invitem.Roll_PK == dorolldet.Roll_PK
                                select invitem;

                        foreach (var rolldet in m)
                        {
                            rolldet.IsDelivered = "Y";

                        }


                        var q2 = from rllinvdata in enty.RollInventoryMasters
                                where rllinvdata.Roll_PK == dorolldet.Roll_PK && rllinvdata.IsPresent == "Y"
                                select rllinvdata;
                        foreach (var element in q2)
                        {
                            element.IsPresent = "N";
                            element.DeliveredVia = Donum;
                       

                        }






                        RollInventoryMaster rvinvmstr = new RollInventoryMaster();

                        rvinvmstr.Addeddate = DateTime.Now;
                        rvinvmstr.DocumentNum = Donum;
                        rvinvmstr.AddedVia = "WF";
                        rvinvmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                        rvinvmstr.Location_Pk = Dodata.Domstrdata.ToLocation_PK; 
                        rvinvmstr.Roll_PK = dorolldet.Roll_PK;
                        rvinvmstr.IsPresent = "Y";
                        rvinvmstr.FactId= Dodata.Domstrdata.ToLocation_PK;
                        enty.RollInventoryMasters.Add(rvinvmstr);
                        enty.SaveChanges();

                      




                    }

                    enty.SaveChanges();
                }
              

            }


            return Donum;
        }













        /// <summary>
        /// Returns the Item to the Inventory of Warehouse.
        /// Auto Addition is happening
        /// </summary>
        /// <param name="Dodata"></param>
        /// <returns></returns>
        public String insertFactoryReturnDO(DeliveryOrder Dodata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                DeliveryOrderMaster domstr = new DeliveryOrderMaster();
                domstr.AtcID = Dodata.Domstrdata.AtcID;
                domstr.AddedDate = DateTime.Now;
                domstr.AddedBy = Dodata.Domstrdata.AddedBy;
                domstr.ToLocation_PK = Dodata.Domstrdata.ToLocation_PK;
                domstr.FromLocation_PK = Dodata.Domstrdata.FromLocation_PK;
                domstr.DoType = Dodata.Domstrdata.DoType;
                domstr.BoeNum = Dodata.Domstrdata.BoeNum;
                domstr.ContainerNumber = Dodata.Domstrdata.ContainerNumber;
                domstr.DeliveryDate = Dodata.Domstrdata.DeliveryDate;
                enty.DeliveryOrderMasters.Add(domstr);


                enty.SaveChanges();

                Donum = domstr.DONum = CodeGenerator.GetUniqueCode("FW", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(domstr.DO_PK.ToString()));




                foreach (DeliveryOrderDetailsData di in Dodata.DeliveryOrderDetailsDataCollection)
                {
                    //Add the delivery details
                    DeliveryOrderDetail dlvrdet = new DeliveryOrderDetail();
                    dlvrdet.DO_PK = domstr.DO_PK;
                    dlvrdet.InventoryItem_PK = di.InventoryItem_PK;
                    dlvrdet.DeliveryQty = di.DeliveryQty;

                    enty.DeliveryOrderDetails.Add(dlvrdet);

                    enty.SaveChanges();

                    int mrndet_pk = 0;
                    int podet_pk = 0;
                    int skudetPK = 0;
                    decimal curate = 0;
                    int uom_pk = 0;
                    // Reduce Goods in Inventory
                    var q = from invitem in enty.InventoryMasters
                            where invitem.InventoryItem_PK == di.InventoryItem_PK
                            select invitem;

                    foreach (var invitemdetail in q)
                    {

                        invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + di.DeliveryQty;
                        invitemdetail.OnhandQty = invitemdetail.OnhandQty - di.DeliveryQty;

                        mrndet_pk = int.Parse(invitemdetail.MrnDet_PK.ToString());
                        podet_pk = int.Parse(invitemdetail.PoDet_PK.ToString());
                        skudetPK = int.Parse(invitemdetail.SkuDet_Pk.ToString());
                        curate = decimal.Parse(invitemdetail.CURate.ToString());
                        uom_pk = int.Parse(invitemdetail.Uom_Pk.ToString());
                    }
                    //Add goods to transit
                    InventoryMaster invmstr = new InventoryMaster();
                    invmstr.MrnDet_PK = mrndet_pk;
                    invmstr.PoDet_PK = podet_pk;
                    invmstr.SkuDet_Pk = skudetPK;
                    invmstr.ReceivedQty = di.DeliveryQty;
                    invmstr.OnhandQty = di.DeliveryQty;
                    invmstr.DeliveredQty = 0;
                    invmstr.ReceivedVia = "FW";
                    invmstr.Location_PK = Dodata.Domstrdata.ToLocation_PK;
                    invmstr.CURate = curate;
                    invmstr.AddedDate = DateTime.Now.Date;
                    invmstr.Uom_Pk = uom_pk;
                    invmstr.Refnum = domstr.DONum;
                    enty.InventoryMasters.Add(invmstr);


                }
                enty.SaveChanges();

            }


            return Donum;
        }






    }

    public class DeliveryOrderMasterData
    {
        public int DO_PK { get; set; }
        public string DONum { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int FromLocation_PK { get; set; }
        public int ToLocation_PK { get; set; }
        public DateTime DODate { get; set; }
        public string ContainerNumber { get; set; }
        public string BoeNum { get; set; }
        public int Deliverymethod_Pk { get; set; }
        public int AtcID { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string DoType { get; set; }
        public string ExportContainer { get; set; }
        
    }

    public class DeliveryOrderDetailsData
    {



       

      
        public int DODet_PK { get; set; }
        public int DO_PK { get; set; }
        public int InventoryItem_PK { get; set; }
        public Decimal  DeliveryQty { get; set; }
        public string Remark { get; set; }

        public int Cutid { get; set; }
    }






    public class InventoryMissingDetailsData
    {

        public int MisplaceAppDet_PK { get; set; }
        public int MisplaceApp_PK { get; set; }
        public int InventoryItem_PK { get; set; }
        public int Qty { get; set; }
        public string Remark { get; set; }

    }

    public class InventoryMissingRequestData

    {

        public int MisplaceApp_pk { get; set; }
        public int FromLctn_pk { get; set; }
        public int Atc_id { get; set; }
        public DateTime MisplaceDate { get; set; }
        public string Explanation { get; set; }
        public string AddedBy { get; set; }
        public DateTime Addeddate { get; set; }
        public string Level1Approval { get; set; }
        public string Level1ApprovedBY { get; set; }
        public int IsApproved { get; set; }
        public int ApprovedBy { get; set; }


        public DeliveryOrderMasterData Domstrdata { get; set; }
        public List<DeliveryOrderDetailsData> DeliveryOrderDetailsDataCollection
        {
            get; set;
        }


        public String insertMissingInventoryRequest(InventoryMissingRequestData Dodata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
             InventoryMissingRequest   domstr = new InventoryMissingRequest();
                domstr.Atc_id = Dodata.Domstrdata.AtcID;
                domstr.Addeddate = DateTime.Now;
                domstr.AddedBy = Dodata.Domstrdata.AddedBy;
                domstr.Explanation = Dodata.Explanation;
                // domstr. = Dodata.Domstrdata.ToLocation_PK;
                domstr.FromLctn_pk = Dodata.Domstrdata.FromLocation_PK;
                domstr.MisplaceDate = Dodata.Domstrdata.DeliveryDate;
                domstr.Level1Approval ="N";
                domstr.Level1ApprovedBY = "";
                domstr.IsApproved = "N";
                //domstr.ApprovedBy ="N";
                //domstr.IsApproved ="N";
                enty.InventoryMissingRequests.Add(domstr);


                enty.SaveChanges();



                Donum = domstr.reqnum="MINV"+domstr.MisplaceApp_pk.ToString().PadLeft(6, '0');


                foreach (DeliveryOrderDetailsData di in Dodata.DeliveryOrderDetailsDataCollection)
                {
                    InventoryMissingDetail dlvrdet = new DataModels.InventoryMissingDetail();

                     dlvrdet.MisplaceApp_PK = domstr.MisplaceApp_pk;
                    dlvrdet.InventoryItem_PK = di.InventoryItem_PK;
                    dlvrdet.Qty = di.DeliveryQty;

                    enty.InventoryMissingDetails.Add(dlvrdet);



                }
                enty.SaveChanges();

            }

            
            return Donum;
        }


        public void GetMissingInventoryApproved(int loan_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from lnmstr in enty.InventoryMissingRequests
                        where lnmstr.MisplaceApp_pk == loan_pk
                        select lnmstr;

                foreach (var element in q)
                {
                  
                    element.IsApproved = "Y";
                    element.ApprovedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.Addeddate = DateTime.Now;
                    
                }

                enty.SaveChanges();

            }
        }



        public void GetMissingInventoryApprovedLevel1(int loan_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from lnmstr in enty.InventoryMissingRequests
                        where lnmstr.MisplaceApp_pk == loan_pk
                        select lnmstr;

                foreach (var element in q)
                {

                    element.Level1Approval = "Y";
                    element.Level1ApprovedBY = HttpContext.Current.Session["Username"].ToString().Trim();
                

                }

                enty.SaveChanges();

            }
        }
    }



    public class WrongInventoryDcumentData

    {

        public int WrongInvtDoc_PK { get; set; }
        public string DocumentType { get; set; }
        public int User_PK { get; set; }
        public string DocumentNumber { get; set; }
        public string Explanation { get; set; }
        public string ActionRequired { get; set; }
        public string ITRemark { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }


     



        /// <summary>
        /// Load The Documnet based on user
        /// </summary>
        /// <param name="Debitfrom"></param>
        /// <param name="cmb_debit"></param>
        public void LoadCombo(String Debitfrom, CustomDropDown.DropDownListChosen cmb_doc)
        {


            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                if (Debitfrom == "TransferNote")
                {

                    var PoQuery = from bmstr in enty.DeliveryOrderMasters
                                 
                                  select new
                                  {
                                      name = bmstr.DONum,
                                      pk = bmstr.DO_PK
                                  };
                    cmb_doc.DataSource = PoQuery.ToList();
                    cmb_doc.DataBind();
                }
                else if (Debitfrom == "MRN")
                {
                    var PoQuery = from atcmstr in enty.MrnMasters
                                  select new
                                  {
                                      name = atcmstr.MrnNum,
                                      pk = atcmstr.Mrn_PK
                                  };
                    cmb_doc.DataSource = PoQuery.ToList();
                    cmb_doc.DataBind();
                }
                else if (Debitfrom == "RO Receipt")
                {

                    var PoQuery = from atcmstr in enty.RequestOrderMasters
                                  select new
                                  {
                                      name = atcmstr.RONum,
                                      pk = atcmstr.RO_Pk
                                  };
                    cmb_doc.DataSource = PoQuery.ToList();
                    cmb_doc.DataBind();
                }

                else if (Debitfrom == "LOAN")
                {

                    var PoQuery = from order in enty.InventoryLoanMasters
                                
                                  select new
                                  {
                                      name = order.LoanNum,
                                      pk = order.Loan_PK
                                  };
                    cmb_doc.DataSource = PoQuery.ToList();
                    cmb_doc.DataBind();
                }



            }

            //showAllPoPackATC();
        }














        public String insertMissingInventoryRequest()
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                WrongInventoryDocument wdomstr = new WrongInventoryDocument();
                wdomstr.DocumentType = this.DocumentType;
                wdomstr.Explanation = this.Explanation;              
                wdomstr.User_PK = this.User_PK;
                wdomstr.DocumentNumber = this.DocumentNumber;
                wdomstr.Explanation = this.Explanation;
                wdomstr.ActionRequired = this.ActionRequired;
                wdomstr.ITRemark = this.Explanation;
               wdomstr.AddedDate = DateTime.Now;
                wdomstr.AddedBy = this.AddedBy;
               
                enty.WrongInventoryDocuments.Add(wdomstr);


                enty.SaveChanges();



                Donum = wdomstr.Reqnum = "WINV" + wdomstr.WrongInvtDoc_PK.ToString().PadLeft(6, '0');


              
                enty.SaveChanges();

            }


            return Donum;
        }




    }

}