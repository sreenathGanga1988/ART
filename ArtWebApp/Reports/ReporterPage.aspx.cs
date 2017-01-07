using ArtWebApp.DataModels;
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
    public partial class ReporterPage : System.Web.UI.Page
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
                Upd_atc.Update();



            }
        }



        public void FillOurstyleCombo(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.AtcDetails
                        where ponmbr.AtcId == atcid
                        select new
                        {
                            name = ponmbr.OurStyle,
                            pk = ponmbr.OurStyleID
                        };

                drp_ourstyle.DataSource = q.ToList();
                drp_ourstyle.DataBind();
                Upd_ourstyle.Update();



            }
        }






        public void FillAllApprovedCostingnum( int ourstyleid)
       {


           using (ArtEntitiesnew entty = new ArtEntitiesnew())
           {
               var q = from ponmbr in entty.StyleCostingMasters
                       where ponmbr.OurStyleID == ourstyleid && ponmbr.IsApproved=="A"
                       select new
                       {
                        name = ponmbr.Costing_PK.ToString () +"-"+ ponmbr.CostingCount.ToString (),

                         //  name=ponmbr.CostingCount,
                           pk = ponmbr.Costing_PK
                       };

              
               drp_costingpk.DataSource = q.ToList();
               drp_costingpk.DataBind();
               Upd_costing.Update();



           }

       }


        public void FillAllSubmittedCostingnum(int ourstyleid)
        {


            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.StyleCostingMasters
                        where ponmbr.OurStyleID == ourstyleid && ponmbr.IsSubmitted == "Y"
                        select new
                        {
                              name = ponmbr.Costing_PK.ToString () +"-"+ ponmbr.CostingCount.ToString (),

                          //  name = ponmbr.CostingCount,
                            pk = ponmbr.Costing_PK
                        };


                drp_costingpk.DataSource = q.ToList();
                drp_costingpk.DataBind();
                Upd_costing.Update();



            }

        }
        public void FillAllCostingnum(int ourstyleid)
        {


            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.StyleCostingMasters
                        where ponmbr.OurStyleID == ourstyleid 
                        select new
                        {
                            name = ponmbr.Costing_PK.ToString() + "-" + ponmbr.CostingCount.ToString(),
                            pk = ponmbr.Costing_PK
                        };


                drp_costingpk.DataSource = q.ToList();
                drp_costingpk.DataBind();
                Upd_costing.Update();



            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            FillOurstyleCombo(int.Parse(drp_Atc.SelectedValue.ToString()));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FillAllApprovedCostingnum(int.Parse(drp_ourstyle.SelectedValue.ToString()));
        }

        protected void ShowAll_Click(object sender, EventArgs e)
        {
            FillAllCostingnum(int.Parse(drp_ourstyle.SelectedValue.ToString()));
        }

        protected void btn_showAllSubmitted_Click(object sender, EventArgs e)
        {
            FillAllSubmittedCostingnum(int.Parse(drp_ourstyle.SelectedValue.ToString()));
        }

        protected void Button5_Click(object sender, EventArgs e)
        {

            int costingid= int.Parse (drp_costingpk.SelectedValue.ToString() );
            Session["printcostpk"] = costingid;

            Response.Redirect("~/Reports/IndividualCostingPrint.aspx");
  //          Page.ClientScript.RegisterStartupScript(
  //this.GetType(), "OpenWindow", "window.open('/Reports/IndividualCostingPrint.aspx','_newtab');", true);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DBTransaction.ReportTransactions.AccountReportrans acctrn = new DBTransaction.ReportTransactions.AccountReportrans();

            DataTable dt = acctrn.GetCostingData(int.Parse(drp_Atc.SelectedValue.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\costingreport.rdlc";
        }

        // ?costingid=" + costingpk.ToString()
    }
}