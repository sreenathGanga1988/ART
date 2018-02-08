using ArtWebApp.DataModels;
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


        public DataTable GetFabriclocationGroup(int Atcid)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"CreateLocationfabricDetails_SP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AtcId", Atcid);


                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        using (ArtWebApp.DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
                        {



                            foreach (DataRow row in dt.Rows)
                            {

                                int skudetpk = int.Parse(row["SkuDet_PK"].ToString());
                                int locationpk = int.Parse(row["Location_pk"].ToString());
                                int ourstyleid = int.Parse(row["OurStyleID"].ToString());

                                if (!enty.FabricInLocation_tbl.Any(f => f.SkuDet_PK == skudetpk && f.OurStyleId == ourstyleid && f.Location_pk == locationpk))
                                {


                                    FabricInLocation_tbl fabricInLocation_Tbl = new FabricInLocation_tbl();

                                    fabricInLocation_Tbl.AtcId = int.Parse(row["AtcId"].ToString());
                                    fabricInLocation_Tbl.OurStyleId = int.Parse(row["OurStyleID"].ToString());
                                    fabricInLocation_Tbl.Location_pk = int.Parse(row["Location_pk"].ToString());
                                    fabricInLocation_Tbl.SkuDet_PK = int.Parse(row["SkuDet_PK"].ToString());
                                    fabricInLocation_Tbl.ColorName = row["ColorName"].ToString();
                                    fabricInLocation_Tbl.ColorCode = row["ColorCode"].ToString();
                                    fabricInLocation_Tbl.ItemColor = row["ItemColor"].ToString();
                                    fabricInLocation_Tbl.LocationName = row["LocationName"].ToString();
                                    fabricInLocation_Tbl.ITemdEscription = row["ITemdEscription"].ToString();
                                    fabricInLocation_Tbl.Description = row["Description"].ToString();
                                    fabricInLocation_Tbl.IsClosed = "N";
                                    fabricInLocation_Tbl.Status = "Open";

                                    enty.FabricInLocation_tbl.Add(fabricInLocation_Tbl);


                                }

                                enty.SaveChanges();

                            }
















                        }
                    }




                }
              
            }

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = @"FabricoflocationOfAtc_SP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AtcId", Atcid);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }
            return dt;










        }
    }
}