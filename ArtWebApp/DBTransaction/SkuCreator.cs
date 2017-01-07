using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ArtWebApp.DataModels;
using System.Data.SqlClient;

namespace ArtWebApp.DBTransaction
{
    public class SkuCreator
    {String connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
       
        /// <summary>
        /// creates SKU master for Atc Based on raw material detail
        /// </summary>
        /// <param name="atcid"></param>
        public void CreateSkuMaster(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {

                var q = from rawmtrial in entty.AtcRawMaterialMasters
                        join atcmaster in entty.AtcMasters on rawmtrial.Atc_id equals atcmaster.AtcId
                        where rawmtrial.Atc_id == atcid
                        select new
                        {
                            rawmtrial.AtcRaw_PK,
                            rawmtrial.Template_PK,
                            rawmtrial.TempCode,
                            rawmtrial.TemplateCount,
                           atcmaster.AtcId,
                           atcmaster.AtcNum,
                           rawmtrial.IsGD,

                        };

                foreach (var element in q)
                {
                    int templatepk = int.Parse(element.Template_PK.ToString());
                    int atc_rawPK = int.Parse(element.AtcRaw_PK.ToString());
                    string templateCode = element.TempCode;
                    String Atcnum = element.AtcNum;
                    int atc_id = int.Parse(element.AtcId.ToString());
                    int templatecount = int.Parse(element.TemplateCount.ToString());
                    String rmnum = "";
                    if (templatecount > 0)
                    {
                        

                        var existingrawmaterialcount = (from skumstr in entty.SkuRawMaterialMasters
                                                where skumstr.Atc_id == atc_id && skumstr.AtcRaw_PK == atc_rawPK

                                                select skumstr).Count();
                        if (templatecount > int.Parse(existingrawmaterialcount.ToString()))
                        {


                            //if template is present in the skumaster
                            if (int.Parse(existingrawmaterialcount.ToString()) > 0)
                            {
                                //loop start with existing count+1
                                for (int j = existingrawmaterialcount + 1; j <= templatecount; j++)
                                {
                                    rmnum = Atcnum + templateCode + j.ToString();
                                    SkuRawMaterialMaster skumasterddata = new SkuRawMaterialMaster();
                                    skumasterddata.Atc_id = atc_id;
                                    skumasterddata.AtcRaw_PK = atc_rawPK;
                                    skumasterddata.RMNum = rmnum.Trim();
                                    skumasterddata.Template_pk = templatepk;
                                    skumasterddata.isCommon = "N";
                                    skumasterddata.IsCD = "N";
                                    skumasterddata.IsSD = "N";
                                    skumasterddata.IsGD = element.IsGD.Trim ();
                                    entty.SkuRawMaterialMasters.Add(skumasterddata);
                                }
                            }
                            //if template is not present in skumaster
                            else
                            {
                                //loopstarts from 1 
                                for (int j = 1; j <= templatecount; j++)
                                {

                                    rmnum = Atcnum + templateCode + j.ToString();
                                    SkuRawMaterialMaster skumasterddata = new SkuRawMaterialMaster();
                                    skumasterddata.Atc_id = atc_id;
                                    skumasterddata.AtcRaw_PK = atc_rawPK;
                                    skumasterddata.RMNum = rmnum.Trim();
                                    skumasterddata.Template_pk = templatepk;
                                    skumasterddata.isCommon = "N";
                                    skumasterddata.IsCD = "N";
                                    skumasterddata.IsSD = "N";
                                    skumasterddata.IsGD = element.IsGD.Trim();
                                    entty.SkuRawMaterialMasters.Add(skumasterddata);
                                }

                            }


                           
                           

                        }
                        else
                        {


                        }

                        




                    }
                }



                entty.SaveChanges();

            }


        }



