using Infragistics.Web.UI.NavigationControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
     



        if (!IsPostBack)
        {
           lbl_loc.Text = Session["lOC_Code"].ToString ();

          //    loadexplorerebar();
        }
    }

 


    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        Response.Redirect("Login.aspx");
    }

    protected void lbm_mvc_Click(object sender, EventArgs e)
    {
        UrlHelper urlHelp = new UrlHelper(HttpContext.Current.Request.RequestContext);
       
        Response.Redirect(urlHelp.Action( "Index", "Home",new { area = "ArtMVC" }));
    }
}
