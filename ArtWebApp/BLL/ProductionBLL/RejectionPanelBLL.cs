using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.ProductionBLL
{
    public class RejectionPanelBLL
    {
    }

    public class RejectReqMasterData
    {
        public int AtcID { get; set; }
        public int Location_PK { get; set; }
        public String AddedBY { get; set; }
        public String reqnum { get; set; }
        public int AddedDate { get; set; }

        public List<RejectReqDetailsData> RejectReqDetailsDataCollection { get; set; }



        public String InsertFullgarmentRejectionExtraRequest()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                RejectReqMaster lsmstr = new RejectReqMaster();
                lsmstr.AtcID = this.AtcID;
                lsmstr.Location_PK = this.Location_PK;            
           
              
                lsmstr.AddedBY = HttpContext.Current.Session["Username"].ToString();
                lsmstr.AddedDate = DateTime.Now;
                lsmstr.IsAdjusted = false;
                lsmstr.RejectionType = "F";
                enty.RejectReqMasters.Add(lsmstr);
                enty.SaveChanges();
                Cutn = "FGR" + lsmstr.RejReqMasterID;
                lsmstr.Reqnum = Cutn;


  
           
                enty.SaveChanges();

        



                foreach (RejectReqDetailsData di in this.RejectReqDetailsDataCollection)
                {
                    RejectReqDetail lcdet = new RejectReqDetail();
                    lcdet.RejFabReqID = di.RejFabReqID;
                    lcdet.AllowedQty = di.AllowedQty;
                    lcdet.RejReqMasterID = lsmstr.RejReqMasterID;
                    enty.RejectReqDetails.Add(lcdet);



                    var qlayroll = from rlldata in enty.RejectionExtraFabbReqs
                                   where rlldata.RejFabReqID == di.RejFabReqID
                                   select rlldata;
                    foreach (var element1 in qlayroll)
                    {
                        element1.IsApproved = true;
                    }





                }





                enty.SaveChanges();



            }

            return Cutn;

        }


        public String InsertPanelRejectionExtraRequest()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                RejectReqMaster lsmstr = new RejectReqMaster();
                lsmstr.AtcID = this.AtcID;
                lsmstr.Location_PK = this.Location_PK;


                lsmstr.AddedBY = HttpContext.Current.Session["Username"].ToString();
                lsmstr.AddedDate = DateTime.Now;
                lsmstr.IsAdjusted = false;
                lsmstr.RejectionType = "P";
                enty.RejectReqMasters.Add(lsmstr);
                enty.SaveChanges();
                Cutn = "PR" + lsmstr.RejReqMasterID;
                lsmstr.Reqnum = Cutn;




                enty.SaveChanges();





                foreach (RejectReqDetailsData di in this.RejectReqDetailsDataCollection)
                {
                    RejectReqDetail lcdet = new RejectReqDetail();
                    lcdet.RejFabReqID = di.RejFabReqID;
                    lcdet.AllowedQty = di.AllowedQty;
                    lcdet.RejReqMasterID = lsmstr.RejReqMasterID;
                    enty.RejectReqDetails.Add(lcdet);



                    var qlayroll = from rlldata in enty.RejectionExtraFabbReqs
                                   where rlldata.RejFabReqID == di.RejFabReqID
                                   select rlldata;
                    foreach (var element1 in qlayroll)
                    {
                        element1.IsApproved = true;
                    }





                }





                enty.SaveChanges();



            }

            return Cutn;

        }
    }

    public class RejectReqDetailsData
    {
        public int RejFabReqID { get; set; }
        public Decimal AllowedQty { get; set; }
        public int RejReqMasterID { get; set; }
       
    }


    public static class RejectionPanelFunction
    {





      








        public static DataTable GetPendingRejectionRequest(int OurStyleID, int Location_PK)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = @"SELECT        RejectionExtraFabbReq.Fabreqid, RejectionExtraFabbReq.Fabreqno, RejectionExtraFabbReq.RejFabReqID, RejectionExtraFabbReq.Reqdate, RejectionExtraFabbReq.DepartmentName, 
                         RejectionExtraFabbReq.ReqQty, POPackDetails.ColorName, AtcDetails.OurStyle, LocationMaster.LocationName, 0.0 AS Allowedfabric, RejectionExtraFabbReq.IsApproved, AtcDetails.OurStyleID, 
                         RejectionExtraFabbReq.Location_PK
FROM            RejectionExtraFabbReq INNER JOIN
                         POPackDetails ON RejectionExtraFabbReq.PoPack_Detail_PK = POPackDetails.PoPack_Detail_PK INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         LocationMaster ON RejectionExtraFabbReq.Location_PK = LocationMaster.Location_PK
WHERE        (RejectionExtraFabbReq.IsApproved = 0) AND (AtcDetails.OurStyleID = @OurStyleID) AND (RejectionExtraFabbReq.Location_PK = @Location_PK)";

                cmd.Parameters.AddWithValue("@OurStyleID", OurStyleID);
                cmd.Parameters.AddWithValue("@Location_PK", Location_PK);


                return QueryFunctions.ReturnQueryResultDatatable(cmd);

            }


        }


        public static void SendLaysheetDetailstokenya(int laysheetPK)
        {






        }










    }


}