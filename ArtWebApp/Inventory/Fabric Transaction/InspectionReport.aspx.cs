using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class InspectionReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.RollInventoryData fbrolldet = new BLL.InventoryBLL.RollInventoryData();
            tbl_InverntoryDetails.DataSource = fbrolldet.getRollDetailsofATC(int.Parse(drp_atc.SelectedValue.ToString()));
         
            //upd_grid.Update();
        }

        protected void drp_atc_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataSource1.DataBind();
            tbl_InverntoryDetails.DataSource = SqlDataSource1;
            tbl_InverntoryDetails.DataBind();
        }
    }
}