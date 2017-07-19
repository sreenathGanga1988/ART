using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.ArtMVC.Models.ViewModel
{


    public class IPOListViewModel
    {
        [Display(Name = "IPO")]
        public int Poid { get; set; }
        public List<IPOViewModel> IPOMasterlist { get; set; } = null;
    }


    public class IPOViewModel
    {

        public String OODoLocation { get; set; }
        public String IPONumber { get; set; }
        public Decimal? Poid { get; set; }

        public List<IPODetailsViewModel> IPODetailsViewModellist
        {
            get
            {
                return iPODetailsViewModellist;
            }

            set
            {
                iPODetailsViewModellist = value;
            }
        }

        private List<IPODetailsViewModel> iPODetailsViewModellist;
    }

    public class IPODetailsViewModel
    {
        public String ItemDescription { get; set; }
        public Decimal? POLIneID { get; set; }

        public Decimal? RequiredQty { get; set; }

        public String OODOUOM { get; set; }
        public List<SpoDetails> SpoDetailsList { get; set; }

    }

    public class SpoDetails
    {
        public String SPOnum { get; set; }

        public Decimal? Spodet_pk { get; set; }

        public Decimal? Qty { get; set; }

        public String AddedDate { get; set; }

        public String Suppplier { get; set; }

        public String SPOUOM { get; set; }

        public List<SMRNDetails> SMRNDetailsList { get; set; }
    }



    public class SMRNDetails
    {
        public String SMRNNUM { get; set; }

        public Decimal? Qty { get; set; }


        public Decimal? smrndet_pk { get; set; }
        public Decimal? ExtraQty { get; set; }

        public String SPOnum { get; set; }

        public DateTime? SMRNDate { get; set; }
        public List<SDODetails> SDODetailsList { get; set; }
        public String invoice { get; set; }
    }

    public class SDODetails
    {
        public String SDONUM { get; set; }

        public Decimal? Qty { get; set; }

        public String AddedBy { get; set; }

        public DateTime? AddedDate { get; set; }
        public String Doc { get; set; }

        public DateTime? SMRNDate { get; set; }

    }
}