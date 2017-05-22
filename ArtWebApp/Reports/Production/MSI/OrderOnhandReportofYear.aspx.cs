using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.Production.MSI
{
    public partial class OrderOnhandReportofYear : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }

        protected void Button3_Click1(object sender, EventArgs e)
        {



            loaDYearlyShippedReport();

        }



        public void loaDYearlyShippedReport()
        {



            int year = int.Parse(cmb_year.SelectedItem.Text);

            System.Data.DataTable dt = DBTransaction.Productiontransaction.SchedularReportTransaction.OrderonHandofyear(year);
            String Reportheading = "OrderOnhand  report for Year " + year + "as of " + DateTime.Now.Date.ToString () ;



            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);


            ReportParameter rp1 = new ReportParameter("Heading", Reportheading);
            this.ReportViewer1.LocalReport.DisplayName = "Report for the Year of " + year;
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\OrderOnhand.rdlc";
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });



        }






    }
}