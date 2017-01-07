using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Masters
{
    public partial class BuyerAttributes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String navtype = Request.QueryString["navtype"];

            string v = Request.QueryString["navtype"];
            if (navtype == "Channel")
            {
                MultiView1.ActiveViewIndex = 0;
            }
            else if (navtype == "Destination")
            {
                MultiView1.ActiveViewIndex = 1;
            }
            else if (navtype == "BuyerStyle")
            {
                MultiView1.ActiveViewIndex = 2;
            }
            else if (navtype == "PO")
            {
                MultiView1.ActiveViewIndex = 3;
            }
        }

        protected void BuyerNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}