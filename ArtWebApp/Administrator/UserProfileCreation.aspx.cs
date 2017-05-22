using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Administrator
{
    public partial class UserProfileCreation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_saveProfile_Click(object sender, EventArgs e)
        {
            List<BLL.UserBLL.UserRight> rk = new List<BLL.UserBLL.UserRight>(); 
            foreach (ListItem item in chkbx_gditem.Items)
            {
                if (item.Selected)
                {
                    BLL.UserBLL.UserRight usrright = new BLL.UserBLL.UserRight();

                    usrright.Profilepk = int.Parse(drp_userprofile.SelectedValue.ToString ());
                    usrright.Menu_pk = int.Parse(item.Value.ToString());
                    rk.Add(usrright);
                }
                else
                {
                  

                }
            }

            if(rk.Count>0)
            {
                BLL.UserBLL.UserRightmaster usrmstr = new BLL.UserBLL.UserRightmaster();
               usrmstr.profile_pk = int.Parse(drp_userprofile.SelectedValue.ToString());
                usrmstr.UserRightDataCollection = rk;
                usrmstr.InsertUserRight();
                chkbx_gditem.DataSource = null;
                chkbx_gditem.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            chkbx_gditem.DataBind();
            int userprof= int.Parse(drp_userprofile.SelectedValue.ToString());
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from prop in enty.UserProfileRights
                        where prop.UserProfile_Pk == userprof
                        select prop;

                foreach (var element in q)
                {
                    foreach (ListItem item in chkbx_gditem.Items)
                    {
                        if (item.Value == element.Menu_PK.ToString())
                        {
                            item.Selected = true;
                            item.Attributes.Add("style", "Color: Red");
                        }

                    }
                    }
            }
            }

        protected void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkbx_gditem.Items.Count; i++)
            {
                chkbx_gditem.Items[i].Selected = true;
            }
        }
    }
}