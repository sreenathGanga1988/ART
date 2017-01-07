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
    public partial class RoReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_showpo_Click(object sender, EventArgs e)
        {
            loaDROreport();
        }



        public void loaDROreport()
        {



            DBTransaction.ProcurementTransaction potrsans = new DBTransaction.ProcurementTransaction();

            DataTable dt = potrsans.GetROData(int.Parse(drp_spo.SelectedValue.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\ro.rdlc";


        }

        public void loadstockreport()
        {



            DBTransaction.ProcurementTransaction potrsans = new DBTransaction.ProcurementTransaction();

            DataTable dt = potrsans.GetStockROData(int.Parse(drp_smrn.SelectedValue.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\sro.rdlc";


        }

        protected void btn_showmrn_Click(object sender, EventArgs e)
        {
            loadstockreport();
        }
    }
}