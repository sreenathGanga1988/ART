using ArtWebApp.BLL.MerchandsingBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Approvals
{
    public partial class ASQApprovals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_asqShuffle_Click(object sender, EventArgs e)
        {
            AsqShuffleBLL ashfbll = new AsqShuffleBLL();
            for (int i = 0; i < tbl_asqShuffledetails.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_asqShuffledetails.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int costingpk = int.Parse(tbl_asqShuffledetails.Rows[i].Cells[1].Text);
                    ashfbll.ApproveAsqShuffle(costingpk);
                }

            }

            tbl_asqShuffledetails.DataBind();
        }
    }
}