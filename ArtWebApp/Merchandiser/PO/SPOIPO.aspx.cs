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
    public partial class SPOIPO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                filldata();
            }
        }
        private DataTable GetData()
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT        tt.SPODetails_PK, tt.SPO_Pk, tt.SPONum, tt.SupplierName, tt.CurrencyCode, tt.Description, tt.Remark, tt.POQty, tt.ReceivedQty, tt.UomName, tt.AddedDate, 
                         tt.IsApproved, ODOOGPOMaster.PONum, ODOOGPOMaster.OdooLocation
FROM            ODOOGPOMaster INNER JOIN
                         StocPOForODOO ON ODOOGPOMaster.POId = StocPOForODOO.POId AND ODOOGPOMaster.POLineID = StocPOForODOO.POLineID RIGHT OUTER JOIN
                             (SELECT        StockPODetails.SPODetails_PK, StockPOMaster.SPO_Pk, StockPOMaster.SPONum, SupplierMaster.SupplierName, CurrencyMaster.CurrencyCode, 
                                                         ISNULL(Template_Master.Description, '') + ' ' + ISNULL(StockPODetails.Composition, '') + ' ' + ISNULL(StockPODetails.Construct, '') 
                                                         + ' ' + ISNULL(StockPODetails.TemplateColor, '') + ' ' + ISNULL(StockPODetails.TemplateSize, '') + ' ' + ISNULL(StockPODetails.TemplateWidth, '') 
                                                         + ' ' + ISNULL(StockPODetails.TemplateWeight, '') AS Description, StockPOMaster.Remark, StockPODetails.POQty, 
                                                         SUM(StockMRNDetails.ReceivedQty) AS ReceivedQty, UOMMaster.UomName, StockPOMaster.AddedDate, StockPOMaster.IsApproved
                               FROM            StockPOMaster INNER JOIN
                                                         StockPODetails ON StockPOMaster.SPO_Pk = StockPODetails.SPO_PK INNER JOIN
                                                         CurrencyMaster ON StockPOMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                                                         UOMMaster ON StockPODetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                                                         Template_Master ON StockPODetails.Template_PK = Template_Master.Template_PK INNER JOIN
                                                         SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK LEFT OUTER JOIN
                                                         StockMRNDetails ON StockPODetails.SPODetails_PK = StockMRNDetails.SPODetails_PK
                               GROUP BY CurrencyMaster.CurrencyCode, StockPOMaster.SPONum, UOMMaster.UomName, StockPOMaster.AddedDate, StockPOMaster.Remark, 
                                                         StockPOMaster.IsApproved, ISNULL(Template_Master.Description, '') + ' ' + ISNULL(StockPODetails.Composition, '') 
                                                         + ' ' + ISNULL(StockPODetails.Construct, '') + ' ' + ISNULL(StockPODetails.TemplateColor, '') + ' ' + ISNULL(StockPODetails.TemplateSize, '') 
                                                         + ' ' + ISNULL(StockPODetails.TemplateWidth, '') + ' ' + ISNULL(StockPODetails.TemplateWeight, ''), StockPODetails.POQty, StockPOMaster.SPO_Pk, 
                                                         SupplierMaster.SupplierName, StockPODetails.SPODetails_PK) AS tt ON StocPOForODOO.Spo_PK = tt.SPO_Pk AND 
                         StocPOForODOO.SPoDet_PK = tt.SPODetails_PK WHERE        (tt.SPODetails_PK = 4520)"))


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
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            filldata();
        }
        public void filldata()
        {

            //Populating a DataTable from database.
            DataTable dt = this.GetData();

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            html.Append("<table  id='example' class='example' border = '2'>");

            //Building the Header row.
            html.Append(" <thead> <tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr></thead>");

         








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
            MasterDiv.Controls.Add(new Literal { Text = html.ToString() });

        }
    }
}