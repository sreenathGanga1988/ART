using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using Infragistics.Web.UI.NavigationControls;

namespace ArtWebApp
{

    public partial class DynamicMenuBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                loadexplorerebar();
                this.WebExplorerBar1.EnableViewState = true;
           
        }

        public void loadexplorerebar()
        {
            DataTable dt = null;
            DataTable dt2 = null;

            if (Session["MainMenuMaster"] == null || Session["SubMenuMaster"] == null)
            {
                getMenuData();
            }

            dt = (DataTable)Session["MainMenuMaster"];
            dt2 = (DataTable)Session["SubMenuMaster"];




            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ExplorerBarGroup grp = new ExplorerBarGroup();
                    grp.Text = dt.Rows[i]["MainmenuName"].ToString();
                    this.WebExplorerBar1.Groups.Add(grp);
                    int MAINMENU_PK = int.Parse(dt.Rows[i]["mAINmENU_pk"].ToString());
                    try
                    {

                        DataTable mainmenuchild = dt2.Select("parentid=" + MAINMENU_PK + "").CopyToDataTable();

                        foreach (DataRow drow in mainmenuchild.Rows)
                        {

                            int childid = int.Parse(drow["Menu_PK"].ToString());
                            ExplorerBarItem item = new ExplorerBarItem();
                            item.Text = drow["MenuText"].ToString();
                            item.NavigateUrl = drow["MenuURL"].ToString().Trim();
                            grp.Items.Add(item);
                            try
                            {
                                getnewItem(item, childid, dt2);
                            }
                            catch (Exception)
                            {


                            }
                        }
                    }
                    catch (Exception)
                    {


                    }


                }


            }

        }



        public void getnewItem(ExplorerBarItem item, int parentid, DataTable mainmenuchild)
        {
            if (parentid == 220)
            {
                int k = 0;
            }
            DataTable mainmenuchildtemp = mainmenuchild.Select("parentid=" + parentid + "").CopyToDataTable();
            foreach (DataRow drow in mainmenuchildtemp.Rows)
            {

                try
                {
                    int childid = int.Parse(drow["Menu_PK"].ToString());
                    ExplorerBarItem itemnum = new ExplorerBarItem();
                    itemnum.Text = drow["MenuText"].ToString();
                    itemnum.NavigateUrl = drow["MenuURL"].ToString().Trim();
                    item.Items.Add(itemnum);
                    getnewItem(itemnum, childid, mainmenuchild);
                }
                catch (Exception)
                {
                    ;
                }
            }
        }


        public void getMenuData()
        {
            SqlCommand cmd = new SqlCommand("select * from MainMenuMaster");

            DataTable dt = ReturnQueryResultDatatable(cmd);


            if (int.Parse(Session["UserProfile_Pk"].ToString()) != 1)
            {
                SqlCommand cmd1 = new SqlCommand(@"SELECT        SubMenuMaster.Menu_PK, SubMenuMaster.MenuText, SubMenuMaster.MenuURL, SubMenuMaster.ParentID, SubMenuMaster.isEnable, SubMenuMaster.IsNormal
FROM            SubMenuMaster INNER JOIN
                         UserProfileRights ON SubMenuMaster.Menu_PK = UserProfileRights.Menu_PK
WHERE(UserProfileRights.UserProfile_Pk = @Param2)");
                cmd1.Parameters.AddWithValue("@Param2", int.Parse(Session["UserProfile_Pk"].ToString()));
                DataTable dt2 = ReturnQueryResultDatatable(cmd1);
                Session["SubMenuMaster"] = dt2;
            }
            else
            {
                SqlCommand cmd1 = new SqlCommand(@"SELECT     * from SubMenuMaster");

                DataTable dt2 = ReturnQueryResultDatatable(cmd1);
                Session["SubMenuMaster"] = dt2;

            }



            Session["MainMenuMaster"] = dt;


        }


    

        public DataTable ReturnQueryResultDatatable(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString()))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);
            }

            return dt;
        }

    }
}