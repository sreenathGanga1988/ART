using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.Production.MSI
{
    public partial class YearlyShippedOrTargetReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }

        protected void Button3_Click1(object sender, EventArgs e)
        {

            loaDYearlyTargetShippedReport();




        }




        public void loaDYearlyTargetShippedReport()
        {




            int year = int.Parse(cmb_year.SelectedItem.Text);
            int month = int.Parse(cmb_Month.SelectedValue.ToString());
      ;

     
            System.Data.DataTable dt = DBTransaction.Productiontransaction.SchedularReportTransaction.GetBuyerWiseTargetorShippedofyear(year, month);

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = "Yearly Report for the month of " + month + " ," + year;
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\Targetshipmentdetails.rdlc";


        }






    }
}