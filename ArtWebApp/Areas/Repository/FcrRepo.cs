using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Repository
{
    public class FcrRepo
    {
        public DataTable GetAtcTemplateData(int skudet_pk)
        {
            DataTable dt = new DataTable();

           


                SqlCommand cmd = new SqlCommand(@"GetLaysheetFCR_SP");
              
              
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);

            cmd.CommandType = CommandType.StoredProcedure;


            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd); ;
        }

        public DataTable GetLayShortageData(int skudet_pk)
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@"  SELECT LayAdjustDetails.LayCutAdjustID, LayAdjustDetails.Qty, CutOrderMaster.Cut_NO, LayShortageReqMaster.LayShortageReqCode, CutOrderMaster.OurStyleID, CutOrderMaster.ToLoc
FROM            LayAdjustDetails INNER JOIN
                         CutOrderMaster ON LayAdjustDetails.CutID = CutOrderMaster.CutID INNER JOIN
                         LayShortageReqMaster ON LayAdjustDetails.LayShortageMasterID = LayShortageReqMaster.LayShortageMasterID
WHERE        (CutOrderMaster.SkuDet_pk = @skudet_pk) ");


            cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
           





            return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }

        public DataTable GetRejectionData(int skudet_pk)
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@"  SELECT RejectionAdjustDetails.Qty, RejectReqMaster.Reqnum, RejectReqMaster.RejectionType, CutOrderMaster.Cut_NO, CutOrderMaster.SkuDet_pk,CutOrderMaster.OurStyleID, CutOrderMaster.ToLoc
FROM            RejectionAdjustDetails INNER JOIN
                         RejectReqMaster ON RejectionAdjustDetails.RejReqMasterIDID = RejectReqMaster.RejReqMasterID INNER JOIN
                         CutOrderMaster ON RejectionAdjustDetails.CutID = CutOrderMaster.CutID
WHERE         (CutOrderMaster.SkuDet_pk = @skudet_pk) ");


            cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);

         


            return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }

       
    }
}