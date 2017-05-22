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

namespace ArtWebApp.Production.CutOrder
{
    public partial class DisplayRoll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                String skudetpk = Request.QueryString["skudetpk"];
                String shrinkage = Request.QueryString["shrinkage"];
                String width = Request.QueryString["width"];
                String markertype = Request.QueryString["markertype"];
                String factid = Request.QueryString["factid"];
                int warehouseid= int.Parse(Session["UserLoc_pk"].ToString());
                fillDeliveredRoll(int.Parse(skudetpk), shrinkage, width, markertype, int.Parse(factid));
                FillavailableRoll(int.Parse(skudetpk), shrinkage, width, markertype, warehouseid);
                //filldata();
                //filldata1();
                //filldata2();
                //filldata3();
                //filldata4();

                //  fillPO();
                //  FillCosting();
                fillCutplanofColor(int.Parse(skudetpk));
                 FillCutorderofColor(int.Parse(skudetpk)); ;
                // fillEBOM();
            }
        }



        public void fillDeliveredRoll(int skudetpk, string shrinkage, string width, string markertype,int factid)
        {
            DataTable dt = GetDeliveredRollData(skudetpk, shrinkage, width, markertype, factid); 
            filldataforAll(dt, MasterDiv);

        }
        public void FillavailableRoll(int skudetpk, string shrinkage, string width, string markertype, int wareshouseid)
        {

            DataTable dt = GetAvailbleRollData(skudetpk, shrinkage, width, markertype, wareshouseid);
            filldataforAll(dt, MasterDiv2);
        }
        public void fillCutplanofColor(int skudetpk)
        {
            DataTable dt = getCutPlanofSKU(skudetpk);
            filldataforAll(dt, MasterDiv3);

        }
        public void FillCutorderofColor(int skudetpk)
        {

            DataTable dt = GetCutorder(skudetpk);
            filldataforAll(dt, MasterDiv4);
        }
        public void fillEBOM()
        {
            DataTable dt = GetEBOMDATA();
            filldataforAll(dt, MasterDiv5);

        }

        private DataTable GetDeliveredRollData(int skudetpk, string skrinkagegroup, string WidthGroup, string markertype,int factid)
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
                         (FabricRollmaster.MarkerType = @markertype) AND (RollInventoryMaster.IsPresent = N'Y') AND (RollInventoryMaster.Location_Pk = @factid) AND (FabricRollmaster.IsDelivered = N'Y')"))


                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@skudetpk", skudetpk);
                    cmd.Parameters.AddWithValue("@skrinkagegroup", skrinkagegroup);
                    cmd.Parameters.AddWithValue("@WidthGroup", WidthGroup);
                    cmd.Parameters.AddWithValue("@markertype", markertype);
                    cmd.Parameters.AddWithValue("@factid", factid);
                    
                    sda.SelectCommand = cmd;


                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        private DataTable GetAvailbleRollData(int skudetpk, string skrinkagegroup, string WidthGroup, string markertype,int warehouseid)
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
                         (FabricRollmaster.MarkerType = @markertype) AND (RollInventoryMaster.IsPresent = N'Y') AND (RollInventoryMaster.Location_Pk = @factid)"))


                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@skudetpk", skudetpk);
                    cmd.Parameters.AddWithValue("@skrinkagegroup", skrinkagegroup);
                    cmd.Parameters.AddWithValue("@WidthGroup", WidthGroup);
                    cmd.Parameters.AddWithValue("@markertype", markertype);
                    cmd.Parameters.AddWithValue("@factid", warehouseid);
                    
                    sda.SelectCommand = cmd;


                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        private DataTable getCutPlanofSKU(int skudetpk)
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT        CutPlanMaster.CutPlanNUM, AtcDetails.OurStyle, LocationMaster.LocationName, CutPlanMaster.ShrinkageGroup, CutPlanMaster.WidthGroup, CutPlanMaster.FabDescription, CutPlanMaster.MarkerType, CutPlanMaster.Fabrication, 
                         CutPlanMaster.RefPattern, SUM(CutPlanASQDetails.CutQty) AS CutQty, CutPlanMaster.CutPlanFabReq, CutPlanMaster.CutplanEfficency, CutPlanMaster.CutplanConsumption, CutPlanMaster.IsApproved, 
                         CutPlanMaster.IsPatternAdded, CutPlanMaster.IsCutorderGiven, CutPlanMaster.SkuDet_PK
FROM            CutPlanMaster INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         CutPlanASQDetails ON CutPlanMaster.CutPlan_PK = CutPlanASQDetails.CutPlan_PK INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID
GROUP BY CutPlanMaster.CutPlanNUM, LocationMaster.LocationName, CutPlanMaster.ShrinkageGroup, CutPlanMaster.WidthGroup, CutPlanMaster.FabDescription, CutPlanMaster.MarkerType, CutPlanMaster.Fabrication, 
                         CutPlanMaster.RefPattern, CutPlanMaster.CutPlanFabReq, CutPlanMaster.CutplanEfficency, CutPlanMaster.CutplanConsumption, CutPlanMaster.IsApproved, CutPlanMaster.IsPatternAdded, 
                         CutPlanMaster.IsCutorderGiven, CutPlanMaster.SkuDet_PK, AtcDetails.OurStyle
HAVING        (CutPlanMaster.SkuDet_PK = @skudetpk)"))


                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@skudetpk", skudetpk);
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        private DataTable GetCutorder(int skudetpk)
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT        CutOrderMaster.CutID, CutOrderMaster.Cut_NO, AtcDetails.OurStyle, CutOrderMaster.CutQty, CutOrderMaster.FabQty, SUM(CutOrderDO.DeliveryQty) AS DeliveredQty
FROM            CutOrderMaster INNER JOIN
                         AtcDetails ON CutOrderMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         CutOrderDO ON CutOrderMaster.CutID = CutOrderDO.CutID
GROUP BY CutOrderMaster.CutID, CutOrderMaster.Cut_NO, AtcDetails.OurStyle, CutOrderMaster.CutQty, CutOrderMaster.FabQty, CutOrderMaster.SkuDet_pk
HAVING        (CutOrderMaster.SkuDet_pk = @skudetpk)"))


                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@skudetpk", skudetpk);
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
