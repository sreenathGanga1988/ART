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
    
    public partial class TemplateComposition
    {
        public decimal TemplateCom_Pk { get; set; }
        public Nullable<decimal> Template_Pk { get; set; }
        public string Composition { get; set; }
    
        public virtual Template_Master Template_Master { get; set; }
    }
}
