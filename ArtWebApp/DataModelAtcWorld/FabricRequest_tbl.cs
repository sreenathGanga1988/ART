//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArtWebApp.DataModelAtcWorld
{
    using System;
    using System.Collections.Generic;
    
    public partial class FabricRequest_tbl
    {
        public decimal Fabreqid { get; set; }
        public string Fabreqno { get; set; }
        public Nullable<System.DateTime> Reqdate { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<decimal> PoPack_Detail_PK { get; set; }
        public Nullable<decimal> ReqQty { get; set; }
        public Nullable<decimal> User_PK { get; set; }
        public Nullable<decimal> Location_PK { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string HostName { get; set; }
        public string MachineIP { get; set; }
        public Nullable<bool> IsUploaded { get; set; }
    
        public virtual LocationMaster_tbl LocationMaster_tbl { get; set; }
    }
}