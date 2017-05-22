using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
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
            FillLaysheetCombo(int.Parse(drp_Atc.SelectedValue.ToString()));

            FillLaysheetRollCombo();
        }

        public void FillLaysheetCombo(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.LaySheetMasters
                        where ponmbr.AtcID == atcid
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



        public void FillLaysheetRollCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.LaySheetRollMasters
                     
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
    }
}