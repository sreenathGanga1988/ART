using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.Production
{
    public partial class CutplanHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //Populating a DataTable from database.
                DataTable dt = this.GetData();

                //Building an HTML string.
                StringBuilder html = new StringBuilder();

                //Table start.
                html.Append("<table  id='example' class='example' border = '1'>");

                //Building the Header row.
                html.Append(" <thead> <tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<th>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr></thead>");

                //html.Append(" <thead> <tr>");
                //foreach (DataColumn column in dt.Columns)
                //{
                //    html.Append("<td>");
                //    html.Append(column.ColumnName);
                //    html.Append("</td>");
                //}
                //html.Append("</tr></thead>");


                html.Append(" <tfoot> <tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<th>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr></tfoot>");


                //Building the Data rows.
                foreach (DataRow row in dt.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        html.Append("<td>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }

                //Table end.
                html.Append("</table>");

                //Append the HTML string to Placeholder.
                wholediv.Controls.Add(new Literal { Text = html.ToString() });
            }
        }



        private DataTable GetData()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "CutplanHistory_SP";
                cmd.Parameters.AddWithValue("@atcid", 145);
                cmd.CommandType = CommandType.StoredProcedure;

           DataTable dt=     ArtWebApp.QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
                return dt;
            }


           

        }
    }
}