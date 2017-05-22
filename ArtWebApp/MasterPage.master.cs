using Infragistics.Web.UI.NavigationControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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

            //  loadexplorerebar();
        }
    }

//    public void loadexplorerebar()
//    {

//        SqlCommand cmd = new SqlCommand("select * from MainMenuMaster");

//        DataTable dt2 = new DataTable();
//        if(Session["UserProfileName"].ToString()=="Admin")
//        {
//            SqlCommand cmd1 = new SqlCommand(@"select * from SubMenuMaster");
         
//             dt2 = ArtWebApp.QueryFunctions.ReturnQueryResultDatatable(cmd1);
//        }
//        else
//        {
//            SqlCommand cmd1 = new SqlCommand(@"SELECT        SubMenuMaster.Menu_PK, SubMenuMaster.MenuText, SubMenuMaster.MenuURL, SubMenuMaster.ParentID
//FROM            SubMenuMaster INNER JOIN
//                         UserProfileRights ON SubMenuMaster.Menu_PK = UserProfileRights.Menu_PK
//WHERE(SubMenuMaster.isEnable = N'Y') AND(UserProfileRights.UserProfile_Pk = @Param1)");
//            cmd1.Parameters.AddWithValue("@Param1", int.Parse(Session["UserProfile_Pk"].ToString()));
//             dt2 = ArtWebApp.QueryFunctions.ReturnQueryResultDatatable(cmd1);

//        }


//        DataTable dt = ArtWebApp.QueryFunctions. ReturnQueryResultDatatable(cmd);

      
//        if (dt != null)
//        {
//            for (int i = 0; i < dt.Rows.Count; i++)
//            {
//                ExplorerBarGroup grp = new ExplorerBarGroup();
//                grp.Text = dt.Rows[i]["MainmenuName"].ToString();
//                this.WebExplorerBar1.Groups.Add(grp);
//                int MAINMENU_PK = int.Parse(dt.Rows[i]["mAINmENU_pk"].ToString());
//                try
//                {

//                    DataTable mainmenuchild = dt2.Select("parentid=" + MAINMENU_PK + "").CopyToDataTable();

//                    foreach (DataRow drow in mainmenuchild.Rows)
//                    {

//                        int childid = int.Parse(drow["Menu_PK"].ToString());
//                        ExplorerBarItem item = new ExplorerBarItem();
//                        item.Text = drow["MenuText"].ToString();
//                        item.NavigateUrl = drow["MenuURL"].ToString();
//                        grp.Items.Add(item);
//                        try
//                        {
//                            getnewItem(item, childid, dt2);
//                        }
//                        catch (Exception)
//                        {


//                        }
//                    }
//                }
//                catch (Exception)
//                {


//                }


//            }


//        }

//    }



//    public void getnewItem(ExplorerBarItem item, int parentid, DataTable mainmenuchild)
//    {
//        if (parentid == 220)
//        {
//            int k = 0;
//        }
//        DataTable mainmenuchildtemp = mainmenuchild.Select("parentid=" + parentid + "").CopyToDataTable();
//        foreach (DataRow drow in mainmenuchildtemp.Rows)
//        {

//            try
//            {
//                int childid = int.Parse(drow["Menu_PK"].ToString());
//                ExplorerBarItem itemnum = new ExplorerBarItem();
//                itemnum.Text = drow["MenuText"].ToString();
//                itemnum.NavigateUrl = drow["MenuURL"].ToString();
//                item.Items.Add(itemnum);
//                getnewItem(itemnum, childid, mainmenuchild);
//            }
//            catch (Exception)
//            {
//                ;
//            }
//        }
//    }


    protected void WebDataMenu1_ItemClick(object sender, Infragistics.Web.UI.NavigationControls.DataMenuItemEventArgs e)
    {


 //       if(e.Item.Value=="Master")
 //       {
 //           WebExplorerBar1.Groups[0].Visible =true;
 //       }
 //       else if(e.Item.Value=="Merchandising")
 //       {
 //            WebExplorerBar1.Groups[1].Visible =true;
 //       }
 //        else if(e.Item.Value=="Inventory")
 //       {
 //             WebExplorerBar1.Groups[2].Visible =true;
 //       }
 //        else if(e.Item.Value=="Production")
 //       {
 //             WebExplorerBar1.Groups[3].Visible =true;
 //       }

 //             else if(e.Item.Value=="Accounts")
 //       {
 //WebExplorerBar1.Groups[4].Visible =true;
 //       }
 //             else if(e.Item.Value=="Reports")
 //       {
 //WebExplorerBar1.Groups[5].Visible =true;
 //       }
 //             else if(e.Item.Value=="Administrator")
 //       {
 //                  WebExplorerBar1.Groups[6].Visible =true;
 //       }
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
      

      
    
}
