using ArtWebApp.BLL.UserBLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp
{
    public partial class ArtLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        public void login()
        {
            UserData usdata = new UserData();
            if (usdata.IsUserAuthenicated(email.Text.Trim(), password.Text.Trim()))
            {



                FormsAuthentication.RedirectFromLoginPage(email.Text, false);

                string sessionId = System.Web.HttpContext.Current.Session.SessionID;

                Application.Lock();

                DataTable userdata = (DataTable)(Application["SessionData"]);

                userdata.Rows.Add(Session["Username"], sessionId, Session["lOC_Code"]);
              
                Application["SessionData"] = userdata;
                Application.UnLock();







                Response.Redirect("~/Default2.aspx");
            }
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            login();
        }
    }
}