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
    
    public partial class GroupDependantItem
    {
        public decimal GDItem_PK { get; set; }
        public decimal Sku_PK { get; set; }
        public Nullable<decimal> POPackID { get; set; }
        public Nullable<decimal> OurStyleID { get; set; }
        public string IsDepenant { get; set; }
    
        public virtual PoPackMaster PoPackMaster { get; set; }
        public virtual SkuRawMaterialMaster SkuRawMaterialMaster { get; set; }
    }
}