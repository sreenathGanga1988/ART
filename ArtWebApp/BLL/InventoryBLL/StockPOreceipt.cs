using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.InventoryBLL
{
    public class StockPOreceipt
    {
        public StockMRNMasterData smrnmstrdata { get; set; }
        public List<StockMRNDetailsData> StockMRNDetailsDataCollection { get; set; }
        public String InsertSMRNData(StockPOreceipt SpoRcpt)
        {
            String mrnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                StockMrnMaster smrnmstrdb = new StockMrnMaster();
                smrnmstrdb.DoNumber = SpoRcpt.smrnmstrdata.DoNumber;
                smrnmstrdb.AddedDate = DateTime.Now;
                smrnmstrdb.SPo_PK = SpoRcpt.smrnmstrdata.SPo_PK;
                smrnmstrdb.AddedBY = SpoRcpt.smrnmstrdata.AddedBY;

                smrnmstrdb.Location_Pk = SpoRcpt.smrnmstrdata.Location_Pk;
                smrnmstrdb.SReciept_Pk = int.Parse(SpoRcpt.smrnmstrdata.Reciept_Pk.ToString());
                enty.StockMrnMasters.Add(smrnmstrdb);
                enty.SaveChanges();


                smrnmstrdb.SMrnNum = "SMR" + HttpContext.Current.Session["lOC_Code"].ToString().Trim() + smrnmstrdb.SMrn_PK.ToString().PadLeft(6, '0');

                foreach (StockMRNDetailsData mrnrdet in SpoRcpt.StockMRNDetailsDataCollection)
                {
                    StockMRNDetail smrndetdb = new StockMRNDetail();
                    smrndetdb.SMRN_Pk = smrnmstrdb.SMrn_PK;
                    smrndetdb.SPODetails_PK = mrnrdet.SPODetails_PK;
                    smrndetdb.SPO_PK = mrnrdet.SPO_PK;
                    smrndetdb.ReceivedQty = mrnrdet.ReceivedQty;
                    smrndetdb.Unitprice = mrnrdet.Unitprice;
                    smrndetdb.Uom_PK = mrnrdet.Uom_PK1;
                    smrndetdb.ExtraQty = mrnrdet.ExtraQty;
                    enty.StockMRNDetails.Add(smrndetdb);

                    enty.SaveChanges();



                    StockInventoryMaster sinvmstr = new StockInventoryMaster();

                    sinvmstr.SMRNDet_Pk = smrndetdb.SMRNDet_Pk;
                    sinvmstr.SPODetails_PK = mrnrdet.SPODetails_PK;
                    sinvmstr.Template_PK = mrnrdet.Template_PK;
                    sinvmstr.OnHandQty = mrnrdet.ReceivedQty + mrnrdet.ExtraQty;
                    sinvmstr.ReceivedQty = mrnrdet.ReceivedQty + mrnrdet.ExtraQty;
                    sinvmstr.DeliveredQty = 0;
                    sinvmstr.Unitprice = mrnrdet.Unitprice;
                    sinvmstr.Composition = mrnrdet.Composition;
                    sinvmstr.Construct = mrnrdet.Construct;
                    sinvmstr.TemplateColor = mrnrdet.TemplateColor;
                    sinvmstr.TemplateSize = mrnrdet.TemplateSize;
                    sinvmstr.TemplateWidth = mrnrdet.TemplateWidth;
                    sinvmstr.TemplateWeight = mrnrdet.TemplateWeight;
                    sinvmstr.Uom_PK = smrndetdb.Uom_PK;
                    sinvmstr.CuRate = mrnrdet.CuRate;
                    sinvmstr.ReceivedVia = "SMR";
                    sinvmstr.Location_Pk = smrnmstrdb.Location_Pk;
                    sinvmstr.Refnum = smrnmstrdb.SMrnNum;
                    sinvmstr.AddedDate = DateTime.Now.Date;
                    enty.StockInventoryMasters.Add(sinvmstr);
                }

                enty.SaveChanges();

                mrnum = smrnmstrdb.SMrnNum;
            }

            return mrnum;






        }

        public String ShowPOType(int popk)
        {
            String potype = "normal";

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from ogpo in enty.StocPOForODOOs
                        where ogpo.Spo_PK == popk
                        select ogpo;
                foreach(var element in q)
                {
                    potype = "IPO";
                }
            }

            return potype;
         }


        public String ShowCountry(int LocationPK)
        {
            String countryname = "UAE";

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from ogpo in enty.LocationMasters
                        join contry in enty.CountryMasters on ogpo.CountryID equals contry.CountryID
                        where ogpo.Location_PK == LocationPK
                        select new { contry.ShortName};
                foreach (var element in q)
                {
                    countryname = element.ShortName;
                }
            }

            return countryname;
        }


        public String InsertSMRNDataIPO(StockPOreceipt SpoRcpt)
        {
            String mrnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                StockMrnMaster smrnmstrdb = new StockMrnMaster();
                smrnmstrdb.DoNumber = SpoRcpt.smrnmstrdata.DoNumber;
                smrnmstrdb.AddedDate = DateTime.Now;
                smrnmstrdb.SPo_PK = SpoRcpt.smrnmstrdata.SPo_PK;
                smrnmstrdb.AddedBY = SpoRcpt.smrnmstrdata.AddedBY;

                smrnmstrdb.Location_Pk = SpoRcpt.smrnmstrdata.Location_Pk;
                smrnmstrdb.SReciept_Pk = int.Parse(SpoRcpt.smrnmstrdata.Reciept_Pk.ToString());
                enty.StockMrnMasters.Add(smrnmstrdb);
                enty.SaveChanges();


                smrnmstrdb.SMrnNum = "SMR" + HttpContext.Current.Session["lOC_Code"].ToString().Trim() + smrnmstrdb.SMrn_PK.ToString().PadLeft(6, '0');




                InventorySalesMaster trnmstr = new InventorySalesMaster();

                trnmstr.SalesDate = DateTime.Now;

                trnmstr.FromLocation_PK = SpoRcpt.smrnmstrdata.Location_Pk;
                trnmstr.ToLocation_PK = SpoRcpt.smrnmstrdata.Location_Pk;

                trnmstr.Deliverymethod_Pk = 5;
                trnmstr.SalesDODate = DateTime.Now;
                trnmstr.ISApproved = "N";
                trnmstr.DoType = "Internal";
                trnmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                trnmstr.AddedDate = DateTime.Now;
                trnmstr.ContainerNumber = "AutoSales";

                enty.InventorySalesMasters.Add(trnmstr);
                enty.SaveChanges();
                mrnum = trnmstr.SalesDONum = CodeGenerator.GetUniqueCode("DO", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(trnmstr.SalesDO_PK.ToString()));





                foreach (StockMRNDetailsData mrnrdet in SpoRcpt.StockMRNDetailsDataCollection)
                {
                    StockMRNDetail smrndetdb = new StockMRNDetail();
                    smrndetdb.SMRN_Pk = smrnmstrdb.SMrn_PK;
                    smrndetdb.SPODetails_PK = mrnrdet.SPODetails_PK;
                    smrndetdb.SPO_PK = mrnrdet.SPO_PK;
                    smrndetdb.ReceivedQty = mrnrdet.ReceivedQty;
                    smrndetdb.Unitprice = mrnrdet.Unitprice;
                    smrndetdb.Uom_PK = mrnrdet.Uom_PK1;
                    smrndetdb.ExtraQty = mrnrdet.ExtraQty;
                    enty.StockMRNDetails.Add(smrndetdb);

                    enty.SaveChanges();



                    StockInventoryMaster sinvmstr = new StockInventoryMaster();

                    sinvmstr.SMRNDet_Pk = smrndetdb.SMRNDet_Pk;
                    sinvmstr.SPODetails_PK = mrnrdet.SPODetails_PK;
                    sinvmstr.Template_PK = mrnrdet.Template_PK;
                    sinvmstr.OnHandQty = 0;
                    sinvmstr.ReceivedQty = mrnrdet.ReceivedQty + mrnrdet.ExtraQty;
                    sinvmstr.DeliveredQty = mrnrdet.ReceivedQty + mrnrdet.ExtraQty;
                    sinvmstr.Unitprice = mrnrdet.Unitprice;
                    sinvmstr.Composition = mrnrdet.Composition;
                    sinvmstr.Construct = mrnrdet.Construct;
                    sinvmstr.TemplateColor = mrnrdet.TemplateColor;
                    sinvmstr.TemplateSize = mrnrdet.TemplateSize;
                    sinvmstr.TemplateWidth = mrnrdet.TemplateWidth;
                    sinvmstr.TemplateWeight = mrnrdet.TemplateWeight;
                    sinvmstr.Uom_PK = smrndetdb.Uom_PK;
                    sinvmstr.CuRate = mrnrdet.CuRate;
                    sinvmstr.ReceivedVia = "SMR";
                    sinvmstr.Location_Pk = smrnmstrdb.Location_Pk;
                    sinvmstr.Refnum = smrnmstrdb.SMrnNum;
                    sinvmstr.AddedDate = DateTime.Now.Date;
                    enty.StockInventoryMasters.Add(sinvmstr);
                    enty.SaveChanges();

                    InventorySalesDetail sinvdetdb = new InventorySalesDetail();
                    sinvdetdb.SalesDO_PK = trnmstr.SalesDO_PK;
                    sinvdetdb.SInventoryItem_PK = sinvmstr.SInventoryItem_PK;
                    sinvdetdb.DeliveryQty = sinvmstr.DeliveredQty;
                    sinvdetdb.CuRate = sinvmstr.CuRate;
                    enty.InventorySalesDetails.Add(sinvdetdb);








                }

                enty.SaveChanges();

                mrnum = smrnmstrdb.SMrnNum;
            }

            return mrnum;






        }
    }

    public class StockMRNDetailsData
    {
        public int SMRNDet_Pk { get; set; }
        public int SMRN_Pk { get; set; }
        public int SPODetails_PK { get; set; }
        public int SPO_PK { get; set; }
        public int Template_PK { get; set; }
        public string Composition { get; set; }
        public string Construct { get; set; }
        public string TemplateColor { get; set; }
        public string TemplateSize { get; set; }
        public string TemplateWidth { get; set; }
        public string TemplateWeight { get; set; }
        public string UOMCode { get; set; }
        public Decimal Unitprice { get; set; }
        public Decimal ReceivedQty { get; set; }
        public Decimal ExtraQty { get; set; }
        private int Uom_PK;


        public Decimal CuRate
        {
            get { return getCurate(); }
            set { CuRate = value; }
        }




        public int Uom_PK1
        {
            get { return getUOM_Pk(UOMCode); }
            set { Uom_PK = value; }
        }

        public int getUOM_Pk(String UOM)
        {
            int uompk = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from um in enty.UOMMasters
                         where um.UomCode.Trim() == UOM

                         select um.Uom_PK).FirstOrDefault();

                uompk = int.Parse(q.ToString());
            }

            return uompk;
        }
        public Decimal getCurate()
        {
            Decimal cu = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from um in enty.StockPODetails
                         where um.SPODetails_PK == this.SPODetails_PK

                         select um.CUrate).FirstOrDefault();

                cu = Decimal.Parse(q.ToString());
            }

            return cu;
        }

    }
    public class StockMRNMasterData
    {
        public int SMrn_PK { get; set; }
        public string SMrnNum { get; set; }
        public int SPo_PK { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Remark { get; set; }
        public string AddedBY { get; set; }
        public DateTime AddedDate { get; set; }
        public string DoNumber { get; set; }
        public int Location_Pk { get; set; }

        public int Reciept_Pk { get; set; }




    }

    public class StockRecieptMasterData
    {
        public int SReciept_Pk { get; set; }
        public string SRecieptNum { get; set; }
        public int RecptLocation_PK { get; set; }
        public string ContainerNum { get; set; }
        public string BOENum { get; set; }
        public string Remark { get; set; }
        public DateTime InhouseDate { get; set; }
        public DateTime Deliverydate { get; set; }
        public int Supplier_PK { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string IsCompleted { get; set; }



        public String InsertReciptMstr(StockRecieptMasterData reptmstr)
        {

            String num = "";
            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {
                StockRecieptMaster reptdb = new StockRecieptMaster();


                reptdb.RecptLocation_PK = reptmstr.RecptLocation_PK;
                reptdb.ContainerNum = reptmstr.ContainerNum;
                reptdb.BOENum = reptmstr.BOENum;
                reptdb.Remark = reptmstr.Remark;
                reptdb.InhouseDate = DateTime.Now;
                reptdb.Deliverydate = DateTime.Now;
                reptdb.Supplier_PK = reptmstr.Supplier_PK;
                reptdb.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                reptdb.AddedDate = DateTime.Now;
                reptdb.IsCompleted = reptmstr.IsCompleted;


                entry.StockRecieptMasters.Add(reptdb);

                entry.SaveChanges();

                reptdb.StockRecieptNum = "SR" + HttpContext.Current.Session["lOC_Code"].ToString().Trim() + reptdb.SReciept_Pk.ToString().PadLeft(6, '0');
                num = reptdb.StockRecieptNum;

                entry.SaveChanges();


            }


            return num;

        }


    }



    # region DeliveryOrderStock

    public class StockDeliveryOrder
    {
        public StockDeliveryOrderMasterData Domstrdata { get; set; }
        public List<StockDeliveryOrderDetailsData> DeliveryOrderDetailsDataCollection { get; set; }

        public String insertStockWarehouseDO(StockDeliveryOrder Dodata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                DeliveryOrderStockMaster domstr = new DeliveryOrderStockMaster();

                domstr.AddedDate = DateTime.Now;
                domstr.AddedBy = Dodata.Domstrdata.AddedBy;
                domstr.ToLocation_PK = Dodata.Domstrdata.ToLocation_PK;
                domstr.FromLocation_PK = Dodata.Domstrdata.FromLocation_PK;
                domstr.DoType = Dodata.Domstrdata.DoType;
                domstr.BoeNum = Dodata.Domstrdata.BoeNum;
                domstr.ContainerNumber = Dodata.Domstrdata.ContainerNumber;
                domstr.DeliveryDate = Dodata.Domstrdata.DeliveryDate;
                enty.DeliveryOrderStockMasters.Add(domstr);


                enty.SaveChanges();

                Donum = domstr.SDONum = CodeGenerator.GetUniqueCode("SDOWW", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(domstr.SDO_PK.ToString()));




                foreach (StockDeliveryOrderDetailsData di in Dodata.DeliveryOrderDetailsDataCollection)
                {
                    //Add the delivery details
                    DeliveryOrderStockDetail dlvrdet = new DeliveryOrderStockDetail();
                    dlvrdet.SDO_PK = domstr.SDO_PK;
                    dlvrdet.SInventoryItem_PK = di.InventoryItem_PK;
                    dlvrdet.DeliveryQty = di.DeliveryQty;

                    enty.DeliveryOrderStockDetails.Add(dlvrdet);

                    int SMRNDet_Pk = 0;
                    int SPODetails_PK = 0;
                    int Template_PK = 0;


                    Decimal Unitprice = 0;
                    String Composition = "";
                    String Construct = "";
                    String TemplateColor = "";
                    String TemplateSize = "";
                    String TemplateWidth = "";
                    String TemplateWeight = "";
                    int Uom_PK = 0;

                    // Reduce Goods in Inventory
                    var q = from invitem in enty.StockInventoryMasters
                            where invitem.SInventoryItem_PK == di.InventoryItem_PK
                            select invitem;

                    foreach (var invitemdetail in q)
                    {





                        SMRNDet_Pk = int.Parse(invitemdetail.SMRNDet_Pk.ToString());
                        SPODetails_PK = int.Parse(invitemdetail.SPODetails_PK.ToString());
                        Template_PK = int.Parse(invitemdetail.Template_PK.ToString());

                        Unitprice = Decimal.Parse(invitemdetail.Unitprice.ToString());
                        Composition = invitemdetail.Composition;
                        Construct = invitemdetail.Construct;
                        TemplateColor = invitemdetail.TemplateColor;
                        TemplateSize = invitemdetail.TemplateSize;
                        TemplateWidth = invitemdetail.TemplateWidth;
                        TemplateWeight = invitemdetail.TemplateWeight;
                        Uom_PK = int.Parse(invitemdetail.Uom_PK.ToString());





                        invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + di.DeliveryQty;
                        invitemdetail.OnHandQty = invitemdetail.OnHandQty - di.DeliveryQty;

                    }
                    //Add goods to transit
                    StockGoodsInTransit gtn = new StockGoodsInTransit();
                    gtn.SInventoryItem_PK = di.InventoryItem_PK; ;
                    gtn.SDO_PK = domstr.SDO_PK;
                    gtn.TransitQty = di.DeliveryQty;
                    enty.StockGoodsInTransits.Add(gtn);
                    enty.SaveChanges();


                }
                enty.SaveChanges();

            }


            return Donum;
        }



        public String insertStockFactoryDO(StockDeliveryOrder Dodata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                DeliveryOrderStockMaster domstr = new DeliveryOrderStockMaster();

                domstr.AddedDate = DateTime.Now;
                domstr.AddedBy = Dodata.Domstrdata.AddedBy;
                domstr.ToLocation_PK = Dodata.Domstrdata.ToLocation_PK;
                domstr.FromLocation_PK = Dodata.Domstrdata.FromLocation_PK;
                domstr.DoType = Dodata.Domstrdata.DoType;
                domstr.BoeNum = Dodata.Domstrdata.BoeNum;
                domstr.ContainerNumber = Dodata.Domstrdata.ContainerNumber;
                domstr.DeliveryDate = Dodata.Domstrdata.DeliveryDate;
                enty.DeliveryOrderStockMasters.Add(domstr);


                enty.SaveChanges();

                Donum = domstr.SDONum = CodeGenerator.GetUniqueCode("SWF", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(domstr.SDO_PK.ToString()));




                foreach (StockDeliveryOrderDetailsData di in Dodata.DeliveryOrderDetailsDataCollection)
                {
                    //Add the delivery details
                    DeliveryOrderStockDetail dlvrdet = new DeliveryOrderStockDetail();
                    dlvrdet.SDO_PK = domstr.SDO_PK;
                    dlvrdet.SInventoryItem_PK = di.InventoryItem_PK;
                    dlvrdet.DeliveryQty = di.DeliveryQty;

                    enty.DeliveryOrderStockDetails.Add(dlvrdet);
                    int SMRNDet_Pk = 0;
                    int SPODetails_PK = 0;
                    int Template_PK = 0;
                    int uom_pk = 0;
                    Decimal Unitprice = 0;
                    String Composition = "";
                    String Construct = "";
                    String TemplateColor = "";
                    String TemplateSize = "";
                    String TemplateWidth = "";
                    String TemplateWeight = "";
                    decimal curate = 0;

                    var q = from invitem in enty.StockInventoryMasters
                            where invitem.SInventoryItem_PK == di.InventoryItem_PK
                            select invitem;

                    foreach (var invitemdetail in q)
                    {

                        Composition = invitemdetail.Composition.ToString();
                        Construct = invitemdetail.Construct.ToString();
                        TemplateColor = invitemdetail.TemplateColor.ToString();
                        TemplateSize = invitemdetail.TemplateSize.ToString();
                        TemplateWidth = invitemdetail.TemplateWidth.ToString();
                        TemplateWeight = invitemdetail.TemplateWeight.ToString();
                        Template_PK = int.Parse(invitemdetail.Template_PK.ToString());
                        SMRNDet_Pk = int.Parse(invitemdetail.SMRNDet_Pk.ToString());
                        SPODetails_PK = int.Parse(invitemdetail.SPODetails_PK.ToString());
                        Unitprice = decimal.Parse(invitemdetail.Unitprice.ToString());
                        curate = decimal.Parse(invitemdetail.CuRate.ToString());

                        uom_pk = int.Parse(invitemdetail.Uom_PK.ToString());



                        invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + di.DeliveryQty;
                        invitemdetail.OnHandQty = invitemdetail.OnHandQty - di.DeliveryQty;
                    }


                    StockInventoryMaster sinvmstr = new StockInventoryMaster();

                    sinvmstr.SMRNDet_Pk = SMRNDet_Pk;
                    sinvmstr.SPODetails_PK = SPODetails_PK;
                    sinvmstr.Template_PK = Template_PK;
                    sinvmstr.OnHandQty = di.DeliveryQty;
                    sinvmstr.ReceivedQty = di.DeliveryQty;
                    sinvmstr.DeliveredQty = 0;
                    sinvmstr.Unitprice = Unitprice;
                    sinvmstr.Composition = Composition;
                    sinvmstr.Construct = Construct;
                    sinvmstr.TemplateColor = TemplateColor;
                    sinvmstr.TemplateSize = TemplateSize;
                    sinvmstr.TemplateWidth = TemplateWidth;
                    sinvmstr.TemplateWeight = TemplateWeight;
                    sinvmstr.Uom_PK = uom_pk;
                    sinvmstr.ReceivedVia = "FR";
                    sinvmstr.CuRate = curate;
                    sinvmstr.Location_Pk = Dodata.Domstrdata.ToLocation_PK ;
                    sinvmstr.Refnum = domstr.SDONum;
                    enty.StockInventoryMasters.Add(sinvmstr);


                    enty.SaveChanges();


                }
                enty.SaveChanges();

            }


            return Donum;
        }

        ///// <summary>
        ///// Give Do to the factory and Make Auto Reciept of the DO
        ///// </summary>
        ///// <param name="Dodata"></param>
        ///// <returns></returns>
        //public String insertFactoryDO(StockDeliveryOrder Dodata)
        //{
        //    String Donum = "";
        //    using (ArtEntitiesnew enty = new ArtEntitiesnew())
        //    {
        //        DeliveryOrderMaster domstr = new DeliveryOrderMaster();
        //        domstr.AtcID = Dodata.Domstrdata.AtcID;
        //        domstr.AddedDate = DateTime.Now;
        //        domstr.AddedBy = Dodata.Domstrdata.AddedBy;
        //        domstr.ToLocation_PK = Dodata.Domstrdata.ToLocation_PK;
        //        domstr.FromLocation_PK = Dodata.Domstrdata.FromLocation_PK;
        //        domstr.DoType = Dodata.Domstrdata.DoType;
        //        domstr.BoeNum = Dodata.Domstrdata.BoeNum;
        //        domstr.ContainerNumber = Dodata.Domstrdata.ContainerNumber;
        //        domstr.DeliveryDate = Dodata.Domstrdata.DeliveryDate;
        //        enty.DeliveryOrderMasters.Add(domstr);


        //        enty.SaveChanges();

        //        Donum = domstr.DONum = CodeGenerator.GetUniqueCode("WF", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(domstr.DO_PK.ToString()));




        //        foreach (StockDeliveryOrderDetailsData di in Dodata.DeliveryOrderDetailsDataCollection)
        //        {
        //            //Add the delivery details
        //            DeliveryOrderDetail dlvrdet = new DeliveryOrderDetail();
        //            dlvrdet.DO_PK = domstr.DO_PK;
        //            dlvrdet.InventoryItem_PK = di.InventoryItem_PK;
        //            dlvrdet.DeliveryQty = di.DeliveryQty;

        //            enty.DeliveryOrderDetails.Add(dlvrdet);

        //            enty.SaveChanges();

        //            int mrndet_pk = 0;
        //            int podet_pk = 0;
        //            int skudetPK = 0;
        //            decimal curate = 0;
        //            int uom_pk = 0;
        //            // Reduce Goods in Inventory
        //            var q = from invitem in enty.InventoryMasters
        //                    where invitem.InventoryItem_PK == di.InventoryItem_PK
        //                    select invitem;

        //            foreach (var invitemdetail in q)
        //            {

        //                invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + di.DeliveryQty;
        //                invitemdetail.OnhandQty = invitemdetail.OnhandQty - di.DeliveryQty;

        //                mrndet_pk = int.Parse(invitemdetail.MrnDet_PK.ToString());
        //                podet_pk = int.Parse(invitemdetail.PoDet_PK.ToString());
        //                skudetPK = int.Parse(invitemdetail.SkuDet_Pk.ToString());
        //                curate = decimal.Parse(invitemdetail.CURate.ToString());
        //                uom_pk = int.Parse(invitemdetail.Uom_Pk.ToString());
        //            }
        //            //Add goods to transit
        //            InventoryMaster invmstr = new InventoryMaster();
        //            invmstr.MrnDet_PK = mrndet_pk;
        //            invmstr.PoDet_PK = podet_pk;
        //            invmstr.SkuDet_Pk = skudetPK;
        //            invmstr.ReceivedQty = di.DeliveryQty;
        //            invmstr.OnhandQty = di.DeliveryQty;
        //            invmstr.DeliveredQty = 0;
        //            invmstr.ReceivedVia = "FR";
        //            invmstr.Location_PK = Dodata.Domstrdata.ToLocation_PK;
        //            invmstr.CURate = curate;
        //            invmstr.AddedDate = DateTime.Now.Date;
        //            invmstr.Uom_Pk = uom_pk;
        //            invmstr.Refnum = domstr.DONum;
        //            enty.InventoryMasters.Add(invmstr);


        //        }
        //        enty.SaveChanges();

        //    }


        //    return Donum;
        //}


        ///// <summary>
        ///// Transfer Fabric from One warehouse to factory based on Cut order
        ///// </summary>
        ///// <param name="Dodata"></param>
        ///// <returns></returns>
        //public String insertFactoryFabricDO(StockDeliveryOrder Dodata)
        //{
        //    String Donum = "";
        //    using (ArtEntitiesnew enty = new ArtEntitiesnew())
        //    {
        //        DeliveryOrderMaster domstr = new DeliveryOrderMaster();
        //        domstr.AtcID = Dodata.Domstrdata.AtcID;
        //        domstr.AddedDate = DateTime.Now;
        //        domstr.AddedBy = Dodata.Domstrdata.AddedBy;
        //        domstr.ToLocation_PK = Dodata.Domstrdata.ToLocation_PK;
        //        domstr.FromLocation_PK = Dodata.Domstrdata.FromLocation_PK;
        //        domstr.DoType = Dodata.Domstrdata.DoType;
        //        domstr.BoeNum = Dodata.Domstrdata.BoeNum;
        //        domstr.ContainerNumber = Dodata.Domstrdata.ContainerNumber;
        //        domstr.DeliveryDate = Dodata.Domstrdata.DeliveryDate;
        //        enty.DeliveryOrderMasters.Add(domstr);


        //        enty.SaveChanges();

        //        Donum = domstr.DONum = CodeGenerator.GetUniqueCode("WF", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(domstr.DO_PK.ToString()));




        //        foreach (StockDeliveryOrderDetailsData di in Dodata.DeliveryOrderDetailsDataCollection)
        //        {
        //            //Add the delivery details
        //            DeliveryOrderDetail dlvrdet = new DeliveryOrderDetail();
        //            dlvrdet.DO_PK = domstr.DO_PK;
        //            dlvrdet.InventoryItem_PK = di.InventoryItem_PK;
        //            dlvrdet.DeliveryQty = di.DeliveryQty;

        //            enty.DeliveryOrderDetails.Add(dlvrdet);

        //            enty.SaveChanges();

        //            int mrndet_pk = 0;
        //            int podet_pk = 0;
        //            int skudetPK = 0;
        //            decimal curate = 0;
        //            int uom_pk = 0;
        //            // Reduce Goods in Inventory
        //            var q = from invitem in enty.InventoryMasters
        //                    where invitem.InventoryItem_PK == di.InventoryItem_PK
        //                    select invitem;

        //            foreach (var invitemdetail in q)
        //            {

        //                invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + di.DeliveryQty;
        //                invitemdetail.OnhandQty = invitemdetail.OnhandQty - di.DeliveryQty;

        //                mrndet_pk = int.Parse(invitemdetail.MrnDet_PK.ToString());
        //                podet_pk = int.Parse(invitemdetail.PoDet_PK.ToString());
        //                skudetPK = int.Parse(invitemdetail.SkuDet_Pk.ToString());
        //                curate = decimal.Parse(invitemdetail.CURate.ToString());
        //                uom_pk = int.Parse(invitemdetail.Uom_Pk.ToString());
        //            }
        //            //Add goods to transit
        //            InventoryMaster invmstr = new InventoryMaster();
        //            invmstr.MrnDet_PK = mrndet_pk;
        //            invmstr.PoDet_PK = podet_pk;
        //            invmstr.SkuDet_Pk = skudetPK;
        //            invmstr.ReceivedQty = di.DeliveryQty;
        //            invmstr.OnhandQty = di.DeliveryQty;
        //            invmstr.DeliveredQty = 0;
        //            invmstr.ReceivedVia = "FR";
        //            invmstr.Location_PK = Dodata.Domstrdata.ToLocation_PK;
        //            invmstr.CURate = curate;
        //            invmstr.AddedDate = DateTime.Now.Date;

        //            invmstr.Refnum = domstr.DONum;
        //            enty.InventoryMasters.Add(invmstr);




        //            CutOrderDO ctordrdo = new CutOrderDO();
        //            ctordrdo.CutID = di.Cutid;
        //            ctordrdo.Skudet_PK = skudetPK;
        //            ctordrdo.DoDet_Pk = dlvrdet.DODet_PK;
        //            ctordrdo.DeliveryQty = dlvrdet.DeliveryQty;
        //            enty.CutOrderDOes.Add(ctordrdo);




        //        }
        //        enty.SaveChanges();

        //    }


        //    return Donum;
        //}

    }



    public class StockDeliveryOrderMasterData
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

    }

    public class StockDeliveryOrderDetailsData
    {
        public int DODet_PK { get; set; }
        public int DO_PK { get; set; }
        public int InventoryItem_PK { get; set; }
        public Decimal DeliveryQty { get; set; }
        public string Remark { get; set; }

        public int Cutid { get; set; }
    }


    #endregion
    public class StockDeliveryReciept
    {
        public StockDeliveryRecieptData Domstrdata { get; set; }
        public List<StockDeliveryRecieptDetailsData> DeliveryOrderDetailsDataCollection { get; set; }



        public string insertStockDOR()
        {
            String msg = "";

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                DeliveryStockReceiptMaster dorcpt = new DeliveryStockReceiptMaster();
                dorcpt.SDO_PK = this.Domstrdata.SDO_PK;
                dorcpt.AddedDate = DateTime.Now;
                dorcpt.AddedBy = this.Domstrdata.AddedBy;
                dorcpt.Location_PK = this.Domstrdata.Location_PK; ;
                dorcpt.DOReceiptType = this.Domstrdata.DoRecieptType;

                enty.DeliveryStockReceiptMasters.Add(dorcpt);


                enty.SaveChanges();

                dorcpt.SDORNum = "SWR" + HttpContext.Current.Session["lOC_Code"].ToString().Trim() + dorcpt.SDOR_PK.ToString().PadLeft(6, '0');


                foreach (StockDeliveryRecieptDetailsData di in this.DeliveryOrderDetailsDataCollection)
                {
                    DeliveryReceiptStockDetail drd = new DeliveryReceiptStockDetail();
                    drd.SDODet_PK = di.SDODet_PK;
                    drd.SDOR_PK = dorcpt.SDOR_PK;
                    drd.ReceivedQty = di.ReceivedQty;
                    drd.SInventoryItem_PK = di.SInventoryItem_PK;

                    enty.DeliveryReceiptStockDetails.Add(drd);


                    int SMRNDet_Pk = 0;
                    int SPODetails_PK = 0;
                    int Template_PK = 0;
                    int uom_pk = 0;
                    Decimal Unitprice = 0;
                    String Composition = "";
                    String Construct = "";
                    String TemplateColor = "";
                    String TemplateSize = "";
                    String TemplateWidth = "";
                    String TemplateWeight = "";
                    decimal curate = 0;

                    var q = from invitem in enty.StockInventoryMasters
                            where invitem.SInventoryItem_PK == di.SInventoryItem_PK
                            select invitem;

                    foreach (var invitemdetail in q)
                    {

                        Composition = invitemdetail.Composition.ToString();
                        Construct = invitemdetail.Construct.ToString();
                        TemplateColor = invitemdetail.TemplateColor.ToString();
                        TemplateSize = invitemdetail.TemplateSize.ToString();
                        TemplateWidth = invitemdetail.TemplateWidth.ToString();
                        TemplateWeight = invitemdetail.TemplateWeight.ToString();
                        Template_PK = int.Parse(invitemdetail.Template_PK.ToString());
                        SMRNDet_Pk = int.Parse(invitemdetail.SMRNDet_Pk.ToString());
                        SPODetails_PK = int.Parse(invitemdetail.SPODetails_PK.ToString());
                        Unitprice = decimal.Parse(invitemdetail.Unitprice.ToString());
                        curate = decimal.Parse(invitemdetail.CuRate.ToString());
                        
                        uom_pk = int.Parse(invitemdetail.Uom_PK.ToString());
                    }

                    var q1 = from godsintran in enty.StockGoodsInTransits
                             where godsintran.SDO_PK.ToString().Trim() == this.Domstrdata.SDO_PK.ToString().Trim() && godsintran.SInventoryItem_PK.ToString().Trim() == di.SInventoryItem_PK.ToString().Trim()
                             select godsintran;

                    foreach (var trans in q1)
                    {
                        trans.TransitQty = trans.TransitQty - di.ReceivedQty;
                    }


                    StockInventoryMaster sinvmstr = new StockInventoryMaster();

                    sinvmstr.SMRNDet_Pk = SMRNDet_Pk;
                    sinvmstr.SPODetails_PK = SPODetails_PK;
                    sinvmstr.Template_PK = Template_PK;
                    sinvmstr.OnHandQty = di.ReceivedQty;
                    sinvmstr.ReceivedQty = di.ReceivedQty;
                    sinvmstr.DeliveredQty = 0;
                    sinvmstr.Unitprice = Unitprice;
                    sinvmstr.Composition = Composition;
                    sinvmstr.Construct = Construct;
                    sinvmstr.TemplateColor = TemplateColor;
                    sinvmstr.TemplateSize = TemplateSize;
                    sinvmstr.TemplateWidth = TemplateWidth;
                    sinvmstr.TemplateWeight = TemplateWeight;
                    sinvmstr.Uom_PK = uom_pk;
                    sinvmstr.ReceivedVia = "SWR";
                    sinvmstr.CuRate = curate;
                    sinvmstr.Location_Pk = this.Domstrdata.Location_PK; ;
                    sinvmstr.Refnum = dorcpt.SDORNum;
                    enty.StockInventoryMasters.Add(sinvmstr);


                    enty.SaveChanges();


                }
               msg = "DO # : " + dorcpt.SDORNum + " is generated Sucessfully";
            }

            


            return msg;

            //enty.SaveChanges();
        }




    }
    public class StockDeliveryRecieptData
    {
        public int SDO_PK { get; set; }
        public string SDORNum { get; set; }

        public int Location_PK { get; set; }


        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string DoRecieptType { get; set; }

    }

    public class StockDeliveryRecieptDetailsData
    {
        public int DODet_PK { get; set; }
        public int SDOR_PK { get; set; }
        public int SInventoryItem_PK { get; set; }
        public Decimal ReceivedQty { get; set; }


        public int SDODet_PK { get; set; }
    }
}