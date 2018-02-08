using ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Repository
{
    public class MerchandiserRepo
    {











    }

    public class FreightChargeRepo
    {

        String connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;

        public String InsertFreightCharges(FreightRequestMasterViewModel order)
        {

            String reqnum = "";
            using (ArtEntitiesnew db = new ArtEntitiesnew())
            {
                FreightRequestMaster mstr = new FreightRequestMaster();
                mstr.FromParty = order.FromParty;
                mstr.ToParty = order.ToParty;
                mstr.Shipper = order.Shipper;
                mstr.Weight = order.Weight;
                mstr.ContentofPackage = order.ContentofPackage;
                mstr.DebitTo = order.DebitTo;
                mstr.Reason = order.Reason;
                mstr.Merchandiser = order.Merchandiser;
                mstr.ForwarderDetails = order.ForwarderDetails;
                mstr.ApproximateCharges = order.ApproximateCharges;
                mstr.Remark = order.Remark;
                mstr.IsApproved = "N";
                mstr.IsDeleted = "N";
                mstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
                mstr.AddedDate = DateTime.Now;
                mstr.ShipementType = order.ShipmentType;
                db.FreightRequestMasters.Add(mstr);

                db.SaveChanges();


                if (order.ShipmentType == "Sea")
                {
                    reqnum = mstr.FreightRequestNum = "SFRQ-" + mstr.FreightRequestID;
                }
                else
                {
                    reqnum = mstr.FreightRequestNum = "FRQ-" + mstr.FreightRequestID;
                }


                foreach (FreightChargeDetailViewMoodel element in order.FreightChargeDetails)
                {

                    FreightChargeDetail frightchargedetail = new FreightChargeDetail();
                    frightchargedetail.AtcID = element.AtcID;
                    frightchargedetail.FreightCharge = element.FreightCharge;
                    frightchargedetail.FreightRequestID = mstr.FreightRequestID;
                    frightchargedetail.FirstFreightCharge = element.FreightCharge;
                    db.FreightChargeDetails.Add(frightchargedetail);
                }

                db.SaveChanges();
            }

            return reqnum;
        }
        public String InsertStocFreightCharges(FreightRequestMasterViewModel order)
        {

            String reqnum = "";
            using (ArtEntitiesnew db = new ArtEntitiesnew())
            {
                StockFreightRequestMaster mstr = new StockFreightRequestMaster();
                mstr.FromParty = order.FromParty;
                mstr.ToParty = order.ToParty;
                mstr.Shipper = order.Shipper;
                mstr.Weight = order.Weight;
                mstr.ContentofPackage = order.ContentofPackage;
                mstr.DebitTo = order.DebitTo;
                mstr.Reason = order.Reason;
                mstr.Merchandiser = order.Merchandiser;
                mstr.ForwarderDetails = order.ForwarderDetails;
                mstr.ApproximateCharges = order.ApproximateCharges;
                mstr.Remark = order.Remark;
                mstr.IsApproved = "N";
                mstr.IsDeleted = "N";
                mstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
                mstr.AddedDate = DateTime.Now;
                mstr.ShipementType = order.ShipmentType;
                db.StockFreightRequestMasters.Add(mstr);

                db.SaveChanges();

                reqnum = mstr.StockFreightRequestNum = "STFRQ-" + mstr.StockFreightRequestID;

                foreach (StockFreightChargeDetailViewMoodel element in order.stockFreightChargeDetails)
                {

                    StockFreightChargeDetail frightchargedetail = new StockFreightChargeDetail();
                    frightchargedetail.SPO_PK = int.Parse(element.SpoPK.ToString());
                    frightchargedetail.SPODetails_PK = int.Parse(element.SPODetails_PK.ToString());
                    frightchargedetail.StockFreightCharge = element.FreightCharge;
                    frightchargedetail.StockFreightRequestID = mstr.StockFreightRequestID;
                    frightchargedetail.FirstStockFreightCharge = element.FreightCharge;
                    db.StockFreightChargeDetails.Add(frightchargedetail);
                }

                db.SaveChanges();
            }

            return reqnum;
        }
        public String InsertLabCharges(LabChargeMasterViewModel order)
        {

            String reqnum = "";
            using (ArtEntitiesnew db = new ArtEntitiesnew())
            {
                LabRequestMaster mstr = new LabRequestMaster();
                mstr.Supplier_pk = order.SupplierPK;

                mstr.Reason = order.Reason;
                mstr.Merchandiser = order.Merchandiser;

                mstr.ApproximateCharges = order.ApproximateCharges;
                mstr.Remark = order.Remark;
                mstr.IsApproved = "N";

                mstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
                mstr.AddedDate = DateTime.Now;

                db.LabRequestMasters.Add(mstr);

                db.SaveChanges();

                reqnum = mstr.LabRequestNum = "LRQ-" + mstr.LabRequestID;

                foreach (FreightChargeDetailViewMoodel element in order.FreightChargeDetails)
                {

                    LabChargeDetail frightchargedetail = new LabChargeDetail();
                    frightchargedetail.AtcID = element.AtcID;
                    frightchargedetail.LabCharge = element.FreightCharge;
                    frightchargedetail.FirstLabCharge = element.FreightCharge;
                    frightchargedetail.LabRequestID = mstr.LabRequestID;
                    db.LabChargeDetails.Add(frightchargedetail);
                }

                db.SaveChanges();
            }

            return reqnum;
        }

        public String UpdateFreightCharges(FreightRequestMasterViewModel order)
        {

            String reqnum = "";
            using (ArtEntitiesnew db = new ArtEntitiesnew())
            {


                var q = from frgtmstr in db.FreightRequestMasters
                        where frgtmstr.FreightRequestID == order.FreightRequestID
                        select frgtmstr;

                foreach (var element in q)
                {
                    element.FromParty = order.FromParty;
                    element.ToParty = order.ToParty;
                    element.Shipper = order.Shipper;
                    element.Weight = order.Weight;
                    element.ContentofPackage = order.ContentofPackage;
                    element.DebitTo = order.DebitTo;
                    element.Reason = order.Reason;
                    element.Merchandiser = order.Merchandiser;
                    element.ForwarderDetails = order.ForwarderDetails;
                    element.ApproximateCharges = order.ApproximateCharges;
                    element.Remark = order.Remark;

                    try
                    {
                        var tot = order.FreightChargeDetails.Sum(u => u.FreightCharge);
                        element.ApproximateCharges = tot.ToString();
                    }
                    catch (Exception)
                    {
                        element.ApproximateCharges = order.ApproximateCharges;

                    }
                    element.IsApproved = order.IsApproved;
                    
                    element.IsDeleted = "N";
                    element.AddedBy = HttpContext.Current.Session["Username"].ToString();
                    element.AddedDate = DateTime.Now;

                }







                foreach (FreightChargeDetailViewMoodel element in order.FreightChargeDetails)
                {

                    var q1 = from frgtdet in db.FreightChargeDetails
                             where frgtdet.FreightReqDetID == element.FreightReqDetID
                             select frgtdet;

                    foreach (var frightchargedetail in q1)
                    {
                        frightchargedetail.FreightCharge = element.FreightCharge;
                        frightchargedetail.Remark = element.Remark;
                    }



                }

                db.SaveChanges();
            }

            return reqnum;
        }
        public string UpdateLabCharge(LabChargeMasterViewModel order)
        {
            String reqnum = "";
            using (ArtEntitiesnew db = new ArtEntitiesnew())
            {
                var q = from labchargemaster in db.LabRequestMasters
                        where labchargemaster.LabRequestID == order.LabRequestID
                        select labchargemaster;
                foreach (var element in q)
                {

                    element.Merchandiser = order.Merchandiser;
                    element.Reason = order.Reason;
                    element.Remark = order.Remark;
                    element.Supplier_pk = order.SupplierPK;
                    element.IsApproved = order.IsApproved;
                    element.AddedBy = HttpContext.Current.Session["Username"].ToString();
                    element.AddedDate = DateTime.Now;
                }
                foreach (FreightChargeDetailViewMoodel element in order.FreightChargeDetails)
                {
                    var q1 = from labdet in db.LabChargeDetails
                             where labdet.LabReqDetID == element.FreightReqDetID
                             select labdet;

                    foreach (var labdetails in q1)
                    {
                        labdetails.LabCharge = element.FreightCharge;
                        labdetails.Remark = element.Remark;
                    }



                }
                db.SaveChanges();

            }
            return reqnum;
        }
        /// <summary>
        /// get the last costing
        /// </summary>
        /// <param name="ourstyle"></param>
        /// <returns></returns>
        public FreightChargeDetail GetAllowedFreightCharges(FreightChargeDetail freightChargeDetailnew)
        {
            Decimal costingpk = 0;
            Decimal alreadyUsed = 0;


            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT      sum(  Allowedvalue) As AllowedValue
FROM            (SELECT        OurStyleID, Qty * Compvalue AS Allowedvalue, AtcId
                          FROM            (SELECT        OurStyleID, ISNULL
                             ((SELECT        SUM(PoQty) AS Expr1
                                 FROM            POPackDetails
                                 WHERE        (OurStyleID = AtcDetails.OurStyleID)), 0) AS Qty, ISNULL
                             ((SELECT        MAX(StyleCostingComponentDetails.CompValue) AS Expr1
                                 FROM            StyleCostingComponentDetails INNER JOIN
                                                          StyleCostingMaster ON StyleCostingComponentDetails.Costing_PK = StyleCostingMaster.Costing_PK
                                 WHERE        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingComponentDetails.CostComp_PK = 8) AND (StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID)), 0) AS Compvalue, AtcId
FROM            AtcDetails
WHERE        (AtcId = @atcid)) AS tt) AS ttt
GROUP BY  AtcId", con);

                cmd.Parameters.AddWithValue("@atcid", freightChargeDetailnew.AtcID);
                try
                {
                    costingpk = Decimal.Parse(cmd.ExecuteScalar().ToString());
                }
                catch (Exception)
                {

                    costingpk = 0;
                }



            }
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from frgdet in enty.FreightChargeDetails
                         where frgdet.AtcID == freightChargeDetailnew.AtcID
                         select new { frgdet.FreightCharge }).ToList();

                foreach (var element in q)
                {

                    try
                    {
                        if (decimal.Parse(element.FreightCharge.ToString()) > 0)
                        {
                            alreadyUsed = alreadyUsed + decimal.Parse(element.FreightCharge.ToString());
                        }

                    }
                    catch (Exception)
                    {


                    }
                }

                var q1 = (from frgdet in enty.ProcurementMasters
                          where frgdet.AtcId == freightChargeDetailnew.AtcID && frgdet.IsDeleted == "N"
                          select new { frgdet.FreightCharge }).ToList();

                foreach (var element in q1)
                {

                    try
                    {
                        if (decimal.Parse(element.FreightCharge.ToString()) > 0)
                        {
                            alreadyUsed = alreadyUsed + decimal.Parse(element.FreightCharge.ToString());
                        }

                    }
                    catch (Exception)
                    {


                    }
                }


            }




            freightChargeDetailnew.AllowedValue = costingpk.ToString();
            freightChargeDetailnew.UsedValue = alreadyUsed.ToString();
            freightChargeDetailnew.BalanceValue = (Decimal.Parse(costingpk.ToString()) - Decimal.Parse(alreadyUsed.ToString())).ToString();
            costingpk = costingpk - alreadyUsed;


            return freightChargeDetailnew;
        }
        public LabChargeDetail GetAllowedLabCharges(LabChargeDetail freightChargeDetailnew)
        {
            Decimal costingpk = 0;
            Decimal alreadyUsed = 0;


            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT      sum(  Allowedvalue) As AllowedValue
FROM            (SELECT        OurStyleID, Qty * Compvalue AS Allowedvalue, AtcId
                          FROM            (SELECT        OurStyleID, ISNULL
                             ((SELECT        SUM(PoQty) AS Expr1
                                 FROM            POPackDetails
                                 WHERE        (OurStyleID = AtcDetails.OurStyleID)), 0) AS Qty, ISNULL
                             ((SELECT        MAX(StyleCostingComponentDetails.CompValue) AS Expr1
                                 FROM            StyleCostingComponentDetails INNER JOIN
                                                          StyleCostingMaster ON StyleCostingComponentDetails.Costing_PK = StyleCostingMaster.Costing_PK
                                 WHERE        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingComponentDetails.CostComp_PK = 10) AND (StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID)), 0) AS Compvalue, AtcId
FROM            AtcDetails
WHERE        (AtcId = @atcid)) AS tt) AS ttt
GROUP BY  AtcId", con);

                cmd.Parameters.AddWithValue("@atcid", freightChargeDetailnew.AtcID);
                try
                {
                    costingpk = Decimal.Parse(cmd.ExecuteScalar().ToString());
                }
                catch (Exception)
                {

                    costingpk = 0;
                }



            }
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from frgdet in enty.LabChargeDetails
                         where frgdet.AtcID == freightChargeDetailnew.AtcID
                         select new { frgdet.LabCharge }).ToList();

                foreach (var element in q)
                {

                    try
                    {
                        if (decimal.Parse(element.LabCharge.ToString()) > 0)
                        {
                            alreadyUsed = alreadyUsed + decimal.Parse(element.LabCharge.ToString());
                        }

                    }
                    catch (Exception)
                    {


                    }
                }



            }




            freightChargeDetailnew.AllowedValue = costingpk.ToString();
            freightChargeDetailnew.UsedValue = alreadyUsed.ToString();
            freightChargeDetailnew.BalanceValue = (Decimal.Parse(costingpk.ToString()) - Decimal.Parse(alreadyUsed.ToString())).ToString();
            costingpk = costingpk - alreadyUsed;


            return freightChargeDetailnew;
        }


    }



    public class AtcClosingRepo
    {

        public DataTable GetNonClosedAtc()
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@"NonClosedAtcList_SP");

            cmd.CommandType = CommandType.StoredProcedure;


            return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }

        public DataTable GetClosedAtc(string month)
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@"SELECT        AtcMaster.AtcId, AtcMaster.AtcNum, BuyerMaster.BuyerName, CountryMaster.ShortName, AtcAction.ActionDoneBy, AtcAction.ActionDoneDate, AtcAction.ActionType, AtcAction.Month, AtcAction.Year
FROM            AtcMaster INNER JOIN
                         AtcAction ON AtcMaster.AtcId = AtcAction.AtcID INNER JOIN
                         BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID INNER JOIN
                         CountryMaster ON AtcMaster.ProductionCountryID = CountryMaster.CountryID
