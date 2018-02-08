using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.CuttingMVC.Models
{
    public class RejectionPanelViewModal
    {
        public IEnumerable<SelectListItem> ActOptions { get; set; }
        public IEnumerable<SelectListItem> OurStyleOptions { get; set; }
        public IEnumerable<SelectListItem> LocationoOtions { get; set; }

        [Display(Name = "Atcc#")]
        [Required(ErrorMessage = "Please select a Atc")]
        public int? AtcID { get; set; }

        [Display(Name = "OurStyle#")]
        [Required(ErrorMessage = "Please select a ....")]
        public int OurStyleID { get; set; }


        [Display(Name = "Location#")]
        [Required(ErrorMessage = "Please select a ....")]
        public int LocationID { get; set; }


        public SelectList AtcList { get; set; }

        public SelectList OurStyleList { get; set; }

        public SelectList LocaionList { get; set; }


        public List<FabreqDetails> FabreqDetails { get; set; }

        public FabreqDetails FabreqDetailsModdel { get; set; }

        public DateTime AddedDate { get; set; }
        public String AddedBy { get; set; }
        public String Type { get; set; }
    }


    public class FabreqDetails
    {
        public Decimal? RejFabPanelReqID { get; set; }
        public String Fabreqno { get; set; }
        public DateTime? Reqdate { get; set; }
        public String DepartmentName { get; set; }
        public Decimal ReqQty { get; set; }
        public String ColorName { get; set; }
        public String OurStyle { get; set; }
        public String LocationName { get; set; }
        public Decimal Allowedfabric { get; set; }
        public Boolean IsSelected { get; set; }
     

    }
}