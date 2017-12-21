﻿using ArtWebApp.DataModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports
{
    public partial class AtcCrystalReporter : System.Web.UI.Page
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
            List<decimal?> list = Session["ApprovedLocationlist"] as List<decimal?>;
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

                var q1 = from order in entty.LocationMasters
                         where order.LocType == "F" && list.Contains(order.Location_PK)
                         select new
                         {
                             name = order.LocationName,
                             pk = order.Location_PK
                         };
                drp_fact.DataSource = q1.ToList();
                drp_fact.DataBind();
                drp_fact.Items.Insert(0, new ListItem("Select All", "0"));
                UPD_FACT.Update();


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


        public void FillShrinkageofSkudetpk(int ourstyleid,int skudetpk)
        {

            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = (from ponmbr in entty.CutPlanMasters
                        where ponmbr.OurStyleID == ourstyleid && ponmbr.SkuDet_PK== skudetpk
                        select new { name= ponmbr.ShrinkageGroup} ).Distinct();

                drp_shrinkage.DataSource = q.ToList();
                drp_shrinkage.DataTextField = "name";
                drp_shrinkage.DataValueField = "name";
                drp_shrinkage.DataBind();
                upd_shrinkage.Update();



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
        public void FillAllcutPlan(int atcid)
        {


            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.CutPlanMasters
                        where ponmbr.AtcDetail.AtcId == atcid  && ponmbr.IsDeleted=="N"
                        select new
                        {
                            name = ponmbr.CutPlanNUM,

                            //  name=ponmbr.CostingCount,
                            pk = ponmbr.CutPlan_PK
                        };


                drp_cutplan.DataSource = q.ToList();
                drp_cutplan.DataBind();
                upd_cutplan.Update();



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



            DBTransaction.CutOrderTransaction potrsans = new DBTransaction.CutOrderTransaction();

            DataTable dt = potrsans.GetCutOrderData(int.Parse ( drp_costingpk.SelectedValue.ToString ().Trim()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
              this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\cutorder.rdlc";
           
              

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

        protected void Button8_Click(object sender, EventArgs e)
        {
            int costingid = int.Parse(drp_costingpk.SelectedValue.ToString());
            Response.Redirect(String.Format("~/Reports/Production/CutOrderReport/CutorderReport.aspx?cutpk={0}", costingid));



        }

        protected void btn_showApprovedcutplan_Click(object sender, EventArgs e)
        {
            FillAllcutPlan(int.Parse(drp_Atc.SelectedValue.ToString()));
        }

        protected void btn_showCutplam_Click(object sender, EventArgs e)
        {
            Session["cutpkrpt"] = int.Parse(drp_cutplan.SelectedValue.ToString());
            Response.Redirect("~/Reports/Production/CutPlanHtmlReport.aspx");
        }

        protected void btn_showApprovedcutplan0_Click(object sender, EventArgs e)
        {
            int atcid = int.Parse(drp_Atc.SelectedValue.ToString());
            Response.Redirect(String.Format("~/Production/Cutting/CutplanAndRollofAtc.aspx?atcid={0}", atcid));
           

        }

        protected void btn_showCutplanRoll_Click(object sender, EventArgs e)
        {
            Session["cutpkrpt"] = int.Parse(drp_cutplan.SelectedValue.ToString());
            Response.Redirect("~/Reports/Production/CutPlanHtmlReportWithRoll.aspx");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            loadDOPOreport();
        }




        public void loadDOPOreport()
        {



            DBTransaction.CutOrderTransaction potrsans = new DBTransaction.CutOrderTransaction();

            DataTable dt = potrsans.GetCutOrderDO(int.Parse(drp_costingpk.SelectedValue.ToString().Trim()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\cutorderDO.rdlc";



        }

        protected void btn_showShrinkage_Click(object sender, EventArgs e)
        {
            FillShrinkageofSkudetpk(int.Parse(drp_ourstyle.SelectedValue.ToString()), int.Parse(ddl_color.SelectedValue.ToString()));
        }

        protected void btn_showshrinkagecritical_Click(object sender, EventArgs e)
        {

            DBTransaction.CutOrderTransaction potrsans = new DBTransaction.CutOrderTransaction();
            DataTable dt = new DataTable();
            if (drp_fact.SelectedItem.Text == "Select All")
            {
                dt = potrsans.GetCriticalPath(int.Parse(drp_ourstyle.SelectedValue.ToString().Trim()), int.Parse(ddl_color.SelectedValue.ToString().Trim()), drp_shrinkage.SelectedValue.ToString(),0);
            }
            else
            {
                dt = potrsans.GetCriticalPath(int.Parse(drp_ourstyle.SelectedValue.ToString().Trim()), int.Parse(ddl_color.SelectedValue.ToString().Trim()), drp_shrinkage.SelectedValue.ToString(),int.Parse(drp_ourstyle.SelectedValue.ToString().Trim()));
            }

            String Reportheading = "Critical Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy") +"for "+drp_ourstyle.SelectedItem.Text  + " " +ddl_color.SelectedItem.Text +"  Shrinkage :"+ drp_shrinkage.SelectedItem.Text;
            ReportParameter rp1 = new ReportParameter("Heading", Reportheading);
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\CriticalReport.rdlc";
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
        }

        protected void btn_showshrinkagereportofstyle_Click(object sender, EventArgs e)
        {
            DBTransaction.CutOrderTransaction potrsans = new DBTransaction.CutOrderTransaction();
            DataTable dt = new DataTable();
            if (drp_fact.SelectedItem.Text=="Select All")
            {
                dt = potrsans.GetCriticalPath(int.Parse(drp_ourstyle.SelectedValue.ToString().Trim()), int.Parse(ddl_color.SelectedValue.ToString().Trim()), "NA",0);
            }
            else
            {
                dt = potrsans.GetCriticalPath(int.Parse(drp_ourstyle.SelectedValue.ToString().Trim()), int.Parse(ddl_color.SelectedValue.ToString().Trim()), "NA",int.Parse (drp_fact.SelectedValue.ToString()));
            }

    
            String Reportheading = "Critical Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  for " + drp_ourstyle.SelectedItem.Text + " " + ddl_color.SelectedItem.Text;

            ReportParameter rp1 = new ReportParameter("Heading", Reportheading);
           

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\CriticalReport.rdlc";
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
        }

        protected void btn_showshrinkagereportofAtc_Click(object sender, EventArgs e)
        {

            DBTransaction.CutOrderTransaction potrsans = new DBTransaction.CutOrderTransaction();
            DataTable dt = new DataTable();
            if (drp_fact.SelectedItem.Text == "Select All")
            {
                dt = potrsans.GetCriticalPath(int.Parse(drp_Atc.SelectedValue.ToString().Trim()), 0);
            }
            else
            {
                dt = potrsans.GetCriticalPath(int.Parse(drp_Atc.SelectedValue.ToString().Trim()), int.Parse(drp_fact.SelectedValue.ToString()));
            }


            String Reportheading = "Critical Report  As  of " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  for " + drp_Atc.SelectedItem.Text + " in " + drp_fact.SelectedItem.Text;

            ReportParameter rp1 = new ReportParameter("Heading", Reportheading);
            

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\CriticalReport.rdlc";
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
        }

        protected void Button10_Click(object sender, EventArgs e)
        {

            int atcid = int.Parse(drp_Atc.SelectedValue.ToString());
            UrlHelper urlHelp = new UrlHelper(HttpContext.Current.Request.RequestContext);

            Response.Redirect(urlHelp.Action("Index", "AtcPerformance", new { area = "CuttingMVC", id = atcid }));
        
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            int ourStyleid = 0;
            int locationpk = 0;
            int skudetpk = int.Parse(ddl_color.SelectedValue.ToString());

            locationpk = int.Parse(drp_fact.SelectedValue.ToString());
             ourStyleid = int.Parse(drp_ourstyle.SelectedValue.ToString());
            UrlHelper urlHelp = new UrlHelper(HttpContext.Current.Request.RequestContext);
            Response.Redirect(urlHelp.Action( "Index", "FCR",new { area = "CuttingMVC", id = skudetpk,  ourStyleid= ourStyleid, locationpk= locationpk }));
        }
    }
}