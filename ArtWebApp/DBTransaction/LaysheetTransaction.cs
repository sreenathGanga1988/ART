using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ArtWebApp.DataModels;
using System.Collections;
namespace ArtWebApp.DBTransaction
{
    public static class LaysheetTransaction
    {


        //       /// <summary>
        //       /// roll
        //       /// 
        //       /// </summary>
        //       /// <param name="asn"></param>
        //       /// <param name="atcid"></param>
        //       /// <returns></returns>
        //        public static DataTable getRolldeliveredagainstACutorderandNotLayed(int cutid ,int factid )
        //        {

        //            using (SqlCommand cmd = new SqlCommand())
        //            {
//        cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, FabricRollmaster.SShade, FabricRollmaster.AShade, 
//                         FabricRollmaster.ShadeGroup, FabricRollmaster.SWidth, FabricRollmaster.AWidth, FabricRollmaster.WidthGroup, FabricRollmaster.SShrink, FabricRollmaster.AShrink, FabricRollmaster.ShrinkageGroup, 
//                         FabricRollmaster.SYard, FabricRollmaster.AYard, RollInventoryMaster.FactId, CutOrderMaster.CutID, RollInventoryMaster.IsPresent
//FROM            FabricRollmaster INNER JOIN
//                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
//                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK INNER JOIN
//                         CutOrderMaster ON FabricRollmaster.WidthGroup = CutOrderMaster.CutWidth AND FabricRollmaster.ShrinkageGroup = CutOrderMaster.Shrinkage AND 
//                         FabricRollmaster.MarkerType = CutOrderMaster.MarkerType AND FabricRollmaster.SkuDet_PK = CutOrderMaster.SkuDet_pk
//WHERE        (FabricRollmaster.Roll_PK NOT IN
//                             (SELECT        Roll_PK
//                               FROM            LaySheetDetails
//                               WHERE        (Roll_PK = FabricRollmaster.Roll_PK) AND (IsRecuttable = 'N'))) AND (FabricRollmaster.Roll_PK NOT IN
//                             (SELECT        Roll_PK
//                               FROM            LaySheetRollDetails
//                               WHERE        (Roll_PK = FabricRollmaster.Roll_PK) AND ((IsUsed = 'W')OR (IsUsed = 'Y')))) AND (RollInventoryMaster.FactId = @factid) AND (CutOrderMaster.CutID = @cutid) AND (RollInventoryMaster.IsPresent = N'Y')";
//                cmd.Parameters.AddWithValue("@cutid", cutid);
//                cmd.Parameters.AddWithValue("@factid", factid);
//                return QueryFunctions.ReturnQueryResultDatatable(cmd);
//            }
//        }


        /// <summary>
        /// roll
        /// 
        /// </summary>
        /// <param name="asn"></param>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static DataTable getRolldeliveredagainstACutorderandNotLayed(int cutid, int factid)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, FabricRollmaster.SShade, FabricRollmaster.AShade, 
                         FabricRollmaster.ShadeGroup, FabricRollmaster.SWidth, FabricRollmaster.AWidth, FabricRollmaster.WidthGroup, FabricRollmaster.SShrink, FabricRollmaster.AShrink, FabricRollmaster.ShrinkageGroup, 
                         FabricRollmaster.SYard, FabricRollmaster.AYard, RollInventoryMaster.FactId, CutOrderMaster.CutID, RollInventoryMaster.IsPresent,'New' as RollStatus
FROM            FabricRollmaster INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK INNER JOIN
                         CutOrderMaster ON FabricRollmaster.WidthGroup = CutOrderMaster.CutWidth AND FabricRollmaster.ShrinkageGroup = CutOrderMaster.Shrinkage AND 
                         FabricRollmaster.MarkerType = CutOrderMaster.MarkerType AND FabricRollmaster.SkuDet_PK = CutOrderMaster.SkuDet_pk
WHERE        (FabricRollmaster.Roll_PK NOT IN
                             (SELECT        Roll_PK
                               FROM            LaySheetRollDetails
                               WHERE        (Roll_PK = FabricRollmaster.Roll_PK)))  AND (RollInventoryMaster.FactId = @factid) AND (CutOrderMaster.CutID = @cutid) AND (RollInventoryMaster.IsPresent = N'Y')



Union


SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, FabricRollmaster.SShade, FabricRollmaster.AShade, 
                         FabricRollmaster.ShadeGroup, FabricRollmaster.SWidth, FabricRollmaster.AWidth, FabricRollmaster.WidthGroup, FabricRollmaster.SShrink, FabricRollmaster.AShrink, FabricRollmaster.ShrinkageGroup, 
                         FabricRollmaster.SYard, LaySheetRollDetails.balanceyardage as AYard , RollInventoryMaster.FactId, CutOrderMaster.CutID, RollInventoryMaster.IsPresent ,'ReCut' as RollStatus
FROM            FabricRollmaster INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK INNER JOIN
                         CutOrderMaster ON FabricRollmaster.WidthGroup = CutOrderMaster.CutWidth AND FabricRollmaster.ShrinkageGroup = CutOrderMaster.Shrinkage AND 
                         FabricRollmaster.MarkerType = CutOrderMaster.MarkerType AND FabricRollmaster.SkuDet_PK = CutOrderMaster.SkuDet_pk INNER JOIN
                         LaySheetRollDetails ON FabricRollmaster.Roll_PK = LaySheetRollDetails.Roll_PK
WHERE        (RollInventoryMaster.FactId = @factid) AND (CutOrderMaster.CutID = @cutid) AND (RollInventoryMaster.IsPresent = N'Y') AND (LaySheetRollDetails.IsUsed = N'R') ";
                cmd.Parameters.AddWithValue("@cutid", cutid);
                cmd.Parameters.AddWithValue("@factid", factid);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }




