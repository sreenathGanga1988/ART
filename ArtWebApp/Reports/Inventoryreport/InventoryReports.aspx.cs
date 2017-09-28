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

namespace ArtWebApp.Reports.Inventoryreport
{
    public partial class InventoryReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if( !IsPostBack)
        {
            filltowarehouses();
            FillAtcCombo();
        }

        }


        public void filltowarehouses()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.LocationMasters
                        where order.LocType == "W" || order.LocType == "F" 
                        select new
                        {
                            name = order.LocationName,
                            pk = order.Location_PK
                        };
                   drp_ToWarehouse.DataSource = q.ToList();
                drp_ToWarehouse.DataBind();
            
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








        protected void btn_showAtcMRN_Click(object sender, EventArgs e)
        {
            loadINVreport();
        }


        public void loadINVreport()
        {
            String Reportheading = "Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");


            DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();

            DataTable dt = invtran.GetInventoryofAllLocation();

            //ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            //this.ReportViewer1.LocalReport.DataSources.Clear();
            //this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            //this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\Inventory.rdlc";

            showInventoryReport(dt, Reportheading);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String Reportheading = "Trim Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
              DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();

              DataTable dt = invtran.GetTrimInventoryofLocation();

            //ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            //this.ReportViewer1.LocalReport.DataSources.Clear();
            //this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            //this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\Inventory.rdlc";
              showInventoryReport(dt, Reportheading);
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            String Reportheading = "Fab Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();

            DataTable dt = invtran.GetFabricInventoryofLocation();
            showInventoryReport(dt, Reportheading);
            //ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            //this.ReportViewer1.LocalReport.DataSources.Clear();
            //this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            //this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\Inventory.rdlc";
        }

        protected void btn_showPO_Click(object sender, EventArgs e)
        {

            ArrayList atcArraylist = getAtclist();

            String Reportheading = "Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");

            if (atcArraylist.Count > 0 && atcArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
                DataTable dt = invtran.GetInventoryofAtcList(atcArraylist);
              
                showInventoryReport(dt,Reportheading);
            }
        }

        protected void btn_showlocationInventory_Click(object sender, EventArgs e)
        {
            ArrayList LocArraylist = getLoclist();

            String Reportheading = "Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            if (LocArraylist.Count > 0 && LocArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
                DataTable dt = invtran.GetInventoryofLocList(LocArraylist);

                showInventoryReport(dt, Reportheading);
            }
        }

        protected void btn_showAtcLocInventory_Click(object sender, EventArgs e)
        {

            ArrayList atcArraylist = getAtclist();


            ArrayList LocArraylist = getLoclist();

            String Reportheading = " Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            if (LocArraylist.Count > 0 && LocArraylist != null && atcArraylist.Count > 0 && atcArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
                DataTable dt = invtran.GetInventoryofLocListAndAtcAndType(atcArraylist, LocArraylist,"All");

                showInventoryReport(dt, Reportheading);

            }




        }

        protected void btn_trimatc_Click(object sender, EventArgs e)
        {
            ArrayList atcArraylist = getAtclist();
            String Reportheading = " Trim Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            if (atcArraylist.Count > 0 && atcArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
                DataTable dt = invtran.GetTrimInventoryofAtcList(atcArraylist);
                showInventoryReport(dt, Reportheading);

            }
        }

        protected void btn_fabatc_Click(object sender, EventArgs e)
        {
            String Reportheading = " Fabric Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            ArrayList atcArraylist = getAtclist();

            if (atcArraylist.Count > 0 && atcArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
                DataTable dt = invtran.GetFabricInventoryofAtcList(atcArraylist);

                showInventoryReport(dt, Reportheading);
            }
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
    
       public ArrayList getLoclist()
        {
           
            ArrayList LocArraylist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> locitems = drp_ToWarehouse.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in locitems)
            {

                int atcid = int.Parse(item.Value.ToString());
                LocArraylist.Add(atcid);
            }

            return LocArraylist;
        }
    
    
   
    public void showInventoryReport(DataTable dt, String Msg)
    {
        ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
        this.ReportViewer1.LocalReport.DataSources.Clear();
        this.ReportViewer1.LocalReport.DataSources.Add(datasource);
        ReportParameter rp1 = new ReportParameter("Heading", Msg);
        
        this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\Inventory.rdlc";
        ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
    }
     
        protected void Button3_Click(object sender, EventArgs e)
    {
        }

    protected void btn_showlocTrims_Click(object sender, EventArgs e)
    {
        ArrayList LocArraylist = getLoclist();


        if (LocArraylist.Count > 0 && LocArraylist != null)
        {
            String Reportheading = " Trim Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
            DataTable dt = invtran.GetTRIMInventoryofLocList(LocArraylist);

            showInventoryReport(dt,Reportheading );
        }
    }

    protected void btn_showLocFabric_Click(object sender, EventArgs e)
    {
        ArrayList LocArraylist = getLoclist();


        if (LocArraylist.Count > 0 && LocArraylist != null)
        {
            String Reportheading = " Fabric Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
            DataTable dt = invtran.GetFABInventoryofLocList(LocArraylist);

            showInventoryReport(dt, Reportheading);
        }
    }

        protected void Button3_Click1(object sender, EventArgs e)
        {
            DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
            DataTable dt = invtran.GetGoodsintransist(0);
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
       

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\Goodsintransist.rdlc";

        }

        protected void btn_showgstock_Click(object sender, EventArgs e)
        {

            ArrayList LocArraylist = getLoclist();


            if (LocArraylist.Count > 0 && LocArraylist != null)
            {

                DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
                DataTable dt = invtran.GetStockInventoryofLocList(LocArraylist);
                ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(datasource);


                this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\GStock.rdlc";
            }

              
        }

        protected void btn_showAtcLocInventory0_Click(object sender, EventArgs e)
        {
            ArrayList atcArraylist = getAtclist();


            ArrayList LocArraylist = getLoclist();

            String Reportheading = " Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            if (LocArraylist.Count > 0 && LocArraylist != null && atcArraylist.Count > 0 && atcArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
                DataTable dt = invtran.GetInventoryofLocListAndAtcAndType(atcArraylist, LocArraylist, "Trim");

                showInventoryReport(dt, Reportheading);

            }

        }

        protected void btn_showAtcLocInventory1_Click(object sender, EventArgs e)
        {
            ArrayList atcArraylist = getAtclist();


            ArrayList LocArraylist = getLoclist();

            String Reportheading = " Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            if (LocArraylist.Count > 0 && LocArraylist != null && atcArraylist.Count > 0 && atcArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
                DataTable dt = invtran.GetInventoryofLocListAndAtcAndType(atcArraylist, LocArraylist, "Fabric");

                showInventoryReport(dt, Reportheading);

            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
            DataTable dt = invtran.GetGoodsintransist(int.Parse (drp_ToWarehouse.SelectedItems[0].Value.ToString ()));
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);


            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\GoodsintransistLinear.rdlc";
        }

        protected void Button5_Click(object sender, EventArgs e)
        {

            DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
            DataTable dt = invtran.GetGoodsintransist(0);
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);


            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\GoodsintransistLinear.rdlc";
        }

        protected void btn_atcgdtrnst_Click(object sender, EventArgs e)
        {
            ArrayList atcArraylist = getAtclist();

           

            if (atcArraylist.Count > 0 && atcArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
                DataTable dt = invtran.GetGoodsIntransistofAtcList(atcArraylist);

                ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(datasource);


                this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\GoodsintransistLinear.rdlc";
            }
        }

        protected void btn_atctrimwithZeroBalance_Click(object sender, EventArgs e)
        {
            ArrayList atcArraylist = getAtclist();
            String Reportheading = " Trim Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            if (atcArraylist.Count > 0 && atcArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
                DataTable dt = invtran.GetTrimInventoryofAtcListWithOnhandZeroAlso(atcArraylist);
                showInventoryReport(dt, Reportheading);

            }
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            String Reportheading = " Fabric Inventory Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy");
            ArrayList atcArraylist = getAtclist();

            if (atcArraylist.Count > 0 && atcArraylist != null)
            {
                DBTransaction.InventoryTransaction.InventoryReportTransaction invtran = new DBTransaction.InventoryTransaction.InventoryReportTransaction();
                DataTable dt = invtran.GetFabricInventoryofAtcListWithOnhandZeroAlso(atcArraylist);

                showInventoryReport(dt, Reportheading);
            }
        }
    }
}