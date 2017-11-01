using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.MVCTNA.ViewModel
{

    public class ProductionTNAVModelMaster
    {
        [Display(Name = "Atcc#")]
        [Required(ErrorMessage = "Please select Atc")]
        public int? AtcID { get; set; }
        public IEnumerable<SelectListItem> ActOptions { get; set; }
        public SelectList AtcList { get; set; }

        public List<ProductionTNAVModel> ProductionTNAVModelList { get; set; }
        public ProductionTNAVModel productionTNAVModel { get; set; }
    }


    public class ProductionTNAVModel
    {



        public int? AtcID { get; set; }
        public Nullable<decimal> ProductionTNID { get; set; }
        public Nullable<decimal> OurStyleID { get; set; }
        public Nullable<decimal> Location_PK { get; set; }
        public Nullable<decimal> OrderQty { get; set; }
        
        public string LocationName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PCD { get; set; }
        public String FINALMARKER { get; set; }
        public String FC1 { get; set; }
        public String PPMEETING { get; set; }
        public String SIZESET { get; set; }
        public String SEWINGTRIM { get; set; }
        public String BULKFABRIC { get; set; }
        public String RECEIPTOFORGINALDOCUMENT { get; set; }
        public String GRADDEDPATTERN { get; set; }
        public String SAMPLEYARDAGES { get; set; }
        public String PPAPPROVAL { get; set; }
        public String PPSUBMISSIONDATEMERCHANT { get; set; }
        public String INPUT { get; set; }
        public String PACKINGTRIMS { get; set; }
        public String FACTORYPLANNEDPCD { get; set; }








        public String OurStyle { get; set; }
        public String BuyerStyle { get; set; }
        public String ShortName { get; set; }
        public String AtcNum { get; set; }


        

       
    }
}