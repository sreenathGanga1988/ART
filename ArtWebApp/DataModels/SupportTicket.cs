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
    
    public partial class SupportTicket
    {
        public decimal Support_pk { get; set; }
        public string Supportnum { get; set; }
        public string SupportTittle { get; set; }
        public string SupportDescription { get; set; }
        public string Priority { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<decimal> Location_pk { get; set; }
        public string Status { get; set; }
        public string IsCompleted { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public string Remark { get; set; }
        public string CompletedBy { get; set; }
    }
}
