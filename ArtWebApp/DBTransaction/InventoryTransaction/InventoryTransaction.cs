using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.DBTransaction.InventoryTransaction
{
    public class InventoryTransaction
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;



        /// <summary>
        /// gets the inventory Of Template of a Atc
        /// Irespective of Location
        /// </summary>
        /// <param name="atcid"></param>
        /// <param name="temp_pk"></param>
        /// <returns></returns>
        public DataTable GetInventoryOfaAtctemplate(int atcid, int temp_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        InventoryMaster.InventoryItem_PK,InventoryMaster.SkuDet_Pk, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.OnhandQty, ProcurementDetails_1.POUnitRate, 
                         SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.Template_pk, LocationMaster.LocationPrefix,ProcurementDetails.CURate
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         ProcurementDetails AS ProcurementDetails_1 ON InventoryMaster.PoDet_PK = ProcurementDetails_1.PODet_PK INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK
WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (SkuRawMaterialMaster.Template_pk = @temp_pk) AND (InventoryMaster.OnhandQty > 0) 
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode", con);
                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@temp_pk", temp_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }





        /// <summary>
        /// Get Inventory Of An Atc Item Within A Location
        /// </summary>
        /// <param name="atcid"></param>
        /// <param name="temp_pk"></param>
        /// <param name="Location_pk"></param>
        /// <returns></returns>
        public DataTable GetAtcTemplateInLocation(int atcid, int temp_pk, int Location_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




//                SqlCommand cmd = new SqlCommand(@"SELECT        InventoryMaster.InventoryItem_PK, InventoryMaster.SkuDet_Pk, SkuRawMaterialMaster.RMNum, 
//                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
//                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.OnhandQty, ProcurementDetails_1.POUnitRate, 
//                         SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.Template_pk, LocationMaster.LocationPrefix, ProcurementDetails.CURate, InventoryMaster.Uom_Pk
//FROM            InventoryMaster INNER JOIN
//                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
//                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
//                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
//                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
//                         ProcurementDetails AS ProcurementDetails_1 ON InventoryMaster.PoDet_PK = ProcurementDetails_1.PODet_PK INNER JOIN
//                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK
//WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (SkuRawMaterialMaster.Template_pk = @temp_pk) AND (LocationMaster.Location_PK = @loc_pk) AND (InventoryMaster.OnhandQty > 0)
//ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode", con);


//                SqlCommand cmd = new SqlCommand(@"SELECT        InventoryMaster.InventoryItem_PK, InventoryMaster.SkuDet_Pk, SkuRawMaterialMaster.RMNum, 
//                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
//                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.OnhandQty, SkuRawMaterialMaster.Atc_id, 
//                         SkuRawMaterialMaster.Template_pk, LocationMaster.LocationPrefix, ProcurementDetails.CURate, InventoryMaster.Uom_Pk, ProcurementDetails.POUnitRate, InventoryMaster.Refnum,ISNULL
//                                                        ((SELECT        SUM(RequestOrderDetails.Qty) AS Expr1
//                                                            FROM            RequestOrderDetails INNER JOIN
//                                                                                     RequestOrderMaster ON RequestOrderDetails.RO_Pk = RequestOrderMaster.RO_Pk
//                                                            WHERE        (RequestOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK) AND (RequestOrderMaster.IsDeleted = N'N') AND (RequestOrderMaster.IsCompleted = N'N')), 0) 
//                                                    + ISNULL
//                                                        ((SELECT        SUM(LoanQty) AS Expr1
//                                                            FROM            InventoryLoanMaster
//                                                            WHERE        (FromIIT_Pk = InventoryMaster.InventoryItem_PK) AND (IsApproved = N'N')), 0) AS BlockedQty
//FROM            InventoryMaster INNER JOIN
//                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
//                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
//                         SkuRawMaterialMaster ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
//                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
//                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK
//WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (SkuRawMaterialMaster.Template_pk = @temp_pk) AND (LocationMaster.Location_PK = @loc_pk) AND (InventoryMaster.OnhandQty > 0)
//ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode", con);



                SqlCommand cmd = new SqlCommand(@"SELECT        InventoryItem_PK, SkuDet_Pk, RMNum, Description, ItemColor, ItemSize, SupplierSize, SupplierColor, UomCode, OnhandQty as TotalOnhand, BlockedQty,(OnhandQty-BlockedQty) as OnhandQty, Atc_id, Template_pk, LocationPrefix, CURate, Uom_Pk, POUnitRate, Refnum, 
                         BlockedQty
FROM            (SELECT        TOP (100) PERCENT InventoryMaster.InventoryItem_PK, InventoryMaster.SkuDet_Pk, SkuRawMaterialMaster.RMNum, 
                                                    SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, 
                                                    SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.OnhandQty, 
                                                    SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.Template_pk, LocationMaster.LocationPrefix, ProcurementDetails.CURate, InventoryMaster.Uom_Pk, ProcurementDetails.POUnitRate, 
                                                    InventoryMaster.Refnum, ISNULL
                                                        ((SELECT        SUM(RequestOrderDetails.Qty) AS Expr1
                                                            FROM            RequestOrderDetails INNER JOIN
                                                                                     RequestOrderMaster ON RequestOrderDetails.RO_Pk = RequestOrderMaster.RO_Pk
                                                            WHERE        (RequestOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK) AND (RequestOrderMaster.IsDeleted = N'N') AND (RequestOrderMaster.IsCompleted = N'N')), 0) 
                                                    + ISNULL
                                                        ((SELECT        SUM(LoanQty) AS Expr1
                                                            FROM            InventoryLoanMaster
                                                            WHERE        (FromIIT_Pk = InventoryMaster.InventoryItem_PK) AND (IsApproved = N'N')), 0) AS BlockedQty
                          FROM            InventoryMaster INNER JOIN
                                                    ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                                                    SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                                                    SkuRawMaterialMaster ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                                                    UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                                                    LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK
                          WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (SkuRawMaterialMaster.Template_pk = @temp_pk) AND (LocationMaster.Location_PK = @loc_pk) AND (InventoryMaster.OnhandQty > 0)
                          ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, 
                                                    UOMMaster.UomCode) AS tt", con);






                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@temp_pk", temp_pk);
                cmd.Parameters.AddWithValue("@loc_pk", Location_pk);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }





        /// <summary>
        /// Get Inventory a item lying in Gstock of a location
        /// </summary>
        /// <param name="atcid"></param>
        /// <param name="temp_pk"></param>
        /// <param name="Location_pk"></param>
        /// <returns></returns>
        public DataTable GetGStockInventoryofanItem(int temp_pk, int Location_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




//                SqlCommand cmd = new SqlCommand(@"SELECT        StockInventoryMaster.SInventoryItem_PK, Template_Master.Description, StockInventoryMaster.Composition, StockInventoryMaster.Construct, StockInventoryMaster.TemplateColor, 
//                         StockInventoryMaster.TemplateSize, StockInventoryMaster.TemplateWidth + ' ' + StockInventoryMaster.TemplateWeight AS width, StockInventoryMaster.Unitprice, StockInventoryMaster.ReceivedQty, 
//                         StockInventoryMaster.OnHandQty, UOMMaster.UomName, StockInventoryMaster.Location_Pk, 0.0 AS deliveryqty, StockInventoryMaster.Template_PK, StockInventoryMaster.CuRate
//FROM            StockInventoryMaster INNER JOIN
//                         Template_Master ON StockInventoryMaster.Template_PK = Template_Master.Template_PK INNER JOIN
//                         UOMMaster ON StockInventoryMaster.Uom_PK = UOMMaster.Uom_PK
//WHERE        (StockInventoryMaster.Location_Pk = @loctn_pk) AND (StockInventoryMaster.OnHandQty > 0) AND (StockInventoryMaster.Template_PK = @temp_pk)", con);






                SqlCommand cmd = new SqlCommand(@"SELECT        SInventoryItem_PK, Description, Composition, Construct, TemplateColor, TemplateSize, width, Unitprice, ReceivedQty,  OnhandQty as TotalOnhand, BlockedQty,(OnhandQty-BlockedQty) as OnhandQty, UomName, Location_Pk, deliveryqty, Template_PK, CuRate, Refnum, 
                         BlockedQty
FROM            (SELECT        StockInventoryMaster.SInventoryItem_PK, Template_Master.Description, StockInventoryMaster.Composition, StockInventoryMaster.Construct, StockInventoryMaster.TemplateColor, 
                                                    StockInventoryMaster.TemplateSize, StockInventoryMaster.TemplateWidth + ' ' + StockInventoryMaster.TemplateWeight AS width, StockInventoryMaster.Unitprice, StockInventoryMaster.ReceivedQty, 
                                                    StockInventoryMaster.OnHandQty, UOMMaster.UomName, StockInventoryMaster.Location_Pk, 0.0 AS deliveryqty, StockInventoryMaster.Template_PK, StockInventoryMaster.CuRate, 
                                                    StockInventoryMaster.Refnum, ISNULL
                                                        ((SELECT        SUM(RequestOrderStockDetails.Qty) AS Expr1
                                                            FROM            RequestOrderStockMaster INNER JOIN
                                                                                     RequestOrderStockDetails ON RequestOrderStockMaster.SRO_Pk = RequestOrderStockDetails.SRO_Pk
                                                            WHERE        (RequestOrderStockMaster.Iscompleted = N'N') AND (RequestOrderStockMaster.IsDeleted = N'N') AND 
                                                                                     (RequestOrderStockDetails.SInventoryItem_PK = StockInventoryMaster.SInventoryItem_PK)), 0) AS BlockedQty
                          FROM            StockInventoryMaster INNER JOIN
                                                    Template_Master ON StockInventoryMaster.Template_PK = Template_Master.Template_PK INNER JOIN
                                                    UOMMaster ON StockInventoryMaster.Uom_PK = UOMMaster.Uom_PK
                          WHERE        (StockInventoryMaster.Location_Pk = @loctn_pk) AND (StockInventoryMaster.OnHandQty > 0) AND (StockInventoryMaster.Template_PK = @temp_pk)) AS ttt", con);



                cmd.Parameters.AddWithValue("@temp_pk", temp_pk);
                cmd.Parameters.AddWithValue("@loctn_pk", Location_pk);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }





        public DataTable GetAtcInventoryInALoc(int atcid, int Location_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        InventoryMaster.InventoryItem_PK, InventoryMaster.SkuDet_Pk, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.OnhandQty, ProcurementDetails_1.POUnitRate, 
                         SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.Template_pk, LocationMaster.LocationPrefix, ProcurementDetails.CURate, InventoryMaster.OnhandQty AS TransferQty
FROM            UOMMaster INNER JOIN
                         InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK INNER JOIN
                         ProcurementDetails AS ProcurementDetails_1 ON InventoryMaster.PoDet_PK = ProcurementDetails_1.PODet_PK INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK
WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (LocationMaster.Location_PK = @loc_pk) AND (InventoryMaster.OnhandQty > 0)
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode", con);
                cmd.Parameters.AddWithValue("@atcid", atcid);

                cmd.Parameters.AddWithValue("@loc_pk", Location_pk);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }








        public DataTable GetReceipt()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        RecieptMaster.RecieptNum, RecieptMaster.ContainerNum, RecieptMaster.BOENum, RecieptMaster.Remark, RecieptMaster.InhouseDate, RecieptMaster.Deliverydate, MrnDetails.ExtraQty, 
                         MrnDetails.Remark AS Expr1, MrnDetails.ReceiptQty, MrnMaster.MrnNum, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName, SkuRawmaterialDetail.RqdQty, 
                         SkuRawmaterialDetail.SupplierSize, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemColor, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS ItemDescription, UOMMaster.UomName, 
                         AtcMaster.AddedDate, RecieptMaster.AddedDate AS Expr2, LocationMaster.LocationName
FROM            RecieptMaster INNER JOIN
                         MrnMaster ON RecieptMaster.Reciept_Pk = MrnMaster.Reciept_Pk INNER JOIN
                         MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                         ProcurementMaster ON MrnMaster.Po_PK = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         SupplierMaster ON RecieptMaster.Supplier_PK = SupplierMaster.Supplier_PK INNER JOIN
                         SkuRawMaterialMaster ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id INNER JOIN
                         SkuRawmaterialDetail ON MrnDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK AND SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         UOMMaster ON MrnDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         LocationMaster ON RecieptMaster.RecptLocation_PK = LocationMaster.Location_PK", con);



                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        public DataTable GetDeliveryReceipt()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        DeliveryReceiptMaster.DORNum, DeliveryReceiptMaster.DOReceiptType, DeliveryReceiptMaster.AddedDate, DeliveryReceiptDetail.ReceivedQty, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS ItemDescription, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor
FROM            DeliveryReceiptMaster INNER JOIN
                         DeliveryReceiptDetail ON DeliveryReceiptMaster.DOR_PK = DeliveryReceiptDetail.DOR_PK INNER JOIN
                         InventoryMaster ON DeliveryReceiptDetail.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK", con);



                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public DataTable GetNonCompletedTransactionofaIIT_PK(int iit_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        RequestOrderMaster.RONum AS dOCNUM, RequestOrderDetails.Qty AS Qty
FROM            RequestOrderMaster INNER JOIN
                         RequestOrderDetails ON RequestOrderMaster.RO_Pk = RequestOrderDetails.RO_Pk
WHERE        (RequestOrderMaster.IsCompleted = N'N') AND (RequestOrderMaster.IsDeleted = N'N') AND (RequestOrderDetails.InventoryItem_PK = @iit_pk)
Union
SELECT        LoanNum AS dOCNUM , LoanQty AS Qty
FROM            InventoryLoanMaster
WHERE        (FromIIT_Pk = @iit_pk) AND (IsApproved = N'N')", con);

                cmd.Parameters.AddWithValue("@iit_pk", iit_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }
        public DataTable GetReceiptofPOItem(int podetpk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        MrnMaster.MrnNum, MrnMaster.AddedDate, MrnDetails.ReceiptQty, MrnDetails.ExtraQty, UOMMaster.UomCode
FROM            MrnMaster INNER JOIN
                         MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                         UOMMaster ON MrnDetails.Uom_PK = UOMMaster.Uom_PK
WHERE        (MrnDetails.PODet_PK = @podetpk)", con);

                cmd.Parameters.AddWithValue("@podetpk", podetpk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

    }



    public class InventoryReportTransaction
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;



        /// <summary>
        /// gets the inventory Of Template of a Atc
        /// Irespective of Location
        /// </summary>
        /// <param name="atcid"></param>
        /// <param name="temp_pk"></param>
        /// <returns></returns>
        public DataTable GetInventoryofAllLocation()
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                         InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value, Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";

                return QueryFunctions.ReturnQueryResultDatatable(cmd);



            }

        }



        /// <summary>
        /// Get fabric inventory of a location
        /// </summary>
        /// <returns></returns>
        public DataTable GetFabricInventoryofLocation()
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                         InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value, ItemGroupMaster.ItemGroupName, Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID
WHERE        (ItemGroupMaster.ItemGroupName = N'Fabric')
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";

                return QueryFunctions.ReturnQueryResultDatatable(cmd);



            }

        }
        /// <summary>
        /// Get the Trim Inventory of a location
        /// </summary>
        /// <returns></returns>
        public DataTable GetTrimInventoryofLocation()
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                         InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value, ItemGroupMaster.ItemGroupName,Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID
   WHERE(ItemGroupMaster.ItemGroupName <> N'Fabric')
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";

                return QueryFunctions.ReturnQueryResultDatatable(cmd);



            }

        }




        /// <summary>
        /// Get the inventory of list of Atc Irespective of Location
        /// </summary>
        /// <param name="jobdetlist"></param>
        /// <returns></returns>

        public DataTable GetInventoryofAtcList(ArrayList atcdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";

            for (int i = 0; i < atcdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " Where  AtcMaster.AtcId =" + atcdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or AtcMaster.AtcId=" + atcdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                         InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value, ItemGroupMaster.ItemGroupName,Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            Template_Master INNER JOIN
                         AtcMaster INNER JOIN
                         SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id ON 
                         Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         LocationMaster INNER JOIN
                         UOMMaster INNER JOIN
                         InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON LocationMaster.Location_PK = InventoryMaster.Location_PK ON 
                         SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk" + condition + " ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = query;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }
            return dt;

        }





        public DataTable GetSamplingFabDetails(int code)
        {

            DataTable dt = new DataTable();
            String query = @"select SamplingFab_PK, Code, Merch_Name, Description, Color, Qty, Width, Supplier, SuperRef, Date, AwbNum, Weight, Unit
FROM            SampleFabricEntryMaster
WHERE        (SamplingFab_PK = @code)";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@code", code);
                cmd.CommandText = query;
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            }
            return dt;
        }














        /// <summary>
        /// Get the inventory of list of Location Irespective of Atc
        /// </summary>
        /// <param name="jobdetlist"></param>
        /// <returns></returns>

        public DataTable GetInventoryofLocList(ArrayList locdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";

            for (int i = 0; i < locdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + "Where  LocationMaster.Location_PK  =" + locdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                         InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value, ItemGroupMaster.ItemGroupName, Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            Template_Master INNER JOIN
                         AtcMaster INNER JOIN
                         SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id ON 
                         Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         LocationMaster INNER JOIN
                         UOMMaster INNER JOIN
                         InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON LocationMaster.Location_PK = InventoryMaster.Location_PK ON 
                         SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk " + condition + "  AND (InventoryMaster.OnhandQty > 0) ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = query;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }
            return dt;

        }







        /// <summary>
        /// Get the inventory of list of Location Irespective of Atc
        /// </summary>
        /// <param name="jobdetlist"></param>
        /// <returns></returns>

        public DataTable GetStockInventoryofLocList(ArrayList locdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";

            for (int i = 0; i < locdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + "Where  LocationMaster.Location_PK  =" + locdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        StockInventoryMaster.SInventoryItem_PK, Template_Master.Description, StockInventoryMaster.Composition, StockInventoryMaster.Construct, StockInventoryMaster.TemplateColor, 
                         StockInventoryMaster.TemplateSize, StockInventoryMaster.TemplateWidth + ' ' + StockInventoryMaster.TemplateWeight AS width, StockInventoryMaster.CURate as Unitprice, StockInventoryMaster.OnHandQty, 
                         UOMMaster.UomName, StockInventoryMaster.Location_Pk, 0.0 AS deliveryqty, StockInventoryMaster.TemplateWidth, StockInventoryMaster.TemplateWeight, StockInventoryMaster.ReceivedVia, 
                         LocationMaster.LocationName,StockInventoryMaster.CURate * StockInventoryMaster.OnHandQty AS PoValue
FROM            StockInventoryMaster INNER JOIN
                         Template_Master ON StockInventoryMaster.Template_PK = Template_Master.Template_PK INNER JOIN
                         UOMMaster ON StockInventoryMaster.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         LocationMaster ON StockInventoryMaster.Location_Pk = LocationMaster.Location_PK " + condition + "  AND (StockInventoryMaster.OnHandQty > 0) ";
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = query;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }
            return dt;

        }
















