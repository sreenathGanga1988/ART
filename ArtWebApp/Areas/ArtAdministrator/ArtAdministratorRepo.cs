using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtWebApp.DataModelAtcWorld;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO.Compression;

namespace ArtWebApp.Areas.ArtAdministrator
{
    public static class ArtAdministratorRepo
    {


        public static void UpdateCostperminute()
        {

            using (AtcWorldEntities atcenty = new AtcWorldEntities())
            {
                using (ArtEntitiesnew artenty = new DataModels.ArtEntitiesnew())
                {

                    var q = (from loctation in atcenty.FactoryCostMasters
                             select loctation).ToList();


                    foreach (var element in q)
                    {

                        var artLocation = from loctation in artenty.LocationMasters
                                          where loctation.Location_PK == element.LocationMaster_tbl.ArtLocation_PK
                                          select loctation;

                        foreach (var artloc in artLocation)
                        {


                            artloc.CostPerMinute = element.CostMinute / 100;


                        }




                    }

                    artenty.SaveChanges();

                }


            }

        }



        public static void UpDateLocation()
        {
            DataTable dt = GetLocation();


            using (ArtEntitiesnew artenty = new DataModels.ArtEntitiesnew())
            {




                try
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        int ArtLocation_PK = int.Parse(row["ArtLocation_PK"].ToString());
                        int Location_PK = int.Parse(row["Location_PK"].ToString());

                     
                        var q = from atcdet in artenty.LocationMasters
                                where atcdet.Location_PK == ArtLocation_PK
                                select atcdet;
                        foreach (var element in q)
                        {


                           element.AtcWorldlocation_PK = Location_PK;



                        }

                    }
                }
                catch (Exception ex)
                {

                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }





