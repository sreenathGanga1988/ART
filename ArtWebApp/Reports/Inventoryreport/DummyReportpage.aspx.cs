using ArtWebApp.Areas.Inventory;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.Inventoryreport
{
    public partial class DummyReportpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           

            DataTable dt = GetGstockData(0);

            this.ReportViewer1.LocalReport.DataSources.Clear();
          
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\GstockWithTransferdata.rdlc";
        }


        public DataTable GetGstockData(int locationpk)
        {
            DataTable dt = new DataTable();


            using (SqlCommand cmd = new SqlCommand(@"GstockWithTransferData_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location_Pk", locationpk);

                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }



            return dt;
        }
    }
}