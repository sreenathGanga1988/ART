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
    
    public partial class ATCWorldToArtShipData
    {
        public decimal ArtShipID { get; set; }
        public Nullable<decimal> ArtLocation_PK { get; set; }
        public Nullable<decimal> ATCWorldLocation_PK { get; set; }
        public string LocationName { get; set; }
        public string PackingIns { get; set; }
        public string SDONo { get; set; }
        public string Mode { get; set; }
        public Nullable<decimal> PLID { get; set; }
        public Nullable<System.DateTime> ShipmentDate { get; set; }
        public Nullable<System.DateTime> HandOverDate { get; set; }
        public Nullable<decimal> BuyerID { get; set; }
        public Nullable<decimal> Atc_Id { get; set; }
        public Nullable<decimal> Season_PK { get; set; }
        public Nullable<decimal> OurStyleId { get; set; }
        public string BuyerStyle { get; set; }
        public Nullable<decimal> CategoryID { get; set; }
        public Nullable<decimal> POPackID { get; set; }
        public Nullable<decimal> PoPack_Detail_PK { get; set; }
        public Nullable<decimal> ColorID { get; set; }
        public Nullable<decimal> SizeID { get; set; }
        public Nullable<decimal> Totalcarton { get; set; }
        public Nullable<decimal> ShipQty { get; set; }
        public string IsBooked { get; set; }
        public Nullable<System.DateTime> BookedDate { get; set; }
        public string BookedBy { get; set; }
        public string Country { get; set; }
        public Nullable<decimal> ProductionArtLocation { get; set; }
        public Nullable<decimal> CMPerPc { get; set; }
        public string ContainerNo { get; set; }
    }
}
