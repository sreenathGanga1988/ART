﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ArtWebApp.DBTransaction;
using ArtWebApp.DataModels;
using System.Data.SqlClient;
using System.Data.Entity.Validation;

namespace ArtWebApp.BLL.CutOrderBLL
{
    public class CutOrderData
    {
        DBTransaction.CutOrderTransaction cuttrans = null;
        public CutOrderData ()
        {

        }
        public List<CutDetailsData> CutDetailsDataCollection { get; set; }
        public int Atcid { get; set; }
        public int Ourstyleid { get; set; }
        public String FabDescription { get; set; }
        public int CuitId { get; set; }
        public int Skudet_pk { get; set; }
        public String CutNum { get; set; }
        public String patername { get; set; }
        public String MarkerType { get; set; }

        public int Tofactid { get; set; }
        public Decimal Cutqty { get; set; }
        public String  CutorderType { get; set; }
        public Decimal CutOrderQty { get; set; }
        public String Cutablewidth { get; set; }

        public String Shrinkage { get; set; }
        public Decimal CofabAllocation { get; set; }
        public Decimal BalToCutQty { get; set; }
        public Decimal DeliveredQty { get; set; }
        public Decimal ApprovedConsumption { get; set; }

        public Decimal ExtraReason_Pk { get; set; }


        public int AddedBy { get; set; }
        public int AddedDate { get; set; }





        public Boolean IsCutOrdernumPresent(String Cutnum)
        {
            Boolean ispresent = false;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                if (!enty.CutOrderMasters.Any(f => f.Cut_NO.Trim() == Cutnum.Trim()))
                {
                    ispresent = false;
                }
                else
                {
                    ispresent = true;
                }
            }
            return ispresent;
        }



