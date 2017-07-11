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





        public String GetASQGroupNumber ( string Popackdetlist,int atcid,string atcnum)
        {
            String groupname = "";

            String tempgroupcount = "";
            int count = 0;


            string qrystring = @"SELECT   top 1     ASQShuffleMaster.ASQShuffleGroup
 FROM            ASQShuffleMaster INNER JOIN
                          ASQShuffleDetails ON ASQShuffleMaster.AsqShuffle_PK = ASQShuffleDetails.AsqShuffle_PK INNER JOIN
                         POPackDetails ON ASQShuffleDetails.ToPOPackDet_PK = POPackDetails.PoPack_Detail_PK
WHERE(ASQShuffleMaster.FromPOPackID in (" + Popackdetlist + ")) or (POPackDetails.POPackId in (" + Popackdetlist + "))";

            try
            {
                groupname = QueryFunctions.ReturnQueryValue(qrystring).ToString();
            }
            catch (Exception)
            {

                groupname="";
            }


            if(groupname=="")
            {


               qrystring = @" SELECT AtcDetails.AtcId, COUNT(ASQShuffleMaster.AsqShuffle_PK) AS Expr1
FROM ASQShuffleMaster INNER JOIN
                         AtcDetails ON ASQShuffleMaster.OurStyleID = AtcDetails.OurStyleID
GROUP BY AtcDetails.AtcId
HAVING(AtcDetails.AtcId = "+ atcid + ")";
                try
                {
                    tempgroupcount = QueryFunctions.ReturnQueryValue(qrystring).ToString();
            }
            catch (Exception)
            {

                    tempgroupcount = "";
            }
            if (tempgroupcount == "")
                {
                    groupname = "GP"+ atcnum + "-" + 1;

                }
                else
                {
                    count = int.Parse(tempgroupcount) +1;

                    groupname = atcnum + "-" + count.ToString();
                }
            }


            return groupname;

        }











        public void ApproveAsqShuffle(int asqShuffle_PK)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                using (DataModelAtcWorld.AtcWorldEntities enttty = new DataModelAtcWorld.AtcWorldEntities())
                {


                      var atcworldartshufflepk = enttty.ASQShuffleMasterAtcs.Where(u => u.ArtAsqShuffle_PK == asqShuffle_PK).Select(u => u.AsqShuffle_PK).FirstOrDefault();

                    int frompopackid = 0;
                    int ourstyleid = 0;
                    var datatoapproveonart = from cstingmstr in enty.ASQShuffleMasters
                                        where cstingmstr.AsqShuffle_PK == asqShuffle_PK
                                        select cstingmstr;

                    foreach (var element123 in datatoapproveonart)
                    {

                        element123.IsApproved = "Y";
                        element123.ApprovedBy = HttpContext.Current.Session["Username"].ToString();
                        element123.ApprovedDate = DateTime.Now;
                        frompopackid = int.Parse(element123.FromPOPackID.ToString());
                        ourstyleid = int.Parse(element123.OurStyleID.ToString());

                    }

                    var datatoapproveonatcworld = from cstingmstr in enttty.ASQShuffleMasterAtcs
                                        where cstingmstr.AsqShuffle_PK == atcworldartshufflepk
                                                  select cstingmstr;

                    foreach (var element123 in datatoapproveonatcworld)
                    {

                        element123.IsApproved = "Y";
                        element123.ApprovedBy = HttpContext.Current.Session["Username"].ToString();
                        element123.ApprovedDate = DateTime.Now;

                    }

                    enty.SaveChanges();
                    enttty.SaveChanges();
                        int frompopackdet_pk = 0;
                    int topopackdet_pk = 0;
                    Decimal adjustingQty = 0;
                    var q = (from asdetail in enty.ASQShuffleDetails
                            where asdetail.AsqShuffle_PK == asqShuffle_PK
                            select asdetail).ToList();

                    foreach(var element in q)
                    {

                        frompopackdet_pk = int.Parse( element.FromPOPackDet_PK.ToString());
                        topopackdet_pk = int.Parse(element.ToPOPackDet_PK.ToString());

                        var frompopackdetupdate = from Pckdet in enty.POPackDetails
                                                  where Pckdet.PoPack_Detail_PK == frompopackdet_pk
                                                  select Pckdet;

                        foreach(var frompopack in frompopackdetupdate)
                        {
                            frompopack.PoQty = frompopack.PoQty - element.AddedQty;
                        }

                        var topopackupdate = from Pckdet in enty.POPackDetails
                                                  where Pckdet.PoPack_Detail_PK == topopackdet_pk
                                                  select Pckdet;

                        foreach (var topopack in topopackupdate)
                        {
                            topopack.PoQty =  element.revisedQtyofToPOPackDet_PK;
                        }



                        var updateatcworldkenyafrom = from Pckdet in enttty.ASQAllocationMaster_tbl
                                                  where Pckdet.PoPack_Detail_PK == frompopackdet_pk
                                                  select Pckdet;
                        foreach (var frompopackatcworld in updateatcworldkenyafrom)
                        {
                            frompopackatcworld.Qty = frompopackatcworld.Qty - element.AddedQty;
                        }

                        var updateatcworldkenyato= from Pckdet in enttty.ASQAllocationMaster_tbl
                                                      where Pckdet.PoPack_Detail_PK == topopackdet_pk
                                                   select Pckdet;
                        foreach (var frompopackatcworld in updateatcworldkenyato)
                        {
                            frompopackatcworld.Qty = element.revisedQtyofToPOPackDet_PK;
                        }

                        enttty.SaveChanges();
                        enty.SaveChanges();
                    }



                    var newpopackidqty = enty.POPackDetails.Where(i => i.POPackId== frompopackid && i.OurStyleID== ourstyleid).Select(i => i.PoQty).DefaultIfEmpty(0).Sum();
                    var datatoapproveonartlast = from cstingmstr in enty.ASQShuffleMasters
                                             where cstingmstr.AsqShuffle_PK == asqShuffle_PK
                                             select cstingmstr;

                    foreach (var element123 in datatoapproveonartlast)
                    {
                        element123.RevisedFromQty = Decimal.Parse(newpopackidqty.ToString ()); 
                        

                    }

                    var datatoapproveonatcworldlast = from cstingmstr in enttty.ASQShuffleMasterAtcs
                                                  where cstingmstr.AsqShuffle_PK == atcworldartshufflepk
                                                  select cstingmstr;

                    foreach (var element123 in datatoapproveonatcworldlast)
                    {

                        element123.RevisedFromQty = Decimal.Parse(newpopackidqty.ToString()); 

                    }

                    enttty.SaveChanges();
                }
             
                enty.SaveChanges();
            }

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
        public String asqgroup { get; set; }

        public List<ASQShuffleDetailsData> ASQShuffleDetailsDataCollection { get; set; }
        public string insertasqshufflemaster()
        {
            string asqshuffle = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                ASQShuffleMaster ctmstr = new ASQShuffleMaster();

                ctmstr.OurStyleID = this.ourstyleid;
                ctmstr.FromPOPackID = this.FromPOPackID;
                ctmstr.ASQShuffleGroup = this.asqgroup;
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
                    atcproctmstr.ASQShuffleGroup = this.asqgroup;
                    atcproctmstr.IsApproved = "N";
                    atcproctmstr.AddedBY = HttpContext.Current.Session["Username"].ToString();
                    atcproctmstr.AddedDate = DateTime.Now;
                    atcproctmstr.ArtAsqShuffle_PK = ctmstr.AsqShuffle_PK;
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
                    wrngdet.revisedQtyofToPOPackDet_PK = rdet.newToQty;
                    wrngdet.RevisedQtyofFromPOPackDet_PK = (0 - rdet.AddedQty);

                    //  var popackdetialpk = enty.POPackDetails.Where(u => u.ColorName == rdet.colorname && u.SizeName == rdet.sizename && u.POPackId == ctmstr.FromPOPackID && u.OurStyleID == ctmstr.OurStyleID).Select(u => u.PoPack_Detail_PK).FirstOrDefault();
                    wrngdet.FromPOPackDet_PK = int.Parse(rdet.FromPOPackDet_PK.ToString());

                    enty.ASQShuffleDetails.Add(wrngdet);

                    using (DataModelAtcWorld.AtcWorldEntities enttty = new DataModelAtcWorld.AtcWorldEntities())
                    {
                        ASQShuffleDetailsAtc Propkdet1 = new ASQShuffleDetailsAtc();

                        Propkdet1.OurStyleID = rdet.OurStyleID;
                        Propkdet1.ToPOPackDet_PK = rdet.ToPOPackDet_PK;
                        Propkdet1.AddedQty = rdet.AddedQty;
                        Propkdet1.AsqShuffle_PK = wrngdet.AsqShuffle_PK;
                        Propkdet1.RevisedQtyofToPOPackDet_PK = rdet.newToQty;
                        Propkdet1.FromPOPackDet_PK = int.Parse(rdet.FromPOPackDet_PK.ToString());
                        Propkdet1.ArtAsqShuffleDet_PK = wrngdet.AsqShuffleDet_PK;
                        Propkdet1.RevisedQtyofFromPOPackDet_PK = (0 - rdet.AddedQty);

                       

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
        public int newToQty { get; set; }

        public string colorcode { get; set; }
        public string colorname { get; set; }
        public string sizecode { get; set; }
        public string sizename { get; set; }

    }
}