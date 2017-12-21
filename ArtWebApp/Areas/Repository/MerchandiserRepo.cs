using ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
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
             using(ArtEntitiesnew db= new ArtEntitiesnew())
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


                if(order.ShipmentType== "Sea")
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
                    frightchargedetail.SPO_PK =int.Parse( element.SpoPK.ToString());
                    frightchargedetail.SPODetails_PK = int.Parse(element.SPODetails_PK.ToString());
                    frightchargedetail.StockFreightCharge = element.FreightCharge;
                    frightchargedetail.StockFreightRequestID = mstr.StockFreightRequestID;
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
                    frightchargedetail.LabRequestID = mstr.LabRequestID;
                    db.LabChargeDetails.Add(frightchargedetail);
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


    }






}