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

namespace ArtWebApp.Production.Cutting
{
    public partial class CutplanAndRollofAtc : System.Web.UI.Page
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
                String navtype = Request.QueryString["atcid"];
                int  atcid = int.Parse (navtype);

                fillCutplan(atcid);
                FillCutOrder(atcid);
                fillCutorderDO(atcid);
                fillCutorderDOROLL(atcid);
                fillpENDINGrOLL(atcid);
            }
        }



        public void fillCutplan(int atcid)
        {
            DataTable dt = fillCutplanofatc(atcid);
            filldataforAll(dt, MasterDiv);

        }
        public void FillCutOrder(int atcid)
        {

            DataTable dt = FillCutOrderofatc(atcid);
            filldataforAll(dt, MasterDiv2);
        }
        public void fillCutorderDO(int atcid)
        {
            DataTable dt = fillCutorderDOofatc(atcid);
            filldataforAll(dt, MasterDiv3);

        }
        public void fillCutorderDOROLL(int atcid)
        {

            DataTable dt = fillCutorderDOROLLofatc(atcid);
            filldataforAll(dt, MasterDiv4);
        }
        public void fillpENDINGrOLL(int atcid)
        {
            DataTable dt = fillpENDINGrOLLofatc(atcid);
            filldataforAll(dt, MasterDiv5);

        }

        private DataTable fillCutplanofatc(int atcid)
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT        AtcDetails.OurStyle, CutPlanMaster.CutPlanNUM, CutPlanMaster.FabDescription, CutPlanMaster.ColorName, CutPlanMaster.CutPlanFabReq, SUM(CutPlanASQDetails.CutQty) AS CutQty, AtcDetails.AtcId, 
                         CutPlanMaster.ShrinkageGroup, CutPlanMaster.WidthGroup, CutPlanMaster.MarkerType, CutPlanMaster.SkuDet_PK
FROM            CutPlanMaster INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         CutPlanASQDetails ON CutPlanMaster.CutPlan_PK = CutPlanASQDetails.CutPlan_PK
GROUP BY AtcDetails.OurStyle, CutPlanMaster.CutPlanNUM, CutPlanMaster.CutPlanFabReq, AtcDetails.AtcId, CutPlanMaster.ColorName, CutPlanMaster.ColorCode, CutPlanMaster.FabDescription, 
                         CutPlanMaster.ShrinkageGroup, CutPlanMaster.WidthGroup, CutPlanMaster.MarkerType, CutPlanMaster.SkuDet_PK
HAVING        (AtcDetails.AtcId = @atcid)"))


                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@atcid", atcid);
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        private DataTable FillCutOrderofatc(int atcid)
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT        AtcDetails.OurStyle, CutOrderMaster.Cut_NO, CutOrderMaster.Color, CutOrderMaster.CutQty, CutOrderMaster.FabQty, CutOrderMaster.MarkerType, CutOrderMaster.CutWidth, CutOrderMaster.Shrinkage, 
                         CutOrderMaster.AtcID, CutOrderMaster.CutPlan_Pk, CutOrderMaster.SkuDet_pk
FROM            CutOrderMaster INNER JOIN
                         AtcDetails ON CutOrderMaster.OurStyleID = AtcDetails.OurStyleID
WHERE        (CutOrderMaster.AtcID = @atcid)
"))


                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@atcid", atcid);
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        private DataTable fillCutorderDOofatc(int atcid)
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT        CutOrderMaster.Cut_NO, CutOrderMaster.Shrinkage, CutOrderMaster.CutWidth, CutOrderMaster.MarkerType, SUM(CutOrderDO.DeliveryQty) AS DeliveryQty, CutOrderMaster.AtcID, 
                         CutOrderMaster.SkuDet_pk
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.Cut_NO, CutOrderMaster.Shrinkage, CutOrderMaster.CutWidth, CutOrderMaster.MarkerType, CutOrderMaster.AtcID, CutOrderMaster.SkuDet_pk
HAVING        (CutOrderMaster.AtcID = @atcid)"))


                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@atcid", atcid);
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        private DataTable fillCutorderDOROLLofatc(int atcid)
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT        DeliveryOrderMaster.DONum, FabricRollmaster.RollNum, FabricRollmaster.ShrinkageGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.WidthGroup, FabricRollmaster.MarkerType, CutOrderMaster.AtcID, 
                         FabricRollmaster.SkuDet_PK, CutOrderMaster.Cut_NO
FROM            DORollDetails INNER JOIN
                         DeliveryOrderMaster ON DORollDetails.DO_PK = DeliveryOrderMaster.DO_PK INNER JOIN
                         CutOrderMaster ON DORollDetails.CutID = CutOrderMaster.CutID INNER JOIN
                         FabricRollmaster ON DORollDetails.Roll_PK = FabricRollmaster.Roll_PK
WHERE        (CutOrderMaster.AtcID = @atcid)
"))


                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@atcid", atcid);
                    sda.SelectCommand = cmd;
                   
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        private DataTable fillpENDINGrOLLofatc(int atcid)
        {
            string constr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, RollInventoryMaster.DocumentNum, RollInventoryMaster.IsPresent, LocationMaster.LocationName, FabricRollmaster.AYard, FabricRollmaster.MarkerType, 
                         FabricRollmaster.ShrinkageGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.WidthGroup, FabricRollmaster.IsDelivered, SkuRawMaterialMaster.Atc_id, FabricRollmaster.SkuDet_PK
FROM            FabricRollmaster INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK INNER JOIN
                         LocationMaster ON RollInventoryMaster.Location_Pk = LocationMaster.Location_PK INNER JOIN
                         SkuRawmaterialDetail ON FabricRollmaster.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
WHERE        (RollInventoryMaster.IsPresent = N'Y') AND (FabricRollmaster.IsDelivered = N'N') AND (SkuRawMaterialMaster.Atc_id = 159)"))


                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@atcid", atcid);
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
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

