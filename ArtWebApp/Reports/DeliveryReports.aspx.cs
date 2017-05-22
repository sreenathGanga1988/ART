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
    public partial class DeliveryReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAtcCombo();
            }
        }
        public void FillAtcCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
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


        public void FillDOCombo(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from dlvordr in entty.DeliveryOrderMasters
                        where dlvordr.AtcID==atcid
                        select new
                        {
                            name = dlvordr.DONum,
                            pk = dlvordr.DO_PK
                        };

                // Create a table from the query.
                drp_do.DataSource = q.ToList();
                drp_do.DataBind();




            }
        }



        public void FillDORCombo(int doid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from dlvordr in entty.DeliveryReceiptMasters
                        where dlvordr.DO_PK == doid
                        select new
                        {
                            name = dlvordr.DORNum,
                            pk = dlvordr.DOR_PK
                        };

                // Create a table from the query.
                drp_rcpt.DataSource = q.ToList();
                drp_rcpt.DataBind();

            }
        }

        protected void Btn_show_Click(object sender, EventArgs e)
        {
            try
            {
                FillDOCombo(int.Parse(drp_Atc.SelectedValue.ToString()));
            }
            catch (Exception)
            {


            }
        }

        protected void Btn_showDOR_Click(object sender, EventArgs e)
        {
            try
            {
                FillDORCombo(int.Parse(drp_do.SelectedValue.ToString()));
            }
            catch (Exception)
            {


            }
        }

        protected void Btn_showdDO_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_do.SelectedItem.Value != null)
                {
                    loadDOReport();
                }
            }
            catch (Exception exp)
            {

                Response.Write(exp.ToString());
            }
        }

        public void loadDOReport()
        {
            DBTransaction.ReportTransactions.ReporterTrans rpttran = new DBTransaction.ReportTransactions.ReporterTrans();
            DataTable dt = rpttran.GetDOData(int.Parse(drp_do.SelectedItem.Value.Trim()));
            //DeliveryDataTableAdapter adapt = new DeliveryDataTableAdapter();
            ////  adapt.Connection.ConnectionString = Program.ConnStr;
            //DataTable dt = adapt.GetDataby(int.Parse( drp_do.SelectedItem.Value.Trim()));
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\DO.rdlc";
        }

        public void loadDORReport()
        {
            DeliveryDataTableAdapter adapt = new DeliveryDataTableAdapter();
            //  adapt.Connection.ConnectionString = Program.ConnStr;
            DataTable dt = adapt.GetDataby(int.Parse(drp_do.SelectedItem.Value.Trim()));
           
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\DOR.rdlc";
        }

        protected void Btn_showDORrpt_Click(object sender, EventArgs e)
        {
            DBTransaction.ReportTransactions.ReporterTrans rpttran = new DBTransaction.ReportTransactions.ReporterTrans();

            DataTable dt = rpttran.GetDORData(int.Parse(drp_rcpt.SelectedItem.Value.Trim()));
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\DOR.rdlc";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DBTransaction.ReportTransactions.ReporterTrans rpttran = new DBTransaction.ReportTransactions.ReporterTrans();
            DataTable dt = rpttran.GetDOData(int.Parse(drp_do.SelectedItem.Value.Trim()));
            //DeliveryDataTableAdapter adapt = new DeliveryDataTableAdapter();
            ////  adapt.Connection.ConnectionString = Program.ConnStr;
            //DataTable dt = adapt.GetDataby(int.Parse( drp_do.SelectedItem.Value.Trim()));
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\DO Physical.rdlc";
        }

        protected void BTN_DOROLL_Click(object sender, EventArgs e)
        {
            loadDoWithRollReport();
        }




        public void loadDoWithRollReport()
        {


            DBTransaction.ReportTransactions.ReporterTrans rpttran = new DBTransaction.ReportTransactions.ReporterTrans();
            DataTable dt = rpttran.GetDORollDataofSelectedRoll(int.Parse(drp_do.SelectedItem.Value.Trim()));
            //DeliveryDataTableAdapter adapt = new DeliveryDataTableAdapter();
            ////  adapt.Connection.ConnectionString = Program.ConnStr;
            //DataTable dt = adapt.GetDataby(int.Parse( drp_do.SelectedItem.Value.Trim()));
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\DeliveryOrderRoll.rdlc";







        }
    }
}