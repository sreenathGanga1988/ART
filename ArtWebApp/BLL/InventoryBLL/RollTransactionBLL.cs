using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ArtWebApp.DataModels;
using System.Collections;

namespace ArtWebApp.BLL.InventoryBLL
{
    public static class RollTransactionBLL
    {
        /// <summary>
        /// get all the fanbric details of a mrn
        /// </summary>
        /// <param name="mrn_pk"></param>
        /// <returns></returns>
        public static DataTable getfabricdetailsofMRN(int mrn_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"
SELECT        SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + '  ' + SkuRawMaterialMaster.Weight + SkuRawMaterialMaster.Width + '  ' + ProcurementDetails.SupplierColor
                          + '  ' + ProcurementDetails.SupplierSize AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK, ProcurementDetails.PODet_PK, MrnDetails.MrnDet_PK
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         ProcurementDetails ON SkuRawmaterialDetail.SkuDet_PK = ProcurementDetails.SkuDet_PK INNER JOIN
                         MrnDetails ON ProcurementDetails.PODet_PK = MrnDetails.PODet_PK
WHERE        (ItemGroupMaster.ItemGroupName = N'Fabric') AND (MrnDetails.Mrn_PK = @mrn_pk)";
                cmd.Parameters.AddWithValue("@mrn_pk", mrn_pk);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        /// <summary>
        /// get all the fanbric details of a mrn
        /// </summary>
        /// <param name="mrn_pk"></param>
        /// <returns></returns>
        public static DataTable getfabricdetailsofATC(int atcid)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"
SELECT        SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + '  ' + SkuRawMaterialMaster.Weight + SkuRawMaterialMaster.Width + '  ' + ProcurementDetails.SupplierColor
                          + '  ' + ProcurementDetails.SupplierSize AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.Atc_id
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         ProcurementDetails ON SkuRawmaterialDetail.SkuDet_PK = ProcurementDetails.SkuDet_PK
WHERE        (ItemGroupMaster.ItemGroupName = N'Fabric') AND (SkuRawMaterialMaster.Atc_id = @atcid)";
                cmd.Parameters.AddWithValue("@atcid", atcid);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }



        /// <summary>
        /// Get All Fabric DO of Atc WW
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static DataTable getFabricDoDetails(int atcid,int LCTNPK)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        DeliveryOrderMaster.DO_PK, DeliveryOrderMaster.DONum, DeliveryOrderMaster.FromLocation_PK, DeliveryOrderMaster.DoType
FROM            DeliveryOrderDetails INNER JOIN
                         InventoryMaster ON DeliveryOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         DeliveryOrderMaster ON DeliveryOrderDetails.DO_PK = DeliveryOrderMaster.DO_PK
WHERE        (ItemGroupMaster.ItemGroupName = 'Fabric') AND (DeliveryOrderMaster.AtcID = @atcid)
GROUP BY DeliveryOrderMaster.DO_PK, DeliveryOrderMaster.DONum, DeliveryOrderMaster.FromLocation_PK, DeliveryOrderMaster.DoType
HAVING        (DeliveryOrderMaster.FromLocation_PK = @LCTNPK) AND (DeliveryOrderMaster.DoType = N'WW')";
                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@LCTNPK", LCTNPK);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }



        /// <summary>
        /// Get All Fabric DO of Atc
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static DataTable getFabricDeliveryReturn(int atcid, int LCTNPK)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        DeliveryOrderMaster.DO_PK, DeliveryOrderMaster.DONum, DeliveryOrderMaster.FromLocation_PK, DeliveryOrderMaster.DoType
FROM            DeliveryOrderDetails INNER JOIN
                         InventoryMaster ON DeliveryOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         DeliveryOrderMaster ON DeliveryOrderDetails.DO_PK = DeliveryOrderMaster.DO_PK
WHERE        (ItemGroupMaster.ItemGroupName = 'Fabric') AND (DeliveryOrderMaster.AtcID = @atcid)
GROUP BY DeliveryOrderMaster.DO_PK, DeliveryOrderMaster.DONum, DeliveryOrderMaster.FromLocation_PK, DeliveryOrderMaster.DoType
HAVING        (DeliveryOrderMaster.FromLocation_PK = @LCTNPK) AND (DeliveryOrderMaster.DoType = N'FW')";
                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@LCTNPK", LCTNPK);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        /// <summary>
        /// get All fabric DOR
        /// </summary>
        /// <param name="atcid"></param>
        /// <param name="LCTNPK"></param>
        /// <returns></returns>
        public static DataTable getFabricDORDetails(int atcid, int LCTNPK)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT    distinct       DeliveryReceiptMaster.DOR_PK, DeliveryReceiptMaster.DORNum, DeliveryReceiptMaster.DOReceiptType, SkuRawMaterialMaster.Template_pk, DeliveryReceiptMaster.Location_PK, 
                         SkuRawMaterialMaster.Atc_id, Template_Master.ItemGroup_PK
FROM            DeliveryReceiptMaster INNER JOIN
                         DeliveryReceiptDetail ON DeliveryReceiptMaster.DOR_PK = DeliveryReceiptDetail.DOR_PK INNER JOIN
                         InventoryMaster ON DeliveryReceiptDetail.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (Template_Master.ItemGroup_PK = 1) AND (DeliveryReceiptMaster.Location_PK = @LCTNPK) AND (SkuRawMaterialMaster.Atc_id = @atcid)";
                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@LCTNPK", LCTNPK);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }

        /// <summary>
        /// Get All Fabric DO of Atc
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static DataTable getFabricROIN(int atcid, int LCTNPK)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT DISTINCT ROINMaster.ROInNum, ROINMaster.ROIN_PK, ROINMaster.Location_pk
FROM            ROINMaster INNER JOIN
                         RequestOrderMaster ON ROINMaster.RO_Pk = RequestOrderMaster.RO_Pk INNER JOIN
                         RequestOrderDetails ON RequestOrderMaster.RO_Pk = RequestOrderDetails.RO_Pk INNER JOIN
                         SkuRawmaterialDetail ON RequestOrderDetails.FromSkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (Template_Master.ItemGroup_PK = 1) AND (ROINMaster.Location_pk = @LCTNPK)";
                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@LCTNPK", LCTNPK);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        public static DataTable getFabricLOAN(int atcid, int LCTNPK)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        InventoryLoanMaster.Loan_PK, InventoryLoanMaster.LoanNum, Template_Master.ItemGroup_PK, InventoryLoanMaster.Location_PK, InventoryLoanMaster.IsApproved
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         InventoryLoanMaster ON SkuRawmaterialDetail.SkuDet_PK = InventoryLoanMaster.ToSkuDet_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (Template_Master.ItemGroup_PK = 1) AND (InventoryLoanMaster.Location_PK = @LCTNPK) AND (InventoryLoanMaster.IsApproved = N'Y')";
                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@LCTNPK", LCTNPK);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }








        /// <summary>
        /// Get All Fabric DO of Atc
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static DataTable getFabricDetailsInsideDO(int do_pk)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT DISTINCT 
                         ISNULL(SkuRawMaterialMaster.RMNum, '') + ' ' + ISNULL(SkuRawMaterialMaster.Composition, '') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, '') + '  ' + ISNULL(SkuRawMaterialMaster.Weight, '') 
                         + ISNULL(SkuRawMaterialMaster.Width, '') + '  ' + ISNULL(ProcurementDetails.SupplierColor, '') + '   ' + ProcurementDetails.SupplierSize AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK
FROM            DeliveryOrderDetails INNER JOIN
                         InventoryMaster ON DeliveryOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK
WHERE        (DeliveryOrderDetails.DO_PK = @do_pk)

";
               
                cmd.Parameters.AddWithValue("@do_pk", do_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }



        /// <summary>
        /// Get All Fabric DO of Atc
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static DataTable GetCutOrderOFDO(int loc_pk_pk, int skudet_PK,int do_PK)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT DISTINCT CutOrderMaster.Cut_NO, CutOrderMaster.CutID, DeliveryOrderDetails.DO_PK
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID INNER JOIN
                         DeliveryOrderDetails ON CutOrderDO.DoDet_Pk = DeliveryOrderDetails.DODet_PK
GROUP BY CutOrderMaster.Cut_NO, CutOrderMaster.CutID, CutOrderMaster.ToLoc, CutOrderMaster.SkuDet_pk, DeliveryOrderDetails.DO_PK
HAVING        (CutOrderMaster.ToLoc = @loc_pk_pk) AND (CutOrderMaster.SkuDet_pk = @skudet_PK) AND (DeliveryOrderDetails.DO_PK = @do_PK)

";

                cmd.Parameters.AddWithValue("@loc_pk_pk", loc_pk_pk);
                cmd.Parameters.AddWithValue("@skudet_PK", skudet_PK);
                cmd.Parameters.AddWithValue("@do_PK", do_PK);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }







        /// <summary>
        /// get all fabric inside DOT
        /// </summary>
        /// <param name="do_pk"></param>
        /// <returns></returns>

        public static DataTable getFabricDetailsInsideDOR(int dor_pk)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
