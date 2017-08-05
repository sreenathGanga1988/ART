using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.CuttingMVC.Models
{
    public class ApprovelaysheetModel
    {

      

        [Display(Name = "ID")]
        public Decimal LaySheetDet_PK { get; set; }
        public Boolean IsSelected { get; set; }
        public Decimal LaySheet_PK { get; set; }

        [Display(Name = "Lay#")]
        public String LayCutNum { get; set; }
        [Display(Name = "Roll#")]
        public String RollNum { get; set; }
        [Display(Name = "No of Plies")]
        public Decimal NoOfPlies { get; set; }
        [Display(Name = "Fab Used")]
        public Decimal FabUtilized { get; set; }
        [Display(Name = "EndBit")]
        public Decimal EndBit { get; set; }
        [Display(Name = "Balance")]
        public Decimal BalToCut { get; set; }
        [Display(Name = "Excess/Short")]
        public Decimal ExcessOrShort { get; set; }
        [Display(Name = "Recuttable")]
        public String IsRecuttable { get; set; }
        [Display(Name = "RollID")]
        public Decimal Roll_PK { get; set; }
        [Display(Name = "Shade")]
        public String ShadeGroup { get; set; }
        [Display(Name = "Shrinkage")]
        public String ShrinkageGroup { get; set; }
        [Display(Name = "Width")]
        public String WidthGroup { get; set; }
        [Display(Name = "MArkerType")]
        public String MarkerType { get; set; }





    }

    public class LaySheetShortageViewModel
    {

       
      
     
      
        public IEnumerable<SelectListItem> ActOptions { get; set; }
        public IEnumerable<SelectListItem> OurStyleOptions { get; set; }
        public IEnumerable<SelectListItem> LaySheetOptions { get; set; }

     
        [Display(Name = "ID")]
        public Decimal ShortageID { get; set; }
        public int Location_pk { get; set; }
        [Display(Name = "Atcc#")]
        [Required(ErrorMessage = "Please select a ....")]
        public int? AtcID { get; set; }

        [Display(Name = "Fab#")]
        [Required(ErrorMessage = "Please select a Fabric")]
        public int? SkuID { get; set; }

        [Display(Name = "OurStyle#")]
        [Required(ErrorMessage = "Please select a ....")]
        public IEnumerable<int> OurStyleID { get; set; }

      


        [Display(Name = "LaySheet#")]
        [Required(ErrorMessage = "Please select a ....")]
        public IEnumerable<int> LaySheetID { get; set; }


        [Display(Name = " Against EndBIT")]
        public Boolean IsEndBIT { get; set; }
        [Display(Name = " Against LayShortage")]
        public Boolean IsLayShortage { get; set; }


        public SelectList AtcList { get; set; }

        public SelectList OurStyleList { get; set; }

        public SelectList SkuList { get; set; }

        public List<ApprovelaysheetModel> RollDetails { get; set; }


        public ApprovelaysheetModel approvelaysheetModel { get; set; }


        public DateTime AddedDate { get; set; }
        public String AddedBy { get; set; }
        public String Type { get; set; }
    }
}