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
    
    public partial class InvoiceMaster
    {
        public InvoiceMaster()
        {
            this.InvoiceDetails = new HashSet<InvoiceDetail>();
        }
    
        public decimal Invoice_PK { get; set; }
        public string InvoiceNum { get; set; }
        public Nullable<decimal> Location_PK { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<decimal> Bank_PK { get; set; }
        public Nullable<decimal> ActualInvoiceValue { get; set; }
        public string Sinvnum { get; set; }
        public string Refnum { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string IsPosted { get; set; }
    
        public virtual BankMaster BankMaster { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
