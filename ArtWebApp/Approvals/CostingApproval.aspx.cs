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
    public partial class CostingApproval : System.Web.UI.Page
    {
        List<String> Approverlist = new List<String>(new String[] { "Mannan", "siraj", "Abhishek", "Sreenath" });

        List<String> forwaderlist = new List<String>(new String[] { "Mahendra", "Vijeesh", "Abhishek", "vineeth" });
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                string currentusername = HttpContext.Current.User.Identity.Name.ToString();

                if (Approverlist.Contains(currentusername, StringComparer.OrdinalIgnoreCase) || forwaderlist.Contains(currentusername, StringComparer.OrdinalIgnoreCase))
                {


                    String navtype = Request.QueryString["navtype"];

                    string v = Request.QueryString["navtype"];
                    if (navtype == "Costing")
                    {
                        MultiView1.ActiveViewIndex = 0;
                        if (forwaderlist.Contains(currentusername, StringComparer.OrdinalIgnoreCase))
                        {
                            btn_approveAll.Visible = false;
                            //   btn_approveourStyle.Visible = false;
                            setgridview();
                        }
                    }
                    else if (navtype == "Ourstyle")
                    {
                        //    btn_approveourStyle.Visible = false;
                        MultiView1.ActiveViewIndex = 1;
                        setOurStyleGridview();
                    }
                }
                else
                {
                    Response.Redirect("../Authorisation.aspx?navtype=Approval");
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
WHERE(StyleCostingMaster.IsApproved = N'N') AND(StyleCostingMaster.IsSubmitted = N'Y') AND(StyleCostingMaster.IsLast = N'Y') AND(AtcMaster.MerchandiserName  like '" + Session["username"].ToString().Trim() + "') order by StyleCostingMaster.Costing_PK desc";
               
            }

            GridView1.DataBind();
        }



        public void setOurStyleGridview()
        {


            if (HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh")
            {


                OurStyleData.SelectCommand = @"SELECT        AtcDetailApproval_1.OurStyleApproval_PK, AtcDetails.OurStyleID, AtcDetails.OurStyle, AtcDetails.BuyerStyle, ISNULL
                             ((SELECT        SUM(Quantity) AS Expr1
                                 FROM            AtcDetailApproval
                                 WHERE        (IsFirst = N'Y') AND (OurStyleID = OurStyleID)), 0) AS IntialQty, AtcDetails.AtcId, AtcDetailApproval_1.Quantity, AtcDetails.FOB, AtcDetailApproval_1.IsForwarded, AtcDetailApproval_1.IsApproved, 
                         UserMaster.Department_PK
FROM            AtcDetails INNER JOIN
                         AtcDetailApproval AS AtcDetailApproval_1 ON AtcDetails.OurStyleID = AtcDetailApproval_1.OurStyleID INNER JOIN
                         UserMaster ON AtcDetailApproval_1.AddedBY = UserMaster.UserName
WHERE        (AtcDetailApproval_1.IsApproved = N'N') AND (UserMaster.Department_PK =  " + int.Parse(Session["Department_PK"].ToString()) + ")";

            }

            tbl_podetails.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String isforwarded = e.Row.Cells[14].Text;
                if (isforwarded.Trim () == "Y")
                {
                    e.Row.BackColor = Color.Aqua;
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
                    csttrans.ApproveOurStyle(costingpk);
                }

            }
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
                    csttrans.ForwardOurstyle(ourstylepk);
                }

            }
            setOurStyleGridview();
           // tbl_podetails.DataBind();
        }
    }
}