        /// <summary>
        /// DisplaySelected Cutorder
        /// </summary>
        /// <param name="cutid"></param>
        /// <returns></returns>
        public static DataTable getSelectedRolldeliveredagainstACutorderandNotLayed(int cutid)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, FabricRollmaster.SShade, FabricRollmaster.AShade, 
                         FabricRollmaster.ShadeGroup, FabricRollmaster.SWidth, FabricRollmaster.AWidth, FabricRollmaster.WidthGroup, FabricRollmaster.SShrink, FabricRollmaster.AShrink, FabricRollmaster.ShrinkageGroup, 
                         FabricRollmaster.SYard, FabricRollmaster.AYard, DORollDetails.CutID, LaySheetRollDetails.IsUsed
FROM            DORollDetails INNER JOIN
                         FabricRollmaster ON DORollDetails.Roll_PK = FabricRollmaster.Roll_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         LaySheetRollDetails ON FabricRollmaster.Roll_PK = LaySheetRollDetails.Roll_PK AND DORollDetails.CutID = LaySheetRollDetails.Cutid
WHERE        (DORollDetails.CutID = @cutid) AND (FabricRollmaster.Roll_PK NOT IN
                             (SELECT        Roll_PK
                               FROM            LaySheetDetails
                               WHERE        (Roll_PK = FabricRollmaster.Roll_PK) AND (IsRecuttable = 'N'))) AND (LaySheetRollDetails.IsUsed = N'W')";
                cmd.Parameters.AddWithValue("@cutid", cutid);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }





//        /// <summary>
//        /// DisplaySelected Cutorder
//        /// </summary>
//        /// <param name="cutid"></param>
//        /// <returns></returns>
//        public static DataTable getSelectedRollNotLayedagainstlaysheetref(String Laysheetref)
//        {

//            using (SqlCommand cmd = new SqlCommand())
//            {
//                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, FabricRollmaster.SShade, FabricRollmaster.AShade, 
//                         FabricRollmaster.ShadeGroup, FabricRollmaster.SWidth, FabricRollmaster.AWidth, FabricRollmaster.WidthGroup, FabricRollmaster.SShrink, FabricRollmaster.AShrink, FabricRollmaster.ShrinkageGroup, 
//                         FabricRollmaster.SYard, FabricRollmaster.AYard, LaySheetRollDetails.IsUsed, LaySheetRollDetails.LayRollRef
//FROM            SupplierDocumentMaster INNER JOIN
//                         FabricRollmaster ON SupplierDocumentMaster.SupplierDoc_pk = FabricRollmaster.SupplierDoc_pk INNER JOIN
//                         LaySheetRollDetails ON FabricRollmaster.Roll_PK = LaySheetRollDetails.Roll_PK
//WHERE        (FabricRollmaster.Roll_PK NOT IN
//                             (SELECT        Roll_PK
//                               FROM            LaySheetDetails
//                               WHERE        (Roll_PK = FabricRollmaster.Roll_PK) AND (IsRecuttable = 'N'))) AND (LaySheetRollDetails.IsUsed = N'W') AND (LaySheetRollDetails.LayRollRef = @Laysheetref)";
//                cmd.Parameters.AddWithValue("@Laysheetref", Laysheetref);

