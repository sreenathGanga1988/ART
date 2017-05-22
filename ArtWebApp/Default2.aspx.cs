
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["JSChecked"] == null)
        //    JSChecked - indicates if it tried to run the javascript version
        //{
        //    prevent infinite loop
        //   Session["JSChecked"] = "Checked";
        //    string path = Request.Url + "?JScript=1";
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "redirect",
        //      "window.location.href='" + path + "';", true);
        //}
        //if (Request.QueryString["JScript"] == null)
        //{
        //    Response.Redirect("Authorisation.aspx?navtype=Javascript"); ;
        //}

        //else
        //{


        //}

    }


    public void loadmarque()
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //var hubContext = GlobalHost.ConnectionManager.GetHubContext<ArtWebApp.ArtWebHub>();
        //hubContext.Clients.All.SayMessage();

    }
}