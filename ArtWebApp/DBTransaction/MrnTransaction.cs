using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.DBTransaction
{
    public class MrnTransaction
    {
        String connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;

        public DataTable GetPODetails(int po_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"  select tt.SkuDet_PK, tt.PODet_PK ,tt.RMNum, tt.Description,tt.ItemColor,tt.ItemSize,tt.SupplierColor,tt.SupplierSize,tt.UomCode,tt.POQty,tt.ReceivedQty,(tt.POQty-tt.ReceivedQty)as BalanceQty,tt.CURate,tt.Uom_PK from 
(SELECT        ProcurementDetails.PODet_PK, ProcurementDetails.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierColor, ProcurementDetails.SupplierSize, UOMMaster.UomCode, ProcurementDetails.POQty,ProcurementDetails.CURate, ProcurementDetails.Uom_PK,
                             (SELECT        ISNULL(SUM(receiptqty) + SUM(ExtraQty), 0) AS Expr1
                               FROM            MrnDetails
                               WHERE        (PODet_PK = ProcurementDetails.PODet_PK) AND (SkuDet_PK = ProcurementDetails.SkuDet_PK)) AS ReceivedQty
FROM            ProcurementDetails INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK
WHERE        (ProcurementDetails.PO_Pk = @po_pk))tt ", con);
                cmd.Parameters.AddWithValue("@po_pk", po_pk);
            
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }




        public DataTable getMultiPodetails(String Qry)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(Qry , con);
                

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }




        public DataTable GetPODetailsforMRN(int po_pk ,int doc_pk)
        {
            DataTable dt = new DataTable();

           



                SqlCommand cmd = new SqlCommand(@"GetDataforMRN_SP");
                cmd.Parameters.AddWithValue("@po_pk", po_pk);
                cmd.Parameters.AddWithValue("@Doc_PK", doc_pk);
            cmd.CommandType = CommandType.StoredProcedure;
            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

             
        }





    }
}

