using ArtWebApp.Areas.ProductionMVC.Viewmodel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.ProductionMVC
{
    public class ProductionRepo
    {


        public AsqTrackerModel GetProductionTNAData()
        {
            AsqTrackerModel asqTrackerModel = new AsqTrackerModel();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"ASQTracker_SP";
                cmd.CommandType = CommandType.StoredProcedure;

               

                DataTable dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {






                        asqTrackerModel.AsqData = dt;










                    }
                }




            }

            return asqTrackerModel;
        }


    }


    public class ProductionReportRepo
    {


        public ReportDataModel GetBEpercentReport(String Month)
        {
            ReportDataModel reportDataModel = new ReportDataModel();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"BEpercentReport_SP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Month", Month);


                DataTable dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {






                        reportDataModel.AsqData = dt;










                    }
                }




            }

            return reportDataModel;
        }

        public DataTable GetShipmentofAtc( int atcid=0)
        {
            DataTable dt = new DataTable();

         




                using (SqlCommand cmd = new SqlCommand(@"ShipmentofAtc_SP"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@atcid", atcid);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }

              




              

               



           
            return dt;
        }



        public DataTable GetRejectionRequest(int atcid = 0)
        {
            DataTable dt = new DataTable();






            using (SqlCommand cmd = new SqlCommand(@"RejectionRequest_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@atcid", atcid);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }













            return dt;
        }



        




        public DataTable GetDObetweenDate(DateTime fromdate,DateTime todate,String dotype)
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


    }
}