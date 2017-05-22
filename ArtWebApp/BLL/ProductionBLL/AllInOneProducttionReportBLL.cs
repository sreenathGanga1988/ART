using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.ProductionBLL
{
    public static  class AllInOneProducttionReportBLL
    {




        public static DataTable getProductionDataofCountryWithinPeriod(string Countryname,DateTime fromdate ,DateTime todate)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        PoPackId, SeasonName, BuyerName, AtcNum, PoPacknum, BuyerPO, OurStyle, BuyerStyle, POQty, POQty * FOB AS POvalue, ShipedQty, ShipedQty * FOB AS ShippedValue, Balance, 
                         Balance * FOB AS BalanceValue, CONVERT(DATE, FirstDeliveryDate, 101) AS FirstDeliveryDate, CONVERT(DATE, HandoverDate, 101) AS HandoverDate, CONVERT(DATE, DeliveryDate, 101) AS DeliveryDate, 
                         Iscuttable, LocationName, IsShortClosed, FOB, OurStyleID, Description
FROM            (SELECT        tt.PoPackId, tt.OurStyleID, tt.SeasonName, BuyerMaster.BuyerName, AtcMaster_1.AtcNum, tt.PoPacknum, tt.BuyerPO, tt.OurStyle, tt.BuyerStyle, tt.POQty, tt.ShipedQty, 
                                                    tt.POQty - tt.ShipedQty AS Balance, tt.FirstDeliveryDate, tt.HandoverDate, tt.DeliveryDate, ISNULL(tt.IsCutable, 'N') AS Iscuttable, ISNULL(tt.LocationName,
                                                        (SELECT DISTINCT LocationMaster.LocationName
                                                          FROM            ASQAllocationMaster INNER JOIN
                                                                                    LocationMaster ON ASQAllocationMaster.Locaion_PK = LocationMaster.Location_PK
                                                          WHERE        (ASQAllocationMaster.PoPackId = tt.PoPackId) AND (ASQAllocationMaster.OurStyleId = tt.OurStyleID))) AS LocationName, tt.IsShortClosed, tt.FOB, tt.Description
                          FROM            (SELECT        PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, SUM(POPackDetails_1.PoQty) AS POQty, ISNULL
                                                                                  ((SELECT        SUM(ShipmentHandOverDetails.ShippedQty) AS Expr1
                                                                                      FROM            ShipmentHandOverDetails INNER JOIN
                                                                                                               JobContractDetail ON ShipmentHandOverDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk
                                                                                      GROUP BY JobContractDetail.PoPackID, JobContractDetail.OurStyleID
                                                                                      HAVING        (JobContractDetail.PoPackID = PoPackMaster.PoPackId) AND (JobContractDetail.OurStyleID = POPackDetails_1.OurStyleID)), 0) AS ShipedQty, AtcDetails.OurStyleID, 
                                                                              PoPackMaster.FirstDeliveryDate, PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, MAX(POPackDetails_1.IsCutable) AS IsCutable, 
                                                                              PoPackMaster.SeasonName, LocationMaster_1.LocationName, AtcDetails.FOB, MAX(POPackDetails_1.IsShortClosed) AS IsShortClosed, CountryMaster_1.Description
                                                    FROM            PoPackMaster INNER JOIN
                                                                              POPackDetails AS POPackDetails_1 ON PoPackMaster.PoPackId = POPackDetails_1.POPackId INNER JOIN
                                                                              AtcDetails ON POPackDetails_1.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                                                                              AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId INNER JOIN
                                                                              CountryMaster AS CountryMaster_1 ON AtcMaster.ProductionCountryID = CountryMaster_1.CountryID LEFT OUTER JOIN
                                                                              LocationMaster AS LocationMaster_1 ON PoPackMaster.ExpectedLocation_PK = LocationMaster_1.Location_PK
                                                    GROUP BY PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, POPackDetails_1.OurStyleID, AtcDetails.OurStyleID, 
                                                                              PoPackMaster.FirstDeliveryDate, PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, PoPackMaster.SeasonName, LocationMaster_1.LocationName, 
                                                                              AtcDetails.FOB, CountryMaster_1.Description) AS tt INNER JOIN
                                                    AtcMaster AS AtcMaster_1 ON tt.AtcId = AtcMaster_1.AtcId INNER JOIN
                                                    BuyerMaster ON AtcMaster_1.Buyer_ID = BuyerMaster.BuyerID) AS ttt
WHERE        (DeliveryDate BETWEEN @fromdate AND @todate) AND (Description = @countryname)";



                cmd.Parameters.AddWithValue("@countryname", Countryname);
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);


                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
            }

            return dt;
        }



        public static DataTable getProductionDataofBuyerWithinPeriod(string BuyerName, DateTime fromdate, DateTime todate)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        PoPackId, SeasonName, BuyerName, AtcNum, PoPacknum, BuyerPO, OurStyle, BuyerStyle, POQty, POQty * FOB AS POvalue, ShipedQty, ShipedQty * FOB AS ShippedValue, Balance, 
                         Balance * FOB AS BalanceValue, CONVERT(DATE, FirstDeliveryDate, 101) AS FirstDeliveryDate, CONVERT(DATE, HandoverDate, 101) AS HandoverDate, CONVERT(DATE, DeliveryDate, 101) AS DeliveryDate, 
                         Iscuttable, LocationName, IsShortClosed, FOB, OurStyleID
