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
    
    public partial class GetPurchasesfromSupplier_SP_Result
    {
        public string PONum { get; set; }
        public string AtcNum { get; set; }
        public string CurrencyCode { get; set; }
        public string SupplierName { get; set; }
        public Nullable<decimal> POvalue { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string POType { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string BuyerName { get; set; }
    }
}