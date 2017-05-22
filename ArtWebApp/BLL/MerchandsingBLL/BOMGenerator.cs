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
    public static class BOMGenerator
    {
        static float garqty = 0;
        static  String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        //   public static DataTable CalculateRequiredPOIssued(DataTable dt,int atcid)
        //   {
        //       DataTable skudata = GetSKUData(atcid);
        //       DataTable EBOMData = GetEBOMData(atcid);
        //       DataTable POData = GetPOData(atcid);
        //       DataTable WPOData = GetWPOData(atcid);




        //  DBTransaction.BOMTransaction bomtrans = new DBTransaction.BOMTransaction();
        //       for (int i = 0; i < dt.Rows.Count; i++)
        //       {
        //           float requiredqty = 0;
        //           int extrabomqty = 0;
        //           int posiisued = 0;
        //           int wrongpoissued = 0;


        //           int balanceqty = 0;

        //           int skudetpk = int.Parse(dt.Rows[i]["SkuDet_PK"].ToString().Trim());

        //           if(skudetpk== 56771)
        //           {
        //               int k = 9;
        //           }
        //           int uom_pk = int.Parse(dt.Rows[i]["uom_pk"].ToString().Trim());
        //           String isCD = dt.Rows[i]["IsCD"].ToString().Trim();
        //           String isSD = dt.Rows[i]["IsSD"].ToString().Trim();
        //           String isCM = dt.Rows[i]["isCommon"].ToString().Trim();
        //           String IsGD = dt.Rows[i]["IsGD"].ToString().Trim();



        //           int reqty = (int)Math.Round(requiredQtyCalculate(skudetpk, isCD, isSD, isCM, IsGD, skudata), 0);

        //           try
        //           {
        //               int ordermin = (int)Math.Round(float.Parse(dt.Rows[i]["OrderMin"].ToString()));
        //               if (reqty < ordermin)
        //               {
        //                   reqty = ordermin;
        //               }

        //           }
        //           catch (Exception)
        //           {


        //           }
        //           try
        //           {
        //               extrabomqty = (int)Math.Round(GetExtraBOMRequest(skudetpk, EBOMData));
        //       }
        //           catch (Exception)
        //       {


        //       }
        //           try
        //           {
        //               posiisued = (int)Math.Round(GetPoIssuedQtyinBaseUOM(skudetpk, uom_pk, POData), 0);
        //   }
        //           catch (Exception)
        //           {


        //           }
        //           try
        //           {
        //               wrongpoissued = (int)Math.Round(GetWrongPoIssuedQtyinBaseUOMwithApproval(skudetpk, uom_pk,WPOData), 0);
        //    }
        //           catch (Exception)
        //           {


        //           }
        //           try
        //           {
        //               balanceqty = (reqty - posiisued) + wrongpoissued + extrabomqty;

        //}
        //           catch (Exception)
        //           {


        //           }

        //           dt.Rows[i]["RqdQty"] = reqty;
        //           dt.Rows[i]["PoIssuedQty"] = posiisued;
        //           dt.Rows[i]["BalanceQty"] = balanceqty;


        //       }
        //       return dt;
        //   }
        //public static float requiredQtyCalculate(int skudetpk, String isCD, String isSD, String isCM, String IsGD, DataTable skudata)
        //{
        //    float requiredqty = 0;
        //    if (isCM == "Y" && isCD == "N" && isSD == "N" && IsGD == "N")
        //    {
        //        try
        //        {
        //            DataTable newresult = skudata.Select("SkuDet_PK = " + skudetpk + "").CopyToDataTable();

        //        var sum = newresult.Compute("SUM(PoQty)", "");
        //        var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
        //        var wastage = newresult.Compute("MAX(WastagePercentage)", "");

        //        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

        //        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


        //        requiredqty = requiredqty + wastageqty;
        //    }
        //        catch (Exception)
        //    {


        //    }
        //}
        //    else if (isCM == "N" && isCD == "Y" && isSD == "N" && IsGD == "N")
        //    {

        //        try
        //        {
        //            DataTable newresult = skudata.Select("SkuDet_PK = " + skudetpk + " and ColorCode =SKUColorCode").CopyToDataTable();

        //            var sum = newresult.Compute("SUM(PoQty)", "");
        //            var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
        //            var wastage = newresult.Compute("MAX(WastagePercentage)", "");

        //            requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

        //            float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


        //            requiredqty = requiredqty + wastageqty;
        //        }
        //        catch (Exception)
        //        {


        //        }

        //    }
        //    else if (isCM == "N" && isCD == "N" && isSD == "Y" && IsGD == "N")
        //    {
        //        try
        //        {
        //            DataTable newresult = skudata.Select("SkuDet_PK = " + skudetpk + " and SIzeCode =SKUSizeCode").CopyToDataTable();

        //        var sum = newresult.Compute("SUM(PoQty)", "");
        //        var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
        //        var wastage = newresult.Compute("MAX(WastagePercentage)", "");

        //        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

        //        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


        //        requiredqty = requiredqty + wastageqty;
        //        }
        //        catch (Exception)
        //        {


        //        }
        //    }
        //    else if (isCM == "N" && isCD == "Y" && isSD == "Y" && IsGD == "N")
        //    {
        //            try
        //            {
        //                DataTable newresult = skudata.Select("SkuDet_PK = " + skudetpk + " and SIzeCode =SKUSizeCode and ColorCode =SKUColorCode").CopyToDataTable();

        //        var sum = newresult.Compute("SUM(PoQty)", "");
        //        var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
        //        var wastage = newresult.Compute("MAX(WastagePercentage)", "");

        //        requiredqty = float.Parse((float.Parse(sum.ToString()) * float.Parse(CONSUMPTION.ToString())).ToString());

        //        float wastageqty = requiredqty * (float.Parse(wastage.ToString()) / 100);


        //        requiredqty = requiredqty + wastageqty;
        //        }
        //        catch (Exception exp)
        //        {


        //        }
        //    }
        //    else if (IsGD == "Y")
        //    {
        //        // if ggroup dependa

        //        try
        //        {
        //            if (isCM == "Y" && isCD == "N" && isSD == "N" && IsGD == "Y")
        //        {
        //            requiredqty = GroupDependantCommonQty(skudetpk);
        //        }
        //        else if (isCM == "N" && isCD == "Y" && isSD == "N" && IsGD == "Y")
        //        {
        //            requiredqty = GroupDependantColorQty(skudetpk);

        //        }
        //        else if (isCM == "N" && isCD == "N" && isSD == "Y" && IsGD == "Y")
        //        {
        //            requiredqty = GroupDependantSizeQty(skudetpk);

        //        }
        //        else if (isCM == "N" && isCD == "Y" && isSD == "Y" && IsGD == "Y")
        //        {
        //            requiredqty = GroupDependantSizeandColorQty(skudetpk);

        //        }
        //        }
        //        catch (Exception)
        //        {


        //        }
        //    }

        //    return requiredqty;
        //}


        public static DataTable CalculateRequiredPOIssued(DataTable dt, int atcid)
        {
            DataTable skudata = GetSKUData(atcid);
            DataTable EBOMData = GetEBOMData(atcid);
            DataTable POData = GetPOData(atcid);
            DataTable WPOData = GetWPOData(atcid);
            DataTable MissedData = GetInventorymisplacedData(atcid);
            DataTable RoOutQtyData = GetROOutQtyData(atcid);
            DataView ourstyleview = new DataView(skudata);
            DataTable distinctOurstyleData = ourstyleview.ToTable(true, "OurStyleID");



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

                if (skudetpk == 57591)
                {
                    int k = 9;
                }
                int uom_pk = int.Parse(dt.Rows[i]["uom_pk"].ToString().Trim());
                String isCD = dt.Rows[i]["IsCD"].ToString().Trim();
                String isSD = dt.Rows[i]["IsSD"].ToString().Trim();
                String isCM = dt.Rows[i]["isCommon"].ToString().Trim();
                String IsGD = dt.Rows[i]["IsGD"].ToString().Trim();









                //int reqty = (int)Math.Round(requiredQtyCalculate(skudetpk, isCD, isSD, isCM, IsGD, skudata), 0);
                int reqty = (int)Math.Round(requiredQtyCalculate(skudetpk, isCD, isSD, isCM, IsGD, skudata, distinctOurstyleData), 0);
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
                    balanceqty = (reqty - posiisued) + wrongpoissued + extrabomqty+ missedqty+roqty;

                }
                catch (Exception)
                {


                }

                dt.Rows[i]["RqdQty"] = reqty;
                dt.Rows[i]["PoIssuedQty"] = posiisued;
                dt.Rows[i]["BalanceQty"] = balanceqty;
                dt.Rows[i]["GarmentQty"] = garmentqty;



            }
            return dt;
        }

        public static float requiredQtyCalculate(int skudetpk, String isCD, String isSD, String isCM, String IsGD, DataTable skudata, DataTable ourstyles)
        {
            float requiredqty = 0;

            garqty = 0;


            if (isCM == "Y" && isCD == "N" && isSD == "N" && IsGD == "N")
            {
                try
                {


                    for (int i = 0; i < ourstyles.Rows.Count; i++)
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










        public static float GetExtraBOMRequest(int skudetpk, DataTable EBomData)

        { 
            float extraqty = 0;
            try
            {
                var sum = EBomData.Compute("SUM(ExtraQty)", "Skudet_PK=" + skudetpk + "");
                extraqty = float.Parse(sum.ToString());
            }
            catch (Exception)
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

        public static float GetPoIssuedQtyinBaseUOM(int skudetpk, int baseuom_pk,DataTable POData)
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






        public static System.Data.DataTable GetSKUData(int ATCID)
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
WHERE        (StyleCostingMaster.IsApproved = N'A') AND (SkuRawMaterialMaster.Atc_id = @ATCID) AND (StyleCostingDetails.IsRequired = N'Y')";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
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



    }









}