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
            if(!IsPostBack)
            {
                tbl_InverntoryDetails.DataSource = null;
                tbl_InverntoryDetails.DataBind();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.RollInventoryData fbrolldet = new BLL.InventoryBLL.RollInventoryData();
            tbl_InverntoryDetails.DataSource = fbrolldet.getRollAllDetailsofATC(int.Parse(drp_atc.SelectedValue.ToString()));
            tbl_InverntoryDetails.DataBind();
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