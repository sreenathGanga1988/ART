using ArtWebApp.DataModels;
using ArtWebApp.DBTransaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory
{
    public partial class ADNClosing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_closeadn.Enabled = false;
        }
        protected void btn_confirmAtc_Click(object sender, EventArgs e)
        {
            DBTransaction.InventoryTransaction.InventoryTransaction invtra = new DBTransaction.InventoryTransaction.InventoryTransaction();
            DataTable dt = invtra.GetADNDetails(int.Parse(Session["UserLoc_pk"].ToString()));
            GridView1.DataSource = dt;
            if (dt != null)
            {
                btn_closeadn.Enabled = true;
            }
            GridView1.DataBind();
            UpdatePanel1.Update();

        }

        protected void btn_adnclose_Click(object sender, EventArgs e)
        {

            if (ArtWebApp.Controls.Gridviewvalidation.countofRowselected(GridView1, "chk_select") > 0)
            {
                InsertDOdata();

            }
            else
            {
                string Msg = "alert('Please select the Items')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
            }

        }
        public void InsertDOdata()
        {

            foreach (GridViewRow di in GridView1.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int item_pk = int.Parse(((di.FindControl("lblDoc_Pk") as Label).Text.ToString()));
                    using (ArtEntitiesnew enty = new ArtEntitiesnew())
                    {
                        var q = from adn in enty.DocMasters   where adn.Doc_Pk == item_pk select adn;
                        foreach (var element in q)
                        {
                            element.IsClosed = "Y";
                            element.ClosedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                            element.ClosedDate= DateTime.Now;
                        }
                        enty.SaveChanges();

                    }
                }
            }
            GridView1.DataSource = null;
            GridView1.DataBind();
            btn_closeadn.Enabled = false;
            MessgeboxUpdate("sucess", "ADN Closed");

        }
        public void MessgeboxUpdate(String Messagetype, String Messg)
        {
            if (Messagetype == "sucess")
            {
                Messaediv.Attributes["class"] = "success";
                Messaediv.InnerText = Messg;
            }
            else
            {
                Messaediv.Attributes["class"] = "error-message";
                Messaediv.InnerText = Messg;
            }
        }
    }
}