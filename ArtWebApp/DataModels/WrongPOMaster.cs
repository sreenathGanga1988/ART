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
    
    public partial class WrongPOMaster
    {
        public WrongPOMaster()
        {
            this.WrongPODetails = new HashSet<WrongPODetail>();
        }
    
        public decimal WrongPO_Pk { get; set; }
        public Nullable<decimal> PO_PK { get; set; }
        public string AddedBY { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string L1ApprovedBY { get; set; }
        public Nullable<System.DateTime> L1ApprovedDate { get; set; }
        public string ApprovedBY { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string MerchandiserName { get; set; }
        public string Explanation { get; set; }
        public string IsApproved { get; set; }
        public string Reqnum { get; set; }
        public string Isforwarded { get; set; }
        public string ForwardedBy { get; set; }
    
        public virtual ProcurementMaster ProcurementMaster { get; set; }
        public virtual ICollection<WrongPODetail> WrongPODetails { get; set; }
    }
}