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
                    if (navtype == "Costing")
                    {
                        MultiView1.ActiveViewIndex = 0;
                        setgridviewPO();
                    }
                    else if (navtype == "RO")
                    {
                        //    btn_approveourStyle.Visible = false;
                        MultiView1.ActiveViewIndex = 1;
                        setgridviewRO();
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

            setgridviewPO();
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



        public void setgridviewPO()
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
        public void setgridviewRO()
        {


            if (HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh")
            {


                SqlDataSource3.SelectCommand = @"SELECT        RO_Pk, RONum, FRMATC, TOATC, ISNULL(FRMTEMP, '') + '' + ISNULL(FRMCOMP, '') + '' + ISNULL(FRMCONS, '') + '' + ISNULL(FRMWEIG, '') + '' + ISNULL(FRMITEMCOLOR, '') + '' + ISNULL(FRMSUPPCOLOR, '') 
                         + '' + ISNULL(FRMITEMSIZE, '') + '' + ISNULL(FRMSUPPSIZE, '') AS DESCRIPTION, Qty, Qty * RATE AS POVALUE, UOM, LocationName, LocationAddress, IsForwarded
FROM            (SELECT        RequestOrderMaster.RONum, AtcMaster.AtcNum AS FRMATC, AtcMaster_1.AtcNum AS TOATC, Template_Master.Description AS TOTEMP, Template_Master_1.Description AS FRMTEMP, RequestOrderDetails.Qty, 
                         SkuRawMaterialMaster.Composition AS FRMCOMP, SkuRawMaterialMaster.Construction AS FRMCONS, SkuRawMaterialMaster.Weight AS FRMWEIG, SkuRawMaterialMaster.Width AS FROMWID, 
                         SkuRawmaterialDetail.ItemColor AS FRMITEMCOLOR, SkuRawmaterialDetail.SupplierColor AS FRMSUPPCOLOR, SkuRawmaterialDetail.ItemSize AS FRMITEMSIZE, 
                         SkuRawmaterialDetail.SupplierSize AS FRMSUPPSIZE, RequestOrderDetails.CUnitPrice AS RATE, UOMMaster.UomName AS UOM, SkuRawMaterialMaster_1.Composition, SkuRawMaterialMaster_1.Construction, 
                         LocationMaster.LocationName, LocationMaster.LocationAddress, RequestOrderMaster.RO_Pk, RequestOrderMaster.IsApproved, RequestOrderMaster.IsForwarded, AtcMaster.MerchandiserName
FROM            SkuRawmaterialDetail INNER JOIN
                         RequestOrderMaster INNER JOIN
                         RequestOrderDetails ON RequestOrderMaster.RO_Pk = RequestOrderDetails.RO_Pk ON SkuRawmaterialDetail.SkuDet_PK = RequestOrderDetails.FromSkuDet_PK INNER JOIN
                         SkuRawmaterialDetail AS SkuRawmaterialDetail_1 ON RequestOrderDetails.ToSkuDet_PK = SkuRawmaterialDetail_1.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         SkuRawMaterialMaster AS SkuRawMaterialMaster_1 ON SkuRawmaterialDetail_1.Sku_PK = SkuRawMaterialMaster_1.Sku_Pk INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         AtcMaster AS AtcMaster_1 ON SkuRawMaterialMaster_1.Atc_id = AtcMaster_1.AtcId INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         Template_Master AS Template_Master_1 ON SkuRawMaterialMaster_1.Template_pk = Template_Master_1.Template_PK INNER JOIN
                         InventoryMaster ON RequestOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         UOMMaster ON InventoryMaster.Uom_Pk = UOMMaster.Uom_PK
GROUP BY RequestOrderMaster.RONum, RequestOrderMaster.CreatedDate, RequestOrderMaster.AddedBy, AtcMaster.AtcNum, AtcMaster_1.AtcNum, Template_Master.Description, Template_Master_1.Description, 
                         RequestOrderDetails.Qty, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Construction, SkuRawMaterialMaster.Weight, SkuRawMaterialMaster.Width, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize, RequestOrderDetails.CUnitPrice, UOMMaster.UomName, SkuRawMaterialMaster_1.Composition, 
                         SkuRawMaterialMaster_1.Construction, SkuRawMaterialMaster_1.Weight, SkuRawMaterialMaster_1.Width, SkuRawmaterialDetail_1.ItemColor, SkuRawmaterialDetail_1.SupplierColor, 
                         SkuRawmaterialDetail_1.ItemSize, SkuRawmaterialDetail_1.SupplierSize, LocationMaster.LocationName, LocationMaster.LocationAddress, RequestOrderMaster.RO_Pk, RequestOrderMaster.IsApproved, 
                         RequestOrderMaster.IsForwarded, AtcMaster.MerchandiserName
HAVING        (RequestOrderMaster.IsApproved = N'N') AND (AtcMaster.MerchandiserName  like '" + Session["username"].ToString().Trim() + "')) AS TT WHERE        (FRMATC = TOATC)";

            }

            tbl_ro.DataBind();
        }

        protected void btn_ro_Click(object sender, EventArgs e)
        {
            approveRO();
        }

        /// <summary>
        /// Approve RO
        /// </summary>
        public void approveRO()
        {

            BLL.ProcurementBLL.RequestOrderMasterData rmmstr = new BLL.ProcurementBLL.RequestOrderMasterData();
            for (int i = 0; i < tbl_ro.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_ro.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_ro.Rows[i].Cells[1].Text);
                    rmmstr.GetROApproved(popk);
                }

            }

            tbl_ro.DataBind();
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

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            selectall();
        }

        public void selectall()
        {
            if (chk_Costaproval.Checked == true)
            {
                foreach (GridViewRow di in GridView1.Rows)
                {
                    CheckBox chkBx = (CheckBox)di.FindControl("chk_select");
                    chkBx.Checked = true;

                }
            }
            else
            {
                foreach (GridViewRow di in GridView1.Rows)
                {
                    CheckBox chkBx = (CheckBox)di.FindControl("chk_select");
                    chkBx.Checked = false;

                }
            }
          ;
        }


        public void selectallRO()
        {
            if (chk_ro.Checked == true)
            {
                foreach (GridViewRow di in tbl_ro.Rows)
                {
                    CheckBox chkBx = (CheckBox)di.FindControl("chk_select");
                    chkBx.Checked = true;

                }
            }
            else
            {
                foreach (GridViewRow di in tbl_ro.Rows)
                {
                    CheckBox chkBx = (CheckBox)di.FindControl("chk_select");
                    chkBx.Checked = false;

                }
            }
        ;
        }

        protected void chk_ro_CheckedChanged(object sender, EventArgs e)
        {
            selectallRO();
        }
    }
}