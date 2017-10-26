using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVCGPO.ViewModal
{
    public class GMRNViewModal
    {
        public int? SupplierPK { get; set; }
        [Display(Name = "MRN#")]
        [Required(ErrorMessage = "Please select a MRN")]
        public int? MrnID { get; set; }

        public SelectList MrnList { get; set; }

        public List <MrnFileUpload> MrnFileUploads { get; set; }
    }

    public class SalesDoModel
    {
        public int? SupplierPK { get; set; }
        [Display(Name = "SDO#")]
        [Required(ErrorMessage = "Please select a SDO")]
        public int? SalesDO_PK { get; set; }

        public SelectList SDOList { get; set; }

        public List<MrnFileUpload> MrnFileUploads { get; set; }
    }
}