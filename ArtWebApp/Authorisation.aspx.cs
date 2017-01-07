using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp
{
    public partial class Authorisation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string message = "You are  not Authorised for this action .You will be redirected to the Home Page.";
            AuthorisationHeader.InnerText = message;
            AuthorisationHeader.Attributes["class"] = "error-message";
        }
    }
}