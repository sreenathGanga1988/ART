using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DataModels;
using ArtWebApp.DBTransaction;
using System.Drawing;

namespace ArtWebApp.Approvals
{
    public partial class LocalCostingApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                if (HttpContext.Current.User.Identity.Name == "Mannan" || HttpContext.Current.User.Identity.Name == "Sreenath" || HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh")
                {
                    if (HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh")
                    {
                        //btn_approveAll.Visible = false;
                        setgridview();
                    }
                    else
                    {

                    }
                }
                else
                {
                    //string message = "You are  not Authorised for this action .You will be redirected to the Home Page.";
                    //string url = "./Default2.aspx";
                    //string script = "window.onload = function(){ alert('";
                    //script += message;
                    //script += "');";
                    //script += "window.location = '";
                    //script += url;
                    //script += "'; }";
                    //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                    Response.Redirect("../Authorisation.aspx?navtype = Approval");
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int costingpk = int.Parse(GridView1.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text);
            if (e.CommandName == "Approve")
            {
                CostingTransaction csttrans = new CostingTransaction();
                csttrans.ApproveCosting(costingpk);
            }
            else if (e.CommandName == "Reject")
            {

            }
            else if (e.CommandName == "Show")
            {

                Response.Redirect("~/Reports/Stylecostingprintable.aspx?costingid=" + costingpk.ToString());
            }



        }



        public void approveAction(int costingPK)
        {


        }

        protected void btn_approveAll_Click(object sender, EventArgs e)
        {
            CostingTransaction csttrans = new CostingTransaction();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                String chk_isreq = ((GridView1.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int costingpk = int.Parse(GridView1.Rows[i].Cells[1].Text);
                    csttrans.ApproveCosting(costingpk);
                }

            }

            setgridview();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CostingTransaction csttrans = new CostingTransaction();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                String chk_isreq = ((GridView1.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int costingpk = int.Parse(GridView1.Rows[i].Cells[1].Text);
                    csttrans.ForwardCosting(costingpk);
                }

            }

            GridView1.DataBind();
        }



        public void setgridview()
        {


            if (HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh")
            {


                SqlDataSource1.SelectCommand = @"SELECT StyleCostingMaster.Costing_PK, AtcDetails.OurStyle, AtcDetails.BuyerStyle, StyleCostingMaster.CreatedBy, StyleCostingMaster.CreatedDate, StyleCostingMaster.ApprovedBy, 
                         StyleCostingMaster.ApprovedDate, StyleCostingMaster.CostingCount, StyleCostingMaster.IsApplicable, StyleCostingMaster.IsSubmitted, StyleCostingMaster.IsAccountable, StyleCostingMaster.FOB, 
                         StyleCostingMaster.MarginValue, StyleCostingMaster.Margin, AtcMaster.MerchandiserName, StyleCostingMaster.IsFowarded
FROM            StyleCostingMaster INNER JOIN
                         AtcDetails ON StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId
WHERE        (StyleCostingMaster.IsApproved = N'N') AND (StyleCostingMaster.IsSubmitted = N'Y') AND (StyleCostingMaster.IsLast = N'Y') AND (StyleCostingMaster.IsLocalApproval = N'Y')
AND(AtcMaster.MerchandiserName  like '" + Session["username"].ToString().Trim() + "') order by StyleCostingMaster.Costing_PK desc";

            }

            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String isforwarded = e.Row.Cells[14].Text;
                if (isforwarded.Trim() == "Y")
                {
                    e.Row.BackColor = Color.Red;
                }
            }
        }
    }
}