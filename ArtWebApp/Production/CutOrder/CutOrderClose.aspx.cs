using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production.CutOrder
{
    public partial class CutOrderClose : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_close_Click(object sender, EventArgs e)
        {
            
        
                    using(ArtWebApp.DataModels.ArtEntitiesnew enty= new DataModels.ArtEntitiesnew())
                    {

                foreach (GridViewRow di in GridView1.Rows)
                {
                    CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                    if (chkBx != null && chkBx.Checked)
                    {
                        int cutid = int.Parse(((di.FindControl("lbl_cutid") as Label).Text.ToString()));



                        var q = from cutorer in enty.CutOrderMasters
                                where cutorer.CutID == cutid
                                select cutorer;
                        foreach(var element in q)
                        {
                            element.IsClosed = "Y";
                        }
                       
                    }
                }
                enty.SaveChanges();
            }

            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}