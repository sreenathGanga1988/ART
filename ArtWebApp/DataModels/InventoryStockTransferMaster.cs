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
    
    public partial class InventoryStockTransferMaster
    {
        public InventoryStockTransferMaster()
        {
            this.InventoryStockTransferDetails = new HashSet<InventoryStockTransferDetail>();
        }
    
        public decimal Gtrans_Pk { get; set; }
        public string TransNum { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string IsApproved { get; set; }
        public string IsDeleted { get; set; }
        public string AddedBy { get; set; }
        public string DeletedBy { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.DateTime> Approveddate { get; set; }
        public Nullable<decimal> Location_PK { get; set; }
    
        public virtual ICollection<InventoryStockTransferDetail> InventoryStockTransferDetails { get; set; }
    }
}