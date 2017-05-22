using ArtWebApp.DBTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Approvals
{
    public partial class AtcprojectionApproval : System.Web.UI.Page
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (HttpContext.Current.User.Identity.Name == "Mannan" || HttpContext.Current.User.Identity.Name == "sree" || HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh" || HttpContext.Current.User.Identity.Name == "Abhi")
                {
                    setOurStyleGridview();
                }
            }
        }

        protected void btn_approveourStyle_Click(object sender, EventArgs e)
        {
            AtcTransaction csttrans = new AtcTransaction();
            for (int i = 0; i < tbl_podetails.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_podetails.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int costingpk = int.Parse(tbl_podetails.Rows[i].Cells[1].Text);
                    csttrans.ApproveAtcProjection(costingpk);
                }

            }
            tbl_podetails.DataBind();
        }

        protected void btn_forwardourstylw_Click(object sender, EventArgs e)
        {
            AtcTransaction csttrans = new AtcTransaction();
            for (int i = 0; i < tbl_podetails.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_podetails.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int ourstylepk = int.Parse(tbl_podetails.Rows[i].Cells[1].Text);
                    csttrans.ForwardAtcProjection(ourstylepk);
                }

            }
            setOurStyleGridview();
        }







        public void setOurStyleGridview()
        {


            if (HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh")
            {


                OurStyleData.SelectCommand = @"SELECT        AtcApproval_1.AtcApproval_PK, AtcMaster.AtcNum, ISNULL
                             ((SELECT        SUM(Quantity) AS Expr1
                                 FROM            AtcApproval
                                 WHERE        (IsFirst = N'Y') AND (AtcId = AtcMaster.AtcId)),  AtcMaster.ProjectionQty) AS IntialQty, AtcApproval_1.Quantity, AtcApproval_1.IsForwarded, AtcApproval_1.IsApproved, BuyerMaster.BuyerName, AtcMaster.AtcId
FROM            AtcMaster INNER JOIN
                         AtcApproval AS AtcApproval_1 ON AtcMaster.AtcId = AtcApproval_1.AtcId INNER JOIN
                         BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID INNER JOIN
                         UserMaster ON AtcApproval_1.AddedBY = UserMaster.UserName
WHERE        (AtcApproval_1.IsApproved = N'N') AND (UserMaster.Department_PK =  " + int.Parse(Session["Department_PK"].ToString()) + ")";

            }

            tbl_podetails.DataBind();
        }

        protected void tbl_podetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DBTransaction.AtcTransaction atctran = new DBTransaction.AtcTransaction();
                Label lbl_atcid = (e.Row.FindControl("lbl_atcid") as Label);
                GridView tbl_atcdetail = (e.Row.FindControl("tbl_atcdetail") as GridView);
                int atcid = int.Parse(lbl_atcid.Text);
                tbl_atcdetail.DataSource = atctran.GetOurStyleDetails(atcid);
                tbl_atcdetail.DataBind();
            }
         
          
        }
    }
}