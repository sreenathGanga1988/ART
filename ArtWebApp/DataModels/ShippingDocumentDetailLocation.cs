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
    
    public partial class ShippingDocumentDetailLocation
    {
        public decimal ShippinfDetailLCtn_PK { get; set; }
        public Nullable<decimal> ShippingDet_PK { get; set; }
        public Nullable<decimal> DocDet_Pk { get; set; }
        public Nullable<decimal> Location_PK { get; set; }
        public string Type { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string IsDeleted { get; set; }
        public string IsReceived { get; set; }
        public string ReceivedBy { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
    }
}
