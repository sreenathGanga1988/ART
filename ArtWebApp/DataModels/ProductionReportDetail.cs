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
    
    public partial class ProductionReportDetail
    {
        public decimal ProductionReportDet_Pk { get; set; }
        public Nullable<decimal> JobContractDetail_pk { get; set; }
        public Nullable<decimal> CutQty { get; set; }
        public Nullable<decimal> SewnQty { get; set; }
        public Nullable<decimal> WashedQty { get; set; }
        public Nullable<decimal> PackedQty { get; set; }
        public Nullable<decimal> ShippedQty { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<System.DateTime> ShipmentHandOverDate { get; set; }
        public Nullable<decimal> POPackId { get; set; }
        public Nullable<decimal> OurStyleID { get; set; }
        public Nullable<decimal> ProducedLctn_PK { get; set; }
        public Nullable<System.DateTime> SDODate { get; set; }
        public string SDONum { get; set; }
        public Nullable<decimal> Location_PK { get; set; }
    
        public virtual JobContractDetail JobContractDetail { get; set; }
    }
}
