using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.MerchandsingBLL
{
    public class BOMData
    {




        public DataTable ShowBOM( int atcid)
        {
            ArtWebApp.DBTransaction.SkuCreator skucrtr = new ArtWebApp.DBTransaction.SkuCreator();
          //  skucrtr.CreateSkUDetails(atcid);
            DBTransaction.BOMTransaction bomtrans = new DBTransaction.BOMTransaction();
            DataTable BomData = bomtrans.GetBOM(atcid);
            
            if (BomData.Rows.Count <= 0)
            {
              
            }
            else
            {
                foreach (System.Data.DataColumn col in BomData.Columns) col.ReadOnly = false;
                // CalculateRequiredPOIssued(BomData);
                MerchandsingBLL.BOMGenerator.CalculateRequiredPOIssued(BomData, atcid);


            }
            return BomData;

            
        }





        public DataTable ShowPOdetails(System.Collections.ArrayList shpdetlist ,int atcid)
        {

            DataTable BomData = GetBOMOfSpecificData(shpdetlist);

            if (BomData.Rows.Count <= 0)
            {

            }
            else
            {
                foreach (System.Data.DataColumn col in BomData.Columns) col.ReadOnly = false;
               // CalculateRequiredPOIssued(BomData);

                MerchandsingBLL.BOMGenerator.CalculateRequiredPOIssued(BomData, atcid);

            }
            return BomData;


        }














        public void CalculateRequiredPOIssued(DataTable dt)
        {
            DBTransaction.BOMTransaction bomtrans = new DBTransaction.BOMTransaction();
            for(int i=0;i<dt.Rows.Count;i++)
            {
                int skudetpk =int.Parse ( dt.Rows[i]["SkuDet_PK"].ToString ().Trim());

                int uom_pk = int.Parse(dt.Rows[i]["uom_pk"].ToString().Trim());
                String isCD = dt.Rows[i]["IsCD"].ToString().Trim();
                String isSD = dt.Rows[i]["IsSD"].ToString().Trim();
                String isCM = dt.Rows[i]["isCommon"].ToString ().Trim();
                String IsGD = dt.Rows[i]["IsGD"].ToString().Trim();
                //if(skudetpk==30249)
                //{
                //    int k = 0;
                //}

                // int reqty = (int)Math.Round(bomtrans.requiredqtycalculator(skudetpk, isCD, isSD, isCM), 0);
                int reqty = (int)Math.Round(bomtrans.requiredqtycalculator(skudetpk, isCD, isSD, isCM, IsGD), 0);

                try
                {
                    int ordermin = (int)Math.Round(float.Parse(dt.Rows[i]["OrderMin"].ToString()));
                   if(reqty<ordermin)
                   {
                       reqty = ordermin;
                   }
                   
                }
                catch (Exception)
                {
                    
                    
                }












                int extrabomqty = (int)Math.Round(bomtrans.GetExtraBOMRequest(skudetpk));


                int posiisued = (int)Math.Round(bomtrans.GetPoIssuedQtyinBaseUOM(skudetpk, uom_pk), 0);


                int wrongpoissued= (int)Math.Round(bomtrans.GetWrongPoIssuedQtyinBaseUOMwithApproval(skudetpk, uom_pk), 0);


                int balanceqty = (reqty - posiisued)+wrongpoissued + extrabomqty;

                

                dt.Rows[i]["RqdQty"] = reqty;
                dt.Rows[i]["PoIssuedQty"] = posiisued;
                dt.Rows[i]["BalanceQty"] = balanceqty;
                

            }

        }



        public DataTable GetBOMOfSpecificData(System.Collections.ArrayList shpdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";

            for (int i = 0; i < shpdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " WHERE        SkuRawmaterialDetail.SkuDet_PK = " + shpdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or SkuRawmaterialDetail.SkuDet_PK =" + shpdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ColorCode, 
                         SkuRawmaterialDetail.SizeCode, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize, ISNULL
                             ((SELECT        MAX(StyleCostingDetails.Rate) AS Expr1
                                 FROM            StyleCostingDetails INNER JOIN
                                                          StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK
                                 GROUP BY StyleCostingDetails.Sku_PK, StyleCostingMaster.IsApproved
                                 HAVING        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk)), SkuRawmaterialDetail.UnitRate) AS UnitRate, SkuRawmaterialDetail.RqdQty, 
                         0000 AS PoIssuedQty, 0000 AS BalanceQty, SkuRawMaterialMaster.Uom_PK, SkuRawMaterialMaster.AltUom_pk, SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.isCommon, SkuRawMaterialMaster.IsCD, 
                         SkuRawMaterialMaster.IsSD, UOMMaster.UomCode, SkuRawMaterialMaster.Template_pk, ISNULL(SkuRawMaterialMaster.OrderMin, 0) AS OrderMin ,SizeMaster.SizeName,SkuRawMaterialMaster.IsGD,00 as garmentQty
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON SkuRawMaterialMaster.AltUom_pk = UOMMaster.Uom_PK  left outer JOIN
                         SizeMaster ON SkuRawmaterialDetail.SizeCode = SizeMaster.SizeCode " + condition + " ORDER BY SkuRawMaterialMaster.RMNum";
                DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
                dt = pktrans.getPodetails(query);
            }
            return dt;

        }





    }
}