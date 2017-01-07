using ArtWebApp.DataModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.AccountsReport
{
    public partial class FinalCosting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                FillAtcCombo();
            }
        }




        public void FillAtcCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from atcorder in entty.AtcMasters
                        select new
                        {
                            name = atcorder.AtcNum,
                            pk = atcorder.AtcId
                        };

                // Create a table from the query.
                drp_Atc.DataSource = q.ToList();
                drp_Atc.DataTextField = "name";
                drp_Atc.DataValueField = "pk";
                drp_Atc.DataBind();
                Upd_atc.Update();



            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            BLL.MerchandsingBLL .AtcChart atcchrt = new BLL.MerchandsingBLL.AtcChart();

            DataTable dt = atcchrt.GetAtcChart(int.Parse(drp_Atc.SelectedValue.ToString()));
            showreport(dt);
        }



        public void showreport(DataTable dt)
        {
            DBTransaction.ReportTransactions.AccountReportrans acttrsans = new DBTransaction.ReportTransactions.AccountReportrans();

            //System.Data.DataTable dt = acttrsans.GetSUPInvData(int.Parse(drp_voucher.SelectedValue.ToString().Trim()));


            Microsoft.Reporting.WebForms.ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\FinalCosting.rdlc";
        }
    }
}