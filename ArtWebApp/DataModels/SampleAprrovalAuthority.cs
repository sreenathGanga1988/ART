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
    
    public partial class SampleAprrovalAuthority
    {
        public SampleAprrovalAuthority()
        {
            this.SampleTypeMasterDummies = new HashSet<SampleTypeMasterDummy>();
        }
    
        public decimal SampleApprovalID { get; set; }
        public string SampleApprover { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    
        public virtual ICollection<SampleTypeMasterDummy> SampleTypeMasterDummies { get; set; }
    }
}