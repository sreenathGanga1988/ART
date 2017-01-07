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

namespace ArtWebApp.Reports.Inventoryreport
{
    public partial class MRNReport : System.Web.UI.Page
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

        public void FillPOCombo(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.ProcurementMasters
                        where ponmbr.AtcId == atcid
                        select new
                        {
                            name = ponmbr.PONum,
                            pk = ponmbr.PO_Pk
                        };

                drp_PO.DataSource = q.ToList();
                drp_PO.DataTextField = "name";
                drp_PO.DataValueField = "pk";
                drp_PO.DataBind();




            }
        }


        public void FillRcptCombo(int poid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from mrnmstr in entty.MrnMasters
                        join rcptmstr in entty.RecieptMasters
                        on mrnmstr.Reciept_Pk equals rcptmstr.Reciept_Pk
                        where mrnmstr.Po_PK == poid
                        select new
                        {
                            name = rcptmstr.RecieptNum,
                            pk = rcptmstr.Reciept_Pk
                        };





                drp_rcpt.DataSource = q.ToList();
                drp_rcpt.DataTextField = "name";
                drp_rcpt.DataValueField = "pk";
                drp_rcpt.DataBind();




            }
        }

        public void FillMRNCombo(int rcptpk)
        {

            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {

                var q = from mrnmstr in entty.MrnMasters
                        where mrnmstr.Reciept_Pk == rcptpk
                        select new
                        {
                            name = mrnmstr.MrnNum,
                            pk = mrnmstr.Mrn_PK
                        };


                drp_mrn.DataSource = q.ToList();
                drp_mrn.DataTextField = "name";
                drp_mrn.DataValueField = "pk";
                drp_mrn.DataBind();




            }
        }

        public void fillMRNofAtc(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {



                var q = from mrnmstr in entty.MrnMasters
                        join pomstr in entty.ProcurementMasters
                            on mrnmstr.Po_PK equals pomstr.PO_Pk

                        where pomstr.AtcId == atcid
                        select new
                        {
                            name = mrnmstr.MrnNum,
                            pk = mrnmstr.Mrn_PK
                        };


                drp_mrn.DataSource = q.ToList();
                drp_mrn.DataTextField = "name";
                drp_mrn.DataValueField = "pk";
                drp_mrn.DataBind();


            }
        }







        protected void Btn_mrnshow_Click(object sender, EventArgs e)
        {
            try
            {
                loadMRNreport();
            }
            catch (Exception exp)
            {

                Response.Write(exp.ToString());
            }
        }







        public void loadMRNreport()
        {
            DBTransaction.InventoryTransaction.RecieptReportTransaction invtran = new DBTransaction.InventoryTransaction.RecieptReportTransaction();
            DataTable dt = invtran.GetMRNData(int.Parse (drp_mrn.SelectedValue.ToString ()));

            //RecieptDataTableAdapter adapt = new RecieptDataTableAdapter();
            //  adapt.Connection.ConnectionString = Program.ConnStr;
         //   DataTable dt = adapt.GetDataByMRN(drp_mrn.SelectedItem.Text.Trim());
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\MRN.rdlc";

        }


        public void loadRCPTreport()
        {

            RecieptDataTableAdapter adapt = new RecieptDataTableAdapter();
            //  adapt.Connection.ConnectionString = Program.ConnStr;
            DataTable dt = adapt.GetDataByRecieptnum(drp_rcpt.SelectedItem.Text.Trim());
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\MRN.rdlc";
        }

        protected void Btn_showallmrn_Click(object sender, EventArgs e)
        {
            if (drp_rcpt.SelectedItem.Value != null)
            {
                try
                {
                    FillMRNCombo(int.Parse(drp_rcpt.SelectedValue.ToString().Trim()));
                }
                catch (Exception)
                {


                }
            }
        }

        protected void btn_showPO_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_Atc.SelectedItem.Value != null)
                {
                    FillPOCombo(int.Parse(drp_Atc.SelectedValue.ToString()));
                }
            }
            catch (Exception exp)
            {

                Response.Write(exp.ToString());
            }
        }

        protected void Btn_showRcptReport_Click(object sender, EventArgs e)
        {
             try
            {
                if (drp_rcpt.SelectedItem.Value != null)
                {
                    loadRCPTreport();
                }
            }
            catch (Exception exp)
            {

                Response.Write(exp.ToString());
            }

            
        }

        protected void Btn_mrnshowreport_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_mrn.SelectedItem.Value != null)
                {
                    loadMRNreport();
                }
            }
            catch (Exception exp)
            {

                Response.Write(exp.ToString());
            }
        }

        protected void Btn_showReceipt_Click(object sender, EventArgs e)
        {
            if (drp_PO.SelectedItem.Value != null)
            {
                FillRcptCombo(int.Parse(drp_PO.SelectedValue.ToString().Trim()));
            }
        }

        protected void btn_showAtcMRN_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_Atc.SelectedItem.Value != null)
                {
                    fillMRNofAtc(int.Parse(drp_Atc.SelectedValue.ToString()));
                }
            }
            catch (Exception exp)
            {

                Response.Write(exp.ToString());
            }
        }

        protected void btn_showPhysical_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_Atc.SelectedItem.Value != null)
                {
                    loadPOPHysicalreport();
                }
            }
            catch (Exception exp)
            {


            }
        }




        public void loadPOPHysicalreport()
        {



            DBTransaction.ProcurementTransaction potrsans = new DBTransaction.ProcurementTransaction();

            DataTable dt = potrsans.GetPOData(drp_PO.SelectedItem.Text.Trim());

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
           
                this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\PHYSICAL PO.rdlc";
            

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
          
        }

      
    
    
    }
}