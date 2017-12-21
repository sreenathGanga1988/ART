using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ArtWebApp.DBTransaction
{
    public class DeliveryOrdertransaction
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
      /// <summary>
      /// Get the Inventory of a ATC in a location
      /// </summary>
      /// <param name="atcid"></param>
      /// <param name="lctn_pk"></param>
      /// <returns></returns>
        public DataTable GetStockDetails(int atcid,int lctn_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




//                SqlCommand cmd = new SqlCommand(@"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
//                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
//                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
//                         InventoryMaster.OnhandQty
//FROM            UOMMaster INNER JOIN
//                         InventoryMaster INNER JOIN
//                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK INNER JOIN
//                         SkuRawMaterialMaster INNER JOIN
//                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK
//WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (InventoryMaster.Location_PK = @loctn_pk) AND (InventoryMaster.OnhandQty > 0)
//ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode", con);






                SqlCommand cmd = new SqlCommand(@"SELECT        InventoryItem_PK, RMNum, Description, ItemColor, ItemSize, SupplierSize, SupplierColor, UomCode, ReceivedQty, DeliveredQty, OnhandQty as TotalOnhand, BlockedQty,(OnhandQty-BlockedQty) as OnhandQty,Refnum
FROM            (SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                                                    SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, 
                                                    SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, 
                                                    InventoryMaster.DeliveredQty, InventoryMaster.OnhandQty, ISNULL
                                                        ((SELECT        SUM(RequestOrderDetails.Qty) AS Expr1
                                                            FROM            RequestOrderDetails INNER JOIN
                                                                                     RequestOrderMaster ON RequestOrderDetails.RO_Pk = RequestOrderMaster.RO_Pk
                                                            WHERE        (RequestOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK) AND (RequestOrderMaster.IsDeleted = N'N') AND (RequestOrderMaster.IsCompleted = N'N')), 0) 
                                                    + ISNULL
                                                        ((SELECT        SUM(LoanQty) AS Expr1
                                                            FROM            InventoryLoanMaster
                                                            WHERE        (FromIIT_Pk = InventoryMaster.InventoryItem_PK) AND (IsApproved = N'N')), 0) AS BlockedQty,InventoryMaster.Refnum
                          FROM            UOMMaster INNER JOIN
                                                    InventoryMaster INNER JOIN
                                                    ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK INNER JOIN
                                                    SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK
 WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (InventoryMaster.Location_PK = @loctn_pk) AND (InventoryMaster.OnhandQty > 0)) AS tt
ORDER BY RMNum, Description, ItemColor, ItemSize, SupplierSize, SupplierColor, UomCode

", con);










                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@loctn_pk", lctn_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }




        /// <summary>
        /// get the inventory of a specific item group of a atc in a location
        /// </summary>
        /// <param name="atcid"></param>
        /// <param name="lctn_pk"></param>
        /// <param name="Itemtype"></param>
        /// <returns></returns>


        public DataTable GetStockItemDetails(int atcid, int lctn_pk,String Itemtype)
        {
            DataTable dt = new DataTable();
            int itemgroup=1;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




//                SqlCommand cmd = new SqlCommand(@"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
//                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' +  SkuRawMaterialMaster.Width + ' (  ' + ISNULL(SkuRawmaterialDetail.ColorCode, '')+ '  ) '     AS Description, SkuRawmaterialDetail.ItemColor, 
//                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
//                         InventoryMaster.OnhandQty, Template_Master.ItemGroup_PK
//FROM            Template_Master INNER JOIN
//                         SkuRawMaterialMaster INNER JOIN
//                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
//                         UOMMaster INNER JOIN
//                         InventoryMaster INNER JOIN
//                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON 
//                         SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk
//WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (InventoryMaster.Location_PK =@loctn_pk) AND (Template_Master.ItemGroup_PK = @itemgroup) AND (InventoryMaster.OnhandQty > 0)
//ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode", con);





                SqlCommand cmd = new SqlCommand(@"SELECT        InventoryItem_PK, RMNum, Description, ItemColor, ItemSize, SupplierSize, SupplierColor, UomCode, ReceivedQty, DeliveredQty,  OnhandQty as TotalOnhand, BlockedQty,(OnhandQty-BlockedQty) as OnhandQty, ItemGroup_PK,Refnum
FROM            (SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                                                    SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width + ' (  ' + ISNULL(SkuRawmaterialDetail.ColorCode,
                                                     '') + '  ) ' AS Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, 
                                                    InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, InventoryMaster.OnhandQty, Template_Master.ItemGroup_PK, ISNULL
                                                        ((SELECT        SUM(RequestOrderDetails.Qty) AS Expr1
                                                            FROM            RequestOrderDetails INNER JOIN
                                                                                     RequestOrderMaster ON RequestOrderDetails.RO_Pk = RequestOrderMaster.RO_Pk
                                                            WHERE        (RequestOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK) AND (RequestOrderMaster.IsDeleted = N'N') AND (RequestOrderMaster.IsCompleted = N'N')), 0) 
                                                    + ISNULL
                                                        ((SELECT        SUM(LoanQty) AS Expr1
                                                            FROM            InventoryLoanMaster
                                                            WHERE        (FromIIT_Pk = InventoryMaster.InventoryItem_PK) AND (IsApproved = N'N')), 0) AS BlockedQty, InventoryMaster.Refnum
                          FROM            Template_Master INNER JOIN
                                                    SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                                                    UOMMaster INNER JOIN
                                                    InventoryMaster INNER JOIN
                                                    ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON 
                                                    SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk
WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (InventoryMaster.Location_PK =@loctn_pk) AND (Template_Master.ItemGroup_PK = @itemgroup) AND (InventoryMaster.OnhandQty > 0)) AS tt
ORDER BY RMNum, Description, ItemColor, ItemSize, SupplierSize, SupplierColor, UomCode", con);
                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@loctn_pk", lctn_pk);

                if(Itemtype=="Trims")
                {
                    itemgroup=2;
                }
                else if (Itemtype=="Fabric")
                {
                    itemgroup = 1;
                }


                cmd.Parameters.AddWithValue("@itemgroup", itemgroup);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }





        /// <summary>
        /// Get the details of a DO
        /// </summary>
        /// <param name="dopk"></param>
        /// <returns></returns>


        public DataTable GetDetailsOfDO(int dopk)
        {
               DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        DeliveryOrderDetails.DODet_PK, InventoryMaster.InventoryItem_PK, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, ProcurementDetails.SupplierSize, 
                         ProcurementDetails.SupplierColor, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, SkuRawMaterialMaster.RMNum, UOMMaster.UomCode, DeliveryOrderDetails.DeliveryQty, 
                         ISNULL(SUM(DeliveryReceiptDetail.ReceivedQty), 0) AS ReceivedQty, DeliveryOrderDetails.DeliveryQty - ISNULL(SUM(DeliveryReceiptDetail.ReceivedQty), 0) AS BalanceQty, DeliveryOrderDetails.DO_PK
FROM            DeliveryOrderDetails INNER JOIN
                         InventoryMaster ON DeliveryOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK LEFT OUTER JOIN
                         DeliveryReceiptDetail ON DeliveryOrderDetails.DODet_PK = DeliveryReceiptDetail.DODet_PK
GROUP BY DeliveryOrderDetails.DODet_PK, InventoryMaster.InventoryItem_PK, DeliveryOrderDetails.DO_PK, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width, DeliveryOrderDetails.DeliveryQty, 
                         ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, SkuRawMaterialMaster.RMNum, UOMMaster.UomCode
HAVING        (DeliveryOrderDetails.DO_PK = @dopk)", con);
                cmd.Parameters.AddWithValue("@dopk", dopk);
               
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
            
        }



        /// <summary>
        /// get the general stock details of a location
        /// </summary>
        /// <param name="lctn_pk"></param>
        /// <returns></returns>
        public DataTable GetGeneralStockDetails(int lctn_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        StockInventoryMaster.SInventoryItem_PK, Template_Master.Description, StockInventoryMaster.Composition, StockInventoryMaster.Construct, StockInventoryMaster.TemplateColor, 
                         StockInventoryMaster.TemplateSize, StockInventoryMaster.TemplateWidth + ' ' + StockInventoryMaster.TemplateWeight AS width, StockInventoryMaster.Unitprice, StockInventoryMaster.ReceivedQty, 
                         StockInventoryMaster.OnHandQty, UOMMaster.UomName, StockInventoryMaster.Location_Pk, 0.0 AS deliveryqty , StockInventoryMaster.CUrate
FROM            StockInventoryMaster INNER JOIN
                         Template_Master ON StockInventoryMaster.Template_PK = Template_Master.Template_PK INNER JOIN
                         UOMMaster ON StockInventoryMaster.Uom_PK = UOMMaster.Uom_PK
WHERE        (StockInventoryMaster.Location_Pk = @loctn_pk) AND (StockInventoryMaster.OnHandQty > 0)", con);
               
                cmd.Parameters.AddWithValue("@loctn_pk", lctn_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }
    
    
  



                               /// <summary>
        /// Get the details of a DO
        /// </summary>
        /// <param name="dopk"></param>
        /// <returns></returns>


        public DataTable GetDetailsOfsTOCKDO(int Sdopk)
        {
               DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@" sELECT TT.SInventoryItem_PK,TT.SDODet_PK,TT.Description,TT.TemplateSize, TT.TemplateWidth, 
                         TT.TemplateWeight, TT.DeliveredQty AS DeliveredQty, TT.Uom_PK, TT.UomCode,TT.ReceivedQty,(TT.DeliveredQty-TT.ReceivedQty) AS OnhandQty
						  FROM (SELECT        StockInventoryMaster.SInventoryItem_PK, DeliveryOrderStockDetails.SDODet_PK, ISNULL(Template_Master.Description, ' ') + ' ' + ISNULL(StockInventoryMaster.Composition, ' ') 
                         + ' ' + ISNULL(StockInventoryMaster.Construct, ' ') + ' ' + ISNULL(StockInventoryMaster.TemplateColor, '') AS Description, StockInventoryMaster.TemplateSize, StockInventoryMaster.TemplateWidth, 
                         StockInventoryMaster.TemplateWeight, DeliveryOrderStockDetails.DeliveryQty AS DeliveredQty, StockInventoryMaster.Uom_PK, UOMMaster.UomCode, ISNULL
                             ((SELECT        SUM(ReceivedQty) AS Expr1
                                 FROM            DeliveryReceiptStockDetail AS SD
                                 GROUP BY  SDODet_PK
                                 HAVING        (SDODet_PK = DeliveryOrderStockDetails.SDODet_PK)), 0) AS ReceivedQty, DeliveryOrderStockMaster.SDO_PK
FROM            DeliveryOrderStockMaster INNER JOIN
                         DeliveryOrderStockDetails ON DeliveryOrderStockMaster.SDO_PK = DeliveryOrderStockDetails.SDO_PK INNER JOIN
                         StockInventoryMaster ON DeliveryOrderStockDetails.SInventoryItem_PK = StockInventoryMaster.SInventoryItem_PK INNER JOIN
                         Template_Master ON StockInventoryMaster.Template_PK = Template_Master.Template_PK INNER JOIN
                         UOMMaster ON StockInventoryMaster.Uom_PK = UOMMaster.Uom_PK
WHERE        (DeliveryOrderStockMaster.SDO_PK = @dopk))TT ", con);
                cmd.Parameters.AddWithValue("@dopk", Sdopk);
               
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
            
        }









    }


  




}