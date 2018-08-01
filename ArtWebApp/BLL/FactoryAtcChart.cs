using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ArtWebApp.DataModels;
using System.Web.UI.WebControls;

namespace ArtWebApp.BLL
{
    public static class FactoryAtcChart
    {
        static String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;

        static float garqty = 0;
        public static DataTable ShowBOM(int atcid,string type,int ourstyleid)
        {
            DataTable BomData = new DataTable();
            if (type=="atc")
            {
               BomData = GetBOM(atcid);
            }
           else if(type=="style")
            {
               BomData = GetBOMofStyle(ourstyleid);
            }
            else if (type == "PO")
            {
                BomData = GetBOM(atcid);
            }

            if (BomData.Rows.Count <= 0)
            {

            }
            else
            {
                foreach (System.Data.DataColumn col in BomData.Columns) col.ReadOnly = false;
                // CalculateRequiredPOIssued(BomData);
                CalculateRequiredPOIssued(BomData, atcid,type,ourstyleid);


            }
            HttpContext.Current.Session["condition"] = null;
            return BomData;


        }



        public static DataTable GetBOM(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ColorCode, 
                         SkuRawmaterialDetail.SizeCode, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize, ISNULL
                             ((SELECT        MAX(StyleCostingDetails.Rate) AS Expr1
                                 FROM            StyleCostingDetails INNER JOIN
                                                          StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK
                                 GROUP BY StyleCostingDetails.Sku_PK, StyleCostingMaster.IsApproved
                                 HAVING        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk)), SkuRawmaterialDetail.UnitRate) AS UnitRate, ISNULL
                             ((SELECT        MAX(StyleCostingDetails_1.Consumption) AS Expr1
                                 FROM            StyleCostingDetails AS StyleCostingDetails_1 INNER JOIN
                                                          StyleCostingMaster AS StyleCostingMaster_1 ON StyleCostingDetails_1.Costing_PK = StyleCostingMaster_1.Costing_PK
                                 GROUP BY StyleCostingDetails_1.Sku_PK, StyleCostingMaster_1.IsApproved
                                 HAVING        (StyleCostingMaster_1.IsApproved = N'A') AND (StyleCostingDetails_1.Sku_PK = SkuRawMaterialMaster.Sku_Pk)), 0) AS Consumption, SkuRawmaterialDetail.RqdQty, 0000 AS PoIssuedQty, 
                         0000 AS BalanceQty, SkuRawMaterialMaster.Uom_PK, SkuRawMaterialMaster.AltUom_pk, SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.isCommon, SkuRawMaterialMaster.IsCD, 
                         SkuRawMaterialMaster.IsSD, SkuRawMaterialMaster.IsGD, UOMMaster.UomCode, SkuRawMaterialMaster.Template_pk, ISNULL(SkuRawMaterialMaster.OrderMin, 0) AS OrderMin, 
                         Template_Master.ItemGroup_PK, SizeMaster.SizeName, 00 AS GarmentQty, ColorMaster.ColorName, SkuRawMaterialMaster.WastagePercentage, SkuRawMaterialMaster.Sku_Pk
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON SkuRawMaterialMaster.AltUom_pk = UOMMaster.Uom_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ColorMaster ON SkuRawmaterialDetail.ColorCode = ColorMaster.ColorCode LEFT OUTER JOIN
                         SizeMaster ON SkuRawmaterialDetail.SizeCode = SizeMaster.SizeCode
WHERE        (SkuRawMaterialMaster.Atc_id = @atcid)
ORDER BY Template_Master.ItemGroup_PK, SkuRawMaterialMaster.RMNum
                ", con);



