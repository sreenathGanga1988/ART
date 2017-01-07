using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class PreQadApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       

        protected void Button1_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.RollInventoryData fbrolldet = new BLL.InventoryBLL.RollInventoryData();
            tbl_InverntoryDetails.DataSource = fbrolldet.getRollDetailsofATC(int.Parse(drp_atc.SelectedValue.ToString()));
            tbl_InverntoryDetails.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollEntryMRN mrnrolldata = new BLL.InventoryBLL.FabricRollEntryMRN();
            mrnrolldata.Rolldatacollection = GetRollDetailsData();
            mrnrolldata.UpproveRollInspectiondata();
        }

        public List<BLL.InventoryBLL.FabricRollmasterDataDetails> GetRollDetailsData()
        {

            List<BLL.InventoryBLL.FabricRollmasterDataDetails> rk = new List<BLL.InventoryBLL.FabricRollmasterDataDetails>();
            for (int i = 0; i < tbl_InverntoryDetails.Rows.Count; i++)
            {

                CheckBox chkBx = (CheckBox)tbl_InverntoryDetails.Rows[i].FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {

                    String lbl_rollpk = ((tbl_InverntoryDetails.Rows[i].FindControl("lbl_rollpk") as Label).Text.ToString());
                   



                    BLL.InventoryBLL.FabricRollmasterDataDetails rolldata = new BLL.InventoryBLL.FabricRollmasterDataDetails();


                    rolldata.Roll_PK = int.Parse(lbl_rollpk);

                    DropDownList drp_markerType = (tbl_InverntoryDetails.Rows[i].FindControl("drp_markerType") as DropDownList);
                    DropDownList drp_acceptable = (tbl_InverntoryDetails.Rows[i].FindControl("drp_acceptable") as DropDownList);



                    rolldata.MarkerType = drp_markerType.SelectedItem.Text.Trim();
                    rolldata.IsAccepted = drp_markerType.SelectedItem.Text.Trim();

                    rk.Add(rolldata);
                }
            }




            return rk;


        }
    }
}