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
    
    public partial class InvoiceDetail
    {
        public decimal InvoiceDet_PK { get; set; }
        public Nullable<decimal> Invoice_PK { get; set; }
        public Nullable<decimal> ShipmentHandOver_PK { get; set; }
        public Nullable<decimal> FOB { get; set; }
        public Nullable<decimal> PoPackID { get; set; }
        public Nullable<decimal> OurStyleID { get; set; }
        public Nullable<decimal> InvoiceQty { get; set; }
        public Nullable<decimal> CartonNum { get; set; }
    
        public virtual InvoiceMaster InvoiceMaster { get; set; }
    }
}
