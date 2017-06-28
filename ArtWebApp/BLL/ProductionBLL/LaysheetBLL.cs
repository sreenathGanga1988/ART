using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ArtWebApp.DataModels;
using System.Collections;

namespace ArtWebApp.BLL.ProductionBLL
{
    public class LaysheetBLL
    {

        public DataTable getRollofaCutorder(int cutid)
        {
            return DBTransaction.LaysheetTransaction.getRolldeliveredagainstACutorder(cutid);
        }
        public DataTable getRollofaCutorderofRollSelected(int cutid)
        {
            return DBTransaction.LaysheetTransaction.getSelectedRolldeliveredagainstACutorderandNotLayed(cutid);
        }

        public DataTable getRollSelectedAgainstALaysheetroll(string laysheetrollref)
        {
            return DBTransaction.LaysheetTransaction.getSelectedRollNotLayedagainstlaysheetref(laysheetrollref);
        }

        public DataTable getRollofaCutorderNotlayed(int cutid,int factid)
        {
            return DBTransaction.LaysheetTransaction.getRolldeliveredagainstACutorderandNotLayed(cutid, factid);
        }



        public float GetCutPlies(int CutOrderDet_PK)
        {
            float balqty = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select isnull(qty,0)as  NoOfPlies from  ( SELECT        SUM(LaySheetDetails.NoOfPlies) AS qty
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK
GROUP BY LaySheetMaster.CutOrderDet_PK
HAVING        (LaySheetMaster.CutOrderDet_PK = @param1))tt";
            cmd.Parameters.AddWithValue("@param1", CutOrderDet_PK);

            var obj = QueryFunctions.ReturnQueryValue(cmd);
            if (obj == null)
            {
                obj = "0";
            }
            else
            {

            }
             balqty = float.Parse(obj.ToString ());

            return balqty;
        }


        public float GetCutPliesFORROLL(int CutOrderDet_PK)
        {
            float balqty = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"
Select isnull((SELECT       isnull(  SUM(NoofPlies),0) as  NoOfPlies
FROM            LaySheetRollMaster
GROUP BY CutOrderDet_PK
HAVING        (CutOrderDet_PK = @param1)),0)";
            cmd.Parameters.AddWithValue("@param1", CutOrderDet_PK);

            var obj = QueryFunctions.ReturnQueryValue(cmd);
            if (obj == null)
            {
                obj = "0";
            }
            else
            {

            }
            balqty = float.Parse(obj.ToString());

            return balqty;
        }




        public ArrayList getcutplanMarkerdata(int CutOrderDet_PK)
        {
            ArrayList ary = new ArrayList();
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from cutplndet in enty.CutPlanMarkerDetails
                        join cutordrdet in enty.CutOrderDetails on cutplndet.CutPlanMarkerDetails_PK equals cutordrdet.CutPlanMarkerDetails_PK
                        where cutordrdet.CutOrderDet_PK==CutOrderDet_PK
                        select new { cutplndet.CutPerPlies,cutplndet.NoOfPlies,cutplndet.Cutreq };

                foreach (var element in q)
                {

                    ary.Add(element.CutPerPlies);
                    ary.Add(element.NoOfPlies);
                    ary.Add(element.Cutreq);
                }
            }

