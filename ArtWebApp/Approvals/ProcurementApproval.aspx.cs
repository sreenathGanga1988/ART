using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DBTransaction;
using System.Drawing;

namespace ArtWebApp.Approvals
{
    public partial class ProcurementApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (HttpContext.Current.User.Identity.Name == "Mannan" || HttpContext.Current.User.Identity.Name == "sree" || HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh" || HttpContext.Current.User.Identity.Name == "vinod")
                {
                    String navtype = Request.QueryString["navtype"];

                    string v = Request.QueryString["navtype"];
                    if (navtype == "PO")
                    {
                        MultiView1.ActiveViewIndex = 0;
                    }
                    else if (navtype == "SPOApproval")
                    {
                        MultiView1.ActiveViewIndex = 1;
                    }
                    else if (navtype == "RO")
                    {
                        MultiView1.ActiveViewIndex = 2;
                    }
                    else if (navtype == "SRO")
                    {
                        MultiView1.ActiveViewIndex = 3;
                    }
                    else if (navtype == "WPO")
                    {
                        MultiView1.ActiveViewIndex = 4;
                    }
                    else if (navtype == "EBOM")
                    {
                        MultiView1.ActiveViewIndex = 5;
                    }


                    if (HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh" || HttpContext.Current.User.Identity.Name == "vinod")
                    {
                        btn_approveAll.Visible = false;
                        btn_spoapproval.Visible = false;
                        btn_stockro.Visible = false;
                        btn_wrongpo.Visible = false;
                        btn__approveextrabom.Visible = false;
                        btn_ro.Visible = false;
                        setgridview();
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

                    Response.Redirect("../Authorisation.aspx");
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
        }








        #region PO





        protected void btn_approveAll_Click(object sender, EventArgs e)
        {
            approvePO();
        }



        /// <summary>
        /// Approve PO
        /// </summary>
        public void approvePO()
        {

            ProcurementTransaction ptrans = new ProcurementTransaction();
            for (int i = 0; i < tbl_podata.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_podata.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_podata.Rows[i].Cells[1].Text);
                    ptrans.ApprovePO(popk);
                }

            }

            tbl_podata.DataBind();
        }
        /// <summary>
        /// Forward PO for Approval in Tablet
        /// </summary>
        public void ForwardPOforApproval()
        {
            ProcurementTransaction ptrans = new ProcurementTransaction();
            for (int i = 0; i < tbl_podata.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_podata.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_podata.Rows[i].Cells[1].Text);
                    ptrans.ForwardApprovePO(popk);
                }

            }

