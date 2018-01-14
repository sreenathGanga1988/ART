using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Shipping.ViewModel
{
    public class ImportViewModel
    {
        
        public String AtcNum { get; set; }
        public Boolean IsSelected { get; set; }
        public String RMNum { get; set; }
        public String Description { get; set; }
        public String ItemColor { get; set; }
        public String ItemSize { get; set; }
        public String SupplierColor { get; set; }
        public String SupplierSize { get; set; }
        public String UomCode { get; set; }
        public String PONum { get; set; }
        public String DocNum { get; set; }
        public String Qty { get; set; }
        public String Donumber { get; set; }
        public String AddedBy { get; set; }
        public String AddedDate { get; set; }
        public String DocAddedBy { get; set; }
        public String DocAddedDate { get; set; }
        public String SupplierName { get; set; }
        public String ImpDocumentNUm { get; set; }
        public String Location_PK { get; set; }
        public String ShippingDet_PK { get; set; }
        public String Locationname { get; set; }
        public String DocDet_Pk { get; set; }
        public String isReceived { get; set; }
        
    }
    public class ImportViewModelMaster
    {
     
        public ArtWebApp.DataModels.ShippingDocumentMaster shippingDocument { get; set; }

        public List<ImportViewModel> ImportViewModels { get; set; }

        public String Location_PK { get; set; }

        public int  ID { get; set; }

        public Decimal  RemainingCtn { get; set; }

    }
}