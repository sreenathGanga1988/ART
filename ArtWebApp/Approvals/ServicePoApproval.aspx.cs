using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DBTransaction;                      
namespace ArtWebApp.Approvals
{
    public partial class ServicePoApproval : System.Web.UI.Page
    {
        DBTransaction.ProcurementTransaction proc = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.User.Identity.Name == "Mannan" || HttpContext.Current.User.Identity.Name == "Sreenath")
                {
                    proc = new ProcurementTransaction();
                    tbl_servicePO.DataSource = proc.GetServicePoForApproval();
                    tbl_servicePO.DataBind();

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

        protected void tbl_servicePO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            proc = new ProcurementTransaction();
            int popk = int.Parse(tbl_servicePO.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text);
            if (e.CommandName == "Approve")
            {
                proc.ApproveServicePO(popk);
            }
            else if (e.CommandName == "Reject")
            {
                proc.DeleteServicePO(popk);
            }
            else if (e.CommandName == "Show")
            {

            }
            tbl_servicePO.DataSource = proc.GetServicePoForApproval();
            tbl_servicePO.DataBind();
        }

        protected void btn_approveAll_Click(object sender, EventArgs e)
        {
            approveServiceO();
        }


        /// Approve RO
        /// </summary>
        public void approveServiceO()
        {

            proc = new ProcurementTransaction();
            for (int i = 0; i < tbl_servicePO.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_servicePO.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_servicePO.Rows[i].Cells[1].Text);
                    proc.ApproveServicePO(popk);
                }

            }
            tbl_servicePO.DataSource = proc.GetServicePoForApproval();
            tbl_servicePO.DataBind();
        }
    }
}