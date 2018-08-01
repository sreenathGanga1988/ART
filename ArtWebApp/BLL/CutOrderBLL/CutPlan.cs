using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ArtWebApp.DBTransaction;
using ArtWebApp.DataModels;
using System.Data.SqlClient;
using System.Collections;

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
                float deliveredayardsumdummy = 0;
                

                int balcountcount = 0;
                float baldayardsum = 0;
                float balWeightSum = 0;


                float alreadycutforselectedfactory = 0;

                Decimal alreadycutforselectedfactoryofgroup = 0;
                var UOMName = "";
               float consumption = 0;
                float alreadycut = 0;
                var q = from rolldet in entty.FabricRollmasters
                        join rollinv in entty.RollInventoryMasters on rolldet.Roll_PK equals rollinv.Roll_PK
                        where rolldet.SkuDet_PK == skudet_PK && rolldet.ShrinkageGroup == Shrinkagegroup && rolldet.WidthGroup == widthgroup 
                        && rolldet.MarkerType == markerTyple && rollinv.IsPresent=="Y" && rollinv.Location_Pk== locationpk
                        select new { rolldet.AYard, rolldet.Roll_PK ,rolldet.IsDelivered,rolldet.SWeight};

                foreach (var element in q.ToList())
                {

                    if(element.IsDelivered.Trim ()=="Y")
                    {
                        deliveredcount++;

                        deliveredayardsumdummy += float.Parse(element.AYard.ToString());
                    }
                    else
                    {
                        balcountcount++;

                        baldayardsum += float.Parse(element.AYard.ToString());

                        try
                        {
                            balWeightSum += float.Parse(element.SWeight.ToString());
                        }
                        catch (Exception)
                        {

                            
                        }
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

                try
                {
                    UOMName = entty.SkuRawMaterialMasters.Where(u => u.Sku_Pk == skupk).Select(u => u.UOMMaster.UomCode).FirstOrDefault();

                }
                catch (Exception)
                {

                   
                }



                var q3 = (from marasq in entty.CutPlanASQDetails
                          join cutplnmstr in entty.CutPlanMasters on marasq.CutPlan_PK equals cutplnmstr.CutPlan_PK
                          where marasq.Skudet_PK == skudet_PK && cutplnmstr.OurStyleID == ourstyleid && cutplnmstr.IsDeleted=="N"
                          select new { marasq.CutQty });
                foreach (var element in q3)
                {

                    alreadycut = float.Parse(element.CutQty.ToString()) + alreadycut;


                }








                var q4 = (from marasq in entty.CutPlanASQDetails 
                          join cutplnmstr in entty.CutPlanMasters on marasq.CutPlan_PK equals cutplnmstr.CutPlan_PK
                          where marasq.Skudet_PK == skudet_PK && cutplnmstr.Location_PK== tofactid && cutplnmstr.OurStyleID == ourstyleid && cutplnmstr.IsDeleted == "N"
                          select new { marasq.CutQty });
                foreach (var element in q4)
                {

                    alreadycutforselectedfactory = float.Parse(element.CutQty.ToString()) + alreadycutforselectedfactory;


                }

                var q5 = (from marasq in entty.CutPlanASQDetails
                          join cutplnmstr in entty.CutPlanMasters on marasq.CutPlan_PK equals cutplnmstr.CutPlan_PK
                          where marasq.Skudet_PK == skudet_PK && cutplnmstr.Location_PK == tofactid && cutplnmstr.OurStyleID == ourstyleid &&
                          cutplnmstr.ShrinkageGroup == Shrinkagegroup &&
                                   cutplnmstr.WidthGroup == widthgroup && cutplnmstr.MarkerType == markerTyple && cutplnmstr.IsDeleted == "N"
                          select new { marasq.CutQty });
                foreach (var element in q5)
                {

                    alreadycutforselectedfactoryofgroup = decimal.Parse(element.CutQty.ToString()) + alreadycutforselectedfactoryofgroup;


                }

                int cutplanblockedroll = 0;
                Decimal cutplanblockedyard = 0;

             var cutplanrolldetail= (from cplndet in entty.CutPlanRollDetails
                                    where cplndet.CutPlanMaster.SkuDet_PK == skudet_PK && cplndet.CutPlanMaster.ShrinkageGroup == Shrinkagegroup &&
                                   cplndet.CutPlanMaster.WidthGroup == widthgroup && cplndet.CutPlanMaster.MarkerType == markerTyple && cplndet.CutPlanMaster.IsDeleted == "N" && cplndet.IsDeleted == "N"
                                     select new { cplndet.FabricRollmaster.AYard, cplndet.FabricRollmaster.Roll_PK });
                foreach (var element in cutplanrolldetail)
                {
                    cutplanblockedroll++;
                    cutplanblockedyard = Decimal.Parse(element.AYard.ToString()) + cutplanblockedyard;


                }


                CutPlanDetailsData cddetdata = new CutOrderBLL.CutPlanDetailsData();

                cddetdata.RollCount = count;
                cddetdata.rollYard = ayardsum;


                cddetdata.balRollCount = balcountcount;
                cddetdata.balanceyard = baldayardsum;
                cddetdata.balWeightSum = balWeightSum;


                cddetdata.deliveredayardsumdummy = deliveredayardsumdummy;
                
                cddetdata.DeliverdRollCount = deliveredcount;
                cddetdata.DeliverdrollYard = deliveredayardsum;

                cddetdata.alreadycutoflocation = alreadycutforselectedfactory;
                cddetdata.alreadycutforselectedfactoryofgroup = alreadycutforselectedfactoryofgroup;
                cddetdata.bomconsumption = consumption;
               cddetdata.UOMName = UOMName.ToString();
                cddetdata.alreadycut = alreadycut;

                cddetdata.CutplanBlockedRoll = cutplanblockedroll;
                cddetdata.CutplanBlockedYardage= cutplanblockedyard;
                return cddetdata;
            }
        }



        public static CutPlanDetailsDatannEW GetRollDetailsoflocationNew(int ourstyleid, int skudet_PK, String Shrinkagegroup, String widthgroup, String markerTyple, int locationpk, int tofactid)
        {


            String colorname = "";

            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                int RollinWharehousecount = 0;
                float RollinWharehouseayardsum = 0;


                int deliveredtofactorycount = 0;
                float deliveredtofactorycayardsum = 0;


                int balcountcount = 0;
                float baldayardsum = 0;



                float alreadycutforselectedfactory = 0;

                float consumption = 0;
                float CutplanQtyIssued = 0;
                var q = from rolldet in entty.FabricRollmasters
                        join rollinv in entty.RollInventoryMasters on rolldet.Roll_PK equals rollinv.Roll_PK
                        where rolldet.SkuDet_PK == skudet_PK && rolldet.ShrinkageGroup == Shrinkagegroup && rolldet.WidthGroup == widthgroup
                        && rolldet.MarkerType == markerTyple && rollinv.IsPresent == "Y" && rollinv.Location_Pk == locationpk
                        select new { rolldet.AYard, rolldet.Roll_PK, rolldet.IsDelivered };

                foreach (var element in q)
                {

                 
                    RollinWharehousecount++;

                    RollinWharehouseayardsum += float.Parse(element.AYard.ToString());

                }

                var deliveryedrol = from rolldet in entty.FabricRollmasters
                                    join rollinv in entty.RollInventoryMasters on rolldet.Roll_PK equals rollinv.Roll_PK
                                    where rolldet.SkuDet_PK == skudet_PK && rolldet.ShrinkageGroup == Shrinkagegroup &&
                                    rolldet.WidthGroup == widthgroup && rolldet.MarkerType == markerTyple &&
                                    rollinv.IsPresent == "Y" && rollinv.Location_Pk == tofactid
                                    select new { rolldet.AYard, rolldet.Roll_PK, rolldet.IsDelivered };

                foreach (var element123 in deliveryedrol.ToList())
                {
                    if (element123.IsDelivered.Trim() == "Y")
                    {
                        deliveredtofactorycount++;

                        deliveredtofactorycayardsum += float.Parse(element123.AYard.ToString());
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

                    CutplanQtyIssued = float.Parse(element.CutQty.ToString()) + CutplanQtyIssued;


                }








                var q4 = (from marasq in entty.CutPlanASQDetails
                          join cutplnmstr in entty.CutPlanMasters on marasq.CutPlan_PK equals cutplnmstr.CutPlan_PK
                          where marasq.Skudet_PK == skudet_PK && cutplnmstr.Location_PK == tofactid && cutplnmstr.OurStyleID == ourstyleid
                          select new { marasq.CutQty });
                foreach (var element in q4)
                {

                    alreadycutforselectedfactory = float.Parse(element.CutQty.ToString()) + alreadycutforselectedfactory;


                }












                CutPlanDetailsDatannEW cddetdata = new CutOrderBLL.CutPlanDetailsDatannEW();

                cddetdata.RollsinWarehouse = RollinWharehousecount;
                cddetdata.YardsofRollsinWhareHouse = RollinWharehouseayardsum;


                cddetdata.balRollCount = balcountcount;
                cddetdata.balanceyard = baldayardsum;


                cddetdata.RollsAlreadyinFactory = deliveredtofactorycount;
                cddetdata.YardsofRollDeliveredtoFactory = RollinWharehouseayardsum;

                cddetdata.alreadycutoflocation = alreadycutforselectedfactory;
                cddetdata.bomconsumption = consumption;
                //  cddetdata.balanceyard = ayardsum;
                cddetdata.CutplanQtyIssued = CutplanQtyIssued;
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


        public static float GetCutFabreq(int cutplanpk)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        isnull(CutPlanFabReq, (SUM(CutPlanASQDetails.CutQty) *CutPlanMaster.BOMConsumption))as Fabreq
FROM            CutPlanASQDetails INNER JOIN
                         CutPlanMaster ON CutPlanASQDetails.CutPlan_PK = CutPlanMaster.CutPlan_PK
GROUP BY CutPlanMaster.CutPlan_PK, CutPlanMaster.BOMConsumption,CutPlanMaster.CutPlanFabReq
HAVING        (CutPlanMaster.CutPlan_PK = @Param1)";
            cmd.Parameters.AddWithValue("@param1", cutplanpk);
            float balqty = float.Parse(QueryFunctions.ReturnQueryValue(cmd).ToString());

            return balqty;
        }


        public static float GetConsumptionBasedonHistory(int ourstyleid, int location_pk, int skudet_pk, float balanceQty, float bomConsumption)
        {
            float consumptio = 0;
            DataTable dt = GetPreviousCutPlansofSkuofLocation(ourstyleid, location_pk, skudet_pk, balanceQty, bomConsumption);

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {


                    if (dt.Rows[0][0].ToString() != "")
                    {


                        object SumFabreq= dt.Compute("Sum(fabreq)", "");


                        object SumQty = dt.Compute("Sum(Qty)", "");

                        consumptio = float.Parse(SumFabreq.ToString()) / float.Parse(SumQty.ToString());
                    }
                }
            }
                        return consumptio;
        }





        public static DataTable GetPreviousCutPlansofSkuofLocation(int ourstyleid,int location_pk,int skudet_pk,float  balanceQty , float bomConsumption)
        {
            DataTable dt = DBTransaction.Productiontransaction.CutPlanTransaction.GetPreviousCutPlansofSkuofLocation(ourstyleid, location_pk, skudet_pk);

            if (dt != null)
            {
                if(dt.Rows.Count!=0)
                {


                    if(dt.Rows[0][0].ToString()!="")
                    {

                        foreach (DataColumn col in dt.Columns)
                            col.ReadOnly = false;


                        if (balanceQty > 0)
                        {
                            dt.Rows.Add(balanceQty, bomConsumption, balanceQty * bomConsumption);
                        }
                      

                        


                        

                    }

                }
            }
            return dt;
        }

        public static float getAlreadyAllocatedAyardage(int cutplanpk)
        {
            float ayardsumfloat = 0;
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                try
                {
                    var ayardsum = entty.CutPlanRollDetails.Where(u => u.CutPlan_PK == cutplanpk && u.IsDeleted=="N").Sum(u => u.FabricRollmaster.AYard);
                    ayardsumfloat = float.Parse(ayardsum.ToString());
                }
                catch (Exception)
                {

                    ayardsumfloat = 0;
                }
            }

            return ayardsumfloat;
        }

        public static void getBOmConsumptionCalculated()
        {


            Decimal BomConsumption = 0;
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


        public static  String GetreferncepatterofCutplan(int cutplanpk)
        {
            String pattername = "";
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                int ourstyleid = 0, skudetpk = 0, locationpk = 0;
                string shrinkagegroup = "";

                try
                {
                    var q = from cutplnmstr in entty.CutPlanMasters
                            where cutplnmstr.CutPlan_PK == cutplanpk
                            select new { cutplnmstr.Location_PK, cutplnmstr.SkuDet_PK, cutplnmstr.OurStyleID, cutplnmstr.ShrinkageGroup };
                    foreach (var element in q)
                    {
                        ourstyleid = int.Parse(element.OurStyleID.ToString());
                        skudetpk = int.Parse(element.SkuDet_PK.ToString());
                        locationpk = int.Parse(element.Location_PK.ToString());
                        shrinkagegroup = element.ShrinkageGroup.ToString();

                    }
                    pattername = Getreferncepatter(ourstyleid, skudetpk, locationpk, shrinkagegroup);
                }
                catch (Exception)
                {


                }

            }
            return pattername;
        }





        public static String Getreferncepatter(int ourstyleid, int skudetpk, int locationpk, string shrinkagegroup)
        {
            String pattername = "";
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {


                try
                {
                 //   var q = entty.PatternNameBanks.Where(u => u.OurStyleID == ourstyleid && u.Skudetpk == skudetpk && u.Location_Pk == locationpk && u.Shrinkage == shrinkagegroup).Select(u => u).ToList();
                    var q = entty.PatternNameBanks.Where(u => u.OurStyleID == ourstyleid && u.Location_Pk == locationpk && u.Shrinkage == shrinkagegroup).Select(u => u).ToList();

                    foreach (var element in q)
                    {

                        pattername =  element.PatternName;
                    }

                }
                catch (Exception)
                {


                }

            }
            return pattername;
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

            cmd.CommandText = @"Select  sum(CutPlanFabReq)from  (SELECT        SUM(CutPlanFabReq) AS CutPlanFabReq
FROM            (SELECT        ISNULL(CutPlanFabReq,
                             (SELECT        ISNULL(SUM(CutQty), 0) AS CutQty
                               FROM            CutPlanASQDetails
                               WHERE        (CutPlan_PK = CutPlanMaster.CutPlan_PK)) * BOMConsumption) AS CutPlanFabReq, SkuDet_PK, OurStyleID, Location_PK, CutPlan_PK, ShrinkageGroup, WidthGroup, MarkerType
FROM            CutPlanMaster
WHERE        (SkuDet_PK = @skudet_pk) AND (Location_PK = @location) AND (ShrinkageGroup = @ShrinkageGroup) AND (WidthGroup = @WidthGroup) AND (MarkerType = @MarkerType) AND (IsDeleted = N'N')) AS tt
GROUP BY SkuDet_PK, OurStyleID, Location_PK, CutPlan_PK)  as tt";

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

        public static DataTable GetASQSizeData(int popackid, int ourstyleid,String Colorname)
        {
            DataTable dt = new DataTable();

            dt = DBTransaction.Productiontransaction.CutPlanTransaction.GetPopackDetailsdforAddingtoCutplan(popackid, ourstyleid,Colorname);



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
        public String CutType { get; set; }
        
        public String Maxmarkerlength { get; set; }
        public String Fabrication { get; set; }
        public Decimal Efficiecny { get; set; }
        public Decimal cutplanConsumption { get; set; }

        public String newRefPattern { get; set; }
        public String Refpattern { get; set; }
        public string UnapproveReason { get; set; }
        public List<CutPlanDetailsData> CutPlanDetailsDataCollection { get; set; }
        public List<CutPlanMarkerDetailsData> CutPlanMarkerDetailsDataCollection { get; set; }
        public List<CutPlanMarkerTypeData> CutPlanMarkerTypeDataDataCollection { get; set; }



        public int Rejectionid { get; set; }
        public String Rejectionreason { get; set; }




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
                    ctmstr.IsRollAdded = "N";
                    ctmstr.RefPattern = "";
                    ctmstr.RollYard = 0;
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
                        cddetail.CutPlanmarkerTypeName = di.MarkerTypeName;

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

        public String InsertNewCutPlanMasterWithASQ()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                using (var dbContextTransaction = enty.Database.BeginTransaction())
                {


                    try
                    {
                     String fabdesc=   this.FabDescription + ' ' + this.ColorCode;

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
                        ctmstr.FabDescription = fabdesc;
                        ctmstr.BOMConsumption = this.BOMConsumption;
                        ctmstr.MarkerMade = this.MakerMade;
                        ctmstr.CutType = this.CutType;
                        ctmstr.IsPatternAdded = "N";
                        ctmstr.IsApproved = "N";
                        ctmstr.IsRatioAdded = "N";
                        ctmstr.IsDeleted = "N";
                        ctmstr.IsCutorderGiven = "N";
                        ctmstr.IsRollAdded = "N";
                        ctmstr.IsRejected = "N";
                        ctmstr.RefPattern = "";
                        ctmstr.RollYard = 0;
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
                            cddetail.CutPlanmarkerTypeName = di.MarkerTypeName;

                            enty.CutPlanMarkerTypes.Add(cddetail);
                        }
                        foreach (CutPlanDetailsData di in this.CutPlanDetailsDataCollection)
                        {

                            var popackdets_pk = enty.POPackDetails.Where(u => u.SizeName == di.SizeName && u.ColorName == di.ColorName && u.OurStyleID == di.OurStyleId && u.POPackId == di.PoPackId).Select(u => u.PoPack_Detail_PK).FirstOrDefault();

                            CutPlanASQDetail cddetail = new CutPlanASQDetail();
                            cddetail.CutPlan_PK = ctmstr.CutPlan_PK;
                            cddetail.PoPackId = di.PoPackId;
                            cddetail.PoPack_Detail_PK = int.Parse(popackdets_pk.ToString());
                            cddetail.CutQty = di.CutQty;
                            cddetail.ColorName = di.ColorName;
                            cddetail.SizeName = di.SizeName;
                            cddetail.Skudet_PK = di.skudet_PK;
                            cddetail.OurStyleId = this.OurStyleID;
                            cddetail.IsDeleted = "N";
                            cddetail.AddedVia = "Add";
                            cddetail.AddedDate = DateTime.Now;
                            cddetail.AddedBy = this.AddedBy;
                            enty.CutPlanASQDetails.Add(cddetail);
                        }
                        enty.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                   
                    catch (Exception ex)
                    {
                        Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                        dbContextTransaction.Rollback();
                    }
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

                    var popackdets_pk = enty.POPackDetails.Where(u => u.SizeName == di.SizeName && u.ColorName == di.ColorName && u.OurStyleID == di.OurStyleId && u.POPackId == di.PoPackId).Select(u => u.PoPack_Detail_PK).FirstOrDefault();

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
                    element.IsRejected = "N";
                    element.ApprovedBy = HttpContext.Current.Session["Username"].ToString().Trim(); ;
                    element.ApprovedDate = DateTime.Now;
                }
                enty.SaveChanges();
            }
       }

        public string UnApproveCutPlan(int cutplan_pk)
        {
            string msg = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                Cutplan_UnapproveHistory cutunapp = new Cutplan_UnapproveHistory();
                if (enty.CutPlanMasters .Any(f => f.CutPlan_PK == cutplan_pk && f.IsCutorderGiven =="N"))
                {
               
                    var q = from ctplnmstr in enty.CutPlanMasters
                        where ctplnmstr.CutPlan_PK == cutplan_pk
                        select ctplnmstr;

                foreach (var element in q)
                {
                        cutunapp.cutplan_pk = cutplan_pk;
                        cutunapp.AddedDate = DateTime.Now;
                        cutunapp.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                        cutunapp.reason = this.UnapproveReason;
                        enty.Cutplan_UnapproveHistory.Add(cutunapp);
                        element.IsApproved = "N";
                    element.IsPatternAdded  = "N";                    

                }
                enty.SaveChanges();
                    msg = "Cutplan Unapproved Successfully";
            }
                else
                {
                    msg = "Cutorder is already given. Delete CutOrder First";
                }
            }
            return msg;
        }


        public void RejectCutPlan(int cutplan_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from ctplnmstr in enty.CutPlanMasters
                        where ctplnmstr.CutPlan_PK == cutplan_pk
                        select ctplnmstr;

                foreach (var element in q)
                {
                    element.RejectionReason = this.Rejectionreason;
                    element.IsRejected = "Y";
                    element.IsApproved = "N";
                    element.RejectedBy = HttpContext.Current.Session["Username"].ToString().Trim(); ;
                    element.RejectionDate = DateTime.Now;
                    
                }
                CutPlanRejectHistory cutplnhistory = new CutPlanRejectHistory();
                cutplnhistory.Cutplan_PK = this.CutPlan_PK;
                cutplnhistory.CutPlanRejectionID = this.Rejectionid;
                cutplnhistory.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim(); ;
                cutplnhistory.AddedDate = DateTime.Now;
                enty.CutPlanRejectHistories.Add(cutplnhistory);
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
                    element.IsDeleted = "Y";
                 //   enty.CutPlanMarkerSizeDetails.Remove(element);
                   
                }


                var q2 = from ponmbr in enty.CutPlanMarkerTypes
                        where ponmbr.CutPlan_PK == cutplan_pk
                        select ponmbr;


                foreach (var element in q2)
                {
                  //  element.IsDeleted = "Y";
                  //  enty.CutPlanMarkerTypes.Remove(element);
                    
                }

                var q3 = from ponmbr in enty.CutPlanMarkerDetails
                        where ponmbr.CutPlan_PK == cutplan_pk
                        select ponmbr;


                foreach (var element in q3)
                {
                    element.IsDeleted = "Y";
                   // enty.CutPlanMarkerDetails.Remove(element);
                  
                }

                var q4 = from ponmbr in enty.CutPlanASQDetails
                         where ponmbr.CutPlan_PK == cutplan_pk
                         select ponmbr;


                foreach (var element in q4)
                {
                    element.IsDeleted = "Y";
                   // enty.CutPlanASQDetails.Remove(element);
                   
                }



                var q6= from ponmbr in enty.CutPlanRollDetails
                        where ponmbr.CutPlan_PK == cutplan_pk
                        select ponmbr;


                foreach (var element in q6)
                {
                    //element.IsDeleted = "Y";
                    //element.DeletedBy = HttpContext.Current.Session["Username"].ToString().Trim();

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

                    element.IsDeleted = "Y";
                    element.DeletedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.DeletedDate = DateTime.Now;
                    element.RollYard = 0;
                    //    enty.CutPlanMasters.Remove(element);




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




        public string AddCutPlanPattern(int cutplan_pk ,string patternanme)
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q1 = from cutplnmstr in enty.CutPlanMasters
                         where cutplnmstr.CutPlan_PK == cutplan_pk
                         select cutplnmstr;
                foreach (var element in q1)
                {
                    if (!enty.PatternNameBanks.Any(f => f.OurStyleID == element.OurStyleID && f.Location_Pk == element.Location_PK && f.Shrinkage.Trim() == element.ShrinkageGroup.Trim() && f.PatternName.Trim() == patternanme.Trim()))
                    {

                        PatternNameBank ctpnref = new PatternNameBank();

                        ctpnref.OurStyleID = element.OurStyleID;
                        ctpnref.Skudetpk = element.SkuDet_PK;
                        ctpnref.Shrinkage = element.ShrinkageGroup;

                        ctpnref.PatternName = patternanme.Trim();
                        ctpnref.Location_Pk = element.Location_PK;
                        ctpnref.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                        ctpnref.AddedDate = DateTime.Now;

                        enty.PatternNameBanks.Add(ctpnref);
                    }

                }


                   

                enty.SaveChanges();

                Cutn = "Sucess";

            }

            return Cutn;
        }


        public string ReopenCutplan(int cutplan_pk)
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q1 = from cutplnmstr in enty.CutPlanMasters
                         where cutplnmstr.CutPlan_PK == cutplan_pk
                         select cutplnmstr;
                foreach (var element in q1)
                {
                    element.IsPatternAdded = "N";
                    element.IsCutorderGiven = "N";
                    element.IsApproved = "N";

                }




                enty.SaveChanges();

                Cutn = "Sucess";

            }

            return Cutn;
        }


    }


    public class CutPlanMarkerTypeData
    {
        public int CutPlan_PK { get; set; }
        public String MarkerTypeName { get; set; }
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
        public String UOMName { get; set; }
        public int TotalRollCount { get; set; }
        public float TotalrollYard { get; set; }

        public int DeliverdRollCount { get; set; }
        public float DeliverdrollYard { get; set; }
        public float deliveredayardsumdummy { get; set; }
        
        public int RollCount { get; set; }
        public float rollYard { get; set; }

        public float bomconsumption { get; set; }

        public int balRollCount { get; set; }
        public float balanceyard { get; set; }

        public float balWeightSum { get; set; }

        public float alreadycutoflocation { get; set; }


        public int CutplanBlockedRoll { get; set; }
        public Decimal CutplanBlockedYardage { get; set; }
        public Decimal alreadycutforselectedfactoryofgroup { get; set; }


    }



    public class CutPlanDetailsDatannEW
    {


        public int CutPlanASQDetails_PK { get; set; }
        public int CutPlan_PK { get; set; }
        public int PoPackId { get; set; }
        public int OurStyleId { get; set; }
        public int skudet_PK { get; set; }
        public int PoPack_Detail_PK { get; set; }
        public float CutplanQtyIssued { get; set; }
        public int CutQty { get; set; }
        public String ColorName { get; set; }
        public String SizeName { get; set; }

        public int RollsinWarehouse { get; set; }
        public float YardsofRollsinWhareHouse { get; set; }

        public int RollsAlreadyinFactory { get; set; }
        public float YardsofRollDeliveredtoFactory { get; set; }

        public int TotalRolls { get; set; }
        public float TotalRollYard { get; set; }

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
                ctmdet.IsDeleted = "N";
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
                    cddetail.IsDeleted = "N";
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
            int cutplanpk = 0;
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
                        cutplanpk = int.Parse(element.CutPlan_PK.ToString());
                       

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

                    var cutplnmstr = from cutplanmaster in enty.CutPlanMasters
                                     where cutplanmaster.CutPlan_PK == cutplanpk
                                     select cutplanmaster;
                    foreach (var element in cutplnmstr)
                    {
                        element.IsPatternAdded = "N";
                        element.IsRejected = "N";
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
                    ctmdet.IsDeleted = "N";

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
                        cddetail.IsDeleted = "N";
                        enty.CutPlanMarkerSizeDetails.Add(cddetail);

                        enty.SaveChanges();



                    }


                }















              


                enty.SaveChanges();



            }

            return Cutn;

        }



        public string AddCutplanRoll(ArrayList Arry,int CutPlan_PK)
        {
            string sucess = "";

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                for(int i=0;i<Arry.Count;i++)
                {

                    CutPlanRollDetail cdrlldet = new DataModels.CutPlanRollDetail() ;
                    cdrlldet.Roll_PK = int.Parse (Arry[i].ToString ());
                    cdrlldet.CutPlan_PK = CutPlan_PK;
                    cdrlldet.IsDeleted = "N";
                    cdrlldet.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim(); ;
                    cdrlldet.AddedDate = DateTime.Now;

                    enty.CutPlanRollDetails.Add(cdrlldet);

                    sucess = "true";
                }
                enty.SaveChanges();

                if (sucess== "true")
                {
                    var allocatedqty = enty.CutPlanRollDetails.Where(i => i.CutPlan_PK == CutPlan_PK && i.IsDeleted=="N" ).Select(i => i.FabricRollmaster.AYard).DefaultIfEmpty(0).Sum();
                    var q = from cplmstr in enty.CutPlanMasters
                            where cplmstr.CutPlan_PK == CutPlan_PK
                            select cplmstr;
                    foreach(var element in q)
                    {
                        element.IsRollAdded = "Y";
                        element.RollYard = Decimal.Parse(allocatedqty.ToString());
                    }

                }
                enty.SaveChanges();

                sucess = "Roll Details Added Successfully";
            }




                return sucess;
        }

        public String DeleteCutplanRoll(int prpl_pk)
        {
            string asqshuffle = "Error";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var rollpkobj = enty.CutPlanRollDetails.Where(u => u.CutPlanRoll_PK == prpl_pk).Select(u => u.Roll_PK).FirstOrDefault();
                int rollpk = int.Parse(rollpkobj.ToString());

                if (!enty.LaySheetRollDetails.Any(f => f.Roll_PK == rollpk))
                {
                    var q = from ppl in enty.CutPlanRollDetails
                            where ppl.CutPlanRoll_PK == prpl_pk
                            select ppl;

                    foreach (var element in q)
                    {
                        element.IsDeleted = "Y";
                        element.DeletedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    }
                    var allocatedqty = enty.CutPlanRollDetails.Where(i => i.CutPlan_PK == CutPlan_PK && i.IsDeleted == "N").Select(i => i.FabricRollmaster.AYard).DefaultIfEmpty(0).Sum();
                    var q1 = from cplmstr in enty.CutPlanMasters
                            where cplmstr.CutPlan_PK == CutPlan_PK
                            select cplmstr;
                    foreach (var element in q1)
                    {
                       
                        element.RollYard = Decimal.Parse(allocatedqty.ToString());
                    }
                    enty.SaveChanges();
                    asqshuffle = "Sucessfully Deleted";
                }
                else
                {
                    asqshuffle = "Rolls Used in Laysheet Roll Reference Delete it first ";
                 //   Controls.WebMsgBox.Show("Rolls Used in Lausheet Roll Reference Delete it first ");
                }
              
            }

            return asqshuffle;
        }



        public String DeleteCutOrderMarkerSizeData( int CutPlanMarkerDetails_PK)
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {




                var q1 = from cutplanmarksize in enty.CutPlanMarkerSizeDetails
                         where cutplanmarksize.CutPlanMarkerDetails_PK == CutPlanMarkerDetails_PK
                         select cutplanmarksize;
                foreach (var element1 in q1)
                {

                    enty.CutPlanMarkerSizeDetails.Remove(element1);
                }
                enty.SaveChanges();
                var q = from cutplandet in enty.CutPlanMarkerDetails
                            where cutplandet.CutPlanMarkerDetails_PK == CutPlanMarkerDetails_PK
                            select cutplandet;

               foreach (var element in q)
               {

                    enty.CutPlanMarkerDetails.Remove(element);

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