        public Boolean IsCutOrderDOMade(int cutid)
        {
            Boolean ispresent = false;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                if (!enty.CutOrderDOes.Any(f => f.CutID.ToString().Trim () == cutid.ToString ().Trim()))
                {

                    ispresent = false;
                }
                else
                {
                  

                    Decimal deliveryqty = 0;
                    var alreadyallocated = enty.CutOrderDOes.Where(U => U.CutID == cutid).Sum(U => U.DeliveryQty);
                    if (alreadyallocated != null)
                    {
                        deliveryqty = Decimal.Parse(alreadyallocated.ToString());
                    }
                    else
                    {
                        deliveryqty = 0;
                    };


                    if (deliveryqty <= 0)
                    {
                        ispresent = false;
                    }
                    else
                    {
                        ispresent = true;
                    }
                }
            }
            return ispresent;
        }





        public String InsertCutOrder(CutOrderData cdata)
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                CutOrderMaster ctmstr = new CutOrderMaster();
                ctmstr.AtcID = cdata.Atcid;
                ctmstr.OurStyleID = cdata.Ourstyleid;
                ctmstr.Color = cdata.FabDescription;
                ctmstr.Cut_NO = cdata.CutNum;
                ctmstr.ToLoc = cdata.Tofactid;
                ctmstr.CutOrderType = cdata.CutorderType;
                ctmstr.CutQty = cdata.CutOrderQty;
                ctmstr.FabQty = cdata.CofabAllocation;
                ctmstr.CutWidth = cdata.Cutablewidth;
                ctmstr.Shrinkage = cdata.Shrinkage;
                ctmstr.BalanceQty = cdata.BalToCutQty;
                ctmstr.DelivedQty = cdata.DeliveredQty;
                ctmstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
                ctmstr.CutOrderDate = DateTime.Now;
                ctmstr.SkuDet_pk = cdata.Skudet_pk;
                ctmstr.ExtraReason_Pk = cdata.ExtraReason_Pk;
                ctmstr.ConsumptionQty = cdata.ApprovedConsumption;
                ctmstr.IsDeleted = "N";
                enty.CutOrderMasters.Add(ctmstr);

                enty.SaveChanges();
                Cutn = ctmstr.Cut_NO;
            }

            return Cutn;

        }





        public String InsertNewCutOrder()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                CutOrderMaster ctmstr = new CutOrderMaster();
                ctmstr.AtcID = this.Atcid;
                ctmstr.OurStyleID = this.Ourstyleid;
                ctmstr.Color = this.FabDescription;
                ctmstr.Cut_NO = this.CutNum;
                ctmstr.ToLoc = this.Tofactid;
                ctmstr.CutOrderType = this.CutorderType;
                ctmstr.CutQty = this.CutOrderQty;
                ctmstr.FabQty = this.CofabAllocation;
                ctmstr.CutWidth = this.Cutablewidth;
                ctmstr.Shrinkage = this.Shrinkage;
                ctmstr.BalanceQty = this.BalToCutQty;
                ctmstr.DelivedQty = this.DeliveredQty;
                ctmstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
                ctmstr.CutOrderDate = DateTime.Now;
                ctmstr.SkuDet_pk = this.Skudet_pk;
                ctmstr.ExtraReason_Pk = this.ExtraReason_Pk;
                ctmstr.ConsumptionQty = this.ApprovedConsumption;
                ctmstr.MarkerType = this.MarkerType;
                ctmstr.IsDeleted = "N";
                ctmstr.ActualConsumption = this.ApprovedConsumption;
                enty.CutOrderMasters.Add(ctmstr);
                enty.SaveChanges();

                HttpContext.Current.Session["cutid"] = ctmstr.CutID;
               



                foreach (CutDetailsData di in this.CutDetailsDataCollection)
                {
                    CutOrderDetail cddetail = new CutOrderDetail();
                    cddetail.MarkerNo = di.MarkerNo;
                    cddetail.NoOfPc = di.NoOfPc;
                    cddetail.Qty = di.Qty;
                    cddetail.CutID = ctmstr.CutID;
                    enty.CutOrderDetails.Add(cddetail);
                }





                enty.SaveChanges();


                Cutn = ctmstr.Cut_NO;
            }

            return Cutn;

        }







        public void DeleteCutOrder(int  cutid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from ponmbr in enty.CutOrderMasters
                        where ponmbr.CutID == cutid
                        select ponmbr;


                foreach (var element in q)
                {

                    element.IsDeleted = "Y";
                }
                enty.SaveChanges();
            }
        }

        public static DataTable CreateRollRows(int numofrows)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("MarkerNum", typeof(String));
            dt.Columns.Add("NoofPC", typeof(String));
            dt.Columns.Add("Qty", typeof(String));
            dt.Columns.Add("Markerlength", typeof(String));
            dt.Columns.Add("layLength", typeof(String));


            for (int i = 1; i <= numofrows; i++)
            {


                dt.Rows.Add( i.ToString(), "0", "0", "0", "0");

            }

            return dt;


        }

        public String UpdateCutorder(CutOrderData cdata)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from ponmbr in enty.CutOrderMasters
                        where ponmbr.CutID == cdata.CuitId
                        select ponmbr;


                foreach (var element in q)
                {

                    element.AtcID = cdata.Atcid;
                    element.OurStyleID = cdata.Ourstyleid;
                    element.Color = cdata.FabDescription;                  
                    element.ToLoc = cdata.Tofactid;
                    element.CutOrderType = cdata.CutorderType;
                    element.CutQty = cdata.CutOrderQty;
                    element.FabQty = cdata.CofabAllocation;
                    element.CutWidth = cdata.Cutablewidth;
                    element.Shrinkage = cdata.Shrinkage;
                    element.BalanceQty = cdata.BalToCutQty;
                    element.DelivedQty = cdata.DeliveredQty;
                    element.AddedBy = HttpContext.Current.Session["Username"].ToString();
                    element.CutOrderDate = DateTime.Now;
                    element.SkuDet_pk = cdata.Skudet_pk;
                    element.ExtraReason_Pk = cdata.ExtraReason_Pk;
                    element.ConsumptionQty = cdata.ApprovedConsumption;
                    element.ActualConsumption = cdata.ApprovedConsumption; 

                }

                enty.SaveChanges();
            }

            return cdata.CutNum;
        }




        public DataTable GetFabricDescription(int atcid)
        {
            DataTable dt = new DataTable();
            cuttrans=new CutOrderTransaction ();
            dt = cuttrans.GetGarmentDescription(atcid);


            return dt;

        }

        public DataTable GetTrimDescription(int atcid)
        {
            DataTable dt = new DataTable();
            cuttrans = new CutOrderTransaction();
            dt = cuttrans.GetTrimsDescription(atcid);


            return dt;

        }



        public DataTable GetFabricShrinkage(int skudetPk)
        {
            DataTable dt = new DataTable();
            cuttrans = new CutOrderTransaction();
            dt = cuttrans.GetFabricShrinkage(skudetPk);


            return dt;

        }
        public DataTable GetFabricWidth(int skudetPk)
        {
            DataTable dt = new DataTable();
            cuttrans = new CutOrderTransaction();
            dt = cuttrans.GetFabricWidth(skudetPk);


            return dt;

        }

        public DataTable GetFabricMarkertype(int skudetPk)
        {
            DataTable dt = new DataTable();
            cuttrans = new CutOrderTransaction();
            dt = cuttrans.GetFabricMarkerType(skudetPk);


            return dt;

        }



        public DataTable GetFabricShrinkageLocation(int skudetPk,int locationpk)
        {
            DataTable dt = new DataTable();
            cuttrans = new CutOrderTransaction();
            dt = cuttrans.GetFabricShrinkageLocation(skudetPk, locationpk);


            return dt;

        }
        public DataTable GetFabricWidthLocation(int skudetPk, int locationpk)
        {
            DataTable dt = new DataTable();
            cuttrans = new CutOrderTransaction();
            dt = cuttrans.GetFabricWidthLocation(skudetPk, locationpk);


            return dt;

        }

        public DataTable GetFabricMarkertypeLocation(int skudetPk, int locationpk)
        {
            DataTable dt = new DataTable();
            cuttrans = new CutOrderTransaction();
            dt = cuttrans.GetFabricMarkerTypeLocation(skudetPk, locationpk);


            return dt;

        }










        public DataTable GetCutOrderData(int iipk,int toloc)
        {
            DataTable dt = new DataTable();
            cuttrans = new CutOrderTransaction();
            dt = cuttrans.GetCutOrder(iipk, toloc);
           

            return dt;

        }

        public decimal GetAllowedQty(int inv_pk)
        {

            decimal allowedqty = 0;
            int skudet = 0;
            DBTransaction.BOMTransaction bomtrans = new DBTransaction.BOMTransaction();
            using (ArtEntitiesnew enty =new ArtEntitiesnew() )
            {
                var q = from inv in enty.InventoryMasters where inv.InventoryItem_PK == inv_pk select inv;
                foreach(var element in q)
                {
                    skudet = int.Parse(element.SkuDet_Pk.ToString());
                }
            }

            DataTable BomData = bomtrans.GetSkudetBOM(skudet);
            if (BomData.Rows.Count <= 0)
            {

            }
            else
            {
                foreach (System.Data.DataColumn col in BomData.Columns) col.ReadOnly = false;
                allowedqty = CalculateRequiredFORCTI(BomData, skudet);
            }

            return allowedqty;
        }

        public decimal CalculateRequiredFORCTI(DataTable dt, int skudet)
        {
            DataTable skudata = GetSKUDataforCTI(skudet);
            DataView ourstyleview = new DataView(skudata);
            DataTable distinctOurstyleData = ourstyleview.ToTable(true, "OurStyleID");
            decimal requiredqty = 0;
            DBTransaction.BOMTransaction bomtrans = new DBTransaction.BOMTransaction();
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                int skudetpk = int.Parse(dt.Rows[i]["SkuDet_PK"].ToString().Trim());

                if (skudetpk == 57591)
                {
                    int k = 9;
                }
                int uom_pk = int.Parse(dt.Rows[i]["uom_pk"].ToString().Trim());
                String isCD = dt.Rows[i]["IsCD"].ToString().Trim();
                String isSD = dt.Rows[i]["IsSD"].ToString().Trim();
                String isCM = dt.Rows[i]["isCommon"].ToString().Trim();
                String IsGD = dt.Rows[i]["IsGD"].ToString().Trim();


                requiredqty = (int)Math.Round(requiredQtyCalculate(skudetpk, isCD, isSD, isCM, IsGD, skudata, distinctOurstyleData), 0);


            }
            return requiredqty;
        }


        public DataTable GetSKUDataforCTI(int skudet)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @" SELECT        SkuRawMaterialMaster.WastagePercentage, SkuRawMaterialMaster.Sku_Pk, SkuRawmaterialDetail.SkuDet_PK, StyleCostingDetails.Consumption, StyleCostingMaster.OurStyleID, POPackDetails.ColorCode, 
                         POPackDetails.SIzeCode, POPackDetails.PoQty, StyleCostingMaster.IsApproved, SkuRawMaterialMaster.Atc_id, StyleCostingDetails.IsRequired,SkuRawmaterialDetail.ColorCode AS SKUColorCode, 
                         SkuRawmaterialDetail.SizeCode AS SKUSizeCode
