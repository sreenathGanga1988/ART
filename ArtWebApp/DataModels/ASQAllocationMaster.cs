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
    
    public partial class ASQAllocationMaster
    {
        public decimal ASQAllocation_PK { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<decimal> Locaion_PK { get; set; }
        public Nullable<decimal> PoPack_Detail_PK { get; set; }
        public Nullable<decimal> PoPackId { get; set; }
        public Nullable<decimal> OurStyleId { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public string MarkedUnCut { get; set; }
        public string ReAllocated { get; set; }
    
        public virtual POPackDetail POPackDetail { get; set; }
    }
}