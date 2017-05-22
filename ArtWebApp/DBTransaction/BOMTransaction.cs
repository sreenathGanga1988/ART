using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ArtWebApp.DataModels;
using System.Web.UI.WebControls;
namespace ArtWebApp.DBTransaction
{
    public class BOMTransaction
    {

        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        /// <summary>
        /// Get the BOM of an Atc
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public DataTable GetBOM(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


//                SqlCommand cmd = new SqlCommand(@"SELECT        SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
//                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ColorCode, 
//                         SkuRawmaterialDetail.SizeCode, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize, ISNULL
//                             ((SELECT        MAX(StyleCostingDetails.Rate) AS Expr1
//                                 FROM            StyleCostingDetails INNER JOIN
//                                                          StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK
//                                 GROUP BY StyleCostingDetails.Sku_PK, StyleCostingMaster.IsApproved
//                                 HAVING        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk)), SkuRawmaterialDetail.UnitRate) AS UnitRate, SkuRawmaterialDetail.RqdQty, 
//                         0000 AS PoIssuedQty, 0000 AS BalanceQty, SkuRawMaterialMaster.Uom_PK, SkuRawMaterialMaster.AltUom_pk, SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.isCommon, SkuRawMaterialMaster.IsCD, 
//                         SkuRawMaterialMaster.IsSD, UOMMaster.UomCode, SkuRawMaterialMaster.Template_pk, ISNULL(SkuRawMaterialMaster.OrderMin, 0) AS OrderMin, Template_Master.ItemGroup_PK
//FROM            SkuRawmaterialDetail INNER JOIN
//                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
//                         UOMMaster ON SkuRawMaterialMaster.AltUom_pk = UOMMaster.Uom_PK INNER JOIN
//                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
//WHERE        (SkuRawMaterialMaster.Atc_id = @atcid)
//ORDER BY   Template_Master.ItemGroup_PK, SkuRawMaterialMaster.RMNum
//", con);


                SqlCommand cmd = new SqlCommand(@"SELECT        SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ColorCode, 
                         SkuRawmaterialDetail.SizeCode, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize, ISNULL
                             ((SELECT        MAX(StyleCostingDetails.Rate) AS Expr1
                                 FROM            StyleCostingDetails INNER JOIN
                                                          StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK
                                 GROUP BY StyleCostingDetails.Sku_PK, StyleCostingMaster.IsApproved
                                 HAVING        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk)), SkuRawmaterialDetail.UnitRate) AS UnitRate,
								 
								 
								ISNULL ((SELECT        MAX(StyleCostingDetails.Consumption) AS Expr1
                                 FROM            StyleCostingDetails INNER JOIN
                                                          StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK
                                 GROUP BY StyleCostingDetails.Sku_PK, StyleCostingMaster.IsApproved
                                 HAVING        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk)), 0) AS Consumption ,SkuRawmaterialDetail.RqdQty, 
                         0000 AS PoIssuedQty, 0000 AS BalanceQty, SkuRawMaterialMaster.Uom_PK, SkuRawMaterialMaster.AltUom_pk, SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.isCommon, SkuRawMaterialMaster.IsCD, 
                         SkuRawMaterialMaster.IsSD, SkuRawMaterialMaster.IsGD,UOMMaster.UomCode, SkuRawMaterialMaster.Template_pk, ISNULL(SkuRawMaterialMaster.OrderMin, 0) AS OrderMin, Template_Master.ItemGroup_PK, 
                         SizeMaster.SizeName,00 AS GarmentQty
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON SkuRawMaterialMaster.AltUom_pk = UOMMaster.Uom_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK left outer JOIN
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






   











           public DataTable GetTemplatecolor()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"select DISTINCT TemplateColor  from TemplateColor order by  TemplateColor", con);


              

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


