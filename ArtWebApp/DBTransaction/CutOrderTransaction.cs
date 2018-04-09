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






        public DataTable GetTrimsDescription(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@" SELECT SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + ISNULL(SkuRawMaterialMaster.Weight, '') + ISNULL(SkuRawMaterialMaster.Width, '')
                         + '   ' + ISNULL(SkuRawmaterialDetail.ItemColor, '')+ ' (  ' + ISNULL(SkuRawmaterialDetail.ColorCode, '')+ '  ) '   + '   ' + ISNULL(SkuRawmaterialDetail.ItemSize, '') AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID
WHERE        (SkuRawMaterialMaster.Atc_id = @Param1) AND(ItemGroupMaster.ItemGroupName = N'Trims')", con);


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
WHERE        (InventoryMaster.InventoryItem_PK = @Param1) AND (CutOrderMaster.ToLoc = @Param2) AND (CutOrderMaster.IsDeleted = N'N')", con);


                cmd.Parameters.AddWithValue("@Param1", iipk);
                cmd.Parameters.AddWithValue("@Param2", toloc);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        public DataTable GetCutOrderDO(int cutid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        DeliveryOrderMaster.DONum, DeliveryOrderMaster.DeliveryDate, DeliveryOrderMaster.BoeNum, DeliveryOrderMaster.ContainerNumber, DeliveryOrderMaster.AddedBy, DeliveryOrderMaster.AddedDate, 
                         DeliveryOrderMaster.DoType, CutOrderDO.DeliveryQty, CutOrderDO.Skudet_PK, CutOrderDO.CutID
FROM            CutOrderDO INNER JOIN
                         DeliveryOrderDetails ON CutOrderDO.DoDet_Pk = DeliveryOrderDetails.DODet_PK INNER JOIN
                         DeliveryOrderMaster ON DeliveryOrderDetails.DO_PK = DeliveryOrderMaster.DO_PK
WHERE        (CutOrderDO.CutID = @Param1)", con);
                cmd.Parameters.AddWithValue("@Param1", cutid);

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
                                 WHERE        (CutID = CutOrderMaster.CutID)), 0) AS DeliveredQty, AtcMaster.AtcId,  ISNULL
                             ((SELECT        SUM(AYard)
FROM            DORollDetails
WHERE        (CutID =  CutOrderMaster.CutID)), 0) AS Rollyard
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
                                                            GROUP BY LaySheetMaster.CutOrderDet_PK,LaySheetDetails.IsDeleted 
                                                            HAVING        (LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK)  AND (LaySheetDetails.IsDeleted = N'N')), 0) AS Plies
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








        public DataTable GetCriticalPath(int ourstyleid, int  skudetpk,String ShrinkageGroup, int locationid)
        {
            DataTable dt = new DataTable();



            if (ShrinkageGroup == "NA" && ourstyleid != 0 && skudetpk != 0 && locationid==0)
            {





                SqlCommand cmd = new SqlCommand(@"Select   CutPlan_PK, LocationName, OurStyle, ColorName, ColorCode, CutPlanNUM, FabDescription, ShrinkageGroup, WidthGroup, MarkerType, BOMConsumption, CutplanConsumption, NewPatternName, CutOrderConsumption, 
                         MarkerDirection, CutOrderDate, Revisions, Reason ,MarkerStatus ,IsDeleted,RollCount,Rollyard,CutPlanFabReq,cutQty,DOQty,ActualRollYardDelivered,WFRoll ,FWROLL,LayedQty,  Layedfabric,EndBit from (SELECT   CutPlanMaster.CutPlan_PK,      LocationMaster.LocationName, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.ColorCode, CutPlanMaster.CutPlanNUM, CutPlanMaster.FabDescription, CutPlanMaster.ShrinkageGroup, 
                         CutPlanMaster.WidthGroup, CutPlanMaster.MarkerType, CutPlanMaster.BOMConsumption, CutPlanMaster.CutplanConsumption, CutPlanMaster.NewPatternName, CutPlanMaster.CutOrderConsumption,(
SELECT STUFF((SELECT ',' + CutPlanmarkerTypeName 
            FROM (SELECT        CutPlanmarkerTypeName
FROM            CutPlanMarkerType
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK))tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as MarkerDirection ,(SELECT        MAX(CutOrderDate) FROM            CutOrderMaster group by CutPlan_Pk
HAVING        (CutPlan_Pk = CutPlanMaster.CutPlan_PK)) as CutOrderDate ,(SELECT  COUNT(CutplanRejectionDetailID)
FROM            CutPlanRejectHistory
GROUP BY Cutplan_PK
HAVING        (Cutplan_PK =  CutPlanMaster.CutPlan_PK)) as Revisions ,(
SELECT STUFF((SELECT ',' + CutplanRejection 
            FROM (SELECT        CutPlanRejectionMaster.CutplanRejection
FROM            CutPlanRejectHistory INNER JOIN
                         CutPlanRejectionMaster ON CutPlanRejectHistory.CutPlanRejectionID = CutPlanRejectionMaster.CutPlanRejectionID
WHERE        (CutPlanRejectHistory.Cutplan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY CutPlanRejectionMaster.CutplanRejection)tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as Reason ,(CASE 
WHEN IsDeleted = 'Y' THEN 'Deleted'
WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND  IsRejected = 'N'  AND IsDeleted = 'N'  THEN 'Cutplan Submited' 
 WHEN IsRollAdded = 'N' AND IsApproved = 'N' AND IsRejected = 'N' AND IsDeleted = 'N'  THEN 'Not Submitted/Rolls pending' 
 
 WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND IsRejected = 'Y'   AND IsDeleted = 'N' THEN 'Cutplan Rejected' 
 WHEN IsApproved = 'Y' AND IsRejected = 'N' AND IsPatternAdded = 'N' AND IsDeleted = 'N'  THEN 'Cutplan Approved'
  WHEN IsPatternAdded = 'Y' AND IsRejected = 'N' AND IsCutorderGiven = 'N' AND IsDeleted = 'N'  THEN 'CutOrder Pending/marker  ready' 
  WHEN IsCutorderGiven = 'Y' AND  IsRejected = 'N' THEN 'CutOrder Completed' 

       END) as MarkerStatus, CutPlanMaster.IsDeleted,(SELECT        COUNT(Roll_PK) 
FROM            CutPlanRollDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY IsDeleted
HAVING        (IsDeleted = N'N') ) as RollCount ,(SELECT        SUM(FabricRollmaster.AYard) 
FROM            CutPlanRollDetails INNER JOIN
                         FabricRollmaster ON CutPlanRollDetails.Roll_PK = FabricRollmaster.Roll_PK
WHERE        (CutPlanRollDetails.CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (CutPlanRollDetails.IsDeleted = N'N')) as Rollyard, CutPlanMaster.CutPlanFabReq ,(SELECT        SUM(CutQty) 
FROM            CutPlanASQDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (IsDeleted = N'N')) as cutQty ,isnull((SELECT        SUM(CutOrderDO.DeliveryQty) 
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)),0) as DOQty ,ISNULL((SELECT        SUM(LaySheetDetails.FabUtilized)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as Layedfabric ,isnull((SELECT        SUM(LaySheetDetails.EndBit)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as EndBit ,ISNULL((SELECT        SUM(FabricRollmaster.AYard) 
FROM            DORollDetails INNER JOIN
                         FabricRollmaster ON DORollDetails.Roll_PK = FabricRollmaster.Roll_PK INNER JOIN
                         CutOrderMaster ON DORollDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as ActualRollYardDelivered ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty > 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as WFROLL ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty < 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as FWROLL ,(						  
						Select  isnull(Sum(LayedQty),0) from (  SELECT        CutOrderDetails.CutOrderDet_PK, CutOrderMaster.CutPlan_Pk ,isnull(( SELECT      Sum(  Noofplies * RatioSum) 
FROM            (SELECT        SUM(LaySheetDetails.NoOfPlies) AS Noofplies, ISNULL
                                                        ((SELECT        SUM(CutPlanMarkerSizeDetails.Ratio) AS Expr1
                                                            FROM            CutPlanMarkerSizeDetails INNER JOIN
                                                                                     CutOrderDetails as  CutOrderDetails1 ON CutPlanMarkerSizeDetails.CutPlanMarkerDetails_PK = CutOrderDetails1.CutPlanMarkerDetails_PK
                                                            WHERE        (CutOrderDetails1.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK)), 0) AS RatioSum
                          FROM            LaySheetDetails INNER JOIN
                                                    LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK
                          GROUP BY LaySheetMaster.CutOrderDet_PK
                          HAVING         (LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK))as tt),0) as LayedQty
FROM            CutOrderMaster INNER JOIN
                         CutOrderDetails ON CutOrderMaster.CutID = CutOrderDetails.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =CutPlanMaster.CutPlan_PK ))tt) as layedQty
FROM            CutPlanMaster INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID where (CutPlanMaster.OurStyleID = @ourstyleid) AND (CutPlanMaster.SkuDet_PK = @skudetpk)
						)tt

 ");


                cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);
                cmd.Parameters.AddWithValue("@skudetpk", skudetpk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);

            }

             else if (ShrinkageGroup != "NA" && ourstyleid != 0 && skudetpk != 0 && locationid == 0)
            {

                SqlCommand cmd = new SqlCommand(@"Select   CutPlan_PK, LocationName, OurStyle, ColorName, ColorCode, CutPlanNUM, FabDescription, ShrinkageGroup, WidthGroup, MarkerType, BOMConsumption, CutplanConsumption, NewPatternName, CutOrderConsumption, 
                         MarkerDirection, CutOrderDate, Revisions, Reason ,MarkerStatus ,IsDeleted,RollCount,Rollyard,CutPlanFabReq,cutQty,DOQty,ActualRollYardDelivered,WFRoll ,FWROLL,LayedQty,  Layedfabric,EndBit from (SELECT   CutPlanMaster.CutPlan_PK,      LocationMaster.LocationName, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.ColorCode, CutPlanMaster.CutPlanNUM, CutPlanMaster.FabDescription, CutPlanMaster.ShrinkageGroup, 
                         CutPlanMaster.WidthGroup, CutPlanMaster.MarkerType, CutPlanMaster.BOMConsumption, CutPlanMaster.CutplanConsumption, CutPlanMaster.NewPatternName, CutPlanMaster.CutOrderConsumption,(
SELECT STUFF((SELECT ',' + CutPlanmarkerTypeName 
            FROM (SELECT        CutPlanmarkerTypeName
FROM            CutPlanMarkerType
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK))tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as MarkerDirection ,(SELECT        MAX(CutOrderDate) FROM            CutOrderMaster group by CutPlan_Pk
HAVING        (CutPlan_Pk = CutPlanMaster.CutPlan_PK)) as CutOrderDate ,(SELECT  COUNT(CutplanRejectionDetailID)
FROM            CutPlanRejectHistory
GROUP BY Cutplan_PK
HAVING        (Cutplan_PK =  CutPlanMaster.CutPlan_PK)) as Revisions ,(
SELECT STUFF((SELECT ',' + CutplanRejection 
            FROM (SELECT        CutPlanRejectionMaster.CutplanRejection
FROM            CutPlanRejectHistory INNER JOIN
                         CutPlanRejectionMaster ON CutPlanRejectHistory.CutPlanRejectionID = CutPlanRejectionMaster.CutPlanRejectionID
WHERE        (CutPlanRejectHistory.Cutplan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY CutPlanRejectionMaster.CutplanRejection)tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as Reason ,(CASE 
WHEN IsDeleted = 'Y' THEN 'Deleted'
WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND  IsRejected = 'N'  AND IsDeleted = 'N'  THEN 'Cutplan Submited' 
 WHEN IsRollAdded = 'N' AND IsApproved = 'N' AND IsRejected = 'N' AND IsDeleted = 'N'  THEN 'Not Submitted/Rolls pending' 
 
 WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND IsRejected = 'Y'   AND IsDeleted = 'N' THEN 'Cutplan Rejected' 
 WHEN IsApproved = 'Y' AND IsRejected = 'N' AND IsPatternAdded = 'N' AND IsDeleted = 'N'  THEN 'Cutplan Approved'
  WHEN IsPatternAdded = 'Y' AND IsRejected = 'N' AND IsCutorderGiven = 'N' AND IsDeleted = 'N'  THEN 'CutOrder Pending/marker  ready' 
  WHEN IsCutorderGiven = 'Y' AND  IsRejected = 'N' THEN 'CutOrder Completed' 

       END) as MarkerStatus, CutPlanMaster.IsDeleted,(SELECT        COUNT(Roll_PK) 
FROM            CutPlanRollDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY IsDeleted
HAVING        (IsDeleted = N'N') ) as RollCount ,(SELECT        SUM(FabricRollmaster.AYard) 
FROM            CutPlanRollDetails INNER JOIN
                         FabricRollmaster ON CutPlanRollDetails.Roll_PK = FabricRollmaster.Roll_PK
WHERE        (CutPlanRollDetails.CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (CutPlanRollDetails.IsDeleted = N'N')) as Rollyard, CutPlanMaster.CutPlanFabReq ,(SELECT        SUM(CutQty) 
FROM            CutPlanASQDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (IsDeleted = N'N')) as cutQty ,isnull((SELECT        SUM(CutOrderDO.DeliveryQty) 
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)),0) as DOQty ,ISNULL((SELECT        SUM(LaySheetDetails.FabUtilized)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as Layedfabric ,isnull((SELECT        SUM(LaySheetDetails.EndBit)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as EndBit ,ISNULL((SELECT        SUM(FabricRollmaster.AYard) 
FROM            DORollDetails INNER JOIN
                         FabricRollmaster ON DORollDetails.Roll_PK = FabricRollmaster.Roll_PK INNER JOIN
                         CutOrderMaster ON DORollDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as ActualRollYardDelivered ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty > 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as WFROLL ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty < 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as FWROLL ,(						  
						Select  isnull(Sum(LayedQty),0) from (  SELECT        CutOrderDetails.CutOrderDet_PK, CutOrderMaster.CutPlan_Pk ,isnull(( SELECT      Sum(  Noofplies * RatioSum) 
FROM            (SELECT        SUM(LaySheetDetails.NoOfPlies) AS Noofplies, ISNULL
                                                        ((SELECT        SUM(CutPlanMarkerSizeDetails.Ratio) AS Expr1
                                                            FROM            CutPlanMarkerSizeDetails INNER JOIN
                                                                                     CutOrderDetails as  CutOrderDetails1 ON CutPlanMarkerSizeDetails.CutPlanMarkerDetails_PK = CutOrderDetails1.CutPlanMarkerDetails_PK
                                                            WHERE        (CutOrderDetails1.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK)), 0) AS RatioSum
                          FROM            LaySheetDetails INNER JOIN
                                                    LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK
                          GROUP BY LaySheetMaster.CutOrderDet_PK
                          HAVING         (LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK))as tt),0) as LayedQty
FROM            CutOrderMaster INNER JOIN
                         CutOrderDetails ON CutOrderMaster.CutID = CutOrderDetails.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =CutPlanMaster.CutPlan_PK ))tt) as layedQty
FROM            CutPlanMaster INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID where (CutPlanMaster.OurStyleID = @ourstyleid) AND (CutPlanMaster.SkuDet_PK = @skudetpk)  AND (CutPlanMaster.ShrinkageGroup = @shrinkagegroup)  and (CutPlanMaster.Location_PK=@locationid)
						)tt


 ");


                cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);
                cmd.Parameters.AddWithValue("@skudetpk", skudetpk);
                cmd.Parameters.AddWithValue("@shrinkagegroup", ShrinkageGroup);
                cmd.Parameters.AddWithValue("@locationid", locationid);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);

            }

            else if (ShrinkageGroup == "NA" && ourstyleid != 0 && skudetpk != 0 && locationid != 0)
            {





                SqlCommand cmd = new SqlCommand(@"Select   CutPlan_PK, LocationName, OurStyle, ColorName, ColorCode, CutPlanNUM, FabDescription, ShrinkageGroup, WidthGroup, MarkerType, BOMConsumption, CutplanConsumption, NewPatternName, CutOrderConsumption, 
                         MarkerDirection, CutOrderDate, Revisions, Reason ,MarkerStatus ,IsDeleted,RollCount,Rollyard,CutPlanFabReq,cutQty,DOQty,ActualRollYardDelivered,WFRoll ,FWROLL,LayedQty,  Layedfabric,EndBit from (SELECT   CutPlanMaster.CutPlan_PK,      LocationMaster.LocationName, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.ColorCode, CutPlanMaster.CutPlanNUM, CutPlanMaster.FabDescription, CutPlanMaster.ShrinkageGroup, 
                         CutPlanMaster.WidthGroup, CutPlanMaster.MarkerType, CutPlanMaster.BOMConsumption, CutPlanMaster.CutplanConsumption, CutPlanMaster.NewPatternName, CutPlanMaster.CutOrderConsumption,(
SELECT STUFF((SELECT ',' + CutPlanmarkerTypeName 
            FROM (SELECT        CutPlanmarkerTypeName
FROM            CutPlanMarkerType
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK))tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as MarkerDirection ,(SELECT        MAX(CutOrderDate) FROM            CutOrderMaster group by CutPlan_Pk
HAVING        (CutPlan_Pk = CutPlanMaster.CutPlan_PK)) as CutOrderDate ,(SELECT  COUNT(CutplanRejectionDetailID)
FROM            CutPlanRejectHistory
GROUP BY Cutplan_PK
HAVING        (Cutplan_PK =  CutPlanMaster.CutPlan_PK)) as Revisions ,(
SELECT STUFF((SELECT ',' + CutplanRejection 
            FROM (SELECT        CutPlanRejectionMaster.CutplanRejection
FROM            CutPlanRejectHistory INNER JOIN
                         CutPlanRejectionMaster ON CutPlanRejectHistory.CutPlanRejectionID = CutPlanRejectionMaster.CutPlanRejectionID
WHERE        (CutPlanRejectHistory.Cutplan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY CutPlanRejectionMaster.CutplanRejection)tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as Reason ,(CASE 
WHEN IsDeleted = 'Y' THEN 'Deleted'
WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND  IsRejected = 'N'  AND IsDeleted = 'N'  THEN 'Cutplan Submited' 
 WHEN IsRollAdded = 'N' AND IsApproved = 'N' AND IsRejected = 'N' AND IsDeleted = 'N'  THEN 'Not Submitted/Rolls pending' 
 
 WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND IsRejected = 'Y'   AND IsDeleted = 'N' THEN 'Cutplan Rejected' 
 WHEN IsApproved = 'Y' AND IsRejected = 'N' AND IsPatternAdded = 'N' AND IsDeleted = 'N'  THEN 'Cutplan Approved'
  WHEN IsPatternAdded = 'Y' AND IsRejected = 'N' AND IsCutorderGiven = 'N' AND IsDeleted = 'N'  THEN 'CutOrder Pending/marker  ready' 
  WHEN IsCutorderGiven = 'Y' AND  IsRejected = 'N' THEN 'CutOrder Completed' 

       END) as MarkerStatus, CutPlanMaster.IsDeleted,(SELECT        COUNT(Roll_PK) 
FROM            CutPlanRollDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY IsDeleted
HAVING        (IsDeleted = N'N') ) as RollCount ,(SELECT        SUM(FabricRollmaster.AYard) 
FROM            CutPlanRollDetails INNER JOIN
                         FabricRollmaster ON CutPlanRollDetails.Roll_PK = FabricRollmaster.Roll_PK
WHERE        (CutPlanRollDetails.CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (CutPlanRollDetails.IsDeleted = N'N')) as Rollyard, CutPlanMaster.CutPlanFabReq ,(SELECT        SUM(CutQty) 
FROM            CutPlanASQDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (IsDeleted = N'N')) as cutQty ,isnull((SELECT        SUM(CutOrderDO.DeliveryQty) 
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)),0) as DOQty ,ISNULL((SELECT        SUM(LaySheetDetails.FabUtilized)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as Layedfabric ,isnull((SELECT        SUM(LaySheetDetails.EndBit)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as EndBit ,ISNULL((SELECT        SUM(FabricRollmaster.AYard) 
FROM            DORollDetails INNER JOIN
                         FabricRollmaster ON DORollDetails.Roll_PK = FabricRollmaster.Roll_PK INNER JOIN
                         CutOrderMaster ON DORollDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as ActualRollYardDelivered ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty > 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as WFROLL ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty < 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as FWROLL ,(						  
						Select  isnull(Sum(LayedQty),0) from (  SELECT        CutOrderDetails.CutOrderDet_PK, CutOrderMaster.CutPlan_Pk ,isnull(( SELECT      Sum(  Noofplies * RatioSum) 
FROM            (SELECT        SUM(LaySheetDetails.NoOfPlies) AS Noofplies, ISNULL
                                                        ((SELECT        SUM(CutPlanMarkerSizeDetails.Ratio) AS Expr1
                                                            FROM            CutPlanMarkerSizeDetails INNER JOIN
                                                                                     CutOrderDetails as  CutOrderDetails1 ON CutPlanMarkerSizeDetails.CutPlanMarkerDetails_PK = CutOrderDetails1.CutPlanMarkerDetails_PK
                                                            WHERE        (CutOrderDetails1.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK)), 0) AS RatioSum
                          FROM            LaySheetDetails INNER JOIN
                                                    LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK
                          GROUP BY LaySheetMaster.CutOrderDet_PK
                          HAVING         (LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK))as tt),0) as LayedQty
FROM            CutOrderMaster INNER JOIN
                         CutOrderDetails ON CutOrderMaster.CutID = CutOrderDetails.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =CutPlanMaster.CutPlan_PK ))tt) as layedQty
FROM            CutPlanMaster INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID where (CutPlanMaster.OurStyleID = @ourstyleid) AND (CutPlanMaster.SkuDet_PK = @skudetpk)  and (CutPlanMaster.Location_PK=@locationid)
						)tt

 ");


                cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);
                cmd.Parameters.AddWithValue("@skudetpk", skudetpk);
                cmd.Parameters.AddWithValue("@locationid", locationid);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);

            } 

            else if (ShrinkageGroup != "NA" && ourstyleid != 0 && skudetpk != 0 && locationid != 0)
            {

                SqlCommand cmd = new SqlCommand(@"Select   CutPlan_PK, LocationName, OurStyle, ColorName, ColorCode, CutPlanNUM, FabDescription, ShrinkageGroup, WidthGroup, MarkerType, BOMConsumption, CutplanConsumption, NewPatternName, CutOrderConsumption, 
                         MarkerDirection, CutOrderDate, Revisions, Reason ,MarkerStatus ,IsDeleted,RollCount,Rollyard,CutPlanFabReq,cutQty,DOQty,ActualRollYardDelivered,WFRoll ,FWROLL,LayedQty,  Layedfabric,EndBit from (SELECT   CutPlanMaster.CutPlan_PK,      LocationMaster.LocationName, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.ColorCode, CutPlanMaster.CutPlanNUM, CutPlanMaster.FabDescription, CutPlanMaster.ShrinkageGroup, 
                         CutPlanMaster.WidthGroup, CutPlanMaster.MarkerType, CutPlanMaster.BOMConsumption, CutPlanMaster.CutplanConsumption, CutPlanMaster.NewPatternName, CutPlanMaster.CutOrderConsumption,(
SELECT STUFF((SELECT ',' + CutPlanmarkerTypeName 
            FROM (SELECT        CutPlanmarkerTypeName
FROM            CutPlanMarkerType
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK))tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as MarkerDirection ,(SELECT        MAX(CutOrderDate) FROM            CutOrderMaster group by CutPlan_Pk
HAVING        (CutPlan_Pk = CutPlanMaster.CutPlan_PK)) as CutOrderDate ,(SELECT  COUNT(CutplanRejectionDetailID)
FROM            CutPlanRejectHistory
GROUP BY Cutplan_PK
HAVING        (Cutplan_PK =  CutPlanMaster.CutPlan_PK)) as Revisions ,(
SELECT STUFF((SELECT ',' + CutplanRejection 
            FROM (SELECT        CutPlanRejectionMaster.CutplanRejection
FROM            CutPlanRejectHistory INNER JOIN
                         CutPlanRejectionMaster ON CutPlanRejectHistory.CutPlanRejectionID = CutPlanRejectionMaster.CutPlanRejectionID
WHERE        (CutPlanRejectHistory.Cutplan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY CutPlanRejectionMaster.CutplanRejection)tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as Reason ,(CASE 
WHEN IsDeleted = 'Y' THEN 'Deleted'
WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND  IsRejected = 'N'  AND IsDeleted = 'N'  THEN 'Cutplan Submited' 
 WHEN IsRollAdded = 'N' AND IsApproved = 'N' AND IsRejected = 'N' AND IsDeleted = 'N'  THEN 'Not Submitted/Rolls pending' 
 
 WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND IsRejected = 'Y'   AND IsDeleted = 'N' THEN 'Cutplan Rejected' 
 WHEN IsApproved = 'Y' AND IsRejected = 'N' AND IsPatternAdded = 'N' AND IsDeleted = 'N'  THEN 'Cutplan Approved'
  WHEN IsPatternAdded = 'Y' AND IsRejected = 'N' AND IsCutorderGiven = 'N' AND IsDeleted = 'N'  THEN 'CutOrder Pending/marker  ready' 
  WHEN IsCutorderGiven = 'Y' AND  IsRejected = 'N' THEN 'CutOrder Completed' 

       END) as MarkerStatus, CutPlanMaster.IsDeleted,(SELECT        COUNT(Roll_PK) 
FROM            CutPlanRollDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY IsDeleted
HAVING        (IsDeleted = N'N') ) as RollCount ,(SELECT        SUM(FabricRollmaster.AYard) 
FROM            CutPlanRollDetails INNER JOIN
                         FabricRollmaster ON CutPlanRollDetails.Roll_PK = FabricRollmaster.Roll_PK
WHERE        (CutPlanRollDetails.CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (CutPlanRollDetails.IsDeleted = N'N')) as Rollyard, CutPlanMaster.CutPlanFabReq ,(SELECT        SUM(CutQty) 
FROM            CutPlanASQDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (IsDeleted = N'N')) as cutQty ,isnull((SELECT        SUM(CutOrderDO.DeliveryQty) 
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)),0) as DOQty ,ISNULL((SELECT        SUM(LaySheetDetails.FabUtilized)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as Layedfabric ,isnull((SELECT        SUM(LaySheetDetails.EndBit)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as EndBit ,ISNULL((SELECT        SUM(FabricRollmaster.AYard) 
FROM            DORollDetails INNER JOIN
                         FabricRollmaster ON DORollDetails.Roll_PK = FabricRollmaster.Roll_PK INNER JOIN
                         CutOrderMaster ON DORollDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as ActualRollYardDelivered ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty > 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as WFROLL ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty < 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as FWROLL ,(						  
						Select  isnull(Sum(LayedQty),0) from (  SELECT        CutOrderDetails.CutOrderDet_PK, CutOrderMaster.CutPlan_Pk ,isnull(( SELECT      Sum(  Noofplies * RatioSum) 
FROM            (SELECT        SUM(LaySheetDetails.NoOfPlies) AS Noofplies, ISNULL
                                                        ((SELECT        SUM(CutPlanMarkerSizeDetails.Ratio) AS Expr1
                                                            FROM            CutPlanMarkerSizeDetails INNER JOIN
                                                                                     CutOrderDetails as  CutOrderDetails1 ON CutPlanMarkerSizeDetails.CutPlanMarkerDetails_PK = CutOrderDetails1.CutPlanMarkerDetails_PK
                                                            WHERE        (CutOrderDetails1.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK)), 0) AS RatioSum
                          FROM            LaySheetDetails INNER JOIN
                                                    LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK
                          GROUP BY LaySheetMaster.CutOrderDet_PK
                          HAVING         (LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK))as tt),0) as LayedQty
FROM            CutOrderMaster INNER JOIN
                         CutOrderDetails ON CutOrderMaster.CutID = CutOrderDetails.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =CutPlanMaster.CutPlan_PK ))tt) as layedQty
FROM            CutPlanMaster INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID where (CutPlanMaster.OurStyleID = @ourstyleid) AND (CutPlanMaster.SkuDet_PK = @skudetpk)  AND (CutPlanMaster.ShrinkageGroup = @shrinkagegroup)
						)tt


 ");


                cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);
                cmd.Parameters.AddWithValue("@skudetpk", skudetpk);
                cmd.Parameters.AddWithValue("@shrinkagegroup", ShrinkageGroup);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);

            }


            else
            {


                SqlCommand cmd = new SqlCommand(@"Select   CutPlan_PK, LocationName, OurStyle, ColorName, ColorCode, CutPlanNUM, FabDescription, ShrinkageGroup, WidthGroup, MarkerType, BOMConsumption, CutplanConsumption, NewPatternName, CutOrderConsumption, 
                         MarkerDirection, CutOrderDate, Revisions, Reason ,MarkerStatus ,IsDeleted,RollCount,Rollyard,CutPlanFabReq,cutQty,DOQty,ActualRollYardDelivered,WFRoll ,FWROLL,LayedQty,  Layedfabric,EndBit from (SELECT   CutPlanMaster.CutPlan_PK,      LocationMaster.LocationName, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.ColorCode, CutPlanMaster.CutPlanNUM, CutPlanMaster.FabDescription, CutPlanMaster.ShrinkageGroup, 
                         CutPlanMaster.WidthGroup, CutPlanMaster.MarkerType, CutPlanMaster.BOMConsumption, CutPlanMaster.CutplanConsumption, CutPlanMaster.NewPatternName, CutPlanMaster.CutOrderConsumption,(
SELECT STUFF((SELECT ',' + CutPlanmarkerTypeName 
            FROM (SELECT        CutPlanmarkerTypeName
FROM            CutPlanMarkerType
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK))tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as MarkerDirection ,(SELECT        MAX(CutOrderDate) FROM            CutOrderMaster group by CutPlan_Pk
HAVING        (CutPlan_Pk = CutPlanMaster.CutPlan_PK)) as CutOrderDate ,(SELECT  COUNT(CutplanRejectionDetailID)
FROM            CutPlanRejectHistory
GROUP BY Cutplan_PK
HAVING        (Cutplan_PK =  CutPlanMaster.CutPlan_PK)) as Revisions ,(
SELECT STUFF((SELECT ',' + CutplanRejection 
            FROM (SELECT        CutPlanRejectionMaster.CutplanRejection
FROM            CutPlanRejectHistory INNER JOIN
                         CutPlanRejectionMaster ON CutPlanRejectHistory.CutPlanRejectionID = CutPlanRejectionMaster.CutPlanRejectionID
WHERE        (CutPlanRejectHistory.Cutplan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY CutPlanRejectionMaster.CutplanRejection)tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as Reason ,(CASE 
WHEN IsDeleted = 'Y' THEN 'Deleted'
WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND  IsRejected = 'N'  AND IsDeleted = 'N'  THEN 'Cutplan Submited' 
 WHEN IsRollAdded = 'N' AND IsApproved = 'N' AND IsRejected = 'N' AND IsDeleted = 'N'  THEN 'Not Submitted/Rolls pending' 
 
 WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND IsRejected = 'Y'   AND IsDeleted = 'N' THEN 'Cutplan Rejected' 
 WHEN IsApproved = 'Y' AND IsRejected = 'N' AND IsPatternAdded = 'N' AND IsDeleted = 'N'  THEN 'Cutplan Approved'
  WHEN IsPatternAdded = 'Y' AND IsRejected = 'N' AND IsCutorderGiven = 'N' AND IsDeleted = 'N'  THEN 'CutOrder Pending/marker  ready' 
  WHEN IsCutorderGiven = 'Y' AND  IsRejected = 'N' THEN 'CutOrder Completed' 

       END) as MarkerStatus, CutPlanMaster.IsDeleted,(SELECT        COUNT(Roll_PK) 
FROM            CutPlanRollDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY IsDeleted
HAVING        (IsDeleted = N'N') ) as RollCount ,(SELECT        SUM(FabricRollmaster.AYard) 
FROM            CutPlanRollDetails INNER JOIN
                         FabricRollmaster ON CutPlanRollDetails.Roll_PK = FabricRollmaster.Roll_PK
WHERE        (CutPlanRollDetails.CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (CutPlanRollDetails.IsDeleted = N'N')) as Rollyard, CutPlanMaster.CutPlanFabReq ,(SELECT        SUM(CutQty) 
FROM            CutPlanASQDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (IsDeleted = N'N')) as cutQty ,isnull((SELECT        SUM(CutOrderDO.DeliveryQty) 
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)),0) as DOQty ,ISNULL((SELECT        SUM(LaySheetDetails.FabUtilized)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as Layedfabric ,isnull((SELECT        SUM(LaySheetDetails.EndBit)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as EndBit ,ISNULL((SELECT        SUM(FabricRollmaster.AYard) 
FROM            DORollDetails INNER JOIN
                         FabricRollmaster ON DORollDetails.Roll_PK = FabricRollmaster.Roll_PK INNER JOIN
                         CutOrderMaster ON DORollDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as ActualRollYardDelivered ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty > 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as WFROLL ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty < 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as FWROLL ,(						  
						Select  isnull(Sum(LayedQty),0) from (  SELECT        CutOrderDetails.CutOrderDet_PK, CutOrderMaster.CutPlan_Pk ,isnull(( SELECT      Sum(  Noofplies * RatioSum) 
FROM            (SELECT        SUM(LaySheetDetails.NoOfPlies) AS Noofplies, ISNULL
                                                        ((SELECT        SUM(CutPlanMarkerSizeDetails.Ratio) AS Expr1
                                                            FROM            CutPlanMarkerSizeDetails INNER JOIN
                                                                                     CutOrderDetails as  CutOrderDetails1 ON CutPlanMarkerSizeDetails.CutPlanMarkerDetails_PK = CutOrderDetails1.CutPlanMarkerDetails_PK
                                                            WHERE        (CutOrderDetails1.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK)), 0) AS RatioSum
                          FROM            LaySheetDetails INNER JOIN
                                                    LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK
                          GROUP BY LaySheetMaster.CutOrderDet_PK
                          HAVING         (LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK))as tt),0) as LayedQty
FROM            CutOrderMaster INNER JOIN
                         CutOrderDetails ON CutOrderMaster.CutID = CutOrderDetails.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =CutPlanMaster.CutPlan_PK ))tt) as layedQty
FROM            CutPlanMaster INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID where (CutPlanMaster.OurStyleID = @ourstyleid) AND (CutPlanMaster.SkuDet_PK = @skudetpk)
						)tt

 ");


                cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);
                cmd.Parameters.AddWithValue("@skudetpk", skudetpk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);

            }



        }


        public DataTable GetCriticalPath(int atcid,  int locationid)
        {
            DataTable dt = new DataTable();



            if (locationid == 0)
            {





                SqlCommand cmd = new SqlCommand(@"Select   CutPlan_PK, LocationName, OurStyle, ColorName, ColorCode, CutPlanNUM, FabDescription, ShrinkageGroup, WidthGroup, MarkerType, BOMConsumption, CutplanConsumption, NewPatternName, CutOrderConsumption, 
                         MarkerDirection, CutOrderDate, Revisions, Reason ,MarkerStatus ,IsDeleted,RollCount,Rollyard,CutPlanFabReq,cutQty,DOQty,ActualRollYardDelivered,WFRoll ,FWROLL,LayedQty,  Layedfabric,EndBit from (SELECT   CutPlanMaster.CutPlan_PK,      LocationMaster.LocationName, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.ColorCode, CutPlanMaster.CutPlanNUM, CutPlanMaster.FabDescription, CutPlanMaster.ShrinkageGroup, 
                         CutPlanMaster.WidthGroup, CutPlanMaster.MarkerType, CutPlanMaster.BOMConsumption, CutPlanMaster.CutplanConsumption, CutPlanMaster.NewPatternName, CutPlanMaster.CutOrderConsumption,(
SELECT STUFF((SELECT ',' + CutPlanmarkerTypeName 
            FROM (SELECT        CutPlanmarkerTypeName
FROM            CutPlanMarkerType
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK))tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as MarkerDirection ,(SELECT        MAX(CutOrderDate) FROM            CutOrderMaster group by CutPlan_Pk
HAVING        (CutPlan_Pk = CutPlanMaster.CutPlan_PK)) as CutOrderDate ,(SELECT  COUNT(CutplanRejectionDetailID)
FROM            CutPlanRejectHistory
GROUP BY Cutplan_PK
HAVING        (Cutplan_PK =  CutPlanMaster.CutPlan_PK)) as Revisions ,(
SELECT STUFF((SELECT ',' + CutplanRejection 
            FROM (SELECT        CutPlanRejectionMaster.CutplanRejection
FROM            CutPlanRejectHistory INNER JOIN
                         CutPlanRejectionMaster ON CutPlanRejectHistory.CutPlanRejectionID = CutPlanRejectionMaster.CutPlanRejectionID
WHERE        (CutPlanRejectHistory.Cutplan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY CutPlanRejectionMaster.CutplanRejection)tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as Reason ,(CASE 
WHEN IsDeleted = 'Y' THEN 'Deleted'
WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND  IsRejected = 'N'  AND IsDeleted = 'N'  THEN 'Cutplan Submited' 
 WHEN IsRollAdded = 'N' AND IsApproved = 'N' AND IsRejected = 'N' AND IsDeleted = 'N'  THEN 'Not Submitted/Rolls pending' 
 
 WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND IsRejected = 'Y'   AND IsDeleted = 'N' THEN 'Cutplan Rejected' 
 WHEN IsApproved = 'Y' AND IsRejected = 'N' AND IsPatternAdded = 'N' AND IsDeleted = 'N'  THEN 'Cutplan Approved'
  WHEN IsPatternAdded = 'Y' AND IsRejected = 'N' AND IsCutorderGiven = 'N' AND IsDeleted = 'N'  THEN 'CutOrder Pending/marker  ready' 
  WHEN IsCutorderGiven = 'Y' AND  IsRejected = 'N' THEN 'CutOrder Completed' 

       END) as MarkerStatus, CutPlanMaster.IsDeleted,(SELECT        COUNT(Roll_PK) 
FROM            CutPlanRollDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY IsDeleted
HAVING        (IsDeleted = N'N') ) as RollCount ,(SELECT        SUM(FabricRollmaster.AYard) 
FROM            CutPlanRollDetails INNER JOIN
                         FabricRollmaster ON CutPlanRollDetails.Roll_PK = FabricRollmaster.Roll_PK
WHERE        (CutPlanRollDetails.CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (CutPlanRollDetails.IsDeleted = N'N')) as Rollyard, CutPlanMaster.CutPlanFabReq ,(SELECT        SUM(CutQty) 
FROM            CutPlanASQDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (IsDeleted = N'N')) as cutQty ,isnull((SELECT        SUM(CutOrderDO.DeliveryQty) 
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)),0) as DOQty ,ISNULL((SELECT        SUM(LaySheetDetails.FabUtilized)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as Layedfabric ,isnull((SELECT        SUM(LaySheetDetails.EndBit)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as EndBit ,ISNULL((SELECT        SUM(FabricRollmaster.AYard) 
FROM            DORollDetails INNER JOIN
                         FabricRollmaster ON DORollDetails.Roll_PK = FabricRollmaster.Roll_PK INNER JOIN
                         CutOrderMaster ON DORollDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as ActualRollYardDelivered ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty > 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as WFROLL ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty < 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as FWROLL ,(						  
						Select  isnull(Sum(LayedQty),0) from (  SELECT        CutOrderDetails.CutOrderDet_PK, CutOrderMaster.CutPlan_Pk ,isnull(( SELECT      Sum(  Noofplies * RatioSum) 
FROM            (SELECT        SUM(LaySheetDetails.NoOfPlies) AS Noofplies, ISNULL
                                                        ((SELECT        SUM(CutPlanMarkerSizeDetails.Ratio) AS Expr1
                                                            FROM            CutPlanMarkerSizeDetails INNER JOIN
                                                                                     CutOrderDetails as  CutOrderDetails1 ON CutPlanMarkerSizeDetails.CutPlanMarkerDetails_PK = CutOrderDetails1.CutPlanMarkerDetails_PK
                                                            WHERE        (CutOrderDetails1.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK)), 0) AS RatioSum
                          FROM            LaySheetDetails INNER JOIN
                                                    LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK
                          GROUP BY LaySheetMaster.CutOrderDet_PK
                          HAVING         (LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK))as tt),0) as LayedQty
FROM            CutOrderMaster INNER JOIN
                         CutOrderDetails ON CutOrderMaster.CutID = CutOrderDetails.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =CutPlanMaster.CutPlan_PK ))tt) as layedQty
FROM            CutPlanMaster INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID where (AtcDetails.AtcId  = @atcid) 
						)tt


 ");


                cmd.Parameters.AddWithValue("@atcid", atcid);
            
                return QueryFunctions.ReturnQueryResultDatatable(cmd);

            }

            else 
            {

                SqlCommand cmd = new SqlCommand(@"Select   CutPlan_PK, LocationName, OurStyle, ColorName, ColorCode, CutPlanNUM, FabDescription, ShrinkageGroup, WidthGroup, MarkerType, BOMConsumption, CutplanConsumption, NewPatternName, CutOrderConsumption, 
                         MarkerDirection, CutOrderDate, Revisions, Reason ,MarkerStatus ,IsDeleted,RollCount,Rollyard,CutPlanFabReq,cutQty,DOQty,ActualRollYardDelivered,WFRoll ,FWROLL,LayedQty,  Layedfabric,EndBit from (SELECT   CutPlanMaster.CutPlan_PK,      LocationMaster.LocationName, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.ColorCode, CutPlanMaster.CutPlanNUM, CutPlanMaster.FabDescription, CutPlanMaster.ShrinkageGroup, 
                         CutPlanMaster.WidthGroup, CutPlanMaster.MarkerType, CutPlanMaster.BOMConsumption, CutPlanMaster.CutplanConsumption, CutPlanMaster.NewPatternName, CutPlanMaster.CutOrderConsumption,(
SELECT STUFF((SELECT ',' + CutPlanmarkerTypeName 
            FROM (SELECT        CutPlanmarkerTypeName
FROM            CutPlanMarkerType
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK))tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as MarkerDirection ,(SELECT        MAX(CutOrderDate) FROM            CutOrderMaster group by CutPlan_Pk
HAVING        (CutPlan_Pk = CutPlanMaster.CutPlan_PK)) as CutOrderDate ,(SELECT  COUNT(CutplanRejectionDetailID)
FROM            CutPlanRejectHistory
GROUP BY Cutplan_PK
HAVING        (Cutplan_PK =  CutPlanMaster.CutPlan_PK)) as Revisions ,(
SELECT STUFF((SELECT ',' + CutplanRejection 
            FROM (SELECT        CutPlanRejectionMaster.CutplanRejection
FROM            CutPlanRejectHistory INNER JOIN
                         CutPlanRejectionMaster ON CutPlanRejectHistory.CutPlanRejectionID = CutPlanRejectionMaster.CutPlanRejectionID
WHERE        (CutPlanRejectHistory.Cutplan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY CutPlanRejectionMaster.CutplanRejection)tt
            FOR XML PATH('')) ,1,1,'') AS Txt
) as Reason ,(CASE 
WHEN IsDeleted = 'Y' THEN 'Deleted'
WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND  IsRejected = 'N'  AND IsDeleted = 'N'  THEN 'Cutplan Submited' 
 WHEN IsRollAdded = 'N' AND IsApproved = 'N' AND IsRejected = 'N' AND IsDeleted = 'N'  THEN 'Not Submitted/Rolls pending' 
 
 WHEN IsRollAdded = 'Y' AND IsApproved = 'N' AND IsRejected = 'Y'   AND IsDeleted = 'N' THEN 'Cutplan Rejected' 
 WHEN IsApproved = 'Y' AND IsRejected = 'N' AND IsPatternAdded = 'N' AND IsDeleted = 'N'  THEN 'Cutplan Approved'
  WHEN IsPatternAdded = 'Y' AND IsRejected = 'N' AND IsCutorderGiven = 'N' AND IsDeleted = 'N'  THEN 'CutOrder Pending/marker  ready' 
  WHEN IsCutorderGiven = 'Y' AND  IsRejected = 'N' THEN 'CutOrder Completed' 

       END) as MarkerStatus, CutPlanMaster.IsDeleted,(SELECT        COUNT(Roll_PK) 
FROM            CutPlanRollDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK)
GROUP BY IsDeleted
HAVING        (IsDeleted = N'N') ) as RollCount ,(SELECT        SUM(FabricRollmaster.AYard) 
FROM            CutPlanRollDetails INNER JOIN
                         FabricRollmaster ON CutPlanRollDetails.Roll_PK = FabricRollmaster.Roll_PK
WHERE        (CutPlanRollDetails.CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (CutPlanRollDetails.IsDeleted = N'N')) as Rollyard, CutPlanMaster.CutPlanFabReq ,(SELECT        SUM(CutQty) 
FROM            CutPlanASQDetails
WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK) AND (IsDeleted = N'N')) as cutQty ,isnull((SELECT        SUM(CutOrderDO.DeliveryQty) 
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)),0) as DOQty ,ISNULL((SELECT        SUM(LaySheetDetails.FabUtilized)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
WHERE        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as Layedfabric ,isnull((SELECT        SUM(LaySheetDetails.EndBit)
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                         CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as EndBit ,ISNULL((SELECT        SUM(FabricRollmaster.AYard) 
FROM            DORollDetails INNER JOIN
                         FabricRollmaster ON DORollDetails.Roll_PK = FabricRollmaster.Roll_PK INNER JOIN
                         CutOrderMaster ON DORollDetails.CutID = CutOrderMaster.CutID
GROUP BY CutOrderMaster.CutPlan_Pk
HAVING        (CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK)),0) as ActualRollYardDelivered ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty > 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as WFROLL ,isnull((SELECT        SUM(ISNULL(CutOrderDO.RollYard, CutOrderDO.DeliveryQty)) AS Expr1
FROM            CutOrderDO INNER JOIN
                         CutOrderMaster ON CutOrderDO.CutID = CutOrderMaster.CutID
						 where         (CutOrderDO.DeliveryQty < 0) AND (CutOrderMaster.CutPlan_Pk =  CutPlanMaster.CutPlan_PK)
GROUP BY CutOrderMaster.CutPlan_Pk),0) as FWROLL ,(						  
						Select  isnull(Sum(LayedQty),0) from (  SELECT        CutOrderDetails.CutOrderDet_PK, CutOrderMaster.CutPlan_Pk ,isnull(( SELECT      Sum(  Noofplies * RatioSum) 
FROM            (SELECT        SUM(LaySheetDetails.NoOfPlies) AS Noofplies, ISNULL
                                                        ((SELECT        SUM(CutPlanMarkerSizeDetails.Ratio) AS Expr1
                                                            FROM            CutPlanMarkerSizeDetails INNER JOIN
                                                                                     CutOrderDetails as  CutOrderDetails1 ON CutPlanMarkerSizeDetails.CutPlanMarkerDetails_PK = CutOrderDetails1.CutPlanMarkerDetails_PK
                                                            WHERE        (CutOrderDetails1.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK)), 0) AS RatioSum
                          FROM            LaySheetDetails INNER JOIN
                                                    LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK
                          GROUP BY LaySheetMaster.CutOrderDet_PK
                          HAVING         (LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK))as tt),0) as LayedQty
FROM            CutOrderMaster INNER JOIN
                         CutOrderDetails ON CutOrderMaster.CutID = CutOrderDetails.CutID
WHERE        (CutOrderMaster.CutPlan_Pk =CutPlanMaster.CutPlan_PK ))tt) as layedQty
FROM            CutPlanMaster INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID where (AtcDetails.AtcId  = @atcid) AND  (CutPlanMaster.Location_PK=@locationid)
						)tt


 ");


               
                cmd.Parameters.AddWithValue("@atcid", atcid);
        
                cmd.Parameters.AddWithValue("@locationid", locationid);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);

            }

          
            


        }


    }
}