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
    public partial class StockMrnReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            hdnSpo_Pk.Value = drp_spo.SelectedValue.ToString();
            drp_smrn.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            loaDSMRNreport();
        }

        protected void btn_showpo_Click(object sender, EventArgs e)
        {
            loaDSPOreport();
        }

        public void loaDSPOreport()
        {



            DBTransaction.ProcurementTransaction potrsans = new DBTransaction.ProcurementTransaction();

            DataTable dt = potrsans.GetsPOData(int.Parse(drp_spo.SelectedItem.Value.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = drp_spo.SelectedItem.ToString();
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\sPO.rdlc";


        }


        public void loaDSMRNreport()
        {



            DBTransaction.ReportTransactions.ReporterTrans repotrans = new  DBTransaction.ReportTransactions.ReporterTrans();

            DataTable dt = repotrans.GetSmrnData(int.Parse(drp_smrn.SelectedItem.Value.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = drp_smrn.SelectedItem.ToString();
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\STOCK-MRN.rdlc";


        }
        public void loaDSDOeport()
        {



            DBTransaction.ReportTransactions.ReporterTrans repotrans = new DBTransaction.ReportTransactions.ReporterTrans();

            DataTable dt = repotrans.GetSDOData(int.Parse(drp_sdo.SelectedItem.Value.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = drp_sdo.SelectedItem.ToString();
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\stockdo.rdlc";


        }

        protected void btn_showstockro_Click(object sender, EventArgs e)
        {
            loaDSMRNreport();
        }

        protected void btn_sdo_Click(object sender, EventArgs e)
        {
            loaDSDOeport();
        }
    }
}