FROM            (SELECT        PoPackId, OurStyleID, SeasonName, PoPacknum, BuyerPO, OurStyle, BuyerStyle, POQty, ShipedQty, POQty - ShipedQty AS Balance, FirstDeliveryDate, HandoverDate, DeliveryDate, ISNULL(IsCutable, 'N') 
                         AS Iscuttable, ISNULL(LocationName,
                             (SELECT DISTINCT LocationMaster.LocationName
                               FROM            ASQAllocationMaster INNER JOIN
                                                         LocationMaster ON ASQAllocationMaster.Locaion_PK = LocationMaster.Location_PK
                               WHERE        (ASQAllocationMaster.PoPackId = tt.PoPackId) AND (ASQAllocationMaster.OurStyleId = tt.OurStyleID))) AS LocationName, IsShortClosed, FOB, Description, BuyerName, AtcNum
FROM            (SELECT        PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, SUM(POPackDetails_1.PoQty) AS POQty, ISNULL
                                                        ((SELECT        SUM(ShipmentHandOverDetails.ShippedQty) AS Expr1
                                                            FROM            ShipmentHandOverDetails INNER JOIN
                                                                                     JobContractDetail ON ShipmentHandOverDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk
                                                            GROUP BY JobContractDetail.PoPackID, JobContractDetail.OurStyleID
                                                            HAVING        (JobContractDetail.PoPackID = PoPackMaster.PoPackId) AND (JobContractDetail.OurStyleID = POPackDetails_1.OurStyleID)), 0) AS ShipedQty, AtcDetails.OurStyleID, 
                                                    PoPackMaster.FirstDeliveryDate, PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, MAX(POPackDetails_1.IsCutable) AS IsCutable, PoPackMaster.SeasonName, 
                                                    LocationMaster_1.LocationName, AtcDetails.FOB, MAX(POPackDetails_1.IsShortClosed) AS IsShortClosed, AtcMaster.AtcNum, CountryMaster.Description, BuyerMaster.BuyerName
                          FROM            PoPackMaster INNER JOIN
                                                    POPackDetails AS POPackDetails_1 ON PoPackMaster.PoPackId = POPackDetails_1.POPackId INNER JOIN
                                                    AtcDetails ON POPackDetails_1.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                                                    AtcMaster ON PoPackMaster.AtcId = AtcMaster.AtcId INNER JOIN
                                                    CountryMaster ON AtcMaster.ProductionCountryID = CountryMaster.CountryID INNER JOIN
                                                    BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID LEFT OUTER JOIN
                                                    LocationMaster AS LocationMaster_1 ON AtcMaster.ProductionCountryID = LocationMaster_1.CountryID AND PoPackMaster.ExpectedLocation_PK = LocationMaster_1.Location_PK
                          GROUP BY PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, POPackDetails_1.OurStyleID, AtcDetails.OurStyleID, 
                                                    PoPackMaster.FirstDeliveryDate, PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, PoPackMaster.SeasonName, LocationMaster_1.LocationName, AtcDetails.FOB, 
                                                    AtcMaster.AtcNum, CountryMaster.Description, BuyerMaster.BuyerName) AS tt) AS ttt
