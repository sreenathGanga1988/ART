using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ArtWebApp.DataModelAtcWorld;

namespace ArtWebApp.BLL.ProductionBLL
{
    public class JobContractData
    {
        public JobContractMasterData JCmstrdata { get; set; }
        public List<JobContractDetailData> JobContractDetailDataCollection { get; set; }

        //public String insertJObContract(JobContractData jcdata)
        //{
        //    String Donum = "";
        //    using (ArtEntitiesnew enty = new ArtEntitiesnew())
        //    {
        //        JobContractMaster jcmstr = new JobContractMaster();
        //        jcmstr.AtcID = jcdata.JCmstrdata.AtcID;
        //        jcmstr.AddedDate = DateTime.Now;
        //        jcmstr.AddedBy = jcdata.JCmstrdata.AddedBy;
        //        jcmstr.Location_Pk = jcdata.JCmstrdata.Location_Pk;
        //        jcmstr.Remark = JCmstrdata.remark;
        //        enty.JobContractMasters.Add(jcmstr);


        //        enty.SaveChanges();

        //        Donum = jcmstr.JOBContractNUM = CodeGenerator.GetUniqueCode("JC", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(jcmstr.JobContract_pk.ToString()));




        //        foreach (JobContractDetailData di in jcdata.JobContractDetailDataCollection)
        //        {
        //            //Add the delivery details
        //            JobContractDetail jcdetdata = new JobContractDetail();
        //            jcdetdata.JobContract_pk = jcmstr.JobContract_pk;
        //            jcdetdata.OurStyleID = di.OurStyleID;
        //            jcdetdata.PoPackID = di.PoPackID;
        //            jcdetdata.CMvalue = decimal.Parse ( di.CMvalue.ToString ());
                    
        //            enty.JobContractDetails.Add(jcdetdata);





        //        }
        //        enty.SaveChanges();

        //    }


        //    return Donum;
        //}

        public String insertOtherJObContract(JobContractData jcdata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                JobContractOptionalMaster jcmstr = new JobContractOptionalMaster();
                jcmstr.AtcID = jcdata.JCmstrdata.AtcID;
                jcmstr.AddedDate = DateTime.Now;
                jcmstr.AddedBy = jcdata.JCmstrdata.AddedBy;
                jcmstr.Location_Pk = jcdata.JCmstrdata.Location_Pk;

                enty.JobContractOptionalMasters.Add(jcmstr);


                enty.SaveChanges();

                Donum = jcmstr.JobContractOptionalNUM = CodeGenerator.GetUniqueCode("JCO", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(jcmstr.JobContractOptional_pk.ToString()));




                foreach (JobContractDetailData di in jcdata.JobContractDetailDataCollection)
                {
                    //Add the delivery details
                    JobContractOptionalDetail jcdetdata = new JobContractOptionalDetail();
                    jcdetdata.JobContractOptional_pk = jcmstr.JobContractOptional_pk;
                    jcdetdata.OurStyleID = di.OurStyleID;
                    jcdetdata.PoPackID = di.PoPackID;
                    jcdetdata.Wash = di.Washvalue;
                    jcdetdata.EmbroidaryPrinting = di.Embriodaryvalue;
                    jcdetdata.CompanyLogistic = di.cmblogic;
                    jcdetdata.FactoryLogistic = di.factorylogic;
                    jcdetdata.DryProcess = di.DryProcess;
                    jcdetdata.FabCommision = di.FabComission;
                    jcdetdata.GarmentComission = di.GarCommision;
                    enty.JobContractOptionalDetails.Add(jcdetdata);





                }
                enty.SaveChanges();

            }


            return Donum;
        }

        /// <summary>
        /// insert master data in new jobcontract
        /// </summary>
        /// <param name="jcdata"></param>
        /// <returns></returns>

