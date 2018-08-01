using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Shipping
{
    public partial class ShiipingDashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
          
            Response.Redirect("~/Shipping/PendingDisplayer.aspx");
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String refnum = GridView2.Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text;
            string reff = refnum;
            if (e.CommandName == "Show")
            {

                Response.Redirect("~/Shipping/PendingDisplayer.aspx?refnum=" + refnum.ToString());
            }
        }
    }
}