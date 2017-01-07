using ArtWebApp.DataModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.MerchandiserReport
{
    public partial class PurchaseReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                FillMRNCombo();
                FillAtcCombo();
            }
        }
        public void FillAtcCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                entty.Configuration.AutoDetectChangesEnabled = false;
                var q = from atcorder in entty.AtcMasters
                        select new
                        {
                            name = atcorder.AtcNum,
                            pk = atcorder.AtcId
                        };

                // Create a table from the query.
                drp_Atc.DataSource = q.ToList();
                drp_Atc.DataBind();




            }
        }

        public void FillMRNCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
             
                var q1 = from order in entty.MrnMasters
                        
                         select new
                         {
                             name = order.MrnNum,
                             pk = order.Mrn_PK
                         };
               

                ddl_Frommrn.DataSource = q1.ToList();
                ddl_Frommrn.DataTextField = "name";
                ddl_Frommrn.DataValueField = "pk";
                ddl_Frommrn.DataBind();

                ddl_tomrn.DataSource = q1.ToList();
                ddl_tomrn.DataTextField = "name";
                ddl_tomrn.DataValueField = "pk";
                ddl_tomrn.DataBind();
               

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String Reportheading = "Purchase Report  Between  " + ddl_Frommrn.SelectedItem.Text + " And  " + ddl_tomrn.SelectedItem.Text;
            DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();

            DataTable dt = invtran.GetpURCHASEREPORT(int.Parse(ddl_Frommrn.SelectedValue.ToString()), int.Parse(ddl_tomrn.SelectedValue.ToString()));

            
            showInventoryReport(dt, Reportheading);
            
        }


        public void showInventoryReport(DataTable dt, String Msg)
        {
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportParameter rp1 = new ReportParameter("Heading", Msg);

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\PurchaseReport.rdlc";
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
        }
        public ArrayList getAtclist()
        {
            ArrayList atcArraylist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_Atc.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int atcid = int.Parse(item.Value.ToString());
                atcArraylist.Add(atcid);
            }
            return atcArraylist;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ArrayList atcArraylist = getAtclist();
            if (atcArraylist.Count > 0 && atcArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();

                DataTable dt = invtran.GetPurChaseReportOfATC(atcArraylist);

                String Reportheading = "Purchase Report  ";
              


                showInventoryReport(dt, Reportheading);


              
            }
        }
    }
}