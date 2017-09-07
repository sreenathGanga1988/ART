using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
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


            String Reportheading = "Shipment Report for the month of " + cmb_Month.SelectedItem.ToString () + " ," + year+"  as of "+ DateTime.Now.ToString("dd/MM/yyyy");

            DateTime todate = DateTime.Parse(lbl_todate.Text.ToString());
            System.Data.DataTable dt = DBTransaction.Productiontransaction.SchedularReportTransaction.GetShippedTargetandShorClosedofMonth(year,month,fromdate,todate);
          
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = "Report for the month of "+ cmb_Month.SelectedItem.ToString() + " ,"+year;
            ReportParameter rp1 = new ReportParameter("Heading", Reportheading);
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\MonthlyReport.rdlc";



          




          
        

        }





        public void loaDYearlyTargetofMonth()
        {




            int year = int.Parse(cmb_year.SelectedItem.Text);
            int month = int.Parse(cmb_Month.SelectedValue.ToString());
         
            String Reportheading = "Target for the month of " + cmb_Month.SelectedItem.ToString() + " ," + year;

          
            System.Data.DataTable dt = DBTransaction.Productiontransaction.SchedularReportTransaction.GetTargetofMonth(year, month);

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = "Target for the month of " + cmb_Month.SelectedItem.ToString() + " ," + year;
            ReportParameter rp1 = new ReportParameter("Heading", Reportheading);
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\TargetofMonth.rdlc";











        }




        public void loadsTargetofMonthWithDetails()
        {

            int year = int.Parse(cmb_year.SelectedItem.Text);
            int month = int.Parse(cmb_Month.SelectedValue.ToString());
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\TargetofMonthWithdetails.rdlc";
            this.ReportViewer1.LocalReport.DataSources.Clear();
            String Reportheading = "Target for the month of " + cmb_Month.SelectedItem.ToString() + " ," + year;


            System.Data.DataTable dt = DBTransaction.Productiontransaction.SchedularReportTransaction.GetTargetofMonth(year, month);


            DataTable dt1 = DBTransaction.Productiontransaction.SchedularReportTransaction.PreponedofMonth(year, month);

            DataTable dt2 = DBTransaction.Productiontransaction.SchedularReportTransaction.ShortcloseofMonth(year, month);


    
            

            //DataTable dt = padapt.GetDataBy(drp_PO.SelectedItem.Text.Trim());
            //dt1 = adapt.GetDataByPO(int.Parse(drp_PO.SelectedItem.Value.ToString()));
            //Microsoft.Reporting.WebForms.ReportDataSource DataSet1= new ReportDataSource () ;
            //DataSet1.Value = dt;
            //Microsoft.Reporting.WebForms.ReportDataSource DataSet2 = new ReportDataSource();
            //DataSet2.Value = dt1;
            // this.ReportViewer1.LocalReport.DataSources.Add(DataSet1);
            // this.ReportViewer1.LocalReport.DataSources.Add(DataSet2);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rd1 = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Add(rd1);
            if (dt1 != null)
            {
                ReportDataSource rd2 = new ReportDataSource("DataSet2", dt1);
                ReportViewer1.LocalReport.DataSources.Add(rd2);
            }
            if (dt2 != null)
            {
                ReportDataSource rd3 = new ReportDataSource("DataSet3", dt2);
                ReportViewer1.LocalReport.DataSources.Add(rd3);
            }
            this.ReportViewer1.LocalReport.DisplayName = "Target for the month of " + cmb_Month.SelectedItem.ToString() + " ," + year;
            ReportParameter rp1 = new ReportParameter("Heading", Reportheading);
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            ReportViewer1.LocalReport.Refresh();

        }





        protected void Button1_Click(object sender, EventArgs e)
        {
            loaDYearlyTargetofMonth();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            loadsTargetofMonthWithDetails();
        }
    }
}