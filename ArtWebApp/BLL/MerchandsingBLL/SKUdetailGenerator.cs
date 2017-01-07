using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.MerchandsingBLL
{
    public static class SKUdetailGenerator
    {





        public static void CreateSkUDetailsNew(int AtcID)
        {
            DataTable colocode = GetColorCode(AtcID);
            DataTable sizecodedata = GetSizeCode(AtcID);
            BLL.MerchandsingBLL.SKUManager skumgr = new BLL.MerchandsingBLL.SKUManager();
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {

                var q = from skumstr in entty.SkuRawMaterialMasters
                        where skumstr.Atc_id == AtcID
                        select new { skumstr.IsCD, skumstr.isCommon, skumstr.IsSD, skumstr.Sku_Pk, skumstr.Template_pk, skumstr.Rate, skumstr.IsGD };

                foreach (var rawmaterial in q)
                {
                    //Stopwatch stopWatch = new Stopwatch();
                    //stopWatch.Start();
                 
                  

                    //if item is color dependent
                    if (rawmaterial.IsCD.Trim() == "Y" && rawmaterial.IsSD.Trim() == "N" && rawmaterial.isCommon.Trim() == "N")
                    {
                        try
                        {

                            #region colordependant
                            DataTable tempColordatatable = colocode.Select("Sku_PK=" + rawmaterial.Sku_Pk.ToString()).CopyToDataTable();
                        DataView view = new DataView(tempColordatatable);
                        DataTable distinctcolorValues = view.ToTable(true, "ColorCode");
                        // get all the colornames from the PopackDetails
                     
                        for(int i=0;i< distinctcolorValues.Rows.Count;i++)
                        {
                            String colorcode = distinctcolorValues.Rows[i]["ColorCode"].ToString();
                            if (!entty.SkuRawmaterialDetails.Any(f => f.Sku_PK == rawmaterial.Sku_Pk && f.ColorCode == colorcode))
                            {
                                SkuRawmaterialDetail skudet = new SkuRawmaterialDetail();
                                skudet.Sku_PK = rawmaterial.Sku_Pk;
                                skudet.ColorCode = colorcode;
                                skudet.UnitRate = rawmaterial.Rate;
                                skudet.SizeCode = "";
                                skudet.Dependency = "CD";
                                entty.SkuRawmaterialDetails.Add(skudet);

                            }
                            else
                            {

                            }

                        }




                            #endregion
                        }
                        catch (Exception)
                        {

                        }
                    }
                    //if rawmaterial is Size Dependant
                    else if (rawmaterial.IsCD.Trim() == "N" && rawmaterial.IsSD.Trim() == "Y" && rawmaterial.isCommon.Trim() == "N")
                    {
                        try
                        {
                            #region Sizedependant

                            try
                        {
                            DataTable tempsizedata = sizecodedata.Select("Sku_PK=" + rawmaterial.Sku_Pk.ToString()).CopyToDataTable();
                            DataView sizeview = new DataView(tempsizedata);
                            DataTable distinctsizeValues = sizeview.ToTable(true, "SIzeCode");


                            for (int i = 0; i < distinctsizeValues.Rows.Count; i++)
                            {
                                String sizecodecode = distinctsizeValues.Rows[i]["SIzeCode"].ToString();
                                if (!entty.SkuRawmaterialDetails.Any(f => f.Sku_PK == rawmaterial.Sku_Pk && f.SizeCode == sizecodecode))
                                {
                                    SkuRawmaterialDetail skudet = new SkuRawmaterialDetail();
                                    skudet.Sku_PK = rawmaterial.Sku_Pk;
                                    skudet.SizeCode = sizecodecode;
                                    skudet.UnitRate = rawmaterial.Rate;
                                    skudet.ColorCode = "";
                                    skudet.Dependency = "SD";
                                    entty.SkuRawmaterialDetails.Add(skudet);
                                }
                                else
                                {

                                }
                            }
                        }
                        catch (Exception)
                        {

                           
                        }



                            #endregion
                        }
                        catch (Exception)
                        {

                        }
                    }
                    //if rawmaterial is Size Dependant and color dependant
                    else if (rawmaterial.IsCD.Trim() == "Y" && rawmaterial.IsSD.Trim() == "Y" && rawmaterial.isCommon.Trim() == "N")
                    {
                        try
                        {
                            #region ColorandSizeDependant


                            try
                        {
                            DataTable tempColordatatable = colocode.Select("Sku_PK=" + rawmaterial.Sku_Pk.ToString()).CopyToDataTable();
                            DataView view = new DataView(tempColordatatable);
                            DataTable distinctcolorValues = view.ToTable(true, "ColorCode");

                            DataTable tempsizedata = sizecodedata.Select("Sku_PK=" + rawmaterial.Sku_Pk.ToString()).CopyToDataTable();
                            DataView sizeview = new DataView(tempsizedata);
                            DataTable distinctsizeValues = sizeview.ToTable(true, "SIzeCode");


                            for (int i = 0; i < distinctcolorValues.Rows.Count; i++)
                            {
                                for (int j = 0; j < distinctsizeValues.Rows.Count; j++)
                                {
                                    String sizecodecode = distinctsizeValues.Rows[j]["SIzeCode"].ToString();
                                    String colorcode = distinctcolorValues.Rows[i]["ColorCode"].ToString();
                                    if (!entty.SkuRawmaterialDetails.Any(f => f.Sku_PK == rawmaterial.Sku_Pk && f.ColorCode == colorcode && f.SizeCode == sizecodecode))
                                    {
                                        SkuRawmaterialDetail skudet = new SkuRawmaterialDetail();
                                        skudet.Sku_PK = rawmaterial.Sku_Pk;
                                        skudet.SizeCode = sizecodecode;
                                        skudet.ColorCode = colorcode;

                                        skudet.UnitRate = rawmaterial.Rate;
                                        skudet.Dependency = "CSD";
                                        entty.SkuRawmaterialDetails.Add(skudet);

                                    }

                                }

                            }
                        }
                        catch (Exception)
                        {

                          
                        }







                            #endregion
                        }
                        catch (Exception)
                        {

                        }
                    }
                    //if rawmaterial is Common
                    else if (rawmaterial.IsCD.Trim() == "N" && rawmaterial.IsSD.Trim() == "N" && rawmaterial.isCommon.Trim() == "Y")
                    {

                       


                        try
                        {
                            #region Common


                            if (!entty.SkuRawmaterialDetails.Any(f => f.Sku_PK == rawmaterial.Sku_Pk))
                            {
                                SkuRawmaterialDetail skudet = new SkuRawmaterialDetail();
                                skudet.Sku_PK = rawmaterial.Sku_Pk;
                                skudet.SizeCode = "";
                                skudet.ColorCode = "";
                                skudet.Dependency = "CM";
                                skudet.UnitRate = rawmaterial.Rate;

                                entty.SkuRawmaterialDetails.Add(skudet);

                            }
                            else
                            {
                                try
                                {
                                    var asd = from skudet in entty.SkuRawmaterialDetails
                                              where skudet.Sku_PK == rawmaterial.Sku_Pk && skudet.Dependency != "CM"
                                              select skudet;
                                    foreach (var element in asd)
                                    {
                                        int skudet = int.Parse(element.SkuDet_PK.ToString());

                                        skumgr.DeleteSkuDetail(skudet);

                                    }
                                }
                                catch (Exception)
                                {


                                }



                            }

                            #endregion


                        }
                        catch (Exception)
                        {

                        }
                       



                }



                    

                }
                entty.SaveChanges();
            }
        }



        public static System.Data.DataTable GetColorCode(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @" SELECT DISTINCT POPackDetails.ColorCode, StyleCostingDetails.Sku_PK, AtcDetails.AtcId
FROM            StyleCostingMaster INNER JOIN
                         StyleCostingDetails ON StyleCostingMaster.Costing_PK = StyleCostingDetails.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID INNER JOIN
                         AtcDetails ON StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID
WHERE        (StyleCostingDetails.IsRequired = N'Y')
GROUP BY POPackDetails.ColorCode, StyleCostingDetails.Sku_PK, AtcDetails.AtcId
HAVING        (AtcDetails.AtcId = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }

        public static System.Data.DataTable GetSizeCode(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT DISTINCT POPackDetails.SIzeCode, StyleCostingDetails.Sku_PK, AtcDetails.AtcId
FROM            StyleCostingMaster INNER JOIN
                         StyleCostingDetails ON StyleCostingMaster.Costing_PK = StyleCostingDetails.Costing_PK INNER JOIN
                         POPackDetails ON StyleCostingMaster.OurStyleID = POPackDetails.OurStyleID INNER JOIN
                         AtcDetails ON StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID
WHERE        (StyleCostingDetails.IsRequired = N'Y')
GROUP BY POPackDetails.SIzeCode, StyleCostingDetails.Sku_PK, AtcDetails.AtcId
HAVING        (AtcDetails.AtcId = @Param1)";



                cmd.Parameters.AddWithValue("@Param1", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }

    }


      




}