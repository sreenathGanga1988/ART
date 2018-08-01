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
        public IEnumerable<SelectListItem> OurslyeList { get; set; }
        public SelectList AtcList { get; set; }
        public string location { get; set; }
        public List<ProductionTNAVModel> ProductionTNAVModelList { get; set; }
        public ProductionTNAVModel productionTNAVModel { get; set; }
        public TnaUserRight tnaUserRight { get; set; }
        public List<TnaUserRight> tnaUserRights { get; set; }
    }

    public class ProductionTNAATCList
    {
        public string ourstyle { get; set; }
    }

    public class ProductionTNAVModel
    {



        public int? AtcID { get; set; }
        public Nullable<decimal> ProductionTNID { get; set; }
        public Nullable<decimal> OurStyleID { get; set; }
        public Nullable<decimal> Location_PK { get; set; }
        public Nullable<decimal> OrderQty { get; set; }
        public System.Boolean IsSelected { get; set; }
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



        public int  User_IDdFINALMARKER { get; set; }
        public int  User_IDFC1 { get; set; }
        public int  User_IDPPMEETING { get; set; }
        public int  User_IDSIZESET { get; set; }
        public int  User_IDSEWINGTRIM { get; set; }
        public int  User_IDBULKFABRIC { get; set; }
        public int  User_IDRECEIPTOFORGINALDOCUMENT { get; set; }
        public int  User_IDGRADDEDPATTERN { get; set; }
        public int  User_IDSAMPLEYARDAGES { get; set; }
        public int  User_IDPPAPPROVAL { get; set; }
        public int  User_IDPPSUBMISSIONDATEMERCHANT { get; set; }
        public int  User_IDINPUT { get; set; }
        public int  User_IDPACKINGTRIMS { get; set; }
        public int  User_IDFACTORYPLANNEDPCD { get; set; }
        public int  User_IDSYSTEMFILES { get; set; }
        public int  User_IDSHRINKAGE { get; set; }


        public String UsrDdFINALMARKER { get; set; }
        public String UsrDFC1 { get; set; }
        public String UsrDPPMEETING { get; set; }
        public String UsrDSIZESET { get; set; }
        public String UsrDSEWINGTRIM { get; set; }
        public String UsrDBULKFABRIC { get; set; }
        public String UsrDRECEIPTOFORGINALDOCUMENT { get; set; }
        public String UsrDGRADDEDPATTERN { get; set; }
        public String UsrDSAMPLEYARDAGES { get; set; }
        public String UsrDPPAPPROVAL { get; set; }
        public String UsrDPPSUBMISSIONDATEMERCHANT { get; set; }
        public String UsrDINPUT { get; set; }
        public String UsrDPACKINGTRIMS { get; set; }
        public String UsrDFACTORYPLANNEDPCD { get; set; }
        public String UsrDSYSTEMFILES { get; set; }
        public String UsrDSHRINKAGE { get; set; }
               
              
        public String status_dFINALMARKER { get; set; }
        public String status_FC1 { get; set; }
        public String status_PPMEETING { get; set; }
        public String status_SIZESET { get; set; }
        public String status_SEWINGTRIM { get; set; }
        public String status_BULKFABRIC { get; set; }
        public String status_RECEIPTOFORGINALDOCUMENT { get; set; }
        public String status_GRADDEDPATTERN { get; set; }
        public String status_SAMPLEYARDAGES { get; set; }
        public String status_PPAPPROVAL { get; set; }
        public String status_PPSUBMISSIONDATEMERCHANT { get; set; }
        public String status_INPUT { get; set; }
        public String status_PACKINGTRIMS { get; set; }
        public String status_FACTORYPLANNEDPCD { get; set; }
        public String status_SYSTEMFILES { get; set; }
        public String status_SHRINKAGE { get; set; }



        public int Alert_DdFINALMARKER { get; set; }
        public int Alert_DFC1 { get; set; }
        public int Alert_DPPMEETING { get; set; }
        public int Alert_DSIZESET { get; set; }
        public int Alert_DSEWINGTRIM { get; set; }
        public int Alert_DBULKFABRIC { get; set; }
        public int Alert_DRECEIPTOFORGINALDOCUMENT { get; set; }
        public int Alert_DGRADDEDPATTERN { get; set; }
        public int Alert_DSAMPLEYARDAGES { get; set; }
        public int Alert_DPPAPPROVAL { get; set; }
        public int Alert_DPPSUBMISSIONDATEMERCHANT { get; set; }
        public int Alert_DINPUT { get; set; }
        public int Alert_DPACKINGTRIMS { get; set; }
        public int Alert_DFACTORYPLANNEDPCD { get; set; }
        public int Alert_DSYSTEMFILES { get; set; }
        public int Alert_DSHRINKAGE { get; set; }

        public int DaysFINALMARKER { get; set; }
        public int DaysFC1 { get; set; }
        public int DaysPPMEETING { get; set; }
        public int DaysSIZESET { get; set; }
        public int DaysSEWINGTRIM { get; set; }
        public int DaysBULKFABRIC { get; set; }
        public int DaysRECEIPTOFORGINALDOCUMENT { get; set; }
        public int DaysGRADDEDPATTERN { get; set; }
        public int DaysSAMPLEYARDAGES { get; set; }
        public int DaysPPAPPROVAL { get; set; }
        public int DaysPPSUBMISSIONDATEMERCHANT { get; set; }
        public int DaysINPUT { get; set; }
        public int DaysPACKINGTRIMS { get; set; }
        public int DaysFACTORYPLANNEDPCD { get; set; }
        public int DaysSYSTEMFILES { get; set; }
        public int DaysSHRINKAGE { get; set; }



        public String OurStyle { get; set; }
        public String BuyerStyle { get; set; }
        public String ShortName { get; set; }
        public String AtcNum { get; set; }


        

       
    }
}