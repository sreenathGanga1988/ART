using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using ArtWebApp.DataModels;
namespace ArtWebApp.BLL.MerchandsingBLL
{
   

    public class AtcChart
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        public DataTable GetAtcChart(int atcid)
        {
            DataTable BomData = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {

               





                cmd.CommandText = @"GetAtcChart_SP";

                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.CommandType = CommandType.StoredProcedure;
                BomData = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
                if (BomData.Rows.Count <= 0)
                {

                }
                else
                {
                    foreach (System.Data.DataColumn col in BomData.Columns) col.ReadOnly = false;
                    CalculateRequiredPOIssued(BomData);

                }
                return BomData;


            }

        }







        public void CalculateRequiredPOIssued(DataTable dt)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int skudetpk = int.Parse(dt.Rows[i]["SkuDet_PK"].ToString().Trim());

                int uom_pk = int.Parse(dt.Rows[i]["uom_pk"].ToString().Trim());
                String isCD = dt.Rows[i]["IsCD"].ToString().Trim();
                String isSD = dt.Rows[i]["IsSD"].ToString().Trim();
                String isCM = dt.Rows[i]["isCommon"].ToString().Trim();

                //if(skudetpk==30249)
                //{
                //    int k = 0;
                //}

                int reqty = (int)Math.Round(requiredqtycalculator(skudetpk, isCD, isSD, isCM), 0);


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


                int posiisued = (int)Math.Round(GetPoIssuedQtyinBaseUOM(skudetpk, uom_pk), 0);

                int balanceqty = reqty - posiisued;



                dt.Rows[i]["RqdQty"] = reqty;
                dt.Rows[i]["PoIssuedQty"] = posiisued;
                dt.Rows[i]["BalanceQty"] = balanceqty;


            }

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
        public float requiredqtycalculator(int skudetpk, String isCD, String isSD, String isCM)
        {
            float requiredqty = 0;


            if (skudetpk == 30298)
            {
                int k = 0;
            }

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
                else if (isCM == "N" && isCD == "Y" && isSD == "N")
                {
                    using (
























                        ArtEntitiesnew entty = new ArtEntitiesnew())
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
            }
            catch (Exception)
            {


            }


            return requiredqty;
        }



        /// <summary>
        /// Get all the po issued qty and convert them into
        /// baseuom
        /// </summary>
        /// <param name="skudetpk"></param>
        /// <param name="baseuom_pk"></param>
        /// <returns></returns>

        public float GetPoIssuedQtyinBaseUOM(int skudetpk, int baseuom_pk)
        {
            float poissuedqty = 0;
            DataTable dt = new DataTable();





            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        SUM(POQty) AS Poqty, Uom_PK
FROM            ProcurementDetails
GROUP BY Uom_PK, SkuDet_PK
HAVING        (SkuDet_PK = @Param1)
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
        public float UOMConvertortoAlt(int uomPK, int auomPk, float balqtyinBaseuom)
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

        public DataTable getAltuomdata(int baseuom_pk, int altuom_pk)
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



    }
}