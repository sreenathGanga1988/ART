using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Administrator
{
    public partial class UserMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
                String navtype = Request.QueryString["navtype"];

                string v = Request.QueryString["navtype"];
                if (navtype == "NewUser")
                {
                    MultiView1.ActiveViewIndex = 0;
                }
                else if (navtype == "Password")
                {
                    MultiView1.ActiveViewIndex = 1;
                }
                else if (navtype == "Profile")
                {
                    MultiView1.ActiveViewIndex = 2;
                }

            }


        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            insertuser();
        }



        public void insertuser()
        {
            BLL.UserBLL.UserData usmsdta = new BLL.UserBLL.UserData();
            //usmsdta.FetchUserData();
            if (usmsdta.isUserPresent(txt_username.Text.Trim()))
            {
                Label3.Text = "User Already exist";
            }
            else
            {
                usmsdta.UserName = txt_username.Text.Trim();
                usmsdta.Password = txt_password.Text.Trim();
                usmsdta.userloc_pk = int.Parse(drp_userlocation.SelectedValue.ToString());


              usmsdta.UserPROFILE_PK = int.Parse(drp_userlocation.SelectedValue.ToString());
                usmsdta.insertUserdata();
                Label3.Text = "User "+ txt_username.Text.Trim() + " Added Sucessfully";

            }
        }





        public void Updateuser()
        {
            BLL.UserBLL.UserData usmsdta = new BLL.UserBLL.UserData();
            usmsdta.UserName = drp_Name.SelectedItem.Text.ToString();
            usmsdta.Password = txt_oldpassword.Text.Trim();
            //usmsdta.FetchUserData();
            if (!(txt_newpassword.Text.Trim()==txt_confirmpassword.Text.Trim ()))
            {
                lbl_msg.Text = "New Password and Confirm password doesnot match";
            }
            else if(!usmsdta.IsUserAuthenicated ())
            {
                lbl_msg.Text = "Entered Username and Password are Wrong";
            }
            else
            {
                usmsdta.UserID = int.Parse(drp_Name.SelectedValue.ToString ());
                usmsdta.Password = txt_newpassword.Text.Trim();
               
                usmsdta.UpdatePassword();
                lbl_msg.Text = "User " + txt_username.Text.Trim() + " Added Sucessfully";

            }
        }


        protected void btn_submit_Click1(object sender, EventArgs e)
        {
            Updateuser();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            
            BLL.UserBLL.UserData usmsdta = new BLL.UserBLL.UserData();
            usmsdta.UserID = int.Parse ( drp_user.SelectedValue.ToString());
            usmsdta.UserPROFILE_PK = int.Parse(drp_userprofile.SelectedValue.ToString());
            usmsdta.UpdateProfile();
            Response.Write("<script>alert('Done');</script>");
        }
    }
}