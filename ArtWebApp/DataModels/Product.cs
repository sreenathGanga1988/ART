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
    
    public partial class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountForLocation { get; set; }
        public decimal MinimumSPForLocation { get; set; }
        public bool IsAvailable { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public Nullable<bool> IsRateChangable { get; set; }
        public Nullable<int> OdooProductId { get; set; }
    
        public virtual Category Category { get; set; }
    }
}
