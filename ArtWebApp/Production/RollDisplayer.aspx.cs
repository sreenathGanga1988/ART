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

namespace ArtWebApp.Production
{
    public partial class RollDisplayer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //filldata();
                //filldata1();
                //filldata2();
                //filldata3();
                //filldata4();

              //  fillPO();
              //  FillCosting();
              //  fillRO();
              //  fillSRO();
               // fillEBOM();
            }
        }



        public void fillPO()
        {
            //DataTable dt = GetDeliveredRollData();
            //filldataforAll(dt, MasterDiv);

        }
        public void FillCosting()
        {

            DataTable dt = GetCostingdata();
            filldataforAll(dt, MasterDiv2);
        }
        public void fillRO()
        {
            DataTable dt = GetRoData();
            filldataforAll(dt, MasterDiv3);

        }
        public void fillSRO()
        {

            DataTable dt = GetSRO();
            filldataforAll(dt, MasterDiv4);
        }
        public void fillEBOM()
        {
            DataTable dt = GetEBOMDATA();
            filldataforAll(dt, MasterDiv5);

        }

        private DataTable GetDeliveredRollData(int skudetpk,string skrinkagegroup, string WidthGroup, string markertype)
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT        CutOrderMaster.Cut_NO, CutOrderMaster.Color, DeliveryOrderMaster.DONum, FabricRollmaster.RollNum, FabricRollmaster.SupplierDoc_pk, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup, FabricRollmaster.MarkerType, FabricRollmaster.SkuDet_PK, FabricRollmaster.IsDelivered, RollInventoryMaster.IsPresent, RollInventoryMaster.Location_Pk, 
                         RollInventoryMaster.FactId, FabricRollmaster.AYard, LocationMaster.LocationName,FabricRollmaster.SupplierDoc_pk
FROM            DORollDetails INNER JOIN
                         CutOrderMaster ON DORollDetails.CutID = CutOrderMaster.CutID INNER JOIN
                         DeliveryOrderMaster ON DORollDetails.DO_PK = DeliveryOrderMaster.DO_PK INNER JOIN
                         FabricRollmaster ON DORollDetails.Roll_PK = FabricRollmaster.Roll_PK INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK INNER JOIN
                         LocationMaster ON RollInventoryMaster.Location_Pk = LocationMaster.Location_PK
WHERE       (FabricRollmaster.SkuDet_PK = @skudetpk) AND (FabricRollmaster.ShrinkageGroup = @skrinkagegroup) AND (FabricRollmaster.WidthGroup = @WidthGroup)
AND 
                         (FabricRollmaster.MarkerType = @markertype) AND (RollInventoryMaster.IsPresent = N'Y')"))


                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@skudetpk", skudetpk);
                    cmd.Parameters.AddWithValue("@skrinkagegroup", skrinkagegroup);
                    cmd.Parameters.AddWithValue("@WidthGroup", WidthGroup);
                    cmd.Parameters.AddWithValue("@markertype", markertype);
                    sda.SelectCommand = cmd;
                    

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        private DataTable GetCostingdata()
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT    AtcDetailApproval.OurStyleApproval_PK,  AtcDetails.OurStyleID, AtcDetails.OurStyle, AtcDetails.BuyerStyle, ISNULL((
SELECT      sum(  Quantity)
FROM            AtcDetailApproval
WHERE        (IsFirst = N'Y') AND (OurStyleID = AtcDetailApproval.OurStyleID)),0) AS IntialQty, AtcDetails.AtcId, AtcDetailApproval.Quantity, AtcDetails.FOB, AtcDetailApproval.IsForwarded, 
                         AtcDetailApproval.IsApproved
FROM            AtcDetails INNER JOIN
                         AtcDetailApproval ON AtcDetails.OurStyleID = AtcDetailApproval.OurStyleID
WHERE         (AtcDetailApproval.IsApproved = N'N')"))


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

        private DataTable GetRoData()
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT       RO_Pk,  RONum, FRMATC, TOATC,    (ISNULL( FRMTEMP,'')+''+ISNULL( FRMCOMP,'')+''+ISNULL( FRMCONS,'')+''+ISNULL( FRMWEIG,'')+''+ISNULL( FRMITEMCOLOR,'')+''+ISNULL( FRMSUPPCOLOR,'')+''+ISNULL( FRMITEMSIZE,'')+''+ISNULL( FRMSUPPSIZE,'')) AS DESCRIPTION, Qty,  (QTY*RATE)AS POVALUE, UOM,  
                         LocationName, LocationAddress, IsForwarded
