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
    
    public partial class LayShortageCutorderAdjustment
    {
        public decimal LayShortageCutorderAdjustmentID { get; set; }
        public decimal CutID { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> LayShortageMasterID { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string AddedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual CutOrderMaster CutOrderMaster { get; set; }
        public virtual LayShortageReqMaster LayShortageReqMaster { get; set; }
    }
}