                cmd.Parameters.AddWithValue("@atcid", atcid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public static DataTable GetBOMofStyle(int ourstyleid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ColorCode, 
                         SkuRawmaterialDetail.SizeCode, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize, ISNULL
                             ((SELECT        MAX(StyleCostingDetails.Rate) AS Expr1
                                 FROM            StyleCostingDetails INNER JOIN
                                                          StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK
                                 GROUP BY StyleCostingDetails.Sku_PK, StyleCostingMaster.IsApproved
                                 HAVING        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk)), SkuRawmaterialDetail.UnitRate) AS UnitRate, ISNULL
                             ((SELECT        MAX(StyleCostingDetails_1.Consumption) AS Expr1
                                 FROM            StyleCostingDetails AS StyleCostingDetails_1 INNER JOIN
                                                          StyleCostingMaster AS StyleCostingMaster_1 ON StyleCostingDetails_1.Costing_PK = StyleCostingMaster_1.Costing_PK
                                 GROUP BY StyleCostingDetails_1.Sku_PK, StyleCostingMaster_1.IsApproved
                                 HAVING        (StyleCostingMaster_1.IsApproved = N'A') AND (StyleCostingDetails_1.Sku_PK = SkuRawMaterialMaster.Sku_Pk)), 0) AS Consumption, SkuRawmaterialDetail.RqdQty, 0000 AS PoIssuedQty, 
                         0000 AS BalanceQty, SkuRawMaterialMaster.Uom_PK, SkuRawMaterialMaster.AltUom_pk, SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.isCommon, SkuRawMaterialMaster.IsCD, 
                         SkuRawMaterialMaster.IsSD, SkuRawMaterialMaster.IsGD, UOMMaster.UomCode, SkuRawMaterialMaster.Template_pk, ISNULL(SkuRawMaterialMaster.OrderMin, 0) AS OrderMin, 
                         Template_Master.ItemGroup_PK, SizeMaster.SizeName, 00 AS GarmentQty, ColorMaster.ColorName, SkuRawMaterialMaster.WastagePercentage, SkuRawMaterialMaster.Sku_Pk, 
                         StyleCostingMaster_2.IsApproved, StyleCostingDetails_2.IsRequired, StyleCostingMaster_2.OurStyleID
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON SkuRawMaterialMaster.AltUom_pk = UOMMaster.Uom_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ColorMaster ON SkuRawmaterialDetail.ColorCode = ColorMaster.ColorCode INNER JOIN
                         StyleCostingDetails AS StyleCostingDetails_2 ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails_2.Sku_PK INNER JOIN
                         StyleCostingMaster AS StyleCostingMaster_2 ON StyleCostingDetails_2.Costing_PK = StyleCostingMaster_2.Costing_PK LEFT OUTER JOIN
                         SizeMaster ON SkuRawmaterialDetail.SizeCode = SizeMaster.SizeCode
WHERE        (StyleCostingMaster_2.IsApproved = N'A') AND (StyleCostingDetails_2.IsRequired = N'Y') AND (StyleCostingMaster_2.OurStyleID = @OurStyleID)
ORDER BY Template_Master.ItemGroup_PK, SkuRawMaterialMaster.RMNum
                ", con);



                cmd.Parameters.AddWithValue("@OurStyleID", ourstyleid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public static DataTable CalculateRequiredPOIssued(DataTable dt, int atcid,String type,int ourstyleid)
        {
            DataTable skudata = GetSKUData(atcid);

            if (type == "atc")
            {
              
            }
            else if (type == "style")
            {
                try
                {
                    skudata = skudata.Select("OurStyleID=" + ourstyleid + "").CopyToDataTable();
                }
                catch (Exception)
                {
                    skudata = null;


                }
            }
            else if (type == "PO")
            {
                try
                {
                    string condition = HttpContext.Current.Session["condition"].ToString().Trim();
                   
                    skudata = skudata.Select(condition).CopyToDataTable();
                   
                }
                catch (Exception)
                {
                    skudata = null;


                }
            }

            DataTable EBOMData = GetEBOMData(atcid);
            DataTable POData = GetPOData(atcid);
            DataTable WPOData = GetWPOData(atcid);
            DataView ourstyleview = new DataView(skudata);
            DataTable distinctOurstyleData = ourstyleview.ToTable(true, "OurStyleID");
            DataTable MissedData = GetInventorymisplacedData(atcid);
            DataTable RoOutQtyData = GetROOutQtyData(atcid);

            DBTransaction.BOMTransaction bomtrans = new DBTransaction.BOMTransaction();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                float requiredqty = 0;
                int extrabomqty = 0;
                int posiisued = 0;
                int wrongpoissued = 0;
                int missedqty = 0;
                int garmentqty = 0;
               int balanceqty = 0;
                int roqty = 0;

                int skudetpk = int.Parse(dt.Rows[i]["SkuDet_PK"].ToString().Trim());

                if (skudetpk == 61795)
                {
                    int k = 9;
                  }
                int uom_pk = int.Parse(dt.Rows[i]["uom_pk"].ToString().Trim());
                String isCD = dt.Rows[i]["IsCD"].ToString().Trim();
                String isSD = dt.Rows[i]["IsSD"].ToString().Trim();
                String isCM = dt.Rows[i]["isCommon"].ToString().Trim();
                String IsGD = dt.Rows[i]["IsGD"].ToString().Trim();


               


                



                //int reqty = (int)Math.Round(requiredQtyCalculate(skudetpk, isCD, isSD, isCM, IsGD, skudata), 0);
                int reqty = (int)Math.Round(requiredQtyCalculate(skudetpk, isCD, isSD, isCM, IsGD, skudata, distinctOurstyleData,type), 0);
                try
                {
                    int ordermin = (int)Math.Round(float.Parse(dt.Rows[i]["OrderMin"].ToString()));
                    if (reqty < ordermin)
                    {
                        reqty = ordermin;
                    }

                }
                catch (Exception)
                {


                }
                try
                {
                    garmentqty = (int)Math.Round(garqty, 0);


                }
                catch (Exception)
                {


                }
                try
                {
                    extrabomqty = (int)Math.Round(GetExtraBOMRequest(skudetpk, EBOMData));
                }
                catch (Exception)
                {


                }
                try
                {
                    posiisued = (int)Math.Round(GetPoIssuedQtyinBaseUOM(skudetpk, uom_pk, POData), 0);
                }
                catch (Exception)
                {


                }
                try
                {
                    wrongpoissued = (int)Math.Round(GetWrongPoIssuedQtyinBaseUOMwithApproval(skudetpk, uom_pk, WPOData), 0);
                }
                catch (Exception)
                {


                }
                try
                {
                    missedqty = (int)Math.Round(GetMissplacedQtyinBaseUOMwithApproval(skudetpk, uom_pk, MissedData), 0);
                }
                catch (Exception)
                {


                }
                try
                {
                    roqty = (int)Math.Round(GetROOutQtyinBaseUOMwithApproval(skudetpk, uom_pk, RoOutQtyData), 0);
                }
                catch (Exception)
                {


                }
                try
                {
                    balanceqty = (reqty - posiisued) + wrongpoissued + extrabomqty + missedqty + roqty; ;

                }
                catch (Exception)
                {


                }
               
                dt.Rows[i]["RqdQty"] = reqty;
                dt.Rows[i]["PoIssuedQty"] = posiisued;
                dt.Rows[i]["BalanceQty"] = balanceqty;
                dt.Rows[i]["GarmentQty"] = garmentqty;
                
                

            }
            dt = dt.Select("RqdQty > 0 or  GarmentQty> 0 or PoIssuedQty>0 ").CopyToDataTable();
            return dt;
        }
















        public static float requiredQtyCalculate(int skudetpk, String isCD, String isSD, String isCM, String IsGD, DataTable skudata,DataTable ourstyles,string type)
        {
            float requiredqty = 0;

            garqty = 0;



            if (isCM == "Y" && isCD == "N" && isSD == "N" && IsGD == "N")
            {
                try
                {
                   

                    for(int i=0;i< ourstyles.Rows.Count;i++)
                    {
                        try
                        {
                            DataTable newresult = skudata.Select("SkuDet_PK = " + skudetpk + " and OurStyleID=" + int.Parse(ourstyles.Rows[i]["OurStyleID"].ToString()) + "").CopyToDataTable();
                            float ourstylereq = 0;
                            var ourstylesum = newresult.Compute("SUM(PoQty)", "");
                            var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
                            var wastage = newresult.Compute("MAX(WastagePercentage)", "");

                            ourstylereq = float.Parse((float.Parse(ourstylesum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                            float wastageqty = ourstylereq * (float.Parse(wastage.ToString()) / 100);
                            ourstylereq = ourstylereq + wastageqty;
                            requiredqty = requiredqty + ourstylereq;
                            garqty = garqty + (float.Parse(ourstylesum.ToString()));
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
                        float ourstylereq = 0;
                        var ourstylesum = newresult.Compute("SUM(PoQty)", "");
                        var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
                        var wastage = newresult.Compute("MAX(WastagePercentage)", "");

                        ourstylereq = float.Parse((float.Parse(ourstylesum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = ourstylereq * (float.Parse(wastage.ToString()) / 100);
                        ourstylereq = ourstylereq + wastageqty;
                        requiredqty = requiredqty + ourstylereq;
                        garqty = garqty + (float.Parse(ourstylesum.ToString()));
                        }
                        catch (Exception)
                        {


                        }
                    }










                    //DataTable newresult = skudata.Select("SkuDet_PK = " + skudetpk + " and ColorCode =SKUColorCode").CopyToDataTable();

                    //var sum = newresult.Compute("SUM(PoQty)", "");
                    //var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
                    //var wastage = newresult.Compute("MAX(WastagePercentage)", "");

                    //requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                    //float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);

                    //garqty = (float.Parse(sum.ToString()));
                    //requiredqty = requiredqty + wastageqty;
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
                        float ourstylereq = 0;
                        var ourstylesum = newresult.Compute("SUM(PoQty)", "");
                        var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
                        var wastage = newresult.Compute("MAX(WastagePercentage)", "");

                        ourstylereq = float.Parse((float.Parse(ourstylesum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = ourstylereq * (float.Parse(wastage.ToString()) / 100);
                        ourstylereq = ourstylereq + wastageqty;
                        requiredqty = requiredqty + ourstylereq;
                        garqty = garqty + (float.Parse(ourstylesum.ToString()));
                    }
                        catch (Exception)
                {


                }
            }









                    //DataTable newresult = skudata.Select("SkuDet_PK = " + skudetpk + " and SIzeCode =SKUSizeCode").CopyToDataTable();

                    //var sum = newresult.Compute("SUM(PoQty)", "");
                    //var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
                    //var wastage = newresult.Compute("MAX(WastagePercentage)", "");

                    //requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                    //float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);

                    //garqty = (float.Parse(sum.ToString()));
                    //requiredqty = requiredqty + wastageqty;
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
                        float ourstylereq = 0;
                        var ourstylesum = newresult.Compute("SUM(PoQty)", "");
                        var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
                        var wastage = newresult.Compute("MAX(WastagePercentage)", "");

                        ourstylereq = float.Parse((float.Parse(ourstylesum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = ourstylereq * (float.Parse(wastage.ToString()) / 100);
                        ourstylereq = ourstylereq + wastageqty;
                        requiredqty = requiredqty + ourstylereq;
                        garqty = garqty + (float.Parse(ourstylesum.ToString()));
                        }
                        catch (Exception)
                        {


                        }
                    }











                    //DataTable newresult = skudata.Select("SkuDet_PK = " + skudetpk + " and SIzeCode =SKUSizeCode and ColorCode =SKUColorCode").CopyToDataTable();

                    //var sum = newresult.Compute("SUM(PoQty)", "");
                    //var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
                    //var wastage = newresult.Compute("MAX(WastagePercentage)", "");

                    //requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                    //float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);

                    //garqty = (float.Parse(sum.ToString()));
                    //requiredqty = requiredqty + wastageqty;
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
                        if(type=="PO")
                        {
                            string condition = HttpContext.Current.Session["condition"].ToString().Trim();
                            requiredqty = GroupDependantCommonQtyForPO(skudetpk,condition);
                        }
                        else
                        {
                            requiredqty = GroupDependantCommonQty(skudetpk);
                        }
                        
                    }
                    else if (isCM == "N" && isCD == "Y" && isSD == "N" && IsGD == "Y")
                    {
                        if (type == "PO")
                        {
                            string condition = HttpContext.Current.Session["condition"].ToString().Trim();
                            requiredqty = GroupDependantColorQtyforPO(skudetpk, condition);

                        }
                        else
                        {
                            requiredqty = GroupDependantColorQty(skudetpk);
                        }
                      

                    }
                    else if (isCM == "N" && isCD == "N" && isSD == "Y" && IsGD == "Y")
                    {
                        if (type == "PO")
                        {
                            string condition = HttpContext.Current.Session["condition"].ToString().Trim();
                            requiredqty = GroupDependantSizeQtyforPO(skudetpk, condition);
                        }
                        else
                        {
                            requiredqty = GroupDependantSizeQty(skudetpk);
                        }
                       

                    }
                    else if (isCM == "N" && isCD == "Y" && isSD == "Y" && IsGD == "Y")
                    {
                        if (type == "PO")
                        {
                            string condition = HttpContext.Current.Session["condition"].ToString().Trim();
                            requiredqty = GroupDependantSizeandColorQtyforPO(skudetpk, condition);
                        }
                        else
                        {
                            requiredqty = GroupDependantSizeandColorQty(skudetpk);
                        }
                       

                    }
                }
                catch (Exception)
                {


                }
            }

            return requiredqty;
        }











        public static float GetExtraBOMRequest(int skudetpk, DataTable EBomData)

        {
            float extraqty = 0;
            try
            {
                var sum = EBomData.Compute("SUM(ExtraQty)", "Skudet_PK=" + skudetpk + "");
                extraqty = float.Parse(sum.ToString());
            }
            catch (Exception ex)
            {


            }
            return extraqty;
        }



        /// <summary>
        /// Get all the po issued qty and convert them into
        /// baseuom
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <param name="baseuom_pk"></param>
        /// <returns></returns>

        public static float GetPoIssuedQtyinBaseUOM(int skudetpk, int baseuom_pk, DataTable POData)
        {
            float poissuedqty = 0;



            DataTable dt = POData.Select("SkuDet_PK = " + skudetpk + "").CopyToDataTable();



            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Uom_PK"].ToString().Trim() == baseuom_pk.ToString().Trim())
                        {
                            poissuedqty = poissuedqty + float.Parse(dt.Rows[i]["POQty"].ToString());
                        }
                        else
                        {
                            poissuedqty = poissuedqty + UOMConvertortoAlt(baseuom_pk, int.Parse(dt.Rows[i]["Uom_PK"].ToString()), float.Parse(dt.Rows[i]["POQty"].ToString()));
                        }
                    }
                }
            }













            return poissuedqty;
        }


        /// <summary>
        /// convert the qnty in base UOm to the Qty in Alt UOm
        /// </summary>
        /// <param name="uomPK"></param>
        /// <param name="auomPk"></param>
        /// <param name="balqtyinBaseuom"></param>
        /// <returns></returns>
        public static float UOMConvertortoAlt(int uomPK, int auomPk, float balqtyinBaseuom)
        {

            float converttobaseqty = 0;
            float operend = 1;
            String operatorused = "*";

            DataTable dt = getAltuomdata(uomPK, auomPk);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    operend = float.Parse(dt.Rows[0]["Conv_fact"].ToString());
                    operatorused = dt.Rows[0]["Operator"].ToString().Trim();
                    if (operatorused == "*")
                    {

                        converttobaseqty = balqtyinBaseuom / operend;
                    }
                    else if (operatorused == "/")
                    {
                        converttobaseqty = balqtyinBaseuom * operend;
                    }
                }





            }
            return converttobaseqty;


        }


        /// <summary>
        /// Get the conversion factor and Operator for altuom conversion
        /// </summary>
        /// <param name="baseuom_pk"></param>
        /// <param name="altuom_pk"></param>
        /// <returns></returns>

        public static DataTable getAltuomdata(int baseuom_pk, int altuom_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        Conv_fact, Operator
FROM            AltUOMMaster
WHERE        (Uom_PK = @baseuom) AND (AltUom_PK = @altuom)", con);
                cmd.Parameters.AddWithValue("@baseuom", baseuom_pk);
                cmd.Parameters.AddWithValue("@altuom", altuom_pk);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public static float GetROOutQtyinBaseUOMwithApproval(int skudetpk, int baseuom_pk, DataTable RoData)
        {
            float poissuedqty = 0;
            DataTable dt = RoData.Select("SkuDet_PK = " + skudetpk + "").CopyToDataTable();




            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Uom_PK"].ToString().Trim() == baseuom_pk.ToString().Trim())
                        {
                            poissuedqty = poissuedqty + float.Parse(dt.Rows[i]["RoQty"].ToString());
                        }
                        else
                        {
                            poissuedqty = poissuedqty + UOMConvertortoAlt(baseuom_pk, int.Parse(dt.Rows[i]["Uom_PK"].ToString()), float.Parse(dt.Rows[i]["RoQty"].ToString()));
                        }
                    }
                }
            }













            return poissuedqty;
        }
        public static float GetWrongPoIssuedQtyinBaseUOMwithApproval(int skudetpk, int baseuom_pk, DataTable WPOData)
        {
            float poissuedqty = 0;
            DataTable dt = WPOData.Select("SkuDet_PK = " + skudetpk + "").CopyToDataTable();




            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Uom_PK"].ToString().Trim() == baseuom_pk.ToString().Trim())
                        {
                            poissuedqty = poissuedqty + float.Parse(dt.Rows[i]["POQty"].ToString());
                        }
                        else
                        {
                            poissuedqty = poissuedqty + UOMConvertortoAlt(baseuom_pk, int.Parse(dt.Rows[i]["Uom_PK"].ToString()), float.Parse(dt.Rows[i]["POQty"].ToString()));
                        }
                    }
                }
            }













