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
    
    public partial class ShippingDocumentActionDetail
    {
        public decimal ShipipingDocumentActionPK { get; set; }
        public Nullable<decimal> ShipingDoc_PK { get; set; }
        public string ActionType { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string Remark { get; set; }
        public string NoPackages { get; set; }
    
        public virtual ShippingDocumentMaster ShippingDocumentMaster { get; set; }
    }
}
