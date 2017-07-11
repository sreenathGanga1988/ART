using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.DBTransaction.Productiontransaction
{
    public static class SchedularReportTransaction
    {


        public static System.Data.DataTable getShippedTargetandShorClosedofMonth(int year, int month ,DateTime fromdate ,DateTime todate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetCountryWiseBuyerWiseTargetandShipmentofMonth_SP";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Month", month);
            cmd.Parameters.AddWithValue("@years", year);
            cmd.Parameters.AddWithValue("@fromdate", fromdate);
            cmd.Parameters.AddWithValue("@todate", todate);

            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
         


        }


        public static System.Data.DataTable GetYearShippedReport(int year)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetYearlyBuyerwiseShippedReport_SP";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

           
            cmd.Parameters.AddWithValue("@year", year);
           

            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



        }

        public static System.Data.DataTable GetAtcwiseMonthlyShippedReport(int atcid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AtcWiseShipmentdetailsofAMonth_SP";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@atcid", atcid);
          

            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



        }

        public static System.Data.DataTable GetYearShortClosedReport(int year)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ShortClosedofMonthofYear_SP";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Year", year);


            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



        }


        public static System.Data.DataTable OrderonHandofyear(int year)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetOrderOnhandofYear_SP";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@year", year);


            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

            

        }

        public static System.Data.DataTable OrderonHandofyearFactorywise(int year)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetOrderOnhandofYearFactoryWise_SP";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@year", year);


            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

        

        }

        public static System.Data.DataTable GetBuyerWiseTargetorShippedofyear(int year, int month)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetBuyerWiseTargetorShippedofyear_SP";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@Month", month);

            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



        }

        public static System.Data.DataTable GetASQofMonth(int year, int month)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ASQDataOfMonth_SP";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@Month", month);


            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



        }





        public static System.Data.DataTable AfterSalesprofitabiltySales( DateTime fromdate, DateTime todate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AfterSalesProfitability_SP ";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

          
            cmd.Parameters.AddWithValue("@Fromdate", fromdate);
            cmd.Parameters.AddWithValue("@ToDate", todate);

            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



        }





        public static System.Data.DataTable GetTargetofMonth(int year, int month)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TargetofAMonth_SP";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@MonthNum", month);

            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



        }

        public static System.Data.DataTable GetSalesReportofMonth(DateTime fromdate, DateTime todate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SalesofMonth_SP";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@fromdate", fromdate);
            cmd.Parameters.AddWithValue("@todate", todate);

            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



        }



        public static System.Data.DataTable GetShipmentReportofMonth(DateTime fromdate, DateTime todate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ShipmentofMonth_SP";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@fromdate", fromdate);
            cmd.Parameters.AddWithValue("@todate", todate);

            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



        }





    }
}