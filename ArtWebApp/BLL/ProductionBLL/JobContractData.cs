using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.ProductionBLL
{
    public class JobContractData
    {
        public JobContractMasterData JCmstrdata { get; set; }
        public List<JobContractDetailData> JobContractDetailDataCollection { get; set; }

        public String insertJObContract(JobContractData jcdata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                JobContractMaster jcmstr = new JobContractMaster();
                jcmstr.AtcID = jcdata.JCmstrdata.AtcID;
                jcmstr.AddedDate = DateTime.Now;
                jcmstr.AddedBy = jcdata.JCmstrdata.AddedBy;
                jcmstr.Location_Pk = jcdata.JCmstrdata.Location_Pk;

                enty.JobContractMasters.Add(jcmstr);


                enty.SaveChanges();

                Donum = jcmstr.JOBContractNUM = CodeGenerator.GetUniqueCode("JC", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(jcmstr.JobContract_pk.ToString()));




                foreach (JobContractDetailData di in jcdata.JobContractDetailDataCollection)
                {
                    //Add the delivery details
                    JobContractDetail jcdetdata = new JobContractDetail();
                    jcdetdata.JobContract_pk = jcmstr.JobContract_pk;
                    jcdetdata.OurStyleID = di.OurStyleID;
                    jcdetdata.PoPackID = di.PoPackID;
                    jcdetdata.CMvalue = decimal.Parse ( di.CMvalue.ToString ());
                    enty.JobContractDetails.Add(jcdetdata);





                }
                enty.SaveChanges();

            }


            return Donum;
        }

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

    }



    public class JobContractMasterData
    {
        public int JobContract_pk { get; set; }
        public string JOBContractNUM { get; set; }
        public int Location_Pk { get; set; }
        public int AtcID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }





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
                    //Add the delivery details
                    ShipmentHandOverDetail shpdert = new ShipmentHandOverDetail();
                    shpdert.ShipmentHandMaster_PK = shpmstr.ShipmentHandMaster_PK;
                    shpdert.JobContractDetail_pk = di.JobContractDetail_pk;
                    shpdert.ShippedQty = di.ShippedQty;
                    shpdert.ShipmentHandOverDate = di.ShipmenthandOverdate;
                    shpdert.AddedBy = di.AddedBy;
                    shpdert.AddedDate = di.AddedDate;
                    enty.ShipmentHandOverDetails.Add(shpdert);





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
    }



    public class ShipmentHandOverData
        {
            public int ShipmentHandOver_PK { get; set; }

            public int JobContractDetail_pk { get; set; }
            public int ShippedQty { get; set; }
            public DateTime AddedDate { get; set; }
            public string AddedBy { get; set; }

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
                         PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS POPackNUm, AtcDetails.FOB, JobContractDetail.PoPackID, JobContractDetail.OurStyleID, ShipmentHandOverDetails.ShipmentHandOver_PK,
						 ISNULL((SELECT SUM(InvoiceQty) FROM  InvoiceDetail
WHERE        (PoPackID = JobContractDetail.PoPackID) AND (OurStyleID = JobContractDetail.OurStyleID) AND (ShipmentHandOver_PK = ShipmentHandOverDetails.ShipmentHandOver_PK)),0) as InvoicedQty

FROM            ShipmentHandOverMaster INNER JOIN
                         ShipmentHandOverDetails ON ShipmentHandOverMaster.ShipmentHandMaster_PK = ShipmentHandOverDetails.ShipmentHandMaster_PK INNER JOIN
                         JobContractDetail ON ShipmentHandOverDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk INNER JOIN
                         AtcDetails ON JobContractDetail.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId INNER JOIN
                         PoPackMaster ON JobContractDetail.PoPackID = PoPackMaster.PoPackId" + condition + " GROUP BY ShipmentHandOverMaster.ShipmentHandOverCode, AtcDetails.OurStyle, AtcMaster.AtcNum, PoPackMaster.BuyerPO, PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO, AtcDetails.FOB,JobContractDetail.PoPackID, JobContractDetail.OurStyleID, ShipmentHandOverDetails.ShipmentHandOver_PK, ShipmentHandOverMaster.ShipmentHandMaster_PK) tt";
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