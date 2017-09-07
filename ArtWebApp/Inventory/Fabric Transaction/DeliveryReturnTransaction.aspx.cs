using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class DeliveryReturnTransaction : System.Web.UI.Page
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
            catch (Exception)
            {


            }

        }



        public void FillFabriDOcombo(int atcid)
        {
            int lctnpk = int.Parse(Session["UserLoc_pk"].ToString().Trim());
            ddl_do.DataSource = BLL.InventoryBLL.RollTransactionBLL.getFabricDeliveryReturn(atcid, lctnpk);

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


        public void FillCutorderDO(int skudetpk)
        {

            drp_cutorder.DataSource = BLL.InventoryBLL.RollTransactionBLL.GetCutOrderOFDO(int.Parse(Session["UserLoc_pk"].ToString()), skudetpk, int.Parse(ddl_do.SelectedValue.ToString()));

            drp_cutorder.DataValueField = "CutID";
            drp_cutorder.DataTextField = "Cut_NO";
            drp_cutorder.DataBind();
           Upd_cutorder.Update();



        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollmasterDataDetails fbrolldet = new BLL.InventoryBLL.FabricRollmasterDataDetails();

            //  tbl_inventory.DataSource = fbrolldet.getNonDeliveredRollofaIteminOneLocatiom(int.Parse(drp_color.SelectedValue.ToString()), int.Parse(Session["UserLoc_pk"].ToString().Trim()));
            FillCutorderDO(int.Parse(drp_color.SelectedValue.ToString()));
            tbl_inventory.DataSource = null;
            tbl_inventory.DataBind();
            upd_grid.Update();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //mrnrolldata.rollinvdata = getmstrdetails();
            BLL.InventoryBLL.FabricRollEntryDO dorolldata = new BLL.InventoryBLL.FabricRollEntryDO();            
            dorolldata.Docnum = ddl_do.SelectedItem.Text;
            dorolldata.cutid = int.Parse(drp_cutorder.SelectedValue.ToString());
            dorolldata.DoID = int.Parse(ddl_do.SelectedValue.ToString());

            dorolldata.RollInventoryDatadatacollection = GetRollDetailsData();
            dorolldata.insertDOReturnRollData();
            tbl_inventory.DataSource = null;
            tbl_inventory.DataBind();
            upd_grid.Update();
            ArtWebApp.Controls.WebMsgBox.Show("Rolls Returned Successfully");
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

        protected void btn_cutorder_Click(object sender, EventArgs e)
        {
            
            BLL.InventoryBLL.FabricRollmasterDataDetails fbrolldet = new BLL.InventoryBLL.FabricRollmasterDataDetails();
         //   FillCutorderDO(int.Parse(ddl_do.SelectedValue.ToString()), int.Parse(drp_color.SelectedValue.ToString()));
           tbl_inventory.DataSource = fbrolldet.GetAllRollsofAtcofColorWithSamegroupofCutorder( int.Parse(drp_cutorder.SelectedValue.ToString()), int.Parse(Session["UserLoc_pk"].ToString().Trim()), int.Parse(drp_color.SelectedValue.ToString()));
            tbl_inventory.DataBind();
            upd_grid.Update();

        }
    }
}