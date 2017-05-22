using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Shipping
{
    public partial class AtcExpenseMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_showPO_Click(object sender, EventArgs e)
        {
            SqlDataSource2.DataBind();
            tbl_podetails.DataSource = SqlDataSource2;
            tbl_podetails.DataBind();
            upd_grid.Update();
        }

        protected void chk_selectAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btn_JCSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}