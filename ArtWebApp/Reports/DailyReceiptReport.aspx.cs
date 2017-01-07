using ArtWebApp.DataModels;
using ArtWebApp.Reports.Dataset.ReportDataSetTableAdapters;
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
    public partial class DailyReceiptReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                filltowarehouses();
            }
        }


        public void filltowarehouses()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.LocationMasters
                        where order.LocType == "W"
                        select new
                        {
                            name = order.LocationName,
                            pk = order.Location_PK
                        };
              
                // Create a table from the query.
                drp_ToWarehouse.DataSource = q.ToList();
                drp_ToWarehouse.DataBind();

              

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            loadMRNreport();
        }

        public void loadMRNreport()
        {

          
            //  adapt.Connection.ConnectionString = Program.ConnStr;


            DBTransaction.InventoryTransaction.InventoryTransaction invtran = new DBTransaction.InventoryTransaction.InventoryTransaction();

            DataTable dt1 = invtran.GetReceipt();
            DataTable dt2 = invtran.GetDeliveryReceipt();


           
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt1);



            ReportDataSource datasource2 = new ReportDataSource("DataSet2", dt2);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DataSources.Add(datasource2);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\DailyReport.rdlc";

        }
    }
}