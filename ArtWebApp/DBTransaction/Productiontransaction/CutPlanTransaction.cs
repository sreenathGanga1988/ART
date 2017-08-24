using System;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace ArtWebApp.DBTransaction.Productiontransaction
{
    public static class CutPlanTransaction
    {
        static String  connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        public static DataTable GetTheFabricOfOurStyle(int ourstyleid, string colorcode)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string query = "";

                if(colorcode=="")
                {
                    query = @"SELECT        SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + ISNULL(SkuRawMaterialMaster.Weight, '') + ISNULL(SkuRawMaterialMaster.Width, '') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, '') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, '') AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK, StyleCostingMaster.OurStyleID
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK
WHERE        (ItemGroupMaster.ItemGroupName = N'Fabric')  AND (StyleCostingMaster.IsApproved='A')
GROUP BY SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + ISNULL(SkuRawMaterialMaster.Weight, '') + ISNULL(SkuRawMaterialMaster.Width, '') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, '') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ''), SkuRawmaterialDetail.SkuDet_PK, StyleCostingMaster.OurStyleID
HAVING        (StyleCostingMaster.OurStyleID = @ourstyleid)";
                }
                else
                {
                    query = @"SELECT        SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + ISNULL(SkuRawMaterialMaster.Weight, '') + ISNULL(SkuRawMaterialMaster.Width, '') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, '') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, '') AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK, StyleCostingMaster.OurStyleID
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         StyleCostingDetails ON SkuRawMaterialMaster.Sku_Pk = StyleCostingDetails.Sku_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK
WHERE        (ItemGroupMaster.ItemGroupName = N'Fabric') AND (SkuRawmaterialDetail.ColorCode=@colorcode)
GROUP BY SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + ISNULL(SkuRawMaterialMaster.Weight, '') + ISNULL(SkuRawMaterialMaster.Width, '') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, '') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ''), SkuRawmaterialDetail.SkuDet_PK, StyleCostingMaster.OurStyleID
HAVING        (StyleCostingMaster.OurStyleID = @ourstyleid)";
                }
                SqlCommand cmd = new SqlCommand(query, con);

                if (colorcode != "")
                {
                    cmd.Parameters.AddWithValue("@colorcode", colorcode);
                }
                    cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);
               
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public static DataTable GetfabricofATC(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string query = "";

               
              
                    query = @"SELECT        SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + ISNULL(SkuRawMaterialMaster.Weight, '') + ISNULL(SkuRawMaterialMaster.Width, '') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, '') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, '') AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.Atc_id
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID
WHERE        (ItemGroupMaster.ItemGroupName = N'Fabric')
GROUP BY SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N'') + ISNULL(SkuRawMaterialMaster.Width, 
                         N'') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, N'') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, N''), SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.Atc_id, 
                         SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + ISNULL(SkuRawMaterialMaster.Weight, '') + ISNULL(SkuRawMaterialMaster.Width, '') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, '') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, '')
