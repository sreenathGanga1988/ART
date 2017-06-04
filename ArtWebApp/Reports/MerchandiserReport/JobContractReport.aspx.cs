using Microsoft.Reporting.WebForms;
using System;
using System.Data;

namespace ArtWebApp.Reports.MerchandiserReport
{
    public partial class JobContractReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            hdnatc_Pk.Value = drp_spo.SelectedValue.ToString();
            drp_jc.DataBind();
        }

        protected void btn_showjc_Click(object sender, EventArgs e)
        {
            loaDjcreport();
        }


        public void loaDjcreport()
        {



            DBTransaction.ReportTransactions.ReporterTrans repotrans = new DBTransaction.ReportTransactions.ReporterTrans();

            DataTable dt = repotrans.GetJCData(int.Parse(drp_jc.SelectedValue.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\JOB COntract-CM.rdlc";


        }
        public void loaDjcreportNewBasedonAllocation()
        {



            DBTransaction.ReportTransactions.ReporterTrans repotrans = new DBTransaction.ReportTransactions.ReporterTrans();

            DataTable dt = repotrans.GetJCDataNewBasedonAllocation(int.Parse(drp_jc.SelectedValue.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\JOB COntract-CM.rdlc";


        }
        protected void btn_jcnew_Click(object sender, EventArgs e)
        {

        }
    }
}