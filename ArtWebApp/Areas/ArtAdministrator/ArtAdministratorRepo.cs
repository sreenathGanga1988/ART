using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtWebApp.DataModelAtcWorld;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ArtWebApp.Areas.ArtAdministrator
{
    public static class ArtAdministratorRepo
    {


        public static void UpdateCostperminute()
        {

            using (AtcWorldEntities atcenty = new AtcWorldEntities())
            {
                using (ArtEntitiesnew artenty = new DataModels.ArtEntitiesnew())
                {

                    var q = (from loctation in atcenty.FactoryCostMasters
                             select loctation).ToList();


                    foreach (var element in q)
                    {

                        var artLocation = from loctation in artenty.LocationMasters
                                          where loctation.Location_PK == element.LocationMaster_tbl.ArtLocation_PK
                                          select loctation;

                        foreach (var artloc in artLocation)
                        {


                            artloc.CostPerMinute = element.CostMinute / 100;


                        }




                    }

                    artenty.SaveChanges();

                }


            }

        }






        public static void UpDateTTL()
        {
            DataTable dt = GetTTL();


            using (ArtEntitiesnew artenty = new DataModels.ArtEntitiesnew())
            {



                
                foreach (DataRow row in dt.Rows)
                {
                    int ourstyleid = int.Parse(row["OurStyleID"].ToString());

                    Decimal TTLSam = Decimal.Parse(row["TTLSam"].ToString());
                    var q = from atcdet in artenty.AtcDetails
                            where atcdet.OurStyleID == ourstyleid
                            select atcdet;
                    foreach (var element in q)
                    {


                        element.TTLSam = TTLSam;



                    }

                }





                artenty.SaveChanges();

            }




        }







        public static DataTable GetTTL()
        {
            String Qry = @"select isnull( Sum(valueminute),0) AS TTLSAM ,OurStyleID   from ObTarget_tbl GROUP bY OurStyleID";
            return QueryFunctions.ReturnQueryResultDatatablefromAtcWorldkENYA(Qry);

        }








        public static string BackUpDB()
        {
            String ErrorText = "";

            String ConnectionString = (ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString());
            string destdir = "D:\\backupdb";
            if (!System.IO.Directory.Exists(destdir))
            {
                System.IO.Directory.CreateDirectory("C:\\backupdb");
            }


            using (SqlConnection con = new SqlConnection ())
            {
                using(SqlCommand cmd= new SqlCommand())
                {
                    try
                    {
                        cmd.CommandText = "backup database UsersDB to disk = '" + destdir + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".Bak'";
                        con.ConnectionString = ConnectionString;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ErrorText = "SucessFully Updated";
                    }
                    catch (Exception ex)
                    {
                        ErrorText ="Error During backup database!"+ Environment.NewLine+ex.Message;
                    }
                   
                }


            }

            return ErrorText;
        }









    }








}