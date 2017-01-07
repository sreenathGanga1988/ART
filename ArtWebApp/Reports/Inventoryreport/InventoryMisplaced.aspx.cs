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
    }
}