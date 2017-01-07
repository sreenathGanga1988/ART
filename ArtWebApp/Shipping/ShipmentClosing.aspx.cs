using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Shipping
{
    public partial class ShipmentClosing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_showPO_Click(object sender, EventArgs e)
        {
            try
            {
                loadShipmentCode(int.Parse(drp_factory.SelectedValue.ToString()));
            }
            catch (Exception)
            {
                throw;

            }
        }





        public void loadShipmentCode(int factid)
        {
            BLL.ProductionBLL.ShipmentHandOverMasterData SHPMSTR = new BLL.ProductionBLL.ShipmentHandOverMasterData();

            DataTable dt = SHPMSTR.GetIncompletedShipmenthandover(factid);
                tbl_podetails.DataSource = dt;
                tbl_podetails.DataBind();
                upd_grid.Update();
            
        }

        protected void chk_selectAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btn_JCSubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow di in tbl_podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    BLL.ProductionBLL.ShipmentHandOverMasterData SHPMSTR = new BLL.ProductionBLL.ShipmentHandOverMasterData();
                    int lbl_shipemtnDandover = int.Parse(((di.FindControl("lbl_shipemtnDandover") as Label).Text.ToString()));
                    SHPMSTR.markhipmentHandover(lbl_shipemtnDandover);
                }
            }
            loadShipmentCode(int.Parse(drp_factory.SelectedValue.ToString()));
        }
    }
}