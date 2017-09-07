using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.DBTransaction.AccountsTrranscation
{
    public static class AccountsTransaction
    {

        public static System.Data.DataTable ExternalSalesofMonth (int year, int month)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "PrePonedPOsofMonth_SP";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);

            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



        }


    }


    public static class DebitnoteAgainstASNShortage
    {


        public static DataTable getASNWithShortage()
        {

            DataTable dt = new DataTable();

            return dt;
         }

    }
}