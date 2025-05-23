//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArtWebApp.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class SampCutReqMaster
    {
        public SampCutReqMaster()
        {
            this.SamCutAssignmentMasters = new HashSet<SamCutAssignmentMaster>();
            this.SamDailyCutStatus = new HashSet<SamDailyCutStatu>();
            this.SamDailyDeliveryStatus = new HashSet<SamDailyDeliveryStatu>();
            this.SamDailySewStatus = new HashSet<SamDailySewStatu>();
            this.SampleDispatchDetails = new HashSet<SampleDispatchDetail>();
        }
    
        public decimal SampCutreqID { get; set; }
        public string ReqNum { get; set; }
        public string Fabric { get; set; }
        public string StyleDescription { get; set; }
        public Nullable<decimal> BuyerID { get; set; }
        public Nullable<decimal> PatternRefID { get; set; }
        public Nullable<decimal> PatternStyleID { get; set; }
        public Nullable<decimal> SampleTypeID { get; set; }
        public Nullable<System.DateTime> SampleRequiredDate { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string AddedBy { get; set; }
        public string SizeDetail { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<bool> MarkCompleted { get; set; }
        public Nullable<System.DateTime> MarkedCompletedDate { get; set; }
        public string MarkCompletedBy { get; set; }
        public string Remark { get; set; }
        public bool IsTeckPack { get; set; }
        public string IsReceived { get; set; }
        public string Size1 { get; set; }
        public string Size2 { get; set; }
        public string Size3 { get; set; }
        public string Size4 { get; set; }
        public string Size5 { get; set; }
        public string Size6 { get; set; }
        public Nullable<decimal> Qty1 { get; set; }
        public Nullable<decimal> Qty2 { get; set; }
        public Nullable<decimal> Qty3 { get; set; }
        public Nullable<decimal> Qty4 { get; set; }
        public Nullable<decimal> Qty5 { get; set; }
        public Nullable<decimal> Qty6 { get; set; }
        public Nullable<decimal> Size1CutQty { get; set; }
        public Nullable<decimal> Size2CutQty { get; set; }
        public Nullable<decimal> Size3CutQty { get; set; }
        public Nullable<decimal> Size4CutQty { get; set; }
        public Nullable<decimal> Size5CutQty { get; set; }
        public Nullable<decimal> Size6CutQty { get; set; }
        public Nullable<decimal> Size1SewQty { get; set; }
        public Nullable<decimal> Size2SewQty { get; set; }
        public Nullable<decimal> Size3SewQty { get; set; }
        public Nullable<decimal> Size4SewQty { get; set; }
        public Nullable<decimal> Size5SewQty { get; set; }
        public Nullable<decimal> Size6SewQty { get; set; }
        public Nullable<System.DateTime> DateofAction { get; set; }
        public Nullable<decimal> Size1DeliveredQty { get; set; }
        public Nullable<decimal> Size2DeliveredQty { get; set; }
        public Nullable<decimal> Size3DeliveredQty { get; set; }
        public Nullable<decimal> Size4DeliveredQty { get; set; }
        public Nullable<decimal> Size5DeliveredQty { get; set; }
        public Nullable<decimal> Size6DeliveredQty { get; set; }
        public Nullable<decimal> Size1RecivedQty { get; set; }
        public Nullable<decimal> Size2RecivedQty { get; set; }
        public Nullable<decimal> Size3RecivedQty { get; set; }
        public Nullable<decimal> Size4RecivedQty { get; set; }
        public Nullable<decimal> Size5RecivedQty { get; set; }
        public Nullable<decimal> Size6RecivedQty { get; set; }
        public Nullable<System.DateTime> Samplingcommiteddate { get; set; }
        public Nullable<System.DateTime> NewCommitedDate { get; set; }
    
        public virtual BuyerMaster BuyerMaster { get; set; }
        public virtual PatternStyle PatternStyle { get; set; }
        public virtual PatterRefMaster PatterRefMaster { get; set; }
        public virtual ICollection<SamCutAssignmentMaster> SamCutAssignmentMasters { get; set; }
        public virtual ICollection<SamDailyCutStatu> SamDailyCutStatus { get; set; }
        public virtual ICollection<SamDailyDeliveryStatu> SamDailyDeliveryStatus { get; set; }
        public virtual ICollection<SamDailySewStatu> SamDailySewStatus { get; set; }
        public virtual SampCutReqMaster SampCutReqMaster1 { get; set; }
        public virtual SampCutReqMaster SampCutReqMaster2 { get; set; }
        public virtual SampleType SampleType { get; set; }
        public virtual ICollection<SampleDispatchDetail> SampleDispatchDetails { get; set; }
    }
}
