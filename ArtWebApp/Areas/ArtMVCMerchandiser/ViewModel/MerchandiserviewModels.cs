using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel
{
    public class MerchandiserviewModels
    {
    }

    public partial class FreightRequestMasterViewModel
    {
        

        public decimal FreightRequestID { get; set; }
        public string FreightRequestNum { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string FromParty { get; set; }
        public string ToParty { get; set; }
        public string Shipper { get; set; }
        public string Weight { get; set; }
        public string ContentofPackage { get; set; }
        public string DebitTo { get; set; }
        public string Reason { get; set; }
        public string Merchandiser { get; set; }
        public string ForwarderDetails { get; set; }
        public string ApproximateCharges { get; set; }
        public string Remark { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string IsApproved { get; set; }
        public string IsPosted { get; set; }

        public virtual ICollection<FreightChargeDetailViewMoodel> FreightChargeDetails { get; set; }
    }
    public partial class FreightChargeDetailViewMoodel
    {
        public decimal FreightReqDetID { get; set; }
        public Nullable<decimal> AtcID { get; set; }
        public Nullable<decimal> FreightCharge { get; set; }
        public Nullable<decimal> FreightRequestID { get; set; }

        public virtual FreightRequestMasterViewModel FreightRequestMasterViewModel { get; set; }
    }



    public partial class LabChargeMasterViewModel
    {


        public decimal LabRequestID { get; set; }
        public string LabRequestNum { get; set; }
        public int SupplierPK  { get; set; }

        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }      
        public string Reason { get; set; }
        public string Merchandiser { get; set; }
      
        public string ApproximateCharges { get; set; }
        public string Remark { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string IsApproved { get; set; }
        public string IsPosted { get; set; }

        public virtual ICollection<FreightChargeDetailViewMoodel> FreightChargeDetails { get; set; }
    }


}