using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace ArtWebApp.DBTransaction.ManagementTransaction
{
    public class ManagementReportTransaction
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;


        public DataTable GetProfitabilty()
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = @"GetProfitability_SP";
                cmd.Parameters.AddWithValue("@Reporttype", "All");
                cmd.CommandType = CommandType.StoredProcedure;
                return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



            }

        }



     






    }

}