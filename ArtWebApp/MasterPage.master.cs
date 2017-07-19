using Infragistics.Web.UI.NavigationControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
     



        if (!IsPostBack)
        {
          //  UserProfileName
           lbl_loc.Text = Session["lOC_Code"].ToString ();

            lbl_profile.Text= Session["UserProfileName"].ToString();
            //if(Session["menulist"]==null)
            //{
            //    fillMenu(); ;
            //}
            //else
            //{
            //    if(XmlDataSource1==null)
            //    {
            //        XmlDataSource1.Data = Session["menulist"].ToString();
            //    }
            //    else
            //    {
            //        XmlDataSource1.DataBind();

            //    }

            //}


        }
    }


    // public void fillMenu()
    //{

    //    DataSet ds = new DataSet();
    //    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString();
    //    using (SqlConnection conn = new SqlConnection(connStr))
    //    {
    //        string sql = "SELECT        Menu_PK, MenuText, MenuURL, ParentID, isEnable, IsNormal FROM SubMenuMaster";
    //        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
    //        da.Fill(ds);
    //        da.Dispose();
    //    }
    //    ds.DataSetName = "Menus";
    //    ds.Tables[0].TableName = "Menu";
    //    DataRelation relation = new DataRelation("ParentChild",
    //     ds.Tables["Menu"].Columns["Menu_PK"],
    //     ds.Tables["Menu"].Columns["ParentID"], true);

    //    relation.Nested = true;
    //    ds.Relations.Add(relation);

    //    String menulist = ds.GetXml();


    //    Session["menulist"] = menulist; 

    //    XmlDataSource1.Data = menulist;

    //}


    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        Response.Redirect("Login.aspx");
    }

    protected void lbm_mvc_Click(object sender, EventArgs e)
    {
        UrlHelper urlHelp = new UrlHelper(HttpContext.Current.Request.RequestContext);
       
        Response.Redirect(urlHelp.Action( "Index", "Home",new { area = "ArtMVC" }));
    }

    protected void WebExplorerBar1_ItemSelected(object sender, Infragistics.Web.UI.NavigationControls.ExplorerBarItemSelectedEventArgs e)
    {

        if (e.IsExplorerBarGroup() != true)
        {
            Session["SelectedGroup"] = e.NewSelectedItem.ParentItem.Index;
            Session["SelectedIndex"] = e.NewSelectedItem.Index;
        }
        string redirectURL = e.NewSelectedItem.Value.ToString();


       
            int cnt = WebExplorerBar1.Groups.Count;

           
            bool[] state = new bool[cnt];

            for (int i = 0; i < cnt; i++)
            {
                state[i] = WebExplorerBar1.Groups[i].Expanded;
            }

            Session["WebExplorerState"] = state;
        if (redirectURL.Trim() != "")
        {
            redirectURL = redirectURL.Replace("\r", string.Empty).Replace("\n", string.Empty);
            if (redirectURL != "")
                Response.Redirect(redirectURL);


        }

    }
    protected void WebExplorerBar1_PreRender(object sender, EventArgs e)
    {

        bool[] state = (bool[])(Session["WebExplorerState"]);

        if (state != null)
        {
            for (int i = 0; i < WebExplorerBar1.Groups.Count; i++)
            {
                WebExplorerBar1.Groups[i].Expanded = state[i];
            }
        }

        if (Session["SelectedIndex"] != null)
        {
            int selIndex = (int)Session["SelectedIndex"];
            int selGroup = (int)Session["SelectedGroup"];
            //WebExplorerBar1.Groups[selGroup].Items[selIndex].Selected = true;
        }

    }
}
