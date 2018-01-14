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
        public TnaUserRight tnaUserRight { get; set; }
        
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
        public String SYSTEMFILES { get; set; }
        public String SHRINKAGE { get; set; }
        

        public int IdFINALMARKER { get; set; }
        public int IdFC1 { get; set; }
        public int IdPPMEETING { get; set; }
        public int IdSIZESET { get; set; }
        public int IdSEWINGTRIM { get; set; }
        public int IdBULKFABRIC { get; set; }
        public int IdRECEIPTOFORGINALDOCUMENT { get; set; }
        public int IdGRADDEDPATTERN { get; set; }
        public int IdSAMPLEYARDAGES { get; set; }
        public int IdPPAPPROVAL { get; set; }
        public int IdPPSUBMISSIONDATEMERCHANT { get; set; }
        public int IdINPUT { get; set; }
        public int IdPACKINGTRIMS { get; set; }
        public int IdFACTORYPLANNEDPCD { get; set; }
        public int IdSYSTEMFILES { get; set; }
        public int IdSHRINKAGE { get; set; }


        public String ActualFINALMARKER { get; set; }
        public String ActualFC1 { get; set; }
        public String ActualPPMEETING { get; set; }
        public String ActualSIZESET { get; set; }
        public String ActualSEWINGTRIM { get; set; }
        public String ActualBULKFABRIC { get; set; }
        public String ActualRECEIPTOFORGINALDOCUMENT { get; set; }
        public String ActualGRADDEDPATTERN { get; set; }
        public String ActualSAMPLEYARDAGES { get; set; }
        public String ActualPPAPPROVAL { get; set; }
        public String ActualPPSUBMISSIONDATEMERCHANT { get; set; }
        public String ActualINPUT { get; set; }
        public String ActualPACKINGTRIMS { get; set; }
        public String ActualFACTORYPLANNEDPCD { get; set; }
        public String ActualSYSTEMFILES { get; set; }
        public String ActualSHRINKAGE { get; set; }

        public String OurStyle { get; set; }
        public String BuyerStyle { get; set; }
        public String ShortName { get; set; }
        public String AtcNum { get; set; }


        

       
    }
}