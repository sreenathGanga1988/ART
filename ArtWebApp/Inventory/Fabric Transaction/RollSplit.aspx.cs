using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class RollSplit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_confirmAtc_Click(object sender, EventArgs e)
        {

        }

        protected void btn_atc_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_atc.SelectedValue != null)
                {
                    FillFabricombo(int.Parse(drp_atc.SelectedValue.ToString()));

                }
            }
            catch (Exception exp)
            {


            }
        }

    






   









    






        public void FillFabricombo(int atcid)
        {

            drp_color.DataSource = BLL.InventoryBLL.RollTransactionBLL.getfabricdetailsofATC(atcid);

            drp_color.DataValueField = "SkuDet_PK";
            drp_color.DataTextField = "ItemDescription";
            drp_color.DataBind();
            upd_color.Update();



        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollmasterDataDetails fbrolldet = new BLL.InventoryBLL.FabricRollmasterDataDetails();

            tbl_inventory.DataSource = fbrolldet.getNonDeliveredRollofaIteminOneLocatiom(int.Parse(drp_color.SelectedValue.ToString()), int.Parse(Session["UserLoc_pk"].ToString().Trim()));
            tbl_inventory.DataBind();
            upd_grid.Update();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String lbl_rollpk = "0";
            for (int i = 0; i < tbl_inventory.Rows.Count; i++)
            {
                CheckBox chkBx = (CheckBox)tbl_inventory.Rows[i].FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                     lbl_rollpk = ((tbl_inventory.Rows[i].FindControl("lbl_rollpk") as Label).Text.ToString());

                                    
                }
            }

            Response.Redirect(String.Format("~/Inventory/Fabric Transaction/RollSplitterForm.aspx?rollpk={0}", lbl_rollpk));

        }
    }
}