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
    
    public partial class SkuRawMaterialMaster
    {
        public SkuRawMaterialMaster()
        {
            this.GroupDependantItems = new HashSet<GroupDependantItem>();
            this.SkuRawmaterialDetails = new HashSet<SkuRawmaterialDetail>();
            this.StyleCostingDetails = new HashSet<StyleCostingDetail>();
        }
    
        public decimal Sku_Pk { get; set; }
        public Nullable<decimal> Atc_id { get; set; }
        public Nullable<decimal> AtcRaw_PK { get; set; }
        public Nullable<decimal> Template_pk { get; set; }
        public string RMNum { get; set; }
        public string Composition { get; set; }
        public string Construction { get; set; }
        public Nullable<decimal> OrderMulti { get; set; }
        public Nullable<decimal> OrderMin { get; set; }
        public string Weight { get; set; }
        public string Width { get; set; }
        public Nullable<decimal> Uom_PK { get; set; }
        public Nullable<decimal> AltUom_pk { get; set; }
        public string isCommon { get; set; }
        public string IsCD { get; set; }
        public string IsSD { get; set; }
        public Nullable<decimal> Currency_pk { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> WastagePercentage { get; set; }
        public string IsGD { get; set; }
        public string IsTD { get; set; }
        public string IsPD { get; set; }
        public string BodyPartName { get; set; }
    
        public virtual AtcRawMaterialMaster AtcRawMaterialMaster { get; set; }
        public virtual ICollection<GroupDependantItem> GroupDependantItems { get; set; }
        public virtual ICollection<SkuRawmaterialDetail> SkuRawmaterialDetails { get; set; }
        public virtual UOMMaster UOMMaster { get; set; }
        public virtual ICollection<StyleCostingDetail> StyleCostingDetails { get; set; }
        public virtual AtcMaster AtcMaster { get; set; }
    }
}