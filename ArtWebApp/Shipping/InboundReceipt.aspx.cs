using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Shipping
{
    public partial class InboundReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            updatePOPackmaster();
        }

        public void updatePOPackmaster()
        {
            foreach (GridViewRow di in tbl_podetails.Rows)
            {

                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");
                TextBox dtp_deliverydate = (TextBox)di.FindControl("dtp_deliverydate");
                if (chkBx != null && chkBx.Checked)
                {
                    int lbl_ShipingDoc_PK = int.Parse(((di.FindControl("lbl_ShipingDoc_PK") as Label).Text.ToString()));

                    string s = DateTime.Parse(Request.Form[dtp_deliverydate.UniqueID].ToString()).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);


                    BLL.ShippingBLL.ShippingDocumentMasterData shpmstrdata = new BLL.ShippingBLL.ShippingDocumentMasterData();

                    shpmstrdata.ReceiveShippingDocument(lbl_ShipingDoc_PK, DateTime.Parse(s));
                    

                         
                       

                   



                }
            }

            tbl_podetails.DataSource = null;
            tbl_podetails.DataBind();

            String msg = "Receipt Updated Successfully ";

            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
        }
    }
}