             public DataTable GetTemplateSize()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"select DISTINCT TemplateSize  from TemplateSize order by TemplateSize", con);


              

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        public DataTable GetBOMofAtcTemplate(int atcid,int temppk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ColorCode, 
                         SkuRawmaterialDetail.SizeCode, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize, SkuRawmaterialDetail.UnitRate, 
                         SkuRawmaterialDetail.RqdQty, 0000 AS PoIssuedQty, 0000 AS BalanceQty, SkuRawMaterialMaster.Uom_PK, SkuRawMaterialMaster.AltUom_pk, SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.isCommon, 
                         SkuRawMaterialMaster.IsCD, SkuRawMaterialMaster.IsSD, UOMMaster.UomCode, SkuRawMaterialMaster.Template_pk
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON SkuRawMaterialMaster.AltUom_pk = UOMMaster.Uom_PK
WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (SkuRawMaterialMaster.Template_pk = @temppk)
ORDER BY SkuRawMaterialMaster.RMNum", con);


                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@temppk", temppk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }















        /// <summary>
        /// calculate the required Qty of each SkudetPk
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <param name="isCD"></param>
        /// <param name="isSD"></param>
        /// <param name="isCM"></param>
        /// <param name="ColorCode"></param>
        /// <param name="SizeCode"></param>
        /// <returns></returns>
        public float requiredqtycalculator(int skudetpk ,String isCD,String isSD,String isCM)
        {
             float  requiredqty=0;


            

             try
             {
                 if (isCM == "Y" && isCD == "N" && isSD == "N")
                 {
                   
                     using (ArtEntitiesnew entty = new ArtEntitiesnew())
                     {
                         
                         entty.Configuration.AutoDetectChangesEnabled = false;
                         var query = from SkUD in entty.SkuRawmaterialDetails
                                     join SKUM in entty.SkuRawMaterialMasters on SkUD.Sku_PK equals SKUM.Sku_Pk
                                     join STYCD in entty.StyleCostingDetails on SKUM.Sku_Pk equals STYCD.Sku_PK
                                     join STYCM in entty.StyleCostingMasters on STYCD.Costing_PK equals STYCM.Costing_PK
                                     join PPKD in entty.POPackDetails on STYCM.OurStyleID equals PPKD.OurStyleID
                                     where SkUD.SkuDet_PK == skudetpk && STYCM.IsApproved=="A"
                                     select new { PPKD.PoQty, STYCD.Consumption,SKUM.WastagePercentage };

                         var sum = query.Select(c => c.PoQty).Sum();
                         var CONSUMPTION = query.Select(c => c.Consumption).Max();
                             var wastage = query.Select(c => c.WastagePercentage).DefaultIfEmpty(0).Max ();
                         requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                         float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                         requiredqty = requiredqty + wastageqty;

                     }
                 }
                 else if (isCM == "N" && isCD == "Y" && isSD == "N")
                 {
                     using (ArtEntitiesnew entty = new ArtEntitiesnew())
                     {
                         entty.Configuration.AutoDetectChangesEnabled = false;
                         var query = from SkUD in entty.SkuRawmaterialDetails
                                     join SKUM in entty.SkuRawMaterialMasters on SkUD.Sku_PK equals SKUM.Sku_Pk
                                     join STYCD in entty.StyleCostingDetails on SKUM.Sku_Pk equals STYCD.Sku_PK
                                     join STYCM in entty.StyleCostingMasters on STYCD.Costing_PK equals STYCM.Costing_PK
                                     join PPKD in entty.POPackDetails on STYCM.OurStyleID equals PPKD.OurStyleID
                                     where SkUD.SkuDet_PK == skudetpk && PPKD.ColorCode == SkUD.ColorCode && STYCM.IsApproved == "A"
                                     select new { PPKD.PoQty, STYCD.Consumption, SKUM.WastagePercentage };

                         var sum = query.Select(c => c.PoQty).Sum();
                         var CONSUMPTION = query.Select(c => c.Consumption).Max();
                         var wastage = query.Select(c => c.WastagePercentage).DefaultIfEmpty(0).Max();
                         requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());
                         float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                         requiredqty = requiredqty + wastageqty;


                     }
                 }
                 else if (isCM == "N" && isCD == "N" && isSD == "Y")
                 {
                     using (ArtEntitiesnew entty = new ArtEntitiesnew())
                     {
                         entty.Configuration.AutoDetectChangesEnabled = false;
                         var query = from SkUD in entty.SkuRawmaterialDetails
                                     join SKUM in entty.SkuRawMaterialMasters on SkUD.Sku_PK equals SKUM.Sku_Pk
                                     join STYCD in entty.StyleCostingDetails on SKUM.Sku_Pk equals STYCD.Sku_PK
                                     join STYCM in entty.StyleCostingMasters on STYCD.Costing_PK equals STYCM.Costing_PK
                                     join PPKD in entty.POPackDetails on STYCM.OurStyleID equals PPKD.OurStyleID
                                     where SkUD.SkuDet_PK == skudetpk && PPKD.SIzeCode == SkUD.SizeCode && STYCM.IsApproved == "A"
                                     select new { PPKD.PoQty, STYCD.Consumption, SKUM.WastagePercentage };

                         var sum = query.Select(c => c.PoQty).Sum();
                         var CONSUMPTION = query.Select(c => c.Consumption).Max();
                         var wastage = query.Select(c => c.WastagePercentage).DefaultIfEmpty(0).Max();
                         requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                         float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                         requiredqty = requiredqty + wastageqty;
                     }
                 }
                 else if (isCM == "N" && isCD == "Y" && isSD == "Y")
                 {
                     using (ArtEntitiesnew entty = new ArtEntitiesnew())
                     {
                         var query = from SkUD in entty.SkuRawmaterialDetails
                                     join SKUM in entty.SkuRawMaterialMasters on SkUD.Sku_PK equals SKUM.Sku_Pk
                                     join STYCD in entty.StyleCostingDetails on SKUM.Sku_Pk equals STYCD.Sku_PK
                                     join STYCM in entty.StyleCostingMasters on STYCD.Costing_PK equals STYCM.Costing_PK
                                     join PPKD in entty.POPackDetails on STYCM.OurStyleID equals PPKD.OurStyleID
                                     where SkUD.SkuDet_PK == skudetpk && PPKD.SIzeCode == SkUD.SizeCode && PPKD.ColorCode == SkUD.ColorCode && STYCM.IsApproved == "A"
                                     select new { PPKD.PoQty, STYCD.Consumption, SKUM.WastagePercentage };

                         var sum = query.Select(c => c.PoQty).Sum();
                         var CONSUMPTION = query.Select(c => c.Consumption).Max();
                         var wastage = query.Select(c => c.WastagePercentage).DefaultIfEmpty(0).Max();
                         requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                         float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                         requiredqty = requiredqty + wastageqty;
                     }
                 }
                else if (isCM == "N" && isCD == "Y" && isSD == "Y" )
                {
                    using (ArtEntitiesnew entty = new ArtEntitiesnew())
                    {
                        var query = from SkUD in entty.SkuRawmaterialDetails
                                    join SKUM in entty.SkuRawMaterialMasters on SkUD.Sku_PK equals SKUM.Sku_Pk
                                    join STYCD in entty.StyleCostingDetails on SKUM.Sku_Pk equals STYCD.Sku_PK
                                    join STYCM in entty.StyleCostingMasters on STYCD.Costing_PK equals STYCM.Costing_PK
                                    join PPKD in entty.POPackDetails on STYCM.OurStyleID equals PPKD.OurStyleID
                                    where SkUD.SkuDet_PK == skudetpk && PPKD.SIzeCode == SkUD.SizeCode && PPKD.ColorCode == SkUD.ColorCode && STYCM.IsApproved == "A"
                                    select new { PPKD.PoQty, STYCD.Consumption, SKUM.WastagePercentage };

                        var sum = query.Select(c => c.PoQty).Sum();
                        var CONSUMPTION = query.Select(c => c.Consumption).Max();
                        var wastage = query.Select(c => c.WastagePercentage).DefaultIfEmpty(0).Max();
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;
                    }
                }
            }
             catch (Exception)
             {
                 
                
             }


            return requiredqty;
        }



        /// <summary>
        /// calculate the required Qty of each SkudetPk
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <param name="isCD"></param>
        /// <param name="isSD"></param>
        /// <param name="isCM"></param>
        /// <param name="ColorCode"></param>
        /// <param name="SizeCode"></param>
        /// <returns></returns>
        public float requiredqtycalculator(int skudetpk, String isCD, String isSD, String isCM, String isGD )
        {
            float requiredqty = 0;




            try
            {
                if (isCM == "Y" && isCD == "N" && isSD == "N" && isGD == "N")
                {

                    using (ArtEntitiesnew entty = new ArtEntitiesnew())
                    {

                        entty.Configuration.AutoDetectChangesEnabled = false;
                        var query = from SkUD in entty.SkuRawmaterialDetails
                                    join SKUM in entty.SkuRawMaterialMasters on SkUD.Sku_PK equals SKUM.Sku_Pk
                                    join STYCD in entty.StyleCostingDetails on SKUM.Sku_Pk equals STYCD.Sku_PK
                                    join STYCM in entty.StyleCostingMasters on STYCD.Costing_PK equals STYCM.Costing_PK
                                    join PPKD in entty.POPackDetails on STYCM.OurStyleID equals PPKD.OurStyleID
                                    where SkUD.SkuDet_PK == skudetpk && STYCM.IsApproved == "A"
                                    select new { PPKD.PoQty, STYCD.Consumption, SKUM.WastagePercentage };

                        var sum = query.Select(c => c.PoQty).Sum();
                        var CONSUMPTION = query.Select(c => c.Consumption).Max();
                        var wastage = query.Select(c => c.WastagePercentage).DefaultIfEmpty(0).Max();
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;

                    }
                }
                else if (isCM == "N" && isCD == "Y" && isSD == "N" && isGD == "N")
                {
                    using (ArtEntitiesnew entty = new ArtEntitiesnew())
                    {
                        entty.Configuration.AutoDetectChangesEnabled = false;
                        var query = from SkUD in entty.SkuRawmaterialDetails
                                    join SKUM in entty.SkuRawMaterialMasters on SkUD.Sku_PK equals SKUM.Sku_Pk
                                    join STYCD in entty.StyleCostingDetails on SKUM.Sku_Pk equals STYCD.Sku_PK
                                    join STYCM in entty.StyleCostingMasters on STYCD.Costing_PK equals STYCM.Costing_PK
                                    join PPKD in entty.POPackDetails on STYCM.OurStyleID equals PPKD.OurStyleID
                                    where SkUD.SkuDet_PK == skudetpk && PPKD.ColorCode == SkUD.ColorCode && STYCM.IsApproved == "A"
                                    select new { PPKD.PoQty, STYCD.Consumption, SKUM.WastagePercentage };

                        var sum = query.Select(c => c.PoQty).Sum();
                        var CONSUMPTION = query.Select(c => c.Consumption).Max();
                        var wastage = query.Select(c => c.WastagePercentage).DefaultIfEmpty(0).Max();
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());
                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;


                    }
                }
                else if (isCM == "N" && isCD == "N" && isSD == "Y" && isGD == "N")
                {
                    using (ArtEntitiesnew entty = new ArtEntitiesnew())
                    {
                        entty.Configuration.AutoDetectChangesEnabled = false;
                        var query = from SkUD in entty.SkuRawmaterialDetails
                                    join SKUM in entty.SkuRawMaterialMasters on SkUD.Sku_PK equals SKUM.Sku_Pk
                                    join STYCD in entty.StyleCostingDetails on SKUM.Sku_Pk equals STYCD.Sku_PK
                                    join STYCM in entty.StyleCostingMasters on STYCD.Costing_PK equals STYCM.Costing_PK
                                    join PPKD in entty.POPackDetails on STYCM.OurStyleID equals PPKD.OurStyleID
                                    where SkUD.SkuDet_PK == skudetpk && PPKD.SIzeCode == SkUD.SizeCode && STYCM.IsApproved == "A"
                                    select new { PPKD.PoQty, STYCD.Consumption, SKUM.WastagePercentage };

                        var sum = query.Select(c => c.PoQty).Sum();
                        var CONSUMPTION = query.Select(c => c.Consumption).Max();
                        var wastage = query.Select(c => c.WastagePercentage).DefaultIfEmpty(0).Max();
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;
                    }
                }
                else if (isCM == "N" && isCD == "Y" && isSD == "Y" && isGD == "N")
                {
                    using (ArtEntitiesnew entty = new ArtEntitiesnew())
                    {
                        var query = from SkUD in entty.SkuRawmaterialDetails
                                    join SKUM in entty.SkuRawMaterialMasters on SkUD.Sku_PK equals SKUM.Sku_Pk
                                    join STYCD in entty.StyleCostingDetails on SKUM.Sku_Pk equals STYCD.Sku_PK
                                    join STYCM in entty.StyleCostingMasters on STYCD.Costing_PK equals STYCM.Costing_PK
                                    join PPKD in entty.POPackDetails on STYCM.OurStyleID equals PPKD.OurStyleID
                                    where SkUD.SkuDet_PK == skudetpk && PPKD.SIzeCode == SkUD.SizeCode && PPKD.ColorCode == SkUD.ColorCode && STYCM.IsApproved == "A"
                                    select new { PPKD.PoQty, STYCD.Consumption, SKUM.WastagePercentage };

                        var sum = query.Select(c => c.PoQty).Sum();
                        var CONSUMPTION = query.Select(c => c.Consumption).Max();
                        var wastage = query.Select(c => c.WastagePercentage).DefaultIfEmpty(0).Max();
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;
                    }
                }
                else if (isCM == "N" && isCD == "Y" && isSD == "Y" && isGD == "N")
                {
                    using (ArtEntitiesnew entty = new ArtEntitiesnew())
                    {
                        var query = from SkUD in entty.SkuRawmaterialDetails
                                    join SKUM in entty.SkuRawMaterialMasters on SkUD.Sku_PK equals SKUM.Sku_Pk
                                    join STYCD in entty.StyleCostingDetails on SKUM.Sku_Pk equals STYCD.Sku_PK
                                    join STYCM in entty.StyleCostingMasters on STYCD.Costing_PK equals STYCM.Costing_PK
                                    join PPKD in entty.POPackDetails on STYCM.OurStyleID equals PPKD.OurStyleID
                                    where SkUD.SkuDet_PK == skudetpk && PPKD.SIzeCode == SkUD.SizeCode && PPKD.ColorCode == SkUD.ColorCode && STYCM.IsApproved == "A"
                                    select new { PPKD.PoQty, STYCD.Consumption, SKUM.WastagePercentage };

                        var sum = query.Select(c => c.PoQty).Sum();
                        var CONSUMPTION = query.Select(c => c.Consumption).Max();
                        var wastage = query.Select(c => c.WastagePercentage).DefaultIfEmpty(0).Max();
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;
                    }
                }
                else if(isGD == "Y")
                {
                    // if ggroup dependa


                    if (isCM == "Y" && isCD == "N" && isSD == "N" && isGD == "Y")
                    {
                        requiredqty = GroupDependantCommonQty(skudetpk);
                    }
                    else if(isCM == "N" && isCD == "Y" && isSD == "N" && isGD == "Y")
                    {
                        requiredqty = GroupDependantColorQty(skudetpk);
                        
                    }
                    else if (isCM == "N" && isCD == "N" && isSD == "Y" && isGD == "Y")
                    {
                        requiredqty = GroupDependantSizeQty(skudetpk);

                    }
                    else if (isCM == "N" && isCD == "Y" && isSD == "Y" && isGD == "Y")
                    {
                        requiredqty = GroupDependantSizeandColorQty(skudetpk);

                    }

                }
            }
            catch (Exception)
            {


            }


            return requiredqty;
        }









        /// <summary>
        /// Group Dependant and Common
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <returns></returns>
        public float GroupDependantCommonQty(int skudetpk)
        {
            float requiredqty = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ISNULL(MAX(StyleCostingDetails.Consumption), 0) AS Consumption, ISNULL(SUM(POPackDetails.PoQty), 0) AS PoQty, ISNULL(AVG(SkuRawMaterialMaster.WastagePercentage), 0) AS WastagePercentage
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         GroupDependantItems ON SkuRawMaterialMaster.Sku_Pk = GroupDependantItems.Sku_PK INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID AND GroupDependantItems.POPackID = POPackDetails.POPackId
WHERE        (StyleCostingMaster.IsApproved = N'A')
GROUP BY SkuRawmaterialDetail.SkuDet_PK
HAVING        (SkuRawmaterialDetail.SkuDet_PK = @Param1)";


            cmd.Parameters.AddWithValue("@param1", skudetpk);

           

            DataTable dt= QueryFunctions.ReturnQueryResultDatatable(cmd);
            try
            {

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        var sum = dt.Rows[0]["PoQty"];
                        var CONSUMPTION = dt.Rows[0]["Consumption"];
                        var wastage = dt.Rows[0]["Consumption"];
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;

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
        public float GroupDependantColorQty(int skudetpk)
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
                         SkuRawmaterialDetail.ColorCode = POPackDetails.ColorCode
WHERE        (StyleCostingMaster.IsApproved = N'A')
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
                        var wastage = dt.Rows[0]["Consumption"];
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;

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
        public float GroupDependantSizeQty(int skudetpk)
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
                         SkuRawmaterialDetail.SizeCode = POPackDetails.SizeCode
WHERE        (StyleCostingMaster.IsApproved = N'A')
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
                        var wastage = dt.Rows[0]["Consumption"];
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;

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
        public float GroupDependantSizeandColorQty(int skudetpk)
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
                         SkuRawmaterialDetail.SizeCode = POPackDetails.SizeCode and     SkuRawmaterialDetail.ColorCode = POPackDetails.ColorCode
WHERE        (StyleCostingMaster.IsApproved = N'A')
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
                        var wastage = dt.Rows[0]["Consumption"];
                        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

                        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


                        requiredqty = requiredqty + wastageqty;

                    }
                }
            }
            catch (Exception)
            {


            }

            return requiredqty;
        }






        public DataTable SupplierColorAndSizeCalculator(int skudetpk)
        {
            

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"DECLARE @param1 VARCHAR(8000) 

set @param1=30252

DECLARE @COLOR VARCHAR(8000) 
SELECT @COLOR = COALESCE(@COLOR + ' / ', '') + TT.SupplierColor
FROM (SELECT     DISTINCT   SupplierColor
FROM            ProcurementDetails WHERE SkuDet_PK=@param1)TT
WHERE TT.SupplierColor IS NOT NULL


DECLARE @SIZE VARCHAR(8000) 
SELECT @SIZE = COALESCE(@SIZE + ' / ', '') + TT.SupplierSize
FROM (SELECT     DISTINCT   SupplierSize
FROM            ProcurementDetails WHERE SkuDet_PK=@param1)TT
WHERE TT.SupplierSize IS NOT NULL



SELECT isnull(@COLOR,'') as supcolor,isnull(@SIZE,'') as supsize";


            cmd.Parameters.AddWithValue("@param1", skudetpk);

            return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }

   


       







        /// <summary>
        /// check whether Po is Given for a Skudetpk
        /// </summary>
        /// <param name="skudetPK"></param>
        /// <returns></returns>
        public Boolean isPOGiven(int skudetPK)
        {
            Boolean isPOGiven = false;
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {



                if (!entty.ProcurementDetails.Any(f => f.SkuDet_PK == skudetPK))
                {
                    isPOGiven = false;
                }
                else
                {
                    isPOGiven = true;
                }

            }
            return isPOGiven;
        }


        //public Boolean isPOGivenApproved(int skudetPK)
        //{
        //    Boolean isPOGiven = false;
        //    using (ArtEntitiesnew entty = new ArtEntitiesnew())
        //    {
        //        if (!entty.ProcurementDetails.Any(f => f.SkuDet_PK == skudetPK && ))
        //        {
        //            isPOGiven = false;
        //        }
        //        else
        //        {
        //            isPOGiven = true;
        //        }

        //    }
        //    return isPOGiven;
        //}



        /// <summary>
        /// update item and Supplier sizes and color for a skudet_pk
        /// </summary>
        /// <param name="drp_itemcolor"></param>
        /// <param name="drp_itemsize"></param>
        /// <param name="skudet_pk"></param>
        public void updateitemcolorandSize(DropDownList drp_itemcolor, DropDownList drp_itemsize, int skudet_pk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from skudet in entty.SkuRawmaterialDetails
                        where skudet.SkuDet_PK == skudet_pk
                        select skudet;
                foreach(var element in q)
                {
                    element.ItemColor = (drp_itemcolor.Text.Trim() == "") ? "" : drp_itemcolor.SelectedItem.Text.Trim();
                    element.SupplierColor = (drp_itemcolor.Text.Trim() == "") ? "" : drp_itemcolor.SelectedItem.Text.Trim();
                    element.ItemSize = (drp_itemsize.Text.Trim() == "") ? "" : drp_itemsize.SelectedItem.Text.Trim();
                    element.SupplierSize = (drp_itemsize.Text.Trim() == "") ? "" : drp_itemsize.SelectedItem.Text.Trim();
                }
                entty.SaveChanges();
            }
        }





        public void updateitemcolor(String Itemcolor, int skudet_pk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from skudet in entty.SkuRawmaterialDetails
                        where skudet.SkuDet_PK == skudet_pk
                        select skudet;
                foreach (var element in q)
                {
                    element.ItemColor = (Itemcolor.Trim() == "") ? "" : Itemcolor.Trim();
                    element.SupplierColor = (Itemcolor.Trim() == "") ? "" : Itemcolor.Trim();
                  
                }
                entty.SaveChanges();
            }
        }
        public void updateitemSize(String Itemsize, int skudet_pk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from skudet in entty.SkuRawmaterialDetails
                        where skudet.SkuDet_PK == skudet_pk
                        select skudet;
                foreach (var element in q)
                {
                    element.ItemSize = (Itemsize.Trim() == "") ? "" : Itemsize.Trim();
                    element.SupplierSize = (Itemsize.Trim() == "") ? "" : Itemsize.Trim(); ;

                }
                entty.SaveChanges();
            }
        }







        public DataTable GetDetailforRO(int atcid, int temp_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.OnhandQty, ProcurementDetails_1.POUnitRate, 
                         SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.Template_pk, LocationMaster.LocationPrefix
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         ProcurementDetails AS ProcurementDetails_1 ON InventoryMaster.PoDet_PK = ProcurementDetails_1.PODet_PK INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK
WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (SkuRawMaterialMaster.Template_pk = @temp_pk)
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode", con);
                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@loctn_pk", temp_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        /// <summary>
        /// Get all the po issued qty and convert them into
        /// baseuom
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <param name="baseuom_pk"></param>
        /// <returns></returns>

        public float GetPoIssuedQtyinBaseUOM(int skudetpk,int baseuom_pk )
        {
            float poissuedqty = 0;
            DataTable dt=new DataTable ();
            BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData POmstr = new BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData();




            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        SUM(ProcurementDetails.POQty) AS Poqty, ProcurementDetails.Uom_PK
FROM            ProcurementDetails INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk
						 WHERE   (ProcurementMaster.IsDeleted <> N'Y')
GROUP BY ProcurementDetails.Uom_PK, ProcurementDetails.SkuDet_PK
HAVING        (ProcurementDetails.SkuDet_PK = @Param1)
", con);


                cmd.Parameters.AddWithValue("@Param1", skudetpk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);
            }


            if (dt != null)
            {
                if(dt.Rows.Count>0)
                {

                    for(int i=0;i<dt.Rows.Count;i++)
                    {
                        if(dt.Rows[i]["Uom_PK"].ToString().Trim ()==baseuom_pk.ToString().Trim ())
                        {
                            poissuedqty = poissuedqty + float.Parse(dt.Rows[i]["POQty"].ToString());
                        }
                        else
                        {
                            poissuedqty = poissuedqty + POmstr.UOMConvertortoAlt(baseuom_pk, int.Parse(dt.Rows[i]["Uom_PK"].ToString()), float.Parse(dt.Rows[i]["POQty"].ToString()));
                        }
                    }
                }
            }
            
            
            
            
            
            
            
            
            
            
            
            
          
            return poissuedqty;
        }


        /// <summary>
        /// get the wrong PO approved nd add it to BOM
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <param name="baseuom_pk"></param>
        /// <returns></returns>
        public float GetWrongPoIssuedQtyinBaseUOMwithApproval(int skudetpk, int baseuom_pk)
        {
            float poissuedqty = 0;
            DataTable dt = new DataTable();
            BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData POmstr = new BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData();




            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        ProcurementDetails.Uom_PK, SUM(WrongPODetail.Qty) AS POQty
FROM            ProcurementDetails INNER JOIN
                         WrongPODetail ON ProcurementDetails.PODet_PK = WrongPODetail.Podet_PK INNER JOIN
                         WrongPOMaster ON WrongPODetail.WrongPO_Pk = WrongPOMaster.WrongPO_Pk
GROUP BY ProcurementDetails.Uom_PK, ProcurementDetails.SkuDet_PK, WrongPOMaster.IsApproved
HAVING        (ProcurementDetails.SkuDet_PK = @Param1) AND (WrongPOMaster.IsApproved = N'Y')
", con);


                cmd.Parameters.AddWithValue("@Param1", skudetpk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);
            }


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
                            poissuedqty = poissuedqty + POmstr.UOMConvertortoAlt(baseuom_pk, int.Parse(dt.Rows[i]["Uom_PK"].ToString()), float.Parse(dt.Rows[i]["POQty"].ToString()));
                        }
                    }
                }
            }













            return poissuedqty;
        }





        /// <summary>
        /// Get all extrabomRequest of an SKUDETPK
        /// baseuom
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <param name="baseuom_pk"></param>
        /// <returns></returns>

        public float GetExtraBOMRequest(int skudetpk)
        {
            float poissuedqty = 0;
            DataTable dt = new DataTable();
            BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData POmstr = new BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData();




            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT      ISNULL(  SUM(ExtraBOMRequestDetail.Qty),0 ) as ExtraQty
FROM            ExtraBOMRequestMaster INNER JOIN
                         ExtraBOMRequestDetail ON ExtraBOMRequestMaster.ExtraBOM_PK = ExtraBOMRequestDetail.ExtraBOM_PK
GROUP BY ExtraBOMRequestMaster.IsApproved, ExtraBOMRequestDetail.Skudet_PK
HAVING        (ExtraBOMRequestMaster.IsApproved = N'Y') AND (ExtraBOMRequestDetail.Skudet_PK = @Param1)
", con);


                cmd.Parameters.AddWithValue("@Param1", skudetpk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);
            }


            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                       
                            poissuedqty = poissuedqty + float.Parse(dt.Rows[i]["ExtraQty"].ToString());
                       
                    }
                }
            }













            return poissuedqty;
        }












    }
}