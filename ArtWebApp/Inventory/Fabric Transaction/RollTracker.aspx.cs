﻿using System;
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
                         RollInventoryMaster.Location_Pk, FabricRollmaster.SkuDet_PK, FabricRollmaster.IsDelivered, SupplierDocumentMaster.SupplierDocnum+'/'+SupplierDocumentMaster.AtracotrackingNum as ASN
FROM            FabricRollmaster INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK INNER JOIN
                         LocationMaster ON RollInventoryMaster.Location_Pk = LocationMaster.Location_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk
WHERE        (FabricRollmaster.SkuDet_PK = "+txt_pk.Text+")";
            RollDatasource.DataBind();
            GridView1.DataSource = RollDatasource;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            RollDatasource.SelectCommand = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, LocationMaster.LocationName, RollInventoryMaster.DocumentNum, RollInventoryMaster.AddedVia, RollInventoryMaster.DeliveredVia, 
                         FabricRollmaster.AYard, FabricRollmaster.MarkerType, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.IsCut, RollInventoryMaster.IsPresent, 
                         RollInventoryMaster.Location_Pk, FabricRollmaster.SkuDet_PK, FabricRollmaster.IsDelivered, SupplierDocumentMaster.SupplierDocnum+'/'+SupplierDocumentMaster.AtracotrackingNum as ASN
FROM            FabricRollmaster INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK INNER JOIN
                         LocationMaster ON RollInventoryMaster.Location_Pk = LocationMaster.Location_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk
WHERE        (SupplierDocumentMaster.SupplierDoc_pk= " + txt_pk.Text + ")";
            RollDatasource.DataBind();
            GridView1.DataSource = RollDatasource;
            GridView1.DataBind();
        }
    }
}