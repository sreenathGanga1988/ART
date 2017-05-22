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
    
    public partial class InventorySalesMaster
    {
        public InventorySalesMaster()
        {
            this.InventorySalesDetails = new HashSet<InventorySalesDetail>();
        }
    
        public decimal SalesDO_PK { get; set; }
        public string SalesDONum { get; set; }
        public Nullable<System.DateTime> SalesDate { get; set; }
        public Nullable<decimal> FromLocation_PK { get; set; }
        public Nullable<decimal> ToLocation_PK { get; set; }
        public Nullable<System.DateTime> SalesDODate { get; set; }
        public string ContainerNumber { get; set; }
        public Nullable<decimal> BoeNum { get; set; }
        public Nullable<decimal> Deliverymethod_Pk { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string DoType { get; set; }
        public string ISApproved { get; set; }
        public string ApprovedBY { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string IsReceived { get; set; }
        public string IsDebited { get; set; }
    
        public virtual ICollection<InventorySalesDetail> InventorySalesDetails { get; set; }
        public virtual LocationMaster LocationMaster { get; set; }
    }
}