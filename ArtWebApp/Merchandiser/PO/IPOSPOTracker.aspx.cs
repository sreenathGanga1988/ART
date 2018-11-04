﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

namespace ArtWebApp.Merchandiser.PO
{
    public partial class IPOSPOTracker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
            //  filldata();
            }
        }
        private DataTable GetData()
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT        tt.SPODetails_PK, tt.SPO_Pk, tt.SPONum, tt.SupplierName, tt.Unitprice, tt.CurrencyCode, tt.Description, tt.Remark, tt.POQty, tt.ReceivedQty, tt.UomName, FORMAT(tt.AddedDate, 'dd/MMM/yyyy', 'en-us') AS AddedDate, 
                         tt.IsApproved, ODOOGPOMaster.PONum, ODOOGPOMaster.OdooLocation, tt.CUrate, tt.AddedBy
FROM            ODOOGPOMaster INNER JOIN
                         StocPOForODOO ON ODOOGPOMaster.POId = StocPOForODOO.POId AND ODOOGPOMaster.POLineID = StocPOForODOO.POLineID RIGHT OUTER JOIN
                             (SELECT        StockPODetails.SPODetails_PK, StockPOMaster.SPO_Pk, StockPOMaster.SPONum, SupplierMaster.SupplierName, CurrencyMaster.CurrencyCode, ISNULL(Template_Master.Description, '') 
                                                         + ' ' + ISNULL(StockPODetails.Composition, '') + ' ' + ISNULL(StockPODetails.Construct, '') + ' ' + ISNULL(StockPODetails.TemplateColor, '') + ' ' + ISNULL(StockPODetails.TemplateSize, '') 
                                                         + ' ' + ISNULL(StockPODetails.TemplateWidth, '') + ' ' + ISNULL(StockPODetails.TemplateWeight, '') AS Description, StockPOMaster.Remark, StockPODetails.POQty, SUM(StockMRNDetails.ReceivedQty) 
                                                         AS ReceivedQty, UOMMaster.UomName, StockPOMaster.AddedDate, StockPOMaster.IsApproved, StockPODetails.CUrate, StockPODetails.Unitprice, StockPOMaster.AddedBy
                               FROM            StockPOMaster INNER JOIN
                                                         StockPODetails ON StockPOMaster.SPO_Pk = StockPODetails.SPO_PK INNER JOIN
                                                         CurrencyMaster ON StockPOMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                                                         UOMMaster ON StockPODetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                                                         Template_Master ON StockPODetails.Template_PK = Template_Master.Template_PK INNER JOIN
                                                         SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK LEFT OUTER JOIN
                                                         StockMRNDetails ON StockPODetails.SPODetails_PK = StockMRNDetails.SPODetails_PK
                               GROUP BY CurrencyMaster.CurrencyCode, StockPOMaster.SPONum, UOMMaster.UomName, StockPOMaster.AddedDate, StockPOMaster.Remark, StockPOMaster.IsApproved, ISNULL(Template_Master.Description, '') 
                                                         + ' ' + ISNULL(StockPODetails.Composition, '') + ' ' + ISNULL(StockPODetails.Construct, '') + ' ' + ISNULL(StockPODetails.TemplateColor, '') + ' ' + ISNULL(StockPODetails.TemplateSize, '') 
                                                         + ' ' + ISNULL(StockPODetails.TemplateWidth, '') + ' ' + ISNULL(StockPODetails.TemplateWeight, ''), StockPODetails.POQty, StockPOMaster.SPO_Pk, SupplierMaster.SupplierName, 
                                                         StockPODetails.SPODetails_PK, StockPODetails.CUrate, StockPODetails.Unitprice, StockPOMaster.AddedBy) AS tt ON StocPOForODOO.Spo_PK = tt.SPO_Pk AND StocPOForODOO.SPoDet_PK = tt.SPODetails_PK "))









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

        private DataTable GetDataOFYear(ArrayList Popackdetlist)
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;

            DataTable dt = new DataTable();
            string condition = "where";

            for (int i = 0; i < Popackdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " (DATEPART(yy, StockPOMaster.AddedDate) = "+ Popackdetlist[i].ToString().Trim() + ") " ;
                }
                else
                {
                    condition = condition + "  or  (DATEPART(yy, StockPOMaster.AddedDate) = " + Popackdetlist[i].ToString().Trim() + ") ";
                }



            }

            if (condition != "where")
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(@"SELECT        tt.SPODetails_PK, tt.SPO_Pk, tt.SPONum, tt.SupplierName, tt.Unitprice, tt.CurrencyCode, tt.Description, tt.Remark, tt.POQty, tt.ReceivedQty, tt.UomName, FORMAT(tt.AddedDate, 'dd/MMM/yyyy', 'en-us') AS AddedDate, 
                         tt.IsApproved, ODOOGPOMaster.PONum, ODOOGPOMaster.OdooLocation, tt.CUrate, tt.AddedBy,tt.MRNDetails,tt.Sales_DO
FROM            ODOOGPOMaster INNER JOIN
                         StocPOForODOO ON ODOOGPOMaster.POId = StocPOForODOO.POId AND ODOOGPOMaster.POLineID = StocPOForODOO.POLineID RIGHT OUTER JOIN
                             (SELECT        StockPODetails.SPODetails_PK, StockPOMaster.SPO_Pk, StockPOMaster.SPONum, SupplierMaster.SupplierName, CurrencyMaster.CurrencyCode, ISNULL(Template_Master.Description, '') 
                                                         + ' ' + ISNULL(StockPODetails.Composition, '') + ' ' + ISNULL(StockPODetails.Construct, '') + ' ' + ISNULL(StockPODetails.TemplateColor, '') + ' ' + ISNULL(StockPODetails.TemplateSize, '') 
                                                         + ' ' + ISNULL(StockPODetails.TemplateWidth, '') + ' ' + ISNULL(StockPODetails.TemplateWeight, '') AS Description, StockPOMaster.Remark, StockPODetails.POQty, SUM(StockMRNDetails.ReceivedQty) 
                                                         AS ReceivedQty, UOMMaster.UomName, StockPOMaster.AddedDate, StockPOMaster.IsApproved, StockPODetails.CUrate, StockPODetails.Unitprice, StockPOMaster.AddedBy,
														 
(select STUFF ((select ',' + mrn from (
SELECT        (StockMrnMaster.SMrnNum +'('+convert(varchar,(sum(StockMRNDetails.ReceivedQty)+ sum(StockMRNDetails.ExtraQty)))+')') as mrn
FROM            StockMrnMaster INNER JOIN
                         StockMRNDetails ON StockMrnMaster.SMrn_PK = StockMRNDetails.SMRN_Pk
WHERE        (StockMRNDetails.SPODetails_PK = StockPODetails.SPODetails_PK) group by StockMrnMaster.SMrnNum)as tt for xml path('')),1,1,'')as txt) as MRNDetails,
(select STUFF ((select ',' + Sales_DO from (
SELECT        (InventorySalesMaster.SalesDONum+'( '+ convert(varchar, (sum(InventorySalesDetail.DeliveryQty)))+')') as Sales_DO
FROM            InventorySalesMaster INNER JOIN
                         InventorySalesDetail ON InventorySalesMaster.SalesDO_PK = InventorySalesDetail.SalesDO_PK INNER JOIN
                         StockInventoryMaster ON InventorySalesDetail.SInventoryItem_PK = StockInventoryMaster.SInventoryItem_PK INNER JOIN
                         StockMRNDetails ON StockInventoryMaster.SMRNDet_Pk = StockMRNDetails.SMRNDet_Pk
WHERE        (StockMRNDetails.SPODetails_PK = StockPODetails.SPODetails_PK) group by InventorySalesMaster.SalesDONum)as tt for xml path('')),1,1,'')as txt) as Sales_DO
                               FROM            StockPOMaster INNER JOIN
                                                         StockPODetails ON StockPOMaster.SPO_Pk = StockPODetails.SPO_PK INNER JOIN
                                                         CurrencyMaster ON StockPOMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                                                         UOMMaster ON StockPODetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                                                         Template_Master ON StockPODetails.Template_PK = Template_Master.Template_PK INNER JOIN
                                                         SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK LEFT OUTER JOIN
                                                         StockMRNDetails ON StockPODetails.SPODetails_PK = StockMRNDetails.SPODetails_PK
                               GROUP BY CurrencyMaster.CurrencyCode, StockPOMaster.SPONum, UOMMaster.UomName, StockPOMaster.AddedDate, StockPOMaster.Remark, StockPOMaster.IsApproved, ISNULL(Template_Master.Description, '') 
                                                         + ' ' + ISNULL(StockPODetails.Composition, '') + ' ' + ISNULL(StockPODetails.Construct, '') + ' ' + ISNULL(StockPODetails.TemplateColor, '') + ' ' + ISNULL(StockPODetails.TemplateSize, '') 
                                                         + ' ' + ISNULL(StockPODetails.TemplateWidth, '') + ' ' + ISNULL(StockPODetails.TemplateWeight, ''), StockPODetails.POQty, StockPOMaster.SPO_Pk, SupplierMaster.SupplierName, 
                                                         StockPODetails.SPODetails_PK, StockPODetails.CUrate, StockPODetails.Unitprice, StockPOMaster.AddedBy) AS tt ON StocPOForODOO.Spo_PK = tt.SPO_Pk AND StocPOForODOO.SPoDet_PK = tt.SPODetails_PK
group by  tt.SPODetails_PK, tt.SPO_Pk, tt.SPONum, tt.SupplierName, tt.Unitprice, tt.CurrencyCode, tt.Description, tt.Remark, tt.POQty, tt.ReceivedQty, tt.UomName,  AddedDate, 
tt.IsApproved, ODOOGPOMaster.PONum, ODOOGPOMaster.OdooLocation, tt.CUrate, tt.AddedBy,tt.MRNDetails,tt.Sales_DO"))
                          

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;

                        cmd.CommandTimeout = 0;
                        sda.SelectCommand = cmd;

                       
                            sda.Fill(dt);
                            
                       
                    }
                }
            }



            return dt;

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
            html.Append("<table  id='example' class='example mydatagrid' border = '2'>");

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

        public void filldataofyear(ArrayList Popackdetlis)
        {

            //Populating a DataTable from database.
            DataTable dt = this.GetDataOFYear(Popackdetlis);

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            html.Append("<table  id='example' class='example mydatagrid' border = '2'>");

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

        protected void ShoIPOTracker_Click(object sender, EventArgs e)
        {
            ArrayList popaklist = new ArrayList();
            for (int i = 0; i < ListBox1.Items.Count; i++)
            {
                if (ListBox1.Items[i].Selected)
                {
                  
                    int popackid = int.Parse(ListBox1.Items[i].Text);
                    popaklist.Add(popackid);
                }
            }

            if (popaklist.Count > 0 && popaklist != null)
            {
                filldataofyear(popaklist);

               
            }
        }
    }
}