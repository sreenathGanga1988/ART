using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Repository
{
    public class AtcPerformanceRepo
    {


        public DataTable GetAtcTemplateData(int atcid, int itemgroup)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString()))
            {

                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        FabricName, ColorCode, CURate, Uom_PK, Atc_id, Qty, UomCode ,qty*CURate as PurchaseValue
FROM            (SELECT      ISNULL(Template_Master.Description, '')+  ISNULL(SkuRawMaterialMaster.Composition, '') + ISNULL(SkuRawMaterialMaster.Construction, '') + ISNULL(SkuRawMaterialMaster.Weight, '') + ISNULL(SkuRawMaterialMaster.Width, '') 
                                                    + ISNULL(Template_Master.Description, '') AS FabricName, SkuRawmaterialDetail.ColorCode, ProcurementDetails.CURate, ProcurementDetails.Uom_PK, SkuRawMaterialMaster.Atc_id, 
                                                    SUM(MrnDetails.ReceiptQty) AS Qty, UOMMaster.UomCode
                          FROM            SkuRawmaterialDetail INNER JOIN
                                                    SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                                                    Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                                                    ProcurementDetails ON SkuRawmaterialDetail.SkuDet_PK = ProcurementDetails.SkuDet_PK INNER JOIN
                                                    MrnDetails ON ProcurementDetails.PODet_PK = MrnDetails.PODet_PK INNER JOIN
                                                    UOMMaster ON MrnDetails.Uom_PK = UOMMaster.Uom_PK
                          WHERE        (Template_Master.ItemGroup_PK = @Itemgroup)
                          GROUP BY ISNULL(Template_Master.Description, '') + ISNULL(SkuRawMaterialMaster.Composition, '') + ISNULL(SkuRawMaterialMaster.Construction, '') + ISNULL(SkuRawMaterialMaster.Weight, '') + ISNULL(SkuRawMaterialMaster.Width, '') 
                                                    + ISNULL(Template_Master.Description, ''), SkuRawmaterialDetail.ColorCode, ProcurementDetails.CURate, ProcurementDetails.Uom_PK, SkuRawMaterialMaster.Atc_id, UOMMaster.UomCode
                          HAVING         (SkuRawMaterialMaster.Atc_id = @AtcID)) AS tt");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Itemgroup", itemgroup);
                cmd.Parameters.AddWithValue("@AtcID", atcid);

                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);

            }

            return dt;
        }
    }
}