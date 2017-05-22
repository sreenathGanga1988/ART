using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class DORollTransaction : System.Web.UI.Page
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
                    FillFabriDOcombo(int.Parse(drp_atc.SelectedValue.ToString()));
                }
            }
            catch (Exception )
            {


            }

        }



        public void FillFabriDOcombo(int atcid)
        {
            int lctnpk = int.Parse(Session["UserLoc_pk"].ToString().Trim());
            ddl_do.DataSource = BLL.InventoryBLL.RollTransactionBLL.getFabricDoDetails(atcid, lctnpk);

            ddl_do.DataValueField = "DO_PK";
            ddl_do.DataTextField = "DONum";
            ddl_do.DataBind();
            upd_do.Update();



        }

        protected void btn_do_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_do.SelectedValue != null)
                {
                    FillFabricombo(int.Parse(ddl_do.SelectedValue.ToString()));
                }
            }
            catch (Exception exp)
            {


            }
        }





        public void FillFabricombo(int do_pk)
        {

            drp_color.DataSource = BLL.InventoryBLL.RollTransactionBLL.getFabricDetailsInsideDO(do_pk);

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
            BLL.InventoryBLL.FabricRollEntryDO dorolldata = new BLL.InventoryBLL.FabricRollEntryDO();
            //mrnrolldata.rollinvdata = getmstrdetails();
            dorolldata.Docnum = ddl_do.SelectedItem.Text;
            dorolldata.RollInventoryDatadatacollection = GetRollDetailsData();

            dorolldata.insertDORollData();
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

                    rolldata.DocumentNum = ddl_do.Text;
                    rolldata.roll_PK = int.Parse(lbl_rollpk);
                    rolldata.rollinventory_pk = int.Parse(lbl_RollInventory_PK);
                    rk.Add(rolldata);
                }



            }
            return rk;


        }
    }
}