WHERE        (AtcAction.Month = @Param1)");


            cmd.Parameters.AddWithValue("@Param1", month);
            

            return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }



        public DataTable GetBEofMonthAtc(string month)
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@" SELECT AtcMaster.AtcId, AtcMaster.AtcNum, BuyerMaster.BuyerName, CountryMaster.ShortName, BEOfMonth.AddedBy as ActionDoneBy, BEOfMonth.AddedDate as ActionDoneDate,'BE' as ActionType, BEOfMonth.month,'0' as Year
FROM            AtcMaster INNER JOIN
                         BEOfMonth ON AtcMaster.AtcId = BEOfMonth.AtcID INNER JOIN
                         BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID INNER JOIN
                         CountryMaster ON AtcMaster.ProductionCountryID = CountryMaster.CountryID
WHERE        (BEOfmonth.Month = @Param1)");


            cmd.Parameters.AddWithValue("@Param1", month);


            return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }








       
        public List<AtcClosingModel> GetNonclosedatclist()
        {
            List<AtcClosingModel> ls = new List<AtcClosingModel>();
            DataTable dt = GetNonClosedAtc();
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow drow in dt.Rows)
                    {

                        AtcClosingModel atcClosingModel = new AtcClosingModel();
                        atcClosingModel.IsSelected = false;
                        atcClosingModel.AtcNum = drow["AtcNum"].ToString();
                        atcClosingModel.BuyerName = drow["BuyerName"].ToString();
                        atcClosingModel.NoofStyles = drow["NoofStyles"].ToString();
                        atcClosingModel.IsCompleted = drow["IsCompleted"].ToString();
                        atcClosingModel.IsClosed = drow["IsClosed"].ToString();
                        atcClosingModel.Description = drow["Description"].ToString();
                        atcClosingModel.ProjectionQty = drow["ProjectionQty"].ToString();
                        atcClosingModel.AtcId = int.Parse(drow["AtcId"].ToString());
                        ls.Add(atcClosingModel);

                    }








                }

            }




            return ls;


        }

        public DataTable AtcofMonth(string month)
        {
           
            DataTable dt = GetClosedAtc(month);
            if (dt != null)
            {

                

            }




            return dt;


        }
        public void CloseAtc(AtcClosingModelList atcClosingModelList)
        {


            foreach(AtcClosingModel atcClosingModel in atcClosingModelList.atcClosingModels)
            {

               using (ArtEntitiesnew enty= new ArtEntitiesnew())
                {

                    var q = from atcmstr in enty.AtcMasters
                            where atcmstr.AtcId == atcClosingModel.AtcId
                            select atcmstr;
                    foreach (var element in q)
                    {
                        element.IsClosed = "Y";



                    }

                    AtcAction atcAction = new AtcAction();
                    atcAction.AtcID = atcClosingModel.AtcId;
                    atcAction.ActionType = atcClosingModelList.Type;
                    atcAction.ActionDoneDate = atcClosingModelList.Addeddate;
                    atcAction.ActionDoneBy = atcClosingModelList.AddedBy;
                    atcAction.Month = atcClosingModelList.Month;

                        enty.AtcActions.Add(atcAction);

                    enty.SaveChanges();
                }


            }


        }


        public void BEofMonth(AtcClosingModelList atcClosingModelList)
        {


            foreach (AtcClosingModel atcClosingModel in atcClosingModelList.atcClosingModels)
            {

                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                {

                   

                    BEOfMonth bEOfMonth = new BEOfMonth();
                    bEOfMonth.AtcID = atcClosingModel.AtcId;                 
                    bEOfMonth.AddedDate = atcClosingModelList.Addeddate;
                    bEOfMonth.AddedBy = atcClosingModelList.AddedBy;
                    bEOfMonth.Month = atcClosingModelList.Month;
                    enty.BEOfMonths.Add(bEOfMonth);

                    enty.SaveChanges();
                }


            }


        }


        public void RemoveMonth(String Month)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from bedet in enty.BEOfMonths
                        where bedet.Month == Month
                        select bedet;
                foreach(var element in q)
                {
                    enty.BEOfMonths.Remove(element);


                }

                enty.SaveChanges();
            }
            }
        }


}