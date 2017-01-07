using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Approvals
{
    public partial class ResponsiveApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        ////    Attribute to show the Plus Minus Button.
        //    GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //    GridView1.HeaderRow.Cells[1].Attributes["data-class"] = "expand";
        //    GridView1.HeaderRow.Cells[2].Attributes["data-class"] = "expand";
        //    GridView1.HeaderRow.Cells[3].Attributes["data-class"] = "expand";
        //    GridView1.HeaderRow.Cells[4].Attributes["data-class"] = "expand";
        //    GridView1.HeaderRow.Cells[5].Attributes["data-class"] = "expand";
        //    GridView1.HeaderRow.Cells[6].Attributes["data-class"] = "expand";
        //    GridView1.HeaderRow.Cells[7].Attributes["data-class"] = "expand";
        //    GridView1.HeaderRow.Cells[8].Attributes["data-class"] = "expand";
        //    GridView1.HeaderRow.Cells[9].Attributes["data-class"] = "expand";
        //    GridView1.HeaderRow.Cells[10].Attributes["data-class"] = "expand";

        //    //Attribute to hide column in Phone.
        //    GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
        //    GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
        //    GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
        //    GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
        //    GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
        //    GridView1.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
        //    GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
        //    GridView1.HeaderRow.Cells[7].Attributes["data-hide"] = "phone";
        //    GridView1.HeaderRow.Cells[8].Attributes["data-hide"] = "phone";
        //    GridView1.HeaderRow.Cells[9].Attributes["data-hide"] = "phone";
        //    GridView1.HeaderRow.Cells[10].Attributes["data-hide"] = "phone";
        //    //Adds THEAD and TBODY to GridView.
        //    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btn_approveAll_Click(object sender, EventArgs e)
        {
            string pascode = txt_Appcode.Text;
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string pascode = txt_Appcode.Text;

        }
    }
}