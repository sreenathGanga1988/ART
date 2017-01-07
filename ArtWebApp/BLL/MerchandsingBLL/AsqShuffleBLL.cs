using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

using System.Configuration;
using ArtWebApp.DataModelAtcWorld;

namespace ArtWebApp.BLL.MerchandsingBLL
{
    public class AsqShuffleBLL
    {

        public DataTable GetAllPOPackDataofStyleandPopack(int popackid, int ourstyleid)
        {
            DataTable dt = new DataTable();



            DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
            dt = pktrans.GetPOPackDetailsofaStyle(popackid, ourstyleid);



            return dt;
        }

        public DataTable GetAllPOPackDataofStyleandPopack(int ourstyleid, ArrayList Popackdetlist)
        {
            DataTable dt = new DataTable();



            DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
            dt = pktrans.GetPOPACKDetailsofList(ourstyleid, Popackdetlist);



            return dt;
        }
    }
    public class AsqShuffleMasterData
    {



        public int ourstyleid { get; set; }
        public int AsqShuffle_PK { get; set; }
        public int FromPOPackID { get; set; }
        public int AddedBY { get; set; }
        public int AddedDate { get; set; }
        public int IsApproved { get; set; }
        public int ApprovedBy { get; set; }
        public int AsqShuffleNum { get; set; }
        public int ApprovedDate { get; set; }


        public List<ASQShuffleDetailsData> ASQShuffleDetailsDataCollection { get; set; }
        public string insertasqshufflemaster()
        {
            string asqshuffle = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                ASQShuffleMaster ctmstr = new ASQShuffleMaster();

                ctmstr.OurStyleID = this.ourstyleid;
                ctmstr.FromPOPackID = this.FromPOPackID;

                ctmstr.IsApproved = "N";
                ctmstr.AddedBY = HttpContext.Current.Session["Username"].ToString();
                ctmstr.AddedDate = DateTime.Now;

                enty.ASQShuffleMasters.Add(ctmstr);

                enty.SaveChanges();

                asqshuffle = ctmstr.AsqShuffleNum = "ASH" + ctmstr.AsqShuffle_PK.ToString().PadLeft(6, '0');

                using (DataModelAtcWorld.AtcWorldEntities enttty = new DataModelAtcWorld.AtcWorldEntities())
                {

                    ASQShuffleMasterAtc atcproctmstr = new DataModelAtcWorld.ASQShuffleMasterAtc();

                    atcproctmstr.OurStyleID = this.ourstyleid;
                    atcproctmstr.FromPOPackID = this.FromPOPackID;

                    atcproctmstr.IsApproved = "N";
                    atcproctmstr.AddedBY = HttpContext.Current.Session["Username"].ToString();
                    atcproctmstr.AddedDate = DateTime.Now;
                    ctmstr.AsqShuffleNum = asqshuffle;
                   enttty.ASQShuffleMasterAtcs.Add(atcproctmstr);

                    enttty.SaveChanges();
                }

                foreach (ASQShuffleDetailsData rdet in this.ASQShuffleDetailsDataCollection)
                {
                    ASQShuffleDetail wrngdet = new DataModels.ASQShuffleDetail();
                    wrngdet.OurStyleID = rdet.OurStyleID;
                    wrngdet.ToPOPackDet_PK = rdet.ToPOPackDet_PK;
                    wrngdet.AddedQty = rdet.AddedQty;
                    wrngdet.AsqShuffle_PK = ctmstr.AsqShuffle_PK;


                    //  var popackdetialpk = enty.POPackDetails.Where(u => u.ColorName == rdet.colorname && u.SizeName == rdet.sizename && u.POPackId == ctmstr.FromPOPackID && u.OurStyleID == ctmstr.OurStyleID).Select(u => u.PoPack_Detail_PK).FirstOrDefault();
                    wrngdet.FromPOPackDet_PK = int.Parse(rdet.FromPOPackDet_PK.ToString());

                    enty.ASQShuffleDetails.Add(wrngdet);

                    using (DataModelAtcWorld.AtcWorldEntities enttty = new DataModelAtcWorld.AtcWorldEntities())
                    {
                        ASQShuffleDetailsAtc Propkdet1 = new ASQShuffleDetailsAtc();

                        Propkdet1.OurStyleID = rdet.OurStyleID;
                        Propkdet1.ToPOPackDet_PK = rdet.ToPOPackDet_PK;
                        Propkdet1.AddedQty = rdet.AddedQty;
                        Propkdet1.AsqShuffle_PK = ctmstr.AsqShuffle_PK;



                        Propkdet1.FromPOPackDet_PK = int.Parse(rdet.FromPOPackDet_PK.ToString());

                        enttty.ASQShuffleDetailsAtcs.Add(Propkdet1);
                        enttty.SaveChanges();
                    }

                }

               

                enty.SaveChanges();

            }
       
            return asqshuffle;


        }
    }


    public class ASQShuffleDetailsData
    {
        public int AsqShuffleDet_PK { get; set; }
        public int AsqShuffle_PK { get; set; }
        public int OurStyleID { get; set; }
        public int FromPOPackDet_PK { get; set; }
        public int ToPOPackDet_PK { get; set; }
        public int AddedQty { get; set; }


        public string colorcode { get; set; }
        public string colorname { get; set; }
        public string sizecode { get; set; }
        public string sizename { get; set; }

    }
}