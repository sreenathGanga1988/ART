using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Shipping.LC
{
    public partial class LCMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BLL.ShippingBLL.LCMasterData lcmstr = new BLL.ShippingBLL.LCMasterData();
            lcmstr.LCNum = txt_lcnum.Text.Trim ();
            lcmstr.AddedDate = DateTime.Now;
            lcmstr.AddedBy = Session["Username"].ToString().Trim(); 
            lcmstr.Location_Pk = 6;
            lcmstr.Supplier_pk = int.Parse (drp_supplier.SelectedItem.Value.ToString ());
            lcmstr.Bank_PK = int.Parse(drp_bank.SelectedItem.Value.ToString()); 
            lcmstr.Issuedate = DateTime.Parse(dtp_issuedate.Value.ToString());
            lcmstr.ExpiryDate = DateTime.Parse(dtp_expirydate.Value.ToString()); 
           lcmstr.InsertLC ();
           MessgeboxUpdate("sucess", lcmstr.LCNum+"  Added Sucessfully");
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
    } 
    
}