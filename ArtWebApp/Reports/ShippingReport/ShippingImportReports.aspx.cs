using ArtWebApp.DataModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.ShippingReport
{
    public partial class ShippingImportReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillIMP();
                fillDocumentNum();
            }
        }

        protected void btn_showPO_Click(object sender, EventArgs e)
        {
            loadIMPreport();
        }


        public void loadIMPreport()
        {

            DBTransaction.ReportTransactions.ShippingReportTran prdtran = new DBTransaction.ReportTransactions.ShippingReportTran();
            //  adapt.Connection.ConnectionString = Program.ConnStr;
            DataTable dt = prdtran.GetIMP(int.Parse(drp_imp.SelectedValue.ToString()));
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\ExportReport.rdlc";
        }

        public void LoadDocReport()
        {

            DBTransaction.ReportTransactions.ShippingReportTran prdtran = new DBTransaction.ReportTransactions.ShippingReportTran();
            //  adapt.Connection.ConnectionString = Program.ConnStr;
            DataTable dt = prdtran.GetDOC(int.Parse(drp_doc.SelectedValue.ToString()));
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\DocReport.rdlc";
        }

        public void LoadCourierReport()
        {

            DBTransaction.ReportTransactions.ShippingReportTran prdtran = new DBTransaction.ReportTransactions.ShippingReportTran();
            //  adapt.Connection.ConnectionString = Program.ConnStr;
            DataTable dt = prdtran.GetCourierDetailBetween(DateTime.Parse (dtp_fromdate.Value.ToString ()) , DateTime.Parse(dtp_todate.Value.ToString()));
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\CorierDetailsReport.rdlc";
        }



        public void fillIMP()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.ShippingDocumentMasters

                        select new
                        {
                            name = order.ShipDocNum,
                            pk = order.ShipingDoc_PK
                        };

                // Create a table from the query.
                drp_imp.DataSource = q.ToList();
                drp_imp.DataBind();



                // Bind the table to a System.Windows.Forms.BindingSource object, 
                // which acts as a proxy for a System.Windows.Forms.DataGridView object.

            }
        }



        public void fillDocumentNum()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.DocMasters

                        select new
                        {
                            name = order.DocNum,
                            pk = order.Doc_Pk,
                        };

                // Create a table from the query.
                drp_doc.DataSource = q.ToList();
                drp_doc.DataBind();



                // Bind the table to a System.Windows.Forms.BindingSource object, 
                // which acts as a proxy for a System.Windows.Forms.DataGridView object.

            }
        }

        protected void btn_showDoc_Click(object sender, EventArgs e)
        {
            LoadDocReport();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadCourierReport();
        }
    }
}