﻿using ArtWebApp.DataModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.Production.CutOrderReport
{
    public partial class CutOrder : System.Web.UI.Page
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
                drp_Atc.DataTextField = "name";
                drp_Atc.DataValueField = "pk";
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
                drp_ourstyle.DataTextField = "name";
                drp_ourstyle.DataValueField = "pk";
                drp_ourstyle.DataBind();
                Upd_ourstyle.Update();



            }
        }






        public void FillAllcutorder(int atcid)
        {


            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.CutOrderMasters
                        where ponmbr.AtcID == atcid
                        select new
                        {
                            name = ponmbr.Cut_NO,

                            //  name=ponmbr.CostingCount,
                            pk = ponmbr.CutID
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
                            name = ponmbr.Costing_PK.ToString() + "-" + ponmbr.CostingCount.ToString(),

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
        public void fillColorcombo()
        {

            BLL.CutOrderBLL.CutOrderData cdata = new BLL.CutOrderBLL.CutOrderData();
            ddl_color.DataSource = cdata.GetFabricDescription(int.Parse(drp_Atc.SelectedValue.ToString()));

            ddl_color.DataTextField = "ItemDescription";
            ddl_color.DataValueField = "Skudet_pk";
            ddl_color.DataBind();
            UPD_COLOR.Update();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            FillOurstyleCombo(int.Parse(drp_Atc.SelectedValue.ToString()));
            fillColorcombo();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FillAllcutorder(int.Parse(drp_Atc.SelectedValue.ToString()));
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

            loadPOreport();
        }

        public void loadPOreport()
        {

            int costingid = int.Parse(drp_costingpk.SelectedValue.ToString());
            Response.Redirect(String.Format("~/Reports/Production/CutOrderReport/CutorderReport.aspx?cutpk={0}", costingid));

          //  Response.Redirect("", costingid);
            //Response.Redirect(String.Format("~/Reports/View.aspx?navtype={0}&POnum={1}", "PO", ponum));
            //DBTransaction.CutOrderTransaction potrsans = new DBTransaction.CutOrderTransaction();

            //DataTable dt = potrsans.GetCutOrderData(int.Parse(drp_costingpk.SelectedValue.ToString().Trim()));

            //ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            //this.ReportViewer1.LocalReport.DataSources.Clear();
            //this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\cutorder.rdlc";



        }

        protected void Button7_Click(object sender, EventArgs e)
        {


            DBTransaction.CutOrderTransaction potrsans = new DBTransaction.CutOrderTransaction();

            DataTable dt = potrsans.GetCutOrderDataofSKU(int.Parse(ddl_color.SelectedValue.ToString().Trim()), int.Parse(drp_ourstyle.SelectedValue.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\cutordersummary.rdlc";

        }

        protected void Button6_Click(object sender, EventArgs e)
        {

        }
    }
}