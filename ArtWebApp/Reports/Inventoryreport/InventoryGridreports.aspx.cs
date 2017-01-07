using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Infragistics.Web.UI.GridControls;
using System.Data;
using ArtWebApp.DataModels;
namespace ArtWebApp.Reports.Inventoryreport
{
    public partial class InventoryGridreports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                FillAtcCombo();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using(SqlCommand cmd= new SqlCommand ())
            {
                cmd.CommandText = @"SELECT        AtcNum as ATC# , PONum as PO#, SupplierName as Supplier, DeliveryDate as [Delivery Date], IsApproved as Approved, POType, RMNum, ItemDescription as [Item Description], ItemColor, ItemSize, SupplierSize, SupplierColor, POQty, RcvdQty, ExtraQty,(POQty-RcvdQty) as Balance
FROM            (SELECT        AtcMaster.AtcNum, ProcurementMaster.PONum, SupplierMaster.SupplierName, ProcurementMaster.DeliveryDate, ProcurementMaster.IsApproved, ProcurementMaster.POType, 
                                                    SkuRawMaterialMaster.RMNum, 
                                                    SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS ItemDescription, 
                                                    SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, ProcurementDetails.POQty, 
                                                    ISNULL(SUM(MrnDetails.ReceiptQty), 0) AS RcvdQty, ISNULL(SUM(MrnDetails.ExtraQty), 0) AS ExtraQty
                          FROM            ProcurementMaster INNER JOIN
                                                    ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk INNER JOIN
                                                    AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                                                    SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                                                    SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                                                    SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk LEFT OUTER JOIN
                                                    MrnDetails ON ProcurementDetails.PODet_PK = MrnDetails.PODet_PK
												
                                                   GROUP BY AtcMaster.AtcNum, ProcurementMaster.PONum, SupplierMaster.SupplierName, ProcurementMaster.DeliveryDate, ProcurementMaster.IsApproved, ProcurementMaster.POType, 
                                                    SkuRawMaterialMaster.RMNum, SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width, 
                                                    SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, ProcurementDetails.POQty
                                        ) AS tt
WHERE        (POQty - RcvdQty > 0)";

                WebDataGrid1.DataSource = null;
                WebDataGrid1.DataBind();
                System.Data.DataTable dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
                WebDataGrid1.DataSource = dt;
                WebDataGrid1.DataBind();
                ViewState["Dataongrid"] = dt;  
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
           






            //string fileName = HttpUtility.UrlEncode(this.tbFileName.Text);
            //fileName = fileName.Replace("+", "%20");
            //fileName = HttpUtility.UrlDecode(fileName);


            if ((DataTable)ViewState["Dataongrid"] != null)
            {

                WebDataGrid1.DataSource = (DataTable)ViewState["Dataongrid"];
                WebDataGrid1.DataBind();
              



                this.WebExcelExporter1.Export(true, this.WebDataGrid1);
            }

          



                    //this.WebExcelExporter1.DownloadName = fileName;
                    
                    //this.WebExcelExporter1.Export(this.WebDataGrid1); 
            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        tt.Gt_PK, tt.AtcNum, tt.RMNum, tt.Description, tt.ItemColor, tt.ItemSize, tt.SupplierSize, tt.SupplierColor, tt.TransitQty, tt.UomCode, tt.CURate, tt.Value, tt.ItemGroupName, tt.DONum, tt.LocationName, 
                         LocationMaster_1.LocationName AS FromLocation
FROM            (SELECT        TOP (100) PERCENT GoodsInTransit.Gt_PK, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum, 
                                                    SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, 
                                                    SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, GoodsInTransit.TransitQty, UOMMaster.UomCode, 
                                                    InventoryMaster.CURate, InventoryMaster.CURate * GoodsInTransit.TransitQty AS Value, ItemGroupMaster.ItemGroupName, DeliveryOrderMaster.DONum, LocationMaster.LocationName
                          FROM            InventoryMaster INNER JOIN
                                                    ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                                                    SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                                                    SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                                                    UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                                                    AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                                                    Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                                                    ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                                                    GoodsInTransit ON InventoryMaster.InventoryItem_PK = GoodsInTransit.InventoryItem_PK INNER JOIN
                                                    DeliveryOrderMaster ON GoodsInTransit.DO_PK = DeliveryOrderMaster.DO_PK INNER JOIN
                                                    LocationMaster ON DeliveryOrderMaster.ToLocation_PK = LocationMaster.Location_PK
                          ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, 
                                                    UOMMaster.UomCode) AS tt INNER JOIN
                         DeliveryOrderMaster AS DeliveryOrderMaster_1 ON tt.DONum = DeliveryOrderMaster_1.DONum INNER JOIN
                         LocationMaster AS LocationMaster_1 ON DeliveryOrderMaster_1.FromLocation_PK = LocationMaster_1.Location_PK
WHERE        (tt.TransitQty > 0)";


