using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Inventory
{
    public class InventoryReportRepo
    {


        public DataTable GetDObetweenDate(DateTime fromdate, DateTime todate, String dotype)
        {
            DataTable dt = new DataTable();

            
            using (SqlCommand cmd = new SqlCommand(@"GetDeliveryDosBetweenDates_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);
                cmd.Parameters.AddWithValue("@dotype", dotype);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }
            


            return dt;
        }



        public DataTable GetROPending(DateTime fromdate, DateTime todate, int Location_pk)
        {
            DataTable dt = new DataTable();


            using (SqlCommand cmd = new SqlCommand(@"GetPENDINGROBetweenDates_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);
                cmd.Parameters.AddWithValue("@Location_pk", Location_pk);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }



            return dt;
        }




        public DataTable GetRollTrackData(int supplierdock_pk, int Skudetpk, int RollPk ,int cutplanPk,string docnum)
        {
            DataTable dt = new DataTable();


            using (SqlCommand cmd = new SqlCommand(@"RollTracker_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@supplierdock_pk", supplierdock_pk);
                cmd.Parameters.AddWithValue("@Skudetpk", Skudetpk);
                cmd.Parameters.AddWithValue("@RollPk", RollPk);
                cmd.Parameters.AddWithValue("@cutplanPk", cutplanPk);
                cmd.Parameters.AddWithValue("@docnum", docnum);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }



            return dt;
        }

        public DataTable GetGstockData(int locationpk)
        {
            DataTable dt = new DataTable();


            using (SqlCommand cmd = new SqlCommand(@"GstockWithTransferData_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location_Pk", locationpk);
                
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }



            return dt;
        }




        public DataTable GetLocationWiseRoll(int Atc_id)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            using (SqlCommand cmd = new SqlCommand(@"SELECT        SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, ISNULL(Template_Master.Description, '') + ' ' + ISNULL(SkuRawMaterialMaster.Composition, '') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, '') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ColorCode, '') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, '') AS Description, SkuRawmaterialDetail.UnitRate, SkuRawmaterialDetail.Dependency, RollInventoryMaster.Location_Pk, 
                         RollInventoryMaster.IsPresent, LocationMaster.LocationName, SUM(FabricRollmaster.AYard) AS AYard, COUNT(FabricRollmaster.Roll_PK) AS RollCount, SkuRawMaterialMaster.Atc_id
,SkuRawmaterialDetail.ColorCode FROM            RollInventoryMaster INNER JOIN
                         FabricRollmaster ON RollInventoryMaster.Roll_PK = FabricRollmaster.Roll_PK INNER JOIN
                         LocationMaster ON RollInventoryMaster.Location_Pk = LocationMaster.Location_PK INNER JOIN
                         SkuRawmaterialDetail ON FabricRollmaster.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
GROUP BY SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, ISNULL(Template_Master.Description, '') + ' ' + ISNULL(SkuRawMaterialMaster.Composition, '') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, '') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ColorCode, '') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ''), SkuRawmaterialDetail.UnitRate, SkuRawmaterialDetail.Dependency, RollInventoryMaster.Location_Pk, 
                         RollInventoryMaster.IsPresent, LocationMaster.LocationName, SkuRawMaterialMaster.Atc_id,SkuRawmaterialDetail.ColorCode
HAVING        (RollInventoryMaster.IsPresent = N'Y') AND (SkuRawMaterialMaster.Atc_id = @Atc_id)"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Atc_id", Atc_id);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);


                if (dt != null)
                {

                    dt1 = dt.DefaultView.ToTable(true, "SkuDet_PK", "RMNum", "Description", "Dependency", "ColorCode");

                    DataTable locationtable = dt.DefaultView.ToTable(true, "LocationName");


                    foreach(DataRow row in locationtable.Rows)
                    {
                        dt1.Columns.Add(row["LocationName"].ToString());

                    }
                    foreach (DataColumn col in dt1.Columns)
                    {
                        col.ReadOnly = false;

                    }
                    foreach (DataRow row in dt1.Rows)
                    {
                        foreach (DataColumn col in dt1.Columns)
                        {

                            string colname = col.ColumnName;

                          if(colname!= "SkuDet_PK" && colname != "RMNum" && colname != "Description" && colname != "Dependency" && colname != "ColorCode" && colname != "")
                            {

                                int SkuDet_PK = int.Parse(row["SkuDet_PK"].ToString());

                                Decimal onhandbal = 0;
                                Decimal rollcount = 0;
                                try
                                {
                                    var q = dt.Compute("Sum(AYard)", "LocationName ='"+ colname + "' and SkuDet_PK="+ SkuDet_PK + "");
                                    onhandbal = Decimal.Parse(q.ToString());
                                }
                                catch (Exception)
                                {


                                }


                                try
                                {
                                    var q = dt.Compute("Sum(RollCount)", "LocationName ='" + colname + "' and SkuDet_PK=" + SkuDet_PK + "");
                                    rollcount = Decimal.Parse(q.ToString());
                                }
                                catch (Exception)
                                {


                                }




                                row[colname] = onhandbal +"("+ rollcount + "Rolls)";
                            }
                          


                        }

                    }


                }
            }



            return dt1;
        }
        /// <summary>
        /// Used to get transfernote between to dates
        /// </summary>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetInventoryTransferNotesBetweenDate(DateTime fromdate, DateTime todate, String id)
        {
            DataTable dt = new DataTable();


            using (SqlCommand cmd = new SqlCommand(@"GetInventoryTransferNotesBetweenDates_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);
                cmd.Parameters.AddWithValue("@locid", id);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }



            return dt;
        }
        public DataTable GetRoReport()
        {
            DataTable dt = new DataTable();


            using (SqlCommand cmd = new SqlCommand(@"GetROReport_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }



            return dt;
        }

        public DataTable GetStockRoReport()
        {
            DataTable dt = new DataTable();


            using (SqlCommand cmd = new SqlCommand(@"StockRoReport_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }



            return dt;
        }

    }
}