FROM            (SELECT        RequestOrderMaster.RONum, AtcMaster.AtcNum AS FRMATC, AtcMaster_1.AtcNum AS TOATC, Template_Master.Description AS TOTEMP, Template_Master_1.Description AS FRMTEMP, RequestOrderDetails.Qty, 
                         SkuRawMaterialMaster.Composition AS FRMCOMP, SkuRawMaterialMaster.Construction AS FRMCONS, SkuRawMaterialMaster.Weight AS FRMWEIG, SkuRawMaterialMaster.Width AS FROMWID, 
                         SkuRawmaterialDetail.ItemColor AS FRMITEMCOLOR, SkuRawmaterialDetail.SupplierColor AS FRMSUPPCOLOR, SkuRawmaterialDetail.ItemSize AS FRMITEMSIZE, 
                         SkuRawmaterialDetail.SupplierSize AS FRMSUPPSIZE, RequestOrderDetails.CUnitPrice AS RATE, UOMMaster.UomName AS UOM, SkuRawMaterialMaster_1.Composition, SkuRawMaterialMaster_1.Construction, 
                         LocationMaster.LocationName, LocationMaster.LocationAddress, RequestOrderMaster.RO_Pk, RequestOrderMaster.IsApproved, RequestOrderMaster.IsForwarded
FROM            SkuRawmaterialDetail INNER JOIN
                         RequestOrderMaster INNER JOIN
                         RequestOrderDetails ON RequestOrderMaster.RO_Pk = RequestOrderDetails.RO_Pk ON SkuRawmaterialDetail.SkuDet_PK = RequestOrderDetails.FromSkuDet_PK INNER JOIN
                         SkuRawmaterialDetail AS SkuRawmaterialDetail_1 ON RequestOrderDetails.ToSkuDet_PK = SkuRawmaterialDetail_1.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         SkuRawMaterialMaster AS SkuRawMaterialMaster_1 ON SkuRawmaterialDetail_1.Sku_PK = SkuRawMaterialMaster_1.Sku_Pk INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         AtcMaster AS AtcMaster_1 ON SkuRawMaterialMaster_1.Atc_id = AtcMaster_1.AtcId INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         Template_Master AS Template_Master_1 ON SkuRawMaterialMaster_1.Template_pk = Template_Master_1.Template_PK INNER JOIN
                         InventoryMaster ON RequestOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         UOMMaster ON InventoryMaster.Uom_Pk = UOMMaster.Uom_PK
GROUP BY RequestOrderMaster.RONum, RequestOrderMaster.CreatedDate, RequestOrderMaster.AddedBy, AtcMaster.AtcNum, AtcMaster_1.AtcNum, Template_Master.Description, Template_Master_1.Description, 
                         RequestOrderDetails.Qty, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Construction, SkuRawMaterialMaster.Weight, SkuRawMaterialMaster.Width, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize, RequestOrderDetails.CUnitPrice, UOMMaster.UomName, SkuRawMaterialMaster_1.Composition, 
                         SkuRawMaterialMaster_1.Construction, SkuRawMaterialMaster_1.Weight, SkuRawMaterialMaster_1.Width, SkuRawmaterialDetail_1.ItemColor, SkuRawmaterialDetail_1.SupplierColor, 
                         SkuRawmaterialDetail_1.ItemSize, SkuRawmaterialDetail_1.SupplierSize, LocationMaster.LocationName, LocationMaster.LocationAddress, RequestOrderMaster.RO_Pk, RequestOrderMaster.IsApproved, 
                         RequestOrderMaster.IsForwarded
                          HAVING         (RequestOrderMaster.IsApproved = N'N')) AS TT"))


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
        private DataTable GetSRO()
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT        RequestOrderStockMaster.SRO_Pk, RequestOrderStockMaster.RONum, ISNULL(SkuRawMaterialMaster.RMNum, '') + ' ' + ISNULL(SkuRawMaterialMaster.Composition, '') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Construction, '') + ' ' + ISNULL(SkuRawmaterialDetail.SupplierColor, '') + ' ' + ISNULL(SkuRawmaterialDetail.SupplierSize, '') AS dESCRIPTION, RequestOrderStockDetails.Qty, 
                         RequestOrderStockDetails.Qty * RequestOrderStockDetails.CUnitPrice AS Povalue, LocationMaster.LocationName + '  GStock ' AS fromLocation, RequestOrderStockDetails.ToSkuDet_PK, 
                         RequestOrderStockMaster.IsForwarded, RequestOrderStockMaster.IsApproved
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         RequestOrderStockDetails ON SkuRawmaterialDetail.SkuDet_PK = RequestOrderStockDetails.ToSkuDet_PK INNER JOIN
                         StockInventoryMaster ON RequestOrderStockDetails.SInventoryItem_PK = StockInventoryMaster.SInventoryItem_PK INNER JOIN
                         LocationMaster ON StockInventoryMaster.Location_Pk = LocationMaster.Location_PK INNER JOIN
                         RequestOrderStockMaster ON RequestOrderStockDetails.SRO_Pk = RequestOrderStockMaster.SRO_Pk
