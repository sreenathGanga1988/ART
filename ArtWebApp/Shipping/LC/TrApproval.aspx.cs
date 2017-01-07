using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Shipping.LC
{
    public partial class TrApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            inserttrapproval();
            MessgeboxUpdate("sucess", " TR Approved Sucessfully");
            tbl_podetails.DataSource = null;
            tbl_podetails.DataBind();
        }



        public void MessgeboxUpdate(String Messagetype, String Messg)
        {
            if (Messagetype == "sucess")
            {
                Messaediv.Attributes["class"] = "success";
                Messaediv.InnerText = Messg;
            }
            else
            {

            }
        }
   
    
       public void inserttrapproval()
        {
            BLL.ShippingBLL.LCBankAdviceDetailsData invdata = new BLL.ShippingBLL.LCBankAdviceDetailsData();
            foreach (GridViewRow di in tbl_podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int BankAdvicePk = int.Parse(((di.FindControl("lbl_BankAdvicePk") as Label).Text.ToString()));
                    invdata.updateLCBankAdviceDetails(BankAdvicePk);

                }
            }
        }
    
    
    
    
    
    
    
    }
}