FROM            SkuRawMaterialMaster INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID INNER JOIN
                         SkuRawmaterialDetail ON StyleCostingDetails.Sku_PK = SkuRawmaterialDetail.Sku_PK
WHERE        (StyleCostingMaster.IsApproved = N'A') AND (SkuRawmaterialDetail.SkuDet_PK = @skudet) AND (StyleCostingDetails.IsRequired = N'Y')";



                cmd.Parameters.AddWithValue("@skudet", skudet);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }


        public decimal  requiredQtyCalculate(int skudetpk, String isCD, String isSD, String isCM, String IsGD, DataTable skudata, DataTable ourstyles)
        {
            decimal  requiredqty = 0;

            decimal garqty = 0;


            if (isCM == "Y" && isCD == "N" && isSD == "N" && IsGD == "N")
            {
                try
                {


                    for (int i = 0; i < ourstyles.Rows.Count; i++)
                    {
                        try
                        {
                            DataTable newresult = skudata.Select("SkuDet_PK = " + skudetpk + " and OurStyleID=" + int.Parse(ourstyles.Rows[i]["OurStyleID"].ToString()) + "").CopyToDataTable();
                            decimal ourstylereq = 0;
                            var ourstylesum = newresult.Compute("SUM(PoQty)", "");
                            var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
                            var wastage = newresult.Compute("MAX(WastagePercentage)", "");

                            ourstylereq = decimal.Parse((decimal.Parse(ourstylesum.ToString()) * decimal.Parse(CONSUMPTION.ToString())).ToString());

                            decimal wastageqty = ourstylereq * (decimal.Parse(wastage.ToString()) / 100);
                            ourstylereq = ourstylereq + wastageqty;
                            requiredqty = requiredqty + ourstylereq;
                            garqty = garqty + (decimal.Parse(ourstylesum.ToString()));
                        }
                        catch (Exception)
                        {


                        }
                    }


                }
                catch (Exception)
                {


                }
            }
            else if (isCM == "N" && isCD == "Y" && isSD == "N" && IsGD == "N")
            {

                try
                {

                    for (int i = 0; i < ourstyles.Rows.Count; i++)
                    {
                        try
                        {
                            DataTable newresult = skudata.Select("SkuDet_PK = " + skudetpk + " and OurStyleID=" + int.Parse(ourstyles.Rows[i]["OurStyleID"].ToString()) + "  and ColorCode =SKUColorCode").CopyToDataTable();
                            decimal ourstylereq = 0;
                            var ourstylesum = newresult.Compute("SUM(PoQty)", "");
                            var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
                            var wastage = newresult.Compute("MAX(WastagePercentage)", "");

                            ourstylereq = decimal.Parse((decimal.Parse(ourstylesum.ToString()) * decimal.Parse(CONSUMPTION.ToString())).ToString());

                            decimal wastageqty = ourstylereq * (decimal.Parse(wastage.ToString()) / 100);
                            ourstylereq = ourstylereq + wastageqty;
                            requiredqty = requiredqty + ourstylereq;
                            garqty = garqty + (decimal.Parse(ourstylesum.ToString()));
                        }
                        catch (Exception)
                        {


                        }
                    }

                }
                catch (Exception)
                {


                }

            }
            else if (isCM == "N" && isCD == "N" && isSD == "Y" && IsGD == "N")
            {
                try
                {



                    for (int i = 0; i < ourstyles.Rows.Count; i++)
                    {
                        try
                        {
                            DataTable newresult = skudata.Select("SkuDet_PK = " + skudetpk + " and OurStyleID=" + int.Parse(ourstyles.Rows[i]["OurStyleID"].ToString()) + "  and SIzeCode =SKUSizeCode").CopyToDataTable();
                            decimal ourstylereq = 0;
                            var ourstylesum = newresult.Compute("SUM(PoQty)", "");
                            var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
                            var wastage = newresult.Compute("MAX(WastagePercentage)", "");

                            ourstylereq = decimal.Parse((decimal.Parse(ourstylesum.ToString()) * decimal.Parse(CONSUMPTION.ToString())).ToString());

                            decimal wastageqty = ourstylereq * (decimal.Parse(wastage.ToString()) / 100);
                            ourstylereq = ourstylereq + wastageqty;
                            requiredqty = requiredqty + ourstylereq;
                            garqty = garqty + (decimal.Parse(ourstylesum.ToString()));
                        }
                        catch (Exception)
                        {


                        }
                    }


                }
                catch (Exception)
                {


                }
            }
            else if (isCM == "N" && isCD == "Y" && isSD == "Y" && IsGD == "N")
            {
                try
                {


                    for (int i = 0; i < ourstyles.Rows.Count; i++)
                    {
                        try
                        {
                            DataTable newresult = skudata.Select("SkuDet_PK = " + skudetpk + " and OurStyleID=" + int.Parse(ourstyles.Rows[i]["OurStyleID"].ToString()) + "  and SIzeCode =SKUSizeCode and ColorCode =SKUColorCode").CopyToDataTable();
                            decimal ourstylereq = 0;
                            var ourstylesum = newresult.Compute("SUM(PoQty)", "");
                            var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
                            var wastage = newresult.Compute("MAX(WastagePercentage)", "");

                            ourstylereq = decimal.Parse((decimal.Parse(ourstylesum.ToString()) * decimal.Parse(CONSUMPTION.ToString())).ToString());

                            decimal wastageqty = ourstylereq * (decimal.Parse(wastage.ToString()) / 100);
                            ourstylereq = ourstylereq + wastageqty;
                            requiredqty = requiredqty + ourstylereq;
                            garqty = garqty + (decimal.Parse(ourstylesum.ToString()));
                        }
                        catch (Exception)
                        {


                        }
                    }


                }
                catch (Exception exp)
                {


                }
            }
            else if (IsGD == "Y")
            {
                // if ggroup dependa

                try
                {
                    if (isCM == "Y" && isCD == "N" && isSD == "N" && IsGD == "Y")
                    {
                        requiredqty = GroupDependantCommonQty(skudetpk);
                    }
                    else if (isCM == "N" && isCD == "Y" && isSD == "N" && IsGD == "Y")
                    {
                        requiredqty = GroupDependantColorQty(skudetpk);

                    }
                    else if (isCM == "N" && isCD == "N" && isSD == "Y" && IsGD == "Y")
                    {
                        requiredqty = GroupDependantSizeQty(skudetpk);

                    }
                    else if (isCM == "N" && isCD == "Y" && isSD == "Y" && IsGD == "Y")
                    {
                        requiredqty = GroupDependantSizeandColorQty(skudetpk);

                    }
                }
                catch (Exception)
                {


                }
            }

            return requiredqty;
        }

        public decimal GroupDependantCommonQty(int skudetpk)
        {
            decimal requiredqty = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ISNULL(MAX(StyleCostingDetails.Consumption), 0) AS Consumption, ISNULL(SUM(POPackDetails.PoQty), 0) AS PoQty, ISNULL(AVG(SkuRawMaterialMaster.WastagePercentage), 0) AS WastagePercentage
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         GroupDependantItems ON SkuRawMaterialMaster.Sku_Pk = GroupDependantItems.Sku_PK INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID AND GroupDependantItems.POPackID = POPackDetails.POPackId AND GroupDependantItems.OurStyleID = POPackDetails.OurStyleID
WHERE        (StyleCostingMaster.IsApproved = N'A') and (GroupDependantItems.IsDepenant='Y')
GROUP BY SkuRawmaterialDetail.SkuDet_PK
HAVING        (SkuRawmaterialDetail.SkuDet_PK = @Param1)";


            cmd.Parameters.AddWithValue("@param1", skudetpk);



            DataTable dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
            try
            {

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        var sum = dt.Rows[0]["PoQty"];
                        var CONSUMPTION = dt.Rows[0]["Consumption"];
                        var wastage = dt.Rows[0]["WastagePercentage"];
                        requiredqty = decimal.Parse((decimal.Parse(sum.ToString()) * decimal.Parse(CONSUMPTION.ToString())).ToString());

                        decimal wastageqty = requiredqty * (decimal.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;

                    }
                }
            }
            catch (Exception)
            {


            }

            return requiredqty;
        }




       
        public decimal GroupDependantColorQty(int skudetpk)
        {
            decimal requiredqty = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ISNULL(MAX(StyleCostingDetails.Consumption), 0) AS Consumption, ISNULL(SUM(POPackDetails.PoQty), 0) AS PoQty, ISNULL(AVG(SkuRawMaterialMaster.WastagePercentage), 0) AS WastagePercentage
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         GroupDependantItems ON SkuRawMaterialMaster.Sku_Pk = GroupDependantItems.Sku_PK INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID AND GroupDependantItems.POPackID = POPackDetails.POPackId AND 
                         SkuRawmaterialDetail.ColorCode = POPackDetails.ColorCode AND GroupDependantItems.OurStyleID = POPackDetails.OurStyleID
WHERE        (StyleCostingMaster.IsApproved = N'A') and (GroupDependantItems.IsDepenant='Y')
GROUP BY SkuRawmaterialDetail.SkuDet_PK
HAVING        (SkuRawmaterialDetail.SkuDet_PK = @Param1)";


            cmd.Parameters.AddWithValue("@param1", skudetpk);



            DataTable dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
            try
            {

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        var sum = dt.Rows[0]["PoQty"];
                        var CONSUMPTION = dt.Rows[0]["Consumption"];
                        var wastage = dt.Rows[0]["WastagePercentage"];
                        requiredqty = decimal.Parse((decimal.Parse(sum.ToString()) * decimal.Parse(CONSUMPTION.ToString())).ToString());

                        decimal wastageqty = requiredqty * (decimal.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;

                    }
                }
            }
            catch (Exception)
            {


            }

            return requiredqty;
        }

        public decimal GroupDependantSizeQty(int skudetpk)
        {
            decimal requiredqty = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ISNULL(MAX(StyleCostingDetails.Consumption), 0) AS Consumption, ISNULL(SUM(POPackDetails.PoQty), 0) AS PoQty, ISNULL(AVG(SkuRawMaterialMaster.WastagePercentage), 0) AS WastagePercentage
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         GroupDependantItems ON SkuRawMaterialMaster.Sku_Pk = GroupDependantItems.Sku_PK INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID AND GroupDependantItems.POPackID = POPackDetails.POPackId AND 
                         SkuRawmaterialDetail.SizeCode = POPackDetails.SizeCode AND GroupDependantItems.OurStyleID = POPackDetails.OurStyleID
WHERE        (StyleCostingMaster.IsApproved = N'A') and (GroupDependantItems.IsDepenant='Y')
GROUP BY SkuRawmaterialDetail.SkuDet_PK
HAVING        (SkuRawmaterialDetail.SkuDet_PK = @Param1)";


            cmd.Parameters.AddWithValue("@param1", skudetpk);



            DataTable dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
            try
            {

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        var sum = dt.Rows[0]["PoQty"];
                        var CONSUMPTION = dt.Rows[0]["Consumption"];
                        var wastage = dt.Rows[0]["WastagePercentage"];
                        requiredqty = decimal.Parse((decimal.Parse(sum.ToString()) * decimal.Parse(CONSUMPTION.ToString())).ToString());

                        decimal wastageqty = requiredqty * (decimal.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;

                    }
                }
            }
            catch (Exception)
            {


            }

            return requiredqty;
        }

        public decimal GroupDependantSizeandColorQty(int skudetpk)
        {
            decimal requiredqty = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ISNULL(MAX(StyleCostingDetails.Consumption), 0) AS Consumption, ISNULL(SUM(POPackDetails.PoQty), 0) AS PoQty, ISNULL(AVG(SkuRawMaterialMaster.WastagePercentage), 0) AS WastagePercentage
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         GroupDependantItems ON SkuRawMaterialMaster.Sku_Pk = GroupDependantItems.Sku_PK INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID AND GroupDependantItems.POPackID = POPackDetails.POPackId AND 
                         SkuRawmaterialDetail.SizeCode = POPackDetails.SizeCode and     SkuRawmaterialDetail.ColorCode = POPackDetails.ColorCode AND GroupDependantItems.OurStyleID = POPackDetails.OurStyleID
WHERE        (StyleCostingMaster.IsApproved = N'A') and (GroupDependantItems.IsDepenant='Y')
GROUP BY SkuRawmaterialDetail.SkuDet_PK
HAVING        (SkuRawmaterialDetail.SkuDet_PK = @Param1)";


            cmd.Parameters.AddWithValue("@param1", skudetpk);



            DataTable dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
            try
            {

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        var sum = dt.Rows[0]["PoQty"];
                        var CONSUMPTION = dt.Rows[0]["Consumption"];
                        var wastage = dt.Rows[0]["WastagePercentage"];
                        requiredqty = decimal.Parse((decimal.Parse(sum.ToString()) * decimal.Parse(CONSUMPTION.ToString())).ToString());

                        decimal wastageqty = requiredqty * (decimal.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;

                    }
                }
            }
            catch (Exception)
            {


            }

            return requiredqty;
        }





        public int GetDeliverYdsQty(int cutid)
        {
            int balqty = 0;
            decimal bal = 0.0M;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select round(isnull(tt.DeliveredQty,0),0 ) as balQty from 
(SELECT        CutOrderMaster.CutID, MAX(CutOrderMaster.FabQty) AS FabQty, SUM(ISNULL(CutOrderDO.RollYard , CutOrderDO.DeliveryQty )) AS DeliveredQty
FROM            CutOrderMaster LEFT OUTER JOIN
                         CutOrderDO ON CutOrderMaster.CutID = CutOrderDO.CutID where        (CutOrderMaster.CutID =@param1)
GROUP BY CutOrderMaster.CutID)tt";
            cmd.Parameters.AddWithValue("@param1", cutid);

                bal = decimal.Parse(QueryFunctions.ReturnQueryValue(cmd).ToString());
                balqty = Convert.ToInt32(bal);
                return balqty;
            


        }

        public int GetbalanceQty(int cutid)
        {
            int balqty = 0;
            decimal bal = 0.0M;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select round(isnull(tt.FabQty-isnull(tt.DeliveredQty,0),0),0 )as balQty from 
(SELECT        CutOrderMaster.CutID, MAX(CutOrderMaster.FabQty) AS FabQty, SUM(ISNULL(CutOrderDO.RollYard , CutOrderDO.DeliveryQty )) AS DeliveredQty
FROM            CutOrderMaster LEFT OUTER JOIN
                         CutOrderDO ON CutOrderMaster.CutID = CutOrderDO.CutID where        (CutOrderMaster.CutID =@param1)
GROUP BY CutOrderMaster.CutID)tt";
            cmd.Parameters.AddWithValue("@param1", cutid);

            try
            {

                bal = decimal.Parse(QueryFunctions.ReturnQueryValue(cmd).ToString());
                balqty = Convert.ToInt32(bal);
                return balqty;
            }
            catch (Exception exp)
            {

                throw;
            }
           

           
        }






        public int GetDeliveredQty(int cutid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        SUM(ISNULL(DeliveryQty, 0)) AS DeliveredQty
FROM            CutOrderDO
GROUP BY CutID
HAVING        (CutID = @Param1)";
            cmd.Parameters.AddWithValue("@param1", cutid);
            int balqty = int.Parse(QueryFunctions.ReturnQueryValue(cmd).ToString());

            return balqty;
        }










    }



    public class CutDetailsData
    {
        DBTransaction.CutOrderTransaction cuttrans = null;
        public int CutOrderDet_PK { get; set; }
        public int CutID { get; set; }
        public string MarkerNo { get; set; }
        public int NoOfPc { get; set; }
        public int Qty { get; set; }
        public List<CutSizeDetailsData> CutSizeDetailsDataCollection { get; set; }


        public String InsertCutOrderSizeData()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {



               


                    foreach (CutSizeDetailsData di in this.CutSizeDetailsDataCollection)
                {
                    if (!enty.CutOrderSizeDetails.Any(f => f.CutOrderDet_PK == di.CutOrderDet_PK && f.Size.Trim() == di.Sizename.Trim ()))
                    {
                        CutOrderSizeDetail cddetail = new CutOrderSizeDetail();
                        cddetail.CutOrderDet_PK = di.CutOrderDet_PK;
                        cddetail.Size = di.Sizename;
                        cddetail.Qty = di.Qty;
                        cddetail.Ratio = di.Ratio;
                        enty.CutOrderSizeDetails.Add(cddetail);
                    }
                    else
                    {
                        var Q = from cutsizedet in enty.CutOrderSizeDetails
                                where cutsizedet.CutOrderDet_PK == di.CutOrderDet_PK && cutsizedet.Size.Trim() == di.Sizename
                                select cutsizedet;
                        foreach(var element in Q)
                        {
                            element.Qty = di.Qty;
                            element.Ratio = di.Ratio;
                        }

                    }

                    enty.SaveChanges();
                    //  updatecutdetail(di.CutOrderDet_PK);
                    updatecutdetailSP(di.CutOrderDet_PK);


                }





                enty.SaveChanges();


                
            }

            return Cutn;

        }

        public void updatecutdetail(int cutdetpk)
        {
            cuttrans = new CutOrderTransaction();
            cuttrans.updatecutdet(cutdetpk);
        }
        public void updatecutdetailSP(int cutdetpk)
        {
            cuttrans = new CutOrderTransaction();
            cuttrans.updatecutdetSP(cutdetpk);
        }

        public DataTable GetCutOrderSizeData(int cutid)
        {
            DataTable dt = new DataTable();
            cuttrans = new CutOrderTransaction();
            dt = cuttrans.GetCutOrderSizeData(cutid);


            return dt;

        }
        public DataTable GetCutOrderSizeDataofMarker(int cutdetid)
        {
            DataTable dt = new DataTable();
            cuttrans = new CutOrderTransaction();
            dt = cuttrans.GetCutOrderSizeDataofMarker(cutdetid);


            return dt;

        }
        /// <summary>
        /// get sizewise data of total cutorder
        /// </summary>
        /// <param name="cutorder_pk"></param>
        /// <returns></returns>
        public DataTable GetCutOrderMasterSizeDataofCutorder(int cutorder_pk)
        {
            DataTable dt = new DataTable();
            cuttrans = new CutOrderTransaction();
            dt = cuttrans.GetCutOrderSizeDataofCutorder(cutorder_pk);


            return dt;

        }

    }


    public class CutSizeDetailsData
    {
        public int CutOrderDet_PK { get; set; }
        public int CutOrderSizeDet_PK { get; set; }
        public string Sizename { get; set; }
        public Decimal Ratio { get; set; }
        public Decimal Qty { get; set; }




    }



    public class FinalCutOrderEntry
    {
        public Boolean IsCutOrdernumPresent(String Cutnum)
        {
            Boolean ispresent = false;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                if (!enty.CutOrderMasters.Any(f => f.Cut_NO.Trim() == Cutnum.Trim()))
                {
                    ispresent = false;
                }
                else
                {
                    ispresent = true;
                }
            }
            return ispresent;
        }


        DBTransaction.CutOrderTransaction cuttrans = null;
       
        public List<CutDetailsData> CutDetailsDataCollection { get; set; }
        public List<CutSizeDetailsData> CutSizeDetailsDataCollection { get; set; }

        public int cutplanpk { get; set; }

        public int Atcid { get; set; }
        public int Ourstyleid { get; set; }
        public String FabDescription { get; set; }
        public int CuitId { get; set; }
        public int Skudet_pk { get; set; }
        public String CutNum { get; set; }
        public String patername { get; set; }
        public String MarkerType { get; set; }

        public int Tofactid { get; set; }
        public Decimal Cutqty { get; set; }
        public String CutorderType { get; set; }
        public Decimal CutOrderQty { get; set; }
        public String Cutablewidth { get; set; }

        public String Shrinkage { get; set; }

        public String ColorCode { get; set; }
        public String ColorName { get; set; }
        public Decimal CofabAllocation { get; set; }
        public Decimal BalToCutQty { get; set; }
        public Decimal DeliveredQty { get; set; }
        public Decimal ApprovedConsumption { get; set; }

        public Decimal ExtraReason_Pk { get; set; }


        public int AddedBy { get; set; }
        public int AddedDate { get; set; }

        public String InsertNewCutOrder()
        {
            string Cutn = "";
          
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from CutPlnmstr in enty.CutPlanMasters
                        where CutPlnmstr.CutPlan_PK == this.cutplanpk
                        select CutPlnmstr;


                foreach (var element in q)
                {
                    this.FabDescription = element.FabDescription.ToString();
                    this.Cutablewidth = element.WidthGroup;
                    this.Shrinkage = element.ShrinkageGroup;
                    this.MarkerType = element.MarkerType;
                    this.Tofactid = int.Parse ( element.Location_PK.ToString ());
                    this.Skudet_pk = int.Parse(element.SkuDet_PK.ToString());
                    this.ColorCode = element.ColorCode.ToString();
                    this.ColorName = element.ColorName.ToString();
                    this.Ourstyleid= int.Parse(element.OurStyleID.ToString());
                    this.Atcid = int.Parse(element.AtcDetail.AtcId.ToString());
                    element.IsCutorderGiven = "Y";
                    element.CutOrderConsumption= this.ApprovedConsumption;

                }
                CutOrderMaster ctmstr = new CutOrderMaster();

                ctmstr.Color = this.FabDescription.ToString();
                ctmstr.CutWidth = this.Cutablewidth;
                ctmstr.Shrinkage = this.Shrinkage;
                ctmstr.MarkerType = this.MarkerType;
                ctmstr.ToLoc = this.Tofactid;
                ctmstr.SkuDet_pk = this.Skudet_pk;
                ctmstr.CutPlan_Pk = this.cutplanpk;
                ctmstr.AtcID = this.Atcid;
                ctmstr.OurStyleID = this.Ourstyleid;
                ctmstr.Cut_NO = this.CutNum;
                ctmstr.PaternName = this.patername;
                ctmstr.CutOrderType = this.CutorderType;
                ctmstr.CutQty = this.CutOrderQty;
                ctmstr.FabQty = this.CofabAllocation;
                ctmstr.IsDeleted = "N";
                ctmstr.ColorCode = this.ColorCode;
                ctmstr.ColorName = this.ColorName;
                ctmstr.BalanceQty = 0;
                ctmstr.DelivedQty = 0;
                ctmstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
                ctmstr.CutOrderDate = DateTime.Now;

                ctmstr.ExtraReason_Pk = this.ExtraReason_Pk;
                ctmstr.ConsumptionQty = this.ApprovedConsumption;
                ctmstr.ActualConsumption= this.ApprovedConsumption;
                enty.CutOrderMasters.Add(ctmstr);
                enty.SaveChanges();


                HttpContext.Current.Session["cutid"] = ctmstr.CutID;




                var q1 = from CutPlnmstr in enty.CutPlanMarkerDetails
                        where CutPlnmstr.CutPlan_PK == this.cutplanpk
                        select CutPlnmstr;


                foreach (var element123 in q1)
                {
                    CutOrderDetail cddetail = new CutOrderDetail();
                    cddetail.MarkerNo = element123.MarkerNo;
                    cddetail.NoOfPc = element123.NoOfPc;
                    cddetail.Qty = element123.Qty;
                    cddetail.CutID = ctmstr.CutID;
                    cddetail.CutPlanMarkerDetails_PK = element123.CutPlanMarkerDetails_PK;
                    enty.CutOrderDetails.Add(cddetail);

                }
                enty.SaveChanges();




                var Q12 = from cutorderdet in enty.CutOrderDetails
                          where cutorderdet.CutID == ctmstr.CutID
                          select cutorderdet;
                foreach (var element1234 in Q12)
                {
                    int cutdetpk = int.Parse( element1234.CutOrderDet_PK .ToString ());
                    int CutPlanMarkerDetails_PK = int.Parse(element1234.CutPlanMarkerDetails_PK.ToString());

                    var q1234= from CutPlanSizeDetailsData in enty.CutPlanMarkerSizeDetails
                               where CutPlanSizeDetailsData.CutPlanMarkerDetails_PK == CutPlanMarkerDetails_PK
                               select CutPlanSizeDetailsData;
                    foreach (var sizedata in q1234)
                    {
                        if (!enty.CutOrderSizeDetails.Any(f => f.CutOrderDet_PK == cutdetpk && f.Size.Trim() == sizedata.Size.Trim() && f.CutPlanMarkerDetails_PK == CutPlanMarkerDetails_PK))
                        {
                            CutOrderSizeDetail cddetail = new CutOrderSizeDetail();
                            cddetail.CutOrderDet_PK = cutdetpk;
                            cddetail.Size = sizedata.Size;
                            cddetail.Qty = sizedata.Qty;
                            cddetail.Ratio = sizedata.Ratio;
                            cddetail.CutPlanMarkerDetails_PK = CutPlanMarkerDetails_PK;
                            cddetail.CutPlanSize_PK = sizedata.CutPlanSize_PK;
                            enty.CutOrderSizeDetails.Add(cddetail);
                        }
                        else
                        {
                            var Qr = from cutsizedet in enty.CutOrderSizeDetails
                                    where cutsizedet.CutOrderDet_PK == cutdetpk && cutsizedet.Size.Trim() == sizedata.Size && cutsizedet.CutPlanMarkerDetails_PK == CutPlanMarkerDetails_PK
                                    select cutsizedet;
                            foreach (var element in Qr)
                            {
                                element.Qty = sizedata.Qty;
                                element.Ratio = sizedata.Ratio;
                            }

                        }

                    }

                }


                enty.SaveChanges();
                }

            return Cutn;

        }

        public String UpdateCutOrder()
        {
            string Cutn = "";
            decimal cutid = 0;

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            { 

                var q = from CutPlnmstr in enty.CutPlanMasters
                        where CutPlnmstr.CutPlan_PK == this.cutplanpk
                        select CutPlnmstr;


                foreach (var element in q)
                {
                    this.FabDescription = element.FabDescription.ToString();
                    this.Cutablewidth = element.WidthGroup;
                    this.Shrinkage = element.ShrinkageGroup;
                    this.MarkerType = element.MarkerType;
                    this.Tofactid = int.Parse(element.Location_PK.ToString());
                    this.Skudet_pk = int.Parse(element.SkuDet_PK.ToString());
                    this.ColorCode = element.ColorCode.ToString();
                    this.ColorName = element.ColorName.ToString();
                    element.IsCutorderGiven = "Y";
                    element.CutOrderConsumption = this.ApprovedConsumption;

                }


                //Cutorder details

                var alreadyexistingCutorder = from cutorderordr in enty.CutOrderMasters
                                              where cutorderordr.CutPlan_Pk == this.cutplanpk
                                              select cutorderordr;
                foreach (var ctmstr in alreadyexistingCutorder)
                {
                    ctmstr.Color = this.FabDescription.ToString();
                    ctmstr.CutWidth = this.Cutablewidth;
                    ctmstr.Shrinkage = this.Shrinkage;
                    ctmstr.MarkerType = this.MarkerType;
                    ctmstr.ToLoc = this.Tofactid;
                    ctmstr.SkuDet_pk = this.Skudet_pk;
                    ctmstr.CutPlan_Pk = this.cutplanpk;
                    ctmstr.AtcID = this.Atcid;
                    ctmstr.OurStyleID = this.Ourstyleid;
                    ctmstr.Cut_NO = this.CutNum;
                    ctmstr.PaternName = this.patername;
                    ctmstr.CutOrderType = this.CutorderType;
                    ctmstr.CutQty = this.CutOrderQty;
                    ctmstr.FabQty = this.CofabAllocation;
                    ctmstr.IsDeleted = "N";
                    ctmstr.IsClosed = "N";
                    ctmstr.ColorCode = this.ColorCode;
                    ctmstr.ColorName = this.ColorName;
                    ctmstr.BalanceQty = 0;
                    ctmstr.DelivedQty = 0;
                    ctmstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
                    ctmstr.CutOrderDate = DateTime.Now;

                    ctmstr.ExtraReason_Pk = this.ExtraReason_Pk;
                    ctmstr.ConsumptionQty = this.ApprovedConsumption;
                    ctmstr.ActualConsumption = this.ApprovedConsumption;

                    cutid = ctmstr.CutID;
                }

                var q1 = from CutPlnmstr in enty.CutPlanMarkerDetails
                         where CutPlnmstr.CutPlan_PK == this.cutplanpk
                         select CutPlnmstr;


                foreach (var element123 in q1)
                {
                    if (!enty.CutOrderDetails.Any(f => f.CutPlanMarkerDetails_PK == element123.CutPlanMarkerDetails_PK))
                    {

                        CutOrderDetail cddetail = new CutOrderDetail();
                        cddetail.MarkerNo = element123.MarkerNo;
                        cddetail.NoOfPc = element123.NoOfPc;
                        cddetail.Qty = element123.Qty;
                        cddetail.CutID = cutid;
                        cddetail.CutPlanMarkerDetails_PK = element123.CutPlanMarkerDetails_PK;
                        enty.CutOrderDetails.Add(cddetail);

                     

                    }
                    else
                    {
                        var existingcutorderdertailq = from cutorderdetails in enty.CutOrderDetails
                                                       where cutorderdetails.CutPlanMarkerDetails_PK == element123.CutPlanMarkerDetails_PK
                                                       select cutorderdetails;
                        foreach (var cddetail in existingcutorderdertailq)
                        {
                            cddetail.MarkerNo = element123.MarkerNo;
                            cddetail.NoOfPc = element123.NoOfPc;
                            cddetail.Qty = element123.Qty;
                            cddetail.CutID = cutid;
                            cddetail.CutPlanMarkerDetails_PK = element123.CutPlanMarkerDetails_PK;

                        }

                    }







                }
               
                    enty.SaveChanges();

               




                var Q12 = from cutorderdet in enty.CutOrderDetails
                          where cutorderdet.CutID == cutid
                          select cutorderdet;
                foreach (var element1234 in Q12)
                {
                    int cutdetpk = int.Parse(element1234.CutOrderDet_PK.ToString());

                    int CutPlanMarkerDetails_PK = int.Parse(element1234.CutPlanMarkerDetails_PK.ToString());

                    var q1234 = from CutPlanSizeDetailsData in enty.CutPlanMarkerSizeDetails
                                where CutPlanSizeDetailsData.CutPlanMarkerDetails_PK == CutPlanMarkerDetails_PK
                                select CutPlanSizeDetailsData;
                    foreach (var sizedata in q1234)
                    {
                        if (!enty.CutOrderSizeDetails.Any(f => f.CutOrderDet_PK == cutdetpk && f.Size.Trim() == sizedata.Size.Trim() && f.CutPlanMarkerDetails_PK == CutPlanMarkerDetails_PK))
                        {
                            CutOrderSizeDetail cddetail = new CutOrderSizeDetail();
                            cddetail.CutOrderDet_PK = cutdetpk;
                            cddetail.Size = sizedata.Size;
                            cddetail.Qty = sizedata.Qty;
                            cddetail.Ratio = sizedata.Ratio;
                            cddetail.CutPlanMarkerDetails_PK = CutPlanMarkerDetails_PK;
                            cddetail.CutPlanSize_PK = sizedata.CutPlanSize_PK;
                            enty.CutOrderSizeDetails.Add(cddetail);
                        }
                        else
                        {
                            var Qr = from cutsizedet in enty.CutOrderSizeDetails
                                     where cutsizedet.CutOrderDet_PK == cutdetpk && cutsizedet.Size.Trim() == sizedata.Size && cutsizedet.CutPlanMarkerDetails_PK == CutPlanMarkerDetails_PK
                                     select cutsizedet;
                            foreach (var element in Qr)
                            {
                                element.Qty = sizedata.Qty;
                                element.Ratio = sizedata.Ratio;
                            }

                        }

                    }

                }


                enty.SaveChanges();
            }

            return Cutn;
        }
        


    }












}