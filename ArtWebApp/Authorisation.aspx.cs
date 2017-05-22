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
            if (!IsPostBack)
            {

                String navtype = Request.QueryString["navtype"];

                if (navtype == "Approval")
                {
                    NoAuthorization();
                }
                else if (navtype == "Javascript")
                {
                    javascriptDisAbled();
                }
            }


        
        }


        public void NoAuthorization()
        {
            string message = "You are  not Authorised for this action .You will be redirected to the Home Page.";
            AuthorisationHeader.InnerText = message;
            AuthorisationHeader.Attributes["class"] = "error-message";
        }

        public void javascriptDisAbled()
        {
            string message = "Javascript is Disabled in this Browser .WebArt requires Javascript to work Correctly  .Please Contact IT  .";
            AuthorisationHeader.InnerText = message;
            AuthorisationHeader.Attributes["class"] = "error-message";
        }

    }
}