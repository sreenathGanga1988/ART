using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ArtWebApp.DBTransaction;
using ArtWebApp.DataModels;
using System.Data.SqlClient;
namespace ArtWebApp.BLL.CutOrderBLL
{
    public static class CutPlan
    {


        /// <summary>
        /// GET aSQ sUBDATA
        /// </summary>
        /// <param name="ourstyleid"></param>
        /// <param name="ColorCode"></param>
        /// <returns></returns>

        public static DataTable GetPODataofColor(int ourstyleid, String ColorCode)
        {
            DataTable dt = new DataTable();

            dt = BLL.popackupdater.GetASQofAStyleAndColor(ourstyleid, ColorCode);



            return dt;

        }


        public static DataTable GetAlreadyCutofColor(int ourstyleid, String ColorCode ,int skudet_pk)
        {
            DataTable dt = new DataTable();

            dt = BLL.popackupdater.GetAlreadyCutQtyofAStyleAndColor(ourstyleid, ColorCode , skudet_pk);



            return dt;

        }

        public static DataTable fillFabColor(int ourstyleid, String ColorCode)
        {
            DataTable dt = DBTransaction.Productiontransaction.CutPlanTransaction.GetTheFabricOfOurStyle(ourstyleid, ColorCode);
            return dt;

        }


        public static String getStylename(int ourstyleid)
        {


            String stylename = "";

            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var skupk = entty.AtcDetails.Where(u => u.OurStyleID == ourstyleid).Select(u => u.BuyerStyle).FirstOrDefault();
                stylename = skupk.ToString();
            }

            return stylename;
        }

        public static String getGarmentColor(int skudet_PK)
        {


            String colorname = "";

            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var skupk = entty.SkuRawmaterialDetails.Where(u => u.SkuDet_PK == skudet_PK).Select(u => u.Sku_PK).FirstOrDefault();
                int sku_pk = int.Parse(skupk.ToString());
                var Dependency = entty.SkuRawMaterialMasters.Where(u => u.Sku_Pk == sku_pk).Select(u => u.IsCD).FirstOrDefault();

                if (Dependency.ToString().Trim() == "Y")
                {
                    var colornamenew = entty.SkuRawmaterialDetails.Where(u => u.SkuDet_PK == skudet_PK).Select(u => u.ColorCode).FirstOrDefault();
                    colorname = colornamenew;
                }
                else
                {
                    colorname = "CM";
                }

            }

