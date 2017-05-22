using ArtWebApp.DataModels;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.BLL.InventoryBLL
{
    public partial class SessionChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = Application["OnlineUsers"].ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
         //   CopyCostingFromOneStyletoAnother(1,2);

            for (int i = 0; i < Session.Contents.Count; i++)
            {
              
            }
           
            }

        protected void Username_Click(object sender, EventArgs e)
        {

        






            DataTable userdata = (DataTable)(Application["SessionData"]);

            GridView1.DataSource = userdata;
            GridView1.DataBind();

            ArrayList ary = (ArrayList)(Application["OnlineUsersname"]);
            for (int i = 0; i < ary.Count; i++)
            {
                Response.Write(ary[i].ToString() + "<br />");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //var hubContext = GlobalHost.ConnectionManager.GetHubContext<ArtWebApp.ArtWebHub>();
            //hubContext.Clients.All.SayMessage();

        }
    }
}