        public String insertJObContractNewMaster()
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                if (this.JCmstrdata.Location_Pk != 14)
                {

                    using (AtcWorldEntities atcenty = new AtcWorldEntities())
                    {

                        var atclocation_pk = atcenty.LocationMaster_tbl.Where(u => u.ArtLocation_PK == this.JCmstrdata.Location_Pk).Select(u => u.Location_PK).FirstOrDefault();

                        int locid = int.Parse(atclocation_pk.ToString());

                        if (!enty.JobContractMasters.Any(f => f.OurStyleID == this.JCmstrdata.Ourstyleid && f.Location_Pk == this.JCmstrdata.Location_Pk))
                        {

                            JobContractMaster jcmstr = new JobContractMaster();
                            jcmstr.AtcID = this.JCmstrdata.AtcID;
                            jcmstr.AddedDate = DateTime.Now;
                            jcmstr.AddedBy = this.JCmstrdata.AddedBy;
                            jcmstr.Location_Pk = this.JCmstrdata.Location_Pk;
                            jcmstr.Remark = this.JCmstrdata.remark;
                            jcmstr.OurStyleID = this.JCmstrdata.Ourstyleid;
                            jcmstr.CM = this.JCmstrdata.CMCost;
                            jcmstr.IsApproved = "N";
                            enty.JobContractMasters.Add(jcmstr);//
                            enty.SaveChanges();
                            Donum = jcmstr.JOBContractNUM = CodeGenerator.GetUniqueCode("JC", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(jcmstr.JobContract_pk.ToString()));
                            enty.SaveChanges();
                            ArtJobContractMaster ajcmstr = new DataModelAtcWorld.ArtJobContractMaster();

                            ajcmstr.Location_PK = locid;
                            ajcmstr.OurStyleID = this.JCmstrdata.Ourstyleid;
                            ajcmstr.AtcID = this.JCmstrdata.AtcID;
                            ajcmstr.AtcNum = this.JCmstrdata.Atcnum;
                            ajcmstr.OurStyle = this.JCmstrdata.OurStylenum;
                            ajcmstr.CM = this.JCmstrdata.CMCost;
                            ajcmstr.JobContractNum = Donum;
                            atcenty.ArtJobContractMasters.Add(ajcmstr);


                            atcenty.SaveChanges();
                            enty.SaveChanges();
                        }
                        else
                        {

                            var q = from jbmstr in enty.JobContractMasters
                                    where jbmstr.OurStyleID == this.JCmstrdata.Ourstyleid && jbmstr.Location_Pk == this.JCmstrdata.Location_Pk
                                    select jbmstr;
                            foreach (var element in q)
                            {
                                element.AddedDate = DateTime.Now;
                                element.AddedBy = this.JCmstrdata.AddedBy;
                                element.IsApproved = "N";
                                element.Remark = this.JCmstrdata.remark;
                                Donum = element.JOBContractNUM;
                                element.CM = this.JCmstrdata.CMCost;
                                
                            }
                            var q1 = from jbmstr in atcenty.ArtJobContractMasters
                                     where jbmstr.OurStyleID == this.JCmstrdata.Ourstyleid && jbmstr.Location_PK == locid
                                     select jbmstr;
                            foreach (var element in q1)
                            {
                                element.CM = this.JCmstrdata.CMCost;


                            }

                            atcenty.SaveChanges();
                            enty.SaveChanges();

                        }

                        updateJcInAtcWorld(atcenty);


                    }



                }

