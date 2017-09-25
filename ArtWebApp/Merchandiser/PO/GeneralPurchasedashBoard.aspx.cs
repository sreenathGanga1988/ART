using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser.PO
{
    public partial class GeneralPurchasedashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string selectedpoid = "";
            foreach(GridViewRow gdrow in GridView1.Rows)
            {

                int polineid = int.Parse((gdrow.FindControl("lbl_polineid") as Label).Text);

                if (selectedpoid == "")
                {
                    selectedpoid = selectedpoid + polineid.ToString ().Trim();
                }
                else
                {
                    selectedpoid = selectedpoid + "," + polineid.ToString().Trim();
                }

            }
            Response.Redirect("IPOMultiCreator.aspx?selectionid="+selectedpoid+"");
        }

    
    }
}