using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Approvals
{
    public partial class InventoryApprovals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String navtype = Request.QueryString["navtype"];

            string v = Request.QueryString["navtype"];
            if (navtype == "Ro Approval")
            {
                MultiView1.ActiveViewIndex = 0;
            }
            else if (navtype == "Loan Approval")
            {
                MultiView1.ActiveViewIndex = 1;
            }
            else if (navtype == "Transfer")
            {
                MultiView1.ActiveViewIndex = 2;
            }




        }

        protected void tbl_ROApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ro_pk = int.Parse(tbl_ROApproval.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text);
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

        protected void tbl_loanApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int loan_pk = int.Parse(tbl_loanApproval.Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text);
            //if (e.CommandName == "Approve")
            //{
            //    BLL.InventoryBLL.LoanTransfer lnmstr = new BLL.InventoryBLL.LoanTransfer();
            //    lnmstr.GetLoanApproved(loan_pk);
            //}
            //else if (e.CommandName == "Reject")
            //{
            //    BLL.InventoryBLL.LoanTransfer lnmstr = new BLL.InventoryBLL.LoanTransfer();
            //    lnmstr.GetLoanDeleted(loan_pk);
            //}
            //else if (e.CommandName == "Show")
            //{

            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
          

        }



        public void ApproveLoan()
        {
            BLL.InventoryBLL.LoanTransfer lnmstr = new BLL.InventoryBLL.LoanTransfer();
            for (int i = 0; i < tbl_loanApproval.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_loanApproval.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int loanpk = int.Parse(tbl_loanApproval.Rows[i].Cells[1].Text);
                    lnmstr.GetLoanApproved(loanpk);
                }

            }
            tbl_loanApproval.DataBind();
        }


        public void RejectLoan()
        {
            BLL.InventoryBLL.LoanTransfer lnmstr = new BLL.InventoryBLL.LoanTransfer();
            for (int i = 0; i < tbl_loanApproval.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_loanApproval.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int loanpk = int.Parse(tbl_loanApproval.Rows[i].Cells[1].Text);
                    lnmstr.GetLoanDeleted(loanpk);
                }

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
           
        }

        protected void btn_approveloan(object sender, EventArgs e)
        {
            ApproveLoan();
        }

        protected void btn_rejectloan(object sender, EventArgs e)
        {
            RejectLoan();
        }



        public void ApproveTransfertoGstock()
        {
            BLL.InventoryBLL.LoanTransfer lnmstr = new BLL.InventoryBLL.LoanTransfer();
            for (int i = 0; i < tbl_transfer.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_transfer.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int loanpk = int.Parse(tbl_transfer.Rows[i].Cells[1].Text);
                    lnmstr.GetLoanDeleted(loanpk);
                }

            }
        }




        protected void Button3_Click(object sender, EventArgs e)
        {

        }
    }
}