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
    
    public partial class PatternNameBank
    {
        public decimal PatternID { get; set; }
        public Nullable<decimal> Skudetpk { get; set; }
        public Nullable<decimal> OurStyleID { get; set; }
        public string Shrinkage { get; set; }
        public string PatternName { get; set; }
        public string ReferncePatternName { get; set; }
        public Nullable<decimal> Location_Pk { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
    
        public virtual AtcDetail AtcDetail { get; set; }
        public virtual LocationMaster LocationMaster { get; set; }
        public virtual SkuRawmaterialDetail SkuRawmaterialDetail { get; set; }
    }
}
