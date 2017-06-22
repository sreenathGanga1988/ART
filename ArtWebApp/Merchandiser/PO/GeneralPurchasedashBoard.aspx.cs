using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

                int polineid = int.Parse((gdrow.FindControl("lbl_polineid") as Label).Text);

                if (selectedpoid == "")
                {
                    selectedpoid = selectedpoid + polineid.ToString ();
                }
                else
                {
                    selectedpoid = selectedpoid + "," + polineid.ToString();
                }

            }
            Response.Redirect("IPOMultiCreator.aspx?selectionid="+selectedpoid+"");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            ExportGridviewtoExcel(tbl_pendingtoreceive, Response);
        }

        public static void ExportGridviewtoExcel(GridView gridviewtabel, HttpResponse Response)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gridviewtabel.AllowPaging = false;


                gridviewtabel.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in gridviewtabel.HeaderRow.Cells)
                {
                    cell.BackColor = gridviewtabel.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gridviewtabel.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gridviewtabel.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gridviewtabel.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gridviewtabel.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }
}