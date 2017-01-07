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
    public class AllocationBLL
    {
        public int PoPackId { get; set; }
        public int Ourstyleid { get; set; }
        public int atcid { get; set; }
        public String Colorname { get; set; }

        public String Sizename { get; set; }
        public String AddedBy { get; set; }
        public String AddedDate { get; set; }
        public Decimal Poqty { get; set; }

        public String Colorcode { get; set; }
        public String Sizecode { get; set; }

        public String Atcnum { get; set; }
        public String Potype { get; set; }
        public String OurStyle { get; set; }
        public String ASQ { get; set; }
        public String BuyerPO { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime Inhousedate { get; set; }


        public String Destination { get; set; }
        public String Garmentcatagory { get; set; }
        public String BuyerStyle { get; set; }
        public String seasonName { get; set; }
        public String ChannelName { get; set; }
        public int ChannelID { get; set; }
        public int BuyerDestination_PK { get; set; }
        public int CategoryID { get; set; }
        public int Season_PK { get; set; }
        public String getColorcode(string Colorname)
        {


            var colorcode = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                colorcode = enty.ColorMasters.Where(u => u.ColorName == Colorname).Select(u => u.ColorCode).FirstOrDefault();
            }
            return colorcode.ToString();
        }
        public String BuyerName { get; set; }
        public int BuyerID { get; set; }


        public int sizeid { get; set; }
        public int coloid { get; set; }


        public String getSizecode(string sizename)
        {

            var Sizecode = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                Sizecode = enty.SizeMasters.Where(u => u.SizeName == sizename).Select(u => u.SizeCode).FirstOrDefault();
            }
            return Sizecode.ToString();


        }




        public int getColorID(string Colorname)
        {


            var colorcode = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                colorcode = enty.ColorMasters.Where(u => u.ColorName == Colorname).Select(u => u.ColorId).Single().ToString();
            }
            return int.Parse(colorcode.ToString());
        }




        public void AllocatePO(int popackid, int ourstyleid, int location_pk)
        {
            using (AtcWorldEntities enty = new ArtWebApp.DataModelAtcWorld.AtcWorldEntities())
            {
                using (ArtEntitiesnew artentty = new DataModels.ArtEntitiesnew())
                {

                    if (!enty.ASQAllocationMaster_tbl.Any(f => f.OurStyleId == ourstyleid && f.POPackID == popackid))
                    {

                        var q = from ppc in artentty.POPackDetails
                                where ppc.OurStyleID == ourstyleid && ppc.POPackId == popackid
                                select ppc;
                        foreach (var element in q.ToList())
                        {

                            ASQAllocationMaster mstr = new DataModels.ASQAllocationMaster();
                            mstr.Locaion_PK = location_pk;
                            mstr.PoPack_Detail_PK = element.PoPack_Detail_PK;
                            mstr.Qty = element.PoQty;
                            mstr.OurStyleId = element.OurStyleID;
                            mstr.PoPackId = element.POPackId;
                            mstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                            mstr.AddedDate = DateTime.Now;

                            artentty.ASQAllocationMasters.Add(mstr);

                            artentty.SaveChanges();





                            ASQAllocationMaster_tbl asqmstr = new ASQAllocationMaster_tbl();
                            asqmstr.OurStyleId = element.OurStyleID;
                            asqmstr.POPackID = element.POPackId;
                            asqmstr.Qty = element.PoQty;
                            asqmstr.SizeName = element.SizeName;
                            asqmstr.ColorName = element.ColorName;
                            asqmstr.ArtLocaion_PK = location_pk;
                            asqmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                            asqmstr.AddedDate = DateTime.Now;
                            asqmstr.ColorCode = element.ColorCode;
                            asqmstr.SizeCode = element.SIzeCode;
                            asqmstr.PoPack_Detail_PK = element.PoPack_Detail_PK;
                            var atclocation_pk = enty.LocationMaster_tbl.Where(u => u.ArtLocation_PK == location_pk).Select(u => u.Location_PK).FirstOrDefault();
                            //   var PoPack_Detail_PK = artentty.POPackDetails.Where(u => u.POPackId == popackid && u.OurStyleID == ourstyleid && u.ColorName == element.ColorName && u.SizeName == popackid).Select(u => u.Location_PK).FirstOrDefault();
                            asqmstr.ColorID = element.ColorId;
                            asqmstr.SizeID = element.SizeID;
                            asqmstr.Location_PK = int.Parse(atclocation_pk.ToString());
                            asqmstr.Atcnum = this.Atcnum;
                            asqmstr.OurStyle = this.OurStyle;
                            asqmstr.ASQ = this.ASQ;
                            asqmstr.BuyerPO = this.BuyerPO;
                            asqmstr.BuyerStyle = this.BuyerStyle;
                            asqmstr.DeliveryDate = this.DeliveryDate;
                            asqmstr.Destination = this.Destination;
                            asqmstr.GarmentCategory = this.Garmentcatagory;
                            asqmstr.Season = this.seasonName;
                            asqmstr.CategoryID = this.CategoryID;
                            asqmstr.ChannelID = this.ChannelID;
                            asqmstr.BuyerDestination_PK = this.BuyerDestination_PK;
                            asqmstr.Season_PK = this.Season_PK;
                            asqmstr.Atc_Id = this.atcid;
                            asqmstr.ChannelName = this.ChannelName;
                            asqmstr.BuyerID = this.BuyerID;
                            asqmstr.BuyerName = this.BuyerName;
                            asqmstr.AllocatedQty = element.PoQty;
                            asqmstr.ArtAllocation_PK = mstr.ASQAllocation_PK;

                            asqmstr.POType = this.Potype;

                            enty.ASQAllocationMaster_tbl.Add(asqmstr);




                        }
                        enty.SaveChanges();

                    }
                }

            }


        }






        public DataTable GetPOPACKMasterofList(ArrayList Popackdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "where";

            for (int i = 0; i < Popackdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " PoPackMaster.PoPackId=" + Popackdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or PoPackMaster.PoPackId=" + Popackdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS ASQ, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.PoPackId, POPackDetails.OurStyleID, AtcDetails.OurStyle, 
                         AtcDetails.BuyerStyle, PoPackMaster.AtcId, PoPackMaster.IsCutable, POPackDetails.IsPackable, CAST(PoPackMaster.DeliveryDate AS date) AS DeliveryDate, PoPackMaster.SeasonName, ISNULL
                             ((SELECT DISTINCT LocationMaster.LocationName
                                 FROM            ASQAllocationMaster INNER JOIN
                                                          LocationMaster ON ASQAllocationMaster.Locaion_PK = LocationMaster.Location_PK
                                 WHERE        (ASQAllocationMaster.POPackID = PoPackMaster.PoPackId) AND (ASQAllocationMaster.OurStyleId = POPackDetails.OurStyleID)), N'NA') AS LocationName, ChannelMaster.ChannelName, 
                         BuyerDestinationMaster.BuyerDestination, AtcMaster.AtcNum, GarmentCategory.CategoryName, GarmentCategory.CategoryID, ChannelMaster.ChannelID, BuyerDestinationMaster.BuyerDestination_PK, 
                         SeasonMaster.Season_PK, BuyerMaster.BuyerID, BuyerMaster.BuyerName
FROM            PoPackMaster INNER JOIN
                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         ChannelMaster ON PoPackMaster.ChannelID = ChannelMaster.ChannelID INNER JOIN
                         BuyerDestinationMaster ON PoPackMaster.BuyerDestination_PK = BuyerDestinationMaster.BuyerDestination_PK INNER JOIN
                         AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId INNER JOIN
                         GarmentCategory ON AtcDetails.CategoryID = GarmentCategory.CategoryID INNER JOIN
                         SeasonMaster ON PoPackMaster.SeasonName = SeasonMaster.SeasonName INNER JOIN
                         BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID  " + condition + @" GROUP BY PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.PoPackId, POPackDetails.OurStyleID, AtcDetails.OurStyle, AtcDetails.BuyerStyle, 
                         PoPackMaster.AtcId, PoPackMaster.IsCutable, POPackDetails.IsPackable, PoPackMaster.DeliveryDate, PoPackMaster.SeasonName, ChannelMaster.ChannelName, BuyerDestinationMaster.BuyerDestination, 
                         AtcMaster.AtcNum, GarmentCategory.CategoryName, GarmentCategory.CategoryID, ChannelMaster.ChannelID, BuyerDestinationMaster.BuyerDestination_PK, SeasonMaster.Season_PK, BuyerMaster.BuyerID, 
                         BuyerMaster.BuyerName
ORDER BY PoPackMaster.PoPackId";
                DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
                dt = pktrans.getPodetails(query);
            }
            return dt;

        }



        public DataTable GetAllAllocatedPO(ArrayList Popackdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "where";

            for (int i = 0; i < Popackdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " ASQAllocationMaster_tbl.PoPackId=" + Popackdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or ASQAllocationMaster_tbl.PoPackId=" + Popackdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        ASQAllocationMaster_tbl.ASQAllocation_PK, ASQAllocationMaster_tbl.Atcnum, ASQAllocationMaster_tbl.OurStyle, ASQAllocationMaster_tbl.ASQ, ASQAllocationMaster_tbl.BuyerPO, 
                         ASQAllocationMaster_tbl.BuyerStyle, ASQAllocationMaster_tbl.DeliveryDate, ASQAllocationMaster_tbl.Destination, ASQAllocationMaster_tbl.GarmentCategory, ASQAllocationMaster_tbl.Season, 
                         ASQAllocationMaster_tbl.ChannelName, ASQAllocationMaster_tbl.ColorName, ASQAllocationMaster_tbl.SizeName, ASQAllocationMaster_tbl.Qty, LocationMaster_tbl.LocationName, 
                         LocationMaster_tbl.Location_PK
FROM            ASQAllocationMaster_tbl INNER JOIN
                         LocationMaster_tbl ON ASQAllocationMaster_tbl.Location_PK = LocationMaster_tbl.Location_PK " + condition + "";
                DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
                dt = pktrans.getPodetails(query);
            }
            return dt;

        }

        /// <summary>
        /// Requery the database to get the size color asq matrix
        /// </summary>
        /// <param name="ourstyleid"></param>
        /// <param name="popackid"></param>
        /// <returns></returns>
        public static DataTable createdatatable(int ourstyleid, int popackid, int location_pk, DataTable datacolorsize)
        {
            DataTable dt = new DataTable();
            int i = 0;
            dt.Columns.Add("Color", typeof(String));


            DataTable newresult = datacolorsize.Select("POPackID = " + popackid + " and OurStyleId =" + ourstyleid + " and Locaion_PK =" + location_pk + "").CopyToDataTable();



            DataView sizeview = new DataView(newresult);
            DataTable distinctsizeValues = sizeview.ToTable(true, "SizeName");

            for (int j = 0; j < distinctsizeValues.Rows.Count; j++)
            {
                dt.Columns.Add(distinctsizeValues.Rows[j]["SizeName"].ToString(), typeof(String));
            }



            DataView colorview = new DataView(newresult);
            DataTable distinctcolorValues = colorview.ToTable(true, "ColorName");

            for (int k = 0; k < distinctcolorValues.Rows.Count; k++)
            {
                dt.Rows.Add();
                dt.Rows[i]["Color"] = distinctcolorValues.Rows[k]["ColorName"].ToString();
                i++;
            }


            if (dt != null)
            {

                if (dt.Rows.Count >= 1)
                {


                    for (int rowcount = 0; rowcount < dt.Rows.Count; rowcount++)
                    {
                        String Colorname = dt.Rows[rowcount]["Color"].ToString().Trim();
                        for (int coloumncount = 1; coloumncount < dt.Columns.Count; coloumncount++)
                        {
                            String Sizename = dt.Columns[coloumncount].ColumnName.ToString().Trim();


                            try
                            {
                                object poqty = newresult.Compute("Sum(Qty)", "ColorName= '" + Colorname + "' and  SizeName ='" + Sizename + "'");

                                if (poqty.ToString().Trim() == "")
                                {
                                    poqty = "0";
                                }

                                dt.Rows[rowcount][coloumncount] = poqty.ToString();
                            }
                            catch
                            {

                            }

                        }

                    }

                }


            }

            //var sum = newresult.Compute("SUM(PoQty)", "");
            //var CONSUMPTION = newresult.Compute("MAX(Consumption)", "");
            //var wastage = newresult.Compute("MAX(WastagePercentage)", "");








            return dt;
        }



        public DataTable GetAllAllocatedPOforPacking(ArrayList Popackdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "where";

            for (int i = 0; i < Popackdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " ASQAllocationMaster.PoPackId=" + Popackdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or ASQAllocationMaster.PoPackId=" + Popackdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        ASQAllocationMaster.Locaion_PK, ASQAllocationMaster.PoPackId, ASQAllocationMaster.OurStyleId, PoPackMaster.AtcId, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, 
                         LocationMaster.LocationName, PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS ASQ
FROM            ASQAllocationMaster INNER JOIN
                         PoPackMaster ON ASQAllocationMaster.PoPackId = PoPackMaster.PoPackId INNER JOIN
                         AtcDetails ON ASQAllocationMaster.OurStyleId = AtcDetails.OurStyleID INNER JOIN
                         LocationMaster ON ASQAllocationMaster.Locaion_PK = LocationMaster.Location_PK " + condition + @" GROUP BY ASQAllocationMaster.Locaion_PK,ASQAllocationMaster.PoPackId, ASQAllocationMaster.OurStyleId, PoPackMaster.AtcId, PoPackMaster.BuyerPO, AtcDetails.OurStyle, 
                         AtcDetails.BuyerStyle, LocationMaster.LocationName, PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO";
                DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
                dt = pktrans.getPodetails(query);
            }
            return dt;

        }
        public DataTable GetAllAllocatedPODetailsforPacking(ArrayList Popackdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "where";

            for (int i = 0; i < Popackdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " ASQAllocationMaster.PoPackId=" + Popackdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or ASQAllocationMaster.PoPackId=" + Popackdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        POPackID, OurStyleId, ColorName, SizeName, Qty, Locaion_PK
FROM            ASQAllocationMaster " + condition;
                DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
                dt = pktrans.getPodetails(query);
            }
            return dt;

        }



        public DataTable GetAllASQforPacking(ArrayList Popackdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "where";

            for (int i = 0; i < Popackdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " POPackDetails.PoPackId=" + Popackdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or POPackDetails.PoPackId=" + Popackdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        POPackDetails.POPackId, POPackDetails.OurStyleID, PoPackMaster.AtcId, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, 0 AS Locaion_PK, 'NA' as LocationName,
                         PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS ASQ
FROM            POPackDetails INNER JOIN
                         PoPackMaster ON POPackDetails.POPackId = PoPackMaster.PoPackId INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID " + condition + @" GROUP BY POPackDetails.POPackId, POPackDetails.OurStyleID, PoPackMaster.AtcId, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO";
                DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
                dt = pktrans.getPodetails(query);
            }
            return dt;

        }

        public DataTable GetAllASQqDetailsforPacking(ArrayList Popackdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "where";

            for (int i = 0; i < Popackdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " POPackDetails.PoPackId=" + Popackdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or POPackDetails.PoPackId=" + Popackdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"SELECT        POPackID, OurStyleId, ColorName, SizeName, PoQty as qty, 0 as Locaion_PK
FROM            POPackDetails " + condition;
                DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
                dt = pktrans.getPodetails(query);
            }
            return dt;

        }

    }
}