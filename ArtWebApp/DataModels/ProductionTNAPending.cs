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
    
    public partial class ProductionTNAPending
    {
        public int ProductionTNAGraphID { get; set; }
        public string ProductionTNACompID { get; set; }
        public string CompName { get; set; }
        public Nullable<decimal> Whitevalue { get; set; }
        public Nullable<decimal> Red { get; set; }
        public Nullable<decimal> Green { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<decimal> Yellow { get; set; }
        public Nullable<int> OurStyleID { get; set; }
    }
}