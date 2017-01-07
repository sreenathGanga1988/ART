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




        public float GetCutPlies(int CutOrderDet_PK)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select isnull(qty,0)as  NoOfPlies from  ( SELECT        SUM(LaySheetDetails.NoOfPlies) AS qty
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK
GROUP BY LaySheetMaster.CutOrderDet_PK
HAVING        (LaySheetMaster.CutOrderDet_PK = @param1))tt";
            cmd.Parameters.AddWithValue("@param1", CutOrderDet_PK);
            float balqty = float.Parse(QueryFunctions.ReturnQueryValue(cmd).ToString());

            return balqty;
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
        public String LayLength { get; set; }
        public int Location_PK { get; set; }
        public String AddedBY { get; set; }
        public DateTime AddedDate { get; set; }
        public int CutOrderDet_PK { get; set; }
        
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
                lsmstr.AddedBY = HttpContext.Current.Session["Username"].ToString();
                lsmstr.AddedDate = DateTime.Now;
              
                enty.LaySheetMasters.Add(lsmstr);
                enty.SaveChanges();

               




                foreach (LaysheetDetaolsData di in this.LaysheetDetaolsDataCollection)
                {
                    LaySheetDetail lcdet = new LaySheetDetail();
                    lcdet.Roll_PK = di.Roll_PK;
                    lcdet.NoOfPlies = di.NoOfPlies;
                    lcdet.EndBit = di.Balance;
                   
                    lcdet.LaySheet_PK = lsmstr.LaySheet_PK;
                    lcdet.FabUtilized = di.fabqty;
                    enty.LaySheetDetails.Add(lcdet);

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


       

    }

    public class LaysheetDetaolsData
    {
     
        public int LaySheetDet_PK { get; set; }
        public int LaySheet_PK { get; set; }
        public int Roll_PK { get; set; }
        public int NoOfPlies { get; set; }
        public Decimal Balance { get; set; }
        public Decimal ExceSShortage { get; set; }
        public Decimal fabqty { get; set; }
    }

}