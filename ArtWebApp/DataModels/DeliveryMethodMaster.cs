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
    
    public partial class DeliveryMethodMaster
    {
        public DeliveryMethodMaster()
        {
            this.DeliveryOrderStockMasters = new HashSet<DeliveryOrderStockMaster>();
            this.DeliveryOrderMasters = new HashSet<DeliveryOrderMaster>();
        }
    
        public decimal Deliverymethod_Pk { get; set; }
        public string DeliveryMethod { get; set; }
    
        public virtual ICollection<DeliveryOrderStockMaster> DeliveryOrderStockMasters { get; set; }
        public virtual ICollection<DeliveryOrderMaster> DeliveryOrderMasters { get; set; }
    }
}
