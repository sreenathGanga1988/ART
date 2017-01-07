using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.General_Reports
{
    public partial class GeneralReportform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                String navtype = Request.QueryString["navtype"];


                if (navtype == "CuttingTicket")
                {
                    String Cutid = Request.QueryString["ID"];
                    fillCuttingDetail(int.Parse(Cutid.ToString()));
                }
                if (navtype == "CurrierDetails")
                {
                    String Cutid = Request.QueryString["ID"];
                    fillCuttingDetail(int.Parse(Cutid.ToString()));
                }
            }

           
        }



        public void fillCuttingDetail(int Cutid)
        {
            DBTransaction.ReportTransactions.GeneralReportTrans gentrans = new DBTransaction.ReportTransactions.GeneralReportTrans();

            System.Data.DataTable dt = gentrans.GetCuttingDetails(Cutid);


            Microsoft.Reporting.WebForms.ReportDataSource datasource = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\CuttingDetailsReport.rdlc";
        }


    }
}