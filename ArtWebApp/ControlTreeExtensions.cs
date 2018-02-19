using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ArtWebApp
{
    public static class ControlTreeExtensions
    {
        public static TContainer ClosestContainer<TContainer>(this Control theControl) where TContainer : Control
        {
            if (theControl == null) throw new ArgumentNullException("theControl");

            Control current = theControl.NamingContainer;
            TContainer result = current as TContainer;
            while (current != null && result == null)
            {
                current = current.NamingContainer;
                result = current as TContainer;
            }

            return result;
        }
    }

  
    public static class QuantityValidator
    {
        /// <summary>
        /// returns true if Qty1 is less than QTY2
        /// </summary>
        /// <param name="Qty1"></param>
        /// <param name="Qty2"></param>
        /// <returns></returns>
        public static Boolean ISQuantityLesser(int Qty1,int Qty2)
        {
            Boolean result = false;

            if(Qty1>Qty2)
            {
                result = false;
            }
            else
            {
                result = true;
            }


            return result;
        }

        public static Boolean ISFloatQuantityLesser(float AllowedQty, float EnteredQty)
        {
            Boolean result = false;

            if (EnteredQty > AllowedQty)
            {
                result = false;
            }
            else
            {
                result = true;
            }


            return result;
        }


        public static Boolean ISFloat(String VAlue)
        {
            Boolean result = false;

            try
            {
                float valueflt = float.Parse(VAlue);
                result = true;
            }
            catch (Exception)
            {

                result = false;
            }


            return result;
        }


    }


    public static class QueryFunctions
    {
       
           public static Object  ReturnQueryValue(SqlCommand cmd)
           {
               Object returnValue;
           using(  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString ()))
           {
               


             
               cmd.CommandType = CommandType.Text;
               cmd.Connection = con;
               con.Open();

               returnValue = cmd.ExecuteScalar();
           }
                           
             return returnValue;
           }


        public static Object ReturnQueryValue(String Qry)
        {
            Object returnValue;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString()))
            {



                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText =Qry;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();

                    returnValue = cmd.ExecuteScalar();
                }
            }

            return returnValue;
        }





        public static DataTable ReturnQueryResultDatatable(SqlCommand cmd)
           {
               DataTable dt= new DataTable ();
               using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString()))
               {
                   cmd.CommandType = CommandType.Text;
                   cmd.Connection = con;
                   con.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    dt.Load(rdr);
                }

                   
               }

               return dt;
           }

        public static DataTable ReturnQueryResultDatatable(String Qry)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = Qry;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                    }
                }
            }

            return dt;
        }
        public static DataTable ReturnQueryResultDatatableforSP(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString()))
            {

                cmd.Connection = con;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);
            }

            return dt;
        }





        public static DataTable ReturnQueryResultDatatablefromAtcWorldkENYA(String Qry)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AtcWorldConnectionString"].ConnectionString.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = Qry;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    dt.Load(rdr);
                }
            }

            return dt;
        }











    }





}