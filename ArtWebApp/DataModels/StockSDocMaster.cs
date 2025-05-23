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
    
    public partial class StockSDocMaster
    {
        public decimal SDoc_Pk { get; set; }
        public string SDocNum { get; set; }
        public Nullable<decimal> Location_PK { get; set; }
        public string ContainerNum { get; set; }
        public string BOENum { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> InhouseDate { get; set; }
        public Nullable<System.DateTime> ETADate { get; set; }
        public Nullable<decimal> Supplier_PK { get; set; }
        public string AddedBy { get; set; }
        public System.DateTime AddedDate { get; set; }
        public string IsCompleted { get; set; }
        public Nullable<decimal> SDocValue { get; set; }
        public Nullable<decimal> Currency_PK { get; set; }
    
        public virtual LocationMaster LocationMaster { get; set; }
        public virtual SupplierMaster SupplierMaster { get; set; }
    }
}