//        /// <summary>
//        /// Get the inventory of selected Atc  of selected Location 
//        /// </summary>
//        /// <param name="jobdetlist"></param>
//        /// <returns></returns


//        public DataTable GetInventoryofLocListAndAtc(ArrayList atcdetlist, ArrayList locdetlist)
//        {
//            DataTable dt = new DataTable();
//            string condition = "Where (";

//            for (int i = 0; i < atcdetlist.Count; i++)
//            {

//                if (i == 0)
//                {
//                    condition = condition + " AtcMaster.AtcId  =" + atcdetlist[i].ToString().Trim();
//                }
//                else
//                {
//                    condition = condition + "  or AtcMaster.AtcId  =" + atcdetlist[i].ToString().Trim();
//                }






//            }
//            condition = condition + ")";





//            for (int i = 0; i < locdetlist.Count; i++)
//            {
//                if (i == 0)
//                {
//                    if (condition.Trim() == "Where (")
//                    {
//                        condition = condition + " LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
//                    }
//                    else
//                    {
//                        condition = condition + " AND ( LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
//                    }


//                }
//                else
//                {
//                    condition = condition + "  or LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
//                }



//            }

//            condition = condition + ")";






//            if (condition != "Where ()")
//            {
//                String query = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
//                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
//                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
//                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
//                         InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value, ItemGroupMaster.ItemGroupName
//FROM            InventoryMaster INNER JOIN
//                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
//                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
//                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
//                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
//                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
//                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
//                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
//                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID " + condition + " AND (InventoryMaster.OnhandQty > 0)  ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";
//                using (SqlCommand cmd = new SqlCommand())
//                {

