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


        public DataTable GetFabricToEndbit( int lctn_pk)
        {
            DataTable dt = new DataTable();
            int itemgroup = 1;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        InventoryItem_PK, RMNum, Description, ItemColor, ItemSize, SupplierSize, SupplierColor, UomCode, ReceivedQty, DeliveredQty,  OnhandQty as TotalOnhand, OnhandQty, SkuDet_Pk,ItemGroup_PK,Refnum
FROM            (SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                                                    SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width + ' (  ' + ISNULL(SkuRawmaterialDetail.ColorCode,
                                                     '') + '  ) ' AS Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.SkuDet_Pk ,
                                                    InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, InventoryMaster.OnhandQty, Template_Master.ItemGroup_PK,InventoryMaster.Refnum
                          FROM            Template_Master INNER JOIN
                                                    SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                                                    UOMMaster INNER JOIN
                                                    InventoryMaster INNER JOIN
                                                    ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON 
                                                    SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk inner join AtcMaster on AtcMaster.AtcId =SkuRawMaterialMaster.Atc_id 
WHERE        (InventoryMaster.Location_PK =@loctn_pk) AND (Template_Master.ItemGroup_PK = 1) AND (InventoryMaster.OnhandQty > 0) and (AtcMaster.AtcId<458) and AtcMaster.IsShipmentCompleted ='Y') AS tt
ORDER BY RMNum, Description, ItemColor, ItemSize, SupplierSize, SupplierColor, UomCode", con);
                
                cmd.Parameters.AddWithValue("@loctn_pk", lctn_pk);

                

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public DataTable GetEndBitDetails(int atcid, int lctn_pk)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        tt.Roll_PK, tt.RollNum,  tt.itemDescription, tt.WidthGroup, tt.ShadeGroup, tt.ShrinkageGroup, tt.AYard,  RollInventoryMaster.IsPresent, tt.SkuDet_PK ,tt.LaySheetDet_PK,
                         RollInventoryMaster.Location_Pk AS Expr1, tt.MarkerType, tt.AWidth, tt.AShrink, tt.AShade,ISNULL( tt.SWeight,'NA') as SWeight, ISNULL (tt.EndBit,0)as Endbit
FROM            (SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, 
                                                    ISNULL(SkuRawMaterialMaster.Composition, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N' ') 
                                                    + ' ' + ISNULL(SkuRawMaterialMaster.Width, N' ')+ ' ' + ISNULL(ProcurementDetails.SupplierSize, N' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, N' ') AS itemDescription, LaySheetMaster.Location_PK ,
                                                    FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.AYard,
                                                    FabricRollmaster.MarkerType, FabricRollmaster.AWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.SWeight,
													LaySheetDetails.EndBit ,FabricRollmaster.SkuDet_PK ,LaySheetDetails.LaySheetDet_PK
                          FROM            SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                                                    FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
													LaySheetDetails on LaySheetDetails.Roll_PK =FabricRollmaster.Roll_PK inner join
                                                    ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
													LaySheetMaster on LaySheetMaster.LaySheet_PK =LaySheetDetails.LaySheet_PK 
                                                    
                          WHERE       (LaySheetMaster.AtcID =@atcid) AND (LaySheetDetails.IsRecuttable = N'N') AND (LaySheetDetails.IsReturned is null) AND (LaySheetDetails.EndBit > 0) AND (LaySheetDetails.IsDeleted = N'N')) AS tt INNER JOIN
                         RollInventoryMaster ON tt.Roll_PK = RollInventoryMaster.Roll_PK
WHERE        (RollInventoryMaster.IsPresent = N'Y') and (RollInventoryMaster.Location_Pk =@loctn_pk)
ORDER BY tt.RollNum", con);
                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@loctn_pk", lctn_pk);
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
            }
            return dt;
        }

        public DataTable GetEndBitStockDetails(int lctn_pk)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, 
                                                    ISNULL(SkuRawMaterialMaster.Composition, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N' ') 
                                                    + ' ' + ISNULL(SkuRawMaterialMaster.Width, N' ')+ ' ' + ISNULL(ProcurementDetails.SupplierSize, N' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, N' ') AS itemDescription,
                                                    FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.AYard,
                                                    FabricRollmaster.MarkerType, FabricRollmaster.AWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.SWeight,
													EndbitInventory.OnhandQty ,FabricRollmaster.SkuDet_PK,EndbitInventory.Location_pk,AtcMaster.AtcNum ,EndbitInventory.Endbit_pk
                          FROM            SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                                                    FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN													
                                                    ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
													EndbitInventory on EndbitInventory.roll_pk=FabricRollmaster.Roll_PK inner join 
													ProcurementMaster on ProcurementMaster.PO_Pk=ProcurementDetails.PO_Pk inner join 
													AtcMaster on AtcMaster.AtcId =ProcurementMaster.AtcId 
                                                    
                          WHERE    (EndbitInventory.OnhandQty>0) and (EndbitInventory.Location_pk=@lctn_pk)", con);
                cmd.Parameters.AddWithValue("@lctn_pk", lctn_pk);
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
            }
            return dt;
        }
        public DataTable GetOldEndBitStockDetails(int lctn_pk)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT      ISNULL(SkuRawMaterialMaster.Composition, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N' ') 
          + ' ' + ISNULL(SkuRawMaterialMaster.Width, N' ')+ ' ' + ISNULL(ProcurementDetails.SupplierSize, N' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, N' ') AS itemDescription,
           EndbitInventory.OnhandQty ,EndbitInventory.Location_pk,AtcMaster.AtcNum ,EndbitInventory.Endbit_pk,InventoryMaster.InventoryItem_PK ,InventoryMaster.SkuDet_Pk,0 as Roll_PK,
		   AtcMaster.AtcNum as RollNum,0 as WidthGroup,0 as ShadeGroup,0 as ShrinkageGroup,0 as AWidth,0 as AShrink,0 as AShade,0 as AYard  
                          FROM            SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
													EndbitInventory on EndbitInventory.Skudet_pk =SkuRawmaterialDetail.SkuDet_PK inner join
													InventoryMaster on InventoryMaster.InventoryItem_PK =EndbitInventory.inventoryitem_pk inner join
													ProcurementDetails ON ProcurementDetails.PODet_PK =InventoryMaster.PoDet_PK  INNER JOIN	
													ProcurementMaster on ProcurementMaster.PO_Pk=ProcurementDetails.PO_Pk inner join 
													AtcMaster on AtcMaster.AtcId =ProcurementMaster.AtcId 													                                                    
                          WHERE    (EndbitInventory.OnhandQty>0) and (EndbitInventory.Location_pk=@lctn_pk) and EndbitInventory.Roll_Pk =0
", con);
                cmd.Parameters.AddWithValue("@lctn_pk", lctn_pk);
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




                SqlCommand cmd = new SqlCommand(@"SELECT        SInventoryItem_PK, Description, Composition, Construct, TemplateColor, TemplateSize, width, Unitprice, ReceivedQty, (OnHandQty-BlockedQty)as OnHandQty, UomName, Location_Pk, deliveryqty, CuRate, BlockedQty
FROM            (SELECT        StockInventoryMaster.SInventoryItem_PK, Template_Master.Description, StockInventoryMaster.Composition, StockInventoryMaster.Construct, StockInventoryMaster.TemplateColor, 
                                                    StockInventoryMaster.TemplateSize, isnull(StockInventoryMaster.TemplateWidth,'') + ' ' + isnull(StockInventoryMaster.TemplateWeight,'') AS width, StockInventoryMaster.Unitprice, StockInventoryMaster.ReceivedQty, 
                                                    StockInventoryMaster.OnHandQty, UOMMaster.UomName, StockInventoryMaster.Location_Pk, 0.0 AS deliveryqty, StockInventoryMaster.CuRate, ISNULL
                                                        ((SELECT        SUM(RequestOrderStockDetails.Qty) AS Expr1
                                                            FROM            RequestOrderStockMaster INNER JOIN
                                                                                     RequestOrderStockDetails ON RequestOrderStockMaster.SRO_Pk = RequestOrderStockDetails.SRO_Pk
                                                            WHERE        (RequestOrderStockMaster.Iscompleted = N'N') AND (RequestOrderStockMaster.IsDeleted = N'N') AND (RequestOrderStockDetails.SInventoryItem_PK = StockInventoryMaster.SInventoryItem_PK)), 0) 
                                                    AS BlockedQty
                          FROM            StockInventoryMaster INNER JOIN
                                                    Template_Master ON StockInventoryMaster.Template_PK = Template_Master.Template_PK INNER JOIN
                                                    UOMMaster ON StockInventoryMaster.Uom_PK = UOMMaster.Uom_PK
                          WHERE        (StockInventoryMaster.Location_Pk = @loctn_pk) AND (StockInventoryMaster.OnHandQty > 0)) AS tt", con);
               
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