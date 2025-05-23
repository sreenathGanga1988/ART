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
    
    public partial class PrePackMaster
    {
        public decimal PrePackId { get; set; }
        public string PrePacknum { get; set; }
        public Nullable<decimal> AtcId { get; set; }
        public Nullable<decimal> PoPackId { get; set; }
        public string BuyerPO { get; set; }
        public string PackingInstruction { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<System.DateTime> Inhousedate { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string AddedBy { get; set; }
        public Nullable<decimal> ChannelID { get; set; }
        public Nullable<decimal> BuyerDestination_PK { get; set; }
        public string PoGroup { get; set; }
        public string TagGroup { get; set; }
        public string SeasonName { get; set; }
        public string IsCutable { get; set; }
        public Nullable<System.DateTime> FirstDeliveryDate { get; set; }
        public Nullable<System.DateTime> HandoverDate { get; set; }
        public Nullable<decimal> ExpectedLocation_PK { get; set; }
        public string IsDeleted { get; set; }
        public string IsShuffulable { get; set; }
        public Nullable<System.DateTime> SalesHandoverDate { get; set; }
    
        public virtual AtcMaster AtcMaster { get; set; }
        public virtual BuyerDestinationMaster BuyerDestinationMaster { get; set; }
        public virtual ChannelMaster ChannelMaster { get; set; }
    }
}
