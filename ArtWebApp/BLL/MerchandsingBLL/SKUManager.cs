using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.MerchandsingBLL
{
    public class SKUManager
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
       /// <summary>
       /// Delete the Skudetails and SKU Master and Reset the count in atcrawmaterialmaster
       /// </summary>
       /// <param name="skuid"></param>
       /// <returns></returns>
        public Boolean DeleteSKUID(int skuid)
        {
            Boolean isdeleted = false;
            //if Po not given
            if (!IsPOGivenforsku(skuid))
          {
              DBTransaction.SKUTransaction skucrtr = new DBTransaction.SKUTransaction();
              //Deletes the SKudetails first
              skucrtr.DeleteSKUDetailOfSkUID(skuid);

              //Deletes The SKUMaster then
              int atcrawpk=     skucrtr.DeleteSkUID(skuid);

                //Updates the templatecount
              skucrtr.GetTemplateCountofATC(atcrawpk);
              isdeleted = true;
          }
            else
            {
                isdeleted = false;

            }

            return isdeleted;
        }

        /// <summary>
        /// Return true if PO is given for a sku
        /// </summary>
        /// <param name="skuid"></param>
        /// <returns></returns>
        public Boolean IsPOGivenforsku(int skuid)
        {
            Boolean ispogiven = false;
            DBTransaction.ProcurementTransaction potran = new DBTransaction.ProcurementTransaction();
            DataTable dt = potran.GetPOofSkuid(skuid);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if(dt.Rows[0][0].ToString ().Trim ()!="")
                    {
                        ispogiven = true;
                    }
                }
            }

            return ispogiven;
        }


        /// <summary>
        /// Check if po given for SKudet_PK
        /// </summary>
        /// <param name="skudet_pk"></param>
        /// <returns></returns>
        public Boolean IfPOGivenforSKUDET_PK(int skudet_pk)
        {
            Boolean ispogiven = false;
            DBTransaction.ProcurementTransaction potran = new DBTransaction.ProcurementTransaction();
            DataTable dt = potran.GetPOofSkuDetPK(skudet_pk);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString().Trim() != "")
                    {
                        ispogiven = true;
                    }
                }
            }

            return ispogiven;
        }



        /// <summary>
        /// delete SKU Details if no PO given
        /// </summary>
        /// <param name="skudet_pk"></param>
        public void DeleteSkuDetail(int skudet_pk)
        {
            if (IfPOGivenforSKUDET_PK(skudet_pk) != true)
            {
                using (ArtEntitiesnew entty = new ArtEntitiesnew())
                {
                    var skudetails = from skumstr in entty.SkuRawmaterialDetails
                                     where skumstr.SkuDet_PK == skudet_pk
                                     select skumstr;


                    if (skudetails.Count() > 0)
                    {

                        foreach (var detail in skudetails)
                        {
                           
                            entty.SkuRawmaterialDetails.Remove(detail);

                        }


                        entty.SaveChanges();
                    }


                }

            }
        }



        /// <summary>
        /// Get the Buyer PoPack of a Atc
        /// 
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>

        public DataTable GetAllSKUDetails(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ColorCode,
                         SkuRawmaterialDetail.SizeCode, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize,  SkuRawMaterialMaster.Uom_PK, SkuRawMaterialMaster.AltUom_pk,  SkuRawMaterialMaster.isCommon, SkuRawMaterialMaster.IsCD, 
                         SkuRawMaterialMaster.IsSD, UOMMaster.UomCode, SkuRawMaterialMaster.Template_pk, ISNULL(SkuRawMaterialMaster.OrderMin, 0) AS OrderMin
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON SkuRawMaterialMaster.AltUom_pk = UOMMaster.Uom_PK
WHERE        (SkuRawMaterialMaster.Atc_id =@Param1)
ORDER BY SkuRawMaterialMaster.RMNum ", con);


                cmd.Parameters.AddWithValue("@Param1", atcid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



    }

    public class SKUCopy
    {





        public void Copyrawmaterial(int fromatcid, int toatcid,String fromatcnum, String toatcnum)
        {
            AddRawMaterialmaster(fromatcid, toatcid);

            generateSKU(toatcid);
            UpdateSkuMaster(fromatcid, toatcid, fromatcnum, toatcnum);
        }



        public void AddRawMaterialmaster(int fromatcid ,int toatcid)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var q = from fromatcraw in enty.AtcRawMaterialMasters
                        where fromatcraw.Atc_id == fromatcid
                        select fromatcraw;



                foreach (var element in q)
                {


                    if (!enty.AtcRawMaterialMasters.Any(f => f.Template_PK == element.Template_PK && f.Atc_id == toatcid))
                    {

                        AtcRawMaterialMaster stsz = new AtcRawMaterialMaster();



                        stsz.TempCode =  element.TempCode.ToString();
                        stsz.Template_PK = element.Template_PK;
                        stsz.Atc_id = toatcid;
                        stsz.TemplateName = element.TemplateName;
                        stsz.TemplateCount = element.TemplateCount;
                        stsz.IsGD = element.IsGD;
                        enty.AtcRawMaterialMasters.Add(stsz);


                    }

                   
                }
                enty.SaveChanges();
            }






                   
                
                
            }


        /// <summary>
        /// generate SKUmaster
        /// </summary>
        public void generateSKU(int toatcid)
        {
            ArtWebApp.DBTransaction.SkuCreator skucrtr = new ArtWebApp.DBTransaction.SkuCreator();
            skucrtr.CreateSkuMaster(toatcid);
            
        }



        public void UpdateSkuMaster(int fromatcid, int toatcid, String fromatcnum, String toatcnum)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q1 = from skumst in enty.SkuRawMaterialMasters
                        where skumst.Atc_id == fromatcid
                        select skumst;


                foreach (var element1 in q1)
                {
                   

                    String rmnum = element1.RMNum.ToString().Trim().Replace(fromatcnum, toatcnum);

                    var q = from skumst in enty.SkuRawMaterialMasters
                            where skumst.Atc_id == toatcid && skumst.Template_pk== element1.Template_pk && skumst.RMNum.Trim ()==rmnum
                            select skumst;

                    foreach (var element in q)
                    {
                        element.Composition = element1.Composition;
                        element.Construction = element1.Construction;
                        element.Weight =element1.Weight;
                        element.Width = element1.Width;
                        element.IsCD = element1.IsCD;
                        element.IsSD = element1.IsSD;
                        element.AltUom_pk = element1.AltUom_pk;
                        element.Uom_PK = element1.Uom_PK;
                        element.isCommon = element1.isCommon;
                        element.Rate = element1.Rate;
                        element.WastagePercentage = element1.WastagePercentage;
                        element.OrderMin = element1.OrderMin;
                    }

                }
                enty.SaveChanges();
            }
        }













    }







}