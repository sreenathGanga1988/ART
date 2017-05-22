using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ArtWebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Username", typeof(string));
            table.Columns.Add("SessionID", typeof(string));
            table.Columns.Add("Location", typeof(string));
            Application["SessionData"] = table;


            Application["OnlineUsers"] = 0;

            ArrayList User = new ArrayList();
            Application["OnlineUsersname"] = User;
            ////when factories do mrn
            //Application["MRN"] = "MR";
            //Application["WWDO"] = "WW";
            //Application["WWRCPT"] = "WR";
            //Application["WFDO"] = "WF";
            //Application["WWRCPT"] = "FR";
            //Application["FWDO"] = "FW";
            //Application["DO"] = "DO";


            //Application["RO"] = "RO";
            //Application["ROIN"] = "RR";
            //Application["LOANOUT"] = "LO";
            //Application["LR"] = "LO";
            //Application["STOCKTRANSFER"] = "ST";


            //Application["APO"] = "APO";
            //Application["StockPO"] = "MPO";
            //Application["ServicePO"] = "SPO";
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["OnlineUsers"] = (int)Application["OnlineUsers"] + 1;
            Application.UnLock();

            //Session["Username"] = "Sree";
            //Session["UserLoc_pk"] = 4;
            //Session["lOC_Code"] = "ATRW";
            //Session["lOC_Type"] = "W";

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Exception exc = Server.GetLastError();

            //if (exc is HttpUnhandledException)
            //{
            //    // Pass the error on to the error page.
            //    Server.Transfer("~\\Error\\ErrorPage.aspx?handler=Application_Error%20-%20Global.asax", true);
            //}
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["OnlineUsers"] = (int)Application["OnlineUsers"] - 1;
            DataTable userdata = (DataTable)(Application["SessionData"]);

            for (int i = userdata.Rows.Count - 1; i >= 0; i--)
            {
                if (userdata.Rows[i]["Username"].ToString() == Session["Username"].ToString ())
                {
                    userdata.Rows[i].Delete();
                }
            }


            Application.UnLock();

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}