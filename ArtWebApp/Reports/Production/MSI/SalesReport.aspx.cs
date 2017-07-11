using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.Production.MSI
{
    public partial class SalesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_sales_Click(object sender, EventArgs e)
        {
            loaDSalesReport();
        }
        public void loaDSalesReport()
        {



            DateTime fromdate = dtp_from.Date;
            DateTime Todate = dtp_to.Date;

            System.Data.DataTable dt = DBTransaction.Productiontransaction.SchedularReportTransaction.GetSalesReportofMonth(fromdate, Todate);
            String Reportheading = "Sales report from " + fromdate.ToString("MMMM dd,yyyy") + " to "+ Todate.ToString("MMMM dd,yyyy");
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportParameter rp1 = new ReportParameter("Heading", Reportheading);
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
         
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = Reportheading;
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\SalesReport.rdlc";
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });



        }


        public void loaDShipmentReport()
        {



            DateTime fromdate = dtp_from.Date;
            DateTime Todate = dtp_to.Date;

            System.Data.DataTable dt = DBTransaction.Productiontransaction.SchedularReportTransaction.GetShipmentReportofMonth(fromdate, Todate);
            String Reportheading = "Shipment report from " + fromdate.ToString("MMMM dd,yyyy") + " to " + Todate.ToString("MMMM dd,yyyy");
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportParameter rp1 = new ReportParameter("Heading", Reportheading);
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);

            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = Reportheading;
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\Shipmentreport.rdlc";
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });



        }

        protected void btn_Shipmenthandover_Click(object sender, EventArgs e)
        {
            loaDShipmentReport();
        }
    }
}