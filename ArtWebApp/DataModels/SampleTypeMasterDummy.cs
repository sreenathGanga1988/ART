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
    
    public partial class SampleTypeMasterDummy
    {
        public SampleTypeMasterDummy()
        {
            this.SampCutTicketMasters = new HashSet<SampCutTicketMaster>();
        }
    
        public decimal SampleTypeMasterID { get; set; }
        public string SampleTypeName { get; set; }
        public string Description { get; set; }
        public string CutTicketFrom { get; set; }
        public Nullable<bool> NeedApproval { get; set; }
        public Nullable<decimal> SampleApprovalID { get; set; }
        public Nullable<bool> NeedReApproval { get; set; }
    
        public virtual ICollection<SampCutTicketMaster> SampCutTicketMasters { get; set; }
        public virtual SampleAprrovalAuthority SampleAprrovalAuthority { get; set; }
    }
}
