using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel
{
    public class AtcClosingModel
    {

        public Boolean IsSelected { get; set; }
        public String AtcNum { get; set; }
        public String BuyerName { get; set; }
        public String NoofStyles { get; set; }
        public String IsCompleted { get; set; }
        public String IsClosed { get; set; }
        public String Description { get; set; }
        public String ProjectionQty { get; set; }
        public int AtcId { get; set; }

     
    }


    public class AtcClosingModelList
    {
        public List<AtcClosingModel> atcClosingModels { get; set; }

        public String Month { get; set; }

        public String Type { get; set; }

        public String AddedBy { get; set; }

        public DateTime Addeddate { get; set; }

        public DataTable ClosedAtc { get; set; }
        public  List<SelectListItem> MonthList = new List<SelectListItem>()
    {   new SelectListItem() {Text="TotalAlloc", Value="TotalAlloc"},
        new SelectListItem() {Text="January", Value="January"},
        new SelectListItem() { Text="February", Value="February"},
        new SelectListItem() { Text="March", Value="March"},
        new SelectListItem() { Text="April", Value="April"},
        new SelectListItem() { Text="May", Value="May"},
        new SelectListItem() { Text="June", Value="June"},
        new SelectListItem() { Text="July", Value="July"},
        new SelectListItem() { Text="August", Value="August"},
        new SelectListItem() { Text="September", Value="September"},
        new SelectListItem() { Text="October", Value="October"},
         new SelectListItem() { Text="November", Value="November"},
        new SelectListItem() { Text="December", Value="December"},

    };
    }

    public class AtcPerformance
    {
        
        public String AtcNum { get; set; }
        public String Generateddate { get; set; }
        public DataTable ATCDatatable { get; set; }
        public DataTable ATCFabricDatatable { get; set; }
        public DataTable ATCTrimDatatable { get; set; }
        public DataTable InventoryMisplace { get; set; }
        public DataTable charges{ get; set; }
        public DataTable Creditnotes{ get; set; }
        public DataTable atcpl{ get; set; }

    }

 }