            return colorname;
        }

        public static CutPlanDetailsData GetRollDetails(int ourstyleid,int skudet_PK, String Shrinkagegroup, String widthgroup, String markerTyple)
        {


            String colorname = "";

            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                int count = 0;
                float ayardsum = 0;
                float consumption = 0;
                float alreadycut = 0;
                var q = from rolldet in entty.FabricRollmasters
                        where rolldet.SkuDet_PK == skudet_PK && rolldet.ShrinkageGroup == Shrinkagegroup && rolldet.WidthGroup == widthgroup && rolldet.MarkerType == markerTyple
                        select new { rolldet.AYard, rolldet.Roll_PK };

                foreach (var element in q)
                {
                    count++;

                    ayardsum += float.Parse(element.AYard.ToString());

                }


                var skupk = entty.SkuRawmaterialDetails.Where(u => u.SkuDet_PK == skudet_PK).Select(u => u.Sku_PK).FirstOrDefault();
                int sku_pk = int.Parse(skupk.ToString());
                var q1 = from stylmstr in entty.StyleCostingMasters
                         join styldet in entty.StyleCostingDetails
                         on stylmstr.Costing_PK equals styldet.Costing_PK
                         where styldet.Sku_PK == sku_pk && stylmstr.IsApproved == "A" &&stylmstr.OurStyleID==ourstyleid
                         select new { styldet.Consumption };
                foreach (var element in q1)
                {

                    consumption = float.Parse(element.Consumption.ToString()) * float.Parse("0.975");


                }



                var q3 = (from marasq in entty.CutPlanASQDetails
                         where marasq.Skudet_PK==skudet_PK
                         select new { marasq.CutQty }) ;
                foreach (var element in q3)
                {

                    alreadycut = float.Parse(element.CutQty.ToString()) + alreadycut;


                }



                CutPlanDetailsData cddetdata = new CutOrderBLL.CutPlanDetailsData();

                cddetdata.RollCount = count;
                cddetdata.rollYard = ayardsum;
                cddetdata.bomconsumption = consumption;
                cddetdata.balanceyard = ayardsum;
                cddetdata.alreadycut = alreadycut;



                return cddetdata;
            }
        }



        public static CutPlanDetailsData GetRollDetailsoflocation(int ourstyleid, int skudet_PK, String Shrinkagegroup, String widthgroup, String markerTyple,int locationpk ,int tofactid)
        {


            String colorname = "";

            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                int count = 0;
                float ayardsum = 0;


                int deliveredcount = 0;
                float deliveredayardsum = 0;


                int balcountcount = 0;
                float baldayardsum = 0;



                float alreadycutforselectedfactory = 0;

               float consumption = 0;
                float alreadycut = 0;
                var q = from rolldet in entty.FabricRollmasters
                        join rollinv in entty.RollInventoryMasters on rolldet.Roll_PK equals rollinv.Roll_PK
                        where rolldet.SkuDet_PK == skudet_PK && rolldet.ShrinkageGroup == Shrinkagegroup && rolldet.WidthGroup == widthgroup && rolldet.MarkerType == markerTyple && rollinv.IsPresent=="Y" && rollinv.Location_Pk== locationpk
                        select new { rolldet.AYard, rolldet.Roll_PK ,rolldet.IsDelivered};

                foreach (var element in q)
                {

                    if(element.IsDelivered.Trim ()=="Y")
                    {
                        deliveredcount++;

                        deliveredayardsum += float.Parse(element.AYard.ToString());
                    }
                    else
                    {
                        balcountcount++;

                        baldayardsum += float.Parse(element.AYard.ToString());
                    }
                    count++;

                    ayardsum += float.Parse(element.AYard.ToString());

                }

                var deliveryedrol= from rolldet in entty.FabricRollmasters
                                   join rollinv in entty.RollInventoryMasters on rolldet.Roll_PK equals rollinv.Roll_PK
                                   where rolldet.SkuDet_PK == skudet_PK && rolldet.ShrinkageGroup == Shrinkagegroup &&
                                   rolldet.WidthGroup == widthgroup && rolldet.MarkerType == markerTyple && 
                                   rollinv.IsPresent == "Y" && rollinv.Location_Pk == tofactid
                                   select new { rolldet.AYard, rolldet.Roll_PK, rolldet.IsDelivered };

                foreach (var element123 in deliveryedrol.ToList())
                {
                    if (element123.IsDelivered.Trim() == "Y")
                    {
                        deliveredcount++;

                        deliveredayardsum += float.Parse(element123.AYard.ToString());
                    }

                }

                    var skupk = entty.SkuRawmaterialDetails.Where(u => u.SkuDet_PK == skudet_PK).Select(u => u.Sku_PK).FirstOrDefault();
                int sku_pk = int.Parse(skupk.ToString());
                var q1 = from stylmstr in entty.StyleCostingMasters
                         join styldet in entty.StyleCostingDetails
                         on stylmstr.Costing_PK equals styldet.Costing_PK
                         where styldet.Sku_PK == sku_pk && stylmstr.IsApproved == "A" && stylmstr.OurStyleID == ourstyleid
                         select new { styldet.Consumption };
                foreach (var element in q1)
                {

                    consumption = float.Parse(element.Consumption.ToString()) * float.Parse("0.975");


                }





                var q3 = (from marasq in entty.CutPlanASQDetails
                          join cutplnmstr in entty.CutPlanMasters on marasq.CutPlan_PK equals cutplnmstr.CutPlan_PK
                          where marasq.Skudet_PK == skudet_PK && cutplnmstr.OurStyleID == ourstyleid
                          select new { marasq.CutQty });
                foreach (var element in q3)
                {

                    alreadycut = float.Parse(element.CutQty.ToString()) + alreadycut;


                }








                var q4 = (from marasq in entty.CutPlanASQDetails 
                          join cutplnmstr in entty.CutPlanMasters on marasq.CutPlan_PK equals cutplnmstr.CutPlan_PK
                          where marasq.Skudet_PK == skudet_PK && cutplnmstr.Location_PK== tofactid && cutplnmstr.OurStyleID == ourstyleid
                          select new { marasq.CutQty });
                foreach (var element in q4)
                {

                    alreadycutforselectedfactory = float.Parse(element.CutQty.ToString()) + alreadycutforselectedfactory;


                }












                CutPlanDetailsData cddetdata = new CutOrderBLL.CutPlanDetailsData();

                cddetdata.RollCount = count;
                cddetdata.rollYard = ayardsum;


                cddetdata.balRollCount = balcountcount;
                cddetdata.balanceyard = baldayardsum;


                cddetdata.DeliverdRollCount = deliveredcount;
                cddetdata.DeliverdrollYard = deliveredayardsum;

                cddetdata.alreadycutoflocation = alreadycutforselectedfactory;
                cddetdata.bomconsumption = consumption;
              //  cddetdata.balanceyard = ayardsum;
                cddetdata.alreadycut = alreadycut;
                return cddetdata;
            }
        }


        public static DataTable GetAlreadyCutofcolor(int ourstyleid, String ColorCode)
        {
            DataTable dt = DBTransaction.Productiontransaction.CutPlanTransaction.GetTheFabricOfOurStyle(ourstyleid, ColorCode);
            return dt;

        }


        public static DataTable fillCutplanqty(int cutplanpk)
        {
            DataTable dt = DBTransaction.Productiontransaction.CutPlanTransaction.GetCutplanQty (cutplanpk);
            return dt;

        }



        #region datatable creator
        public static DataTable AddToTalQty(DataTable dt,DataTable alrdeaycut)
        {


            dt.Rows.Add();
            int lastrowindex = dt.Rows.Count - 1;
            dt.Rows[lastrowindex][0] = "Total";

            foreach (DataColumn dc in dt.Columns)
            {

                if (dc.ColumnName == "Color")
                {
                    dt.Rows[lastrowindex]["Color"] = "Total";
                }
                else
                {
                    dt.Rows[lastrowindex][dc.ColumnName] = "0";
                    float sum = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sum = sum + float.Parse(dt.Rows[i][dc.ColumnName].ToString());
                    }
                    dt.Rows[lastrowindex][dc.ColumnName] = sum.ToString();
                }


            }
            return AddCutQty(dt,alrdeaycut);
        }
        public static DataTable AddCutQty(DataTable dt, DataTable alrdeaycut)
        {

           
            dt.Rows.Add();
            int lastrowindex = dt.Rows.Count - 1;
            dt.Rows[lastrowindex][0] = "Cut";

            foreach (DataColumn dc in dt.Columns)
            {

                if (dc.ColumnName == "Color")
                {
                    dt.Rows[lastrowindex]["Color"] = "Cut";
                }
                else
                {
                    string sizename = dc.ColumnName.ToString().Trim();

                    float sumofsize = 0; 

                    try
                    {
                        object Sumfabric = alrdeaycut.Compute("Sum(CutQty)", "SizeName= '" + sizename + "'");

                        sumofsize = float.Parse(Sumfabric.ToString());
                    }
                    catch (Exception)
                    {


                    }


                    dt.Rows[lastrowindex][dc.ColumnName] = sumofsize;
                    //float sum = 0;
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    sum = sum + float.Parse(dt.Rows[i][dc.ColumnName].ToString());
                    //}
                    //dt.Rows[lastrowindex][dc.ColumnName] = sum.ToString();
                }


            }
            return AddBalancetoCutQty(dt);
        }
        public static DataTable AddBalancetoCutQty(DataTable dt)
        {


            dt.Rows.Add();
            int lastrowindex = dt.Rows.Count - 1;
            dt.Rows[lastrowindex][0] = "Cut";

            foreach (DataColumn dc in dt.Columns)
            {

                if (dc.ColumnName == "Color")
                {
                    dt.Rows[lastrowindex]["Color"] = "Balance";
                }
                else
                {
                    dt.Rows[lastrowindex][dc.ColumnName] = "0";
                    float sum = float.Parse(dt.Rows[lastrowindex - 2][dc.ColumnName].ToString()) - float.Parse(dt.Rows[lastrowindex - 1][dc.ColumnName].ToString());
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    sum = sum + float.Parse(dt.Rows[i][dc.ColumnName].ToString());
                    //}
                    dt.Rows[lastrowindex][dc.ColumnName] = sum.ToString();
                }


            }
            return AddNewCutQty(dt);
        }
        public static DataTable AddNewCutQty(DataTable dt)
        {


            dt.Rows.Add();
            int lastrowindex = dt.Rows.Count - 1;
            dt.Rows[lastrowindex][0] = "Cut";

            foreach (DataColumn dc in dt.Columns)
            {

                if (dc.ColumnName == "Color")
                {
                    dt.Rows[lastrowindex]["Color"] = "New Cut";
                }
                else
                {
                    dt.Rows[lastrowindex][dc.ColumnName] = dt.Rows[lastrowindex - 1][dc.ColumnName].ToString();
                    //  float sum = float.Parse(dt.Rows[lastrowindex - 2][dc.ColumnName].ToString()) - float.Parse(dt.Rows[lastrowindex - 1][dc.ColumnName].ToString());
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    sum = sum + float.Parse(dt.Rows[i][dc.ColumnName].ToString());
                    //}

                }


            }
            return dt;
        }



        #endregion


        public static int GetCutplanQty (int cutplanpk)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        SUM(CutPlanMarkerSizeDetails.Qty) AS Expr1
