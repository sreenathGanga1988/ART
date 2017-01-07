using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Shipping
{
    public partial class InboundEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_show_Click(object sender, EventArgs e)
        {
            BLL.ShippingBLL.ShippingDocumentMasterData shpmstrdata = new BLL.ShippingBLL.ShippingDocumentMasterData();

            string exptype = shpmstrdata.getinboundtype(int.Parse(drp_doc.SelectedValue.ToString()));


            if(exptype=="Via")
            {
                drp_rcpt.DataSource = shpmstrdata.GetAWList();
            }
            else if (exptype == "Direct")
            {
                drp_rcpt.DataSource = shpmstrdata.GetADNList();
            }
        }
    }
}