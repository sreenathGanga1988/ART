using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser
{
    public partial class StyleCostingCopy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       

        protected void btn_CopyCosting_Click(object sender, EventArgs e)
        {
            BLL.CostingBLL.StyleCostingMaster scm = new BLL.CostingBLL.StyleCostingMaster();
        int costingid=    scm.CopyCostingFromOneStyletoAnother(int.Parse(ddl_frmourstyle.SelectedItem.Value.ToString()), int.Parse(ddl_toourstyle.SelectedItem.Value.ToString()));
        lbl_costingid.Text = "New Costing ID " + costingid.ToString() + "Is created Succesfully";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                hdf_atcid.Value = ddl_atc.SelectedValue.ToString();
                ddl_frmourstyle.DataBind();
                ddl_toourstyle.DataBind();
            }
            catch (Exception)
            {


            }
        }

        protected void tbl_costing_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void txt_consumption_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txt_rate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddl_frmourstyle_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
        {

        }

        protected void ddl_toourstyle_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
        {

        }

        protected void btn_atccost_Click(object sender, EventArgs e)
        {
            BLL.MerchandsingBLL.SKUCopy ckcpy = new BLL.MerchandsingBLL.SKUCopy();
            ckcpy.Copyrawmaterial(int.Parse(drp_frmatc.SelectedValue.ToString()), int.Parse(drp_toatc.SelectedValue.ToString()), drp_frmatc.SelectedItem.Text.Trim(), drp_toatc.SelectedItem.Text.Trim());

        }
    }
}