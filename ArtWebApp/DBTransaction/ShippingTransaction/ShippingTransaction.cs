using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.DBTransaction.ShippingTransaction
{
    public  static  class ShippingTransaction
    {
      static  String   connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;

        public static DataTable GetIncompletedShipmenthandover(int locPk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT ShipmentHandMaster_PK, ShipmentHandOverCode, IsCompleted, Location_Pk
FROM ShipmentHandOverMaster
WHERE(Location_Pk = @Param1) AND (IsCompleted = N'N')", con);


                cmd.Parameters.AddWithValue("@Param1", locPk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

    }
}