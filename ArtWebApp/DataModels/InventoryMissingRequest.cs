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
    
    public partial class InventoryMissingRequest
    {
        public InventoryMissingRequest()
        {
            this.InventoryMissingDetails = new HashSet<InventoryMissingDetail>();
        }
    
        public decimal MisplaceApp_pk { get; set; }
        public Nullable<decimal> FromLctn_pk { get; set; }
        public Nullable<decimal> Atc_id { get; set; }
        public System.DateTime MisplaceDate { get; set; }
        public string Explanation { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> Addeddate { get; set; }
        public string Level1Approval { get; set; }
        public string Level1ApprovedBY { get; set; }
        public string IsApproved { get; set; }
        public string ApprovedBy { get; set; }
        public string reqnum { get; set; }
        public Nullable<System.DateTime> L1ApprovedDate { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string Reason { get; set; }
        public Nullable<decimal> MoqValue { get; set; }
        public Nullable<decimal> Freightvalue { get; set; }
        public Nullable<decimal> OtherValues { get; set; }
    
        public virtual ICollection<InventoryMissingDetail> InventoryMissingDetails { get; set; }
    }
}
