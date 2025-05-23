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
    
    public partial class MCRDetail
    {
        public decimal McrDetails_pk { get; set; }
        public Nullable<decimal> Atcid { get; set; }
        public Nullable<decimal> InventoryItem_pk { get; set; }
        public Nullable<decimal> Location_pk { get; set; }
        public Nullable<decimal> ReceivedQty { get; set; }
        public Nullable<decimal> DeliveredQty { get; set; }
        public Nullable<decimal> Onhandqty { get; set; }
        public Nullable<decimal> PhysicalQty { get; set; }
        public Nullable<decimal> DiffQty { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string Addedby { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string RMNum { get; set; }
        public string Description { get; set; }
        public string ItemColor { get; set; }
        public string SupplierColor { get; set; }
        public string UOM { get; set; }
        public string type { get; set; }
        public Nullable<decimal> ActualCU_Rate { get; set; }
        public Nullable<decimal> CU_Rate { get; set; }
        public Nullable<decimal> Template_pk { get; set; }
        public Nullable<decimal> Skudet_pk { get; set; }
        public Nullable<decimal> Mcr_pk { get; set; }
        public string IsApproved { get; set; }
        public string IsConfirm { get; set; }
        public string ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<decimal> ActualReceive { get; set; }
        public Nullable<decimal> ActualDiffQty { get; set; }
        public string Packages { get; set; }
        public string AlterUOM { get; set; }
        public Nullable<decimal> AlterUOM_qty { get; set; }
    }
}