            return poissuedqty;
        }


        /// <summary>
        /// Group Dependant and Common
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <returns></returns>
        public static float GroupDependantCommonQty(int skudetpk)
        {
            float requiredqty = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ISNULL(MAX(StyleCostingDetails.Consumption), 0) AS Consumption, ISNULL(SUM(POPackDetails.PoQty), 0) AS PoQty, ISNULL(AVG(SkuRawMaterialMaster.WastagePercentage), 0) AS WastagePercentage
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         GroupDependantItems ON SkuRawMaterialMaster.Sku_Pk = GroupDependantItems.Sku_PK INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID AND GroupDependantItems.POPackID = POPackDetails.POPackId AND 
                         GroupDependantItems.OurStyleID = POPackDetails.OurStyleID
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
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;
                        garqty = (float.Parse(sum.ToString()));
                    }
                }
            }
            catch (Exception)
            {


            }

            return requiredqty;
        }



        /// <summary>
        /// Group Dependant and Common for selected PO
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <returns></returns>
        public static float GroupDependantCommonQtyForPO(int skudetpk ,String Condition)
        {
            float requiredqty = 0;
          
            Condition = Condition.Replace("PoPackId ", "POPackDetails.POPackId");


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ISNULL(MAX(StyleCostingDetails.Consumption), 0) AS Consumption, ISNULL(SUM(POPackDetails.PoQty), 0) AS PoQty, ISNULL(AVG(SkuRawMaterialMaster.WastagePercentage), 0) AS WastagePercentage
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         GroupDependantItems ON SkuRawMaterialMaster.Sku_Pk = GroupDependantItems.Sku_PK INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID AND GroupDependantItems.POPackID = POPackDetails.POPackId AND 
                         GroupDependantItems.OurStyleID = POPackDetails.OurStyleID
WHERE        (StyleCostingMaster.IsApproved = N'A') and (GroupDependantItems.IsDepenant='Y') and ("+ Condition + @" )
GROUP BY SkuRawmaterialDetail.SkuDet_PK HAVING        (SkuRawmaterialDetail.SkuDet_PK = @Param1)";


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
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;
                        garqty = (float.Parse(sum.ToString()));
                    }
                }
            }
            catch (Exception)
            {


            }

            return requiredqty;
        }




        /// <summary>
        /// Group Dependant and Color
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <returns></returns>
        public static float GroupDependantColorQty(int skudetpk)
        {
            float requiredqty = 0;

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
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;
                        garqty = (float.Parse(sum.ToString()));
                    }
                }
            }
            catch (Exception)
            {


            }

            return requiredqty;
        }





        /// <summary>
        /// Group Dependant and Color for Selected PO
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <returns></returns>
        public static float GroupDependantColorQtyforPO(int skudetpk, String Condition)
        {
            float requiredqty = 0;
            Condition = Condition.Replace("PoPackId ", "POPackDetails.POPackId");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ISNULL(MAX(StyleCostingDetails.Consumption), 0) AS Consumption, ISNULL(SUM(POPackDetails.PoQty), 0) AS PoQty, ISNULL(AVG(SkuRawMaterialMaster.WastagePercentage), 0) AS WastagePercentage
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         GroupDependantItems ON SkuRawMaterialMaster.Sku_Pk = GroupDependantItems.Sku_PK INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID AND GroupDependantItems.POPackID = POPackDetails.POPackId AND 
                         SkuRawmaterialDetail.ColorCode = POPackDetails.ColorCode AND GroupDependantItems.OurStyleID = POPackDetails.OurStyleID
WHERE        (StyleCostingMaster.IsApproved = N'A') and (GroupDependantItems.IsDepenant='Y') and (" + Condition + @" )
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
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;
                        garqty = (float.Parse(sum.ToString()));
                    }
                }
            }
            catch (Exception)
            {


            }

            return requiredqty;
        }







        /// <summary>
        /// Group Dependant and Size
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <returns></returns>
        public static float GroupDependantSizeQty(int skudetpk)
        {
            float requiredqty = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ISNULL(MAX(StyleCostingDetails.Consumption), 0) AS Consumption, ISNULL(SUM(POPackDetails.PoQty), 0) AS PoQty, ISNULL(AVG(SkuRawMaterialMaster.WastagePercentage), 0) AS WastagePercentage
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         GroupDependantItems ON SkuRawMaterialMaster.Sku_Pk = GroupDependantItems.Sku_PK INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID AND GroupDependantItems.POPackID = POPackDetails.POPackId AND 
                         SkuRawmaterialDetail.SizeCode = POPackDetails.SizeCode  AND GroupDependantItems.OurStyleID = POPackDetails.OurStyleID
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
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;
                        garqty = (float.Parse(sum.ToString()));
                    }
                }
            }
            catch (Exception)
            {


            }

            return requiredqty;
        }



        /// <summary>
        /// Group Dependant and Size for Selected PO
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <returns></returns>
        public static float GroupDependantSizeQtyforPO(int skudetpk, String Condition)
        {
            float requiredqty = 0;
            Condition = Condition.Replace("PoPackId ", "POPackDetails.POPackId");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ISNULL(MAX(StyleCostingDetails.Consumption), 0) AS Consumption, ISNULL(SUM(POPackDetails.PoQty), 0) AS PoQty, ISNULL(AVG(SkuRawMaterialMaster.WastagePercentage), 0) AS WastagePercentage
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         GroupDependantItems ON SkuRawMaterialMaster.Sku_Pk = GroupDependantItems.Sku_PK INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID AND GroupDependantItems.POPackID = POPackDetails.POPackId AND 
                         SkuRawmaterialDetail.SizeCode = POPackDetails.SizeCode  AND GroupDependantItems.OurStyleID = POPackDetails.OurStyleID
WHERE        (StyleCostingMaster.IsApproved = N'A') and (GroupDependantItems.IsDepenant='Y') and (" + Condition + @" )
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
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;
                        garqty = (float.Parse(sum.ToString()));
                    }
                }
            }
            catch (Exception)
            {


            }

            return requiredqty;
        }




        /// <summary>
        /// Group Dependant and Color and Size
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <returns></returns>
        public static float GroupDependantSizeandColorQty(int skudetpk)
        {
            float requiredqty = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ISNULL(MAX(StyleCostingDetails.Consumption), 0) AS Consumption, ISNULL(SUM(POPackDetails.PoQty), 0) AS PoQty, ISNULL(AVG(SkuRawMaterialMaster.WastagePercentage), 0) AS WastagePercentage
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         GroupDependantItems ON SkuRawMaterialMaster.Sku_Pk = GroupDependantItems.Sku_PK INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID AND GroupDependantItems.POPackID = POPackDetails.POPackId AND 
                         SkuRawmaterialDetail.SizeCode = POPackDetails.SizeCode and     SkuRawmaterialDetail.ColorCode = POPackDetails.ColorCode  AND GroupDependantItems.OurStyleID = POPackDetails.OurStyleID
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
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;
                        garqty = (float.Parse(sum.ToString()));
                    }
                }
            }
            catch (Exception)
            {


            }

            return requiredqty;
        }









        /// <summary>
        /// Group Dependant and Color and Size
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <returns></returns>
        public static float GroupDependantSizeandColorQtyforPO(int skudetpk, String Condition)
        {
            float requiredqty = 0;
            Condition = Condition.Replace("PoPackId ", "POPackDetails.POPackId");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ISNULL(MAX(StyleCostingDetails.Consumption), 0) AS Consumption, ISNULL(SUM(POPackDetails.PoQty), 0) AS PoQty, ISNULL(AVG(SkuRawMaterialMaster.WastagePercentage), 0) AS WastagePercentage
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         GroupDependantItems ON SkuRawMaterialMaster.Sku_Pk = GroupDependantItems.Sku_PK INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID AND GroupDependantItems.POPackID = POPackDetails.POPackId AND 
                         SkuRawmaterialDetail.SizeCode = POPackDetails.SizeCode and     SkuRawmaterialDetail.ColorCode = POPackDetails.ColorCode  AND GroupDependantItems.OurStyleID = POPackDetails.OurStyleID
WHERE        (StyleCostingMaster.IsApproved = N'A') and (GroupDependantItems.IsDepenant='Y') and (" + Condition + @" )
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
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;
                        garqty = (float.Parse(sum.ToString()));
                    }
                }
            }
            catch (Exception)
            {


            }

            return requiredqty;
        }















        public static System.Data.DataTable GetSKUData(int ATCID)
        {
          

            using (SqlCommand cmd = new SqlCommand())
            {
                DataTable dt = new DataTable();



                cmd.CommandText = @" SELECT        SkuRawMaterialMaster.WastagePercentage, SkuRawMaterialMaster.Sku_Pk, SkuRawmaterialDetail.SkuDet_PK, StyleCostingDetails.Consumption, StyleCostingMaster.OurStyleID, POPackDetails.ColorCode, 
                                         POPackDetails.SIzeCode, POPackDetails.PoQty, StyleCostingMaster.IsApproved, SkuRawMaterialMaster.Atc_id, StyleCostingDetails.IsRequired,SkuRawmaterialDetail.ColorCode AS SKUColorCode, 
                                         SkuRawmaterialDetail.SizeCode AS SKUSizeCode, POPackDetails.POPackId
                FROM            SkuRawMaterialMaster INNER JOIN
                                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID INNER JOIN
                                         SkuRawmaterialDetail ON StyleCostingDetails.Sku_PK = SkuRawmaterialDetail.Sku_PK
                WHERE        (StyleCostingMaster.IsApproved = N'A') AND (SkuRawMaterialMaster.Atc_id = @ATCID) AND (StyleCostingDetails.IsRequired = N'Y')";


                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                return dt;

            }
           
        }

        public static System.Data.DataTable GetEBOMData(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        ISNULL(SUM(ExtraBOMRequestDetail.Qty), 0) AS ExtraQty,ExtraBOMRequestDetail.Skudet_PK
FROM            ExtraBOMRequestMaster INNER JOIN
                         ExtraBOMRequestDetail ON ExtraBOMRequestMaster.ExtraBOM_PK = ExtraBOMRequestDetail.ExtraBOM_PK
GROUP BY ExtraBOMRequestMaster.IsApproved, ExtraBOMRequestDetail.Skudet_PK, ExtraBOMRequestMaster.AtcId
HAVING        (ExtraBOMRequestMaster.IsApproved = N'Y') AND (ExtraBOMRequestMaster.AtcId = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }


        public static System.Data.DataTable GetPOData(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT SUM(ProcurementDetails.POQty) AS Poqty, ProcurementDetails.Uom_PK, ProcurementMaster.AtcId,ProcurementDetails.SkuDet_PK
FROM            ProcurementDetails INNER JOIN
                  ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk
WHERE        (ProcurementMaster.IsDeleted<> N'Y')
GROUP BY ProcurementDetails.Uom_PK, ProcurementDetails.SkuDet_PK, ProcurementMaster.AtcId
HAVING(ProcurementMaster.AtcId = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }

        public static float GetMissplacedQtyinBaseUOMwithApproval(int skudetpk, int baseuom_pk, DataTable MissedData)
        {
            float poissuedqty = 0;
            DataTable dt = MissedData.Select("SkuDet_PK = " + skudetpk + "").CopyToDataTable();




            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Uom_PK"].ToString().Trim() == baseuom_pk.ToString().Trim())
                        {
                            poissuedqty = poissuedqty + float.Parse(dt.Rows[i]["Qty"].ToString());
                        }
                        else
                        {
                            poissuedqty = poissuedqty + UOMConvertortoAlt(baseuom_pk, int.Parse(dt.Rows[i]["Uom_PK"].ToString()), float.Parse(dt.Rows[i]["Qty"].ToString()));
                        }
                    }
                }
            }













            return poissuedqty;
        }

        public static System.Data.DataTable GetWPOData(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        ProcurementDetails.Uom_PK, SUM(WrongPODetail.Qty) AS POQty, WrongPODetail.Skudet_pk, ProcurementMaster.AtcId
FROM            ProcurementDetails INNER JOIN
                         WrongPODetail ON ProcurementDetails.PODet_PK = WrongPODetail.Podet_PK INNER JOIN
                         WrongPOMaster ON WrongPODetail.WrongPO_Pk = WrongPOMaster.WrongPO_Pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk
GROUP BY ProcurementDetails.Uom_PK, ProcurementDetails.SkuDet_PK, WrongPOMaster.IsApproved, WrongPODetail.Skudet_pk, ProcurementMaster.AtcId
HAVING        (WrongPOMaster.IsApproved = N'Y') AND (ProcurementMaster.AtcId = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }





        public static System.Data.DataTable GetProcurementPlan(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        ProcurmentPlanDetails.ProcPlan_PK, ProcurmentPlanDetails.Skudet_Pk, ProcurmentPlanDetails.Qty, ProcurmentPlanDetails.ETADate, SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.Sku_Pk
FROM            ProcurmentPlanDetails INNER JOIN
                         SkuRawmaterialDetail ON ProcurmentPlanDetails.Skudet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
WHERE        (SkuRawMaterialMaster.Atc_id = @ATCID) and (ProcurmentPlanDetails.IsDeleted='N')";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }



        public static System.Data.DataTable GetInboundData(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        ShippingDocumentMaster.ShipperInv, ShippingDocumentMaster.ETA,  isnull(ShippingDocumentMaster.Conatianer,'')+'/'+isnull(ShippingDocumentMaster.BL,'') as Conatianer, DocDetails.Qty, ProcurementMaster.AtcId, ProcurementDetails.SkuDet_PK,DocDet_Pk as Det_Pk
FROM            ShippingDocumentMaster INNER JOIN
                         ShippingDocumentDetails ON ShippingDocumentMaster.ShipingDoc_PK = ShippingDocumentDetails.ShipingDoc_PK INNER JOIN
                         DocMaster ON ShippingDocumentDetails.Doc_Pk = DocMaster.Doc_Pk INNER JOIN
                         DocDetails ON DocMaster.Doc_Pk = DocDetails.Doc_Pk INNER JOIN
                         ProcurementDetails ON DocDetails.PODet_Pk = ProcurementDetails.PODet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk
WHERE(ProcurementMaster.AtcId = @ATCID)
union
SELECT        ShippingDocumentMaster.ShipperInv, ShippingDocumentMaster.ETA,  isnull(ShippingDocumentMaster.Conatianer,'')+'/'+isnull(ShippingDocumentMaster.BL,'') as Conatianer, DeliveryOrderDetails.DeliveryQty as Qty, DeliveryOrderMaster.AtcID, InventoryMaster.SkuDet_Pk,DeliveryOrderDetails.DODet_PK as det
FROM            ShippingDocumentMaster INNER JOIN
                         ShippingDocumentDODetails ON ShippingDocumentMaster.ShipingDoc_PK = ShippingDocumentDODetails.ShipingDoc_PK INNER JOIN
                         DeliveryOrderMaster ON ShippingDocumentDODetails.DO_PK = DeliveryOrderMaster.DO_PK INNER JOIN
                         DeliveryOrderDetails ON DeliveryOrderMaster.DO_PK = DeliveryOrderDetails.DO_PK INNER JOIN
                         InventoryMaster ON DeliveryOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK
WHERE(DeliveryOrderMaster.AtcID = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }





        public static System.Data.DataTable GetPODataofAtc(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


//                //                cmd.CommandText = @"
//                SELECT ProcurementMaster.PONum, ProcurementDetails.POQty, ProcurementDetails.SkuDet_PK, ProcurementMaster.AtcId, UOMMaster.UomCode, SupplierMaster.SupplierName
//         FROM            ProcurementDetails INNER JOIN
//                                  ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
//                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
//                         SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK
//WHERE(ProcurementMaster.IsDeleted = N'N') AND(ProcurementMaster.IsApproved = N'Y') AND(ProcurementMaster.AtcId = @ATCID)



//";
                cmd.CommandText = @"SELECT        ProcurementMaster.PONum, ProcurementDetails.POQty, ProcurementDetails.SkuDet_PK, ProcurementMaster.AtcId, UOMMaster.UomCode, SupplierMaster.SupplierName, SkuRawMaterialMaster.Uom_PK  AS  BaseUomPK, 
                         ProcurementDetails.Uom_PK ,000.00 as BaseUOMQty
FROM            ProcurementDetails INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
WHERE        (ProcurementMaster.IsDeleted = N'N') AND (ProcurementMaster.IsApproved = N'Y') AND (ProcurementMaster.AtcId = @ATCID)";


                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
                foreach (System.Data.DataColumn col in dt.Columns) col.ReadOnly = false;

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    float poQty = float.Parse(dt.Rows[i]["Poqty"].ToString().Trim());

                    int uompk = int.Parse(dt.Rows[i]["Uom_PK"].ToString());
                    int baseuompk= int.Parse(dt.Rows[i]["BaseUomPK"].ToString());


                    if (uompk == baseuompk)
                    {
                        poQty = poQty;
                    }
                    else
                    {
                        poQty =  UOMConvertortoAlt(baseuompk, uompk, poQty);
                    }
                    dt.Rows[i]["BaseUOMQty"] = poQty;


                }



            }
            return dt;
        }



        public static System.Data.DataTable GetOnhandQty(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        LocationMaster.LocationPrefix, SUM(InventoryMaster.OnhandQty) AS OnhandQty, InventoryMaster.SkuDet_Pk, LocationMaster.LocType
FROM            InventoryMaster INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
WHERE        (SkuRawMaterialMaster.Atc_id = @ATCID)
GROUP BY LocationMaster.LocationPrefix, InventoryMaster.SkuDet_Pk, LocationMaster.LocType
HAVING        (SUM(InventoryMaster.OnhandQty) > 0) AND (LocationMaster.LocType = N'W')



";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }

        public static System.Data.DataTable GetTransistQty(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"
SELECT        TOP (100) PERCENT GoodsInTransit.Gt_PK, GoodsInTransit.TransitQty, InventoryMaster.CURate * GoodsInTransit.TransitQty AS Value, DeliveryOrderMaster.DONum, LocationMaster.LocationPrefix, 
                         InventoryMaster.SkuDet_Pk, DeliveryOrderMaster.AtcID
FROM            InventoryMaster INNER JOIN
                         GoodsInTransit ON InventoryMaster.InventoryItem_PK = GoodsInTransit.InventoryItem_PK INNER JOIN
                         DeliveryOrderMaster ON GoodsInTransit.DO_PK = DeliveryOrderMaster.DO_PK INNER JOIN
                         LocationMaster ON DeliveryOrderMaster.ToLocation_PK = LocationMaster.Location_PK
WHERE        (GoodsInTransit.TransitQty > 0) AND (DeliveryOrderMaster.AtcID = @ATCID)

";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }

        public static System.Data.DataTable GetOnhandQty(int ATCID, string qrtype)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


//                cmd.CommandText = @"SELECT        LocationMaster.LocationPrefix, SUM(InventoryMaster.OnhandQty) AS OnhandQty, InventoryMaster.SkuDet_Pk, LocationMaster.LocType
//FROM            InventoryMaster INNER JOIN
//                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
//                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
//                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
//WHERE        (SkuRawMaterialMaster.Atc_id = @ATCID)
//GROUP BY LocationMaster.LocationPrefix, InventoryMaster.SkuDet_Pk, LocationMaster.LocType
//HAVING        (SUM(InventoryMaster.OnhandQty) > 0) ";




                cmd.CommandText = @"SELECT LocationMaster.LocationPrefix, SUM(InventoryMaster.OnhandQty) AS OnhandQty, InventoryMaster.SkuDet_Pk, LocationMaster.LocType, InventoryMaster.Uom_Pk, SkuRawMaterialMaster.Uom_PK AS BaseUomPK,000.00 as BaseUOMQty
FROM            InventoryMaster INNER JOIN
                  LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
WHERE(SkuRawMaterialMaster.Atc_id = @ATCID)
GROUP BY LocationMaster.LocationPrefix, InventoryMaster.SkuDet_Pk, LocationMaster.LocType, InventoryMaster.Uom_Pk, SkuRawMaterialMaster.Uom_PK
HAVING(SUM(InventoryMaster.OnhandQty) > 0)";


                if (qrtype == "F")
                {
                    cmd.CommandText = cmd.CommandText + "AND(LocationMaster.LocType = N'F')";
                }
                else if (qrtype == "W")
                {
                    cmd.CommandText = cmd.CommandText + "AND(LocationMaster.LocType = N'W')";
                }

                //
                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                foreach (System.Data.DataColumn col in dt.Columns) col.ReadOnly = false;

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    float poQty = float.Parse(dt.Rows[i]["OnhandQty"].ToString().Trim());

                    try
                    {
                        int uompk = int.Parse(dt.Rows[i]["Uom_PK"].ToString());
                        int baseuompk = int.Parse(dt.Rows[i]["BaseUomPK"].ToString());


                        if (uompk == baseuompk)
                        {
                            poQty = poQty;
                        }
                        else
                        {
                            poQty = UOMConvertortoAlt(baseuompk, uompk, poQty);
                        }
                    }
                    catch (Exception)
                    {

                       
                    }
                    dt.Rows[i]["BaseUOMQty"] = poQty;


                }

            }
            return dt;
        }

        public static System.Data.DataTable GetOurStyleConsumption(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        SkuRawmaterialDetail.SkuDet_PK, StyleCostingDetails.Consumption, StyleCostingMaster.OurStyleID, AtcDetails.OurStyle
FROM            SkuRawMaterialMaster INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         SkuRawmaterialDetail ON StyleCostingDetails.Sku_PK = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         AtcDetails ON StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID
WHERE        (StyleCostingMaster.IsApproved = N'A') AND (SkuRawMaterialMaster.Atc_id = @ATCID) AND (StyleCostingDetails.IsRequired = N'Y')";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }


        public static System.Data.DataTable GetAtcDetails(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        AtcMaster.HouseDate, SUM(AtcDetails.Quantity) AS Qty
FROM            AtcMaster INNER JOIN
                         AtcDetails ON AtcMaster.AtcId = AtcDetails.AtcId
GROUP BY AtcMaster.HouseDate, AtcMaster.AtcId
HAVING        (AtcMaster.AtcId = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }


        public static System.Data.DataTable GetCutOrderDetails(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        CutOrderMaster.Cut_NO, CutOrderMaster.FabQty, LocationMaster.LocationPrefix, CutOrderMaster.SkuDet_pk
FROM            CutOrderMaster INNER JOIN
                         LocationMaster ON CutOrderMaster.ToLoc = LocationMaster.Location_PK
WHERE        (CutOrderMaster.AtcID = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }


        public static System.Data.DataTable GetADNDetails(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT DocMaster.DocNum, DocMaster.ContainerNum, DocDetails.Qty, ProcurementMaster.PONum, ProcurementDetails.SkuDet_PK, DocMaster.ADNType, DocDetails.ExtraQty,DocMaster.BOENum,
DocMaster.AddedDate
FROM            DocMaster INNER JOIN
                         DocDetails ON DocMaster.Doc_Pk = DocDetails.Doc_Pk INNER JOIN
                         ProcurementDetails ON DocDetails.PODet_Pk = ProcurementDetails.PODet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk
WHERE        (ProcurementMaster.AtcId = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }

        public static System.Data.DataTable GetPlanningRemark(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        PlaningRemarkMaster.SkuDet_PK, PlaningRemarkMaster.Remark, PlaningRemarkMaster.AddedDate, SkuRawMaterialMaster.Atc_id, PlaningRemarkMaster.AddedBy, PlaningRemarkMaster.PlanRemark_PK
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         PlaningRemarkMaster ON SkuRawmaterialDetail.SkuDet_PK = PlaningRemarkMaster.SkuDet_PK
WHERE(SkuRawMaterialMaster.Atc_id = @ATCID)  AND (PlaningRemarkMaster.IsDeleted = 'N')";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }


        public static System.Data.DataTable GetInventorymisplacedData(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        SUM(InventoryMissingDetails.Qty) AS Qty, InventoryMaster.SkuDet_Pk, InventoryMaster.Uom_Pk
FROM            InventoryMissingDetails INNER JOIN
                         InventoryMissingRequest ON InventoryMissingDetails.MisplaceApp_PK = InventoryMissingRequest.MisplaceApp_pk INNER JOIN
                         InventoryMaster ON InventoryMissingDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK
GROUP BY InventoryMaster.SkuDet_Pk, InventoryMissingRequest.Atc_id, InventoryMissingRequest.IsApproved, InventoryMaster.Uom_Pk
HAVING(InventoryMissingRequest.Atc_id = @ATCID) AND(InventoryMissingRequest.IsApproved = N'Y')";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }

        public static System.Data.DataTable GetROOutQtyData(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        RoInDetails.RoQty, InventoryMaster.Uom_Pk, RoInDetails.FromSkuDet_PK as SkuDet_PK, SkuRawMaterialMaster.Atc_id
FROM            RoInDetails INNER JOIN
                         InventoryMaster ON RoInDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
WHERE        (SkuRawMaterialMaster.Atc_id = @Param1)";



                cmd.Parameters.AddWithValue("Param1", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }



        public static System.Data.DataTable GetASQDetails(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        PoPackMaster.PoPackId, POPackDetails.ColorName, POPackDetails.SizeName, POPackDetails.PoQty, PoPackMaster.AtcId, StyleSize.Orderof
FROM            PoPackMaster INNER JOIN
                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
                         StyleSize ON POPackDetails.OurStyleID = StyleSize.OurStyleID AND POPackDetails.SizeName = StyleSize.SizeName
WHERE        (PoPackMaster.AtcId = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }


        /// <summary>
        /// get asqdetails with style atc location and season
        /// </summary>
        /// <param name="ATCID"></param>
        /// <returns></returns>
        public static System.Data.DataTable GetASQDetailsWithSeasonAndLocationandStyle(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        PoPackMaster.PoPackId, POPackDetails.ColorName, POPackDetails.SizeName, POPackDetails.PoQty, PoPackMaster.AtcId, PoPackMaster.ExpectedLocation_PK, PoPackMaster.SeasonName, 
                         POPackDetails.OurStyleID, StyleSize.Orderof
FROM            PoPackMaster INNER JOIN
                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
                         StyleSize ON POPackDetails.OurStyleID = StyleSize.OurStyleID AND POPackDetails.SizeName = StyleSize.SizeName
WHERE        (PoPackMaster.AtcId = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }

        /// <summary>
        /// Create the Size color Matrix from the popackdetails provided  without query
        /// </summary>
        /// <param name="AsqData"></param>
        /// <param name="popackid"></param>
        /// <returns></returns>
        public static DataTable createdatatable(DataTable AsqData)
        {


            DataTable UniqueSizeunsorted = AsqData.DefaultView.ToTable(true, "SizeName", "Orderof");
          

            DataView dv = UniqueSizeunsorted.DefaultView;
            dv.Sort = "Orderof ASC";
            DataTable UniqueSize = dv.ToTable();


            UniqueSize = UniqueSize.DefaultView.ToTable(true, "SizeName");
            DataTable UniqueColor = AsqData.DefaultView.ToTable(true, "ColorName");
            DataTable dt = new DataTable();

            dt.Columns.Add("Color", typeof(String));


            for (int i = 0; i < UniqueSize.Rows.Count; i++)
            {
                dt.Columns.Add(UniqueSize.Rows[i][0].ToString(), typeof(String));
            }



            for (int i = 0; i < UniqueColor.Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < dt.Columns.Count; j++)
                {


                    if (j == 0)
                    {
                        dt.Rows[i][j] = UniqueColor.Rows[i][0].ToString();
                    }
                    else
                    {
                        try
                        {
                            object poqty = AsqData.Compute("Sum(PoQty)", "ColorName= '" + dt.Rows[i][0].ToString() + "' and  SizeName ='" + dt.Columns[j].ColumnName.ToString() + "'");

                            if (poqty.ToString().Trim() == "")
                            {
                                poqty = "0";
                            }

                            dt.Rows[i][j] = poqty.ToString();
                        }
                        catch
                        {

                        }

                    }

                }



            }


            return addColumntotal(dt);
        }







        /// <summary>
        /// Create the Size color Matrix from the popackdetails provided  without query
        /// </summary>
        /// <param name="AsqData"></param>
        /// <param name="popackid"></param>
        /// <returns></returns>
        public static DataTable createdatatable(DataTable AsqData,int popackid,int ourstyleid)
        {
             AsqData = AsqData.Select("PoPackId="+ popackid + "and  OurStyleID="+ ourstyleid+"").CopyToDataTable();

            DataTable UniqueSizeunsorted = AsqData.DefaultView.ToTable(true, "SizeName", "Orderof");


            DataView dv = UniqueSizeunsorted.DefaultView;
            dv.Sort = "Orderof ASC";
            DataTable UniqueSize = dv.ToTable();


            UniqueSize = UniqueSize.DefaultView.ToTable(true, "SizeName");
            DataTable UniqueColor = AsqData.DefaultView.ToTable(true, "ColorName");
            DataTable dt = new DataTable();

            dt.Columns.Add("Color", typeof(String));


            for (int i = 0; i < UniqueSize.Rows.Count; i++)
            {
                dt.Columns.Add(UniqueSize.Rows[i][0].ToString(), typeof(String));
            }



            for (int i = 0; i < UniqueColor.Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < dt.Columns.Count; j++)
                {


                    if (j == 0)
                    {
                        dt.Rows[i][j] = UniqueColor.Rows[i][0].ToString();
                    }
                    else
                    {
                        try
                        {
                            object poqty = AsqData.Compute("Sum(PoQty)", "ColorName= '" + dt.Rows[i][0].ToString() + "' and  SizeName ='" + dt.Columns[j].ColumnName.ToString() + "'");

                            if (poqty.ToString().Trim() == "")
                            {
                                poqty = "0";
                            }

                            dt.Rows[i][j] = poqty.ToString();
                        }
                        catch
                        {

                        }

                    }

                }



            }


            return addColumntotal(dt);
        }







        public static DataTable addColumntotal(DataTable dt)
        {
            dt.Columns.Add("ColorTotal", typeof(String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                float rowsum = 0;

                for (int j = 1; j < dt.Columns.Count - 1; j++)
                {

                    rowsum = rowsum + float.Parse(dt.Rows[i][j].ToString());

                }

                dt.Rows[i]["ColorTotal"] = rowsum.ToString();
            }





            return addRowtotal(dt);
        }


        public static DataTable addRowtotal(DataTable dt)
        {
            DataRow row1 = dt.NewRow();
            row1[0] = "SizeTotal";
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                row1[i] = 0;
            }

            dt.Rows.Add(row1);



            for (int j = 1; j < dt.Columns.Count; j++)
            {
                float colsum = 0;
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    colsum = colsum + float.Parse(dt.Rows[i][j].ToString());

                }
                dt.Rows[dt.Rows.Count - 1][j] = colsum.ToString();
            }








            return dt;
        }







    }
}