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
    
    public partial class ProcurmentPlanDetail
    {
        public decimal ProcPlan_PK { get; set; }
        public Nullable<decimal> Skudet_Pk { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<System.DateTime> ETADate { get; set; }
        public string AddedBY { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string IsDeleted { get; set; }
        public string DeletedBy { get; set; }
    
        public virtual SkuRawmaterialDetail SkuRawmaterialDetail { get; set; }
    }
}
