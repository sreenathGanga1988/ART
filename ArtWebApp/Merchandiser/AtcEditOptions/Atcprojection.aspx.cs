using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser.AtcEditOptions
{
    public partial class Atcprojection : System.Web.UI.Page
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
                    int lbl_AtcId = int.Parse(((di.FindControl("lbl_AtcId") as Label).Text.ToString()));
                    Decimal txt_qty = Decimal.Parse(((di.FindControl("txt_qty") as TextBox).Text.ToString()));


                    BLL.AtcData ourdata = new BLL.AtcData();
                    ourdata.Atcid = lbl_AtcId;
                    ourdata.Qty = txt_qty;


                    ourdata.AtcForpproval();
                    k++;
                }
            }

            if (k > 0)
            {
                string msg = " Atc Projection Added";
                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
            }

        }

    }
}