                artenty.SaveChanges();

            }




        }


        public static void UpDateTTL()
        {
            DataTable dt = GetTTL();


            using (ArtEntitiesnew artenty = new DataModels.ArtEntitiesnew())
            {




                try
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        int ourstyleid = int.Parse(row["OurStyleID"].ToString());

                        Decimal TTLSam = Decimal.Parse(row["TTLSam"].ToString());
                        var q = from atcdet in artenty.AtcDetails
                                where atcdet.OurStyleID == ourstyleid
                                select atcdet;
                        foreach (var element in q)
                        {


                            element.TTLSam = TTLSam;



                        }

                    }
                }
                catch (Exception ex)
                {

                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }





                artenty.SaveChanges();

            }




        }



        public static void UpdateJobcontractTokenya()
        {
            DataTable dt = GetJobContract();

          
           




               
                    foreach (DataRow row in dt.Rows)
                    {
                    using (AtcWorldEntities atcenty = new AtcWorldEntities())
                {
                    try
                    {
                        int locid = int.Parse(row["Location_Pk"].ToString());
                        int OurStyleID = int.Parse(row["OurStyleID"].ToString());
                        var atclocation_pk = atcenty.LocationMaster_tbl.Where(u => u.ArtLocation_PK == locid).Select(u => u.Location_PK).FirstOrDefault();
                        int atcworldlocid= int.Parse(atclocation_pk.ToString()); ;


                        if (!atcenty.ArtJobContractMasters.Any(f => f.OurStyleID == OurStyleID  && f.Location_PK == atcworldlocid))
                        {

                            ArtJobContractMaster ajcmstr = new DataModelAtcWorld.ArtJobContractMaster();
                           
                            ajcmstr.Location_PK = 
                            ajcmstr.OurStyleID = int.Parse(row["OurStyleID"].ToString());
                            ajcmstr.AtcID = int.Parse(row["AtcId"].ToString());
                            ajcmstr.AtcNum = row["AtcNum"].ToString();
                            ajcmstr.OurStyle = row["OurStyle"].ToString();
                            ajcmstr.CM = Decimal.Parse(row["CM"].ToString());



                            ajcmstr.JobContractNum = row["JOBContractNUM"].ToString();
                            atcenty.ArtJobContractMasters.Add(ajcmstr);




                            atcenty.SaveChanges();
                        }
                   
                    }
                    catch (Exception ex)
                    {

                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }

                    }







          
            




        }
        


              public static void UpdateJobcontractOptionalTokenya()
        {
            DataTable dt = GetJobContractOptional();








            foreach (DataRow row in dt.Rows)
            {
                using (AtcWorldEntities atcenty = new AtcWorldEntities())
                {
                    try
                    {
                        int JobContractOptionalDetail_pk = int.Parse(row["JobContractOptionalDetail_pk"].ToString());
                        int JobContractOptional_pk = int.Parse(row["JobContractOptional_pk"].ToString());
                        int Location_Pk = int.Parse(row["Location_Pk"].ToString());

                        if (!atcenty.ArtJobContractOptionalMasters.Any(f => f.JobContractOptional_pk == JobContractOptional_pk && f.JobContractOptionalDetail_pk == JobContractOptionalDetail_pk))
                        {

                            ArtJobContractOptionalMaster ajcmstr = new DataModelAtcWorld.ArtJobContractOptionalMaster();
                            var atclocation_pk = atcenty.LocationMaster_tbl.Where(u => u.ArtLocation_PK == Location_Pk).Select(u => u.Location_PK).FirstOrDefault();
                          
                            ajcmstr.JobContractOptionalDetail_pk = int.Parse(row["JobContractOptionalDetail_pk"].ToString());
                            ajcmstr.JobContractOptional_pk = int.Parse(row["JobContractOptional_pk"].ToString());
                            ajcmstr.OurStyleID = int.Parse(row["OurStyleID"].ToString());
                            ajcmstr.Wash = Decimal.Parse(row["Wash"].ToString());
                            ajcmstr.EmbroidaryPrinting = Decimal.Parse(row["EmbroidaryPrinting"].ToString());                          

                            ajcmstr.CompanyLogistic = Decimal.Parse(row["CompanyLogistic"].ToString());
                            ajcmstr.FactoryLogistic = Decimal.Parse(row["FactoryLogistic"].ToString());
                            ajcmstr.DryProcess = Decimal.Parse(row["DryProcess"].ToString());
                            ajcmstr.FabCommision = Decimal.Parse(row["FabCommision"].ToString());
                            ajcmstr.GarmentComission = Decimal.Parse(row["GarmentComission"].ToString());
                            ajcmstr.Printing = Decimal.Parse(row["Printing"].ToString());
                            ajcmstr.JobContractOptionalNUM = row["JobContractOptionalNUM"].ToString();
                            ajcmstr.Location_Pk = int.Parse(atclocation_pk.ToString());
                            ajcmstr.AtcID = int.Parse(row["AtcID"].ToString());
                            ajcmstr.AddedDate = DateTime.Parse(row["AddedDate"].ToString());
                            ajcmstr.AddedBy = row["AddedBy"].ToString();                         
                            atcenty.ArtJobContractOptionalMasters.Add(ajcmstr);




                            atcenty.SaveChanges();
                        }

                    }
                    catch (Exception ex)
                    {

                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }

            }













        }

        public static  void UpdatePL()
        {
            DataTable dt = GetPL();


            using (ArtEntitiesnew artenty = new DataModels.ArtEntitiesnew())
            {
                //artenty.Configuration.AutoDetectChangesEnabled = false;

                try
                {

                    foreach (DataRow row in dt.Rows)
                {
                    Decimal OurStyleId = Decimal.Parse(row["OurStyleId"].ToString());
                    int Location_PK = int.Parse(row["Location_PK"].ToString());
                    int SewingProducedQty = int.Parse(row["SewingProducedQty"].ToString());
                    DateTime EffDate = DateTime.Parse(row["EffDate"].ToString());
                    Decimal PL = Decimal.Parse(row["P&L"].ToString());



                    if (!artenty.PLAtcworld_tbl.Any(f => f.OurstyleId == OurStyleId && f.EffectiveDate == EffDate && f.Location_pk == Location_PK))
                    {

                        PLAtcworld_tbl pLAtcworld_Tbl = new PLAtcworld_tbl();
                        pLAtcworld_Tbl.factory_pl = PL;
                        pLAtcworld_Tbl.OurstyleId = SewingProducedQty;
                        pLAtcworld_Tbl.SewQty = SewingProducedQty;
                        pLAtcworld_Tbl.Location_pk = Location_PK;
                        pLAtcworld_Tbl.EffectiveDate = EffDate;

                        artenty.PLAtcworld_tbl.Add(pLAtcworld_Tbl);


                    }
                    else
                    {
                        var q = from atcdet in artenty.PLAtcworld_tbl
                                where atcdet.OurstyleId == OurStyleId && atcdet.EffectiveDate == EffDate && atcdet.Location_pk == Location_PK
                                select atcdet;
                        foreach (var element in q)
                        {

                            element.SewQty = SewingProducedQty;

                            element.factory_pl = PL;


                        }
                    }




                    artenty.SaveChanges();




                    

                }


                }
                catch (Exception ex)
                {

                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }




            }


        }

        public static void UpdateUpdateInputlineData()
        {
            DataTable dt = GetInputDate();
            using (ArtEntitiesnew artenty = new ArtEntitiesnew())
            {
                foreach(DataRow row in dt.Rows)
                {
                    try
                    {
                        Decimal Location_PK = Decimal.Parse(row["Location_PK"].ToString());                        
                        int OurStyleId = int.Parse(row["OurStyleId"].ToString());
                        var atclocation_pk = artenty.LocationMasters.Where(u => u.AtcWorldlocation_PK == Location_PK).Select(u => u.Location_PK).FirstOrDefault();

                        if (!artenty.ProductionTNADetails.Any(f => f.ProductionTNACompID == 13 && f.OurStyleID == OurStyleId && f.Location_PK == atclocation_pk && f.IsDeleted == "N"))
                        {


                            ProductionTNADetail productionTNADetail = new ProductionTNADetail();
                            productionTNADetail.Location_PK = atclocation_pk;
                            productionTNADetail.OurStyleID = OurStyleId;
                            productionTNADetail.ProductionTNACompID = 13;
                            productionTNADetail.Actionname = "IsInputDate";
                            //productionTNADetail.MarkedBy = HttpContext.Session["Username"].ToString();
                            productionTNADetail.MarkedDate = DateTime.Parse(row["FirstInput"].ToString());
                            productionTNADetail.IsDeleted = "N";
                            artenty.ProductionTNADetails.Add(productionTNADetail);

                        }
                        artenty.SaveChanges();

                    }
                    catch (Exception exp)
                    {

                    }
                }

            }
        }

        public static void UpdateCSFA()
        {
            DataTable dt = GetCSFA();


            using (ArtEntitiesnew artenty = new DataModels.ArtEntitiesnew())
            {
                //artenty.Configuration.AutoDetectChangesEnabled = false;

             

                    foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        Decimal Location_PK = Decimal.Parse(row["Location_PK"].ToString());
                        int DeptID_Pk = int.Parse(row["DeptID_Pk"].ToString());
                        String DeptName = row["DeptName"].ToString();
                        int OurStyleId = int.Parse(row["OurStyleId"].ToString());
                        Decimal DeptCM = 0;
                        Decimal ProducedGarment = 0;
                        Decimal Revenue = 0;
                        Decimal DeptSAM = 0;
                        Decimal JCM = 0;
                        try
                        {
                            JCM = Decimal.Parse(row["JCM"].ToString()) / 12;
                        }
                        catch (Exception)
                        {
                        }
                        DateTime effDate = new DateTime();
                            
                            try
                        {
                            effDate = DateTime.Parse(row["effDate"].ToString());
                        }
                        catch (Exception)
                        {                           
                        }
                        

                              try
                        {
                            Revenue = Decimal.Parse(row["Revenue"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        { Revenue = Decimal.Parse(row["Revenue"].ToString());
                        }
                        catch (Exception)
                        {                           
                        }
                        try
                        { ProducedGarment = Decimal.Parse(row["ProducedGarment"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            DeptCM = Decimal.Parse(row["DeptCM"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            DeptSAM = Decimal.Parse(row["DeptSAM"].ToString());
                        }
                        catch (Exception)
                        {
                        }


                        
                        var atclocation_pk = artenty.LocationMasters.Where(u => u.AtcWorldlocation_PK == Location_PK).Select(u => u.Location_PK).FirstOrDefault();



                        if (!artenty.OurStyleDeptCMs.Any(f => f.OurStyleID == OurStyleId && f.DeptID == DeptID_Pk && f.AtcWorldLocation_PK == Location_PK && f.effDate== effDate))
                        {

                            OurStyleDeptCM ourStyleDeptCM = new OurStyleDeptCM();
                            ourStyleDeptCM.Location_pk = int.Parse(atclocation_pk.ToString());
                            ourStyleDeptCM.AtcWorldLocation_PK = Location_PK;
                            ourStyleDeptCM.DeptID = DeptID_Pk;
                            ourStyleDeptCM.DeptName = DeptName;
                            ourStyleDeptCM.OurStyleID = OurStyleId;
                            ourStyleDeptCM.DeptCM = DeptCM;
                            ourStyleDeptCM.effDate = effDate;
                            ourStyleDeptCM.ProducedGarment = ProducedGarment;
                            ourStyleDeptCM.Revenue = Revenue;
                            ourStyleDeptCM.DeptSAM = DeptSAM;
                            ourStyleDeptCM.JCM = JCM;
                            artenty.OurStyleDeptCMs.Add(ourStyleDeptCM);
                            artenty.SaveChanges();

                        }
                        else
                        {
                            var q = from atcdet in artenty.OurStyleDeptCMs
                                    where atcdet.OurStyleID == OurStyleId && atcdet.DeptID == DeptID_Pk && atcdet.AtcWorldLocation_PK == Location_PK && atcdet.effDate == effDate
                                    select atcdet;
                            foreach (var element in q)
                            {

                                element.DeptCM = DeptCM;
                                element.ProducedGarment = ProducedGarment;
                                element.Revenue = Revenue;
                                element.DeptSAM = DeptSAM;
                                element.JCM = JCM;
                            }

                            artenty.SaveChanges();
                        }




                        artenty.SaveChanges();

                    }
                catch (Exception ex)
                {
                        int DeptID_Pk = int.Parse(row["DeptID_Pk"].ToString());
                        int OurStyleId = int.Parse(row["OurStyleId"].ToString());
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }




            }


               




            }


        }





        public static void UpdateCSFA( DateTime fromdate,DateTime todate)
        {
            DataTable dt = GetCSFA(fromdate,todate);


            using (ArtEntitiesnew artenty = new DataModels.ArtEntitiesnew())
            {
                //artenty.Configuration.AutoDetectChangesEnabled = false;

              
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        Decimal Location_PK = Decimal.Parse(row["Location_PK"].ToString());
                        int DeptID_Pk = int.Parse(row["DeptID_Pk"].ToString());
                        String DeptName = row["DeptName"].ToString();
                        int OurStyleId = int.Parse(row["OurStyleId"].ToString());
                        Decimal DeptCM = 0;
                        Decimal ProducedGarment = 0;
                        Decimal Revenue = 0;
                        Decimal JCM = 0;
                        Decimal DeptSAM = 0;
                        DateTime effDate = new DateTime();

                        try
                        {
                            effDate = DateTime.Parse(row["effDate"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            JCM = Decimal.Parse(row["JCM"].ToString())/12;
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            Revenue = Decimal.Parse(row["Revenue"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            Revenue = Decimal.Parse(row["Revenue"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            ProducedGarment = Decimal.Parse(row["ProducedGarment"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            DeptCM = Decimal.Parse(row["DeptCM"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            DeptSAM = Decimal.Parse(row["DeptSAM"].ToString());
                        }
                        catch (Exception)
                        {
                        }



                        var atclocation_pk = artenty.LocationMasters.Where(u => u.AtcWorldlocation_PK == Location_PK).Select(u => u.Location_PK).FirstOrDefault();



                        if (!artenty.OurStyleDeptCMs.Any(f => f.OurStyleID == OurStyleId && f.DeptID == DeptID_Pk && f.AtcWorldLocation_PK == Location_PK && f.effDate == effDate))
                        {

                            OurStyleDeptCM ourStyleDeptCM = new OurStyleDeptCM();
                            ourStyleDeptCM.Location_pk = int.Parse(atclocation_pk.ToString());
                            ourStyleDeptCM.AtcWorldLocation_PK = Location_PK;
                            ourStyleDeptCM.DeptID = DeptID_Pk;
                            ourStyleDeptCM.DeptName = DeptName;
                            ourStyleDeptCM.OurStyleID = OurStyleId;
                            ourStyleDeptCM.DeptCM = DeptCM;
                            ourStyleDeptCM.effDate = effDate;
                            ourStyleDeptCM.ProducedGarment = ProducedGarment;
                            ourStyleDeptCM.Revenue = Revenue;
                            ourStyleDeptCM.DeptSAM = DeptSAM;
                            ourStyleDeptCM.JCM = JCM;
                            artenty.OurStyleDeptCMs.Add(ourStyleDeptCM);
                            artenty.SaveChanges();

                        }
                        else
                        {
                            var q = from atcdet in artenty.OurStyleDeptCMs
                                    where atcdet.OurStyleID == OurStyleId && atcdet.DeptID == DeptID_Pk && atcdet.AtcWorldLocation_PK == Location_PK && atcdet.effDate == effDate
                                    select atcdet;
                            foreach (var element in q)
                            {

                                element.DeptCM = DeptCM;
                                element.ProducedGarment = ProducedGarment;
                                element.Revenue = Revenue;
                                element.DeptSAM = DeptSAM;
                                element.JCM = JCM;
                            }

                            artenty.SaveChanges();
                        }




                        artenty.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        int DeptID_Pk = int.Parse(row["DeptID_Pk"].ToString());
                        int OurStyleId = int.Parse(row["OurStyleId"].ToString());
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }




                }







            }


        }










        public static DataTable GetTTL()
        {
            String Qry = @"select isnull( Sum(valueminute),0) AS TTLSAM ,OurStyleID   from ObTarget_tbl GROUP bY OurStyleID";
            return QueryFunctions.ReturnQueryResultDatatablefromAtcWorldkENYA(Qry);

        }
        public static DataTable GetLocation()
        {
            String Qry = @"SELECT        Location_PK, ArtLocation_PK FROM            LocationMaster_tbl";
            return QueryFunctions.ReturnQueryResultDatatablefromAtcWorldkENYA(Qry);

        }
        public static DataTable GetPL()
        {
            String Qry = @"select * from PandLSewprodvsfactoryPL";
            return QueryFunctions.ReturnQueryResultDatatablefromAtcWorldkENYA(Qry);

        }

        public static DataTable GetInputDate()
        {
            String Qry = @"
SELECT        SewInput_Tbl.Location_PK,LocationMaster_tbl.ArtLocation_PK,LocationMaster_tbl.LocationName, CutSummary_tbl.OurStyleId, MIN(cast(SewInput_Tbl.SewinDate as date)) AS FirstInput
FROM          CutSummary_tbl INNER JOIN
CutSummaryDetails_tbl ON CutSummary_tbl.CtrID = CutSummaryDetails_tbl.CtrID INNER JOIN
CutIssue_tbl ON CutSummaryDetails_tbl.Ctrdetailid = CutIssue_tbl.Ctrdetailid INNER JOIN
SewInput_Tbl ON CutIssue_tbl.CtiID = SewInput_Tbl.CtiID
inner Join LocationMaster_tbl on SewInput_Tbl.Location_PK = LocationMaster_tbl.Location_PK 
GROUP BY SewInput_Tbl.Location_PK,LocationMaster_tbl.ArtLocation_PK,LocationMaster_tbl.LocationName, CutSummary_tbl.OurStyleId
";
            return QueryFunctions.ReturnQueryResultDatatablefromAtcWorldkENYA(Qry);

        }
        public static DataTable GetCSFA()
        {
            String Qry = @"
SELECT        Location_PK, DeptID_Pk, DeptName, OurStyleId, DeptSAM, [DeptCM/Cent] / 100 AS DeptCM, SUM(ProducedGarment) AS ProducedGarment, SUM(Revenue$) AS Revenue, effDate,[CM/Doz$]/12 as JCM
FROM            PandLFinal_VW
WHERE        (OurStyleId IS NOT NULL) AND (ProducedGarment > 0)
GROUP BY Location_PK, DeptID_Pk, DeptName, OurStyleId, DeptSAM, [DeptCM/Cent], effDate,[CM/Doz$]/12
HAVING        (effDate > '25 jan 2018')
ORDER BY Location_PK, OurStyleId, DeptID_Pk
";
            return QueryFunctions.ReturnQueryResultDatatablefromAtcWorldkENYA(Qry);

        }


        public static DataTable GetCSFA(DateTime fromdate, DateTime todate)
        {
            String Qry = @"
SELECT        Location_PK, DeptID_Pk, DeptName, OurStyleId, DeptSAM, [DeptCM/Cent] / 100 AS DeptCM, SUM(ProducedGarment) AS ProducedGarment, SUM(Revenue$) AS Revenue, EffDate,[CM/Doz$]/12 as JCM
FROM            PandLFinal_VW
WHERE        (OurStyleId IS NOT NULL) AND (ProducedGarment > 0)
GROUP BY Location_PK, DeptID_Pk, DeptName, OurStyleId, DeptSAM, [DeptCM/Cent], EffDate,[CM/Doz$]/12
HAVING        (EffDate BETWEEN @fromdate AND @todate)
ORDER BY Location_PK, OurStyleId, DeptID_Pk
";
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = Qry;


            sqlCommand.Parameters.AddWithValue("@fromdate", fromdate);
            sqlCommand.Parameters.AddWithValue("@todate", todate);
            return QueryFunctions.ReturnQueryResultDatatablefromAtcWorldkENYA(sqlCommand);

        }

        public static DataTable GetJobContract()
        {
            String Qry = @"SELECT        JobContractMaster.JOBContractNUM, AtcDetails.OurStyle, AtcMaster.AtcNum, MAX(JobContractMaster.CM) AS CM, AtcMaster.AtcId, JobContractMaster.OurStyleID, JobContractMaster.Location_Pk
FROM            JobContractMaster INNER JOIN
                         AtcDetails ON JobContractMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId
GROUP BY JobContractMaster.JOBContractNUM, AtcDetails.OurStyle, AtcMaster.AtcNum, AtcMaster.AtcId, JobContractMaster.OurStyleID, JobContractMaster.Location_Pk";
        

            return QueryFunctions.ReturnQueryResultDatatable(Qry);

        }


        public static DataTable GetJobContractOptional()
        {
            String Qry = @"SELECT        JobContractOptionalDetail.JobContractOptionalDetail_pk, JobContractOptionalDetail.JobContractOptional_pk, JobContractOptionalDetail.OurStyleID, JobContractOptionalDetail.Wash, 
                         JobContractOptionalDetail.EmbroidaryPrinting, JobContractOptionalDetail.CompanyLogistic, JobContractOptionalDetail.FactoryLogistic, JobContractOptionalDetail.DryProcess, JobContractOptionalDetail.FabCommision, 
                         JobContractOptionalDetail.GarmentComission, JobContractOptionalDetail.Printing, JobContractOptionalMaster.JobContractOptionalNUM, JobContractOptionalMaster.Location_Pk, JobContractOptionalMaster.AtcID, 
                         JobContractOptionalMaster.AddedDate, JobContractOptionalMaster.AddedBy
FROM            JobContractOptionalDetail INNER JOIN
                         JobContractOptionalMaster ON JobContractOptionalDetail.JobContractOptional_pk = JobContractOptionalMaster.JobContractOptional_pk
WHERE        (JobContractOptionalDetail.PoPackID = 0)";
            return QueryFunctions.ReturnQueryResultDatatable(Qry);

        }

        public static void UpdateExtraReq()
        {
            fullgarmentRejection();
            PanelRejection();

        }





        public static void fullgarmentRejection()
        {
            using (AtcWorldEntities atcenty = new AtcWorldEntities())
            {
                var q = from lymstr in atcenty.FabricRequest_tbl
                        where lymstr.IsUploaded == false
                        select lymstr;
                foreach (var element in q)
                {

                    using (ArtEntitiesnew entynew = new ArtEntitiesnew())
                    {

                        RejectionExtraFabbReq rejectionExtraFabbReq = new RejectionExtraFabbReq();

                        var atclocation_pk = atcenty.LocationMaster_tbl.Where(u => u.Location_PK == element.Location_PK).Select(u => u.ArtLocation_PK).FirstOrDefault();


                        rejectionExtraFabbReq.Location_PK = int.Parse(atclocation_pk.ToString());
                        rejectionExtraFabbReq.Fabreqid = element.Fabreqid;
                        rejectionExtraFabbReq.Fabreqno = element.Fabreqno;
                        rejectionExtraFabbReq.Reqdate = element.Reqdate;
                        rejectionExtraFabbReq.DepartmentName = element.DepartmentName;
                        rejectionExtraFabbReq.PoPack_Detail_PK = element.PoPack_Detail_PK;
                        rejectionExtraFabbReq.ReqQty = element.ReqQty;
                        rejectionExtraFabbReq.IsApproved = false;
                        rejectionExtraFabbReq.RejectionType = "F";
                        entynew.RejectionExtraFabbReqs.Add(rejectionExtraFabbReq);
                        entynew.SaveChanges();
                    }

                    element.IsUploaded = true;
                }






                atcenty.SaveChanges();

            }



            using (AtcWorldEntities atcenty = new AtcWorldEntities("Ethiopia"))
            {
                var q = from lymstr in atcenty.FabricRequest_tbl
                        where lymstr.IsUploaded == false
                        select lymstr;
                foreach (var element in q)
                {

                    using (ArtEntitiesnew entynew = new ArtEntitiesnew())
                    {

                        RejectionExtraFabbReq rejectionExtraFabbReq = new RejectionExtraFabbReq();

                        var atclocation_pk = atcenty.LocationMaster_tbl.Where(u => u.Location_PK == element.Location_PK).Select(u => u.ArtLocation_PK).FirstOrDefault();


                        rejectionExtraFabbReq.Location_PK = int.Parse(atclocation_pk.ToString());

                        rejectionExtraFabbReq.Fabreqid = element.Fabreqid;
                        rejectionExtraFabbReq.Fabreqno = element.Fabreqno;
                        rejectionExtraFabbReq.Reqdate = element.Reqdate;
                        rejectionExtraFabbReq.DepartmentName = element.DepartmentName;
                        rejectionExtraFabbReq.PoPack_Detail_PK = element.PoPack_Detail_PK;
                        rejectionExtraFabbReq.ReqQty = element.ReqQty;
                        rejectionExtraFabbReq.IsApproved = false;
                        rejectionExtraFabbReq.RejectionType = "F";
                        entynew.RejectionExtraFabbReqs.Add(rejectionExtraFabbReq);
                        entynew.SaveChanges();
                    }

                    element.IsUploaded = true;
                }






                atcenty.SaveChanges();

            }
        }
        public static void PanelRejection()
        {
            using (AtcWorldEntities atcenty = new AtcWorldEntities())
            {
                var q = from lymstr in atcenty.Fabricreqforparts
                        where lymstr.IsUploaded == false
                        select lymstr;
                foreach (var element in q)
                {

                    using (ArtEntitiesnew entynew = new ArtEntitiesnew())
                    {

                        RejectionPanelExtraFabbReq rejectionExtraFabbReq = new RejectionPanelExtraFabbReq();

                        var atclocation_pk = atcenty.LocationMaster_tbl.Where(u => u.Location_PK == element.Location_PK).Select(u => u.ArtLocation_PK).FirstOrDefault();


                        rejectionExtraFabbReq.Location_PK = int.Parse(atclocation_pk.ToString());
                        rejectionExtraFabbReq.Fabreqid = element.FabpartreqID;
                        rejectionExtraFabbReq.Fabreqno = element.FabpartreqNo;
                        rejectionExtraFabbReq.Reqdate = element.Reqdate;
                        rejectionExtraFabbReq.DepartmentName = element.DepartmentName;
                        rejectionExtraFabbReq.PoPack_Detail_PK = element.PoPack_Detail_PK;
                        rejectionExtraFabbReq.ReqQty = element.ReqQty;
                        rejectionExtraFabbReq.IsApproved = false;
                        rejectionExtraFabbReq.RejectionType = "P";
                        entynew.RejectionPanelExtraFabbReqs.Add(rejectionExtraFabbReq);
                        entynew.SaveChanges();
                    }

                    element.IsUploaded = true;
                }






                atcenty.SaveChanges();

            }



            using (AtcWorldEntities atcenty = new AtcWorldEntities("Ethiopia"))
            {
                var q = from lymstr in atcenty.Fabricreqforparts
                        where lymstr.IsUploaded == false
                        select lymstr;
                foreach (var element in q)
                {

                    using (ArtEntitiesnew entynew = new ArtEntitiesnew())
                    {

                        RejectionPanelExtraFabbReq rejectionExtraFabbReq = new RejectionPanelExtraFabbReq();

                        var atclocation_pk = atcenty.LocationMaster_tbl.Where(u => u.Location_PK == element.Location_PK).Select(u => u.ArtLocation_PK).FirstOrDefault();


                        rejectionExtraFabbReq.Location_PK = int.Parse(atclocation_pk.ToString());
                        rejectionExtraFabbReq.Fabreqid = element.FabpartreqID;
                        rejectionExtraFabbReq.Fabreqno = element.FabpartreqNo;
                        rejectionExtraFabbReq.Reqdate = element.Reqdate;
                        rejectionExtraFabbReq.DepartmentName = element.DepartmentName;
                        rejectionExtraFabbReq.PoPack_Detail_PK = element.PoPack_Detail_PK;
                        rejectionExtraFabbReq.ReqQty = element.ReqQty;
                        rejectionExtraFabbReq.IsApproved = false;
                        rejectionExtraFabbReq.RejectionType = "P";
                        entynew.RejectionPanelExtraFabbReqs.Add(rejectionExtraFabbReq);
                        entynew.SaveChanges();
                    }

                    element.IsUploaded = true;
                }






                atcenty.SaveChanges();

            }
        }

















        public static string BackUpDB()
        {
            String ErrorText = "";

            String ConnectionString = (ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString());
            String datestring = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            string destdir = "D:\\backupdb\\" + datestring;
            if (!System.IO.Directory.Exists(destdir))
            {
                System.IO.Directory.CreateDirectory(destdir);
            }


            using (SqlConnection con = new SqlConnection ())
            {
                using(SqlCommand cmd= new SqlCommand())
                {
                    try
                    {
                        String filename = datestring + ".Bak'";
                        cmd.CommandText = "backup database LimsDB to disk = '" + destdir + "\\"+filename+"";
                      

                        con.ConnectionString = ConnectionString;
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ErrorText = destdir;
                     //   ZipBackUP(destdir);
                    }
                    catch (Exception ex)
                    {
                        ErrorText ="Error During backup database!"+ Environment.NewLine+ex.Message;
                    }
                   
                }


            }

            return ErrorText;
        }

        public static string ZipBackUP(String filename)
        {
            String ErrorText = "";



           
            string zipPath = filename +"Comp.zip";//URL for your ZIP file
            ZipFile.CreateFromDirectory(filename, zipPath, CompressionLevel.Fastest, true);
            //string extractPath = @"c:\example\extract";//path to extract
            //ZipFile.ExtractToDirectory(zipPath, extractPath);


            return ErrorText;
        }

        





    }








}