HAVING        (SkuRawMaterialMaster.Atc_id = @atcid)";
                
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@atcid", atcid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }





        /// <summary>
        /// get the summary of sizewise data
        /// </summary>
        /// <param name="cutplan_PK"></param>
        /// <returns></returns>
        public static DataTable GetCutplanQty(int cutplan_PK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string query = @"SELECT        SizeName, SUM(CutQty) AS CutQty
FROM CutPlanASQDetails
WHERE(CutPlan_PK = @CutPlan_PK)
GROUP BY SizeName";

                
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@CutPlan_PK", cutplan_PK);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        

        /// <summary>
        /// get the history of the previous cutplansmade for a color for a location and style
        /// </summary>
        /// <param name="cutplan_PK"></param>
        /// <returns></returns>
        public static DataTable GetPreviousCutPlansofSkuofLocation(int ourstyleid, int location_pk, int skudet_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string query = @"SELECT        SUM(CutPlanASQDetails.CutQty) AS Qty, ISNULL(CutPlanMaster.CutplanConsumption, CutPlanMaster.BOMConsumption) AS CONSUMPTION, SUM(CutPlanASQDetails.CutQty) 
                         * ISNULL(CutPlanMaster.CutplanConsumption, CutPlanMaster.BOMConsumption) AS fabreq
FROM            CutPlanMaster INNER JOIN
                         CutPlanASQDetails ON CutPlanMaster.CutPlan_PK = CutPlanASQDetails.CutPlan_PK
GROUP BY CutPlanMaster.BOMConsumption, CutPlanMaster.CutplanConsumption, CutPlanMaster.SkuDet_PK, CutPlanMaster.Location_PK, CutPlanMaster.OurStyleID
HAVING        (CutPlanMaster.Location_PK = @location_pk) AND (CutPlanMaster.SkuDet_PK = @skudet_pk) AND (CutPlanMaster.OurStyleID = @ourstyleid)";


                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@location_pk", location_pk);
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public static DataTable GetCutplanmarkerSizeQty(int CutPlanMarkerDetails_PK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string query = @"SELECT        CutPlanSize_PK, Size, Ratio, Qty, CutPlanMarkerDetails_PK
FROM            CutPlanMarkerSizeDetails
WHERE        (CutPlanMarkerDetails_PK = @CutPlan_PK)";


                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@CutPlan_PK", CutPlanMarkerDetails_PK);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public static DataTable GetCutplanASQSizeQty(int cutplanpk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string query = @"SELECT        PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS ASQ, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.PoPackId, POPackDetails.OurStyleID, AtcDetails.OurStyle, 
                         AtcDetails.BuyerStyle, CutPlanASQDetails.ColorName, CutPlanASQDetails.SizeName, CutPlanASQDetails.CutQty, CutPlanASQDetails.CutPlan_PK, CutPlanASQDetails.CutPlanASQDetails_PK, 
                         StyleSize.Orderof
FROM            CutPlanASQDetails INNER JOIN
                         POPackDetails ON CutPlanASQDetails.PoPack_Detail_PK = POPackDetails.PoPack_Detail_PK INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         PoPackMaster ON CutPlanASQDetails.PoPackId = PoPackMaster.PoPackId INNER JOIN
                         StyleSize ON POPackDetails.OurStyleID = StyleSize.OurStyleID AND POPackDetails.SizeName = StyleSize.SizeName
WHERE        (CutPlanASQDetails.CutPlan_PK = @cutplanpk)";


                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@cutplanpk", cutplanpk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public static DataTable GetAlreadyCutQtyofColor(int ourstyleid, string colorcode)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string query = "";

                if (colorcode == "")
                {
                    query = @"SELECT        CutPlanASQDetails.ColorName, CutPlanASQDetails.SizeName, CutPlanASQDetails.PoPackId, SUM(CutPlanASQDetails.CutQty) AS CutQty, CutPlanMaster.OurStyleID,  
                         CutPlanMaster.ColorCode
FROM            CutPlanASQDetails INNER JOIN
                         CutPlanMaster ON CutPlanASQDetails.CutPlan_PK = CutPlanMaster.CutPlan_PK
GROUP BY CutPlanASQDetails.ColorName, CutPlanASQDetails.SizeName, CutPlanASQDetails.PoPackId, CutPlanMaster.OurStyleID,CutPlanMaster.ColorCode
HAVING        (CutPlanMaster.OurStyleID = @ourstyleid)  ";
                }
                else
                {
                    query = @"SELECT        CutPlanASQDetails.ColorName, CutPlanASQDetails.SizeName, CutPlanASQDetails.PoPackId, SUM(CutPlanASQDetails.CutQty) AS CutQty, CutPlanMaster.OurStyleID,  
                         CutPlanMaster.ColorCode
FROM            CutPlanASQDetails INNER JOIN
                         CutPlanMaster ON CutPlanASQDetails.CutPlan_PK = CutPlanMaster.CutPlan_PK
GROUP BY CutPlanASQDetails.ColorName, CutPlanASQDetails.SizeName, CutPlanASQDetails.PoPackId, CutPlanMaster.OurStyleID,CutPlanMaster.ColorCode
HAVING        (CutPlanMaster.OurStyleID = @ourstyleid) AND (CutPlanMaster.ColorCode = @colorcode) ";
                }
                SqlCommand cmd = new SqlCommand(query, con);

                if (colorcode != "CM")
                {
                    cmd.Parameters.AddWithValue("@colorcode", colorcode);
                }
                cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public static DataTable GetCutPlanMarkerDetails(int cutplanpk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string query = @"SELECT        CutPlanMarkerDetails_PK, CutPlan_PK, NoOfPc, isnull((SELECT        SUM(Qty) 
FROM            CutPlanMarkerSizeDetails
WHERE        (CutPlanMarkerDetails_PK = CutPlanMarkerDetails.CutPlanMarkerDetails_PK)) ,0) as Qty, NoOfPlies, CutPerPlies, Cutreq, MarkerNo
FROM            CutPlanMarkerDetails
WHERE        (CutPlan_PK = @cutplanpk)";


                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@cutplanpk", cutplanpk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


    }
}