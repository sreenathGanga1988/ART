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
    public partial class SalesandLoanReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            loaDSDOeport();
        }



        public void loaDSDOeport()
        {

            String dotypestrng = "";
            
           DBTransaction.ReportTransactions.ReporterTrans repotrans = new DBTransaction.ReportTransactions.ReporterTrans();


            int sdopk = int.Parse(drp_sdo.SelectedItem.Value.ToString());
            using (DataModels.ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var dotype = entty.InventorySalesMasters.Where(u => u.SalesDO_PK == sdopk).Select(u => u.DoType).FirstOrDefault();
                dotypestrng = dotype.ToString().Trim();
            }
            
            DataTable dt = repotrans.GetSalesDO(sdopk, dotypestrng);

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = drp_sdo.SelectedItem.ToString();
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\SalesDO.rdlc";


        }

        public void loaDLoanReport()
        {



            DBTransaction.ReportTransactions.ReporterTrans repotrans = new DBTransaction.ReportTransactions.ReporterTrans();

            DataTable dt = repotrans.GetLoanReport(int.Parse(drp_loan.SelectedItem.Value.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = drp_loan.SelectedItem.ToString();
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\LOAN.rdlc";


        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            loaDLoanReport();
        }
    }
}