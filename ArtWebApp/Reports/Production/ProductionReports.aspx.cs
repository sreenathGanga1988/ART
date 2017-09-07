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

namespace ArtWebApp.Reports.Production
{
    public partial class ProductionReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAtcCombo();
                FillShipmentHandover();
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
                drp_Atc.DataTextField = "name";
                drp_Atc.DataValueField = "pk";
                drp_Atc.DataBind();




            }
        }

        public void FillJobContractCombo(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                entty.Configuration.AutoDetectChangesEnabled = false;
                var q = from jc in entty.JobContractMasters
                        where jc.AtcID== atcid
                        select new
                        {
                            name = jc.JOBContractNUM,
                            pk = jc.JobContract_pk
                        };

                // Create a table from the query.
                drp_jc.DataSource = q.ToList();
                drp_jc.DataTextField = "name";
                drp_jc.DataValueField = "pk";
                drp_jc.DataBind();

                var q1 = from jc in entty.JobContractOptionalMasters
                        where jc.AtcID == atcid
                        select new
                        {
                            name = jc.JobContractOptionalNUM,
                            pk = jc.JobContractOptional_pk
                        };

                drp_jcothers.DataSource = q1.ToList();
                drp_jcothers.DataTextField = "name";
                drp_jcothers.DataValueField = "pk";
                drp_jcothers.DataBind();

            }
        }
        public void FillShipmentHandover()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                entty.Configuration.AutoDetectChangesEnabled = false;
                var q = from atcorder in entty.ShipmentHandOverMasters
                        
                        select new
                        {
                            name = atcorder.ShipmentHandOverCode,
                            pk = atcorder.ShipmentHandMaster_PK
                        };

                // Create a table from the query.
                drp_shipmentHandover.DataSource = q.ToList();
                drp_shipmentHandover.DataTextField = "name";
                drp_shipmentHandover.DataValueField = "pk";
                drp_shipmentHandover.DataBind();




            }
        }





        public void FillShipmentHandover(int atcid)
        {
            

                  DBTransaction.ReportTransactions.ProductionReportsTrans prdtran = new DBTransaction.ReportTransactions.ProductionReportsTrans();
            //  adapt.Connection.ConnectionString = Program.ConnStr;
            DataTable dt = prdtran.GetShipmentHandoverofAtc(int.Parse(drp_Atc.SelectedValue.ToString()));

          
                // Create a table from the query.
                drp_shipmentHandover.DataSource = dt;
                drp_shipmentHandover.DataTextField = "ShipmentHandOverCode";
                drp_shipmentHandover.DataValueField = "ShipmentHandMaster_PK";
                drp_shipmentHandover.DataBind();




            
        }




        protected void btn_atc_Click(object sender, EventArgs e)
        {
            loadProductionreport();
        }

        protected void Btn_showshipmentHandover_Click(object sender, EventArgs e)
        {
            loadShipmentreportreport();
        }

        


        public void loadShipmentreportreport()
        {

            DBTransaction.ReportTransactions.ProductionReportsTrans prdtran = new DBTransaction.ReportTransactions.ProductionReportsTrans();
            //  adapt.Connection.ConnectionString = Program.ConnStr;
            DataTable dt = prdtran.GetShipmentHandoverData(int.Parse( drp_shipmentHandover.SelectedValue.ToString ()));
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\Shipment Handover.rdlc";
        }



        public void loadProductionreport()
        {

            DBTransaction.ReportTransactions.ProductionReportsTrans prdtran = new DBTransaction.ReportTransactions.ProductionReportsTrans();
            //  adapt.Connection.ConnectionString = Program.ConnStr;
            DataTable dt = prdtran.GetProductionReportofAtc(int.Parse(drp_Atc.SelectedValue.ToString()));
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\ProductionReport.rdlc";
        }

        protected void btn_jc_Click(object sender, EventArgs e)
        {
            FillJobContractCombo(int.Parse(drp_Atc.SelectedValue.ToString()));
        }



        public void loadJCCMreport()
        {

            DBTransaction.ReportTransactions.ProductionReportsTrans prdtran = new DBTransaction.ReportTransactions.ProductionReportsTrans();
            //  adapt.Connection.ConnectionString = Program.ConnStr;
            DataTable dt = prdtran.GetJOBContractCM(int.Parse(drp_jc.SelectedValue.ToString()));
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\jc-cm.rdlc";
        }

        public void loadJCOthersreport()
        {

            DBTransaction.ReportTransactions.ProductionReportsTrans prdtran = new DBTransaction.ReportTransactions.ProductionReportsTrans();
            //  adapt.Connection.ConnectionString = Program.ConnStr;
            DataTable dt = prdtran.GetJOBContractOthers(int.Parse(drp_jcothers.SelectedValue.ToString()));
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\JC-OTHERNew.rdlc";
        }

        protected void btn_showjc_Click(object sender, EventArgs e)
        {
            loadJCCMreport();
        }

        protected void btn_jcothers_Click(object sender, EventArgs e)
        {
            loadJCOthersreport();
        }

        protected void btn_jc0_Click(object sender, EventArgs e)
        {
            FillShipmentHandover(int.Parse(drp_Atc.SelectedValue.ToString()));
        }
    }
}