            return ary;
        }
        public void getcutorderSizeRatioofALaysheet()
        {

        }


    }





        public static class  LaySheetfunction
        {

       



        public static  DataTable CalculateAlreadyCut(DataTable dt,float cutplies)
        {

           if(cutplies.ToString ().Trim()!=""&& cutplies.ToString ()!="0" )
            {
                int cutrowinde = 2;

                for(int i=0;i<dt.Columns.Count;i++)
                {
                    dt.Rows[2][i] = cutplies;
                }


            }



            return dt;
        }



      

    }

    public class LaysheetMasterData
    {
        public int LaySheet_PK { get; set; }
        public int atcid { get; set; }
        public int ourstyleid { get; set; }
        public String LayLength { get; set; }
        public int NoofPlies { get; set; }
        public int Location_PK { get; set; }
        public String AddedBY { get; set; }
        public String LayRollRef { get; set; }
        public int LaysheetRollmaster_Pk { get; set; }
        public String cutnum { get; set; }
        public DateTime AddedDate { get; set; }
        public int CutOrderDet_PK { get; set; }
        public int cutid { get; set; }
        public String markernum { get; set; }

        public List<LaysheetDetaolsData> LaysheetDetaolsDataCollection { get; set; }



        public String InsertLaySheet()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                LaySheetMaster lsmstr = new LaySheetMaster();
                lsmstr.LayLength = this.LayLength;
                lsmstr.Location_PK = this.Location_PK;
                lsmstr.CutOrderDet_PK= this.CutOrderDet_PK;
                lsmstr.AtcID = this.atcid;
                lsmstr.OustyleID = this.ourstyleid;
                lsmstr.AddedBY = HttpContext.Current.Session["Username"].ToString();
                lsmstr.AddedDate = DateTime.Now;
                lsmstr.LayCutNum = this.cutnum;
                Cutn = lsmstr.LaySheetNum = "L" + this.LayRollRef;
                enty.LaySheetMasters.Add(lsmstr);
                enty.SaveChanges();

           //     Cutn = lsmstr.LaySheetNum = CodeGenerator.GetUniqueCode("LS", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(lsmstr.LaySheet_PK.ToString()));






                foreach (LaysheetDetaolsData di in this.LaysheetDetaolsDataCollection)
                {
                    LaySheetDetail lcdet = new LaySheetDetail();
                    lcdet.Roll_PK = di.Roll_PK;
                    lcdet.NoOfPlies = di.NoOfPlies;
                    lcdet.EndBit = di.Balance;
                    lcdet.IsRecuttable = di.IsRecuttable;
                    lcdet.LaySheet_PK = lsmstr.LaySheet_PK;
                    lcdet.FabUtilized = di.fabqty;
                    lcdet.BalToCut = di.Balance;
                    lcdet.ExcessOrShort = di.ExceSShortage;
                    lcdet.LaySheetRoll_Pk = di.LaySheetRoll_Pk;
                    enty.LaySheetDetails.Add(lcdet);


                  
                        var qlayroll = from rlldata in enty.LaySheetRollDetails
                                       where rlldata.Roll_PK == di.Roll_PK  && rlldata.LaySheetRoll_Pk==di.LaySheetRoll_Pk
                                       select rlldata;
                        foreach (var element1 in qlayroll)
                    {
                        if (di.IsRecuttable == "Y")
                        {
                            element1.IsUsed = "R";
                            element1.BalanceYardage = di.Balance;
                        }
                        else
                        {
                            element1.IsUsed = "Y";
                            element1.BalanceYardage = 0;
                        }
                     }


                   


                    var q = from roll in enty.FabricRollmasters
                            where roll.Roll_PK == lcdet.Roll_PK
                            select roll;
                    foreach (var element in q)
                    {
                        element.IsCut = "Y";
                    }
                }





                enty.SaveChanges();


              
            }

            return Cutn;

        }


        public String InsertLaySheetRoll()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                //     lsmstr.CutOrderDet_PK = this.CutOrderDet_PK;


                Cutn = "C"+this.cutnum +"/"+ DBTransaction.LaysheetTransaction.getlaysheetnum(this.cutid).ToString() +" /" +this.markernum+"/" + DBTransaction.LaysheetTransaction.getlaysheetnumofcutorder(this.CutOrderDet_PK).ToString();


                LaySheetRollMaster lmmstr = new DataModels.LaySheetRollMaster();
                lmmstr.LayRollRef = Cutn;
                lmmstr.CutID = this.cutid;
                lmmstr.CutOrderDet_PK = this.CutOrderDet_PK;
                lmmstr.NoofPlies = this.NoofPlies;
                enty.LaySheetRollMasters.Add(lmmstr);
                enty.SaveChanges();

                foreach (LaysheetDetaolsData di in this.LaysheetDetaolsDataCollection)
                {

                    if(di.RollStatus=="New")
                    {
                        LaySheetRollDetail lcdet = new LaySheetRollDetail();
                        lcdet.Roll_PK = di.Roll_PK;
                        lcdet.CutOrderDet_PK = this.CutOrderDet_PK;
                        lcdet.Cutid = this.cutid;
                        lcdet.IsUsed = "W";
                        lcdet.Yardage = di.RollAyard;
                        lcdet.BalanceYardage = di.RollAyard;
                        lcdet.LayRollRef = Cutn;
                        lcdet.LaysheetRollmaster_Pk = lmmstr.LaysheetRollmaster_Pk;
                        lcdet.AddedBy = HttpContext.Current.Session["Username"].ToString();
                        lcdet.AddedDate = DateTime.Now;

                        enty.LaySheetRollDetails.Add(lcdet);
                    }
                    else
                    {
                        var q = from lyrll in enty.LaySheetRollDetails
                                where lyrll.Roll_PK == di.Roll_PK && lyrll.IsUsed =="R"
                                select lyrll;
                        foreach(var element in q)
                        {
                            element.IsUsed = "Y";
                            element.BalanceYardage = 0;
                        }
                        LaySheetRollDetail lcdet = new LaySheetRollDetail();
                        lcdet.Roll_PK = di.Roll_PK;
                        lcdet.CutOrderDet_PK = this.CutOrderDet_PK;
                        lcdet.Cutid = this.cutid;
                        lcdet.IsUsed = "W";
                        lcdet.Yardage = di.RollAyard;
                        lcdet.BalanceYardage = di.RollAyard;
                        lcdet.LayRollRef = Cutn;
                        lcdet.LaysheetRollmaster_Pk = lmmstr.LaysheetRollmaster_Pk;
                        lcdet.AddedBy = HttpContext.Current.Session["Username"].ToString();
                        lcdet.AddedDate = DateTime.Now;

                        enty.LaySheetRollDetails.Add(lcdet);

                    }


                  

                 
                }





                enty.SaveChanges();



            }

            return Cutn;

        }


        //only update the laysheet rolls from edit screen
        public String InsertLaySheetRollRollOnly()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                //     lsmstr.CutOrderDet_PK = this.CutOrderDet_PK;

                var q = (from layref in enty.LaySheetRollDetails
                        where layref.LaysheetRollmaster_Pk == this.LaysheetRollmaster_Pk
                        select new { layref.CutOrderDet_PK, layref.Cutid , layref .LayRollRef}).ToList();
                foreach(var element in q)
                {
                    this.cutid = int.Parse( element.Cutid.ToString ());
                    this.CutOrderDet_PK = int.Parse(element.CutOrderDet_PK.ToString());
                    this.LayRollRef = element.LayRollRef;
                }


             

                foreach (LaysheetDetaolsData di in this.LaysheetDetaolsDataCollection)
                {

                    if (di.RollStatus == "New")
                    {
                        LaySheetRollDetail lcdet = new LaySheetRollDetail();
                        lcdet.Roll_PK = di.Roll_PK;
                        lcdet.CutOrderDet_PK = this.CutOrderDet_PK;
                        lcdet.Cutid = this.cutid;
                        lcdet.IsUsed = "W";
                        lcdet.Yardage = di.RollAyard;
                        lcdet.BalanceYardage = di.RollAyard;
                        lcdet.LayRollRef = Cutn;
                        lcdet.LaysheetRollmaster_Pk = this.LaysheetRollmaster_Pk;
                        lcdet.AddedBy = HttpContext.Current.Session["Username"].ToString();
                        lcdet.AddedDate = DateTime.Now;

                        enty.LaySheetRollDetails.Add(lcdet);
                    }
                    else
                    {
                        var q1 = from lyrll in enty.LaySheetRollDetails
                                where lyrll.Roll_PK == di.Roll_PK && lyrll.IsUsed == "R"
                                select lyrll;
                        foreach (var element in q1)
                        {
                            element.IsUsed = "Y";
                            element.BalanceYardage = 0;
                        }
                        LaySheetRollDetail lcdet = new LaySheetRollDetail();
                        lcdet.Roll_PK = di.Roll_PK;
                        lcdet.CutOrderDet_PK = this.CutOrderDet_PK;
                        lcdet.Cutid = this.cutid;
                        lcdet.IsUsed = "W";
                        lcdet.Yardage = di.RollAyard;
                        lcdet.BalanceYardage = di.RollAyard;
                        lcdet.LayRollRef = Cutn;
                        lcdet.LaysheetRollmaster_Pk = this.LaysheetRollmaster_Pk;
                        lcdet.AddedBy = HttpContext.Current.Session["Username"].ToString();
                        lcdet.AddedDate = DateTime.Now;

                        enty.LaySheetRollDetails.Add(lcdet);

                    }





                }





                enty.SaveChanges();
                Cutn = this.LayRollRef;


            }

            return Cutn;

        }


        public string DeleteLaysheetRoll(int layrrollpk)
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from layroll in enty.LaySheetRollDetails
                        where layroll.LaySheetRoll_Pk== layrrollpk
                        select layroll;
                foreach(var element in q)
                {

                    enty.LaySheetRollDetails.Remove(element);
                    Cutn = "Sucess";
                }
                enty.SaveChanges();
            }
            
            return Cutn;
        }

    }

    public class LaysheetDetaolsData
    {
     
        public int LaySheetDet_PK { get; set; }
        public int LaySheet_PK { get; set; }
        public int LaySheetRoll_Pk { get; set; }
        public int Roll_PK { get; set; }
        public int NoOfPlies { get; set; }
        public Decimal Balance { get; set; }
        public Decimal ExceSShortage { get; set; }
        public Decimal fabqty { get; set; }
        public String IsRecuttable { get; set; }
        public decimal RollAyard { get; set; }
        public String RollStatus { get; set; }
        
    }

}