using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ArtWebApp.DataModels;
using System.Collections;
using ArtWebApp.DataModelAtcWorld;

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

        public DataTable getRollofaCutorderNotlayed(int cutid, int factid)
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
GROUP BY LaySheetMaster.CutOrderDet_PK , LaySheetDetails.IsDeleted
HAVING        (LaySheetMaster.CutOrderDet_PK = @param1) AND (LaySheetDetails.IsDeleted = N'N'))tt";
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

        public float GetActualCutPlies(int CutOrderDet_PK)
        {
            float balqty = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"
Select isnull((SELECT        ISNULL(SUM(LaySheetDetails.NoOfPlies), 0) AS NoOfPlies
FROM            LaySheetDetails INNER JOIN
                         LaySheetMaster ON LaySheetDetails.LaySheet_PK = LaySheetMaster.LaySheet_PK
GROUP BY LaySheetMaster.CutOrderDet_PK, LaySheetDetails.IsDeleted
HAVING        (LaySheetMaster.CutOrderDet_PK = @param1) AND (LaySheetDetails.IsDeleted = N'N')),0) ";
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
                        where cutordrdet.CutOrderDet_PK == CutOrderDet_PK
                        select new { cutplndet.CutPerPlies, cutplndet.NoOfPlies, cutplndet.Cutreq };

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





    public static class LaySheetfunction
    {





        public static DataTable CalculateAlreadyCut(DataTable dt, float cutplies)
        {

            if (cutplies.ToString().Trim() != "" && cutplies.ToString() != "0")
            {
                int cutrowinde = 2;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Rows[2][i] = cutplies;
                }


            }



            return dt;
        }



        public static  void ApprovelaysheetAction(int laysheetPK)
        {
            int locationpk = 0;
            using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {
                var locationpkvar = enty.LaySheetMasters.Where(u => u.LaySheet_PK == laysheetPK).Select(u => u.Location_PK).FirstOrDefault();
                locationpk = int.Parse(locationpkvar.ToString());
            }
            if(locationpk==13 || locationpk == 14)
            {
                ApproveLaysheetEthiopia(laysheetPK);
            }
            else
            {
                ApproveLaysheet(laysheetPK);
            }
        }

        public static void unaprovallaysheet(int layshhet_pk)
        {
            using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {
                var q = from lymstr in enty.LaySheetMasters
                        where lymstr.LaySheet_PK == layshhet_pk
                        select lymstr;
                foreach (var element in q)
                {

                    element.IsApproved = "Y";
                   

                }


            }

            }



            public static void ApproveLaysheet(int laysheetPK)
        {
            Boolean ismasteralreadypresent = false;

            int CutPlanMarkerDetails_PK = 0;

            int cutplanpk = 0;

            using (AtcWorldEntities atcenty = new AtcWorldEntities())
            {

                using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
                {



                    // approves Kenya

                    var q1 = from lymstr in atcenty.ArtLaySheetMasterDatas
                             where lymstr.LaySheet_PK == laysheetPK
                             select lymstr;

                   
                        foreach (var atcwolrdelement in q1)
                        {

                            atcwolrdelement.IsApproved = "Y";
                            ismasteralreadypresent = true;

                        }
                    



                    //approves dubai and then get cutplanmarkerdetails
                    var q = from lymstr in enty.LaySheetMasters
                            where lymstr.LaySheet_PK == laysheetPK
                            select lymstr;

                    foreach (var element in q)
                    {

                        element.IsApproved = "Y";
                        CutPlanMarkerDetails_PK = int.Parse(element.CutOrderDetail.CutPlanMarkerDetails_PK.ToString());

                        if (ismasteralreadypresent == true)
                        {
                            element.IsEdited = "N";
                            element.IsUploaded = "N";
                        }

                    }

                    if (ismasteralreadypresent == false)
                    {


                        DataTable dt = GetLaysheetMasterDataFromArt(laysheetPK);

                        ArtLaySheetMasterData lymstrdata = new DataModelAtcWorld.ArtLaySheetMasterData();
                        foreach (DataRow dr in dt.Rows)
                        {
                            lymstrdata.CutPlanNUM = dr["CutPlanNUM"].ToString();
                            lymstrdata.ColorCode = dr["ColorName"].ToString();
                            lymstrdata.LaySheetNum = dr["LaySheetNum"].ToString().Trim();
                            lymstrdata.LocationName = dr["LocationName"].ToString();
                            lymstrdata.Location_PK = int.Parse(dr["Location_PK"].ToString());
                            lymstrdata.CutOrderDet_PK = int.Parse(dr["CutOrderDet_PK"].ToString());
                            lymstrdata.CutID = int.Parse(dr["CutID"].ToString());
                            lymstrdata.Cut_NO = dr["Cut_NO"].ToString();
                            lymstrdata.Shrinkage = dr["Shrinkage"].ToString();
                            lymstrdata.MarkerType = dr["MarkerType"].ToString();
                            lymstrdata.CutWidth = dr["CutWidth"].ToString();
                            lymstrdata.AtcID = int.Parse(dr["AtcID"].ToString());
                            lymstrdata.OurStyleID = int.Parse(dr["OurStyleID"].ToString());
                            lymstrdata.CutPlan_PK = int.Parse(dr["CutPlan_PK"].ToString());
                            lymstrdata.LaySheet_PK = int.Parse(dr["LaySheet_PK"].ToString());
                            lymstrdata.CutPlanMarkerDetails_PK= CutPlanMarkerDetails_PK;
                            lymstrdata.MarkerNUM = int.Parse(dr["MarkerNo"].ToString());
                            lymstrdata.IsApproved = dr["IsApproved"].ToString();


                            atcenty.ArtLaySheetMasterDatas.Add(lymstrdata);
                        }
                    }

                    //CutplanASQ

                    var objCutPlan_PK = enty.CutPlanMarkerSizeDetails.Where(u => u.CutPlanMarkerDetails_PK == CutPlanMarkerDetails_PK).Select(u => u.CutPlan_PK).FirstOrDefault();
                    cutplanpk = int.Parse(objCutPlan_PK.ToString());
                    var cutplanASQ = (from cplasq in enty.CutPlanASQDetails
                                      where cplasq.CutPlan_PK == cutplanpk
                                      select cplasq).ToList();

                    foreach (var element in cutplanASQ)
                    {
                        int cutplanasqdet = int.Parse(element.CutPlanASQDetails_PK.ToString());

                        if (!atcenty.ArtCutPlanASQDets.Any(f => f.CutPlanASQDetails_PK == cutplanasqdet))
                        {
                            ArtCutPlanASQDet asqdet = new DataModelAtcWorld.ArtCutPlanASQDet();

                            asqdet.PoPackId = int.Parse(element.PoPackId.ToString());
                            asqdet.PoPack_Detail_PK = int.Parse(element.PoPack_Detail_PK.ToString());
                            asqdet.ColorName = element.ColorName.ToString();
                            asqdet.SizeName = element.SizeName.ToString();
                            asqdet.CutQty = Decimal.Parse(element.CutQty.ToString());
                            asqdet.CutPlan_PK = int.Parse(element.CutPlan_PK.ToString());
                            asqdet.CutPlanASQDetails_PK = int.Parse(element.CutPlanASQDetails_PK.ToString());

                            atcenty.ArtCutPlanASQDets.Add(asqdet);


                        }

                    }


                    var cutplanmarkerSize = (from cplasqmar in enty.CutPlanMarkerSizeDetails
                                             where cplasqmar.CutPlanMarkerDetails_PK == CutPlanMarkerDetails_PK
                                             select cplasqmar).ToList();

                    foreach (var element in cutplanmarkerSize)
                    {
                        int CutPlanSize_PK = int.Parse(element.CutPlanSize_PK.ToString());

                        if (!atcenty.ArtCutPlanMarkerSizeDetails.Any(f => f.CutPlanSize_PK == CutPlanSize_PK))
                        {
                            DataModelAtcWorld.ArtCutPlanMarkerSizeDetail asqdet = new DataModelAtcWorld.ArtCutPlanMarkerSizeDetail();



                            asqdet.CutPlanSize_PK = int.Parse(element.CutPlanSize_PK.ToString());
                            asqdet.Size = element.Size.ToString();
                            asqdet.Ratio = int.Parse(element.Ratio.ToString());
                            asqdet.Qty = Decimal.Parse(element.Qty.ToString());
                            asqdet.CutPlan_PK = int.Parse(element.CutPlan_PK.ToString());
                            asqdet.CutPlanMarkerDetails_PK = int.Parse(element.CutPlanMarkerDetails_PK.ToString());


                            atcenty.ArtCutPlanMarkerSizeDetails.Add(asqdet);


                        }

                    }


                    var laysheetDetails = (from lydet in enty.LaySheetDetails
                                             where lydet.LaySheet_PK == laysheetPK
                                             select lydet).ToList();

                    foreach (var element in laysheetDetails)
                    {

                        if (!atcenty.ArtLaySheetDetails.Any(f => f.LaySheetDet_PK == element.LaySheetDet_PK))
                        {
                            string layshheetnum = "";

                            try
                            {
                                var laysheetrefnum = element.LaySheetRollDetail.LaySheetRollMaster.LocationSequencenum;
                                layshheetnum = laysheetrefnum.ToString();
                            }
                            catch (Exception)
                            {

                               
                            }

                            ArtLaySheetDetail artlydet = new ArtLaySheetDetail();

                            artlydet.LaySheetDet_PK = element.LaySheetDet_PK;
                            artlydet.LaySheet_PK = element.LaySheet_PK;
                            artlydet.Roll_PK = element.Roll_PK;
                            artlydet.NoOfPlies = element.NoOfPlies;
                            artlydet.ShadeGroup = element.FabricRollmaster.ShadeGroup;
                            artlydet.LocationSequencenum = layshheetnum;
                            atcenty.ArtLaySheetDetails.Add(artlydet);
                        }
                    }


                        atcenty.SaveChanges();
                    enty.SaveChanges();
                }
            }

        }



        public static void ApproveLaysheetEthiopia(int laysheetPK)
        {
            Boolean ismasteralreadypresent = false;

            int CutPlanMarkerDetails_PK = 0;

            int cutplanpk = 0;

            using (AtcWorldEntities atcenty = new AtcWorldEntities("Ethiopia"))
            {

                using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
                {



                    // approves Kenya

                    var q1 = from lymstr in atcenty.ArtLaySheetMasterDatas
                             where lymstr.LaySheet_PK == laysheetPK
                             select lymstr;

                    foreach (var atcwolrdelement in q1)
                    {

                        atcwolrdelement.IsApproved = "Y";
                        ismasteralreadypresent = true;

                    }



                    //approves dubai and then get cutplanmarkerdetails
                    var q = from lymstr in enty.LaySheetMasters
                            where lymstr.LaySheet_PK == laysheetPK
                            select lymstr;

                    foreach (var element in q)
                    {

                        element.IsApproved = "Y";
                        CutPlanMarkerDetails_PK = int.Parse(element.CutOrderDetail.CutPlanMarkerDetails_PK.ToString());

                        if (ismasteralreadypresent == true)
                        {
                            element.IsEdited = "N";
                            element.IsUploaded = "N";
                        }

                    }

                    if (ismasteralreadypresent == false)
                    {


                        DataTable dt = GetLaysheetMasterDataFromArt(laysheetPK);

                        ArtLaySheetMasterData lymstrdata = new DataModelAtcWorld.ArtLaySheetMasterData();
                        foreach (DataRow dr in dt.Rows)
                        {
                            lymstrdata.CutPlanNUM = dr["CutPlanNUM"].ToString();
                            lymstrdata.ColorCode = dr["ColorName"].ToString();
                            lymstrdata.LaySheetNum = dr["LaySheetNum"].ToString().Trim();
                            lymstrdata.LocationName = dr["LocationName"].ToString();
                            lymstrdata.Location_PK = int.Parse(dr["Location_PK"].ToString());
                            lymstrdata.CutOrderDet_PK = int.Parse(dr["CutOrderDet_PK"].ToString());
                            lymstrdata.CutID = int.Parse(dr["CutID"].ToString());
                            lymstrdata.Cut_NO = dr["Cut_NO"].ToString();
                            lymstrdata.Shrinkage = dr["Shrinkage"].ToString();
                            lymstrdata.MarkerType = dr["MarkerType"].ToString();
                            lymstrdata.CutWidth = dr["CutWidth"].ToString();
                            lymstrdata.AtcID = int.Parse(dr["AtcID"].ToString());
                            lymstrdata.OurStyleID = int.Parse(dr["OurStyleID"].ToString());
                            lymstrdata.CutPlan_PK = int.Parse(dr["CutPlan_PK"].ToString());
                            lymstrdata.LaySheet_PK = int.Parse(dr["LaySheet_PK"].ToString());
                            lymstrdata.CutPlanMarkerDetails_PK = CutPlanMarkerDetails_PK;
                            
                            lymstrdata.IsApproved = dr["IsApproved"].ToString();


                            atcenty.ArtLaySheetMasterDatas.Add(lymstrdata);
                        }
                    }

                    //CutplanASQ

                    var objCutPlan_PK = enty.CutPlanMarkerSizeDetails.Where(u => u.CutPlanMarkerDetails_PK == CutPlanMarkerDetails_PK).Select(u => u.CutPlan_PK).FirstOrDefault();
                    cutplanpk = int.Parse(objCutPlan_PK.ToString());
                    var cutplanASQ = (from cplasq in enty.CutPlanASQDetails
                                      where cplasq.CutPlan_PK == cutplanpk
                                      select cplasq).ToList();

                    foreach (var element in cutplanASQ)
                    {
                        int cutplanasqdet = int.Parse(element.CutPlanASQDetails_PK.ToString());

                        if (!atcenty.ArtCutPlanASQDets.Any(f => f.CutPlanASQDetails_PK == cutplanasqdet))
                        {
                            ArtCutPlanASQDet asqdet = new DataModelAtcWorld.ArtCutPlanASQDet();

                            asqdet.PoPackId = int.Parse(element.PoPackId.ToString());
                            asqdet.PoPack_Detail_PK = int.Parse(element.PoPack_Detail_PK.ToString());
                            asqdet.ColorName = element.ColorName.ToString();
                            asqdet.SizeName = element.SizeName.ToString();
                            asqdet.CutQty = Decimal.Parse(element.CutQty.ToString());
                            asqdet.CutPlan_PK = int.Parse(element.CutPlan_PK.ToString());
                            asqdet.CutPlanASQDetails_PK = int.Parse(element.CutPlanASQDetails_PK.ToString());

                            atcenty.ArtCutPlanASQDets.Add(asqdet);


                        }

                    }


                    var cutplanmarkerSize = (from cplasqmar in enty.CutPlanMarkerSizeDetails
                                             where cplasqmar.CutPlanMarkerDetails_PK == CutPlanMarkerDetails_PK
                                             select cplasqmar).ToList();

                    foreach (var element in cutplanmarkerSize)
                    {
                        int CutPlanSize_PK = int.Parse(element.CutPlanSize_PK.ToString());

                        if (!atcenty.ArtCutPlanMarkerSizeDetails.Any(f => f.CutPlanSize_PK == CutPlanSize_PK))
                        {
                            DataModelAtcWorld.ArtCutPlanMarkerSizeDetail asqdet = new DataModelAtcWorld.ArtCutPlanMarkerSizeDetail();



                            asqdet.CutPlanSize_PK = int.Parse(element.CutPlanSize_PK.ToString());
                            asqdet.Size = element.Size.ToString();
                            asqdet.Ratio = int.Parse(element.Ratio.ToString());
                            asqdet.Qty = Decimal.Parse(element.Qty.ToString());
                            asqdet.CutPlan_PK = int.Parse(element.CutPlan_PK.ToString());
                            asqdet.CutPlanMarkerDetails_PK = int.Parse(element.CutPlanMarkerDetails_PK.ToString());


                            atcenty.ArtCutPlanMarkerSizeDetails.Add(asqdet);


                        }

                    }


                    var laysheetDetails = (from lydet in enty.LaySheetDetails
                                           where lydet.LaySheet_PK == laysheetPK
                                           select lydet).ToList();

                    foreach (var element in laysheetDetails)
                    {

                        if (!atcenty.ArtLaySheetDetails.Any(f => f.LaySheetDet_PK == element.LaySheetDet_PK))
                        {
                            string layshheetnum = "";

                            try
                            {
                                var laysheetrefnum = element.LaySheetRollDetail.LaySheetRollMaster.LocationSequencenum;
                                layshheetnum = laysheetrefnum.ToString();
                            }
                            catch (Exception)
                            {


                            }

                            ArtLaySheetDetail artlydet = new ArtLaySheetDetail();

                            artlydet.LaySheetDet_PK = element.LaySheetDet_PK;
                            artlydet.LaySheet_PK = element.LaySheet_PK;
                            artlydet.Roll_PK = element.Roll_PK;
                            artlydet.NoOfPlies = element.NoOfPlies;
                            artlydet.ShadeGroup = element.FabricRollmaster.ShadeGroup;
                            artlydet.LocationSequencenum = layshheetnum;
                            atcenty.ArtLaySheetDetails.Add(artlydet);
                        }
                    }


                    atcenty.SaveChanges();
                    enty.SaveChanges();
                }
            }

        }

        public static DataTable GetLaysheetMasterDataFromArt(int laysheetpk)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = @"SELECT        CutPlanMaster.CutPlanNUM, CutPlanMaster.ColorName, LaySheetMaster.LaySheetNum, LocationMaster.LocationName, LocationMaster.Location_PK, CutOrderDetails.CutOrderDet_PK, 
CutOrderDetails.CutID, CutOrderMaster.Cut_NO, CutOrderMaster.Shrinkage, CutOrderMaster.MarkerType, CutOrderMaster.CutWidth, CutOrderMaster.AtcID, CutOrderMaster.OurStyleID, CutPlanMaster.CutPlan_PK, 
                         LaySheetMaster.LaySheet_PK, LaySheetMaster.IsEdited, CutOrderMaster.CutQty, LaySheetMaster.IsApproved, LaySheetMaster.IsUploaded, LaySheetMaster.IsDetailUploaded,CutOrderDetails.MarkerNo
FROM            CutOrderDetails INNER JOIN
                         LaySheetMaster ON CutOrderDetails.CutOrderDet_PK = LaySheetMaster.CutOrderDet_PK INNER JOIN
                         CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID INNER JOIN
                         CutPlanMaster ON CutOrderMaster.CutPlan_Pk = CutPlanMaster.CutPlan_PK INNER JOIN
                         LocationMaster ON LaySheetMaster.Location_PK = LocationMaster.Location_PK
GROUP BY CutPlanMaster.CutPlanNUM, CutPlanMaster.ColorName, LaySheetMaster.LaySheetNum, LocationMaster.LocationName, LocationMaster.Location_PK, CutOrderDetails.CutOrderDet_PK, CutOrderDetails.CutID, 
                         CutOrderMaster.Cut_NO, CutOrderMaster.Shrinkage, CutOrderMaster.MarkerType, CutOrderMaster.CutWidth, CutOrderMaster.AtcID, CutOrderMaster.OurStyleID, CutPlanMaster.CutPlan_PK, 
                         LaySheetMaster.LaySheet_PK, LaySheetMaster.IsEdited, CutOrderMaster.CutQty, LaySheetMaster.IsApproved, LaySheetMaster.IsUploaded, LaySheetMaster.IsDetailUploaded, CutOrderDetails.MarkerNo
HAVING        (LaySheetMaster.LaySheet_PK =@laysheetpk)";

                cmd.Parameters.AddWithValue("@laysheetpk", laysheetpk);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);

            }


        }


        public static void SendLaysheetDetailstokenya(int laysheetPK)
        {






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
            decimal? noofplu = 0;
            decimal? excessorshort = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                LaySheetMaster lsmstr = new LaySheetMaster();
                lsmstr.LayLength = this.LayLength;
                lsmstr.Location_PK = this.Location_PK;
                lsmstr.CutOrderDet_PK = this.CutOrderDet_PK;
                lsmstr.AtcID = this.atcid;
                lsmstr.OustyleID = this.ourstyleid;
                lsmstr.AddedBY = HttpContext.Current.Session["Username"].ToString();
                lsmstr.AddedDate = DateTime.Now;
                lsmstr.LayCutNum = this.cutnum;
                lsmstr.NoOfPlies = 0;
               

                lsmstr.IsApproved = "N";
                lsmstr.IsDeleted = "N";
                lsmstr.IsEdited = "Y";
                lsmstr.IsDetailUploaded = "N";
                lsmstr.IsUploaded = "N";
                enty.LaySheetMasters.Add(lsmstr);
                enty.SaveChanges();
                Cutn = lsmstr.LaySheetNum = "L" + this.LayRollRef + '/' + lsmstr.LaySheet_PK;
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
                    lcdet.IsDeleted = "N";
                    lcdet.AYard = di.RollAyard;
                    lcdet.ExcessOrShort = di.ExceSShortage;
                    lcdet.LaySheetRoll_Pk = di.LaySheetRoll_Pk;
                    enty.LaySheetDetails.Add(lcdet);
                    noofplu = noofplu + lcdet.NoOfPlies;
                    excessorshort= excessorshort+ di.ExceSShortage;

                    var qlayroll = from rlldata in enty.LaySheetRollDetails
                                   where rlldata.Roll_PK == di.Roll_PK && rlldata.LaySheetRoll_Pk == di.LaySheetRoll_Pk && rlldata.IsDeleted=="N"
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




                lsmstr.NoOfPlies = noofplu;
                lsmstr.ExxcessOrShort = excessorshort;
                enty.SaveChanges();



            }

            return Cutn;

        }










        public String DeletelaysheetdetailRoll(int laysheetdetpk)
        {
            string Cutn = "";
            decimal? noofplu = 0;
            decimal newbalance = 0;
            String isreusable = "";
            int laysheetrolldetpk = 0;
            int laysheetpk = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from laysheetdet in enty.LaySheetDetails
                        where laysheetdet.LaySheetDet_PK == laysheetdetpk
                        select laysheetdet;
                foreach(var element in q)
                {
                    laysheetrolldetpk = int.Parse( element.LaySheetRoll_Pk.ToString());
                    element.IsDeleted = "Y";
                    element.DeletedBy = HttpContext.Current.Session["Username"].ToString();
                    element.Deleteddate = DateTime.Now;
                    laysheetpk = int.Parse(element.LaySheet_PK.ToString());

                    if (element.IsRecuttable == "Y")
                    {
                        isreusable = "R";
                        newbalance = decimal.Parse(element.FabUtilized.ToString()) + decimal.Parse(element.ExcessOrShort.ToString()) + decimal.Parse(element.BalToCut.ToString());
                    }
                    else
                    {
                        isreusable = "R";
                       
                        newbalance = decimal.Parse(element.FabUtilized.ToString()) + decimal.Parse(element.ExcessOrShort.ToString()) + decimal.Parse(element.BalToCut.ToString());

                    }
                }


                var q1 = from laysheetrolldetil in enty.LaySheetRollDetails
                         where laysheetrolldetil.LaySheetRoll_Pk == laysheetrolldetpk
                         select laysheetrolldetil;

                foreach(var element in q1)
                {
                    element.IsUsed = isreusable;
                    element.BalanceYardage = newbalance;
                }
                enty.SaveChanges();

                try
                {
                    var allocatedqty = enty.LaySheetDetails.Where(i => i.LaySheet_PK == laysheetpk && i.IsDeleted == "N").Select(i => i.FabricRollmaster.AYard).DefaultIfEmpty(0).Sum();
                    noofplu = decimal.Parse(allocatedqty.ToString());
                }
                catch (Exception)
                {

                   
                }

                var q3 = from cplmstr in enty.LaySheetMasters
                         where cplmstr.LaySheet_PK == laysheetpk
                         select cplmstr;
                foreach (var element in q3)
                {

                    element.NoOfPlies = Decimal.Parse(noofplu.ToString());
                }

                enty.SaveChanges();
            }
            return Cutn;
        }


        public String UpdateRollDetailID(int laysheetdetpk)
        {
            String MSG = "";
            string Cutn = "";
            decimal? endbit = 0;
            decimal newbalance = 0;
            String isreusable = "";
            int laysheetrolldetpk = 0;
            int laysheetpk = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from laysheetdet in enty.LaySheetDetails
                        where laysheetdet.LaySheetDet_PK == laysheetdetpk 
                        select laysheetdet;
                foreach (var element in q)
                {

                    if (element.IsDeleted != "Y")
                    {

                        laysheetrolldetpk = int.Parse(element.LaySheetRoll_Pk.ToString());
                        laysheetpk = int.Parse(element.LaySheet_PK.ToString());
                        endbit = decimal.Parse(element.EndBit.ToString());





                        if (element.IsRecuttable == "Y")
                        {

                            var q12 = from laysheetrolldetil in enty.LaySheetRollDetails
                                     where laysheetrolldetil.LaySheetRoll_Pk == laysheetrolldetpk
                                     select laysheetrolldetil;

                            foreach (var element123 in q12)
                            {
                               
                            }





                        }
                        else
                        {
                           
                        }









                    }
                    else
                    {
                        MSG = "Cannot Mark Cuttable on UnCuttable on Deleted Rows";
                    }
                 

                    
                }


                var q1 = from laysheetrolldetil in enty.LaySheetRollDetails
                         where laysheetrolldetil.LaySheetRoll_Pk == laysheetrolldetpk
                         select laysheetrolldetil;

                foreach (var element in q1)
                {
                    element.IsUsed = isreusable;
                    element.BalanceYardage = newbalance;
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


                Cutn = "C" + this.cutnum + "/" + DBTransaction.LaysheetTransaction.getlaysheetnum(this.cutid).ToString() + "/" + this.markernum + "/" + DBTransaction.LaysheetTransaction.getlaysheetnumofcutorder(this.CutOrderDet_PK).ToString();


                LaySheetRollMaster lmmstr = new DataModels.LaySheetRollMaster();
                lmmstr.LayRollRef = Cutn;
                lmmstr.CutID = this.cutid;
                lmmstr.CutOrderDet_PK = this.CutOrderDet_PK;
                lmmstr.NoofPlies = this.NoofPlies;
                lmmstr.Location_Pk = this.Location_PK;
                lmmstr.LocationSequencenum = CreateLaysheetnum(this.Location_PK);
                enty.LaySheetRollMasters.Add(lmmstr);
                enty.SaveChanges();

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
                        lcdet.LaysheetRollmaster_Pk = lmmstr.LaysheetRollmaster_Pk;
                        lcdet.AddedBy = HttpContext.Current.Session["Username"].ToString();
                        lcdet.AddedDate = DateTime.Now;
                        lcdet.Status = di.RollStatus;
                        lcdet.IsDeleted = "N";
                        lcdet.Sequence = di.sequence_no;
                        enty.LaySheetRollDetails.Add(lcdet);
                    }
                    else
                    {
                        var q = from lyrll in enty.LaySheetRollDetails
                                where lyrll.Roll_PK == di.Roll_PK && lyrll.IsUsed == "R"
                                select lyrll;
                        foreach (var element in q)
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
                        lcdet.Status = di.RollStatus;
                        lcdet.AddedBy = HttpContext.Current.Session["Username"].ToString();
                        lcdet.AddedDate = DateTime.Now;
                        lcdet.IsDeleted = "N";
                        lcdet.Sequence = di.sequence_no;
                        enty.LaySheetRollDetails.Add(lcdet);

                    }





                }





                enty.SaveChanges();



            }

            return Cutn;

        }

        public String CreateLaysheetnum(int loctn_PK)
        {
            String laysheetrollnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var count = (from o in enty.LaySheetRollMasters
                             where o.Location_Pk == loctn_PK

                             select o).Count();

                //from buyer master u where u.buyerid = buyerid select prefix


                laysheetrollnum = "LR" + HttpContext.Current.Session["lOC_Code"].ToString().Trim() + (int.Parse(count.ToString()) + 0).ToString();
            }

            return laysheetrollnum;
        }


        //only update the laysheet rolls from edit screen
        public String InsertLaySheetRollRollOnly()
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                //     lsmstr.CutOrderDet_PK = this.CutOrderDet_PK;

                var q = (from layref in enty.LaySheetRollMasters
                         where layref.LaysheetRollmaster_Pk == this.LaysheetRollmaster_Pk
                         select new { layref.CutOrderDet_PK, layref.CutID, layref.LayRollRef }).ToList();
                foreach (var element in q)
                {
                    this.cutid = int.Parse(element.CutID.ToString());
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
                        lcdet.Status = di.RollStatus;
                        lcdet.Yardage = di.RollAyard;
                        lcdet.BalanceYardage = di.RollAyard;
                        lcdet.LayRollRef = this.LayRollRef;
                        lcdet.LaysheetRollmaster_Pk = this.LaysheetRollmaster_Pk;
                        lcdet.AddedBy = HttpContext.Current.Session["Username"].ToString();
                        lcdet.AddedDate = DateTime.Now;
                        lcdet.IsDeleted = "N";
                        lcdet.Sequence = di.sequence_no;
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
                        lcdet.Status = di.RollStatus;
                        lcdet.Yardage = di.RollAyard;
                        lcdet.BalanceYardage = di.RollAyard;
                        lcdet.LayRollRef = this.LayRollRef;
                        lcdet.IsDeleted = "N";
                        lcdet.LaysheetRollmaster_Pk = this.LaysheetRollmaster_Pk;
                        lcdet.AddedBy = HttpContext.Current.Session["Username"].ToString();
                        lcdet.AddedDate = DateTime.Now;
                        lcdet.Sequence = di.sequence_no;

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
                var rollpkobj = enty.LaySheetRollDetails.Where(u => u.LaySheetRoll_Pk == layrrollpk).Select(u => u.Roll_PK).FirstOrDefault();
                int rollpk = int.Parse(rollpkobj.ToString());

                if (!enty.LaySheetDetails.Any(f => f.Roll_PK == rollpk && f.LaySheetRoll_Pk== layrrollpk && f.IsDeleted=="N"))
                {

                    var q = from layroll in enty.LaySheetRollDetails
                            where layroll.LaySheetRoll_Pk == layrrollpk
                            select layroll;
                    foreach (var element in q)
                    {

                        if (element.Status == "New")
                        {
                         //   enty.LaySheetRollDetails.Remove(element);


                        }
                        else
                        {

                            element.IsUsed = "R";
                        }
                        element.IsDeleted = "Y";
                        Cutn = "Sucess";
                    }
                    enty.SaveChanges();


                }
                else
                {









                    Controls.WebMsgBox.Show("Rolls Used in Laysheet  Delete it first ");
                }
                  
            }

            return Cutn;
        }


        public string DeleteLaysheetRollDetails(int laydetpk)
        {
            string Cutn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var rollpkobj = enty.LaySheetDetails.Where(u => u.LaySheetDet_PK == laydetpk).Select(u => u.Roll_PK).FirstOrDefault();
                int rollpk = int.Parse(rollpkobj.ToString());

                var LaySheetRoll_Pkobj = enty.LaySheetDetails.Where(u => u.LaySheetDet_PK == laydetpk).Select(u => u.LaySheetRoll_Pk).FirstOrDefault();
                int LaySheetRoll_Pk = int.Parse(rollpkobj.ToString());


                Decimal rollyard = 0;

                using (var dbContextTransaction = enty.Database.BeginTransaction())
                {
                    try
                    {
                        var q = from layroll in enty.LaySheetDetails
                                where layroll.LaySheetDet_PK == laydetpk
                                select layroll;
                        foreach (var element in q)
                        {

                            rollyard = Decimal.Parse(element.FabUtilized.ToString()) + Decimal.Parse(element.ExcessOrShort.ToString());
                        }

                        
                       var Q1 = from lyrolldet in enty.LaySheetRollDetails
                                 where lyrolldet.LaySheetRoll_Pk == LaySheetRoll_Pk
                                 select lyrolldet;
                       

                      foreach (var laysheetrolls in Q1)
                        {

                            laysheetrolls.IsUsed = "W";
                            laysheetrolls.BalanceYardage = rollyard;


                        }

                        var q3 = from roll in enty.FabricRollmasters
                                where roll.Roll_PK == rollpk
                                 select roll;
                        foreach (var element in q3)
                        {
                            element.IsCut = "Y";
                        }



                        foreach (var element in q)
                        {

                            enty.LaySheetDetails.Remove(element);
                        }

                        enty.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
                
              

                  

                  

                    
                 
                   


               

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
        public int sequence_no { get; set; }
    }

}