FROM            CutPlanMarkerSizeDetails INNER JOIN
                         CutPlanMarkerDetails ON CutPlanMarkerSizeDetails.CutPlanMarkerDetails_PK = CutPlanMarkerDetails.CutPlanMarkerDetails_PK
WHERE        (CutPlanMarkerDetails.CutPlan_PK = @param1)";
            cmd.Parameters.AddWithValue("@param1", cutplanpk);
            int balqty = int.Parse(QueryFunctions.ReturnQueryValue(cmd).ToString());

            return balqty;
        }



        public static float GetCutplanfabutilised(int skudet_pk,int ouustyleid,int location)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        SUM(CutPlanFabReq) AS CutPlanFabReq
FROM            (SELECT        ISNULL(CutPlanFabReq,
                                                        (SELECT        ISNULL(SUM(CutQty), 0) AS CutQty
                                                          FROM            CutPlanASQDetails
                                                          WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK)) * BOMConsumption) AS CutPlanFabReq, SkuDet_PK, OurStyleID, Location_PK, CutPlan_PK
                          FROM            CutPlanMaster
                          WHERE        (SkuDet_PK = @skudet_pk) AND (OurStyleID = @ouustyleid) AND (Location_PK = @location)) AS tt
GROUP BY SkuDet_PK, OurStyleID, Location_PK, CutPlan_PK";
            cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
            cmd.Parameters.AddWithValue("@ouustyleid", ouustyleid);
            cmd.Parameters.AddWithValue("@location", location);
            float balqty = float.Parse(QueryFunctions.ReturnQueryValue(cmd).ToString());

            return balqty;
        }





        public static float GetCutplanfabutilisedofAGroup(int skudet_pk, int ouustyleid, int location, string ShrinkageGroup, string WidthGroup, string MarkerType)
        {
            float balqty = 0;
            SqlCommand cmd = new SqlCommand();
            //            cmd.CommandText = @"SELECT        SUM(CutPlanFabReq) AS CutPlanFabReq
            //FROM            (SELECT        ISNULL(CutPlanFabReq,
            //                             (SELECT        ISNULL(SUM(CutQty), 0) AS CutQty
            //                               FROM            CutPlanASQDetails
            //                               WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK)) * BOMConsumption) AS CutPlanFabReq, SkuDet_PK, OurStyleID, Location_PK, CutPlan_PK, ShrinkageGroup, WidthGroup, MarkerType
            //FROM            CutPlanMaster
            //WHERE        (SkuDet_PK = @skudet_pk) AND (OurStyleID = @ouustyleid) AND (Location_PK = @location) AND (ShrinkageGroup = @ShrinkageGroup) AND (WidthGroup = @WidthGroup) AND (MarkerType = @MarkerType)) AS tt
            //GROUP BY SkuDet_PK, OurStyleID, Location_PK, CutPlan_PK";

            cmd.CommandText = @"SELECT        SUM(CutPlanFabReq) AS CutPlanFabReq
FROM            (SELECT        ISNULL(CutPlanFabReq,
                             (SELECT        ISNULL(SUM(CutQty), 0) AS CutQty
                               FROM            CutPlanASQDetails
                               WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK)) * BOMConsumption) AS CutPlanFabReq, SkuDet_PK, OurStyleID, Location_PK, CutPlan_PK, ShrinkageGroup, WidthGroup, MarkerType
FROM            CutPlanMaster
WHERE        (SkuDet_PK = @skudet_pk) AND (Location_PK = @location) AND (ShrinkageGroup = @ShrinkageGroup) AND (WidthGroup = @WidthGroup) AND (MarkerType = @MarkerType)) AS tt
GROUP BY SkuDet_PK, OurStyleID, Location_PK, CutPlan_PK";

            cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
            cmd.Parameters.AddWithValue("@ouustyleid", ouustyleid);
            cmd.Parameters.AddWithValue("@location", location);

            cmd.Parameters.AddWithValue("@ShrinkageGroup", ShrinkageGroup);
            cmd.Parameters.AddWithValue("@WidthGroup", WidthGroup);
            cmd.Parameters.AddWithValue("@MarkerType", MarkerType);

            try
            {
                string balqtystrng = QueryFunctions.ReturnQueryValue(cmd).ToString();

                balqty = float.Parse(balqtystrng.ToString());
            }
            catch (Exception)
            {

                balqty = 0;
            }

            return balqty;
        }



        /// <summary>
        /// get ASQ MASTER DATA FOR A STYLE AND COLOR
        /// </summary>
        /// <param name="ourstyleid"></param>
        /// <param name="ColorCode"></param>
        /// <returns></returns>
        public static DataTable GetPOMasterDataofColor(int ourstyleid, String ColorCode)
        {
            DataTable dt = new DataTable();

            dt = BLL.popackupdater.GetASQMasterofAStyleAndColor(ourstyleid, ColorCode);

           
            return dt;

        }


        public static DataTable GetPOMasterDataofColorandLocation(int ourstyleid, String ColorCode,int location_pk)
        {
            DataTable dt = new DataTable();

            dt = BLL.popackupdater.GetASQMasterofAStyleAndColorofLocation(ourstyleid, ColorCode, location_pk);


            return dt;

        }



        public static DataTable GetCutPlanSizeData(int cutplanid)
        {
            DataTable dt = new DataTable();

            dt = BLL.popackupdater.GetCutPlanmarkerdetails(cutplanid);



            return dt;


         

        }


        public static DataTable GetCutPlanASQSizeData(int cutplanid)
        {
            DataTable dt = new DataTable();

            dt = DBTransaction.Productiontransaction.CutPlanTransaction.GetCutplanASQSizeQty(cutplanid);



            return dt;




        }

    }






    public class CutPlanMasterData
    {
        public int CutPlan_PK { get; set; }
        public int OurStyleID { get; set; }
        public int SkuDet_PK { get; set; }
        public int location_PK { get; set; }
        
        public int CutQty { get; set; }
        public String ColorName { get; set; }
        public String ColorCode { get; set; }
        public String FabDescription { get; set; }
        public String MarkerType { get; set; }
        public String ShrinkageGroup { get; set; }
        public String WidthGroup { get; set; }
        public String AddedBy { get; set; }

        public DateTime AddedDate { get; set; }
        public Decimal BOMConsumption { get; set; }
        public Decimal CutPlanFabReq { get; set; }
        public String MakerMade { get; set; }
        public String Maxmarkerlength { get; set; }
        public String Fabrication { get; set; }
        public Decimal Efficiecny { get; set; }
        public Decimal cutplanConsumption { get; set; }

        public String newRefPattern { get; set; }
        public String Refpattern { get; set; }
        public List<CutPlanDetailsData> CutPlanDetailsDataCollection { get; set; }
        public List<CutPlanMarkerDetailsData> CutPlanMarkerDetailsDataCollection { get; set; }
        public List<CutPlanMarkerTypeData> CutPlanMarkerTypeDataDataCollection { get; set; }

        public String InsertNewCutPlanMaster()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                try
                {
                    CutPlanMaster ctmstr = new CutPlanMaster();
                    ctmstr.OurStyleID = this.OurStyleID;
                    ctmstr.SkuDet_PK = this.SkuDet_PK;

                    ctmstr.ColorName = this.ColorName;
                    ctmstr.ColorCode = this.ColorCode;
                    ctmstr.ShrinkageGroup = this.ShrinkageGroup;
                    ctmstr.WidthGroup = this.WidthGroup;
                    ctmstr.MarkerType = this.MarkerType;
                    ctmstr.AddedBy = this.AddedBy;
                    ctmstr.AddedDate = this.AddedDate;
                    ctmstr.Location_PK = this.location_PK;
                    ctmstr.FabDescription = this.FabDescription;
                    ctmstr.BOMConsumption = this.BOMConsumption;
                    ctmstr.MarkerMade = this.MakerMade;
                    ctmstr.IsPatternAdded = "N";
                    ctmstr.IsApproved = "N";
                    ctmstr.IsRatioAdded = "N";
                    ctmstr.IsDeleted = "N";
                    ctmstr.IsCutorderGiven = "N";
                    ctmstr.RefPattern = "";
                    ctmstr.CutplanConsumption = 0;
                    ctmstr.CutplanEfficency = 0;
                    ctmstr.Fabrication = this.Fabrication;
                    ctmstr.Maxmarkerlength = this.Maxmarkerlength;
                    enty.CutPlanMasters.Add(ctmstr);
                    enty.SaveChanges();
                    HttpContext.Current.Session["CutPlan_PK"] = ctmstr.CutPlan_PK;
                    Cutn = ctmstr.CutPlanNUM = CodeGenerator.GetUniqueCode("CPL", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(ctmstr.CutPlan_PK.ToString()));

                    foreach (CutPlanMarkerTypeData di in this.CutPlanMarkerTypeDataDataCollection)
                    {

                        CutPlanMarkerType cddetail = new CutPlanMarkerType();
                        cddetail.CutPlan_PK = ctmstr.CutPlan_PK;
                        cddetail.CutPlanmarkerType1 = di.MarkerType;

                        enty.CutPlanMarkerTypes.Add(cddetail);
                    }



                    enty.SaveChanges();
                }
                catch (Exception ex)
                {
                    Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                }




                
            }

            return Cutn;
        }


        public String InsertCutASQDetailsPlan()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                

                foreach (CutPlanDetailsData di in this.CutPlanDetailsDataCollection)
                {

                    var popackdets_pk = enty.POPackDetails.Where(u => u.SizeName == di.SizeName && u.ColorName == di.ColorName && u.OurStyleID == di.OurStyleId).Select(u => u.PoPack_Detail_PK).FirstOrDefault();

                    CutPlanASQDetail cddetail = new CutPlanASQDetail();
                    cddetail.CutPlan_PK = di.CutPlan_PK;
                    cddetail.PoPackId = di.PoPackId;
                    cddetail.PoPack_Detail_PK = int.Parse(popackdets_pk.ToString ());
                    cddetail.CutQty = di.CutQty;
                    cddetail.ColorName = di.ColorName;
                    cddetail.SizeName = di.SizeName;
                    cddetail.Skudet_PK = di.skudet_PK;
                    enty.CutPlanASQDetails.Add(cddetail);
                }





                enty.SaveChanges();


                
            }

            return Cutn;

        }



        public String InsertCutPlanMarkerDetails()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                foreach (CutPlanMarkerDetailsData di in this.CutPlanMarkerDetailsDataCollection)
                {

                    CutPlanMarkerDetail cddetail = new CutPlanMarkerDetail();
                    cddetail.MarkerNo = di.MarkerNo;
                    cddetail.NoOfPc = di.NoOfPc;
                    cddetail.Qty = di.Qty;
                    cddetail.MarkerDetAddedBy= HttpContext.Current.Session["Username"].ToString().Trim();
                    cddetail.MarkerDetAddedDate = DateTime.Now;
                    cddetail.CutPlan_PK = di.CutPlan_PK;
                    enty.CutPlanMarkerDetails.Add(cddetail);
                }





                enty.SaveChanges();



            }

            return Cutn;

        }

        //public String InsertCutPlanMarkerTypes()
        //{
        //    string Cutn = "";
        //    using (ArtEntitiesnew enty = new ArtEntitiesnew())
        //    {


              




        //        enty.SaveChanges();



        //    }

        //    return Cutn;

        //}

        public void ApproveCutPlan(int cutplan_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from ctplnmstr in enty.CutPlanMasters
                        where ctplnmstr.CutPlan_PK == cutplan_pk
                        select ctplnmstr;

                foreach(var element in q)
                {
                    element.RefPattern = this.Refpattern;
                    element.IsApproved = "Y";
                    element.ApprovedBy = HttpContext.Current.Session["Username"].ToString().Trim(); ;
                    element.ApprovedDate = DateTime.Now;
                }
                enty.SaveChanges();
            }
       }

        public String  UpdateMarkerDetails()
        {
            string Cutn = "";
            int cutplanPk = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                foreach (CutPlanMarkerDetailsData di in this.CutPlanMarkerDetailsDataCollection)
                {


                    var q = from cutmrkdet in enty.CutPlanMarkerDetails
                            where cutmrkdet.CutPlanMarkerDetails_PK == di.CutPlanMarkerDetails_PK
                            select cutmrkdet;

                    foreach (var element in q)
                    {

                        element.PaternMarkerName = di.PaternMarkerName;
                        element.MarkerLength = di.MarkerLength;
                        element.Tolerancelength = di.Tolerancelength;
                        element.TotalfabReq = di.TotalfabReq;
                        element.Efficiency = di.efficiency;
                        element.PatternAddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                        element.PatternAddedDate = DateTime.Now;
                        
                        cutplanPk = int.Parse(element.CutPlan_PK.ToString());

                    } 


                    
                }



                var q1 = from cutplnmstr in enty.CutPlanMasters
                         where cutplnmstr.CutPlan_PK == cutplanPk
                         select cutplnmstr;
                foreach (var element in q1)
                {
                    element.IsPatternAdded = "Y";
                    element.CutplanEfficency = this.Efficiecny;
                    element.CutplanConsumption = this.cutplanConsumption;
                    element.CutPlanFabReq = this.CutPlanFabReq;
                    element.NewPatternName = this.newRefPattern;
                    element.PatternAddedBy = HttpContext.Current.Session["Username"].ToString().Trim(); ;
                }
                    enty.SaveChanges();



            }

            return Cutn;

        }


        public void DeleteCutPlan(int cutplan_pk)
        {

            String status = "";
            String cutplannum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q1 = from ponmbr in enty.CutPlanMarkerSizeDetails
                        where ponmbr.CutPlan_PK == cutplan_pk
                        select ponmbr;


                foreach (var element in q1)
                {
                    
                    enty.CutPlanMarkerSizeDetails.Remove(element);
                   
                }


                var q2 = from ponmbr in enty.CutPlanMarkerTypes
                        where ponmbr.CutPlan_PK == cutplan_pk
                        select ponmbr;


                foreach (var element in q2)
                {
                    
                    enty.CutPlanMarkerTypes.Remove(element);
                    
                }

                var q3 = from ponmbr in enty.CutPlanMarkerDetails
                        where ponmbr.CutPlan_PK == cutplan_pk
                        select ponmbr;


                foreach (var element in q3)
                {

                    enty.CutPlanMarkerDetails.Remove(element);
                  
                }

                var q4 = from ponmbr in enty.CutPlanASQDetails
                         where ponmbr.CutPlan_PK == cutplan_pk
                         select ponmbr;


                foreach (var element in q4)
                {

                    enty.CutPlanASQDetails.Remove(element);
                   
                }

                var q5 = from ponmbr in enty.CutPlanMasters
                        where ponmbr.CutPlan_PK == cutplan_pk 
                        select ponmbr;


                foreach (var element in q5)
                {



                  if(element.IsApproved.Trim() == "Y" && element.IsPatternAdded.Trim() == "Y" && element.IsCutorderGiven.Trim() == "Y" && element.IsRatioAdded.Trim() == "Y")
                    {
                        status = "CutOrder Done";
                    }
                   else if (element.IsApproved.Trim() == "Y" && element.IsPatternAdded.Trim() == "N" && element.IsCutorderGiven.Trim() == "N" && element.IsRatioAdded.Trim() == "Y")
                    {
                        status = "Approved But No Pattern";
                    }

                    else if (element.IsApproved.Trim() == "N" && element.IsPatternAdded.Trim() == "N" && element.IsCutorderGiven.Trim() == "N" && element.IsRatioAdded.Trim() == "Y")
                    {
                        status = "Not Approved";
                    }

                    else if (element.IsApproved.Trim() == "N" && element.IsPatternAdded.Trim() == "N" && element.IsCutorderGiven.Trim() == "N" && element.IsRatioAdded.Trim() == "N")
                    {
                        status = "No Ratio";
                    }

                    cutplannum = element.CutPlanNUM.ToString();


                    enty.CutPlanMasters.Remove(element);
                    
                }


                DeletedCutPlan ctplndel = new DeletedCutPlan();
                ctplndel.CutPlan_PK = cutplan_pk;
                ctplndel.Cutplannum = cutplannum;
                ctplndel.DeletedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                ctplndel.Deleteddate = DateTime.Now;
                enty.DeletedCutPlans.Add(ctplndel);
                enty.SaveChanges();

            }
        }

        public void ChangerefpatternCutPlan(int cutplan_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                string oldref = "";
                String olddate ="";
                string oldaddedby = "";
                var q1 = from cutplnmstr in enty.CutPlanMasters
                         where cutplnmstr.CutPlan_PK == cutplan_pk
                         select cutplnmstr;
                foreach (var element in q1)
                {
                    oldref = element.RefPattern.ToString();
                    olddate =element.ApprovedDate.ToString();

                    oldaddedby = element.ApprovedBy;
                    element.RefPattern = this.Refpattern;
                    element.IsApproved = "Y";
                    element.IsPatternAdded = "N";
                    element.ApprovedBy = HttpContext.Current.Session["Username"].ToString().Trim(); ;
                    element.ApprovedDate = DateTime.Now;
                }



                CutplanRefPattern ctpnref = new DataModels.CutplanRefPattern();

                ctpnref.Refpattern = oldref;

                ctpnref.AddedBy = AddedBy;
                ctpnref.Addeddate = olddate;

                enty.CutplanRefPatterns.Add(ctpnref);

                enty.SaveChanges();



                }


            }
        }


    public class CutPlanMarkerTypeData
    {
        public int CutPlan_PK { get; set; }
        public String MarkerType { get; set; }
    }



        public class CutPlanDetailsData
    {


        public int CutPlanASQDetails_PK { get; set; }
        public int CutPlan_PK { get; set; }
        public int PoPackId { get; set; }
        public int OurStyleId { get; set; }
        public int skudet_PK { get; set; }
        public int PoPack_Detail_PK { get; set; }
        public float alreadycut { get; set; }
        public int CutQty { get; set; }
        public String ColorName { get; set; }
        public String SizeName { get; set; }
        public int TotalRollCount { get; set; }
        public float TotalrollYard { get; set; }

        public int DeliverdRollCount { get; set; }
        public float DeliverdrollYard { get; set; }

        public int RollCount { get; set; }
        public float rollYard { get; set; }

        public float bomconsumption { get; set; }

        public int balRollCount { get; set; }
        public float balanceyard { get; set; }

        public float alreadycutoflocation { get; set; }





    }


    public class CutPlanMarkerDetailsData
    {
        DBTransaction.CutOrderTransaction cuttrans = null;
        public int CutPlanMarkerDetails_PK { get; set; }
        public int CutPlan_PK { get; set; }
        public string MarkerNo { get; set; }
        public int NoOfPc { get; set; }
        public int Qty { get; set; }
        public int NoOfPlies { get; set; }
        public int Cutperplies { get; set; }
        public Decimal MarkerLength { get; set; }
        public Decimal Tolerancelength { get; set; }
        public Decimal TotalfabReq { get; set; }
        public String PaternMarkerName { get; set; }
        public int cutreq { get; set; }
        public Decimal efficiency { get; set; }
        public String  isalreadyAdded { get; set; }




        public List<CutPlanSizeDetailsData> CutPlanSizeDetailsDataCollection { get; set; }


        public String InsertCutOrderMarkerSizeData()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                CutPlanMarkerDetail ctmdet = new CutPlanMarkerDetail();
                ctmdet.CutPlan_PK = this.CutPlan_PK;

                ctmdet.MarkerNo = this.MarkerNo;
                ctmdet.NoOfPlies = this.NoOfPlies;
                ctmdet.Cutreq = this.cutreq;
                ctmdet.CutPerPlies = this.Cutperplies;
                ctmdet.MarkerLength = 0;
                ctmdet.Efficiency = 0;
                ctmdet.PaternMarkerName = "";
                ctmdet.MarkerDetAddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                ctmdet.MarkerDetAddedDate = DateTime.Now;

                enty.CutPlanMarkerDetails.Add(ctmdet);
                enty.SaveChanges();



                foreach (CutPlanSizeDetailsData di in this.CutPlanSizeDetailsDataCollection)
                {
                    //if (!enty.CutPlanMarkerSizeDetails.Any(f => f.CutPlan_PK == di.CutPlan_PK && f.Size.Trim() == di.Sizename.Trim()))
                    //{
                        CutPlanMarkerSizeDetail cddetail = new CutPlanMarkerSizeDetail();
                        cddetail.CutPlan_PK = di.CutPlan_PK;
                        cddetail.Size = di.Sizename;
                        cddetail.Qty = di.Qty;
                        cddetail.CutPlanMarkerDetails_PK = ctmdet.CutPlanMarkerDetails_PK;
                        cddetail.Ratio = di.Ratio;
                        enty.CutPlanMarkerSizeDetails.Add(cddetail);
                    //}
                    //else
                    //{
                    //    var Q = from cutsizedet in enty.CutPlanMarkerSizeDetails
                    //            where cutsizedet.CutPlan_PK == di.CutPlan_PK && cutsizedet.Size.Trim() == di.Sizename
                    //            select cutsizedet;
                    //    foreach (var element in Q)
                    //    {
                    //        element.Qty = di.Qty;
                    //        element.Ratio = di.Ratio;
                    //    }

                    //}

                    enty.SaveChanges();
                    //  updatecutdetail(di.CutOrderDet_PK);
                   

                }


                var Q = from ctplnmstr in enty.CutPlanMasters
                        where ctplnmstr.CutPlan_PK == this.CutPlan_PK
                        select ctplnmstr;
                foreach(var element in Q)
                {
                    element.IsRatioAdded = "Y";
                }


                enty.SaveChanges();



            }

            return Cutn;

        }


        public String UpdateCutOrderMarkerSizeData()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {



                if(this.isalreadyAdded.Trim ()=="Y")
                {


                    var q = from cutplandet in enty.CutPlanMarkerDetails
                            where cutplandet.CutPlanMarkerDetails_PK == this.CutPlanMarkerDetails_PK
                            select cutplandet;

                    foreach(var element in q)
                    {

                        element.NoOfPlies = this.NoOfPlies;
                        element.Cutreq = this.cutreq;
                        element.CutPerPlies = this.Cutperplies;
                        element.MarkerLength = 0;
                        element.Efficiency = 0;
                        element.PaternMarkerName = "";

                    }
                    foreach (CutPlanSizeDetailsData di in this.CutPlanSizeDetailsDataCollection)
                    {
                        var q1 = from cutplanmarksize in enty.CutPlanMarkerSizeDetails
                                where cutplanmarksize.CutPlanMarkerDetails_PK == this.CutPlanMarkerDetails_PK && cutplanmarksize.Size==di.Sizename
                                select cutplanmarksize;
                        foreach (var element1 in q1)
                        {
                            
                            element1.Qty = di.Qty;
                            element1.Ratio = di.Ratio;
                        }

                     }
                    enty.SaveChanges();
                }
                else
                {

                    CutPlanMarkerDetail ctmdet = new CutPlanMarkerDetail();
                    ctmdet.CutPlan_PK = this.CutPlan_PK;

                    ctmdet.MarkerNo = this.MarkerNo;
                    ctmdet.NoOfPlies = this.NoOfPlies;
                    ctmdet.Cutreq = this.cutreq;
                    ctmdet.CutPerPlies = this.Cutperplies;
                    ctmdet.MarkerLength = 0;
                    ctmdet.Efficiency = 0;
                   

                    ctmdet.PaternMarkerName = "";
                    ctmdet.MarkerDetAddedBy = HttpContext.Current.Session["Username"].ToString().Trim(); 
                    ctmdet.MarkerDetAddedDate = DateTime.Now;
                    enty.CutPlanMarkerDetails.Add(ctmdet);
                    enty.SaveChanges();

                    foreach (CutPlanSizeDetailsData di in this.CutPlanSizeDetailsDataCollection)
                    {

                        CutPlanMarkerSizeDetail cddetail = new CutPlanMarkerSizeDetail();
                        cddetail.CutPlan_PK = di.CutPlan_PK;
                        cddetail.Size = di.Sizename;
                        cddetail.Qty = di.Qty;
                        cddetail.CutPlanMarkerDetails_PK = ctmdet.CutPlanMarkerDetails_PK;
                        cddetail.Ratio = di.Ratio;
                        enty.CutPlanMarkerSizeDetails.Add(cddetail);

                        enty.SaveChanges();



                    }


                }















              


                enty.SaveChanges();



            }

            return Cutn;

        }


    }


    public class CutPlanSizeDetailsData
    {
        public int CutPlanSize_PK { get; set; }
        public int CutPlan_PK { get; set; }
      
        public string Sizename { get; set; }
        public Decimal Ratio { get; set; }
        public Decimal Qty { get; set; }




    }



}