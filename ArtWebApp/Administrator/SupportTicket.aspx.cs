using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Administrator
{
    public partial class SupportTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void tbl_loanApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            int supportpk = int.Parse(tbl_loanApproval.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text);
            if (e.CommandName == "Approve")
            {
                BLL.UserBLL.SupportTicketBLL sptcktm = new BLL.UserBLL.SupportTicketBLL();
                sptcktm.supportClose(supportpk);
                
            }
            UpdatePanel2.Update();
        }

        public void insertTicket()
        {
            BLL.UserBLL.SupportTicketBLL sptcktm = new BLL.UserBLL.SupportTicketBLL();
            String spnum = sptcktm.InsertSupportticket(getdata());
            lbl_msg.Text = spnum + "is generated Sucessfully";

        }

        public BLL.UserBLL.SupportTicketBLL getdata()
        {
            BLL.UserBLL.SupportTicketBLL sptckt = new BLL.UserBLL.SupportTicketBLL();
            sptckt.SupportDescription= txta_desc.Value;
            sptckt.SupportTittle = txt_tittle.Text;
            sptckt.Status ="Pending";
            sptckt.AddedBy = Session["Username"].ToString ().Trim ();
            
            sptckt.Priority = drp_priority.Text;
            sptckt.Location_pk =  int.Parse (  Session["UserLoc_pk"].ToString ());
            sptckt.IsCompleted = "N";


            return sptckt;
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            insertTicket();
            UpdatePanel2.Update();
        }
    }
}