//                    cmd.CommandText = query;
//                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

//                }
//            }
//            return dt;

//        }








        /// <summary>
        /// Get the inventory of selected Atc  of selected Location 
        /// </summary>
        /// <param name="jobdetlist"></param>
        /// <returns></returns


        public DataTable GetInventoryofLocListAndAtcAndType(ArrayList atcdetlist, ArrayList locdetlist,String Type)
        {
            DataTable dt = new DataTable();
            string condition = "Where (";

            for (int i = 0; i < atcdetlist.Count; i++)
            {

                if (i == 0)
                {
                    condition = condition + " AtcMaster.AtcId  =" + atcdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or AtcMaster.AtcId  =" + atcdetlist[i].ToString().Trim();
                }






            }
            condition = condition + ")";





            for (int i = 0; i < locdetlist.Count; i++)
            {
                if (i == 0)
                {
                    if (condition.Trim() == "Where (")
                    {
                        condition = condition + " LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + " AND ( LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
                    }


                }
                else
                {
                    condition = condition + "  or LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
                }



            }


           

            condition = condition + ")";






            if (condition != "Where ()")
            {
                String querysub = "";

                if (Type == "All")
                {
                    querysub = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                         InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value, ItemGroupMaster.ItemGroupName,Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            Template_Master INNER JOIN
                         AtcMaster INNER JOIN
                         SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id ON 
                         Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         LocationMaster INNER JOIN
                         UOMMaster INNER JOIN
                         InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON LocationMaster.Location_PK = InventoryMaster.Location_PK ON 
                         SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk " + condition + " AND (InventoryMaster.OnhandQty > 0)  ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";
                }
                else if (Type == "Fabric")
                {
                    querysub = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                         InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value, ItemGroupMaster.ItemGroupName,Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            Template_Master INNER JOIN
                         AtcMaster INNER JOIN
                         SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id ON 
                         Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         LocationMaster INNER JOIN
                         UOMMaster INNER JOIN
                         InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON LocationMaster.Location_PK = InventoryMaster.Location_PK ON 
                         SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk " + condition + " AND (InventoryMaster.OnhandQty > 0) and  (ItemGroupMaster.ItemGroupName = N'Fabric')  ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";

                }
                else if (Type == "Trim")
                {
                    querysub = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                         InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value, ItemGroupMaster.ItemGroupName,Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            Template_Master INNER JOIN
                         AtcMaster INNER JOIN
                         SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id ON 
                         Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         LocationMaster INNER JOIN
                         UOMMaster INNER JOIN
                         InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON LocationMaster.Location_PK = InventoryMaster.Location_PK ON 
                         SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk " + condition + " AND (InventoryMaster.OnhandQty > 0)  and  (ItemGroupMaster.ItemGroupName <> N'Fabric') ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";

                }






            using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = querysub;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }
            return dt;

        }


















        /// <summary>
        /// Get the inventory of list of Atc Irespective of Location
        /// </summary>
        /// <param name="jobdetlist"></param>
        /// <returns></returns>

        public DataTable GetFabricInventoryofAtcList(ArrayList atcdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";

            for (int i = 0; i < atcdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + "Where ( AtcMaster.AtcId =" + atcdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or AtcMaster.AtcId=" + atcdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                         InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value, ItemGroupMaster.ItemGroupName,Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            Template_Master INNER JOIN
                         AtcMaster INNER JOIN
                         SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id ON 
                         Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         LocationMaster INNER JOIN
                         UOMMaster INNER JOIN
                         InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON LocationMaster.Location_PK = InventoryMaster.Location_PK ON 
                         SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk " + condition + ") and (ItemGroupMaster.ItemGroupName = N'Fabric') AND (InventoryMaster.OnhandQty > 0) ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = query;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }
            return dt;

        }


        /// <summary>
        /// Get the inventory of list of Atc Irespective of Location
        /// </summary>
        /// <param name="jobdetlist"></param>
        /// <returns></returns>

        public DataTable GetTrimInventoryofAtcList(ArrayList atcdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";

            for (int i = 0; i < atcdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + "Where ( AtcMaster.AtcId =" + atcdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or AtcMaster.AtcId=" + atcdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                        ( InventoryMaster.CURate * InventoryMaster.OnhandQty) AS Value, ItemGroupMaster.ItemGroupName,Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID " + condition + ") and (ItemGroupMaster.ItemGroupName <> N'Fabric') AND (InventoryMaster.OnhandQty > 0)  ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = query;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }
            return dt;

        }




        /// <summary>
        /// Get the inventory of list of Atc Irespective of Location WithOnhandZeroAlso
        /// </summary>
        /// <param name="jobdetlist"></param>
        /// <returns></returns>

        public DataTable GetTrimInventoryofAtcListWithOnhandZeroAlso(ArrayList atcdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";

            for (int i = 0; i < atcdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + "Where ( AtcMaster.AtcId =" + atcdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or AtcMaster.AtcId=" + atcdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                        ( InventoryMaster.CURate * InventoryMaster.OnhandQty) AS Value, ItemGroupMaster.ItemGroupName,Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID " + condition + ") and (ItemGroupMaster.ItemGroupName <> N'Fabric')   ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = query;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }
            return dt;

        }



        /// Get the inventory of list of Atc Irespective of Location WithOnhandZeroAlso
        /// </summary>
        /// <param name="jobdetlist"></param>
        /// <returns></returns>

        public DataTable GetFabricInventoryofAtcListWithOnhandZeroAlso(ArrayList atcdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";

            for (int i = 0; i < atcdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + "Where ( AtcMaster.AtcId =" + atcdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or AtcMaster.AtcId=" + atcdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                         InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value, ItemGroupMaster.ItemGroupName,Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID " + condition + ") and (ItemGroupMaster.ItemGroupName = N'Fabric')  ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = query;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }
            return dt;

        }



        /// <summary>
        /// Get the inventory of list of Atc Irespective of Location
        /// </summary>
        /// <param name="jobdetlist"></param>
        /// <returns></returns>

        public DataTable GetGoodsIntransistofAtcList(ArrayList atcdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";

            for (int i = 0; i < atcdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + "Where ( AtcMaster.AtcId =" + atcdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or AtcMaster.AtcId=" + atcdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                condition = condition + ")";
                String query = @"SELECT        tt.Gt_PK, tt.AtcNum, tt.RMNum, tt.Description, tt.ItemColor, tt.ItemSize, tt.SupplierSize, tt.SupplierColor, tt.TransitQty, tt.UomCode, tt.CURate, tt.Value, tt.ItemGroupName, tt.DONum, tt.LocationName, 
                         LocationMaster_1.LocationName AS FromLocation, DeliveryOrderMaster_1.ContainerNumber
FROM            (SELECT        TOP (100) PERCENT GoodsInTransit.Gt_PK, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, GoodsInTransit.TransitQty, UOMMaster.UomCode, InventoryMaster.CURate, 
                         InventoryMaster.CURate * GoodsInTransit.TransitQty AS Value, ItemGroupMaster.ItemGroupName, DeliveryOrderMaster.DONum, LocationMaster.LocationName
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         GoodsInTransit ON InventoryMaster.InventoryItem_PK = GoodsInTransit.InventoryItem_PK INNER JOIN
                         DeliveryOrderMaster ON GoodsInTransit.DO_PK = DeliveryOrderMaster.DO_PK INNER JOIN
                         LocationMaster ON DeliveryOrderMaster.ToLocation_PK = LocationMaster.Location_PK  " + condition + " ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode) AS tt INNER JOIN  DeliveryOrderMaster AS DeliveryOrderMaster_1 ON tt.DONum = DeliveryOrderMaster_1.DONum INNER JOIN   LocationMaster AS LocationMaster_1 ON DeliveryOrderMaster_1.FromLocation_PK = LocationMaster_1.Location_PK where (tt.TransitQty > 0) ";
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = query;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }
            return dt;

        }


















        /// <summary>
        /// Get the TRIM inventory of list of Location 
        /// </summary>
        /// <param name="jobdetlist"></param>
        /// <returns></returns>

        public DataTable GetTRIMInventoryofLocList(ArrayList locdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";

            for (int i = 0; i < locdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + "Where ( LocationMaster.Location_PK  =" + locdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                         (InventoryMaster.CURate * InventoryMaster.OnhandQty) AS Value, ItemGroupMaster.ItemGroupName,Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID " + condition + ") and (ItemGroupMaster.ItemGroupName <> N'Fabric')  AND (InventoryMaster.OnhandQty > 0)  ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = query;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }
            return dt;

        }


        /// <summary>
        /// Get the TRIM inventory of list of Location 
        /// </summary>
        /// <param name="jobdetlist"></param>
        /// <returns></returns>

        public DataTable GetFABInventoryofLocList(ArrayList locdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";

            for (int i = 0; i < locdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + "Where ( LocationMaster.Location_PK  =" + locdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, 
                         InventoryMaster.OnhandQty, AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, 
                         InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value, ItemGroupMaster.ItemGroupName,Template_Master.TemplateCode, Template_Master.Description AS Templatename
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID " + condition + ") and (ItemGroupMaster.ItemGroupName = N'Fabric') AND (InventoryMaster.OnhandQty > 0) ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = query;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }
            return dt;

        }



        /// <summary>
        /// GET THE PURCHASE REPORT
        /// </summary>
        /// <param name="fromMrnpk"></param>
        /// <param name="tomrnpk"></param>
        /// <returns></returns>

        public DataTable GetpURCHASEREPORT(int fromMrnpk, int tomrnpk)
        {
            DataTable dt = new DataTable();


            String query = @"SELECT        MrnMaster.MrnNum, ProcurementMaster.PONum, ProcurementDetails.CURate, AtcMaster.AtcNum, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Construction, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Weight, SkuRawMaterialMaster.Width, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, Template_Master.Description, SupplierMaster.SupplierName, 
                         UOMMaster.UomCode, LocationMaster.LocationName, MrnDetails.ReceiptQty, MrnDetails.ExtraQty, MrnMaster.AddedDate, MrnMaster.Mrn_PK
FROM            MrnMaster INNER JOIN
                         MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                         ProcurementMaster ON MrnMaster.Po_PK = ProcurementMaster.PO_Pk INNER JOIN
                         ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         SkuRawmaterialDetail ON MrnDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK AND ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         LocationMaster ON MrnMaster.Location_Pk = LocationMaster.Location_PK
WHERE        (MrnMaster.Mrn_PK BETWEEN @param1 AND @Param2)";
            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.Parameters.AddWithValue("@param1", fromMrnpk);
                cmd.Parameters.AddWithValue("@Param2", tomrnpk);

                cmd.CommandText = query;
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            }
            return dt;
        }







        /// <summary>
        /// GET THE PURCHASE REPORT
        /// </summary>
        /// <param name="fromMrnpk"></param>
        /// <param name="tomrnpk"></param>
        /// <returns></returns>

        public DataTable GetPurChaseReportOfATC(ArrayList atcdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";

            for (int i = 0; i < atcdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + "Where ( ProcurementMaster.AtcId =" + atcdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or ProcurementMaster.AtcId=" + atcdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                condition = condition + ")";
                String query = @"SELECT        MrnMaster.MrnNum, ProcurementMaster.PONum, ProcurementDetails.CURate, AtcMaster.AtcNum, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Construction, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Weight, SkuRawMaterialMaster.Width, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, Template_Master.Description, SupplierMaster.SupplierName, 
                         UOMMaster.UomCode, LocationMaster.LocationName, MrnDetails.ReceiptQty, MrnDetails.ExtraQty, MrnMaster.AddedDate, MrnMaster.Mrn_PK
FROM            MrnMaster INNER JOIN
                         MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                         ProcurementMaster ON MrnMaster.Po_PK = ProcurementMaster.PO_Pk INNER JOIN
                         ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         SkuRawmaterialDetail ON MrnDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK AND ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         LocationMaster ON MrnMaster.Location_Pk = LocationMaster.Location_PK  " + condition + "  ";
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = query;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }

            return dt;

        }













        /// <summary>
        /// GET ALL GOODS IN TRANSIST
        /// </summary>
        /// <returns></returns>
        public DataTable GetGoodsintransist(int lctn_PK)
        {
            DataTable dt = new DataTable();


            using (SqlCommand cmd = new SqlCommand())
            {




                cmd.CommandText = "ShowGoodsinTransit_SP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LocationPK", lctn_PK);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

            }
            return dt;
        }







        /// <summary>
        /// GET ALL Gstock 
        /// /// </summary>
        /// <returns></returns>
        public DataTable GetGstockofLocation(int loc_pk)
        {
            DataTable dt = new DataTable();


            String query = @"SELECT        StockInventoryMaster.SInventoryItem_PK, Template_Master.Description, StockInventoryMaster.Composition, StockInventoryMaster.Construct, StockInventoryMaster.TemplateColor, 
                         StockInventoryMaster.TemplateSize, StockInventoryMaster.TemplateWidth + ' ' + StockInventoryMaster.TemplateWeight AS width, StockInventoryMaster.Unitprice, StockInventoryMaster.OnHandQty, 
                         UOMMaster.UomName, StockInventoryMaster.Location_Pk, 0.0 AS deliveryqty, StockInventoryMaster.TemplateWidth, StockInventoryMaster.TemplateWeight, StockInventoryMaster.ReceivedVia, 
                         LocationMaster.LocationName
FROM            StockInventoryMaster INNER JOIN
                         Template_Master ON StockInventoryMaster.Template_PK = Template_Master.Template_PK INNER JOIN
                         UOMMaster ON StockInventoryMaster.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         LocationMaster ON StockInventoryMaster.Location_Pk = LocationMaster.Location_PK
WHERE        (StockInventoryMaster.Location_Pk = 6) AND (StockInventoryMaster.OnHandQty > 0) AND (LocationMaster.LocationName = @Param1)";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@Param1", loc_pk);


                cmd.CommandText = query;
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            }
            return dt;
        }







        /// <summary>
        /// get the Missplaced Inventory details against a request
        /// </summary>
        /// <param name="reqnum"></param>
        /// <returns></returns>
        public DataTable GetInventoryMisplaced(int reqnum)
        {
            DataTable dt = new DataTable();


            String query = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, ISNULL(SkuRawMaterialMaster.Composition, '') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, '') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Weight, '') + ' ' + ISNULL(SkuRawMaterialMaster.Width, '') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, '') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, '') 
                         + '' + ISNULL(ProcurementDetails.SupplierSize, '') + '' + ProcurementDetails.SupplierColor AS Description, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.OnhandQty, 
                         LocationMaster.LocationName, InventoryMissingRequest.MisplaceDate, InventoryMissingRequest.Explanation, InventoryMissingRequest.AddedBy, InventoryMissingRequest.Addeddate, 
                         InventoryMissingRequest.Level1Approval, ISNULL(InventoryMissingRequest.reqnum, '') AS Reqnum, InventoryMissingDetails.MisplaceAppDet_PK, InventoryMissingDetails.Qty AS DeliveredQty, 
                         InventoryMissingRequest.reqnum AS reqnum, isnull( InventoryMissingRequest.Level1ApprovedBY,'') as Level1ApprovedBY, InventoryMissingRequest.IsApproved,  isnull( InventoryMissingRequest.ApprovedBy,'') as ApprovedBy, 
                         isnull( InventoryMissingRequest.L1ApprovedDate,'') as L1ApprovedDate,isnull( InventoryMissingRequest.ApprovedDate,'') as ApprovedDate , InventoryMaster.CURate,(InventoryMaster.CURate*InventoryMissingDetails.Qty) as MisplacedValue, InventoryMissingRequest.Reason
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         InventoryMissingDetails ON InventoryMaster.InventoryItem_PK = InventoryMissingDetails.InventoryItem_PK INNER JOIN
                         InventoryMissingRequest ON InventoryMissingDetails.MisplaceApp_PK = InventoryMissingRequest.MisplaceApp_pk INNER JOIN
                         LocationMaster ON InventoryMissingRequest.FromLctn_pk = LocationMaster.Location_PK