//                return QueryFunctions.ReturnQueryResultDatatable(cmd);
//            }
//        }

        /// <summary>
        /// DisplaySelected Cutorder
        /// </summary>
        /// <param name="cutid"></param>
        /// <returns></returns>
        public static DataTable getSelectedRollNotLayedagainstlaysheetref(String Laysheetref)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, FabricRollmaster.SShade, FabricRollmaster.AShade, 
                         FabricRollmaster.ShadeGroup, FabricRollmaster.SWidth, FabricRollmaster.AWidth, FabricRollmaster.WidthGroup, FabricRollmaster.SShrink, FabricRollmaster.AShrink, FabricRollmaster.ShrinkageGroup, 
                         FabricRollmaster.SYard, LaySheetRollDetails.Yardage as Ayard, LaySheetRollDetails.IsUsed, LaySheetRollDetails.LayRollRef, LaySheetRollDetails.LaySheetRoll_Pk
FROM            SupplierDocumentMaster INNER JOIN
                         FabricRollmaster ON SupplierDocumentMaster.SupplierDoc_pk = FabricRollmaster.SupplierDoc_pk INNER JOIN
                         LaySheetRollDetails ON FabricRollmaster.Roll_PK = LaySheetRollDetails.Roll_PK
WHERE        (LaySheetRollDetails.IsUsed = N'W') AND (LaySheetRollDetails.LayRollRef = @Laysheetref)";
                cmd.Parameters.AddWithValue("@Laysheetref", Laysheetref);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        /// <summary>
        /// rolls delivered against a cutorder
        /// 
        /// </summary>
        /// <param name="asn"></param>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static DataTable getRolldeliveredagainstACutorder(int cutid)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, FabricRollmaster.SShade, FabricRollmaster.AShade, 
                         FabricRollmaster.ShadeGroup, FabricRollmaster.SWidth, FabricRollmaster.AWidth, FabricRollmaster.WidthGroup, FabricRollmaster.SShrink, FabricRollmaster.AShrink, FabricRollmaster.ShrinkageGroup, 
                         FabricRollmaster.SYard, FabricRollmaster.AYard, DORollDetails.CutID
FROM            DORollDetails INNER JOIN
                         FabricRollmaster ON DORollDetails.Roll_PK = FabricRollmaster.Roll_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk
WHERE        (DORollDetails.CutID = @cutid) AND  FabricRollmaster.Roll_PK not in (select LaySheetDetails.Roll_PK from LaySheetDetails where LaySheetDetails.Roll_PK=FabricRollmaster.Roll_PK and LaySheetDetails.isRecuttable='N' )";
                cmd.Parameters.AddWithValue("@cutid", cutid);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }




        public static int  getlaysheetnum( int cutid)
        {
            int laynum = 0;
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {

                    cmd.CommandText = @"SELECT     COUNT(   LaysheetRollmaster_Pk)
FROM            LaySheetRollMaster

where   (CutID  = @cutid)
";
                    cmd.Parameters.AddWithValue("@cutid", cutid);

                    laynum = int.Parse(QueryFunctions.ReturnQueryValue(cmd).ToString());

                }
                catch (Exception)
                {

                    laynum += 1;
                }

            }
            return laynum;
        }


        public static int getlaysheetnumofcutorder(int CutOrderDet_PK)
        {
            int laynum = 0;
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {

                    cmd.CommandText = @"SELECT        COUNT(LaysheetRollmaster_Pk) 
FROM            LaySheetRollMaster where        (CutOrderDet_PK = @CutOrderDet_PK)
";
                    cmd.Parameters.AddWithValue("@CutOrderDet_PK", CutOrderDet_PK);

                    laynum = int.Parse(QueryFunctions.ReturnQueryValue(cmd).ToString());

                }
                catch (Exception)
                {

                    laynum += 1;
                }

            }
            return laynum;
        }

        /// <summary>
        /// get the laysheetmASTERdATA OF A LAYSHEET FOR REPORT
        /// </summary>
        /// <param name="laysheetPK"></param>
        /// <returns></returns>
        public static DataTable GetLaysheetmasterData(int laysheetPK)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "GetlaysheetmasterData_SP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@laysheetpk", laysheetPK);

                return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }
        }



        public static DataTable getSizeRatioofLaysheet(int laysheetPK)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        CutOrderSizeDetails.Size, CutOrderSizeDetails.Ratio, CutOrderSizeDetails.Qty, CutOrderDetails.CutOrderDet_PK, CutOrderDetails.CutID, StyleSize.Orderof, LaySheetMaster.LaySheet_PK
