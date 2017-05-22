using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.InventoryBLL
{
    //public class RoData
    //{



    //    public int RO_pk { get; set; }

    //    public int RoDet_PK { get; set; }

    //    public int FromSkuDet_PK { get; set; } 
    //    public int ToSkuDet_Pk { get; set; }
    //    public int InventoryItem_PK { get; set; } 
    //    public Decimal  RoQty { get; set; }
    //    public Decimal CuRate { get; set; }

    //    public  DataTable Rodetails;

    //    public DataTable Rodetails1
    //    {
    //        get
    //        {
    //            DBTransaction.ProcurementTransaction pktrans = new DBTransaction.ProcurementTransaction();
    //            return pktrans.GetRoDetails(RO_pk);
    //        }
    //        set { Rodetails = value; }
    //    }



    //}





    public class ROIN
    {
        public ROINMasterData RoinmastrData { get; set; }

        public List<BLL.ProcurementBLL.RoDetailsData> rodetaildata { get; set; }




        public Boolean ifStockAvailable(int ro_pk)
        {
            Boolean isbalancethere = true;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                try
                {
                    var rodata = from rodet in enty.RequestOrderDetails
                                 join inventry in enty.InventoryMasters
                                 on rodet.InventoryItem_PK equals inventry.InventoryItem_PK
                                 where rodet.RO_Pk == ro_pk
                                 select new { rodet.Qty, inventry.OnhandQty };

                    foreach (var element in rodata)
                    {
                        if (element.OnhandQty < element.Qty)
                        {
                            isbalancethere = false;
                        }

                    }
                }
                catch (Exception)
                {

                    isbalancethere = false;
                }


            }

            return isbalancethere;
            }

        public String insertRomaterial(ROIN roin)
        {
            string RONUM = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                ROINMaster roinmstrdb = new ROINMaster();



                roinmstrdb.RO_Pk = roin.RoinmastrData.RO_Pk;
                roinmstrdb.AddedBy = roin.RoinmastrData.AddedBy;
                roinmstrdb.AddedDate = DateTime.Now;
                roinmstrdb.Location_pk = roin.RoinmastrData.Location_pk;
                roinmstrdb.ROInType = "ATC";
                enty.ROINMasters.Add(roinmstrdb);

                enty.SaveChanges();

                roinmstrdb.ROInNum = "RI" + HttpContext.Current.Session["lOC_Code"].ToString().Trim() + roinmstrdb.ROIN_PK.ToString().PadLeft(6, '0');

                MrnMaster mrnmstr = new MrnMaster();
                mrnmstr.DoNumber = roinmstrdb.ROInNum;
                mrnmstr.AddedDate = DateTime.Now;
                mrnmstr.Po_PK = roin.RoinmastrData.GetROPO_PK(roin.RoinmastrData.RO_Pk);
                mrnmstr.AddedBY = roin.RoinmastrData.AddedBy;
                mrnmstr.MrnNum = roinmstrdb.ROInNum;
                mrnmstr.Location_Pk = roin.RoinmastrData.Location_pk;
                mrnmstr.Reciept_Pk = 10145;//dummy receipt against Inventory Bureuo
                enty.MrnMasters.Add(mrnmstr);
                enty.SaveChanges();


                foreach (BLL.ProcurementBLL.RoDetailsData rdet in roin.rodetaildata)
                {
                    RoInDetail rodetdb = new RoInDetail();
                    rodetdb.RODet_Pk = rdet.RODet_Pk;
                    rodetdb.ROIN_PK = roinmstrdb.ROIN_PK;
                    rodetdb.FromSkuDet_PK = rdet.FromSkuDet_PK;
                    rodetdb.ToSkuDet_Pk = rdet.ToSkuDet_PK;
                    rodetdb.InventoryItem_PK = rdet.InventoryItem_PK;
                    rodetdb.CuRate = rdet.UnitPrice;
                    rodetdb.RoQty = rdet.Qty;
                    enty.RoInDetails.Add(rodetdb);





                    MrnDetail mrndetdb = new MrnDetail();
                    mrndetdb.Mrn_PK = mrnmstr.Mrn_PK;
                    mrndetdb.PODet_PK = rdet.GetPODet_PK(int.Parse(mrnmstr.Po_PK.ToString()));
                    mrndetdb.SkuDet_PK = rdet.ToSkuDet_PK;
                    mrndetdb.ReceiptQty = rdet.Qty;
                    mrndetdb.ExtraQty = 0;
                    mrndetdb.Remark = "";
                    mrndetdb.Uom_PK = rdet.GetuomPK(rdet.InventoryItem_PK);
                    enty.MrnDetails.Add(mrndetdb);

                    enty.SaveChanges();














                    int mrndet_pk = 0;
                    int podet_pk = 0;
                    int skudetPK = 0;
                    decimal curate = 0;
                    int uom_pk = 0;

                    var Q = from invitems in enty.InventoryMasters
                            where invitems.InventoryItem_PK == rdet.InventoryItem_PK
                            select invitems;


                    foreach (var element in Q)
                    {


                        skudetPK = int.Parse(element.SkuDet_Pk.ToString());
                        mrndet_pk = int.Parse(element.MrnDet_PK.ToString());
                        podet_pk = int.Parse(element.PoDet_PK.ToString());
                        curate = decimal.Parse(element.CURate.ToString());
                        uom_pk = int.Parse(element.Uom_Pk.ToString());

                        element.DeliveredQty = element.DeliveredQty + rdet.Qty;
                        element.OnhandQty = element.OnhandQty - rdet.Qty;


                    }



                    InventoryMaster invmstr = new InventoryMaster();
                    invmstr.MrnDet_PK = mrndetdb.MrnDet_PK;
                    invmstr.PoDet_PK = mrndetdb.PODet_PK;
                    invmstr.SkuDet_Pk = rdet.ToSkuDet_PK;
                    invmstr.Uom_Pk = mrndetdb.Uom_PK;
                    invmstr.ReceivedQty = rdet.Qty;
                    invmstr.OnhandQty = rdet.Qty;
                    invmstr.CURate = rdet.GetCURate(int.Parse(mrndetdb.PODet_PK.ToString()));
                    invmstr.DeliveredQty = 0;
                    invmstr.ReceivedVia = "ROIN";
                    invmstr.Location_PK = roinmstrdb.Location_pk;
                    invmstr.Refnum = roinmstrdb.ROInNum;
                    enty.InventoryMasters.Add(invmstr);


                    var q = from rqmstr in enty.RequestOrderMasters
                            where rqmstr.RO_Pk == roinmstrdb.RO_Pk
                            select rqmstr;

                    foreach (var element in q)
                    {
                        element.IsCompleted = "Y";
                    }




                }

                enty.SaveChanges();
                RONUM = roinmstrdb.ROInNum;
            }
            return RONUM;
        }



        /// <summary>
        /// Stock RO IN
        /// </summary>
        /// <param name="roin"></param>
        /// <returns></returns>
        public String insertStockRomaterial(ROIN roin)
        {
            string RONUM = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                RoInStockMaster roinmstrdb = new RoInStockMaster();



                roinmstrdb.SRO_Pk = roin.RoinmastrData.RO_Pk;
                roinmstrdb.AddedBy = roin.RoinmastrData.AddedBy;
                roinmstrdb.AddedDate = DateTime.Now;
                roinmstrdb.Location_pk = roin.RoinmastrData.Location_pk;
                roinmstrdb.RoInStockType = "GStock";
                enty.RoInStockMasters.Add(roinmstrdb);

                enty.SaveChanges();

                roinmstrdb.RoInStockNum = "SROIN" + HttpContext.Current.Session["lOC_Code"].ToString().Trim() + roinmstrdb.RoInStock_PK.ToString().PadLeft(4, '0');


                MrnMaster mrnmstr = new MrnMaster();
                mrnmstr.DoNumber = roinmstrdb.RoInStockNum;
                mrnmstr.AddedDate = DateTime.Now;
                mrnmstr.Po_PK = roin.RoinmastrData.GetSroPO_PK(roin.RoinmastrData.RO_Pk);
                mrnmstr.AddedBY = roin.RoinmastrData.AddedBy;
                mrnmstr.MrnNum = roinmstrdb.RoInStockNum;
                mrnmstr.Location_Pk = roin.RoinmastrData.Location_pk;
                mrnmstr.Reciept_Pk = 10145;//dummy receipt against Inventory Bureuo
                enty.MrnMasters.Add(mrnmstr);
                enty.SaveChanges();





                foreach (BLL.ProcurementBLL.RoDetailsData rdet in roin.rodetaildata)
                {
                    RoInStockDetail rodetdb = new RoInStockDetail();
                    rodetdb.RODet_Pk = rdet.RODet_Pk;
                    rodetdb.RoInStock_PK = roinmstrdb.RoInStock_PK;

                    rodetdb.ToSkuDet_Pk = rdet.ToSkuDet_PK;
                    rodetdb.InventoryItem_PK = rdet.InventoryItem_PK;
                    rodetdb.CuRate = rdet.UnitPrice;
                    rodetdb.RoQty = rdet.Qty;
                    enty.RoInStockDetails.Add(rodetdb);



                    MrnDetail mrndetdb = new MrnDetail();
                    mrndetdb.Mrn_PK = mrnmstr.Mrn_PK;
                    mrndetdb.PODet_PK =  rdet.GetPODet_PK(int.Parse (mrnmstr.Po_PK.ToString ())); 
                    mrndetdb.SkuDet_PK = rdet.ToSkuDet_PK;
                    mrndetdb.ReceiptQty = rdet.Qty;
                    mrndetdb.ExtraQty =0;
                    mrndetdb.Remark = "";
                    mrndetdb.Uom_PK = rdet.GetsTOCKuomPK(rdet.InventoryItem_PK);
                    enty.MrnDetails.Add(mrndetdb);

                    enty.SaveChanges();

                    Decimal curate = 0;

                    var Q = from invitems in enty.StockInventoryMasters
                            where invitems.SInventoryItem_PK == rdet.InventoryItem_PK
                            select invitems;


                    foreach (var element in Q)
                    {                      
                        element.DeliveredQty = element.DeliveredQty + rdet.Qty;
                        element.OnHandQty = element.OnHandQty - rdet.Qty;
                      

                    }

                   
                    InventoryMaster invmstr = new InventoryMaster();
                    invmstr.MrnDet_PK = mrndetdb.MrnDet_PK;
                    invmstr.PoDet_PK = mrndetdb.PODet_PK;
                    invmstr.SkuDet_Pk = rdet.ToSkuDet_PK;
                    invmstr.Uom_Pk = mrndetdb.Uom_PK;
                    invmstr.ReceivedQty = rdet.Qty;
                    invmstr.OnhandQty = rdet.Qty;
                    invmstr.CURate = rdet.GetCURate(int.Parse(mrndetdb.PODet_PK.ToString()));
                    invmstr.DeliveredQty = 0;
                    invmstr.ReceivedVia = "SROIN";
                    invmstr.Location_PK = roinmstrdb.Location_pk;
                    invmstr.Refnum = roinmstrdb.RoInStockNum;
                    enty.InventoryMasters.Add(invmstr);




                    var q = from rqmstr in enty.RequestOrderStockMasters
                            where rqmstr.SRO_Pk == roinmstrdb.SRO_Pk
                            select rqmstr;

                    foreach (var element in q)
                    {
                        element.Iscompleted = "Y";
                    }




                }

                enty.SaveChanges();
                RONUM = roinmstrdb.RoInStockNum;
            }
            return RONUM;
        }












        public void DoRoTransfer(List<BLL.ProcurementBLL.RoDetailsData> rodetaildata)
        {

        }
    }


    public class ROINMasterData
    {

        public int ROIN_PK { get; set; }
        public int RO_Pk { get; set; }
        public string AddedBy { get; set; }

        public string Ronum { get; set; }
        public DateTime AddedDate { get; set; }

        public int Location_pk { get; set; }


        
        /// <summary>
        /// GET THE PO NUMBER OF SRO
        /// </summary>
        /// <param name="sro_pk"></param>
        /// <returns></returns>
        public int GetSroPO_PK(int sro_pk)
        {
            int PO_PK = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from skudet in enty.RequestOrderStockMasters
                         where skudet.SRO_Pk == sro_pk
                         select skudet.PO_PK).FirstOrDefault();

                PO_PK = int.Parse(q.ToString());
            }

            return PO_PK;
        }




        /// <summary>
        /// GET THE PO NUMBER OF RO
        /// </summary>
        /// <param name="sro_pk"></param>
        /// <returns></returns>
        public int GetROPO_PK(int ro_pk)
        {
            int PO_PK = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from skudet in enty.RequestOrderMasters
                         where skudet.RO_Pk == ro_pk
                         select skudet.PO_PK).FirstOrDefault();

                PO_PK = int.Parse(q.ToString());
            }

            return PO_PK;
        }


        public void insertroinmst(ROINMasterData roinms)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                ROINMasterData roinmsdb = new ROINMasterData();
                roinmsdb.ROIN_PK = roinms.ROIN_PK;
                roinmsdb.RO_Pk = roinms.RO_Pk;
                roinmsdb.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                roinmsdb.AddedDate = DateTime.Now;



            }


        }

    }


}