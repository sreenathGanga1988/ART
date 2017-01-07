using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.MerchandiserReport
{
    public partial class ASQReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_showpo_Click(object sender, EventArgs e)
        {
            DBTransaction.ReportTransactions.GeneralReportTrans invtran = new DBTransaction.ReportTransactions.GeneralReportTrans();

            DataTable dt = invtran.GetASQShuffle(int.Parse(dll_reg.SelectedValue.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\ASQShuffleReport.rdlc";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {



            //DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();

            //DataTable dt = invtran.GetExtraBOMRequest(int.Parse(drp_extrabom.SelectedValue.ToString()));

            //ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            //this.ReportViewer1.LocalReport.DataSources.Clear();
            //this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            //this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\ExtraReqQty.rdlc";
        }
    }
}