                else
                {
                    using (AtcWorldEntities atcenty = new AtcWorldEntities("Ethiopia"))
                    {




                        if (!enty.JobContractMasters.Any(f => f.OurStyleID == this.JCmstrdata.Ourstyleid && f.Location_Pk == this.JCmstrdata.Location_Pk))
                        {

                            JobContractMaster jcmstr = new JobContractMaster();
                            jcmstr.AtcID = this.JCmstrdata.AtcID;
                            jcmstr.AddedDate = DateTime.Now;
                            jcmstr.AddedBy = this.JCmstrdata.AddedBy;
                            jcmstr.Location_Pk = this.JCmstrdata.Location_Pk;
                            jcmstr.Remark = this.JCmstrdata.remark;
                            jcmstr.OurStyleID = this.JCmstrdata.Ourstyleid;
                            jcmstr.CM = this.JCmstrdata.CMCost;
                            jcmstr.IsApproved = "N";
                            enty.JobContractMasters.Add(jcmstr);


                            enty.SaveChanges();

                            Donum = jcmstr.JOBContractNUM = CodeGenerator.GetUniqueCode("JC", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(jcmstr.JobContract_pk.ToString()));
                

                            ArtJobContractMaster ajcmstr = new DataModelAtcWorld.ArtJobContractMaster();
                            ajcmstr.Location_PK = this.JCmstrdata.Location_Pk;
                            ajcmstr.OurStyleID = this.JCmstrdata.Ourstyleid;
                            ajcmstr.CM = this.JCmstrdata.CMCost;
                            ajcmstr.JobContractNum = Donum;
                            atcenty.ArtJobContractMasters.Add(ajcmstr);
                            atcenty.SaveChanges();
                            enty.SaveChanges();
                        }
                        else
                        {

                            var q = from jbmstr in enty.JobContractMasters
                                    where jbmstr.OurStyleID == this.JCmstrdata.Ourstyleid && jbmstr.Location_Pk == this.JCmstrdata.Location_Pk
                                    select jbmstr;
                            foreach (var element in q)
                            {


                                element.AddedDate = DateTime.Now;
                                element.AddedBy = this.JCmstrdata.AddedBy;

                                element.Remark = this.JCmstrdata.remark;
                                Donum = element.JOBContractNUM;
                                element.CM = this.JCmstrdata.CMCost;



                            }


                            var q1 = from jbmstr in atcenty.ArtJobContractMasters
                                     where jbmstr.OurStyleID == this.JCmstrdata.Ourstyleid && jbmstr.Location_PK == this.JCmstrdata.Location_Pk
                                     select jbmstr;
                            foreach (var element in q1)
                            {



                                element.CM = this.JCmstrdata.CMCost;



                            }



                          

                            atcenty.SaveChanges();
                            enty.SaveChanges();

                        }


                        updateJcInAtcWorld(atcenty);


                    }


                }







            }


            return Donum;
        }




        public void updateJcInAtcWorld(AtcWorldEntities atcenty)
        {
            var atclocation_pk = atcenty.LocationMaster_tbl.Where(u => u.ArtLocation_PK == this.JCmstrdata.Location_Pk).Select(u => u.Location_PK).FirstOrDefault();
            int atcworldlctn = int.Parse(atclocation_pk.ToString());



            if (!atcenty.CmDozmasters.Any(f => f.OurStyleId == this.JCmstrdata.Ourstyleid && f.Location_pk == atcworldlctn))
            {
                CmDozmaster cmDozmaster = new CmDozmaster();
                cmDozmaster.CmDoz = this.JCmstrdata.CMCost * 12;
                cmDozmaster.OurStyleId = this.JCmstrdata.Ourstyleid;
                cmDozmaster.Location_pk = atcworldlctn;
                cmDozmaster.User_PK = 0;
                cmDozmaster.AddedDate = DateTime.Now;
                cmDozmaster.HostName = this.JCmstrdata.JOBContractNUM;
                atcenty.CmDozmasters.Add(cmDozmaster);
            }
            else
            {
                var q = from comdoc in atcenty.CmDozmasters
                        where comdoc.OurStyleId == this.JCmstrdata.Ourstyleid && comdoc.Location_pk == atcworldlctn
                        select comdoc;
                foreach (var element in q)
                {

                    element.CmDoz = this.JCmstrdata.CMCost * 12;
                    element.User_PK = 0;
                    element.AddedDate = DateTime.Now;
                    element.HostName = this.JCmstrdata.JOBContractNUM;
                }


            }
        }



        public String insertOtherJObContractDetailsNew(JobContractData jcdata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                if (!enty.JobContractOptionalMasters.Any(f => f.OurStyleID == this.JCmstrdata.Ourstyleid && f.Location_Pk == this.JCmstrdata.Location_Pk))
                {
                    JobContractOptionalMaster jcmstr = new JobContractOptionalMaster();
                    jcmstr.AtcID = jcdata.JCmstrdata.AtcID;
                    jcmstr.AddedDate = DateTime.Now;
                    jcmstr.AddedBy = jcdata.JCmstrdata.AddedBy;
                    jcmstr.Location_Pk = jcdata.JCmstrdata.Location_Pk;
                    jcmstr.OurStyleID = jcdata.JCmstrdata.OurStyleID;
                    enty.JobContractOptionalMasters.Add(jcmstr);


                    enty.SaveChanges();

                    Donum = jcmstr.JobContractOptionalNUM = CodeGenerator.GetUniqueCode("JCO", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(jcmstr.JobContractOptional_pk.ToString()));
                    foreach (JobContractDetailData di in jcdata.JobContractDetailDataCollection)
                    {
                        //Add the delivery details
                        JobContractOptionalDetail jcdetdata = new JobContractOptionalDetail();
                        jcdetdata.JobContractOptional_pk = jcmstr.JobContractOptional_pk;
                        jcdetdata.OurStyleID = di.OurStyleID;
                        jcdetdata.PoPackID = 0;
                        jcdetdata.Wash = di.Washvalue;
                        jcdetdata.EmbroidaryPrinting = di.Embriodaryvalue;
                        jcdetdata.CompanyLogistic = di.cmblogic;
                        jcdetdata.FactoryLogistic = di.factorylogic;
                        jcdetdata.DryProcess = di.DryProcess;
                        jcdetdata.FabCommision = di.FabComission;
                        jcdetdata.GarmentComission = di.GarCommision;
                        jcdetdata.Printing = di.printing;
                        enty.JobContractOptionalDetails.Add(jcdetdata);
                        
                    }

                }
                else
                {
                    foreach (JobContractDetailData di in jcdata.JobContractDetailDataCollection)
                    {
                        var q = from jbmstr in enty.JobContractOptionalDetails
                                where jbmstr.OurStyleID == this.JCmstrdata.Ourstyleid && jbmstr.JobContractOptionalMaster.Location_Pk == this.JCmstrdata.Location_Pk
                                select jbmstr;
                        foreach (var element in q)
                        {

                            
                            element.Wash = di.Washvalue;
                            element.EmbroidaryPrinting = di.Embriodaryvalue;
                            element.CompanyLogistic = di.cmblogic;
                            element.FactoryLogistic = di.factorylogic;
                            element.DryProcess = di.DryProcess;
                            element.FabCommision = di.FabComission;
                            element.GarmentComission = di.GarCommision;
                            element.Printing = di.printing;




                        }

                    }







                }


                


               
                enty.SaveChanges();

            }


            return Donum;
        }






    }



    public class JobContractMasterData
    {
        public int JobContract_pk { get; set; }
        public string JOBContractNUM { get; set; }
        public string OurStylenum { get; set; }
        public int Location_Pk { get; set; }
        public int AtcID { get; set; }
        public int OurStyleID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        public string remark { get; set; }
        public int Ourstyleid { get; set; }
        public string Atcnum { get; set; }
        public Decimal CMCost { get; set; }


    }



    public class JobContractDetailData
    {
        public int JobContractDetail_pk { get; set; }
        public int JobContract_pk { get; set; }
        public int PoPackID { get; set; }
        public int OurStyleID { get; set; }
        public float CMvalue { get; set; }



        public Decimal Washvalue { get; set; }
        public Decimal Embriodaryvalue { get; set; }
        public Decimal factorylogic { get; set; }
        public Decimal cmblogic { get; set; }

        public Decimal printing { get; set; }


        public Decimal DryProcess { get; set; }
        public Decimal FabComission { get; set; }
        public Decimal GarCommision { get; set; }





        public DataTable GetJobContractdetailofList(ArrayList jobdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";

            for (int i = 0; i < jobdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + "Where  JobContractDetail.JobContract_pk=" + jobdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or JobContractDetail.JobContract_pk=" + jobdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                String query = @"Select tt.JobContractDetail_pk,tt.JOBContractNUM,tt.POPackNUm,tt.OurStyle,tt.AtcNum,tt.POqty,tt.ShippedQty,(tt.POqty-tt.ShippedQty) as BalQty  From (

SELECT        JobContractDetail.JobContractDetail_pk, JobContractMaster.JOBContractNUM, PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS POPackNUm, AtcDetails.OurStyle + ' / ' + AtcDetails.BuyerStyle AS OurStyle , AtcMaster.AtcNum, 
                         SUM(POPackDetails.PoQty) AS POqty,isnull((SELECT        SUM(ShippedQty) 
FROM            ShipmentHandOverDetails
GROUP BY JobContractDetail_pk
HAVING        (JobContractDetail_pk = JobContractDetail.JobContractDetail_pk)),0) as ShippedQty
FROM            JobContractDetail INNER JOIN
                         JobContractMaster ON JobContractDetail.JobContract_pk = JobContractMaster.JobContract_pk INNER JOIN
                         POPackDetails ON JobContractDetail.PoPackID = POPackDetails.POPackId AND JobContractDetail.OurStyleID = POPackDetails.OurStyleID INNER JOIN
                         PoPackMaster ON POPackDetails.POPackId = PoPackMaster.PoPackId INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId " + condition + " GROUP BY JobContractDetail.JobContractDetail_pk, JobContractMaster.JOBContractNUM, PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO, AtcDetails.OurStyle + ' / ' + AtcDetails.BuyerStyle , AtcMaster.AtcNum)tt";
                DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
                dt = pktrans.getPodetails(query);
            }
            return dt;

        }


        public DataTable GetJobContractdetailofAtcandLocation(int ourstyleid ,int location_pk)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd= new SqlCommand())
            {

                cmd.CommandText = @"SELECT        ApprovedCosting.OurStyleID, ApprovedCosting.OurStyle, ApprovedCosting.AtcNum, ApprovedCosting.WASH, ApprovedCosting.[DRY PROCESS], ApprovedCosting.[COMPANY LOGISTICS], 
                         ApprovedCosting.[FACTORY LOGISTICS], ApprovedCosting.EMBROIDERY, ApprovedCosting.PRINTING, AlreadyEntereded.EnteredWash, AlreadyEntereded.EnteredEmbroidary, 
                         AlreadyEntereded.EnteredCompanyLogistic, AlreadyEntereded.EnteredFactoryLogistic, AlreadyEntereded.EnteredDryProcess, AlreadyEntereded.EnteredFabCommision, 
                         AlreadyEntereded.EnteredGarmentComission, AlreadyEntereded.EnteredPrinting
FROM(SELECT        AtcDetails.OurStyleID, AtcDetails.OurStyle, AtcMaster.AtcNum, ISNULL
                                                        ((SELECT        TOP(1) ISNULL(StyleCostingComponentDetails.CompValue, 0) AS Expr1
                                                            FROM            StyleCostingMaster INNER JOIN
                                                                                     StyleCostingComponentDetails ON StyleCostingMaster.Costing_PK = StyleCostingComponentDetails.Costing_PK INNER JOIN
                                                                                     CostingComponentMaster ON StyleCostingComponentDetails.CostComp_PK = CostingComponentMaster.CostComp_PK
                                                            WHERE(CostingComponentMaster.ComponentName = N'WASH') AND(StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID) AND(StyleCostingMaster.IsApproved = N'A')), 0) AS WASH,
                                          ISNULL

                                              ((SELECT        TOP(1) ISNULL(StyleCostingComponentDetails_2.CompValue, 0) AS Expr1

                                                  FROM            StyleCostingMaster AS StyleCostingMaster_2 INNER JOIN

                                                                           StyleCostingComponentDetails AS StyleCostingComponentDetails_2 ON StyleCostingMaster_2.Costing_PK = StyleCostingComponentDetails_2.Costing_PK INNER JOIN

                                                                           CostingComponentMaster AS CostingComponentMaster_2 ON StyleCostingComponentDetails_2.CostComp_PK = CostingComponentMaster_2.CostComp_PK

                                                  WHERE(CostingComponentMaster_2.ComponentName = N'DRY PROCESS') AND(StyleCostingMaster_2.OurStyleID = AtcDetails.OurStyleID) AND(StyleCostingMaster_2.IsApproved = N'A')), 0) 
                                                    AS[DRY PROCESS], ISNULL
                                                       ((SELECT        TOP(1) ISNULL(StyleCostingComponentDetails_1.CompValue, 0) AS Expr1

                                                           FROM            StyleCostingMaster AS StyleCostingMaster_1 INNER JOIN

                                                                                    StyleCostingComponentDetails AS StyleCostingComponentDetails_1 ON StyleCostingMaster_1.Costing_PK = StyleCostingComponentDetails_1.Costing_PK INNER JOIN

                                                                                    CostingComponentMaster AS CostingComponentMaster_1 ON StyleCostingComponentDetails_1.CostComp_PK = CostingComponentMaster_1.CostComp_PK

                                                           WHERE(CostingComponentMaster_1.ComponentName = N'COMPANY LOGISTICS') AND(StyleCostingMaster_1.OurStyleID = AtcDetails.OurStyleID) AND
                                                                                    (StyleCostingMaster_1.IsApproved = N'A')), 0) AS[COMPANY LOGISTICS], ISNULL
                                                      ((SELECT        TOP(1) ISNULL(StyleCostingComponentDetails_1.CompValue, 0) AS Expr1

                                                          FROM            StyleCostingMaster AS StyleCostingMaster_1 INNER JOIN

                                                                                   StyleCostingComponentDetails AS StyleCostingComponentDetails_1 ON StyleCostingMaster_1.Costing_PK = StyleCostingComponentDetails_1.Costing_PK INNER JOIN

                                                                                   CostingComponentMaster AS CostingComponentMaster_1 ON StyleCostingComponentDetails_1.CostComp_PK = CostingComponentMaster_1.CostComp_PK

                                                          WHERE(CostingComponentMaster_1.ComponentName = N'FACTORY LOGISTICS') AND(StyleCostingMaster_1.OurStyleID = AtcDetails.OurStyleID) AND
                                                                                   (StyleCostingMaster_1.IsApproved = N'A')), 0) AS[FACTORY LOGISTICS], ISNULL
                                                     ((SELECT        TOP(1) ISNULL(StyleCostingComponentDetails_1.CompValue, 0) AS Expr1

                                                         FROM            StyleCostingMaster AS StyleCostingMaster_1 INNER JOIN

                                                                                  StyleCostingComponentDetails AS StyleCostingComponentDetails_1 ON StyleCostingMaster_1.Costing_PK = StyleCostingComponentDetails_1.Costing_PK INNER JOIN

                                                                                  CostingComponentMaster AS CostingComponentMaster_1 ON StyleCostingComponentDetails_1.CostComp_PK = CostingComponentMaster_1.CostComp_PK

                                                         WHERE(CostingComponentMaster_1.ComponentName = N'EMBROIDERY') AND(StyleCostingMaster_1.OurStyleID = AtcDetails.OurStyleID) AND(StyleCostingMaster_1.IsApproved = N'A')), 0) 
                                                    AS EMBROIDERY, ISNULL
                                                        ((SELECT        TOP(1) ISNULL(StyleCostingComponentDetails_1.CompValue, 0) AS Expr1
                                                            FROM            StyleCostingMaster AS StyleCostingMaster_1 INNER JOIN
                                                                                     StyleCostingComponentDetails AS StyleCostingComponentDetails_1 ON StyleCostingMaster_1.Costing_PK = StyleCostingComponentDetails_1.Costing_PK INNER JOIN
                                                                                     CostingComponentMaster AS CostingComponentMaster_1 ON StyleCostingComponentDetails_1.CostComp_PK = CostingComponentMaster_1.CostComp_PK
                                                            WHERE(CostingComponentMaster_1.ComponentName = N'PRINTING') AND(StyleCostingMaster_1.OurStyleID = AtcDetails.OurStyleID) AND(StyleCostingMaster_1.IsApproved = N'A')), 0) 
                                                    AS PRINTING
                          FROM AtcMaster INNER JOIN
                                                    AtcDetails ON AtcMaster.AtcId = AtcDetails.AtcId
                          WHERE(AtcDetails.OurStyleID = @ourstyleid)) AS ApprovedCosting LEFT OUTER JOIN
                     (SELECT        EnteredWash, EnteredEmbroidary, EnteredCompanyLogistic, EnteredFactoryLogistic, EnteredDryProcess, EnteredFabCommision, EnteredGarmentComission, EnteredPrinting, Location_Pk,
                                                 OurStyleID

                       FROM(SELECT        ISNULL(AVG(JobContractOptionalDetail.Wash), 0) AS EnteredWash, ISNULL(AVG(JobContractOptionalDetail.EmbroidaryPrinting), 0) AS EnteredEmbroidary,
                                                                           ISNULL(AVG(JobContractOptionalDetail.CompanyLogistic), 0) AS EnteredCompanyLogistic, ISNULL(AVG(JobContractOptionalDetail.FactoryLogistic), 0) AS EnteredFactoryLogistic,
                                                                           AVG(JobContractOptionalDetail.DryProcess) AS EnteredDryProcess, ISNULL(AVG(JobContractOptionalDetail.FabCommision), 0) AS EnteredFabCommision,
                                                                           ISNULL(AVG(JobContractOptionalDetail.GarmentComission), 0) AS EnteredGarmentComission, ISNULL(AVG(JobContractOptionalDetail.Printing), 0) AS EnteredPrinting,
                                                                           JobContractOptionalMaster.Location_Pk, JobContractOptionalMaster.OurStyleID

                                                 FROM            JobContractOptionalDetail INNER JOIN

                                                                           JobContractOptionalMaster ON JobContractOptionalDetail.JobContractOptional_pk = JobContractOptionalMaster.JobContractOptional_pk

                                                 GROUP BY JobContractOptionalMaster.Location_Pk, JobContractOptionalMaster.OurStyleID

                                                 HAVING(JobContractOptionalMaster.Location_Pk = @location_pk) AND(JobContractOptionalMaster.OurStyleID = @ourstyleid)) AS AlreadyEntered) AS AlreadyEntereded ON
                         ApprovedCosting.OurStyleID = AlreadyEntereded.OurStyleID";


                cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);

                cmd.Parameters.AddWithValue("@location_pk", location_pk);
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
            }

                return dt;

        }

    }


    public class ShipmentHandOverMasterData
    {
        public int LocationPK_pk { get; set; }
        public List<ShipmentHandOverData> ShipmentHandOverMasterDataCollection { get; set; }


        public String insertShipmentHandOver(ShipmentHandOverMasterData shpmstrdata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                ShipmentHandOverMaster shpmstr = new ShipmentHandOverMaster();
                shpmstr.Location_Pk = shpmstrdata.LocationPK_pk;
                shpmstr.IsCompleted = "N";
                enty.ShipmentHandOverMasters.Add(shpmstr);


               enty.SaveChanges();

                Donum = shpmstr.ShipmentHandOverCode = CodeGenerator.GetUniqueCode("SH", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(shpmstr.ShipmentHandMaster_PK.ToString()));




                foreach (ShipmentHandOverData di in shpmstrdata.ShipmentHandOverMasterDataCollection)
                {

                    var ourstyleid = enty.JobContractDetails.Where(u => u.JobContractDetail_pk == di.JobContractDetail_pk).Select(u => u.OurStyleID).FirstOrDefault();
                    var popackid = enty.JobContractDetails.Where(u => u.JobContractDetail_pk == di.JobContractDetail_pk).Select(u => u.PoPackID).FirstOrDefault();

                    //Add the delivery details
                    ShipmentHandOverDetail shpdert = new ShipmentHandOverDetail();
                    shpdert.ShipmentHandMaster_PK = shpmstr.ShipmentHandMaster_PK;
                    shpdert.JobContractDetail_pk = di.JobContractDetail_pk;
                    shpdert.ShippedQty = di.ShippedQty;
                    shpdert.ShipmentHandOverDate = di.ShipmenthandOverdate;
                    shpdert.AddedBy = di.AddedBy;
                    shpdert.AddedDate = di.AddedDate;
                    shpdert.POPackId = int.Parse(popackid.ToString());
                    shpdert.OurStyleID = int.Parse(ourstyleid.ToString());
                    enty.ShipmentHandOverDetails.Add(shpdert);





                }
                enty.SaveChanges();

            }


            return Donum;
        }

        public String insertShipmentHandOverWithSDO(ShipmentHandOverMasterData shpmstrdata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                ShipmentHandOverMaster shpmstr = new ShipmentHandOverMaster();
                shpmstr.Location_Pk = shpmstrdata.LocationPK_pk;
                shpmstr.IsCompleted = "N";
                enty.ShipmentHandOverMasters.Add(shpmstr);


                enty.SaveChanges();

                Donum = shpmstr.ShipmentHandOverCode = CodeGenerator.GetUniqueCode("SH", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(shpmstr.ShipmentHandMaster_PK.ToString()));




                foreach (ShipmentHandOverData di in shpmstrdata.ShipmentHandOverMasterDataCollection)
                {

                   
                    //Add the delivery details
                    ShipmentHandOverDetail shpdert = new ShipmentHandOverDetail();
                    shpdert.ShipmentHandMaster_PK = shpmstr.ShipmentHandMaster_PK;
                     shpdert.SDONum = di.SDO;
                    shpdert.ShippedQty = di.ShippedQty;
                    shpdert.ShipmentHandOverDate = di.ShipmenthandOverdate;
                    shpdert.AddedBy = di.AddedBy;
                    shpdert.AddedDate = di.AddedDate;
                    shpdert.POPackId = int.Parse(di.Popackid.ToString());
                    shpdert.OurStyleID = int.Parse(di.OurStyleId.ToString());
                    shpdert.ProducedLctn_PK = int.Parse(di.ProducedLctn_PK.ToString());
                    shpdert.SDODate = di.ShipmentDate;
                    shpdert.CombinationCode = shpdert.POPackId.ToString().Trim() + "/" + shpdert.OurStyleID.ToString().Trim();
                    shpdert.FCM = di.Cmperpc;
                    enty.ShipmentHandOverDetails.Add(shpdert);
                 
                    enty.SaveChanges();



                    ProductionReportDetail prrdetdet = new ProductionReportDetail();
                 
                    prrdetdet.CutQty = di.ShippedQty;
                    prrdetdet.SewnQty = di.ShippedQty;
                    prrdetdet.WashedQty = di.ShippedQty;
                    prrdetdet.PackedQty = di.ShippedQty;
                    prrdetdet.ShippedQty = di.ShippedQty;
                    prrdetdet.AddedBy = di.AddedBy;
                    prrdetdet.AddedDate = di.AddedDate;
                    prrdetdet.POPackId = int.Parse(di.Popackid.ToString());
                    prrdetdet.OurStyleID = int.Parse(di.OurStyleId.ToString());
                    prrdetdet.ProducedLctn_PK = int.Parse(di.ProducedLctn_PK.ToString());
                    prrdetdet.SDODate = di.ShipmentDate;
                    prrdetdet.SDONum = di.SDO;
                    prrdetdet.Location_PK= shpmstrdata.LocationPK_pk;
                    enty.ProductionReportDetails.Add(prrdetdet);










                    var q = from atcshp in enty.ATCWorldToArtShipDatas
                            where atcshp.ArtLocation_PK == shpmstrdata.LocationPK_pk && atcshp.POPackID == di.Popackid && atcshp.OurStyleId == di.OurStyleId
                            && atcshp.SDONo == di.SDO 
                            select atcshp;
                    foreach(var element in q)
                    {
                        element.IsBooked = "Y";
                        element.BookedBy = di.AddedBy;
                        element.BookedDate = di.AddedDate;


                        ShipmentHandoverColorSizeDetail shpclrsizedet = new DataModels.ShipmentHandoverColorSizeDetail();

                        shpclrsizedet.ShipmentHandMaster_PK = shpmstr.ShipmentHandMaster_PK;
                        shpclrsizedet.PoPack_Detail_PK = element.PoPack_Detail_PK;
                        shpclrsizedet.ShipmentHandOver_PK = shpdert.ShipmentHandOver_PK;
                        shpclrsizedet.Qty = element.ShipQty;
                        enty.ShipmentHandoverColorSizeDetails.Add(shpclrsizedet);

                    }

                }
                enty.SaveChanges();

            }


            return Donum;
        }

        public void markhipmentHandover(int shipment_PK)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from shpmnt in enty.ShipmentHandOverMasters
                        where shpmnt.ShipmentHandMaster_PK == shipment_PK
                        select shpmnt;

                foreach (var elemt in q)
                {
                    elemt.IsCompleted = "Y";
                }
                enty.SaveChanges();
            }
        }

       

        public DataTable GetIncompletedShipmenthandover(int locpk)
        {
          return  ArtWebApp.DBTransaction.ShippingTransaction.ShippingTransaction.GetIncompletedShipmenthandover(locpk);
        }


        public DataTable GetSDOData(String Condition,int location_pk)
        {
            return ArtWebApp.DBTransaction.ShippingTransaction.ShippingTransaction.GetSDODataFromAtcWorld(Condition,location_pk);
        }
    }



    public class ShipmentHandOverData
        {
            public int ShipmentHandOver_PK { get; set; }

            public int JobContractDetail_pk { get; set; }
        public int Popackid { get; set; }
        public int OurStyleId { get; set; }
        public int ProducedLctn_PK { get; set; }
        public Decimal Cmperpc { get; set; }
        public DateTime ShipmentDate { get; set; }
        public int ShippedQty { get; set; }
            public DateTime AddedDate { get; set; }
            public string AddedBy { get; set; }
        
        public string SDO { get; set; }

        public DateTime ShipmenthandOverdate { get; set; }



            public DataTable GetDataForShipmentData(ArrayList shpdetlist)
            {
                DataTable dt = new DataTable();
                string condition = "";

                for (int i = 0; i < shpdetlist.Count; i++)
                {
                    if (i == 0)
                    {
                        condition = condition + " Where  ShipmentHandOverMaster.ShipmentHandMaster_PK=" + shpdetlist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + "  or ShipmentHandOverMaster.ShipmentHandMaster_PK=" + shpdetlist[i].ToString().Trim();
                    }



                }

                if (condition != "where")
                {
                    String query = @"Select  tt.PoPackID, tt.OurStyleID, tt.ShipmentHandOver_PK ,tt.ShipmentHandOverCode, tt.OurStyle, tt.AtcNum, tt.ShippedQty, tt.InvoicedQty, (tt.ShippedQty-tt.InvoicedQty) as BalToInvoice,
                         tt.POPackNUm, tt.FOB
						 
						  from (

SELECT        ShipmentHandOverMaster.ShipmentHandOverCode, AtcDetails.OurStyle, AtcMaster.AtcNum, SUM(ShipmentHandOverDetails.ShippedQty) AS ShippedQty, 
                         PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS POPackNUm, AtcDetails.FOB, ShipmentHandOverDetails.POPackId, ShipmentHandOverDetails.OurStyleID, 
                         ShipmentHandOverDetails.ShipmentHandOver_PK, ISNULL
                             ((SELECT        SUM(InvoiceQty) AS Expr1
                                 FROM            InvoiceDetail
                                 WHERE        (PoPackID = ShipmentHandOverDetails.POPackId) AND (OurStyleID = ShipmentHandOverDetails.OurStyleID) AND (ShipmentHandOver_PK = ShipmentHandOverDetails.ShipmentHandOver_PK)), 0) 
                         AS InvoicedQty
FROM            AtcMaster INNER JOIN
                         AtcDetails ON AtcMaster.AtcId = AtcDetails.AtcId INNER JOIN
                         ShipmentHandOverMaster INNER JOIN
                         ShipmentHandOverDetails ON ShipmentHandOverMaster.ShipmentHandMaster_PK = ShipmentHandOverDetails.ShipmentHandMaster_PK INNER JOIN
                         PoPackMaster ON ShipmentHandOverDetails.POPackId = PoPackMaster.PoPackId ON AtcDetails.OurStyleID = ShipmentHandOverDetails.OurStyleID " + condition + @" 
                        GROUP BY ShipmentHandOverMaster.ShipmentHandOverCode, AtcDetails.OurStyle, AtcMaster.AtcNum, PoPackMaster.BuyerPO, PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO, AtcDetails.FOB, 
                         ShipmentHandOverDetails.POPackId, ShipmentHandOverDetails.OurStyleID, ShipmentHandOverDetails.ShipmentHandOver_PK, ShipmentHandOverMaster.ShipmentHandMaster_PK) tt";
                    DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
                    dt = pktrans.getPodetails(query);
                }
                return dt;

            }



        }



        public class ProductionReportData
        {


            public List<ProductionReportDetailsData> ProductionReportDetailsDataCollection { get; set; }



            public DataTable GetDataForproductionreport(ArrayList jobdetlist)
            {
                DataTable dt = new DataTable();
                string condition = "";

                for (int i = 0; i < jobdetlist.Count; i++)
                {
                    if (i == 0)
                    {
                        condition = condition + " Where  JobContractDetail.JobContract_pk=" + jobdetlist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + "  or JobContractDetail.JobContract_pk=" + jobdetlist[i].ToString().Trim();
                    }



                }

                if (condition != "where")
                {
                    String query = @"select ttt.JobContractDetail_pk,ttt.JOBContractNUM,ttt.POPackNUm,ttt.OurStyle,ttt.AtcNum,isnull(ttt.POqty,0 ) as POqty,isnull(ttt.ShippedQty,0) as ShippedQty ,
isnull(ttt.BalQty,0 ) as BalQty,isnull(yyyy.SewnQty,0) as SewnQty,isnull(yyyy.WashedQty,0) as WashedQty,isnull(yyyy.PackedQty,0) as PackedQty,isnull(yyyy.CutQty,0) as CutQty ,

(isnull(ttt.POqty,0 )-isnull(yyyy.SewnQty,0)) as BalSewnQty,(isnull(ttt.POqty,0 )-isnull(yyyy.WashedQty,0)) as BalWashedQty,(isnull(ttt.POqty,0 )-isnull(yyyy.PackedQty,0)) as BalPackedQty,(isnull(ttt.POqty,0 )-isnull(yyyy.CutQty,0)) as BalCutQty
from(

Select tt.JobContractDetail_pk,tt.JOBContractNUM,tt.POPackNUm,tt.OurStyle,tt.AtcNum,tt.POqty,tt.ShippedQty,(tt.POqty-tt.ShippedQty) as BalQty From (

SELECT        JobContractDetail.JobContractDetail_pk, JobContractMaster.JOBContractNUM, PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS POPackNUm, AtcDetails.OurStyle, AtcMaster.AtcNum, 
                         SUM(POPackDetails.PoQty) AS POqty,isnull((SELECT        SUM(ShippedQty) 
FROM            ShipmentHandOverDetails
GROUP BY JobContractDetail_pk
HAVING        (JobContractDetail_pk = JobContractDetail.JobContractDetail_pk)),0) as ShippedQty
FROM            JobContractDetail INNER JOIN
                         JobContractMaster ON JobContractDetail.JobContract_pk = JobContractMaster.JobContract_pk INNER JOIN
                         POPackDetails ON JobContractDetail.PoPackID = POPackDetails.POPackId AND JobContractDetail.OurStyleID = POPackDetails.OurStyleID INNER JOIN
                         PoPackMaster ON POPackDetails.POPackId = PoPackMaster.PoPackId INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId" + condition + " GROUP BY JobContractDetail.JobContractDetail_pk, JobContractMaster.JOBContractNUM, PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcMaster.AtcNum)tt )ttt   LEFT  join (SELECT        SUM(CutQty) AS CutQty, SUM(SewnQty) AS SewnQty, SUM(WashedQty) AS WashedQty, SUM(PackedQty) AS PackedQty, SUM(ShippedQty) AS ShippedQty,JobContractDetail_pk FROM ProductionReportDetails GROUP BY JobContractDetail_pk)yyyy on ttt.JobContractDetail_pk=yyyy.JobContractDetail_pk";
                    DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
                    dt = pktrans.getPodetails(query);
                }
                return dt;

            }

            public String insertProductionbReport(ProductionReportData PRDdata)
            {
                String Donum = "";
                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                {

                    foreach (ProductionReportDetailsData di in PRDdata.ProductionReportDetailsDataCollection)
                    {
                        //Add the delivery details
                        ProductionReportDetail prrdetdet = new ProductionReportDetail();
                        prrdetdet.JobContractDetail_pk = di.JobContractDetail_pk;
                        prrdetdet.CutQty = di.CutQty;
                        prrdetdet.SewnQty = di.SewnQty;
                        prrdetdet.WashedQty = di.WashedQty;
                        prrdetdet.PackedQty = di.PackedQty;
                        prrdetdet.ShippedQty = di.ShippedQty;
                        prrdetdet.AddedBy = di.AddedBy;
                        prrdetdet.AddedDate = di.AddedDate;

                        enty.ProductionReportDetails.Add(prrdetdet);





                    }
                    enty.SaveChanges();

                }


                return Donum;
            }

        }


        public class ProductionReportDetailsData
        {
            public int ProductionReportDet_Pk { get; set; }
            public int JobContractDetail_pk { get; set; }
            public int CutQty { get; set; }
            public int SewnQty { get; set; }
            public int WashedQty { get; set; }
            public int PackedQty { get; set; }
            public int ShippedQty { get; set; }
            public string AddedBy { get; set; }
            public DateTime AddedDate { get; set; }
            public DateTime ProductionDate { get; set; }


        }


    

}