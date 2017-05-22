using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser
{
    public partial class AtcClosing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void buttonAtc_Click(object sender, EventArgs e)
        {
            fillcontrol();
        }
        public void fillcontrol()
        {
            



            //showAllPoPackATC();
        }
        protected void cmb_atc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }















        protected void tbl_podata_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void validatepotype()
        {
            int k = 0;
            foreach (GridViewRow row in tbl_podata.Rows)
            {





                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {


                }
            }
        }

     

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (tbl_podata.Rows.Count == 0)
            {
                int atcid = int.Parse(cmb_atc.SelectedValue.ToString());


                ArtEntitiesnew enty = new ArtEntitiesnew();
                var q = from pckmst in enty.AtcMasters
                              where pckmst.AtcId == atcid
                              select pckmst;
                foreach (var element in q)
                {
                    element.IsClosed = "Y";
                }
                enty.SaveChanges();

                String msg = " ATC Closed Sucessfully ";

                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
            }

            
        }

        protected void btn_showallasq_Click(object sender, EventArgs e)
        {
            tbl_podata.DataSource = allPodatasorce;
            //  tbl_podata.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(drp_ourstyle.SelectedValue.ToString()), popaklist);
            tbl_podata.DataBind();


            if(tbl_podata.Rows.Count>0)
            {
                btn_closeatc.Enabled = false;
                String msg = " You Cannot Close an Atc which have Pending ASQ to Ship(Non ShortClosed)";

                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
            }
            else
            {
                btn_closeatc.Enabled = true;
            }

        }

        protected void tbl_podata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

             


            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
           
        }


       

      
    }
}