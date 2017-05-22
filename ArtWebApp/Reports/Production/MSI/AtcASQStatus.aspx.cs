using ArtWebApp.DataModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.Production.MSI
{
    public partial class AtcASQStatus : System.Web.UI.Page
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
                drp_Atc.DataBind();




            }
        }

        protected void btn_showPO_Click(object sender, EventArgs e)
        {
            loaDYearlyShippedReport();
        }


        public void loaDYearlyShippedReport()
        {



            int atcid = int.Parse(drp_Atc.SelectedValue.ToString ());
          
            System.Data.DataTable dt = DBTransaction.Productiontransaction.SchedularReportTransaction.GetAtcwiseMonthlyShippedReport(atcid);
            String Reportheading = "AtcWise report for M" + drp_Atc.SelectedItem.ToString ();
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportParameter rp1 = new ReportParameter("Heading", Reportheading);
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
           
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = "AtcWise report for M" + drp_Atc.SelectedItem.ToString();
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\aTCasqsTATUS.rdlc";



        }
    }
}