using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ArtWebApp.DataModels;

namespace ArtWebApp.Merchandiser.PO
{
    public partial class GeneralPurchasedashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string selectedpoid = "";
            foreach(GridViewRow gdrow in GridView1.Rows)
            {
                CheckBox chkBx = (CheckBox)gdrow.FindControl("chk_select");
                if (chkBx.Checked == true)
                {
                    int polineid = int.Parse((gdrow.FindControl("lbl_polineid") as Label).Text);

                    if (selectedpoid == "")
                    {
                        selectedpoid = selectedpoid + polineid.ToString().Trim();
                    }
                    else
                    {
                        selectedpoid = selectedpoid + "," + polineid.ToString().Trim();
                    }
                }
            }
            Response.Redirect("IPOMultiCreator.aspx?selectionid="+selectedpoid+"");
        }

        protected void btn_closeipo_Click(object sender, EventArgs e)
        {

            string selectedpoid = "";
            foreach (GridViewRow gdrow in GridView1.Rows)
            {
                CheckBox chkBx = (CheckBox)gdrow.FindControl("chk_select");
                if (chkBx.Checked == true)
                {
                    int polineid = int.Parse((gdrow.FindControl("lbl_polineid") as Label).Text);
                    using (ArtEntitiesnew enty = new ArtEntitiesnew())
                    {
                    
                        var q = from pckmst in enty.ODOOGPOMasters
                            where pckmst.POLineID == polineid
                            select pckmst;
                    foreach (var element in q)
                    {
                        element.IsClosed = "Y";
                    }
                    enty.SaveChanges();
                    }
                    String msg = " IPO Closed Sucessfully ";

                    ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);

                }

            }



            }
        protected void btn_closespo_Click(object sender, EventArgs e)
        {

            string selectedspoid = "";
            foreach (GridViewRow gdrow in tbl_pendingtoreceive.Rows)
            {
                CheckBox chkBx = (CheckBox)gdrow.FindControl("chk_select");
                if (chkBx.Checked == true)
                {
                    int polineid = int.Parse((gdrow.FindControl("lbl_SPO_Pk") as Label).Text);
                    using (ArtEntitiesnew enty = new ArtEntitiesnew())
                    {

                        var q1 = from spo in enty.StockPOMasters where spo.SPO_Pk == polineid select spo;

                        foreach (var element in q1)
                        {
                            element.IsClosed  = "Y";
                            element.ClosedBy= Session["Username"].ToString().Trim();
                            element.ClosedDate= DateTime.Now;

                        }
                        enty.SaveChanges();
                    }
                    String msg = " SPO Closed Sucessfully ";

                    ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);

                }

            }



        }
    }
}