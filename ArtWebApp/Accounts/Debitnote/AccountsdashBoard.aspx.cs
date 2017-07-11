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

namespace ArtWebApp.Accounts.Debitnote
{
    public partial class AccountsdashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                fillSPODataForInvoinvg();
            }
        }

        public void fillSPODataForInvoinvg()
        {
            DataTable dt = GetPOData();
            filldataforAll(dt, SPODIV);

        }

        public void filldataforAll(DataTable dt, System.Web.UI.HtmlControls.HtmlGenericControl mydiv)
        {

            //Populating a DataTable from database.


            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            html.Append("<table   class='Sree' border = '2'>");

            //Building the Header row.
            html.Append(" <thead> <tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
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



            //Table end.
            html.Append("</table>");

            //Append the HTML string to Placeholder.
            mydiv.Controls.Add(new Literal { Text = html.ToString() });

        }






        private DataTable GetPOData()
        {

            String Qry = @"SELECT        SPO_Pk, SupplierName, SPONum, POQty, ReceivedQty, ExtraQty, MRNDate, InvoicedQty, ReceivedQty - InvoicedQty AS BancetoInvoice
FROM            (SELECT        StockPOMaster.SPO_Pk, StockPOMaster.SPONum, ISNULL(SUM(StockPODetails_1.POQty), 0) AS POQty, ISNULL(SUM(StockMRNDetails.ReceivedQty), 0) AS ReceivedQty, 
                                                    SupplierMaster.SupplierName, MAX(StockMrnMaster.AddedDate) AS MRNDate, ISNULL(SUM(StockMRNDetails.ExtraQty), 0) AS ExtraQty, ISNULL
                                                        ((SELECT        SUM(SupplierStockInvoiceDetail.InvoiceQty) AS Expr1
                                                            FROM            StockPODetails INNER JOIN
                                                                                     SupplierStockInvoiceDetail ON StockPODetails.SPODetails_PK = SupplierStockInvoiceDetail.SPODetails_PK AND 
                                                                                     StockPODetails.SPODetails_PK = SupplierStockInvoiceDetail.SPODetails_PK
                                                            GROUP BY StockPODetails.SPO_PK
                                                            HAVING        (StockPODetails.SPO_PK = StockPOMaster.SPO_Pk)), 0) AS InvoicedQty
                          FROM            StockPOMaster INNER JOIN
                                                    StockPODetails AS StockPODetails_1 ON StockPOMaster.SPO_Pk = StockPODetails_1.SPO_PK INNER JOIN
                                                    StockMRNDetails ON StockPOMaster.SPO_Pk = StockMRNDetails.SPO_PK INNER JOIN
                                                    StockMrnMaster ON StockPOMaster.SPO_Pk = StockMrnMaster.SPo_PK INNER JOIN
                                                    SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK
                          GROUP BY StockPOMaster.SPO_Pk, StockPOMaster.SPONum, SupplierMaster.SupplierName) AS tt
WHERE        (ReceivedQty - InvoicedQty > 0)";



            return QueryFunctions.ReturnQueryResultDatatable(Qry);
        }







    }
}