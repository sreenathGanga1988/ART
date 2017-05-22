using System;
using System.Collections;
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
            ViewState["exptype"] = exptype;

            if(exptype.Trim ()=="Via")
            {
                drp_rcpt.DataSource = shpmstrdata.GetAWList();
            }
            else if (exptype.Trim() == "Direct")
            {
                drp_rcpt.DataSource = shpmstrdata.GetADNList();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ArrayList doclist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_rcpt.SelectedItems;
            BLL.ShippingBLL.ShippingDocumentMasterData shpmstrdata = new BLL.ShippingBLL.ShippingDocumentMasterData();
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int doc_pk = int.Parse(item.Value.ToString());
                int shipPK = int.Parse(drp_doc.SelectedValue.ToString());
                shpmstrdata.addShippingDetail(shipPK, doc_pk, ViewState["exptype"].ToString());
                string Msg = "alert('Details Added')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);

            }


           
            }
    }
}