//                cmd.CommandText = @"SELECT DISTINCT 
//                         SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + '  ' + SkuRawMaterialMaster.Weight + SkuRawMaterialMaster.Width + '  ' + ProcurementDetails.SupplierColor+ '  ' + SkuRawmaterialDetail.SupplierColor
//                          + '   ' + ProcurementDetails.SupplierSize AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK, DeliveryReceiptDetail.DOR_PK
//FROM            InventoryMaster INNER JOIN
//                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
//                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
//                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK AND SkuRawmaterialDetail.SkuDet_PK = ProcurementDetails.SkuDet_PK INNER JOIN
//                         DeliveryReceiptDetail ON InventoryMaster.InventoryItem_PK = DeliveryReceiptDetail.InventoryItem_PK
//WHERE        (DeliveryReceiptDetail.DOR_PK = @dor_pk)";






                cmd.CommandText = @" SELECT DISTINCT
                         SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + '  ' + SkuRawMaterialMaster.Weight + SkuRawMaterialMaster.Width + '  ' + ProcurementDetails.SupplierColor
                          + '  ' + SkuRawmaterialDetail.SupplierColor + '   ' + ProcurementDetails.SupplierSize AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK, DeliveryReceiptDetail.DOR_PK
FROM            InventoryMaster INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         DeliveryReceiptDetail ON InventoryMaster.InventoryItem_PK = DeliveryReceiptDetail.InventoryItem_PK
WHERE(DeliveryReceiptDetail.DOR_PK = @dor_pk)";





                cmd.Parameters.AddWithValue("@dor_pk", dor_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }







        /// <summary>
        /// gET LIST OF ALL FABRIC INSIDE A ROIN
        /// </summary>
        /// <param name="roin_pk"></param>
        /// <returns></returns>
        public static DataTable getFabricDetailsInsideROin(int roin_pk)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT DISTINCT 
                         SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + '  ' + SkuRawMaterialMaster.Weight + SkuRawMaterialMaster.Width + '  ' + SkuRawmaterialDetail.ItemColor
                          AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK, ROINMaster.ROIN_PK
FROM            ROINMaster INNER JOIN
                         RequestOrderMaster ON ROINMaster.RO_Pk = RequestOrderMaster.RO_Pk INNER JOIN
                         RequestOrderDetails ON RequestOrderMaster.RO_Pk = RequestOrderDetails.RO_Pk INNER JOIN
                         SkuRawmaterialDetail ON RequestOrderDetails.FromSkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (ROINMaster.ROIN_PK = @roin_pk)";

                cmd.Parameters.AddWithValue("@roin_pk", roin_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }



        public static DataTable getFabricDetailsInsideLoan(int loan_PK)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + '  ' + SkuRawMaterialMaster.Weight + SkuRawMaterialMaster.Width + '  ' + SkuRawmaterialDetail.ItemColor
                          AS ItemDescription, InventoryLoanMaster.FromSkudet_PK as SkuDet_PK, InventoryLoanMaster.Loan_PK
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         InventoryLoanMaster ON SkuRawmaterialDetail.SkuDet_PK = InventoryLoanMaster.ToSkuDet_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (InventoryLoanMaster.Loan_PK = @loan_PK)";

                cmd.Parameters.AddWithValue("@loan_PK", loan_PK);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        /// <summary>
        /// Get all types of fabric inside a ASN
        /// </summary>
        /// <param name="mrn_pk"></param>
        /// <returns></returns>
        public static DataTable getfabricinsideASN(int asn, int atcid)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"
SELECT DISTINCT 
                         SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + '  ' + SkuRawMaterialMaster.Weight + SkuRawMaterialMaster.Width + '  ' + ProcurementDetails.SupplierColor
                          + '   ' + ProcurementDetails.SupplierSize AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK
WHERE        (ItemGroupMaster.ItemGroupName = N'Fabric') AND (FabricRollmaster.SupplierDoc_pk = @asn) and   (SkuRawMaterialMaster.Atc_id = @atcid)
GROUP BY SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + '  ' + SkuRawMaterialMaster.Weight + SkuRawMaterialMaster.Width + '  ' + ProcurementDetails.SupplierColor
                          + '   ' + ProcurementDetails.SupplierSize, SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.Atc_id";
                cmd.Parameters.AddWithValue("@asn", asn);
                cmd.Parameters.AddWithValue("@atcid", atcid);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }



        public static DataTable CreateRollRows(int numofrows)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("RollNum", typeof(String));
            dt.Columns.Add("Qty", typeof(String));
            dt.Columns.Add("UOM", typeof(String));
            dt.Columns.Add("Remark", typeof(String));


            for (int i = 1; i <= numofrows; i++)
            {


                dt.Rows.Add("Roll#" + i.ToString(), "0", "YDS", "Good Condition");

            }

            return dt;


        }




        /// <summary>
        /// Get All Fabric Rolls of A Item based on the sskudetpk of that item
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static DataTable getFabricRollofAItemPK(int iitemPK)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, ProcurementMaster.PONum, 
                         ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                         + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS itemDescription, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup, FabricRollmaster.AYard, FabricRollmaster.SkuDet_PK, FabricRollmaster.IsDelivered
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         SupplierMaster ON SupplierDocumentMaster.Supplier_pk = SupplierMaster.Supplier_PK INNER JOIN
                         InventoryMaster ON FabricRollmaster.SkuDet_PK = InventoryMaster.SkuDet_Pk
WHERE        (InventoryMaster.InventoryItem_PK = @iitemPK) AND (FabricRollmaster.IsDelivered <> N'Y')";
                cmd.Parameters.AddWithValue("@iitemPK", iitemPK);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }









        /// <summary>
        /// Get All Fabric Rolls of A Item based on the sskudetpk of that item and will show those rolls which
        /// have same cutwidth and Shrinkage of that cutorder
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static DataTable getFabricRollofAItemPKandCutorder(int iitemPK ,int cutid)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, ProcurementMaster.PONum, 
                         ISNULL(SkuRawMaterialMaster.Composition, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, N' ') 
                         + ' ' + ISNULL(ProcurementDetails.SupplierSize, N' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, N' ') AS itemDescription, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup, FabricRollmaster.AYard, AtcMaster.AtcNum, InventoryMaster.InventoryItem_PK, InventoryMaster.Location_PK
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         SupplierMaster ON SupplierDocumentMaster.Supplier_pk = SupplierMaster.Supplier_PK INNER JOIN
                         InventoryMaster ON FabricRollmaster.SkuDet_PK = InventoryMaster.SkuDet_Pk INNER JOIN
                         CurrencyMaster ON SupplierMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                         CutOrderMaster ON FabricRollmaster.ShrinkageGroup = CutOrderMaster.Shrinkage AND FabricRollmaster.WidthGroup = CutOrderMaster.CutWidth
WHERE        (InventoryMaster.InventoryItem_PK = @iitemPK) AND (CutOrderMaster.CutID = @cutid)  AND (FabricRollmaster.IsDelivered <> N'Y')";
                cmd.Parameters.AddWithValue("@iitemPK", iitemPK);
                cmd.Parameters.AddWithValue("@cutid", cutid);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }






        /// <summary>
        /// Get All Fabric Rolls of A Item based on the sskudetpk of that item and will show those rolls which
        /// have same cutwidth and Shrinkage of that cutorder an is presnt5 in the location(warehouse)
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static DataTable getFabricRollofAItemPKandCutorder(int iitemPK, int cutid,int location_pk)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        tt.Roll_PK, tt.RollNum, tt.ASN, tt.PONum, tt.itemDescription, tt.WidthGroup, tt.ShadeGroup, tt.ShrinkageGroup, tt.AYard, tt.AtcNum, tt.InventoryItem_PK, tt.Location_PK, RollInventoryMaster.IsPresent, 
                         RollInventoryMaster.Location_Pk AS Expr1, tt.MarkerType, tt.AWidth, tt.AShrink, tt.AShade,ISNULL( tt.SWeight,'NA') as SWeight