                WebDataGrid1.DataSource = null;
                WebDataGrid1.DataBind();
                System.Data.DataTable dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
                WebDataGrid1.DataSource = dt;
                WebDataGrid1.DataBind();
                ViewState["Dataongrid"] = dt;  
            }
        }


        public void FillAtcCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from atcorder in entty.AtcMasters
                        select new
                        {
                            name = atcorder.AtcNum,
                            pk = atcorder.AtcId
                        };

                // Create a table from the query.

                var q1 = from order in entty.LocationMasters
                         where order.LocType == "F"
                         select new
                         {
                             name1 = order.LocationName,
                             pk1 = order.Location_PK
                         };
                ddl_atc.DataSource = q.ToList();
                ddl_atc.DataTextField = "name";
                ddl_atc.DataValueField = "pk";
                ddl_atc.DataBind();

                ddl_location.DataSource = q1.ToList();
                ddl_location.DataTextField = "name1";
                ddl_location.DataValueField = "pk1";
                ddl_location.DataBind();
               


            }
        }

        protected void btn_fabpo_Click(object sender, EventArgs e)
        {

            getPOofAtc(int.Parse(ddl_atc.SelectedValue.ToString()), "F");

     

        }



        public void getApprovalpendingpo()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT ProcurementMaster.PO_Pk, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName, CurrencyMaster.CurrencyCode, SUM(ProcurementDetails.POQty * ProcurementDetails.POUnitRate) AS POValue, ProcurementMaster.AddedDate, ProcurementMaster.AddedBy, ProcurementMaster.IsDeleted FROM ProcurementMaster INNER JOIN SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN CurrencyMaster ON ProcurementMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk GROUP BY ProcurementMaster.PO_Pk, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName, CurrencyMaster.CurrencyCode, ProcurementMaster.AddedDate, ProcurementMaster.AddedBy, ProcurementMaster.IsApproved, ProcurementMaster.IsDeleted HAVING (ProcurementMaster.IsApproved = N'N') AND (ProcurementMaster.IsDeleted !=N'Y')";

              
                WebDataGrid1.DataSource = null;
                WebDataGrid1.DataBind();
                System.Data.DataTable dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
                WebDataGrid1.DataSource = dt;
                WebDataGrid1.DataBind();
                ViewState["Dataongrid"] = dt;
            }

        }


        public void getPOofAtc(int atcid, String potype)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @" SELECT        ProcurementMaster.PO_Pk, ProcurementMaster.PONum, SupplierMaster.SupplierName, ProcurementMaster.POType, SUM(ProcurementDetails.POQty * ProcurementDetails.POUnitRate) AS POValue, 
                         CurrencyMaster.CurrencyCode
FROM            ProcurementMaster INNER JOIN
                         CurrencyMaster ON ProcurementMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                         SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk
GROUP BY ProcurementMaster.PONum, CurrencyMaster.CurrencyCode, SupplierMaster.SupplierName, ProcurementMaster.PO_Pk, ProcurementMaster.POType, ProcurementDetails.POQty, ProcurementDetails.POUnitRate, 
                         ProcurementMaster.AtcId
HAVING        (ProcurementMaster.AtcId = @atcid) AND (ProcurementMaster.POType =@potype)";

                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@potype", potype);
                WebDataGrid1.DataSource = null;
                WebDataGrid1.DataBind();
                System.Data.DataTable dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
                WebDataGrid1.DataSource = dt;
                WebDataGrid1.DataBind();
                ViewState["Dataongrid"] = dt;
            }

        }




        protected void btn_trimofatc_Click(object sender, EventArgs e)
{
    getPOofAtc(int.Parse(ddl_atc.SelectedValue.ToString()), "T");
}

        protected void btn_popending_Click(object sender, EventArgs e)
        {
            getApprovalpendingpo();
        }
    }
}