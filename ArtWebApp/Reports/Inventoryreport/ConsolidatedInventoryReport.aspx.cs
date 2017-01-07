using ArtWebApp.DataModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ArtWebApp.Reports.Inventoryreport
{
    public partial class ConsolidatedInventoryReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                filltowarehouses();
                FillAtcCombo();
            }

        }


        public void filltowarehouses()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.LocationMasters
                        where order.LocType == "W" || order.LocType == "F"
                        select new
                        {
                            name = order.LocationName,
                            pk = order.Location_PK
                        };
                drp_ToWarehouse.DataSource = q.ToList();
                drp_ToWarehouse.DataBind();

            }
        }




        public void FillAtcCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                entty.Configuration.AutoDetectChangesEnabled = false;
                var q = from atcorder in entty.AtcMasters
                        select new
                        {
                            name = atcorder.AtcNum,
                            pk = atcorder.AtcId
                        };

                // Create a table from the query.
                drp_Atc.DataSource = q.ToList();
                drp_Atc.DataBind();




            }
        }


        protected void btn_showAtcLocInventory_Click(object sender, EventArgs e)
        {
            ArrayList atcArraylist = getAtclist();


            ArrayList LocArraylist = getLoclist();

            String Reportheading = " Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            if (LocArraylist.Count > 0 && LocArraylist != null && atcArraylist.Count > 0 && atcArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportConsolidatedTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportConsolidatedTransaction();
                DataTable dt = invtran.GetInventoryofLocListAndAtcAndType(atcArraylist, LocArraylist, "All");

                showInventoryReport(dt, Reportheading);

            }
        }

        protected void btn_showAtcLocInventory0_Click(object sender, EventArgs e)
        {

        }

        protected void btn_showAtcLocInventory1_Click(object sender, EventArgs e)
        {

        }

        public ArrayList getAtclist()
        {
            ArrayList atcArraylist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_Atc.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int atcid = int.Parse(item.Value.ToString());
                atcArraylist.Add(atcid);
            }
            return atcArraylist;
        }

        public ArrayList getLoclist()
        {

            ArrayList LocArraylist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> locitems = drp_ToWarehouse.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in locitems)
            {

                int atcid = int.Parse(item.Value.ToString());
                LocArraylist.Add(atcid);
            }

            return LocArraylist;
        }







        public void showInventoryReport(DataTable dt, String Msg)
        {
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportParameter rp1 = new ReportParameter("Heading", Msg);

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\InventorySummary.rdlc";
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
        }

        protected void btn_showAtcLocTrimInventory_Click(object sender, EventArgs e)
        {
            ArrayList atcArraylist = getAtclist();


            ArrayList LocArraylist = getLoclist();

            String Reportheading = "Trims  Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            if (LocArraylist.Count > 0 && LocArraylist != null && atcArraylist.Count > 0 && atcArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportConsolidatedTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportConsolidatedTransaction();
                DataTable dt = invtran.GetInventoryofLocListAndAtcAndType(atcArraylist, LocArraylist, "Trim");

                showInventoryReport(dt, Reportheading);

            }
        }

        protected void btn_showAtcLocfabInventory_Click(object sender, EventArgs e)
        {
            ArrayList atcArraylist = getAtclist();


            ArrayList LocArraylist = getLoclist();

            String Reportheading = "Fabric Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            if (LocArraylist.Count > 0 && LocArraylist != null && atcArraylist.Count > 0 && atcArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportConsolidatedTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportConsolidatedTransaction();
                DataTable dt = invtran.GetInventoryofLocListAndAtcAndType(atcArraylist, LocArraylist, "Fabric");

                showInventoryReport(dt, Reportheading);

            }
        }
    }
}