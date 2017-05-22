using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ArtWebApp.DataModels;
using System.Web.UI.WebControls;

namespace ArtWebApp.BLL.InventoryBLL
{
    public static class FactoryInventory
    {
        static String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;


        public static DataTable GetBOM(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                        Template_Master.Description + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, ColorMaster.ColorName, 
                         SizeMaster.SizeName, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize, UOMMaster.UomCode, 
                         Template_Master.ItemGroup_PK,SkuRawMaterialMaster.Sku_Pk
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON SkuRawMaterialMaster.AltUom_pk = UOMMaster.Uom_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ColorMaster ON SkuRawmaterialDetail.ColorCode = ColorMaster.ColorCode LEFT OUTER JOIN
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


        public static System.Data.DataTable GetMRNetails(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        MrnMaster.MrnNum, MrnDetails.ReceiptQty, MrnDetails.ExtraQty, ProcurementMaster.PONum, UOMMaster.UomCode, ProcurementMaster.AtcId, MrnDetails.SkuDet_PK, LocationMaster.LocationPrefix, 
                         LocationMaster.Location_PK
FROM            MrnMaster INNER JOIN
                         MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                         ProcurementDetails ON MrnDetails.PODet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         UOMMaster ON MrnDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         LocationMaster ON MrnMaster.Location_Pk = LocationMaster.Location_PK
WHERE        (ProcurementMaster.AtcId  = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }


        public static System.Data.DataTable GetDOR(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"
SELECT        DeliveryReceiptMaster.DOR_PK, DeliveryReceiptMaster.DORNum, LocationMaster.Location_PK, LocationMaster.LocationPrefix, DeliveryReceiptDetail.ReceivedQty, UOMMaster.UomCode, 
                         DeliveryOrderMaster.AtcID, DeliveryOrderMaster.DONum, InventoryMaster.SkuDet_Pk
FROM            DeliveryReceiptMaster INNER JOIN
                         DeliveryReceiptDetail ON DeliveryReceiptMaster.DOR_PK = DeliveryReceiptDetail.DOR_PK INNER JOIN
                         InventoryMaster ON DeliveryReceiptDetail.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         LocationMaster ON DeliveryReceiptMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         UOMMaster ON InventoryMaster.Uom_Pk = UOMMaster.Uom_PK INNER JOIN
                         DeliveryOrderMaster ON DeliveryReceiptMaster.DO_PK = DeliveryOrderMaster.DO_PK
WHERE        (DeliveryOrderMaster.AtcID = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }

        public static System.Data.DataTable GetLoanDetails(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"
SELECT        InventoryMaster.ReceivedVia, InventoryMaster.ReceivedQty, InventoryMaster.Refnum, LocationMaster.LocationPrefix, LocationMaster.Location_PK, InventoryMaster.SkuDet_Pk, 
                         SkuRawMaterialMaster.Atc_id
FROM            InventoryMaster INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
WHERE        (InventoryMaster.ReceivedVia = N'LN') AND (SkuRawMaterialMaster.Atc_id =@ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }

        public static System.Data.DataTable GetDODetails(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"
SELECT        DeliveryOrderMaster.DONum, DeliveryOrderDetails.DeliveryQty, LocationMaster.Location_PK AS fromLocation_PK, LocationMaster.LocationPrefix AS frmLocationPrefix, 
                         LocationMaster_1.LocationPrefix AS TOLocationPrefix, LocationMaster_1.Location_PK AS ToLocation_PK, InventoryMaster.SkuDet_Pk, DeliveryOrderMaster.AtcID, DeliveryOrderMaster.DoType
FROM            DeliveryOrderMaster INNER JOIN
                         DeliveryOrderDetails ON DeliveryOrderMaster.DO_PK = DeliveryOrderDetails.DO_PK INNER JOIN
                         LocationMaster ON DeliveryOrderMaster.FromLocation_PK = LocationMaster.Location_PK INNER JOIN
                         LocationMaster AS LocationMaster_1 ON DeliveryOrderMaster.ToLocation_PK = LocationMaster_1.Location_PK INNER JOIN
                         InventoryMaster ON DeliveryOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK
WHERE        (DeliveryOrderMaster.AtcID = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }


        public static System.Data.DataTable GetLoanOutDetails(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"
SELECT        LocationMaster.Location_PK, LocationMaster.LocationPrefix, InventoryLoanMaster.LoanNum, InventoryLoanMaster.LoanQty, InventoryLoanMaster.IsApproved, SkuRawmaterialDetail.SkuDet_PK, 
                         SkuRawMaterialMaster.Atc_id
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         InventoryLoanMaster INNER JOIN
                         LocationMaster ON InventoryLoanMaster.Location_PK = LocationMaster.Location_PK ON SkuRawmaterialDetail.SkuDet_PK = InventoryLoanMaster.FromSkudet_PK
WHERE        (SkuRawMaterialMaster.Atc_id = @ATCID)
";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }



        public static System.Data.DataTable GetROOutDetails(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        RequestOrderMaster.RONum, RequestOrderDetails.Qty, LocationMaster.Location_PK, LocationMaster.LocationPrefix, RequestOrderMaster.IsApproved, InventoryMaster.SkuDet_Pk, 
                         SkuRawMaterialMaster.Atc_id
FROM            RequestOrderMaster INNER JOIN
                         RequestOrderDetails ON RequestOrderMaster.RO_Pk = RequestOrderDetails.RO_Pk INNER JOIN
                         InventoryMaster ON RequestOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
WHERE        (SkuRawMaterialMaster.Atc_id = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }



        public static System.Data.DataTable GetallReceipt(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        MrnMaster.MrnNum, (MrnDetails.ReceiptQty + MrnDetails.ExtraQty) as Qty, UOMMaster.UomCode,ProcurementMaster.PONum, LocationMaster.LocationPrefix, MrnDetails.SkuDet_PK, 
                         LocationMaster.Location_PK
FROM            MrnMaster INNER JOIN
                         MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                         ProcurementDetails ON MrnDetails.PODet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         UOMMaster ON MrnDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         LocationMaster ON MrnMaster.Location_Pk = LocationMaster.Location_PK
WHERE        (ProcurementMaster.AtcId  = @ATCID)
union

SELECT        DeliveryReceiptMaster.DORNum, DeliveryReceiptDetail.ReceivedQty,  UOMMaster.UomCode, DeliveryOrderMaster.DONum,  LocationMaster.LocationPrefix,
                      InventoryMaster.SkuDet_Pk,  LocationMaster.Location_PK
FROM            DeliveryReceiptMaster INNER JOIN
                         DeliveryReceiptDetail ON DeliveryReceiptMaster.DOR_PK = DeliveryReceiptDetail.DOR_PK INNER JOIN
                         InventoryMaster ON DeliveryReceiptDetail.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         LocationMaster ON DeliveryReceiptMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         UOMMaster ON InventoryMaster.Uom_Pk = UOMMaster.Uom_PK INNER JOIN
                         DeliveryOrderMaster ON DeliveryReceiptMaster.DO_PK = DeliveryOrderMaster.DO_PK
WHERE        (DeliveryOrderMaster.AtcID = @ATCID)

union

SELECT        InventoryMaster.ReceivedVia, InventoryMaster.ReceivedQty,  UOMMaster.UomCode,InventoryMaster.Refnum, LocationMaster.LocationPrefix, InventoryMaster.SkuDet_Pk, LocationMaster.Location_PK
                        
FROM            InventoryMaster INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON InventoryMaster.Uom_Pk = UOMMaster.Uom_PK
WHERE        (InventoryMaster.ReceivedVia = N'LN') AND (SkuRawMaterialMaster.Atc_id =@ATCID)


";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }


        public static System.Data.DataTable GetCutorderDO(int ATCID)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"
SELECT        DeliveryOrderMaster.DONum, CutOrderDO.Skudet_PK, CutOrderDO.DeliveryQty, CutOrderMaster.Cut_NO, CutOrderMaster.AtcID
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID INNER JOIN
                         DeliveryOrderDetails ON CutOrderDO.DoDet_Pk = DeliveryOrderDetails.DODet_PK INNER JOIN
                         DeliveryOrderMaster ON DeliveryOrderDetails.DO_PK = DeliveryOrderMaster.DO_PK
WHERE        (CutOrderMaster.AtcID = @ATCID)";



                cmd.Parameters.AddWithValue("@ATCID", ATCID);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }

    }
}