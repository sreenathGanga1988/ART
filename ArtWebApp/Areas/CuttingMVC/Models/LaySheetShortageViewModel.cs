using ArtWebApp.DataModels;
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


        [Display(Name = "Cut#")]
        [Required(ErrorMessage = "Please select a Cut")]
        public IEnumerable<int> Cutid { get; set; }


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

        public SelectList Cutorderlist { get; set; }

        public List<ApprovelaysheetModel> RollDetails { get; set; }


        public ApprovelaysheetModel approvelaysheetModel { get; set; }


        public DateTime AddedDate { get; set; }
        public String AddedBy { get; set; }
        public String Type { get; set; }
    }




    public  class LayShortageCutorderAdjustmentViewModal
    {
            


        public decimal LayShortageCutorderAdjustmentViewModalID { get; set; }

        [Display(Name = "Request Qty#")]
        public Nullable<decimal> RequestQty { get; set; }

        [Display(Name = "Allocated Qty#")]
        public Nullable<decimal> AllocatedQty { get; set; }

        [Display(Name = "Balance Qty To allocate#")]
        public Nullable<decimal> BalanceQty { get; set; }

        [Display(Name = "Cut Order Qty #")]
        public Nullable<decimal> CutOrderQty { get; set; }
        [Display(Name = "New Qty to Adjust#")]
        public Nullable<decimal> ToAddQty { get; set; }
  

        public Nullable<System.DateTime> AddedDate { get; set; }
        public string AddedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }


        [Display(Name = "Atcc#")]
        [Required(ErrorMessage = "Please select a Atc")]
        public int? AtcID { get; set; }

        public SelectList AtcList { get; set; }

        [Display(Name = "LayShortageReq#")]
        [Required(ErrorMessage = "Please select a LayShortageReq")]
        public int? LayShortageMasterID { get; set; }
        public SelectList LayShortageMasterreqlist { get; set; }

        [Display(Name = "CutOrder#")]
        [Required(ErrorMessage = "Please select a CutID")]
        public int? CutID { get; set; }
        public SelectList Cutorderlist { get; set; }



        public string MarkerType { get; set; }

        public string Shrinkage { get; set; }

        public string CutWidth { get; set; }

        public string DeliveredQty { get; set; }

        public string CutQty { get; set; }


    }

    public class RejectionCutorderAdjustmentViewModal
    {



        public decimal LayShortageCutorderAdjustmentViewModalID { get; set; }

        [Display(Name = "Request Qty#")]
        public Nullable<decimal> RequestQty { get; set; }

        [Display(Name = "Allocated Qty#")]
        public Nullable<decimal> AllocatedQty { get; set; }

        [Display(Name = "Balance Qty To allocate#")]
        public Nullable<decimal> BalanceQty { get; set; }

        [Display(Name = "Cut Order Qty #")]
        public Nullable<decimal> CutOrderQty { get; set; }
        [Display(Name = "New Qty to Adjust#")]
        public Nullable<decimal> ToAddQty { get; set; }


        public Nullable<System.DateTime> AddedDate { get; set; }
        public string AddedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }


        [Display(Name = "Atcc#")]
        [Required(ErrorMessage = "Please select a Atc")]
        public int? AtcID { get; set; }

        public SelectList AtcList { get; set; }

        [Display(Name = "LayShortageReq#")]
        [Required(ErrorMessage = "Please select a rejctionID")]
        public int? LayShortageMasterID { get; set; }
        public SelectList LayShortageMasterreqlist { get; set; }

        [Display(Name = "CutOrder#")]
        [Required(ErrorMessage = "Please select a CutID")]
        public int? CutID { get; set; }
        public SelectList Cutorderlist { get; set; }



        public string MarkerType { get; set; }

        public string Shrinkage { get; set; }

        public string CutWidth { get; set; }

        public string DeliveredQty { get; set; }

        public string CutQty { get; set; }


    }



}