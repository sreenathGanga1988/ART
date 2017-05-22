using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.ProcurementBLL
{

    public class RequestOrderMasterData
    {
        public int RO_Pk { get; set; }
        public int ToAtcid { get; set; }
        public string RONum { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IsApproved { get; set; }
        public string IsDeleted { get; set; }
        public string AddedBy { get; set; }
        public string DeletedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime Approveddate { get; set; }
        public int ToSkuDet_PK { get; set; }

        public int Location_Pk { get; set; }

        public List<RoDetailsData> RoDetailsDataCollection { get; set; }


        public List<RoStockDetailsData> RoStockDetailsDataCollection { get; set; }





        public int GetsTOCKuomPK(int sinventotyitem_pk)
        {
            int Uom_PK = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from skudet in enty.StockInventoryMasters
                         where skudet.SInventoryItem_PK == sinventotyitem_pk
                         select skudet.Uom_PK).FirstOrDefault();

                Uom_PK = int.Parse(q.ToString());
            }

            return Uom_PK;
        }


        public int GetuomPK(int inventotyitem_pk)
        {
            int Uom_PK = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from skudet in enty.InventoryMasters
                         where skudet.InventoryItem_PK == inventotyitem_pk
                         select skudet.Uom_Pk).FirstOrDefault();

                Uom_PK = int.Parse(q.ToString());
            }

            return Uom_PK;
        }




        public int GetToAtcID(int toskudet_pk)
        {
            int atcid = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from skudet in enty.SkuRawmaterialDetails
                         join skumstr in enty.SkuRawMaterialMasters
                         on skudet.Sku_PK equals skumstr.Sku_Pk
                         where skudet.SkuDet_PK==toskudet_pk
                         select skumstr.Atc_id).FirstOrDefault();

                atcid = int.Parse(q.ToString());
            }

            return atcid;
        }

        /// <summary>
        /// insert normal RO
        /// </summary>
        /// <param name="rmmstr"></param>
        /// <returns></returns>
        public String insertRowmaterial(RequestOrderMasterData rmmstr)
        {
            string RONUM = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                RequestOrderMaster romstrdb = new RequestOrderMaster();


                romstrdb.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                romstrdb.CreatedDate = DateTime.Now;
                romstrdb.IsApproved = "N";
                romstrdb.IsDeleted = "N";
                romstrdb.IsCompleted = "N";
                romstrdb.AtcID = GetToAtcID(rmmstr.ToSkuDet_PK);
                romstrdb.Location_PK = rmmstr.Location_Pk;

                enty.RequestOrderMasters.Add(romstrdb);

                enty.SaveChanges();
                romstrdb.RONum = "RO" + romstrdb.RO_Pk.ToString();


                ProcurementMaster POmstr = new ProcurementMaster();
                POmstr.Supplier_Pk = 1113;//inventory bureuo
                POmstr.DeliveryTerms_Pk = 1;
                POmstr.PaymentTermID = 3;
                POmstr.DeliveryMethod_Pk = 1;
                POmstr.CurrencyID = 18;
                POmstr.Location_PK = int.Parse(HttpContext.Current.Session["UserLoc_pk"].ToString().Trim());
                POmstr.AtcId = romstrdb.AtcID;
                POmstr.AddedBy = romstrdb.AddedBy;
                POmstr.AddedDate = DateTime.Now;
                POmstr.IsApproved = "Y";
                POmstr.IsDeleted = "N";
                POmstr.POType = "T";
                POmstr.DeliveryDate = DateTime.Now;
                POmstr.Remark = "Po against RO";
                POmstr.PONum = romstrdb.RONum;
                POmstr.IsNormal = "N";
                enty.ProcurementMasters.Add(POmstr);




                enty.SaveChanges();



                romstrdb.PO_PK = POmstr.PO_Pk;



                foreach (RoDetailsData rdet in rmmstr.RoDetailsDataCollection)
                {
                    RequestOrderDetail rodetdb = new RequestOrderDetail();

                    rodetdb.RO_Pk = romstrdb.RO_Pk;
                    rodetdb.ToSkuDet_PK = rdet.ToSkuDet_PK;
                    rodetdb.FromSkuDet_PK = rdet.FromSkuDet_PK;
                    rodetdb.Qty = rdet.Qty;
                    rodetdb.InventoryItem_PK = rdet.InventoryItem_PK;
                    rodetdb.CUnitPrice = float.Parse(rdet.UnitPrice.ToString());

                    enty.RequestOrderDetails.Add(rodetdb);



                    ProcurementDetail pddetails = new ProcurementDetail();
                    pddetails.PO_Pk = POmstr.PO_Pk;
                    pddetails.SkuDet_PK = rdet.ToSkuDet_PK;
                    pddetails.POQty = rdet.Qty;
                    pddetails.POUnitRate = Convert.ToDecimal(rdet.UnitPrice);
                    pddetails.SupplierColor = "";
                    pddetails.SupplierSize = "";
                    pddetails.PO_Pk = POmstr.PO_Pk;
                    pddetails.Uom_PK = GetuomPK(rdet.InventoryItem_PK);
                    pddetails.CURate = Convert.ToDecimal(rdet.UnitPrice);
                    enty.ProcurementDetails.Add(pddetails);




                }

                enty.SaveChanges();
                RONUM = romstrdb.RONum;
            }
            return RONUM;
        }

        /// <summary>
        /// Insert Stock RO
        /// </summary>
        /// <param name="rmmstr"></param>
        /// <returns></returns>
        public String insertStockRowmaterial(RequestOrderMasterData rmmstr)
        {
            string RONUM = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                RequestOrderStockMaster romstrdb = new RequestOrderStockMaster();


                romstrdb.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                romstrdb.CreatedDate = DateTime.Now;
                romstrdb.IsApproved = "N";
                romstrdb.IsDeleted = "N";
                romstrdb.Iscompleted = "N";
                romstrdb.AtcID = GetToAtcID(rmmstr.ToSkuDet_PK);


                enty.RequestOrderStockMasters.Add(romstrdb);

                enty.SaveChanges();
                romstrdb.RONum = "SRO" + romstrdb.SRO_Pk.ToString();
                
                ProcurementMaster POmstr = new ProcurementMaster();
                POmstr.Supplier_Pk = 1113;//inventory bureuo
                POmstr.DeliveryTerms_Pk = 1;
                POmstr.PaymentTermID = 3;
                POmstr.DeliveryMethod_Pk = 1;
                POmstr.CurrencyID = 18;
                POmstr.Location_PK = int.Parse(HttpContext.Current.Session["UserLoc_pk"].ToString().Trim());
                POmstr.AtcId = romstrdb.AtcID;
                POmstr.AddedBy = romstrdb.AddedBy;
                POmstr.AddedDate = DateTime.Now;
                POmstr.IsApproved = "Y";
                POmstr.IsDeleted = "N";
                POmstr.POType = "T";
                POmstr.IsNormal = "N";
                POmstr.DeliveryDate = DateTime.Now;
                POmstr.Remark = "Po against Stock RO";
                POmstr.PONum = romstrdb.RONum;
                enty.ProcurementMasters.Add(POmstr);



                enty.SaveChanges();


                romstrdb.PO_PK = POmstr.PO_Pk;

                

                foreach (RoDetailsData rdet in rmmstr.RoDetailsDataCollection)
                {
                    RequestOrderStockDetail rodetdb = new RequestOrderStockDetail();

                    rodetdb.SRO_Pk = romstrdb.SRO_Pk;
                    rodetdb.ToSkuDet_PK = rdet.ToSkuDet_PK;
                 
                    rodetdb.Qty = rdet.Qty;
                    rodetdb.SInventoryItem_PK = rdet.InventoryItem_PK;
                    rodetdb.CUnitPrice = decimal.Parse(rdet.UnitPrice.ToString());

                    enty.RequestOrderStockDetails.Add(rodetdb);







                    ProcurementDetail pddetails = new ProcurementDetail();
                    pddetails.PO_Pk = POmstr.PO_Pk;
                    pddetails.SkuDet_PK = rdet.ToSkuDet_PK;
                    pddetails.POQty = rdet.Qty;
                    pddetails.POUnitRate = Convert.ToDecimal(rdet.UnitPrice);
                    pddetails.SupplierColor = "";
                    pddetails.SupplierSize = "";
                    pddetails.PO_Pk = POmstr.PO_Pk;
                    pddetails.Uom_PK = GetsTOCKuomPK(rdet.InventoryItem_PK);
                    pddetails.CURate = Convert.ToDecimal(rdet.UnitPrice);
                    enty.ProcurementDetails.Add(pddetails);






                }

                enty.SaveChanges();
                RONUM = romstrdb.RONum;
            }
            return RONUM;
        }









        public void GetsTOCKROApproved(int ro_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                string RONUM = "";
                var q = from rmmstr in enty.RequestOrderStockMasters
                        where rmmstr.SRO_Pk == ro_pk
                        select rmmstr;

                foreach (var element in q)
                {
                    element.IsApproved = "Y";
                    element.ApprovedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.Approveddate = DateTime.Now;
                    RONUM = element.RONum;
                }

                var q1 = from rmmstr1 in enty.ProcurementMasters
                        where rmmstr1.PONum == RONUM
                        select rmmstr1;
                foreach (var element1 in q1)
                {
                    element1.IsApproved = "Y";
                    element1.ApprovedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    element1.Approveddate = DateTime.Now;
                 
                }


                enty.SaveChanges();

            }
        }



        public void ForwardsTOCKROforApproval(int ro_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from rmmstr in enty.RequestOrderStockMasters
                        where rmmstr.SRO_Pk == ro_pk
                        select rmmstr;

                foreach (var element in q)
                {
                    element.IsForwarded = "Y";
                    element.ForwardedBY = HttpContext.Current.Session["Username"].ToString().Trim();

                }

                enty.SaveChanges();

            }
        }











        public void GetROApproved(int ro_pk)
        {
            string RONUM = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from rmmstr in enty.RequestOrderMasters
                        where rmmstr.RO_Pk == ro_pk
                        select rmmstr;

                foreach (var element in q)
                {
                    element.IsApproved = "Y";
                    element.ApprovedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.Approveddate = DateTime.Now;

                    RONUM = element.RONum;
                  


                }

                var q1 = from rmmstr1 in enty.ProcurementMasters
                         where rmmstr1.PONum == RONUM
                         select rmmstr1;
                foreach (var element1 in q1)
                {
                    element1.IsApproved = "Y";
                    element1.ApprovedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    element1.Approveddate = DateTime.Now;

                }

                enty.SaveChanges();

            }
        }



        public void ForwardROforApproval(int ro_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from rmmstr in enty.RequestOrderMasters
                        where rmmstr.RO_Pk == ro_pk
                        select rmmstr;

                foreach (var element in q)
                {
                    element.IsForwarded = "Y";
                    element.ForwardedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                   
                }

                enty.SaveChanges();

            }
        }



        public void GetRODeleted(int ro_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from rmmstr in enty.RequestOrderMasters
                        where rmmstr.RO_Pk == ro_pk
                        select rmmstr;

                foreach (var element in q)
                {
                    element.IsDeleted = "Y";
                    element.IsApproved = "D";
                    element.DeletedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.DeletedDate = DateTime.Now;
                }

                enty.SaveChanges();

            }
        }

    }


    public class RoDetailsData : RequestOrderMaster
    {

        public int RODet_Pk { get; set; }
        public int RO_Pk { get; set; }
        public int FromSkuDet_PK { get; set; }
        public int ToSkuDet_PK { get; set; }
        public int InventoryItem_PK { get; set; }
        public Decimal Qty { get; set; }
        public Decimal UnitPrice { get; set; }

        public DataTable Rodetails;

        public DataTable Rodetails1
        {
            get
            {
                DBTransaction.ProcurementTransaction pktrans = new DBTransaction.ProcurementTransaction();
                return pktrans.GetRoDetails(RO_Pk);
            }
            set { Rodetails = value; }
        }


        public DataTable StocRodetails;

        public DataTable StocRodetails1
        {
            get
            {
                DBTransaction.ProcurementTransaction pktrans = new DBTransaction.ProcurementTransaction();
                return pktrans.GetStockRoDetails(RO_Pk);
            }
            set { Rodetails = value; }
        }




        public int GetsTOCKuomPK(int sinventotyitem_pk)
        {
            int Uom_PK = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from skudet in enty.StockInventoryMasters
                         where skudet.SInventoryItem_PK == sinventotyitem_pk
                         select skudet.Uom_PK).FirstOrDefault();

                Uom_PK = int.Parse(q.ToString());
            }

            return Uom_PK;
        }


        public int GetuomPK(int inventotyitem_pk)
        {
            int Uom_PK = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from skudet in enty.InventoryMasters
                         where skudet.InventoryItem_PK == inventotyitem_pk
                         select skudet.Uom_Pk).FirstOrDefault();

                Uom_PK = int.Parse(q.ToString());
            }

            return Uom_PK;
        }


        public int GetPODet_PK(int PO_Pk)
        {
            int PODet_PK = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from skudet in enty.ProcurementDetails
                         where skudet.PO_Pk == PO_Pk
                         select skudet.PODet_PK).FirstOrDefault();

                PODet_PK = int.Parse(q.ToString());
            }

            return PODet_PK;
        }

        public Decimal GetCURate(int POdet_Pk)
        {
            Decimal curate = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from skudet in enty.ProcurementDetails
                         where skudet.PODet_PK == POdet_Pk
                         select skudet.CURate).FirstOrDefault();

                curate = Decimal.Parse(q.ToString());
            }

            return curate;
        }
    }







    public class RoStockDetailsData : RequestOrderMaster
    {

        public int RODet_Pk { get; set; }
        public int RO_Pk { get; set; }
      
        public int ToSkuDet_PK { get; set; }
        public int SInventoryItem_PK { get; set; }
        public Decimal Qty { get; set; }
        public Decimal UnitPrice { get; set; }

       



        // feilds added for stockRO



    }





    public class WrongPOData
    {

        public int WrongPO_Pk { get; set; }
        public int PO_PK { get; set; }
        public string AddedBY { get; set; }
        public string isapproved
        {
            get; set;
        }
        public string L1ApprovedBY { get; set; }
       
        public string ApprovedBY { get; set; }
       
        public string MerchandiserName { get; set; }
        public string Explanation { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime L1ApprovedDate { get; set; }
        public DateTime ApprovedDate { get; set; }
    }

    public class WrongPODetailData
    {
        public int WrongPO_Pk { get; set; }
        public int podet_pk { get; set; }
        public int skudet_pk { get; set; }
        public decimal qty { get; set; }


    }



    public class WrongPOActions
    {

        public WrongPOData wrngdata { get; set; }
         
        public List<WrongPODetailData> WrongDetailsDataCollection { get; set; }



        public String insertWrongPOdata()
        {
            string num = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                WrongPOMaster wrngpomstr = new DataModels.WrongPOMaster();
                wrngpomstr.PO_PK = wrngdata.PO_PK;
                wrngpomstr.AddedBY = wrngdata.AddedBY;
                wrngpomstr.L1ApprovedBY = wrngdata.L1ApprovedBY;
                wrngpomstr.ApprovedBY = wrngdata.ApprovedBY;
                wrngpomstr.MerchandiserName = wrngdata.MerchandiserName;
                wrngpomstr.Explanation = wrngdata.Explanation;
                wrngpomstr.AddedDate = wrngdata.AddedDate;
                wrngpomstr.IsApproved = "N";
                wrngpomstr.Isforwarded = "N";

                enty.WrongPOMasters.Add(wrngpomstr);
                enty.SaveChanges();

                num= wrngpomstr.Reqnum = "WPO" + wrngpomstr.WrongPO_Pk.ToString().PadLeft(6, '0');
                foreach (WrongPODetailData rdet in this.WrongDetailsDataCollection)
                {
                    WrongPODetail wrngdet = new DataModels.WrongPODetail();
                    wrngdet.Podet_PK = rdet.podet_pk;
                    wrngdet.Skudet_pk = rdet.skudet_pk;
                    wrngdet.Qty = rdet.qty;
                    wrngdet.WrongPO_Pk = wrngpomstr.WrongPO_Pk;

                    enty.WrongPODetails.Add(wrngdet);



                }
                enty.SaveChanges();
                }

            return num;
            }








        public void GetWrongPOApproved(int wro_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from rmmstr in enty.WrongPOMasters
                        where rmmstr.WrongPO_Pk== wro_pk
                        select rmmstr;

                foreach (var element in q)
                {
                    element.IsApproved = "Y";
                    element.ApprovedBY = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.ApprovedDate = DateTime.Now;
                }

                enty.SaveChanges();

            }
        }



        public void ForwardWrongPOApproval(int wro_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from rmmstr in enty.WrongPOMasters
                        where rmmstr.WrongPO_Pk == wro_pk
                        select rmmstr;

                foreach (var element in q)
                {
                    element.Isforwarded = "Y";
                    element.ForwardedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.L1ApprovedBY = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.L1ApprovedDate = DateTime.Now;
                }

                enty.SaveChanges();

            }
        }










    }






    public class ExtraBOMMasterData
    {
        public int Atcid { get; set; }
        public int ExtraBOM_PK { get; set; }
        public string AddedBY { get; set; }
        public DateTime AddedDate { get; set; }
        public string ApprovedBY { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string MerchandiserName { get; set; }
        public string Explanation { get; set; }
        public string IsApproved { get; set; }
        public string Reqnum { get; set; }
        public string Isforwarded { get; set; }
        public string ForwardedBy { get; set; }
    }

    public class ExtraBOMDetailData
    {
        public int ExtraBOMDetail_PK { get; set; }
        public int Skudet_PK { get; set; }
        public Decimal Qty { get; set; }



    }

    public class ExtraBOMActions
    {

        public ExtraBOMMasterData extrabommasterdata { get; set; }

        public List<ExtraBOMDetailData> ExtraBOMDetailDataCollection { get; set; }



        public String insertEBOMdata()
        {
            string num = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                ExtraBOMRequestMaster extramstr = new ExtraBOMRequestMaster();
                extramstr.AddedBY = extrabommasterdata.AddedBY;

                extramstr.ApprovedBY = extrabommasterdata.ApprovedBY;
                extramstr.MerchandiserName = extrabommasterdata.MerchandiserName;
                extramstr.Explanation = extrabommasterdata.Explanation;
                extramstr.AddedDate = extrabommasterdata.AddedDate;
                extramstr.IsApproved= "N";
                extramstr.AtcId = extrabommasterdata.Atcid;
                extramstr.Isforwarded = "N";

                enty.ExtraBOMRequestMasters.Add(extramstr);
                enty.SaveChanges();

                num = extramstr.Reqnum = "EBM" + extramstr.ExtraBOM_PK.ToString().PadLeft(6, '0');
                foreach (ExtraBOMDetailData rdet in this.ExtraBOMDetailDataCollection)
                {
                    ExtraBOMRequestDetail wrngdet = new ExtraBOMRequestDetail();
                    wrngdet.Skudet_PK = rdet.Skudet_PK;
                    wrngdet.Qty = rdet.Qty;
                    wrngdet.ExtraBOM_PK = extramstr.ExtraBOM_PK;
                    

                    enty.ExtraBOMRequestDetails.Add(wrngdet);



                }
                enty.SaveChanges();
            }

            return num;
        }








        public void GetEBOMApproved(int wro_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from rmmstr in enty.ExtraBOMRequestMasters
                        where rmmstr.ExtraBOM_PK == wro_pk
                        select rmmstr;

                foreach (var element in q)
                {
                    element.IsApproved = "Y";
                    element.ApprovedBY = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.ApprovedDate = DateTime.Now;
                }

                enty.SaveChanges();

            }
        }



        public void ForwardEBOMApproval(int wro_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from rmmstr in enty.ExtraBOMRequestMasters
                        where rmmstr.ExtraBOM_PK == wro_pk
                        select rmmstr;

                foreach (var element in q)
                {
                    element.Isforwarded = "Y";
                    element.ForwardedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                 
                }

                enty.SaveChanges();

            }
        }










    }











}