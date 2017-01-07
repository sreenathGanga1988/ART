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
            if (HttpContext.Current.User.Identity.Name == "Mannan" || HttpContext.Current.User.Identity.Name == "sree")
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

        protected void tbl_servicePO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            proc = new ProcurementTransaction();
            int popk = int.Parse(tbl_servicePO.Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text);
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
        }

       
    }
}