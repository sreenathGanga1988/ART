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
    
    public partial class PcdAlertMaster
    {
        public int PcdAlert_Pk { get; set; }
        public Nullable<int> Line_no { get; set; }
        public Nullable<decimal> Atc_id { get; set; }
        public Nullable<System.DateTime> Cut_Start_date { get; set; }
        public string Approval_status { get; set; }
        public string Sewing_Material_Issue { get; set; }
        public string BO_remarks_sewing { get; set; }
        public string BO_remarks_by { get; set; }
        public string Sewing_Bo_plan_accept { get; set; }
        public string Sewing_action { get; set; }
        public string Packing_Material_Issue { get; set; }
        public string BO_remarks_Packing { get; set; }
        public string Packing_bo_plan_accept { get; set; }
        public string Packing_action { get; set; }
        public Nullable<decimal> Location_pk { get; set; }
        public Nullable<decimal> WeekNum { get; set; }
        public Nullable<System.DateTime> Addeddate { get; set; }
        public string Addedby { get; set; }
        public string PackingAddedBy { get; set; }
        public Nullable<System.DateTime> PackingAddedDate { get; set; }
        public Nullable<decimal> OldPcdAlert_Pk { get; set; }
        public string IsActive { get; set; }
        public string type { get; set; }
    
        public virtual AtcMaster AtcMaster { get; set; }
        public virtual LocationMaster LocationMaster { get; set; }
    }
}
