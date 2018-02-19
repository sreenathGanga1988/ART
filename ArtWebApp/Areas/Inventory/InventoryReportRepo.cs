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



        public DataTable GetRollTrackData(int supplierdock_pk, int Skudetpk, int RollPk ,int cutplanPk)
        {
            DataTable dt = new DataTable();


            using (SqlCommand cmd = new SqlCommand(@"RollTracker_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@supplierdock_pk", supplierdock_pk);
                cmd.Parameters.AddWithValue("@Skudetpk", Skudetpk);
                cmd.Parameters.AddWithValue("@RollPk", RollPk);
                cmd.Parameters.AddWithValue("@cutplanPk", cutplanPk);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }



            return dt;
        }






        

    }
}