WHERE        (BuyerName = @BuyerName) AND (DeliveryDate BETWEEN @fromdate AND @todate)";



                cmd.Parameters.AddWithValue("@BuyerName", BuyerName);
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);


                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
            }

            return dt;
        }




        public static DataTable getProductionDataofSelectedAtcWithinPeriod(string Condition, DateTime fromdate, DateTime todate)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        ttt.PoPackId, ttt.SeasonName, ttt.BuyerName, ttt.AtcNum, ttt.PoPacknum, ttt.BuyerPO, ttt.OurStyle, ttt.BuyerStyle, ttt.POQty, ttt.POQty * ttt.FOB AS POvalue, ttt.ShipedQty, ttt.ShipedQty * ttt.FOB AS ShippedValue, 
                         ttt.Balance, ttt.Balance* ttt.FOB AS BalanceValue, CONVERT(DATE, ttt.FirstDeliveryDate, 101) AS FirstDeliveryDate, CONVERT(DATE, ttt.HandoverDate, 101) AS HandoverDate, CONVERT(DATE, ttt.DeliveryDate,

                        101) AS DeliveryDate, ttt.Iscuttable, ttt.LocationName, ttt.IsShortClosed, ttt.FOB, CountryMaster.Description AS Country, ttt.OurStyleID
FROM            CountryMaster INNER JOIN
                        LocationMaster AS LocationMaster_2 ON CountryMaster.CountryID = LocationMaster_2.CountryID RIGHT OUTER JOIN
                            (SELECT        tt.PoPackId, tt.OurStyleID, tt.SeasonName, BuyerMaster.BuyerName, AtcMaster.AtcNum, tt.PoPacknum, tt.BuyerPO, tt.OurStyle, tt.BuyerStyle, tt.POQty, tt.ShipedQty,
                                                        tt.POQty - tt.ShipedQty AS Balance, tt.FirstDeliveryDate, tt.HandoverDate, tt.DeliveryDate, ISNULL(tt.IsCutable, 'N') AS Iscuttable, ISNULL(tt.LocationName,
                                                            (SELECT DISTINCT LocationMaster.LocationName

                                                              FROM            ASQAllocationMaster INNER JOIN

                                                                                        LocationMaster ON ASQAllocationMaster.Locaion_PK = LocationMaster.Location_PK

                                                              WHERE(ASQAllocationMaster.PoPackId = tt.PoPackId) AND(ASQAllocationMaster.OurStyleId = tt.OurStyleID))) AS LocationName, tt.IsShortClosed, tt.FOB
                               FROM(SELECT        PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, SUM(POPackDetails_1.PoQty) AS POQty, ISNULL
                                                                                       ((SELECT        SUM(ShipmentHandOverDetails.ShippedQty) AS Expr1
                                                                                           FROM            ShipmentHandOverDetails INNER JOIN
                                                                                                                    JobContractDetail ON ShipmentHandOverDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk
                                                                                           GROUP BY JobContractDetail.PoPackID, JobContractDetail.OurStyleID
                                                                                           HAVING(JobContractDetail.PoPackID = PoPackMaster.PoPackId) AND(JobContractDetail.OurStyleID = POPackDetails_1.OurStyleID)), 0) AS ShipedQty, AtcDetails.OurStyleID, 
                                                                                   PoPackMaster.FirstDeliveryDate, PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, MAX(POPackDetails_1.IsCutable) AS IsCutable,
                                                                                   PoPackMaster.SeasonName, LocationMaster_1.LocationName, AtcDetails.FOB, MAX(POPackDetails_1.IsShortClosed) AS IsShortClosed
                                                         FROM PoPackMaster INNER JOIN
                                                                                   POPackDetails AS POPackDetails_1 ON PoPackMaster.PoPackId = POPackDetails_1.POPackId INNER JOIN
                                                                                   AtcDetails ON POPackDetails_1.OurStyleID = AtcDetails.OurStyleID LEFT OUTER JOIN
                                                                                   LocationMaster AS LocationMaster_1 ON PoPackMaster.ExpectedLocation_PK = LocationMaster_1.Location_PK
                                                         GROUP BY PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, POPackDetails_1.OurStyleID, AtcDetails.OurStyleID, 
                                                                                   PoPackMaster.FirstDeliveryDate, PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, PoPackMaster.SeasonName, LocationMaster_1.LocationName, 
                                                                                   AtcDetails.FOB) AS tt INNER JOIN
                                                         AtcMaster ON tt.AtcId = AtcMaster.AtcId INNER JOIN
                                                         BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID) AS ttt ON LocationMaster_2.LocationName = ttt.LocationName
WHERE(ttt.DeliveryDate BETWEEN @fromdate AND @todate) "+ Condition;



                cmd.Parameters.AddWithValue("@BuyerName", Condition);
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);


                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
            }

            return dt;
        }

    }
}