FROM            (SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, ProcurementMaster.PONum, 
                                                    ISNULL(SkuRawMaterialMaster.Composition, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N' ') 
                                                    + ' ' + ISNULL(SkuRawMaterialMaster.Width, N' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, N' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, N' ') AS itemDescription, 
                                                    FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.AYard, AtcMaster.AtcNum, InventoryMaster.InventoryItem_PK, 
                                                    InventoryMaster.Location_PK, FabricRollmaster.MarkerType, FabricRollmaster.AWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.SWeight
                          FROM            SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                                                    FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                                                    ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                                                    SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                                                    ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                                                    AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                                                    SupplierMaster ON SupplierDocumentMaster.Supplier_pk = SupplierMaster.Supplier_PK INNER JOIN
                                                    InventoryMaster ON FabricRollmaster.SkuDet_PK = InventoryMaster.SkuDet_Pk INNER JOIN
                                                    CurrencyMaster ON SupplierMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                                                    CutOrderMaster ON FabricRollmaster.ShrinkageGroup = CutOrderMaster.Shrinkage AND FabricRollmaster.WidthGroup = CutOrderMaster.CutWidth
                          WHERE        (InventoryMaster.InventoryItem_PK = @iitemPK) AND (CutOrderMaster.CutID = @cutid) AND (FabricRollmaster.IsDelivered <> N'Y')) AS tt INNER JOIN
                         RollInventoryMaster ON tt.Roll_PK = RollInventoryMaster.Roll_PK
WHERE        (RollInventoryMaster.IsPresent = N'Y') AND (tt.Location_PK = @location_pk)
ORDER BY tt.RollNum ";
                cmd.Parameters.AddWithValue("@iitemPK", iitemPK);
                cmd.Parameters.AddWithValue("@cutid", cutid);
                cmd.Parameters.AddWithValue("@location_pk", location_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }





        public static DataTable getFabricRollAvailableforCutPLan( int cutplan_PK, int location_pk)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        Roll_PK, RollNum, ASN, PONum, itemDescription, WidthGroup, ShadeGroup, ShrinkageGroup, AYard, AtcNum, MarkerType, AWidth, AShrink, AShade, ISNULL(SWeight, 'NA') AS SWeight