WHERE        (RequestOrderStockMaster.IsApproved = 'N')"))


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
        private DataTable GetEBOMDATA()
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT        ExtraBOM_PK, Reqnum, AtcNum, MerchandiserName, Explanation, SUM(ExtraValue) AS ExtraValue, 'USD' as Currency, AddedBY, AddedDate, Isforwarded
FROM            (SELECT        ExtraBOMRequestMaster.ExtraBOM_PK, ExtraBOMRequestMaster.Reqnum, ExtraBOMRequestMaster.AddedBY, ExtraBOMRequestMaster.AddedDate, ExtraBOMRequestMaster.MerchandiserName, 
                                                    ExtraBOMRequestMaster.Explanation, ExtraBOMRequestMaster.IsApproved, ExtraBOMRequestDetail.Skudet_PK, ExtraBOMRequestMaster.Isforwarded, SkuRawmaterialDetail.UnitRate, 
                                                    ExtraBOMRequestDetail.Qty * SkuRawmaterialDetail.UnitRate AS ExtraValue, AtcMaster.AtcNum
                          FROM            ExtraBOMRequestMaster INNER JOIN
                                                    ExtraBOMRequestDetail ON ExtraBOMRequestMaster.ExtraBOM_PK = ExtraBOMRequestDetail.ExtraBOM_PK INNER JOIN
                                                    SkuRawmaterialDetail ON ExtraBOMRequestDetail.Skudet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                                                    SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                                                    AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId
                          WHERE        (ExtraBOMRequestMaster.IsApproved = N'N')) AS tt