            setgridview();
        }

        public void setgridview()
        {
            if (HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh")
            {

                SqlDataSource1.SelectCommand = @"SELECT        ProcurementMaster.PO_Pk, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName, CurrencyMaster.CurrencyCode, SUM(ProcurementDetails.POQty * ProcurementDetails.POUnitRate) 
                         AS POValue, ProcurementMaster.AddedDate, ProcurementMaster.AddedBy, ProcurementMaster.IsDeleted, AtcMaster.MerchandiserName, COUNT(POApproval.ForwardedBy) AS Isforwarded
FROM            ProcurementMaster INNER JOIN
                         SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         CurrencyMaster ON ProcurementMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                         ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk LEFT OUTER JOIN
                         POApproval ON ProcurementMaster.PO_Pk = POApproval.PO_PK
GROUP BY ProcurementMaster.PO_Pk, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName, CurrencyMaster.CurrencyCode, ProcurementMaster.AddedDate, ProcurementMaster.AddedBy, 
                         ProcurementMaster.IsApproved, ProcurementMaster.IsDeleted, AtcMaster.MerchandiserName
HAVING        (ProcurementMaster.IsApproved = N'N') AND (ProcurementMaster.IsDeleted <> N'Y')  AND(AtcMaster.MerchandiserName  like '" + Session["username"].ToString().Trim() + "') order by ProcurementMaster.PO_Pk";
            }
            tbl_podata.DataBind();

        }


        protected void Button2_Click(object sender, EventArgs e)
        {

            ForwardPOforApproval();
        }


        protected void tbl_podata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String isforwarded = e.Row.Cells[12].Text;
                if (isforwarded.Trim() != "0")
                {
                    e.Row.BackColor = Color.Aqua;
                }
            }
        }

        protected void tbl_podata_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ProcurementTransaction ptrans = new ProcurementTransaction();
            int popk = int.Parse(tbl_podata.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text);
            if (e.CommandName == "Approve")
            {

                ptrans.ApprovePO(popk);
            }
            else if (e.CommandName == "Reject")
            {

                ptrans.DeletePO(popk);
            }
            else if (e.CommandName == "Show")
            {
                String ponum = tbl_podata.Rows[int.Parse(e.CommandArgument.ToString())].Cells[2].Text.Trim();
                Response.Redirect(String.Format("~/Reports/View.aspx?navtype={0}&POnum={1}", "PO", ponum));
            }
        }


        #endregion






        #region SPO

        /// <summary>
        /// Approve SPO
        /// </summary>
        public void approveSPO()
        {

            BLL.ProcurementBLL.StockPOMasterdata ptrans = new BLL.ProcurementBLL.StockPOMasterdata();
            for (int i = 0; i < tbl_generalpo.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_generalpo.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_generalpo.Rows[i].Cells[1].Text);
                    ptrans.ApproveSPO(popk);
                }

            }

            tbl_generalpo.DataBind();
        }
        /// <summary>
        /// Forward SPO for Approval in Tablet
        /// </summary>
        public void ForwardSPOforApproval()
        {
            BLL.ProcurementBLL.StockPOMasterdata ptrans = new BLL.ProcurementBLL.StockPOMasterdata();
            for (int i = 0; i < tbl_generalpo.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_generalpo.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_generalpo.Rows[i].Cells[1].Text);
                    ptrans.ForwardforSPOApproval(popk);
                }

            }

            tbl_generalpo.DataBind();
        }

        protected void btn_spoapproval_Click(object sender, EventArgs e)
        {
            approveSPO();
        }

        protected void btn_forwardspo_Click(object sender, EventArgs e)
        {
            ForwardSPOforApproval();
        }
        protected void tbl_generalpo_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        #endregion




        #region RO



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


        public void ForwardROforApproval()
        {
            BLL.ProcurementBLL.RequestOrderMasterData rmmstr = new BLL.ProcurementBLL.RequestOrderMasterData();
            for (int i = 0; i < tbl_ro.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_ro.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_ro.Rows[i].Cells[1].Text);


                    rmmstr.ForwardROforApproval(popk);
                }

            }

            tbl_ro.DataBind();
        }
        protected void tbl_ro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ro_pk = int.Parse(tbl_ro.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text);
            if (e.CommandName == "Approve")
            {
                BLL.ProcurementBLL.RequestOrderMasterData rmmstr = new BLL.ProcurementBLL.RequestOrderMasterData();
                rmmstr.GetROApproved(ro_pk);
            }
            else if (e.CommandName == "Reject")
            {
                BLL.ProcurementBLL.RequestOrderMasterData rmmstr = new BLL.ProcurementBLL.RequestOrderMasterData();
                rmmstr.GetRODeleted(ro_pk);
            }
            else if (e.CommandName == "Show")
            {

            }
        }
       
        protected void btn_forwardro_Click(object sender, EventArgs e)
        {
            ForwardROforApproval();
        }
        protected void btn_ro_Click(object sender, EventArgs e)
        {
            approveRO();
        }
        #endregion




        #region SRO

        /// <summary>
        /// Approve RO
        /// </summary>
        public void approveSRO()
        {

            BLL.ProcurementBLL.RequestOrderMasterData rmmstr = new BLL.ProcurementBLL.RequestOrderMasterData();
            for (int i = 0; i < tbl_sro.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_sro.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_sro.Rows[i].Cells[1].Text);
                    rmmstr.GetsTOCKROApproved(popk);
                }

            }

            tbl_sro.DataBind();
        }


        public void ForwardSROforApproval()
        {
            BLL.ProcurementBLL.RequestOrderMasterData rmmstr = new BLL.ProcurementBLL.RequestOrderMasterData();
            for (int i = 0; i < tbl_sro.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_sro.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_sro.Rows[i].Cells[1].Text);


                    rmmstr.ForwardsTOCKROforApproval(popk);
                }

            }

            tbl_ro.DataBind();
        }







        protected void btn_stockro_Click(object sender, EventArgs e)
        {
            approveSRO();
        }

        protected void btn_forwardsro_Click(object sender, EventArgs e)
        {
            ForwardSROforApproval();
        }

        #endregion












        #region WrongPO




        /// <summary>
        /// Approve WrongPO
        /// </summary>
        public void approveWrongPO()
        {

            BLL.ProcurementBLL.WrongPOActions rmmstr = new BLL.ProcurementBLL.WrongPOActions();
            for (int i = 0; i < tbl_sro.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_sro.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_sro.Rows[i].Cells[1].Text);
                    rmmstr.GetWrongPOApproved(popk);
                }

            }

            tbl_sro.DataBind();
        }


        public void ForwardWrongPOforApproval()
        {
            BLL.ProcurementBLL.WrongPOActions rmmstr = new BLL.ProcurementBLL.WrongPOActions();
            for (int i = 0; i < tbl_sro.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_sro.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_sro.Rows[i].Cells[1].Text);


                    rmmstr.ForwardWrongPOApproval(popk);
                }

            }

            tbl_ro.DataBind();
        }









        protected void btn_wrongpo_Click(object sender, EventArgs e)
        {
            approveWrongPO();
        }

        protected void btn_forwardwrongpo_Click(object sender, EventArgs e)
        {
            ForwardWrongPOforApproval();
        }


        #endregion


        #region EXTRABOM

        

        /// <summary>
        /// Approve RO
        /// </summary>
        public void approveEBOM()
        {

            BLL.ProcurementBLL.ExtraBOMActions wrngmdata = new BLL.ProcurementBLL.ExtraBOMActions();
            for (int i = 0; i < tbl_extrabom.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_extrabom.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_extrabom.Rows[i].Cells[1].Text);
                    wrngmdata.GetEBOMApproved(popk);
                }

            }

            tbl_sro.DataBind();
        }


        public void ForwardEBOMforApproval()
        {
            BLL.ProcurementBLL.ExtraBOMActions wrngmdata = new BLL.ProcurementBLL.ExtraBOMActions();
            for (int i = 0; i < tbl_extrabom.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_extrabom.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_extrabom.Rows[i].Cells[1].Text);


                    wrngmdata.ForwardEBOMApproval(popk);
                }

            }

            tbl_ro.DataBind();
        }

        protected void btn_forwardextrabom_Click(object sender, EventArgs e)
        {
            ForwardEBOMforApproval();
        }
        protected void btn__approveextrabom_Click(object sender, EventArgs e)
        {
            approveEBOM();
        }
        protected void btn__approveextrabom_Click1(object sender, EventArgs e)
        {
            approveEBOM();
        }



        #endregion

       
    }
}