FROM            (SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, ProcurementMaster.PONum, 
                         ISNULL(SkuRawMaterialMaster.Composition, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, N' ') 
                         + ' ' + ISNULL(ProcurementDetails.SupplierSize, N' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, N' ') AS itemDescription, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup, FabricRollmaster.AYard, AtcMaster.AtcNum, FabricRollmaster.MarkerType, FabricRollmaster.AWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, 
                         FabricRollmaster.SWeight, CutPlanMaster.CutPlan_PK, RollInventoryMaster.IsPresent, RollInventoryMaster.Location_Pk
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         CutPlanMaster ON FabricRollmaster.SkuDet_PK = CutPlanMaster.SkuDet_PK AND FabricRollmaster.ShrinkageGroup = CutPlanMaster.ShrinkageGroup AND 
                         FabricRollmaster.WidthGroup = CutPlanMaster.WidthGroup AND FabricRollmaster.MarkerType = CutPlanMaster.MarkerType INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK
WHERE        (FabricRollmaster.IsDelivered <> N'Y') AND (CutPlanMaster.CutPlan_PK = @cutplan_PK) AND (RollInventoryMaster.IsPresent = N'Y') AND (RollInventoryMaster.Location_Pk = @location_pk) and FabricRollmaster.Roll_PK not in (Select Roll_PK from CutPlanRollDetails where   IsDeleted='N')) AS tt
ORDER BY tt.RollNum ";
           ;
                cmd.Parameters.AddWithValue("@cutplan_PK", cutplan_PK);
                cmd.Parameters.AddWithValue("@location_pk", location_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


    }




    public class FabricRollEntryMRN
    {
       

        public List<FabricRollmasterDataDetails> Rolldatacollection { get; set; }

        public String MRNNUM { get; set; }
        public RollInventoryData rollinvdata { get; set; }

        //RollInventoryData rollinvdata, List<FabricRollmasterDataDetails> FabricRollmasterDataDetails
        public void insertMrnRollData()
        {
            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {
                foreach (FabricRollmasterDataDetails rolldata in Rolldatacollection)
                {


                    FabricRollmaster fbmstr = new FabricRollmaster();
                    fbmstr.MRnDet_PK = rolldata.MRnDet_PK;
                    fbmstr.RollNum = rolldata.RollNum;
                    fbmstr.Qty = rolldata.Qty;
                    fbmstr.UOM = rolldata.UOM;
                    fbmstr.Remark = rolldata.Remark;
                    fbmstr.SShrink = rolldata.SShrink;
                    fbmstr.SYard = decimal.Parse(rolldata.SYard.ToString ());
                    fbmstr.SShade = rolldata.SShade;
                    fbmstr.SWidth = rolldata.SWidth;
                    fbmstr.AShrink = rolldata.AShrink;
                    fbmstr.AShade = rolldata.AShade;
                    fbmstr.AWidth = rolldata.AWidth;
                    fbmstr.AYard = Decimal.Parse( rolldata.AYard.ToString ());
                    fbmstr.SkuDet_PK = rolldata.SkuDet_PK;
                    fbmstr.IsSaved = "N";
                    entry.FabricRollmasters.Add(fbmstr);
                    entry.SaveChanges();


                    RollInventoryMaster rvinvmstr = new RollInventoryMaster();

                    rvinvmstr.Addeddate = rollinvdata.Addeddate;
                    rvinvmstr.DocumentNum = rollinvdata.DocumentNum;
                    rvinvmstr.AddedVia = rollinvdata.AddedVia;
                    rvinvmstr.AddedBy = rollinvdata.AddedBy;
                    rvinvmstr.Location_Pk = rollinvdata.Location_Pk;
                    rvinvmstr.Roll_PK = fbmstr.Roll_PK;
                    rvinvmstr.IsPresent = "Y";

                    entry.SaveChanges();


                }


            }
        }

        public void insertSupplierRollData()
        {
            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {
                foreach (FabricRollmasterDataDetails rolldata in Rolldatacollection)
                {


                    FabricRollmaster fbmstr = new FabricRollmaster();
                    fbmstr.MRnDet_PK = rolldata.MRnDet_PK;
                    fbmstr.RollNum = rolldata.RollNum;
                    fbmstr.Qty = rolldata.Qty;
                    fbmstr.UOM = rolldata.UOM;
                    fbmstr.Remark = rolldata.Remark;
                    fbmstr.SShrink = rolldata.SShrink;
                    fbmstr.SYard = decimal.Parse(rolldata.SYard.ToString ());
                    fbmstr.SShade = rolldata.SShade;
                    fbmstr.SWidth = rolldata.SWidth;
                    fbmstr.AShrink = rolldata.AShrink;
                    fbmstr.AShade = rolldata.AShade;
                    fbmstr.AWidth = rolldata.AWidth;
                    fbmstr.AYard = Decimal.Parse(rolldata.AYard);
                    fbmstr.SkuDet_PK = rolldata.SkuDet_PK;
                    fbmstr.podet_pk = rolldata.PoDet_PK;
                    fbmstr.Po_PK = rolldata.PO_PK;
                    fbmstr.IsSaved = "N";
                    fbmstr.IsDelivered = "N";
                    fbmstr.SGsm = rolldata.SGSM;
                    fbmstr.SWeight = rolldata.Sweight;
                    fbmstr.SupplierDoc_pk = rolldata.SUpplierDoc_PK;

                    fbmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    fbmstr.AddedDate = DateTime.Now;
                    entry.FabricRollmasters.Add(fbmstr);
                    entry.SaveChanges();



                    RollInventoryMaster rvinvmstr = new RollInventoryMaster();

                    rvinvmstr.Addeddate = DateTime.Now;
                    rvinvmstr.DocumentNum = this.MRNNUM;
                    rvinvmstr.AddedVia = "MRN";
                    rvinvmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    rvinvmstr.Location_Pk = int.Parse(HttpContext.Current.Session["UserLoc_pk"].ToString().Trim());
                    rvinvmstr.Roll_PK = fbmstr.Roll_PK;
                    rvinvmstr.IsPresent = "Y";
                    entry.RollInventoryMasters.Add(rvinvmstr);
                    

                    entry.SaveChanges();
               
                 
                 

                }


            }
        }


        public FabricRollmasterDataDetails splitrollmaqster = new FabricRollmasterDataDetails();



        public void SplitSupplierRollData(int oldrollpk,decimal oldyardage)
        {
            Boolean isinspected = false;

            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {


                var q = from fbrmstr in entry.FabricRollmasters
                        where fbrmstr.Roll_PK == oldrollpk
                        select fbrmstr;

                foreach (var element in q)
                {
                    if(element.IsSaved.ToString ().Trim ()=="Y")
                    {
                        isinspected = true;
                        element.AYard = Decimal.Parse(oldyardage.ToString()) ;
                    }
                    element.SYard = oldyardage;

                    this.splitrollmaqster.MRnDet_PK = int.Parse ( element.MRnDet_PK.ToString ());
                    this.splitrollmaqster.UOM = element.UOM;
                    this.splitrollmaqster.SShrink = element.SShrink;
                    this.splitrollmaqster.SYard = element.SYard.ToString();
                    this.splitrollmaqster.SShade = element.SShade;
                    this.splitrollmaqster.SWidth = element.SWidth;
                    this.splitrollmaqster.AShrink = element.AShrink;
                    this.splitrollmaqster.AShade = element.AShade;
                    this.splitrollmaqster.AWidth = element.AWidth;
                    this.splitrollmaqster.AYard = element.AYard.ToString();
                    this.splitrollmaqster.Dummyskudetpk = int.Parse(element.SkuDet_PK.ToString());
                    this.splitrollmaqster.Dummypodet_pk = int.Parse(element.podet_pk.ToString());
                    this.splitrollmaqster.PO_PK = int.Parse(element.Po_PK.ToString());
                    this.splitrollmaqster.IsSaved = element.IsSaved;
                    this.splitrollmaqster.SUpplierDoc_PK = int.Parse(element.SupplierDoc_pk.ToString());

                    this.splitrollmaqster.AGSM = element.AGsm;
                    this.splitrollmaqster.SGSM = element.SGsm;

                    this.splitrollmaqster.MarkerType = element.MarkerType;
                    this.splitrollmaqster.IsAccepted = element.IsAcceptable;
                    this.splitrollmaqster.TotalDefect = element.TotalDefect;
                    this.splitrollmaqster.TotalDefecton100 = element.TotalDefecton100;
                    this.splitrollmaqster.TotalPoint = element.TotalPoint;
                    this.splitrollmaqster.TotalPointon100yard = element.TotalPointon100yard;

                    this.splitrollmaqster.widthgroup = element.WidthGroup;
                    this.splitrollmaqster.shadegroup = element.ShadeGroup;
                    this.splitrollmaqster.shrinkagegroup = element.ShrinkageGroup;



                    this.splitrollmaqster.IsGrouped = element.IsGrouped;
                    this.splitrollmaqster.IsApproved = element.IsApproved;


                       
                }


                foreach (FabricRollmasterDataDetails rolldata in Rolldatacollection)
                {


                    FabricRollmaster fbmstr = new FabricRollmaster();

                    fbmstr.RollNum = rolldata.RollNum;
                    fbmstr.Qty = rolldata.Qty;
                    fbmstr.Remark = rolldata.Remark;
                    if (isinspected == true)
                    {
                        fbmstr.AYard = rolldata.Qty;
                    }
                    fbmstr.SYard = rolldata.Qty;




                    fbmstr.MRnDet_PK = this.splitrollmaqster.MRnDet_PK;
                    fbmstr.UOM = this.splitrollmaqster.UOM;
                    fbmstr.SShrink = this.splitrollmaqster.SShrink;
                   
                    fbmstr.SShade = this.splitrollmaqster.SShade;
                    fbmstr.SWidth = this.splitrollmaqster.SWidth;
                    fbmstr.AShrink = this.splitrollmaqster.AShrink;
                    fbmstr.AShade = this.splitrollmaqster.AShade;
                    fbmstr.AWidth = this.splitrollmaqster.AWidth;
                   
                    fbmstr.SkuDet_PK = this.splitrollmaqster.Dummyskudetpk;
                    fbmstr.podet_pk = this.splitrollmaqster.Dummypodet_pk;
                    fbmstr.Po_PK = this.splitrollmaqster.PO_PK;
                    fbmstr.IsSaved = this.splitrollmaqster.IsSaved;
                    fbmstr.SupplierDoc_pk = this.splitrollmaqster.SUpplierDoc_PK;
                    fbmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    fbmstr.AddedDate = DateTime.Now;
                    fbmstr.AGsm = this.splitrollmaqster.AGSM;
                    fbmstr.SGsm = this.splitrollmaqster.SGSM;
                    fbmstr.IsDelivered = "N";
                    fbmstr.MarkerType = this.splitrollmaqster.MarkerType;
                    fbmstr.IsAcceptable = this.splitrollmaqster.IsAccepted;
                    fbmstr.TotalDefect = this.splitrollmaqster.TotalDefect;
                    fbmstr.TotalDefecton100 = this.splitrollmaqster.TotalDefecton100;
                    fbmstr.TotalPoint = this.splitrollmaqster.TotalPoint;
                    fbmstr.TotalPointon100yard = this.splitrollmaqster.TotalPointon100yard;

                    fbmstr.WidthGroup = this.splitrollmaqster.widthgroup;
                    fbmstr.ShadeGroup = this.splitrollmaqster.shadegroup;
                    fbmstr.ShrinkageGroup = this.splitrollmaqster.shrinkagegroup;



                    fbmstr.IsGrouped = this.splitrollmaqster.IsGrouped;
                    fbmstr.IsApproved = this.splitrollmaqster.IsApproved;
                    fbmstr.IsSaved = this.splitrollmaqster.IsSaved;

                    entry.FabricRollmasters.Add(fbmstr);
                    entry.SaveChanges();



                    RollInventoryMaster rvinvmstr = new RollInventoryMaster();

                    rvinvmstr.Addeddate = DateTime.Now;
                    rvinvmstr.AddedVia = "Split";
                    rvinvmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    rvinvmstr.Location_Pk = int.Parse(HttpContext.Current.Session["UserLoc_pk"].ToString().Trim());
                    rvinvmstr.Roll_PK = fbmstr.Roll_PK;
                    rvinvmstr.IsPresent = "Y";
                    entry.RollInventoryMasters.Add(rvinvmstr);


                    entry.SaveChanges();




                }










            }
        }



        public void UpdateRollSupplierdata()
        {
            foreach (FabricRollmasterDataDetails rolldata in Rolldatacollection)
            {
                using (ArtEntitiesnew entry = new ArtEntitiesnew())
                {
                    var q = from fbrmstr in entry.FabricRollmasters
                            where fbrmstr.Roll_PK == rolldata.Roll_PK
                            select fbrmstr;

                    foreach (var element in q)
                    {
                        element.SShade = rolldata.SShade;
                        element.SShrink = rolldata.SShrink;
                        element.SGsm = rolldata.SGSM;
                        element.SYard = decimal.Parse(rolldata.SYard.ToString ());
                        element.Remark = rolldata.Remark; ;
                        element.RollNum = rolldata.RollNum;
                        element.SWidth = rolldata.SWidth; ;
                        element.SWeight = rolldata.Sweight;


                    }

                    entry.SaveChanges();
                }

            }
        }



        public void UpdateRollMRNDetails()
        {
            foreach (FabricRollmasterDataDetails rolldata in Rolldatacollection)
            {
                int mrndetpk = 0;
                int locationpk = 0;
                string docnum = "";

                using (ArtEntitiesnew entry = new ArtEntitiesnew())
                {
                    var q = from fbrmstr in entry.FabricRollmasters
                            where fbrmstr.Roll_PK == rolldata.Roll_PK
                            select fbrmstr;

                    foreach (var element in q)
                    {
                        element.MRnDet_PK = rolldata.MRnDet_PK;

                        mrndetpk = rolldata.MRnDet_PK;
                    }


                    var q1 = from fbrmstr in entry.MrnDetails
                            join mrnmstr in entry.MrnMasters on fbrmstr.Mrn_PK equals mrnmstr.Mrn_PK
                            where fbrmstr.MrnDet_PK == mrndetpk
                            select new { mrnmstr.MrnNum,mrnmstr.Location_Pk } ;

                    foreach (var element in q1)
                    {
                        locationpk = int.Parse ( element.Location_Pk.ToString ());

                        docnum = element.MrnNum.ToString ();
                    }




                    RollInventoryMaster rvinvmstr = new RollInventoryMaster();

                    rvinvmstr.Addeddate = DateTime.Now;
                    rvinvmstr.DocumentNum = docnum;
                    rvinvmstr.AddedVia = "MRN";
                    rvinvmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    rvinvmstr.Location_Pk = locationpk; 
                    rvinvmstr.Roll_PK = rolldata.Roll_PK;
                    rvinvmstr.IsPresent = "Y";
                    entry.RollInventoryMasters.Add(rvinvmstr);
                    entry.SaveChanges();

                    entry.SaveChanges();
                }

            }
        }


        public void UpdateRollInspectiondata()
        {
            foreach (FabricRollmasterDataDetails rolldata in Rolldatacollection)
            {
                using (ArtEntitiesnew entry = new ArtEntitiesnew())
                {
                    var q = from fbrmstr in entry.FabricRollmasters
                            where fbrmstr.Roll_PK == rolldata.Roll_PK
                            select fbrmstr;

                    foreach (var element in q)
                    {
                        element.AShade = rolldata.AShade;
                        element.AShrink = rolldata.AShrink;
                        element.AWidth = rolldata.AWidth;
                        element.AYard =Decimal.Parse( rolldata.AYard.ToString ());
                        element.AGsm = rolldata.AGSM;
                        element.IsApproved = "N";
                        element.IsSaved = "Y";
                        element.IsGrouped = "N";

                    }

                    entry.SaveChanges();
                }

            }
        }

        public void UpproveRollInspectiondata()
        {
            foreach (FabricRollmasterDataDetails rolldata in Rolldatacollection)
            {
                using (ArtEntitiesnew entry = new ArtEntitiesnew())
                {
                    var q = from fbrmstr in entry.FabricRollmasters
                            where fbrmstr.Roll_PK == rolldata.Roll_PK
                            select fbrmstr;

                    foreach (var element in q)
                    {

                        element.IsApproved = "A";

                        element.MarkerType = rolldata.MarkerType;
                        element.IsAcceptable = rolldata.IsAccepted;
                        element.TotalDefect = rolldata.TotalDefect;
                        element.TotalDefecton100 = rolldata.TotalDefecton100;
                        element.TotalPoint = rolldata.TotalPoint;
                        element.TotalPointon100yard = rolldata.TotalPointon100yard;
                    }

                    entry.SaveChanges();
                }

            }
        }



        public void UpproveRollInspectionGroup()
        {
            foreach (FabricRollmasterDataDetails rolldata in Rolldatacollection)
            {
                using (ArtEntitiesnew entry = new ArtEntitiesnew())
                {
                    var q = from fbrmstr in entry.FabricRollmasters
                            where fbrmstr.Roll_PK == rolldata.Roll_PK
                            select fbrmstr;

                    foreach (var element in q)
                    {

                        element.IsApproved = "A";
                        element.IsGrouped = "Y";
                        element.WidthGroup = rolldata.widthgroup;
                        element.ShadeGroup = rolldata.shadegroup;
                        element.ShrinkageGroup = rolldata.shrinkagegroup;
                    }

                    entry.SaveChanges();
                }

            }
        }

    }


    public class FabricRollEntryDO
    {
        public string Docnum { get; set; }

        public int  cutid { get; set; }
        public int DoID { get; set; }

        public List<RollInventoryData> RollInventoryDatadatacollection { get; set; }

        public RollInventoryData rollinvdata { get; set; }

        //RollInventoryData rollinvdata, List<FabricRollmasterDataDetails> FabricRollmasterDataDetails


            //warehouse to warehouse transfer
        public void insertDORollData()
        {

        




            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {
                int locationpk = 0;
                var q1 = from domaster in entry.DeliveryOrderMasters
                        
                         where domaster.DONum == this.Docnum
                         select new { domaster.ToLocation_PK, domaster.DONum
                         };

                foreach (var element in q1)
                {
                    locationpk = int.Parse(element.ToLocation_PK.ToString());

                    
                }



                foreach (RollInventoryData rolldata in RollInventoryDatadatacollection)
                {


                   
                    //creates a roll on the new location with is present as N

                    RollInventoryMaster rvinvmstr = new RollInventoryMaster();

                    rvinvmstr.Addeddate = DateTime.Now;
                    rvinvmstr.DocumentNum = this.Docnum;
                    rvinvmstr.AddedVia = "WW";
                    rvinvmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();                   
                    rvinvmstr.Location_Pk = locationpk;
                    rvinvmstr.Roll_PK = rolldata.roll_PK;
                    rvinvmstr.IsPresent = "W";
                    entry.RollInventoryMasters.Add(rvinvmstr);
                    entry.SaveChanges();

                    var q = from rllinvdata in entry.RollInventoryMasters
                            where rllinvdata.Roll_PK == rolldata.roll_PK && rllinvdata.IsPresent == "Y"
                            select rllinvdata;
                    foreach(var element in q)
                    {
                        element.IsPresent = "N";
                        element.DeliveredVia= this.Docnum;
                        element.NewRollInventory_PK = rvinvmstr.RollInventory_PK;

                    }
                    entry.SaveChanges();
                }


            }
        }

        //Receive Roll data of A DO
        public void UpdateDORRollData()
        {






            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {
               
                foreach (RollInventoryData rolldata in RollInventoryDatadatacollection)
                {



                  

                    var q = from rllinvdata in entry.RollInventoryMasters
                            where rllinvdata.Roll_PK == rolldata.roll_PK && rllinvdata.IsPresent == "W"
                            select rllinvdata;
                    foreach (var element in q)
                    {
                        element.IsPresent = "Y";
                        element.DocumentNum = this.Docnum;
                       

                    }
                    entry.SaveChanges();
                }


            }
        }

        public void insertDOReturnRollData()
        {






            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {
                int locationpk = 0;
                int skudetpk = 0;
                int dodetpk = 0;
                int do_pk = 0;
                Decimal ayard = 0;
                var q1 = from domaster in entry.DeliveryOrderDetails

                         where domaster.DeliveryOrderMaster.DONum == this.Docnum
                         select new
                         {
                             domaster.DeliveryOrderMaster.ToLocation_PK,
                             domaster.DeliveryOrderMaster.DONum,
                            domaster.DODet_PK,
                             domaster.DO_PK
                         };

                foreach (var element in q1)
                {
                    locationpk = int.Parse(element.ToLocation_PK.ToString());

                    dodetpk = int.Parse(element.DODet_PK.ToString());
                    do_pk= int.Parse(element.DO_PK.ToString());
                }



                foreach (RollInventoryData rolldata in RollInventoryDatadatacollection)
                {  //creates a roll on the new location with is present as N

                    RollInventoryMaster rvinvmstr = new RollInventoryMaster();

                    rvinvmstr.Addeddate = DateTime.Now;
                    rvinvmstr.DocumentNum = this.Docnum;
                    rvinvmstr.AddedVia = "FW";
                    rvinvmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    rvinvmstr.Location_Pk = locationpk;
                    rvinvmstr.Roll_PK = rolldata.roll_PK;
                    rvinvmstr.IsPresent = "W";
                    entry.RollInventoryMasters.Add(rvinvmstr);
                    entry.SaveChanges();

                    var q = from rllinvdata in entry.RollInventoryMasters
                            where rllinvdata.Roll_PK == rolldata.roll_PK && rllinvdata.IsPresent == "Y"
                            select rllinvdata;
                    foreach (var element in q)
                    {
                        element.IsPresent = "N";
                        element.DeliveredVia = this.Docnum;
                        element.NewRollInventory_PK = rvinvmstr.RollInventory_PK;

                    }

                    var m = from invitem in entry.FabricRollmasters
                            where invitem.Roll_PK == rolldata.roll_PK
                            select invitem;

                    foreach (var rolldet in m)
                    {
                        rolldet.IsDelivered = "N";
                        skudetpk =int.Parse( rolldet.SkuDet_PK.ToString());

                        ayard += Decimal.Parse(ayard.ToString());

                    }


                    var k = from dorollin in
                                entry.DORollDetails
                            where  dorollin.Roll_PK == rolldata.roll_PK
                    select dorollin;
                    foreach(var element in k)
                    {
                        element.IsRollReturned = "Y";
                      
                    }


                    DORollDetail dorolldet = new DataModels.DORollDetail();

                    dorolldet.CutID = this.cutid;
                    dorolldet.Roll_PK = int.Parse(rolldata.roll_PK.ToString());
                    dorolldet.DODet_PK = dodetpk;
                    dorolldet.DO_PK = do_pk;
                    entry.DORollDetails.Add(dorolldet);



                    rvinvmstr.IsPresent = "Y";
                    
                }
                CutOrderDO ctordrdo = new CutOrderDO();
                ctordrdo.CutID = this.cutid;
                ctordrdo.Skudet_PK = skudetpk;
                ctordrdo.DoDet_Pk = dodetpk;
                ctordrdo.DeliveryQty = (0-ayard);
                entry.CutOrderDOes.Add(ctordrdo);            
                entry.SaveChanges();

            }
        }

    }

    public class FabricRollEntryROIN
    {
        public string Docnum { get; set; }
        public int  SkuDet_PK { get; set; }
        public int roin_PK { get; set; }
        public List<RollInventoryData> RollInventoryDatadatacollection { get; set; }

        public RollInventoryData rollinvdata { get; set; }

        //RollInventoryData rollinvdata, List<FabricRollmasterDataDetails> FabricRollmasterDataDetails
        public void insertROINRollData()
        {

            int toskudetpk = 0;
            int oldrollinventorypk = 0;
            int locpk = 0;


            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {

                var q = (from um in entry.RoInDetails
                         where um.FromSkuDet_PK == this.SkuDet_PK && um.ROIN_PK==this.roin_PK

                         select um.ToSkuDet_Pk).FirstOrDefault();

                toskudetpk = int.Parse(q.ToString());

                if(toskudetpk>0)
                {
                    foreach (RollInventoryData rolldata in RollInventoryDatadatacollection)
                    {




                        var rolldataquery = from fbrcroll in entry.FabricRollmasters
                                            where fbrcroll.Roll_PK == rolldata.roll_PK
                                            select fbrcroll;

                        foreach (var element in rolldataquery)
                        {
                            element.SkuDet_PK = toskudetpk;
                        }


                        var q1 = from rllinvdata in entry.RollInventoryMasters
                                where rllinvdata.Roll_PK == rolldata.roll_PK && rllinvdata.IsPresent == "Y"
                                select rllinvdata;
                        foreach (var element in q1)
                        {
                            element.IsPresent = "N";
                            element.DeliveredVia = this.Docnum;
                            locpk = int.Parse(element.Location_Pk.ToString());
                            oldrollinventorypk= int.Parse(element.RollInventory_PK.ToString());
                        }



                        //creates a roll on the new location with is present as N

                        RollInventoryMaster rvinvmstr = new RollInventoryMaster();

                        rvinvmstr.Addeddate = DateTime.Now;
                        rvinvmstr.DocumentNum = this.Docnum;
                        rvinvmstr.AddedVia = "ROIN";
                        rvinvmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                        rvinvmstr.Location_Pk = locpk;
                        rvinvmstr.Roll_PK = rolldata.roll_PK;
                        rvinvmstr.IsPresent = "Y";
                        entry.RollInventoryMasters.Add(rvinvmstr);
                        entry.SaveChanges();

                       
                        



                        var q3 = from rllinvdata in entry.RollInventoryMasters
                                 where rllinvdata.RollInventory_PK== oldrollinventorypk
                                 select rllinvdata;
                        foreach (var element in q3)
                        {
                            element.NewRollInventory_PK = rvinvmstr.RollInventory_PK;
                        }

                        entry.SaveChanges();

                    }

                }



            }
        }





    }


    public class FabricRollEntryLoan
    {
        public string Docnum { get; set; }
        public int SkuDet_PK { get; set; }
        public int loan_PK { get; set; }
        public List<RollInventoryData> RollInventoryDatadatacollection { get; set; }

        public RollInventoryData rollinvdata { get; set; }

        //RollInventoryData rollinvdata, List<FabricRollmasterDataDetails> FabricRollmasterDataDetails
        public void insertLoanRollData()
        {

            int toskudetpk = 0;
            int oldrollinventorypk = 0;
            int locpk = 0;


            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {

                var q = (from um in entry.InventoryLoanMasters
                         where um.FromSkudet_PK == this.SkuDet_PK && um.Loan_PK == this.loan_PK

                         select um.ToSkuDet_PK).FirstOrDefault();

                toskudetpk = int.Parse(q.ToString());

                if (toskudetpk > 0)
                {
                    foreach (RollInventoryData rolldata in RollInventoryDatadatacollection)
                    {




                        var rolldataquery = from fbrcroll in entry.FabricRollmasters
                                            where fbrcroll.Roll_PK == rolldata.roll_PK
                                            select fbrcroll;

                        foreach (var element in rolldataquery)
                        {
                            element.SkuDet_PK = toskudetpk;
                        }


                        var q1 = from rllinvdata in entry.RollInventoryMasters
                                 where rllinvdata.Roll_PK == rolldata.roll_PK && rllinvdata.IsPresent == "Y"
                                 select rllinvdata;
                        foreach (var element in q1)
                        {
                            element.IsPresent = "N";
                            element.DeliveredVia = this.Docnum;
                            locpk = int.Parse(element.Location_Pk.ToString());
                            oldrollinventorypk = int.Parse(element.RollInventory_PK.ToString());
                        }



                        //creates a roll on the new location with is present as N

                        RollInventoryMaster rvinvmstr = new RollInventoryMaster();

                        rvinvmstr.Addeddate = DateTime.Now;
                        rvinvmstr.DocumentNum = this.Docnum;
                        rvinvmstr.AddedVia = "Loan";
                        rvinvmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                        rvinvmstr.Location_Pk = locpk;
                        rvinvmstr.Roll_PK = rolldata.roll_PK;
                        rvinvmstr.IsPresent = "Y";
                        entry.RollInventoryMasters.Add(rvinvmstr);
                        entry.SaveChanges();


                        var q3 = from rllinvdata in entry.RollInventoryMasters
                                 where rllinvdata.RollInventory_PK == oldrollinventorypk
                                 select rllinvdata;
                        foreach (var element in q3)
                        {
                            element.NewRollInventory_PK = rvinvmstr.RollInventory_PK;
                        }

                        entry.SaveChanges();
                    }

                }



            }
        }





    }

    public class FabricRollmasterDataDetails
    {
        public int Roll_PK { get; set; }
        public string RollNum { get; set; }
        public int PO_PK { get; set; }
        
        public int MRnDet_PK { get; set; }
        public Decimal Qty { get; set; }
        public string UOM { get; set; }
        public string Remark { get; set; }
        public string SShrink { get; set; }
        public string Sweight { get; set; }
        public string SYard { get; set; }
        public string SShade { get; set; }
        public string SWidth { get; set; }
        public string AShrink { get; set; }
        public string AShade { get; set; }
        public string AWidth { get; set; }
        public string AYard { get; set; }
        public string AGSM { get; set; }
        public string SGSM { get; set; }
        public int SUpplierDoc_PK { get; set; }
        public string Lotnum { get; set; }
        
        public string TotalDefect { get; set; }
        public string TotalDefecton100 { get; set; }
        public string TotalPoint { get; set; }
        public string TotalPointon100yard { get; set; }
        //public string SGSM { get; set; }

        public string widthgroup { get; set; }
        public string shadegroup { get; set; }
        public string shrinkagegroup { get; set; }
        public string docnum { get; set; }
        public string IsAccepted { get; set; }
        public string MarkerType { get; set; }

        public string IsSaved { get; set; }
        public string IsGrouped { get; set; }
        public string IsApproved { get; set; }


        public int Dummyskudetpk { get; set; }
        public int Dummypodet_pk { get; set; }
        public int dummymrndetpk { get; set; }



        public int PoDet_PK
        {
            get { return gePODetPk(MRnDet_PK); }
            set { PoDet_PK = value; }
        }

        public int SkuDet_PK
        {
            get { return getSKUDetPk(MRnDet_PK); }
            set { SkuDet_PK = value; }
        }

        public int getSKUDetPk(int mrndetpk)
        {
            int skudetpk = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from um in enty.MrnDetails
                         where um.MrnDet_PK == mrndetpk

                         select um.SkuDet_PK).FirstOrDefault();

                skudetpk = int.Parse(q.ToString());
            }

            return skudetpk;
        }


        public int gePODetPk(int mrndetpk)
        {
            int PODet_PK = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from um in enty.MrnDetails
                         where um.MrnDet_PK == mrndetpk

                         select um.PODet_PK).FirstOrDefault();

                PODet_PK = int.Parse(q.ToString());
            }

            return PODet_PK;
        }

        /// <summary>
        /// get roll details of a mrn
        /// </summary>
        /// <param name="mrn_pk"></param>
        /// <returns></returns>
        public DataTable getRollDetailsofMRN(int mrn_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.Remark, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, 
                         FabricRollmaster.SWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard,FabricRollmaster.SGsm,FabricRollmaster.AGsm,FabricRollmaster.Lotnum
FROM            FabricRollmaster INNER JOIN
                         MrnDetails ON FabricRollmaster.MRnDet_PK = MrnDetails.MrnDet_PK
WHERE        (MrnDetails.Mrn_PK = @mrn_pk) and FabricRollmaster.IsSaved='N'";
                cmd.Parameters.AddWithValue("@mrn_pk", mrn_pk);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        public DataTable getRollDetailsofASNandSKUDetPK(int asn_pk,int skudet_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        Roll_PK, RollNum, Qty, UOM, Remark, SShrink, SYard, SShade, SWidth, AShrink, AShade, AWidth, AYard, SGsm, AGsm, SkuDet_PK, SupplierDoc_pk,Lotnum
FROM            FabricRollmaster
WHERE        (IsSaved = 'N') AND (SkuDet_PK = @skudet_pk) AND (SupplierDoc_pk = @asn_pk)";
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }






        public DataTable getRollDetailsofASNandMrnDetpk(int asn_pk, int mrndetpk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.Remark, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, 
                         FabricRollmaster.SWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.SGsm, FabricRollmaster.AGsm, FabricRollmaster.SkuDet_PK, 
                         FabricRollmaster.SupplierDoc_pk, FabricRollmaster.LOTnum, MrnDetails.MrnDet_PK, FabricRollmaster.MRnDet_PK AS Expr1
FROM            FabricRollmaster INNER JOIN
                         MrnDetails ON FabricRollmaster.SkuDet_PK = MrnDetails.SkuDet_PK
WHERE        (FabricRollmaster.SupplierDoc_pk = @asn_pk) AND (MrnDetails.MrnDet_PK = @mrndetpk) AND (FabricRollmaster.MRnDet_PK = 0)";
                cmd.Parameters.AddWithValue("@mrndetpk", mrndetpk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }



        public DataTable getRollDetailsofASNandMrnDetpk(int asn_pk, int mrndetpk,int po_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.Remark, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, 
                         FabricRollmaster.SWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.SGsm, FabricRollmaster.AGsm, FabricRollmaster.SkuDet_PK, 
                         FabricRollmaster.SupplierDoc_pk, FabricRollmaster.LOTnum, MrnDetails.MrnDet_PK, FabricRollmaster.MRnDet_PK AS Expr1, FabricRollmaster.Po_PK
FROM            FabricRollmaster INNER JOIN
                         MrnDetails ON FabricRollmaster.SkuDet_PK = MrnDetails.SkuDet_PK
WHERE        (FabricRollmaster.SupplierDoc_pk = @asn_pk) AND (MrnDetails.MrnDet_PK = @mrndetpk) AND (FabricRollmaster.MRnDet_PK = 0) AND (FabricRollmaster.Po_PK = @po_pk)";
                cmd.Parameters.AddWithValue("@mrndetpk", mrndetpk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                cmd.Parameters.AddWithValue("@Po_PK", po_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        /// <summary>
        /// get the NonDelivered rolls of a item of a location
        /// </summary>
        /// <param name="skudet_pk"></param>
        /// <param name="lctn_pk"></param>
        /// <returns></returns>

        public DataTable getNonDeliveredRollofaIteminOneLocatiom(int skudet_pk, int lctn_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, RollInventoryMaster.RollInventory_PK, FabricRollmaster.RollNum, RollInventoryMaster.DocumentNum, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup, RollInventoryMaster.IsPresent, FabricRollmaster.AYard, RollInventoryMaster.Location_Pk, FabricRollmaster.IsDelivered, FabricRollmaster.SkuDet_PK
FROM            FabricRollmaster INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK
WHERE        (RollInventoryMaster.IsPresent = N'Y') AND (RollInventoryMaster.Location_Pk = @lctn_pk) AND (FabricRollmaster.IsDelivered = N'N') AND (FabricRollmaster.SkuDet_PK = @skudet_pk)";
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@lctn_pk", lctn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }



        /// <summary>
        /// get all rolls marked 
        /// </summary>
        /// <param name="skudet_pk"></param>
        /// <param name="lctn_pk"></param>
        /// <param name="dor_pk"></param>
        /// <returns></returns>
        public DataTable getNonDeliveredRollofaIteminOneLocatiomagainstADOR(int skudet_pk, int lctn_pk,int dor_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, RollInventoryMaster.RollInventory_PK, FabricRollmaster.RollNum, RollInventoryMaster.DocumentNum, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup, RollInventoryMaster.IsPresent, FabricRollmaster.AYard, RollInventoryMaster.Location_Pk, FabricRollmaster.IsDelivered, FabricRollmaster.SkuDet_PK, 
                         DeliveryReceiptMaster.DOR_PK
FROM            FabricRollmaster INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK INNER JOIN
                         DeliveryOrderMaster ON RollInventoryMaster.DocumentNum = DeliveryOrderMaster.DONum INNER JOIN
                         DeliveryReceiptMaster ON DeliveryOrderMaster.DO_PK = DeliveryReceiptMaster.DO_PK
WHERE        (RollInventoryMaster.IsPresent = N'W') AND (RollInventoryMaster.Location_Pk = @lctn_pk) AND (FabricRollmaster.IsDelivered = N'N') AND (FabricRollmaster.SkuDet_PK = @skudet_pk) AND 
                         (DeliveryReceiptMaster.DOR_PK = @dor_pk)";
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@lctn_pk", lctn_pk);
                cmd.Parameters.AddWithValue("@dor_pk", dor_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }




        /// <summary>
        /// show the roll details even if saved(used for edit)
        /// </summary>
        /// <param name="asn_pk"></param>
        /// <param name="skudet_pk"></param>
        /// <returns></returns>
        public DataTable getRollDetailsofASNandSKUDetPKForEdit(int asn_pk, int skudet_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        Roll_PK, RollNum, Qty, UOM, Remark, SShrink, SYard, SShade, SWidth, AShrink, AShade, AWidth, AYard, SGsm, AGsm, SkuDet_PK, SupplierDoc_pk,Lotnum
FROM            FabricRollmaster
WHERE        (SkuDet_PK = @skudet_pk) AND (SupplierDoc_pk = @asn_pk) ";
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }




        public DataTable GetAllRollsofAtcofColorWithSamegroupofCutorder( int cutid,int Location_Pk,int skudetpk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, RollInventoryMaster.RollInventory_PK, FabricRollmaster.RollNum, RollInventoryMaster.DocumentNum, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup, RollInventoryMaster.IsPresent, FabricRollmaster.AYard, RollInventoryMaster.Location_Pk, FabricRollmaster.IsDelivered, FabricRollmaster.SkuDet_PK, 
                         RollInventoryMaster.AddedVia, RollInventoryMaster.DeliveredVia
FROM            FabricRollmaster INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK INNER JOIN
                         CutOrderMaster ON FabricRollmaster.MarkerType = CutOrderMaster.MarkerType AND FabricRollmaster.WidthGroup = CutOrderMaster.CutWidth AND 
                         FabricRollmaster.ShrinkageGroup = CutOrderMaster.Shrinkage AND FabricRollmaster.SkuDet_PK = CutOrderMaster.SkuDet_pk
WHERE        (RollInventoryMaster.IsPresent = N'Y') AND (RollInventoryMaster.Location_Pk = @Location_Pk) AND (FabricRollmaster.SkuDet_PK = @skudet_pk) AND (CutOrderMaster.CutID = @cutid)";

                cmd.Parameters.AddWithValue("@cutid", cutid);
                cmd.Parameters.AddWithValue("@Location_Pk", Location_Pk);
                cmd.Parameters.AddWithValue("@skudet_pk", skudetpk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }



        /// <summary>
        /// get the MRN Value
        /// </summary>
        /// <param name="mrndet_pk"></param>
        /// <returns></returns>
        public DataTable GetMrnData(int mrndet_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        (MrnDetails.ReceiptQty+MrnDetails.ExtraQty) as receiptQty, UOMMaster.UomCode, MrnDetails.MrnDet_PK
FROM            MrnDetails INNER JOIN
                         UOMMaster ON MrnDetails.Uom_PK = UOMMaster.Uom_PK
WHERE        (MrnDetails.MrnDet_PK = @mrndet_pk)";
                cmd.Parameters.AddWithValue("@mrndet_pk", mrndet_pk);
               
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }

        public DataTable getRollDetailsAsnandFabric(int asn_pk, int skudetpk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        Roll_PK, RollNum, Qty, UOM, Remark, SShrink, SYard, SShade, SWidth,SGsm,SWeight
FROM            FabricRollmaster
WHERE        (SkuDet_PK = @skudetpk) AND (SupplierDoc_pk = @asn_pk) ";
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                cmd.Parameters.AddWithValue("@skudetpk", skudetpk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }




        public DataTable getfullRollDetailsofASNandSKUDetPK(int asn_pk, int skudet_pk)
        {








            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum, FabricRollmaster.SGsm, FabricRollmaster.AGsm, 
                         FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.TotalDefect, FabricRollmaster.TotalDefecton100, FabricRollmaster.TotalPoint, 
                         FabricRollmaster.TotalPointon100yard,FabricRollmaster.Lotnum
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE         (FabricRollmaster.IsSaved = N'Y') AND (FabricRollmaster.SupplierDoc_pk = @asn_pk) and  (FabricRollmaster.SkuDet_PK = @skudet_pk) and FabricRollmaster.Roll_PK not in (Select Roll_PK from CutPlanRollDetails)
";
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }








        public DataTable getfullRollDetailsofASNandSKUDetPKofaWidth(int asn_pk, int skudet_pk,ArrayList widtharray)
        {

            String condition = "";
            for (int i = 0; i < widtharray.Count; i++)
            {
                if (i == 0)
                {
                    condition = " and ( FabricRollmaster.AWidth='" + widtharray[i].ToString().Trim()+"'";
                }
                else
                {
                    condition = condition + "  or FabricRollmaster.AWidth='" + widtharray[i].ToString().Trim() + "'";
                }



            }

          if(condition!="")
            {
                condition = condition + " )";
            }


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum, FabricRollmaster.SGsm, FabricRollmaster.AGsm, 
                         FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.TotalDefect, FabricRollmaster.TotalDefecton100, FabricRollmaster.TotalPoint, 
                         FabricRollmaster.TotalPointon100yard,FabricRollmaster.Lotnum
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE         (FabricRollmaster.IsSaved = N'Y') AND (FabricRollmaster.SupplierDoc_pk = @asn_pk) and  (FabricRollmaster.SkuDet_PK = @skudet_pk)  and FabricRollmaster.Roll_PK not in (Select Roll_PK from CutPlanRollDetails)
" + condition;
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        public DataTable getfullRollDetailsofASNandSKUDetPKofShrinkage(int asn_pk, int skudet_pk, ArrayList widtharray)
        {

            String condition = "";
            for (int i = 0; i < widtharray.Count; i++)
            {
                if (i == 0)
                {
                    condition = " and ( FabricRollmaster.AShrink='" + widtharray[i].ToString().Trim() + "'";
                }
                else
                {
                    condition = condition + "  or FabricRollmaster.AShrink='" + widtharray[i].ToString().Trim() + "'";
                }



            }

            if (condition != "")
            {
                condition = condition + " )";
            }


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum, FabricRollmaster.SGsm, FabricRollmaster.AGsm, 
                         FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.TotalDefect, FabricRollmaster.TotalDefecton100, FabricRollmaster.TotalPoint, 
                         FabricRollmaster.TotalPointon100yard,FabricRollmaster.Lotnum
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE         (FabricRollmaster.IsSaved = N'Y') and FabricRollmaster.Roll_PK not in (Select Roll_PK from CutPlanRollDetails) AND (FabricRollmaster.SupplierDoc_pk = @asn_pk) and  (FabricRollmaster.SkuDet_PK = @skudet_pk) 
" + condition;
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        public DataTable getfullRollDetailsofASNandSKUDetPKofShade(int asn_pk, int skudet_pk, ArrayList widtharray)
        {

            String condition = "";
            for (int i = 0; i < widtharray.Count; i++)
            {
                if (i == 0)
                {
                    condition = " and ( FabricRollmaster.AShade='" + widtharray[i].ToString().Trim() + "'";
                }
                else
                {
                    condition = condition + "  or FabricRollmaster.AShade='" + widtharray[i].ToString().Trim() + "'";
                }



            }

            if (condition != "")
            {
                condition = condition + " )";
            }


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum, FabricRollmaster.SGsm, FabricRollmaster.AGsm, 
                         FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.TotalDefect, FabricRollmaster.TotalDefecton100, FabricRollmaster.TotalPoint, 
                         FabricRollmaster.TotalPointon100yard,FabricRollmaster.Lotnum
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE         (FabricRollmaster.IsSaved = N'Y') and FabricRollmaster.Roll_PK not in (Select Roll_PK from CutPlanRollDetails) AND  (FabricRollmaster.SupplierDoc_pk = @asn_pk) and  (FabricRollmaster.SkuDet_PK = @skudet_pk) 
" + condition;
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


    }


    public class RollInventoryData
    {

        public int roll_PK { get; set; }
        public int rollinventory_pk { get; set; }
        


        public int Location_Pk { get; set; }
        public string DocumentNum { get; set; }
        public string AddedVia { get; set; }
        public string IsPresent { get; set; }
        public string AddedBy { get; set; }
        public DateTime Addeddate { get; set; }



        public DataTable getRollDetailsofATC(int atcid)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, MrnMaster.MrnNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, 
                         FabricRollmaster.SWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, 
                         FabricRollmaster.IsAcceptable, FabricRollmaster.MarkerType, ProcurementMaster.PONum, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId,FabricRollmaster.Lotnum
FROM            MrnDetails INNER JOIN
                         MrnMaster ON MrnDetails.Mrn_PK = MrnMaster.Mrn_PK INNER JOIN
                         FabricRollmaster ON MrnDetails.MrnDet_PK = FabricRollmaster.MRnDet_PK INNER JOIN
                         ProcurementMaster ON MrnMaster.Po_PK = ProcurementMaster.PO_Pk INNER JOIN
                         SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId
WHERE        (AtcMaster.AtcId = @atc_id) and  (FabricRollmaster.IsApproved = N'N') and  (FabricRollmaster.IsSaved = N'Y')";
                cmd.Parameters.AddWithValue("@atc_id", atcid);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        public DataTable getRollAllDetailsofATC(int atcid)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @" SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.Remark, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, 
                         FabricRollmaster.SWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, ISNULL(FabricRollmaster.SGsm, '') AS SGsm, ISNULL(FabricRollmaster.AGsm, '') 
                         AS AGsm, ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' ') 
                         AS itemDescription, SupplierDocumentMaster.SupplierDocnum, SupplierDocumentMaster.AtracotrackingNum, ISNULL(FabricRollmaster.IsAcceptable, '') AS IsAcceptable, ISNULL(FabricRollmaster.MarkerType, '') 
                         AS MarkerType, ISNULL(FabricRollmaster.WidthGroup, '') AS WidthGroup, ISNULL(FabricRollmaster.ShadeGroup, '') AS ShadeGroup, ISNULL(FabricRollmaster.ShrinkageGroup, '') AS ShrinkageGroup, 
                         ISNULL(FabricRollmaster.TotalDefect, '') AS TotalDefect, ISNULL(FabricRollmaster.TotalDefecton100, '') AS TotalDefection100, ISNULL(FabricRollmaster.TotalPoint, '') AS TotalPoint, 
                         ISNULL(FabricRollmaster.TotalPointon100yard, '') AS TotalPointon100yard, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         SupplierMaster ON SupplierDocumentMaster.Supplier_pk = SupplierMaster.Supplier_PK
WHERE        (SkuRawMaterialMaster.Atc_id =@atc_id) ";
                cmd.Parameters.AddWithValue("@atc_id", atcid);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }
        public DataTable getRollDetailsofASN(int asn_pk ,int skudet_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum,FabricRollmaster.Lotnum
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE        (FabricRollmaster.IsApproved = N'N') AND (FabricRollmaster.IsSaved = N'Y') AND (FabricRollmaster.SupplierDoc_pk = @asn_pk)  AND (FabricRollmaster.SkuDet_PK = @skudet_pk)";
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }




        public DataTable getRollDetailsofASNforEdit(int asn_pk, int skudet_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum,FabricRollmaster.Lotnum, FabricRollmaster.TotalDefect, FabricRollmaster.TotalDefecton100, FabricRollmaster.TotalPoint , FabricRollmaster.TotalPointon100yard
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE        (FabricRollmaster.IsApproved = N'A') AND (FabricRollmaster.IsSaved = N'Y') AND (FabricRollmaster.SupplierDoc_pk = @asn_pk)  AND (FabricRollmaster.SkuDet_PK = @skudet_pk)";
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }



        public DataTable GetCompletedataofAsn(int asn_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum, FabricRollmaster.SGsm, FabricRollmaster.AGsm, 
                         FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.TotalDefect, FabricRollmaster.TotalDefecton100, FabricRollmaster.TotalPoint, 
                         FabricRollmaster.TotalPointon100yard,FabricRollmaster.Lotnum
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE        (FabricRollmaster.IsApproved = N'N') AND (FabricRollmaster.IsSaved = N'Y') AND (FabricRollmaster.SupplierDoc_pk = @asn_pk)";
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }





    }




    public class InspectionData
    {
      public DataTable GetDocumentnumber(int atcid)
    {

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = @"SELECT DISTINCT SupplierDocumentMaster.SupplierDocnum + ' / ' + SupplierDocumentMaster.AtracotrackingNum AS name, SupplierDocumentMaster.SupplierDoc_pk AS pk
FROM            SupplierDocumentMaster INNER JOIN
                         FabricRollmaster ON SupplierDocumentMaster.SupplierDoc_pk = FabricRollmaster.SupplierDoc_pk INNER JOIN
                         SkuRawmaterialDetail ON FabricRollmaster.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
WHERE        (SkuRawMaterialMaster.Atc_id = @Param1)";
            cmd.Parameters.AddWithValue("@Param1", atcid);

            return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }
    }


        public DataTable GetDocumentnumberByMRNDETPK(int mrndetpk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT DISTINCT SupplierDocumentMaster.SupplierDocnum + ' / ' + SupplierDocumentMaster.AtracotrackingNum AS name, SupplierDocumentMaster.SupplierDoc_pk AS pk, MrnDetails.MrnDet_PK
FROM            SupplierDocumentMaster INNER JOIN
                         FabricRollmaster ON SupplierDocumentMaster.SupplierDoc_pk = FabricRollmaster.SupplierDoc_pk INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         MrnDetails ON ProcurementDetails.PODet_PK = MrnDetails.PODet_PK
WHERE        (MrnDetails.MrnDet_PK = @Param2)";
                cmd.Parameters.AddWithValue("@Param2", mrndetpk);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }

        public DataTable GetDocumentnumberByMRNDETPKSupplier(int mrndetpk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT DISTINCT SupplierDocumentMaster.SupplierDocnum + ' / ' + SupplierDocumentMaster.AtracotrackingNum AS name, SupplierDocumentMaster.SupplierDoc_pk AS pk, MrnDetails.MrnDet_PK
FROM            MrnDetails INNER JOIN
                         ProcurementDetails ON MrnDetails.PODet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         SupplierDocumentMaster ON ProcurementMaster.Supplier_Pk = SupplierDocumentMaster.Supplier_pk
WHERE        (MrnDetails.MrnDet_PK = @Param2)";
                cmd.Parameters.AddWithValue("@Param2", mrndetpk);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }
    }


}
namespace ArtWebApp.BLL.RollBLL
{




}