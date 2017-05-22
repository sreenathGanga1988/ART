using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace ArtWebApp.Reports.Production.Proddata
{
    public partial class ShippedReport : System.Web.UI.Page
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
                html.Append("<table  id='example' border = '2'>");

                //Building the Header row.
                html.Append(" <thead> <tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<th>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr></thead>");

                html.Append(" <thead class='filters'> <tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(column.ColumnName);
                    html.Append("</td>");
                }
                html.Append("</tr></thead>");


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
                MasterDiv.Controls.Add(new Literal { Text = html.ToString() });
            }
        }

        private DataTable GetData()
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT     BuyerMaster.BuyerName, AtcMaster.AtcNum, PoPackMaster.DeliveryDate, SUM(POPackDetails.PoQty) AS QTY, AtcDetails.FOB, 
                      AtcDetails.FOB * SUM(POPackDetails.PoQty) AS Value, ShipmentHandOverDetails.ShippedQty AS Shipped,
                      ShipmentHandOverDetails.ShippedQty * AtcDetails.FOB AS ShipValue, CountryMaster.Description
FROM         ShipmentHandOverDetails INNER JOIN
                      JobContractDetail ON ShipmentHandOverDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk INNER JOIN
                      BuyerMaster INNER JOIN
                      AtcMaster ON BuyerMaster.BuyerID = AtcMaster.Buyer_ID INNER JOIN
                      PoPackMaster ON AtcMaster.AtcId = PoPackMaster.AtcId INNER JOIN
                      POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
                      AtcDetails ON AtcMaster.AtcId = AtcDetails.AtcId AND POPackDetails.OurStyleID = AtcDetails.OurStyleID ON JobContractDetail.PoPackID = PoPackMaster.PoPackId AND
                       JobContractDetail.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                      JobContractMaster ON JobContractDetail.JobContract_pk = JobContractMaster.JobContract_pk AND AtcMaster.AtcId = JobContractMaster.AtcID INNER JOIN
                      LocationMaster ON JobContractMaster.Location_Pk = LocationMaster.Location_PK INNER JOIN
                      CountryMaster ON LocationMaster.CountryID = CountryMaster.CountryID
GROUP BY BuyerMaster.BuyerName, AtcMaster.AtcNum, PoPackMaster.DeliveryDate, AtcDetails.FOB, ShipmentHandOverDetails.ShippedQty, CountryMaster.Description"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
    }
}