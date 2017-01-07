using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports
{
    public partial class View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                String navtype = Request.QueryString["navtype"];

                string v = Request.QueryString["navtype"];
                if (navtype == "PO")
                {
                    String Ponum = Request.QueryString["ponum"].ToString().Trim();
                    loadPOreport(Ponum);
                }
            }
           
        }


        public void loadPOreport(String POnum)
        {



            DBTransaction.ProcurementTransaction potrsans = new DBTransaction.ProcurementTransaction();

            DataTable dt = potrsans.GetPOData(POnum);

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            if (dt.Rows[0]["POType"].ToString().Trim() == "F")
            {
                this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\FPO.rdlc";
            }
            else
            {
                this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\APO.rdlc";
            }



        }
    }
}