using ArtWebApp.DataModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.Inventoryreport
{
    public partial class InventoryMisplaced : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            filltowarehouses();
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

                // Create a table from the query.
                drp_fromWarehouse.DataSource = q.ToList();
                drp_fromWarehouse.DataValueField = "pk";
                drp_fromWarehouse.DataTextField = "name";
                drp_fromWarehouse.DataBind();



                // Bind the table to a System.Windows.Forms.BindingSource object, 
                // which acts as a proxy for a System.Windows.Forms.DataGridView object.

            }
        }
        protected void btn_showpo_Click(object sender, EventArgs e)
        {
            DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();

            DataTable dt = invtran.GetInventoryMisplaced(int.Parse (dll_reg.SelectedValue.ToString ()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\InventoryMissedReplaced.rdlc";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();

            DataTable dt = invtran.GetInventoryMisplacedofAtc(int.Parse(cmb_atc0.SelectedValue.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\MisplacementofAtc.rdlc";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();

            DataTable dt = invtran.GetInventoryMisplacedofFactory(int.Parse(drp_fromWarehouse.SelectedValue.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\MisplacementofFactory.rdlc";
        }
    }
}