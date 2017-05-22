using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.Production.MSI
{
    public partial class MonthWiseTargetandShipped : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }

        protected void Button3_Click1(object sender, EventArgs e)
        {

            int year = int.Parse(cmb_year.SelectedItem.Text);

            int month = int.Parse(cmb_Month.SelectedValue.ToString());

            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            lbl_fromdate.Text = startDate.ToString();
            lbl_todate.Text = endDate.ToString();

            loaDYearlyTargetShippedReport();

        }




        public void loaDYearlyTargetShippedReport()
        {




            int year = int.Parse(cmb_year.SelectedItem.Text);
            int month = int.Parse(cmb_Month.SelectedValue.ToString ());
            DateTime fromdate = DateTime.Parse(lbl_fromdate.Text.ToString());
            String Reportheading = "Report for the month of " + cmb_Month.SelectedItem.ToString () + " ," + year;

            DateTime todate = DateTime.Parse(lbl_todate.Text.ToString());
            System.Data.DataTable dt = DBTransaction.Productiontransaction.SchedularReportTransaction.getShippedTargetandShorClosedofMonth(year,month,fromdate,todate);
          
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = "Report for the month of "+ cmb_Month.SelectedItem.ToString() + " ,"+year;
            ReportParameter rp1 = new ReportParameter("Heading", Reportheading);
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\MonthlyReport.rdlc";



          




          
        

        }






    }
}