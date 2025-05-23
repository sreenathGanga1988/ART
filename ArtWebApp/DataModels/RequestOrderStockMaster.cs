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
    
    public partial class RequestOrderStockMaster
    {
        public RequestOrderStockMaster()
        {
            this.RequestOrderStockDetails = new HashSet<RequestOrderStockDetail>();
        }
    
        public decimal SRO_Pk { get; set; }
        public string RONum { get; set; }
        public Nullable<decimal> AtcID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string IsApproved { get; set; }
        public string IsDeleted { get; set; }
        public string AddedBy { get; set; }
        public string DeletedBy { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.DateTime> Approveddate { get; set; }
        public Nullable<decimal> Location_PK { get; set; }
        public string ROType { get; set; }
        public Nullable<decimal> PO_PK { get; set; }
        public string IsForwarded { get; set; }
        public string ForwardedBY { get; set; }
        public string Iscompleted { get; set; }
    
        public virtual AtcMaster AtcMaster { get; set; }
        public virtual LocationMaster LocationMaster { get; set; }
        public virtual ICollection<RequestOrderStockDetail> RequestOrderStockDetails { get; set; }
    }
}
