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
    
    public partial class ExtDeliveryToken
    {
        public decimal ExtDeliveryTokenID { get; set; }
        public Nullable<decimal> SkuDet_PK { get; set; }
        public string ItemName { get; set; }
        public Nullable<decimal> Fromlocation { get; set; }
        public Nullable<decimal> ToLocation { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string AddedBY { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<decimal> DeliveredQty { get; set; }
        public Nullable<decimal> BalanceToDeliver { get; set; }
        public string IsApproved { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string Isdeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<decimal> AtcID { get; set; }
        public Nullable<decimal> Itemgroup { get; set; }
        public Nullable<decimal> SKU { get; set; }
    
        public virtual AtcMaster AtcMaster { get; set; }
        public virtual LocationMaster LocationMaster { get; set; }
        public virtual LocationMaster LocationMaster1 { get; set; }
        public virtual SkuRawmaterialDetail SkuRawmaterialDetail { get; set; }
        public virtual ItemGroupMaster ItemGroupMaster { get; set; }
    }
}