using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Masters
{
    public partial class Types : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String navtype = Request.QueryString["navtype"];

            string v = Request.QueryString["navtype"];
            if (navtype == "Warehouse Type")
            {
                MultiView1.ActiveViewIndex = 0;
            }
            else if (navtype == "Factory Type")
            {
                MultiView1.ActiveViewIndex = 1;
            }
            else if (navtype == "Season")
            {
                MultiView1.ActiveViewIndex = 2;
            }
            else if (navtype == "SeasonType")
            {
                MultiView1.ActiveViewIndex = 3;
            }
            else if (navtype == "Container")
            {
                MultiView1.ActiveViewIndex = 4;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            insertseason();
        }


        public void insertseason()
        {
            String year = drp_year.SelectedItem.Text.Trim();
            String sesn = drp_season.SelectedItem.Text.Trim();

            string season = year + "-" + sesn;

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                if (!enty.SeasonMasters.Any(f => f.SeasonName == season))
                {
                    SeasonMaster snmstr = new SeasonMaster();
                    snmstr.SeasonName = season;

                    enty.SeasonMasters.Add(snmstr);
                    enty.SaveChanges();
                }
                
                
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

    }
}