FROM            CutOrderSizeDetails INNER JOIN
                         CutOrderDetails ON CutOrderSizeDetails.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID INNER JOIN
                         StyleSize ON CutOrderMaster.OurStyleID = StyleSize.OurStyleID AND CutOrderSizeDetails.Size = StyleSize.SizeName INNER JOIN
                         LaySheetMaster ON CutOrderDetails.CutOrderDet_PK = LaySheetMaster.CutOrderDet_PK
WHERE        (LaySheetMaster.LaySheet_PK = @laysheetPK)
ORDER BY StyleSize.Orderof";
                cmd.Parameters.AddWithValue("@laysheetPK", laysheetPK);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        public static DataTable getSizeRatioofLaysheetRollPk(int laysheetPK)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        CutOrderSizeDetails.Size, CutOrderSizeDetails.Ratio, CutOrderSizeDetails.Qty, CutOrderDetails.CutOrderDet_PK, CutOrderDetails.CutID, StyleSize.Orderof, LaySheetRollMaster.LaysheetRollmaster_Pk
FROM            CutOrderSizeDetails INNER JOIN
                         CutOrderDetails ON CutOrderSizeDetails.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID INNER JOIN
                         StyleSize ON CutOrderMaster.OurStyleID = StyleSize.OurStyleID AND CutOrderSizeDetails.Size = StyleSize.SizeName INNER JOIN
                         LaySheetRollMaster ON CutOrderMaster.CutID = LaySheetRollMaster.CutID INNER JOIN
                         LaySheetRollDetails ON CutOrderDetails.CutOrderDet_PK = LaySheetRollDetails.CutOrderDet_PK AND LaySheetRollMaster.LaysheetRollmaster_Pk = LaySheetRollDetails.LaysheetRollmaster_Pk AND 
                         LaySheetRollMaster.CutID = LaySheetRollDetails.Cutid AND LaySheetRollMaster.CutOrderDet_PK = LaySheetRollDetails.CutOrderDet_PK
GROUP BY CutOrderSizeDetails.Size, CutOrderSizeDetails.Ratio, CutOrderSizeDetails.Qty, CutOrderDetails.CutOrderDet_PK, CutOrderDetails.CutID, StyleSize.Orderof, LaySheetRollMaster.LaysheetRollmaster_Pk
Having        (LaySheetRollMaster.LaysheetRollmaster_Pk = @laysheetPK)
ORDER BY StyleSize.Orderof";
                cmd.Parameters.AddWithValue("@laysheetPK", laysheetPK);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        public static DataTable getlaysheetpendingCutorder()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        CutID, Cut_NO, AtcNum, OurStyle, Color, FabQty, CutWidth, Shrinkage, MarkerType, PaternName, CutQty, layed, CutQty - layed AS Pending, CutPlan_Pk, LocationName
FROM            (SELECT        CutOrderMaster.CutID, CutOrderMaster.Cut_NO, AtcMaster.AtcNum, AtcDetails.OurStyle, CutOrderMaster.Color, CutOrderMaster.CutQty, CutOrderMaster.FabQty, CutOrderMaster.CutWidth, 
                         CutOrderMaster.Shrinkage, CutOrderMaster.MarkerType, CutOrderMaster.PaternName, ISNULL
                             ((SELECT        SUM(LaySheetDetails.NoOfPlies * CutOrderSizeDetails.Ratio) AS Alreadylayed
                                 FROM            LaySheetDetails INNER JOIN
                                                          LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK INNER JOIN
                                                          CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                                                          CutOrderSizeDetails ON CutOrderDetails.CutOrderDet_PK = CutOrderSizeDetails.CutOrderDet_PK
                                 WHERE        (CutOrderDetails.CutID = CutOrderMaster.CutID)
                                 GROUP BY CutOrderDetails.CutID), 0) AS layed, CutOrderMaster.CutPlan_Pk, LocationMaster.LocationName, CutOrderMaster.CutOrderDate
FROM            CutOrderMaster INNER JOIN
                         AtcDetails ON CutOrderMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON CutOrderMaster.AtcID = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON CutOrderMaster.ToLoc = LocationMaster.Location_PK
WHERE        (CutOrderMaster.CutPlan_Pk IS NOT NULL) AND (CutOrderMaster.CutOrderDate > CONVERT(DATETIME, '2017-05-15 00:00:00', 102))) AS tt
WHERE        (CutQty - layed > 0)
";
             

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }
    }
}