using ArtWebApp.DataModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.Production.LaySheet
{
    public partial class laySheetReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            List<decimal?> list = Session["ApprovedLocationlist"] as List<decimal?>;

            FillLaysheetCombo(int.Parse(drp_Atc.SelectedValue.ToString()), list);

            FillLaysheetRollCombo(int.Parse(drp_Atc.SelectedValue.ToString()), list);

            FillExtrarequest(int.Parse(drp_Atc.SelectedValue.ToString()), list);
        }

        public void FillLaysheetCombo(int atcid, List<decimal?> list)
        {

          

           
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.LaySheetMasters
                        where ponmbr.AtcID == atcid && list.Contains(ponmbr.Location_PK )
                        select new
                        {
                            name = ponmbr.LaySheetNum,
                            pk = ponmbr.LaySheet_PK
                        };

                drp_laysheet.DataSource = q.ToList();
                drp_laysheet.DataTextField = "name";
                drp_laysheet.DataValueField = "pk";
                drp_laysheet.DataBind();
                Upd_loc.Update();



            }
        }



        public void FillLaysheetRollCombo(int atcid, List<decimal?> list)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.LaySheetRollMasters
                        where ponmbr.CutOrderMaster.AtcID== atcid && list.Contains(ponmbr.Location_Pk)
                        select new
                        {
                            name = ponmbr.LayRollRef,
                            pk = ponmbr.LaysheetRollmaster_Pk
                        };

                drp_laysheetroll.DataSource = q.ToList();
                drp_laysheetroll.DataTextField = "name";
                drp_laysheetroll.DataValueField = "pk";
                drp_laysheetroll.DataBind();
                Upd_laysheetroll.Update();



            }
        }


        public void FillExtrarequest(int atcid, List<decimal?> list)
        {
         


            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.LayShortageReqMasters
                        where ponmbr.AtcID == atcid 
                        select new
                        {
                            name = ponmbr.LayShortageReqCode,
                            pk = ponmbr.LayShortageMasterID
                        };

                drp_extrarequest.DataSource = q.ToList();
                drp_extrarequest.DataTextField = "name";
                drp_extrarequest.DataValueField = "pk";
                drp_extrarequest.DataBind();
                upd_extra.Update();



            }
        }




        protected void Button1_Click(object sender, EventArgs e)
        {
                 }

        protected void Button6_Click(object sender, EventArgs e)
        {
            string laysheetpk = drp_laysheet.SelectedValue.ToString();
            Response.Redirect(String.Format("~/Reports/Production/LaySheet/laysheetprintable.aspx?laysheetpk={0}", laysheetpk));

        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            string laysheetpk = drp_laysheetroll.SelectedValue.ToString();
            Response.Redirect(String.Format("~/Reports/Production/LaySheet/laysheetRollPrintable.aspx?laysheetpk={0}", laysheetpk));

        }

        protected void Button8_Click(object sender, EventArgs e)
        {
           
            DataTable dt = DBTransaction.LaysheetTransaction.getlaysheetpendingCutorder();
           
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\CutOrderPendingToLay.rdlc";
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            DataTable dt = DBTransaction.LaysheetTransaction.GetExtraRrequest(int.Parse (drp_extrarequest.SelectedValue.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\ExtraFabricrequest.rdlc";
            
        }
    }
}