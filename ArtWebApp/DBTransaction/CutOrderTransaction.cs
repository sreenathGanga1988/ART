using System;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace ArtWebApp.DBTransaction
{
    public class CutOrderTransaction
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        public DataTable GetGarmentDescription(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + ISNULL(SkuRawMaterialMaster.Weight, '') + ISNULL(SkuRawMaterialMaster.Width, '') 
                         + '   ' + ISNULL(SkuRawmaterialDetail.ItemColor, '')+ ' (  ' + ISNULL(SkuRawmaterialDetail.ColorCode, '')+ '  ) '   + '   ' + ISNULL(SkuRawmaterialDetail.ItemSize, '') AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID
WHERE        (SkuRawMaterialMaster.Atc_id = @Param1) AND (ItemGroupMaster.ItemGroupName = N'Fabric')", con);


                cmd.Parameters.AddWithValue("@Param1", atcid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public DataTable GetCutOrder(int iipk,int toloc)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        CutOrderMaster.CutID, CutOrderMaster.Cut_NO
FROM            InventoryMaster INNER JOIN
                         CutOrderMaster ON InventoryMaster.SkuDet_Pk = CutOrderMaster.SkuDet_pk
WHERE        (InventoryMaster.InventoryItem_PK = @Param1) AND (CutOrderMaster.ToLoc = @Param2)", con);


                cmd.Parameters.AddWithValue("@Param1", iipk);
                cmd.Parameters.AddWithValue("@Param2", toloc);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }





        public DataTable GetCutOrderData(int cutid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        CutOrderMaster.CutID, CutOrderMaster.Cut_NO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, CutOrderMaster.Color,  0 as CutPlan, CutOrderMaster.CutQty, CutOrderMaster.CutWidth, 
                         CutOrderMaster.Shrinkage, CutOrderMaster.FabQty, CutOrderMaster.CutOrderQty, CutOrderMaster.ConsumptionQty, CutOrderMaster.IsCompleted, ExtraRequestReasonMaster.ExtraReason, AtcMaster.AtcNum, 
                         CutOrderMaster.CutOrderDate, CutOrderMaster.AddedBy, CutOrderMaster.CutOrderType, LocationMaster.LocationName, CutOrderMaster.FabNO, CutOrderMaster.BalanceQty, CutOrderMaster.DelivedQty, 
                         CutOrderMaster.FromLoc, CutOrderMaster.ToLoc, CutOrderMaster.OurStyleID, CutOrderMaster.AtcID
FROM            CutOrderMaster INNER JOIN
                         AtcDetails ON CutOrderMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON CutOrderMaster.AtcID = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON CutOrderMaster.ToLoc = LocationMaster.Location_PK LEFT OUTER JOIN
                         ExtraRequestReasonMaster ON CutOrderMaster.ExtraReason_Pk = ExtraRequestReasonMaster.ExtraReason_Pk
WHERE        (CutOrderMaster.CutID = @Param1)", con);
                cmd.Parameters.AddWithValue("@Param1", cutid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public DataTable GetCutOrderMasterData(int cutid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        CutOrderMaster.CutID, CutOrderMaster.Cut_NO, AtcMaster.AtcNum, AtcDetails.OurStyle, CutOrderMaster.Color, CutOrderMaster.CutPlan, CutOrderMaster.CutQty, CutOrderMaster.CutWidth, 
                         CutOrderMaster.Shrinkage, CutOrderMaster.FabQty, CutOrderMaster.CutOrderQty, CutOrderMaster.ConsumptionQty, CutOrderMaster.IsCompleted, CutOrderMaster.BalanceQty, CutOrderMaster.FabNO, 
                         CutOrderMaster.DelivedQty, LocationMaster.LocationName, CutOrderMaster.CutOrderDate, CutOrderMaster.CutOrderType, CutOrderMaster.AddedBy, CutOrderMaster.SkuDet_pk, CutOrderMaster.PaternName, 
                         ExtraRequestReasonMaster.ExtraReason, CutOrderMaster.MarkerType,CutOrderMaster.OurStyleID
FROM            CutOrderMaster INNER JOIN
                         AtcMaster ON CutOrderMaster.AtcID = AtcMaster.AtcId INNER JOIN
                         AtcDetails ON CutOrderMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         LocationMaster ON CutOrderMaster.ToLoc = LocationMaster.Location_PK INNER JOIN
                         ExtraRequestReasonMaster ON CutOrderMaster.ExtraReason_Pk = ExtraRequestReasonMaster.ExtraReason_Pk
WHERE        (CutOrderMaster.CutID = @Param1)", con);
                cmd.Parameters.AddWithValue("@Param1", cutid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public DataTable GetCutOrderDataofSKU(int Skudet_pk, int ourstyleid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        LocationMaster.LocationName, CutOrderMaster.CutID, CutOrderMaster.Cut_NO, AtcDetails.OurStyle, CutOrderMaster.Color, CutOrderMaster.CutQty, CutOrderMaster.FabQty, CutOrderMaster.ConsumptionQty, 
                         ExtraRequestReasonMaster.ExtraReason, AtcMaster.AtcNum, CutOrderMaster.CutOrderDate, CutOrderMaster.CutOrderType, ISNULL
                             ((SELECT        SUM(DeliveryQty) AS Expr1
                                 FROM            CutOrderDO
                                 WHERE        (CutID = CutOrderMaster.CutID)), 0) AS DeliveredQty, AtcMaster.AtcId
FROM            CutOrderMaster INNER JOIN
                         AtcDetails ON CutOrderMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON CutOrderMaster.AtcID = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON CutOrderMaster.ToLoc = LocationMaster.Location_PK LEFT OUTER JOIN
                         ExtraRequestReasonMaster ON CutOrderMaster.ExtraReason_Pk = ExtraRequestReasonMaster.ExtraReason_Pk
WHERE        ( CutOrderMaster.SkuDet_pk = @Param1 ) and  (AtcDetails.OurStyleID=@ourstyleid)", con);
                cmd.Parameters.AddWithValue("@Param1", Skudet_pk);
                cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        public DataTable GetCutOrderSizeData(int cutid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        CutOrderSizeDetails.Size, CutOrderSizeDetails.Ratio, CutOrderSizeDetails.Qty, CutOrderDetails.CutOrderDet_PK, CutOrderDetails.CutID
FROM            CutOrderSizeDetails INNER JOIN
                         CutOrderDetails ON CutOrderSizeDetails.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK
WHERE        (CutOrderDetails.CutID = @Param1)", con);


                cmd.Parameters.AddWithValue("@Param1", cutid);
                

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        public DataTable GetCutOrderSizeDataofMarker(int cutdetid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        Size, Ratio, Qty, CutOrderDet_PK
FROM            CutOrderSizeDetails
WHERE        (CutOrderDet_PK = @cutdetid)", con);


                cmd.Parameters.AddWithValue("@cutdetid", cutdetid);


                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public DataTable GetCutOrderSizeDataofCutorder(int cutorder_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        Size, Ratio, Qty, CutOrderDet_PK, CutID, Plies,(Ratio*Plies) as CutQty
FROM            (SELECT        CutOrderSizeDetails.Size, CutOrderSizeDetails.Ratio, CutOrderSizeDetails.Qty, CutOrderSizeDetails.CutOrderDet_PK, CutOrderDetails.CutID, ISNULL
                                                        ((SELECT        SUM(LaySheetDetails.NoOfPlies) AS Expr1
                                                            FROM            LaySheetDetails INNER JOIN
                                                                                     LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK
                                                            GROUP BY LaySheetMaster.CutOrderDet_PK
                                                            HAVING        (LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK)), 0) AS Plies
                          FROM            CutOrderSizeDetails INNER JOIN
                                                    CutOrderDetails ON CutOrderSizeDetails.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK
                          WHERE        (CutOrderDetails.CutID = @CutID)) AS tt
", con);


                cmd.Parameters.AddWithValue("@CutID", cutorder_pk);


                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public DataTable GetFabricShrinkage(int skudetpk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT   Distinct     ShrinkageGroup
FROM            FabricRollmaster
WHERE        (SkuDet_PK = @Param1)", con);






                cmd.Parameters.AddWithValue("@Param1", skudetpk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public DataTable GetFabricWidth(int skudetpk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT   Distinct     WidthGroup
FROM            FabricRollmaster
WHERE        (SkuDet_PK = @Param1)", con);


                cmd.Parameters.AddWithValue("@Param1", skudetpk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }




        public DataTable GetFabricMarkerType(int skudetpk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT   Distinct     MarkerType
FROM            FabricRollmaster
WHERE        (SkuDet_PK = @Param1)", con);


                cmd.Parameters.AddWithValue("@Param1", skudetpk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }





        public DataTable GetFabricShrinkageLocation(int skudetpk, int locationpk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


     

                SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT FabricRollmaster.ShrinkageGroup
FROM            FabricRollmaster INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK
WHERE        (FabricRollmaster.SkuDet_PK = @Param1) AND (RollInventoryMaster.IsPresent = N'Y') AND (RollInventoryMaster.Location_Pk = @locationpk)", con);






                cmd.Parameters.AddWithValue("@Param1", skudetpk);
                cmd.Parameters.AddWithValue("@locationpk", locationpk);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public DataTable GetFabricWidthLocation(int skudetpk,int locationpk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                
                SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT FabricRollmaster.WidthGroup
FROM            FabricRollmaster INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK
WHERE        (FabricRollmaster.SkuDet_PK = @Param1) AND (RollInventoryMaster.IsPresent = N'Y') AND (RollInventoryMaster.Location_Pk = @locationpk)", con);


                cmd.Parameters.AddWithValue("@Param1", skudetpk);
                cmd.Parameters.AddWithValue("@locationpk", locationpk);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }




        public DataTable GetFabricMarkerTypeLocation(int skudetpk,int locationpk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT FabricRollmaster.MarkerType
FROM            FabricRollmaster INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK
WHERE        (FabricRollmaster.SkuDet_PK = @Param1) AND (RollInventoryMaster.IsPresent = N'Y') AND (RollInventoryMaster.Location_Pk = @locationpk)", con);


                cmd.Parameters.AddWithValue("@Param1", skudetpk);

                cmd.Parameters.AddWithValue("@locationpk", locationpk);


                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }










        /// <summary>
        /// update the marker with the qty
        /// </summary>
        /// <param name="cutdetpk"></param>
        public void updatecutdet(int cutdetpk)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"UPDATE       CutOrderDetails
SET                Qty = (SELECT    ISNULL(   sum (Qty),0) as Qty
FROM            CutOrderSizeDetails
where        (CutOrderSizeDetails.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK)) , NoOfPc = (SELECT    ISNULL(   sum (Ratio),0) as ratio
FROM            CutOrderSizeDetails
where        (CutOrderSizeDetails.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK))
WHERE        (CutOrderDet_PK = @CutOrderDet_PK)", con);


                cmd.Parameters.AddWithValue("@CutOrderDet_PK", cutdetpk);

                cmd.ExecuteNonQuery();

               



            }
            
        }
        


    public void updatecutdetSP(int cutdetpk)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"CutorderDataUpdate_SP", con);


                cmd.Parameters.AddWithValue("@CutDet_PK", cutdetpk);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();





            }

        }
    }
}