WHERE        (InventoryMissingRequest.MisplaceApp_pk = @Param1)
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode";
            using (SqlCommand cmd = new SqlCommand())
            {



                cmd.Parameters.AddWithValue("@Param1", reqnum);
                cmd.CommandText = query;
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            }
            return dt;
        }



        public DataTable GetInventoryMisplacedofAtc(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = @"MisPlacedInventoryOfAtc_SP";
                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.CommandType = CommandType.StoredProcedure;

                return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



            }

        }

        public DataTable GetInventoryMisplacedofFactory(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = @"MisPlacedInventoryOfFactory_SP";
                cmd.Parameters.AddWithValue("@@facid", atcid);
                cmd.CommandType = CommandType.StoredProcedure;

                return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



            }

        }



        /// <summary>
        /// get the Wrong PO details against a request
        /// </summary>
        /// <param name="reqnum"></param>
        /// <returns></returns>
        public DataTable GetWrongPOdetails(int reqnum)
        {
            DataTable dt = new DataTable();


            String query = @"SELECT        ProcurementDetails.PODet_PK, SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                         Template_Master.Description + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Width + ' ' + SkuRawMaterialMaster.Weight AS Description, 
                         (CASE ProcurementDetails.SupplierColor WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE ProcurementDetails.SupplierColor END) AS SupplierColor, 
                         (CASE ProcurementDetails.SupplierSize WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE ProcurementDetails.SupplierSize END) AS SupplierSize, 
                         (CASE SkuRawmaterialDetail.ItemSize WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE SkuRawmaterialDetail.ItemSize END) AS ItemSize, 
                         (CASE SkuRawmaterialDetail.ItemColor WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE SkuRawmaterialDetail.ItemColor END) AS ItemColor, ProcurementDetails.POQty, UOMMaster.UomCode, 
                         ProcurementDetails.POUnitRate, CAST(ProcurementDetails.POQty * ProcurementDetails.POUnitRate AS decimal(18, 2)) AS POvalue, ProcurementMaster.PO_Pk, ProcurementDetails.CURate, WrongPODetail.Qty, 
                         WrongPOMaster.MerchandiserName, WrongPOMaster.Explanation, WrongPOMaster.IsApproved, WrongPOMaster.Reqnum, WrongPOMaster.WrongPO_Pk
FROM            ProcurementMaster INNER JOIN
                         ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         WrongPODetail ON ProcurementDetails.PODet_PK = WrongPODetail.Podet_PK INNER JOIN
                         WrongPOMaster ON ProcurementMaster.PO_Pk = WrongPOMaster.PO_PK AND WrongPODetail.WrongPO_Pk = WrongPOMaster.WrongPO_Pk
WHERE        (WrongPOMaster.WrongPO_Pk = @reqnum)";
            using (SqlCommand cmd = new SqlCommand())
            {



                cmd.Parameters.AddWithValue("@reqnum", reqnum);
                cmd.CommandText = query;
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            }
            return dt;
        }







        /// <summary>
        /// get the Wrong PO details against a request
        /// </summary>
        /// <param name="reqnum"></param>
        /// <returns></returns>
        public DataTable GetExtraBOMRequest(int reqnum)
        {
            DataTable dt = new DataTable();


            String query = @"SELECT        AtcMaster.AtcNum, ExtraBOMRequestMaster.Reqnum, ExtraBOMRequestMaster.IsApproved, ExtraBOMRequestMaster.Explanation, ExtraBOMRequestMaster.MerchandiserName, ExtraBOMRequestDetail.Qty, 
                         ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') AS itemDescription, ExtraBOMRequestMaster.ExtraBOM_PK, UOMMaster.UomCode
FROM            ExtraBOMRequestDetail INNER JOIN
                         ExtraBOMRequestMaster ON ExtraBOMRequestDetail.ExtraBOM_PK = ExtraBOMRequestMaster.ExtraBOM_PK INNER JOIN
                         SkuRawmaterialDetail ON ExtraBOMRequestDetail.Skudet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         UOMMaster ON SkuRawMaterialMaster.Uom_PK = UOMMaster.Uom_PK
WHERE        (ExtraBOMRequestMaster.ExtraBOM_PK = @reqnum)";
            using (SqlCommand cmd = new SqlCommand())
            {



                cmd.Parameters.AddWithValue("@reqnum", reqnum);
                cmd.CommandText = query;
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            }
            return dt;
        }











    }


    public class RecieptReportTransaction
    {

        /// <summary>
        /// Get fabric inventory of a location
        /// </summary>
        /// <returns></returns>
        public DataTable GetMRNData(int mrn_pk)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = @"GetMRNData_SP";
                cmd.Parameters.AddWithValue("@Mrn_pk", mrn_pk);
                cmd.CommandType = CommandType.StoredProcedure;

                return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



            }

        }
    }





    public class InventoryReportConsolidatedTransaction
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;




































        /// <summary>
        /// Get the inventory of selected Atc  of selected Location 
        /// </summary>
        /// <param name="jobdetlist"></param>
        /// <returns></returns


        public DataTable GetInventoryofLocListAndAtcAndType(ArrayList atcdetlist, ArrayList locdetlist, String Type)
        {
            DataTable dt = new DataTable();
            string condition = "Where (";

            for (int i = 0; i < atcdetlist.Count; i++)
            {

                if (i == 0)
                {
                    condition = condition + " AtcMaster.AtcId  =" + atcdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or AtcMaster.AtcId  =" + atcdetlist[i].ToString().Trim();
                }






            }
            condition = condition + ")";





            for (int i = 0; i < locdetlist.Count; i++)
            {
                if (i == 0)
                {
                    if (condition.Trim() == "Where (")
                    {
                        condition = condition + " LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + " AND ( LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
                    }


                }
                else
                {
                    condition = condition + "  or LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
                }



            }




            condition = condition + ")";






            if (condition != "Where ()")
            {
                String querysub = "";

                if (Type == "All")
                {
                    querysub = @"SELECT        AtcNum, LocationName, Description, SUM(OnhandQty) AS OnhandQty, UomCode, ItemName, ItemGroupName, SUM(PoValue) AS PoValue, LocationAddress, CURate
FROM            (SELECT        AtcMaster.AtcNum, LocationMaster.LocationName, ISNULL(SkuRawMaterialMaster.RMNum, ' ') + '  ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
                                                    + '  ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + '  ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                                                    + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') 
                                                    + '  ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS Description, InventoryMaster.OnhandQty, UOMMaster.UomCode, InventoryMaster.CURate * InventoryMaster.OnhandQty AS PoValue, 
                                                    Template_Master.Description AS ItemName, ItemGroupMaster.ItemGroupName, LocationMaster.LocationAddress, InventoryMaster.CURate
                          FROM            InventoryMaster INNER JOIN
                                                    SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                                                    SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                                                    ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                                                    LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                                                    AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                                                    UOMMaster ON SkuRawMaterialMaster.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                                                    Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                                                    ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID 
                                " + condition + @" and (InventoryMaster.OnhandQty > 0) ) AS tt
GROUP BY AtcNum, LocationName, Description, UomCode, ItemName, ItemGroupName, LocationAddress, CURate";
                }
                else if (Type == "Fabric")
                {
                    querysub = @"SELECT        AtcNum, LocationName, Description, SUM(OnhandQty) AS OnhandQty, UomCode, ItemName, ItemGroupName, SUM(PoValue) AS PoValue, LocationAddress, CURate
FROM            (SELECT        AtcMaster.AtcNum, LocationMaster.LocationName, ISNULL(SkuRawMaterialMaster.RMNum, ' ') + '  ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
                                                    + '  ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + '  ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                                                    + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') 
                                                    + '  ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS Description, InventoryMaster.OnhandQty, UOMMaster.UomCode, InventoryMaster.CURate * InventoryMaster.OnhandQty AS PoValue, 
                                                    Template_Master.Description AS ItemName, ItemGroupMaster.ItemGroupName, LocationMaster.LocationAddress, InventoryMaster.CURate
                          FROM            InventoryMaster INNER JOIN
                                                    SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                                                    SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                                                    ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                                                    LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                                                    AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                                                    UOMMaster ON SkuRawMaterialMaster.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                                                    Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                                                    ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID " + condition + @" AND (InventoryMaster.OnhandQty > 0) and  (ItemGroupMaster.ItemGroupName = N'Fabric') ) AS tt  GROUP BY AtcNum, LocationName, Description, UomCode, ItemName, ItemGroupName, LocationAddress, CURate";

                }
                else if (Type == "Trim")
                {
                    querysub = @"SELECT        AtcNum, LocationName, Description, SUM(OnhandQty) AS OnhandQty, UomCode, ItemName, ItemGroupName, SUM(PoValue) AS PoValue, LocationAddress, CURate
FROM            (SELECT        AtcMaster.AtcNum, LocationMaster.LocationName, ISNULL(SkuRawMaterialMaster.RMNum, ' ') + '  ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
                                                    + '  ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + '  ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                                                    + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') 
                                                    + '  ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS Description, InventoryMaster.OnhandQty, UOMMaster.UomCode, InventoryMaster.CURate * InventoryMaster.OnhandQty AS PoValue, 
                                                    Template_Master.Description AS ItemName, ItemGroupMaster.ItemGroupName, LocationMaster.LocationAddress, InventoryMaster.CURate
                          FROM            InventoryMaster INNER JOIN
                                                    SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                                                    SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                                                    ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                                                    LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                                                    AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                                                    UOMMaster ON SkuRawMaterialMaster.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                                                    Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                                                    ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID " + condition + " AND (InventoryMaster.OnhandQty > 0)  and  (ItemGroupMaster.ItemGroupName = N'Trims')) AS tt  GROUP BY AtcNum, LocationName, Description, UomCode, ItemName, ItemGroupName, LocationAddress,CURate";

                }






                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = querysub;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }
            return dt;

        }








        /// <summary>
        /// Get the inventory of selected Atc  of selected Location 
        /// </summary>
        /// <param name="jobdetlist"></param>
        /// <returns></returns


        public DataTable GetInventoryofLocListAndAtcAndTypeWithDate(ArrayList atcdetlist, ArrayList locdetlist, String Type)
        {
            DataTable dt = new DataTable();
            string condition = "Where (";

            for (int i = 0; i < atcdetlist.Count; i++)
            {

                if (i == 0)
                {
                    condition = condition + " AtcMaster.AtcId  =" + atcdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or AtcMaster.AtcId  =" + atcdetlist[i].ToString().Trim();
                }






            }
            condition = condition + ")";





            for (int i = 0; i < locdetlist.Count; i++)
            {
                if (i == 0)
                {
                    if (condition.Trim() == "Where (")
                    {
                        condition = condition + " LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + " AND ( LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
                    }


                }
                else
                {
                    condition = condition + "  or LocationMaster.Location_PK =" + locdetlist[i].ToString().Trim();
                }



            }




            condition = condition + ")";






            if (condition != "Where ()")
            {
                String querysub = "";

                if (Type == "All")
                {
                    querysub = @"SELECT        AtcNum, LocationName, Description, SUM(OnhandQty) AS OnhandQty, UomCode, ItemName, ItemGroupName, SUM(PoValue) AS PoValue, LocationAddress, CURate
FROM            (SELECT        AtcMaster.AtcNum, LocationMaster.LocationName, ISNULL(SkuRawMaterialMaster.RMNum, ' ') + '  ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
                                                    + '  ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + '  ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                                                    + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') 
                                                    + '  ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS Description, InventoryMaster.OnhandQty, UOMMaster.UomCode, InventoryMaster.CURate * InventoryMaster.OnhandQty AS PoValue, 
                                                    Template_Master.Description AS ItemName, ItemGroupMaster.ItemGroupName, LocationMaster.LocationAddress, InventoryMaster.CURate
                          FROM            InventoryMaster INNER JOIN
                                                    SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                                                    SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                                                    ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                                                    LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                                                    AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                                                    UOMMaster ON SkuRawMaterialMaster.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                                                    Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                                                    ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID 
                                " + condition + @" and (InventoryMaster.OnhandQty > 0) ) AS tt
GROUP BY AtcNum, LocationName, Description, UomCode, ItemName, ItemGroupName, LocationAddress, CURate";
                }
                else if (Type == "Fabric")
                {
                    querysub = @"SELECT        AtcNum, LocationName, Description, SUM(OnhandQty) AS OnhandQty, UomCode, ItemName, ItemGroupName, SUM(PoValue) AS PoValue, LocationAddress, CURate, MAX(AddedDate) AS AddedDate
FROM            (SELECT        AtcMaster.AtcNum, LocationMaster.LocationName, ISNULL(SkuRawMaterialMaster.RMNum, ' ') + '  ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
                                                    + '  ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + '  ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                                                    + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') 
                                                    + '  ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS Description, InventoryMaster.OnhandQty, UOMMaster.UomCode, InventoryMaster.CURate * InventoryMaster.OnhandQty AS PoValue, 
                                                    Template_Master.Description AS ItemName, ItemGroupMaster.ItemGroupName, LocationMaster.LocationAddress, InventoryMaster.CURate, InventoryMaster.AddedDate
                          FROM            InventoryMaster INNER JOIN
                                                    SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                                                    SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                                                    ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                                                    LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                                                    AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                                                    UOMMaster ON SkuRawMaterialMaster.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                                                    Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                                                    ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID " + condition + @" AND (InventoryMaster.OnhandQty > 0) and  (ItemGroupMaster.ItemGroupName = N'Fabric') ) AS tt  GROUP BY AtcNum, LocationName, Description, UomCode, ItemName, ItemGroupName, LocationAddress, CURate";

                }
                else if (Type == "Trim")
                {
                    querysub = @"SELECT        AtcNum, LocationName, Description, SUM(OnhandQty) AS OnhandQty, UomCode, ItemName, ItemGroupName, SUM(PoValue) AS PoValue, LocationAddress, CURate, MAX(AddedDate) AS AddedDate
FROM            (SELECT        AtcMaster.AtcNum, LocationMaster.LocationName, ISNULL(SkuRawMaterialMaster.RMNum, ' ') + '  ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
                                                    + '  ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + '  ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                                                    + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') 
                                                    + '  ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS Description, InventoryMaster.OnhandQty, UOMMaster.UomCode, InventoryMaster.CURate * InventoryMaster.OnhandQty AS PoValue, 
                                                    Template_Master.Description AS ItemName, ItemGroupMaster.ItemGroupName, LocationMaster.LocationAddress, InventoryMaster.CURate, InventoryMaster.AddedDate
                          FROM            InventoryMaster INNER JOIN
                                                    SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                                                    SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                                                    ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                                                    LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                                                    AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                                                    UOMMaster ON SkuRawMaterialMaster.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                                                    Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                                                    ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID " + condition + " AND (InventoryMaster.OnhandQty > 0)  and  (ItemGroupMaster.ItemGroupName = N'Trims')) AS tt  GROUP BY AtcNum, LocationName, Description, UomCode, ItemName, ItemGroupName, LocationAddress,CURate";

                }






                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = querysub;
                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                }
            }
            return dt;

        }





















    }








}