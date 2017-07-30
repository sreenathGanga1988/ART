using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class RollTracker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            GridView1.PreRender += new EventHandler(grdcloneProgoffers_PreRender);
        }

        void grdcloneProgoffers_PreRender(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            RollDatasource.SelectCommand = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, LocationMaster.LocationName, RollInventoryMaster.DocumentNum, RollInventoryMaster.AddedVia, RollInventoryMaster.DeliveredVia, 
                         FabricRollmaster.AYard, FabricRollmaster.MarkerType, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.IsCut, RollInventoryMaster.IsPresent, 
                         RollInventoryMaster.Location_Pk, FabricRollmaster.SkuDet_PK, FabricRollmaster.IsDelivered, SupplierDocumentMaster.SupplierDocnum+'/'+SupplierDocumentMaster.AtracotrackingNum as ASN ,(
SELECT STUFF((SELECT ',' + LayRollRef 
            FROM (SELECT        LaySheetRollMaster.LayRollRef
FROM            LaySheetRollDetails INNER JOIN
                         LaySheetRollMaster ON LaySheetRollDetails.LaysheetRollmaster_Pk = LaySheetRollMaster.LaysheetRollmaster_Pk
GROUP BY LaySheetRollDetails.Roll_PK, LaySheetRollMaster.LayRollRef
HAVING        (LaySheetRollDetails.Roll_PK = FabricRollmaster.Roll_PK))tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as LaysheetNUM,(ISNULL(SkuRawMaterialMaster.RMNUM, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') )
                         AS itemDescription
FROM            FabricRollmaster INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK INNER JOIN
                         LocationMaster ON RollInventoryMaster.Location_Pk = LocationMaster.Location_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk
WHERE        (FabricRollmaster.SkuDet_PK = " + txt_pk.Text+")";
            RollDatasource.DataBind();
            GridView1.DataSource = RollDatasource;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            RollDatasource.SelectCommand = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, LocationMaster.LocationName, RollInventoryMaster.DocumentNum, RollInventoryMaster.AddedVia, RollInventoryMaster.DeliveredVia, 
                         FabricRollmaster.AYard, FabricRollmaster.MarkerType, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.IsCut, RollInventoryMaster.IsPresent, 
                         RollInventoryMaster.Location_Pk, FabricRollmaster.SkuDet_PK, FabricRollmaster.IsDelivered, SupplierDocumentMaster.SupplierDocnum+'/'+SupplierDocumentMaster.AtracotrackingNum as ASN ,(
SELECT STUFF((SELECT ',' + LayRollRef 
            FROM (SELECT        LaySheetRollMaster.LayRollRef
FROM            LaySheetRollDetails INNER JOIN
                         LaySheetRollMaster ON LaySheetRollDetails.LaysheetRollmaster_Pk = LaySheetRollMaster.LaysheetRollmaster_Pk
GROUP BY LaySheetRollDetails.Roll_PK, LaySheetRollMaster.LayRollRef
HAVING        (LaySheetRollDetails.Roll_PK = FabricRollmaster.Roll_PK))tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as LaysheetNUM,(ISNULL(SkuRawMaterialMaster.RMNUM, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') )
                         AS itemDescription
FROM            FabricRollmaster INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK INNER JOIN
                         LocationMaster ON RollInventoryMaster.Location_Pk = LocationMaster.Location_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pkINNER JOIN
                         SkuRawmaterialDetail ON FabricRollmaster.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
WHERE        (SupplierDocumentMaster.SupplierDoc_pk= " + txt_pk.Text + ")";
            RollDatasource.DataBind();
            GridView1.DataSource = RollDatasource;
            GridView1.DataBind();
        }
    }
}