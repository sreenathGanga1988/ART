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
    
    public partial class SDocDetail
    {
        public decimal SDocDet_Pk { get; set; }
        public Nullable<decimal> SDoc_Pk { get; set; }
        public Nullable<decimal> SPODet_Pk { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<System.DateTime> Eta { get; set; }
        public string Donumber { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<decimal> ExtraQty { get; set; }
    }
}