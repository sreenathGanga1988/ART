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
    
    public partial class DeliveryOrderStockMaster
    {
        public DeliveryOrderStockMaster()
        {
            this.DeliveryStockReceiptMasters = new HashSet<DeliveryStockReceiptMaster>();
            this.StockGoodsInTransits = new HashSet<StockGoodsInTransit>();
        }
    
        public decimal SDO_PK { get; set; }
        public string SDONum { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<decimal> FromLocation_PK { get; set; }
        public Nullable<decimal> ToLocation_PK { get; set; }
        public Nullable<System.DateTime> DODate { get; set; }
        public string ContainerNumber { get; set; }
        public string BoeNum { get; set; }
        public Nullable<decimal> Deliverymethod_Pk { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string DoType { get; set; }
    
        public virtual DeliveryMethodMaster DeliveryMethodMaster { get; set; }
        public virtual DeliveryOrderStockMaster DeliveryOrderStockMaster1 { get; set; }
        public virtual DeliveryOrderStockMaster DeliveryOrderStockMaster2 { get; set; }
        public virtual LocationMaster LocationMaster { get; set; }
        public virtual LocationMaster LocationMaster1 { get; set; }
        public virtual ICollection<DeliveryStockReceiptMaster> DeliveryStockReceiptMasters { get; set; }
        public virtual ICollection<StockGoodsInTransit> StockGoodsInTransits { get; set; }
    }
}