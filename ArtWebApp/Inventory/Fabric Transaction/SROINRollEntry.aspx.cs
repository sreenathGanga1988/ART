using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class SROINRollEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_atc_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_atc.SelectedValue != null)
                {
                    FillFabricROincombo(int.Parse(drp_atc.SelectedValue.ToString()));
                }
            }
            catch (Exception)
            {


            }

        }



        public void FillFabricROincombo(int atcid)
        {
            int lctnpk = int.Parse(Session["UserLoc_pk"].ToString().Trim());
            ddl_ro.DataSource = BLL.InventoryBLL.RollTransactionBLL.getFabricSROIN(atcid, lctnpk);

            ddl_ro.DataValueField = "RoInStock_PK";
            ddl_ro.DataTextField = "RoInStockNum";
            ddl_ro.DataBind();
            upd_do.Update();



        }

        protected void btn_do_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_ro.SelectedValue != null)
                {
                    FillFabricombo(int.Parse(ddl_ro.SelectedValue.ToString()));
                }
            }
            catch (Exception exp)
            {


            }
        }





        public void FillFabricombo(int do_pk)
        {

            drp_color.DataSource = BLL.InventoryBLL.RollTransactionBLL.getFabricDetailsInsideSROin(do_pk);

            drp_color.DataValueField = "SkuDet_PK";
            drp_color.DataTextField = "ItemDescription";
            drp_color.DataBind();
            upd_color.Update();



        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollmasterDataDetails fbrolldet = new BLL.InventoryBLL.FabricRollmasterDataDetails();

            tbl_inventory.DataSource = fbrolldet.getNonDeliveredRollofaIteminOneLocatiomGstock(int.Parse(drp_color.SelectedValue.ToString()), int.Parse(Session["UserLoc_pk"].ToString().Trim()));
            tbl_inventory.DataBind();
            upd_grid.Update();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollEntryROIN rorolldata = new BLL.InventoryBLL.FabricRollEntryROIN();

            rorolldata.Docnum = ddl_ro.SelectedItem.Text;
            rorolldata.SkuDet_PK = int.Parse(drp_color.SelectedValue.ToString());
            rorolldata.roin_PK = int.Parse(ddl_ro.SelectedValue.ToString());
            //mrnrolldata.rollinvdata = getmstrdetails();
            rorolldata.RollInventoryDatadatacollection = GetRollDetailsData();

            rorolldata.insertSROINRollData();
            tbl_inventory.DataSource = null;
            tbl_inventory.DataBind();
            upd_grid.Update();
        }



        public List<BLL.InventoryBLL.RollInventoryData> GetRollDetailsData()
        {



            List<BLL.InventoryBLL.RollInventoryData> rk = new List<BLL.InventoryBLL.RollInventoryData>();

            for (int i = 0; i < tbl_inventory.Rows.Count; i++)
            {
                CheckBox chkBx = (CheckBox)tbl_inventory.Rows[i].FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    String lbl_rollpk = ((tbl_inventory.Rows[i].FindControl("lbl_rollpk") as Label).Text.ToString());

                    String lbl_RollInventory_PK = ((tbl_inventory.Rows[i].FindControl("lbl_RollInventory_PK") as Label).Text.ToString());


                    BLL.InventoryBLL.RollInventoryData rolldata = new BLL.InventoryBLL.RollInventoryData();


                    rolldata.roll_PK = int.Parse(lbl_rollpk);
                    rolldata.rollinventory_pk = int.Parse(lbl_RollInventory_PK);
                    rk.Add(rolldata);
                }



            }
            return rk;


        }

    }
}