GROUP BY ExtraBOM_PK, Reqnum, AtcNum, MerchandiserName, Explanation, AddedBY, AddedDate, Isforwarded"))


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


        ////protected void Button1_Click(object sender, EventArgs e)
        ////{
        ////    filldata();
        ////}


        //public void filldata()
        //{

        //    //Populating a DataTable from database.
        //    DataTable dt = this.GetData();

        //    //Building an HTML string.
        //    StringBuilder html = new StringBuilder();

        //    //Table start.
        //    html.Append("<table   class='Sree' border = '2'>");

        //    //Building the Header row.
        //    html.Append(" <thead> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<th>");
        //        html.Append(column.ColumnName);
        //        html.Append("</th>");
        //    }
        //    html.Append("</tr></thead>");

        //    html.Append(" <thead class='filters'> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<td>");
        //        html.Append(column.ColumnName);
        //        html.Append("</td>");
        //    }
        //    html.Append("</tr></thead>");


        //    html.Append(" <tfoot> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<th>");
        //        html.Append(column.ColumnName);
        //        html.Append("</th>");
        //    }
        //    html.Append("</tr></tfoot>");






        //    //Building the Data rows.
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        html.Append("<tr>");
        //        foreach (DataColumn column in dt.Columns)
        //        {
        //            html.Append("<td>");
        //            html.Append(row[column.ColumnName]);
        //            html.Append("</td>");
        //        }
        //        html.Append("</tr>");
        //    }



        //    //Table end.
        //    html.Append("</table>");

        //    //Append the HTML string to Placeholder.
        //    MasterDiv.Controls.Add(new Literal { Text = html.ToString() });

        //}
        //public void filldata1()
        //{

        //    //Populating a DataTable from database.
        //    DataTable dt = this.GetData1();

        //    //Building an HTML string.
        //    StringBuilder html = new StringBuilder();

        //    //Table start.
        //    html.Append("<table  class='Sree' border = '2'>");

        //    //Building the Header row.
        //    html.Append(" <thead> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<th>");
        //        html.Append(column.ColumnName);
        //        html.Append("</th>");
        //    }
        //    html.Append("</tr></thead>");

        //    html.Append(" <thead class='filters'> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<td>");
        //        html.Append(column.ColumnName);
        //        html.Append("</td>");
        //    }
        //    html.Append("</tr></thead>");


        //    html.Append(" <tfoot> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<th>");
        //        html.Append(column.ColumnName);
        //        html.Append("</th>");
        //    }
        //    html.Append("</tr></tfoot>");






        //    //Building the Data rows.
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        html.Append("<tr>");
        //        foreach (DataColumn column in dt.Columns)
        //        {
        //            html.Append("<td>");
        //            html.Append(row[column.ColumnName]);
        //            html.Append("</td>");
        //        }
        //        html.Append("</tr>");
        //    }




        //    //Table end.
        //    html.Append("</table>");

        //    //Append the HTML string to Placeholder.
        //    MasterDiv2.Controls.Add(new Literal { Text = html.ToString() });

        //}
        //public void filldata2()
        //{

        //    //Populating a DataTable from database.
        //    DataTable dt = this.GetData2();

        //    //Building an HTML string.
        //    StringBuilder html = new StringBuilder();

        //    //Table start.
        //    html.Append("<table  id='example' class='Sree' border = '2'>");

        //    //Building the Header row.
        //    html.Append(" <thead> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<th>");
        //        html.Append(column.ColumnName);
        //        html.Append("</th>");
        //    }
        //    html.Append("</tr></thead>");

        //    html.Append(" <thead class='filters'> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<td>");
        //        html.Append(column.ColumnName);
        //        html.Append("</td>");
        //    }
        //    html.Append("</tr></thead>");


        //    html.Append(" <tfoot> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<th>");
        //        html.Append(column.ColumnName);
        //        html.Append("</th>");
        //    }
        //    html.Append("</tr></tfoot>");






        //    //Building the Data rows.
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        html.Append("<tr>");
        //        foreach (DataColumn column in dt.Columns)
        //        {
        //            html.Append("<td>");
        //            html.Append(row[column.ColumnName]);
        //            html.Append("</td>");
        //        }
        //        html.Append("</tr>");
        //    }




        //    //Table end.
        //    html.Append("</table>");

        //    //Append the HTML string to Placeholder.
        //    MasterDiv3.Controls.Add(new Literal { Text = html.ToString() });

        //}

        //public void filldata3()
        //{

        //    //Populating a DataTable from database.
        //    DataTable dt = this.GetData3();

        //    //Building an HTML string.
        //    StringBuilder html = new StringBuilder();

        //    //Table start.
        //    html.Append("<table  id='example' class='Sree' border = '2'>");

        //    //Building the Header row.
        //    html.Append(" <thead> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<th>");
        //        html.Append(column.ColumnName);
        //        html.Append("</th>");
        //    }
        //    html.Append("</tr></thead>");

        //    html.Append(" <thead class='filters'> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<td>");
        //        html.Append(column.ColumnName);
        //        html.Append("</td>");
        //    }
        //    html.Append("</tr></thead>");


        //    html.Append(" <tfoot> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<th>");
        //        html.Append(column.ColumnName);
        //        html.Append("</th>");
        //    }
        //    html.Append("</tr></tfoot>");






        //    //Building the Data rows.
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        html.Append("<tr>");
        //        foreach (DataColumn column in dt.Columns)
        //        {
        //            html.Append("<td>");
        //            html.Append(row[column.ColumnName]);
        //            html.Append("</td>");
        //        }
        //        html.Append("</tr>");
        //    }




        //    //Table end.
        //    html.Append("</table>");

        //    //Append the HTML string to Placeholder.
        //    MasterDiv4.Controls.Add(new Literal { Text = html.ToString() });

        //}

        //public void filldata4()
        //{

        //    //Populating a DataTable from database.
        //    DataTable dt = this.GetData4();

        //    //Building an HTML string.
        //    StringBuilder html = new StringBuilder();

        //    //Table start.
        //    html.Append("<table  id='example' class='Sree' border = '2'>");

        //    //Building the Header row.
        //    html.Append(" <thead> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<th>");
        //        html.Append(column.ColumnName);
        //        html.Append("</th>");
        //    }
        //    html.Append("</tr></thead>");

        //    html.Append(" <thead class='filters'> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<td>");
        //        html.Append(column.ColumnName);
        //        html.Append("</td>");
        //    }
        //    html.Append("</tr></thead>");


        //    html.Append(" <tfoot> <tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<th>");
        //        html.Append(column.ColumnName);
        //        html.Append("</th>");
        //    }
        //    html.Append("</tr></tfoot>");






        //    //Building the Data rows.
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        html.Append("<tr>");
        //        foreach (DataColumn column in dt.Columns)
        //        {
        //            html.Append("<td>");
        //            html.Append(row[column.ColumnName]);
        //            html.Append("</td>");
        //        }
        //        html.Append("</tr>");
        //    }




        //    //Table end.
        //    html.Append("</table>");

        //    //Append the HTML string to Placeholder.
        //    MasterDiv5.Controls.Add(new Literal { Text = html.ToString() });

        //}





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



            //Table end.
            html.Append("</table>");

            //Append the HTML string to Placeholder.
            mydiv.Controls.Add(new Literal { Text = html.ToString() });

        }





    }
}
