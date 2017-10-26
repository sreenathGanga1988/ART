using ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Repository
{
    public class MerchandiserRepo
    {











    }

    public class FreightChargeRepo
    {
        


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
                mstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
                mstr.AddedDate = DateTime.Now;
              
                db.FreightRequestMasters.Add(mstr);

                db.SaveChanges();

                reqnum= mstr.FreightRequestNum = "FRQ-" + mstr.FreightRequestID;

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
    }






}