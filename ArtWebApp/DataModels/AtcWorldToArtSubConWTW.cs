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
    
    public partial class AtcWorldToArtSubConWTW
    {
        public decimal WTWRec_PK { get; set; }
        public Nullable<decimal> SDOID { get; set; }
        public string SDONo { get; set; }
        public decimal WTWID { get; set; }
        public string WTWNo { get; set; }
        public Nullable<System.DateTime> TransferDate { get; set; }
        public decimal POPackID { get; set; }
        public decimal PLID { get; set; }
        public decimal RunPackID { get; set; }
        public string CartonNo { get; set; }
        public string BarcodeNo { get; set; }
        public string TruckCanterNo { get; set; }
        public decimal Location_PK { get; set; }
        public Nullable<decimal> ArtLocation_PK { get; set; }
        public string LocationName { get; set; }
        public Nullable<decimal> IssueLocation_PK { get; set; }
        public Nullable<decimal> IssueArtLocation_PK { get; set; }
        public string IssueLocation { get; set; }
        public Nullable<decimal> PcsPerCarton { get; set; }
        public Nullable<bool> InspStatus { get; set; }
    }
}