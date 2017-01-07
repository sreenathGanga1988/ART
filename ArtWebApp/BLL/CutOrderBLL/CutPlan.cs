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

        public static DataTable fillFabColor(int ourstyleid, String ColorCode)
        {
            DataTable dt = DBTransaction.Productiontransaction.CutPlanTransaction.GetTheFabricOfOurStyle(ourstyleid, ColorCode);
            return dt;

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
        public static DataTable AddToTalQty(DataTable dt)
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
            return AddCutQty(dt);
        }
        public static DataTable AddCutQty(DataTable dt)
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
                    dt.Rows[lastrowindex][dc.ColumnName] = "0";
                    float sum = 0;
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    sum = sum + float.Parse(dt.Rows[i][dc.ColumnName].ToString());
                    //}
                    dt.Rows[lastrowindex][dc.ColumnName] = sum.ToString();
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
        


        public List<CutPlanDetailsData> CutPlanDetailsDataCollection { get; set; }
        public List<CutPlanMarkerDetailsData> CutPlanMarkerDetailsDataCollection { get; set; }

        public String InsertNewCutPlanMaster()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
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
                enty.CutPlanMasters.Add(ctmstr);
                enty.SaveChanges();
                HttpContext.Current.Session["CutPlan_PK"] = ctmstr.CutPlan_PK;
                Cutn = ctmstr.CutPlanNUM = CodeGenerator.GetUniqueCode("CPL", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(ctmstr.CutPlan_PK.ToString()));
                enty.SaveChanges();
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
                    cddetail.CutPlan_PK = di.CutPlan_PK;
                    enty.CutPlanMarkerDetails.Add(cddetail);
                }





                enty.SaveChanges();



            }

            return Cutn;

        }







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
        public int RollCount { get; set; }
        public float rollYard { get; set; }
        public float bomconsumption { get; set; }

        public float balanceyard { get; set; }
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
        public int cutreq { get; set; }


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


                enty.CutPlanMarkerDetails.Add(ctmdet);
                enty.SaveChanges();



                foreach (CutPlanSizeDetailsData di in this.CutPlanSizeDetailsDataCollection)
                {
                    if (!enty.CutPlanMarkerSizeDetails.Any(f => f.CutPlan_PK == di.CutPlan_PK && f.Size.Trim() == di.Sizename.Trim()))
                    {
                        CutPlanMarkerSizeDetail cddetail = new CutPlanMarkerSizeDetail();
                        cddetail.CutPlan_PK = di.CutPlanSize_PK;
                        cddetail.Size = di.Sizename;
                        cddetail.Qty = di.Qty;
                        cddetail.CutPlanMarkerDetails_PK = ctmdet.CutPlanMarkerDetails_PK;
                        cddetail.Ratio = di.Ratio;
                        enty.CutPlanMarkerSizeDetails.Add(cddetail);
                    }
                    else
                    {
                        var Q = from cutsizedet in enty.CutPlanMarkerSizeDetails
                                where cutsizedet.CutPlan_PK == di.CutPlan_PK && cutsizedet.Size.Trim() == di.Sizename
                                select cutsizedet;
                        foreach (var element in Q)
                        {
                            element.Qty = di.Qty;
                            element.Ratio = di.Ratio;
                        }

                    }

                    enty.SaveChanges();
                    //  updatecutdetail(di.CutOrderDet_PK);
                   

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