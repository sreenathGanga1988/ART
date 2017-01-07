using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser
{
    public partial class AtcDetailProjection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonAtc_Click(object sender, EventArgs e)
        {
            tbl_podetails.DataBind();
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            int k = 0;
            foreach (GridViewRow di in tbl_podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    BLL.ProductionBLL.ShipmentHandOverMasterData SHPMSTR = new BLL.ProductionBLL.ShipmentHandOverMasterData();
                    int lbl_OurStyleID = int.Parse(((di.FindControl("lbl_OurStyleID") as Label).Text.ToString()));
                    Decimal txt_qty = Decimal.Parse(((di.FindControl("txt_qty") as TextBox).Text.ToString()));


                    BLL.ourstyleData ourdata = new BLL.ourstyleData();
                    ourdata.Ourstyleid = lbl_OurStyleID;
                    ourdata.ProjectionQty = txt_qty;
                    
                    ourdata.ForwardedBY = Session["Username"].ToString().Trim();
                    ourdata.AddedBY = Session["Username"].ToString().Trim();
                    ourdata.OurStyleForpproval();
                    k++;
                }
            }

            if(k>0)
            {
                string msg =  k +" OurStyle Projections are mapped";
                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
            }
           
        }
    
    }
}