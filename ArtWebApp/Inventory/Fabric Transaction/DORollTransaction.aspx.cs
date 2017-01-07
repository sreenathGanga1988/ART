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

            ddl_do.DataSource = BLL.InventoryBLL.RollTransactionBLL.getFabricDoDetails(atcid);

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

            //drp_color.DataSource = BLL.InventoryBLL.RollTransactionBLL.getFabricDetailsofDO(do_pk);

            //drp_color.DataValueField = "DODet_PK";
            //drp_color.DataTextField = "ItemDescription";
            //drp_color.DataBind();
            //upd_color.Update();



        }
    }
}