        public DataSet getattributesforAtc(int atcid)
        {
            DataSet Attributedataset = new DataSet();

            DataTable Compositiondata = Attributedataset.Tables.Add("Compositiondata");
            DataTable Constructiondata = Attributedataset.Tables.Add("Constructiondata"); 
            DataTable Widthdata = Attributedataset.Tables.Add("Widthdata"); 
            DataTable Weightdata = Attributedataset.Tables.Add("Weightdata");
            DataTable UOMData = Attributedataset.Tables.Add("UOMData");

          
            
           
            
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand Compositioncommand = new SqlCommand(@"SELECT        TemplateComposition.Composition, TemplateComposition.Template_Pk, TemplateComposition.TemplateCom_Pk
FROM            TemplateComposition INNER JOIN
                         AtcRawMaterialMaster ON TemplateComposition.Template_Pk = AtcRawMaterialMaster.Template_PK
WHERE        (AtcRawMaterialMaster.Atc_id = @param1)", con);
                Compositioncommand.Parameters.AddWithValue("@param1", atcid);
                SqlDataReader compositionreader = Compositioncommand.ExecuteReader();
                Compositiondata.Load(compositionreader);
               



                SqlCommand Constructioncommand = new SqlCommand(@"SELECT        TemplateConstruction.Construct, TemplateConstruction.Template_Pk, TemplateConstruction.TemplateCon_Pk
FROM            AtcRawMaterialMaster INNER JOIN
                         TemplateConstruction ON AtcRawMaterialMaster.Template_PK = TemplateConstruction.Template_Pk
WHERE        (AtcRawMaterialMaster.Atc_id = @param1)", con);
                Constructioncommand.Parameters.AddWithValue("@param1", atcid);
                SqlDataReader constructdreader = Constructioncommand.ExecuteReader();
                Constructiondata.Load(constructdreader);



                SqlCommand Withcommand = new SqlCommand(@"SELECT        TemplateWidth.Width, TemplateWidth.Template_Pk, TemplateWidth.TemplateWidth_Pk
FROM            AtcRawMaterialMaster INNER JOIN
                         TemplateWidth ON AtcRawMaterialMaster.Template_PK = TemplateWidth.Template_Pk
WHERE        (AtcRawMaterialMaster.Atc_id = @param1)", con);
                Withcommand.Parameters.AddWithValue("@param1", atcid);
                SqlDataReader widthreader = Withcommand.ExecuteReader();
                Widthdata.Load(widthreader);

                SqlCommand weightcommand = new SqlCommand(@"SELECT        TemplateWeight.Weight, TemplateWeight.Template_Pk, TemplateWeight.TemplateWeight_Pk
FROM            AtcRawMaterialMaster INNER JOIN
                         TemplateWeight ON AtcRawMaterialMaster.Template_PK = TemplateWeight.Template_Pk
WHERE        (AtcRawMaterialMaster.Atc_id = @param1)", con);
                weightcommand.Parameters.AddWithValue("@param1", atcid);
                SqlDataReader weightreader = weightcommand.ExecuteReader();
                Weightdata.Load(weightreader);


                SqlCommand altuomcommand = new SqlCommand(@"SELECT        UOMMaster_1.Uom_PK,Template_Master.Template_PK, UOMMaster_1.UomCode, AtcRawMaterialMaster.Atc_id
FROM            Template_Master INNER JOIN
                         UOMMaster ON Template_Master.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         AltUOMMaster ON UOMMaster.Uom_PK = AltUOMMaster.Uom_PK INNER JOIN
                         UOMMaster AS UOMMaster_1 ON AltUOMMaster.AltUom_PK = UOMMaster_1.Uom_PK INNER JOIN
                         AtcRawMaterialMaster ON Template_Master.Template_PK = AtcRawMaterialMaster.Template_PK
WHERE        (AtcRawMaterialMaster.Atc_id = @Param1)
ORDER BY UOMMaster_1.UomCode", con);
                altuomcommand.Parameters.AddWithValue("@param1", atcid);
                SqlDataReader uomreader = altuomcommand.ExecuteReader();
                UOMData.Load(uomreader);



            }


            
 

            return Attributedataset;

        }




        public void CreateSkUDetails(int AtcID)
        {
            BLL.MerchandsingBLL.SKUManager skumgr = new BLL.MerchandsingBLL.SKUManager();
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
              
                var q = from skumstr in entty.SkuRawMaterialMasters
                        where skumstr.Atc_id == AtcID
                        select skumstr;
                
                foreach (var rawmaterial in q)
                {
                   

                    //if item is color dependent
                    if (rawmaterial.IsCD.Trim() == "Y" && rawmaterial.IsSD.Trim() == "N" && rawmaterial.isCommon.Trim() == "N")
                    {
                        // get all the colornames from the PopackDetails
                        var Colordetails = (from StyleCostingDetails in entty.StyleCostingDetails
                                            join POPackDetails in entty.POPackDetails on StyleCostingDetails.StyleCostingMaster.OurStyleID equals POPackDetails.OurStyleID
                                            where
                                         StyleCostingDetails.IsRequired.Trim() == "Y" && StyleCostingDetails.Sku_PK == rawmaterial.Sku_Pk
                                            select new
                                            {
                                                POPackDetails.ColorCode
                                                
                                            }).Distinct();

                        foreach(var color in Colordetails)
                        {
                            if (!entty.SkuRawmaterialDetails.Any(f => f.Sku_PK == rawmaterial.Sku_Pk && f.ColorCode ==color.ColorCode))
                            {
                                SkuRawmaterialDetail skudet = new SkuRawmaterialDetail();
                                skudet.Sku_PK = rawmaterial.Sku_Pk;
                                skudet.ColorCode = color.ColorCode;
                                skudet.UnitRate = rawmaterial.Rate;
                                skudet.SizeCode = "";
                                skudet.Dependency = "CD";
                                entty.SkuRawmaterialDetails.Add(skudet);
                                
                            }
                            else
                            {

                            }
                        }




                    }
                    //if rawmaterial is Size Dependant
                    else if (rawmaterial.IsCD.Trim() == "N" && rawmaterial.IsSD.Trim() == "Y" && rawmaterial.isCommon.Trim() == "N")
                    {
                        // get all the colornames from the PopackDetails
                        var sizedetails = (from StyleCostingDetails in entty.StyleCostingDetails
                                            join POPackDetails in entty.POPackDetails on StyleCostingDetails.StyleCostingMaster.OurStyleID equals POPackDetails.OurStyleID
                                            where
                                             StyleCostingDetails.IsRequired.Trim() == "Y" && StyleCostingDetails.Sku_PK == rawmaterial.Sku_Pk 
                                            select new
                                            {
                                                POPackDetails.SIzeCode
                                            }).Distinct();
                        foreach (var sizecode in sizedetails)
                        {
                            if (!entty.SkuRawmaterialDetails.Any(f => f.Sku_PK == rawmaterial.Sku_Pk && f.SizeCode == sizecode.SIzeCode))
                            {
                                SkuRawmaterialDetail skudet = new SkuRawmaterialDetail();
                                skudet.Sku_PK = rawmaterial.Sku_Pk;
                                skudet.SizeCode = sizecode.SIzeCode;
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
                    //if rawmaterial is Size Dependant and color dependant
                    else if (rawmaterial.IsCD.Trim() == "Y" && rawmaterial.IsSD.Trim() == "Y" && rawmaterial.isCommon.Trim() == "N")
                    {
                        //get all the colors
                        var Colordetails = (from StyleCostingDetails in entty.StyleCostingDetails
                                            join POPackDetails in entty.POPackDetails on StyleCostingDetails.StyleCostingMaster.OurStyleID equals POPackDetails.OurStyleID
                                            where
                                         StyleCostingDetails.IsRequired == "Y" && StyleCostingDetails.Sku_PK == rawmaterial.Sku_Pk
                                            select new
                                            {
                                                POPackDetails.ColorCode

                                            }).Distinct();


                        //get all the Sizes
                        var sizedetails = (from StyleCostingDetails in entty.StyleCostingDetails
                                           join POPackDetails in entty.POPackDetails on StyleCostingDetails.StyleCostingMaster.OurStyleID equals POPackDetails.OurStyleID
                                           where
                                            StyleCostingDetails.IsRequired == "Y" && StyleCostingDetails.Sku_PK == rawmaterial.Sku_Pk
                                           select new
                                           {
                                               POPackDetails.SIzeCode
                                           }).Distinct();

                        foreach (var color in Colordetails)
                        {
                            foreach (var sizecode in sizedetails)
                            {
                                if (!entty.SkuRawmaterialDetails.Any(f => f.Sku_PK == rawmaterial.Sku_Pk && f.ColorCode == color.ColorCode && f.SizeCode == sizecode.SIzeCode))
                                {
                                    SkuRawmaterialDetail skudet = new SkuRawmaterialDetail();
                                    skudet.Sku_PK = rawmaterial.Sku_Pk;
                                    skudet.SizeCode = sizecode.SIzeCode;
                                    skudet.ColorCode = color.ColorCode;
                                  
                                    skudet.UnitRate = rawmaterial.Rate;
                                    skudet.Dependency = "CSD";
                                    entty.SkuRawmaterialDetails.Add(skudet);

                                }
                                else
                                {
                                   

                                   

                                }
                            }
                        }

                    }
                    //if rawmaterial is Common
                    else if (rawmaterial.IsCD.Trim() == "N" && rawmaterial.IsSD.Trim() == "N" && rawmaterial.isCommon.Trim() == "Y")
                    {
                        if (!entty.SkuRawmaterialDetails.Any(f => f.Sku_PK == rawmaterial.Sku_Pk ))
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
                                          where skudet.Sku_PK == rawmaterial.Sku_Pk && skudet.Dependency!="CM"
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
                    }

                }
                entty.SaveChanges();
            }
        }

       



     
    }


    public class SKUTransaction

    {
        String connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;

        /// <summary>
        /// Delete All SKuDetails OF a SKU
        /// </summary>
        /// <param name="skuid"></param>
        public void DeleteSKUDetailOfSkUID(int skuid)
        {

            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var skudetails = from skudet in entty.SkuRawmaterialDetails
                                 where skudet.Sku_PK == skuid
                                 select skudet;


                if (skudetails.Count() > 0)
                {

                    foreach (var detail in skudetails)
                    {
                        try
                        {
                            entty.SkuRawmaterialDetails.Remove(detail);
                        }
                        catch (Exception)
                        {
                            
                            
                        }

                    }
                    entty.SaveChanges();
                }


            }

        }






        /// <summary>
        /// Delete A SKU
        /// </summary>
        /// <param name="skuid"></param>
        /// <returns></returns>
        public int DeleteSkUID(int skuid)
        {

            int atcrawpk = 0;
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var skudetails = from skumstr in entty.SkuRawMaterialMasters
                                 where skumstr.Sku_Pk == skuid
                                 select skumstr;


                if (skudetails.Count() > 0)
                {

                    foreach (var detail in skudetails)
                    {
                        atcrawpk = int.Parse(detail.AtcRaw_PK.ToString());
                        entty.SkuRawMaterialMasters.Remove(detail);

                    }


                    entty.SaveChanges();
                }


            }

            return atcrawpk;
        }


        /// <summary>
        /// Update the Atc Rawmaster Template Count
        /// </summary>
        /// <param name="atcrawpk"></param>
        public void GetTemplateCountofATC(int atcrawpk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var count = (from o in entty.SkuRawMaterialMasters
                             where o.AtcRaw_PK == atcrawpk

                             select o).Count();

                var atcrawmaster = from atcrawmstrdet in entty.AtcRawMaterialMasters

                                   where atcrawmstrdet.AtcRaw_PK == atcrawpk
                                   select atcrawmstrdet;


                foreach (var rawdet in atcrawmaster)
                {
                    if (rawdet.TemplateCount > 0)
                    {
                        rawdet.TemplateCount = int.Parse(count.ToString());
                    }
                }


                entty.SaveChanges();

            }







        }




        /// <summary>
        /// Delete  skudet from skudetailsmaster
        /// </summary>
        /// <param name="skudetpk"></param>
        public void DeleteaSKUDetail(int skudetpk)
        {

            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var skudetails = from skudet in entty.SkuRawmaterialDetails
                                 where skudet.SkuDet_PK== skudetpk
                                 select skudet;


                if (skudetails.Count() > 0)
                {

                    foreach (var detail in skudetails)
                    {
                        try
                        {
                            entty.SkuRawmaterialDetails.Remove(detail);
                        }
                        catch (Exception)
                        {


                        }

                    }
                    entty.SaveChanges();
                }


            }

        }











    }
}