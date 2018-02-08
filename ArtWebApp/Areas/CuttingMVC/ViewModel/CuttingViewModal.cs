using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.CuttingMVC.ViewModel
{
    public class CuttingViewModal
    {
    }

    public class AtcPerformanceMasterVM
    {
        public DateTime ReportDate { get; set; }
        public AtcmasterData atcmasterData { get; set; }
        public List<AtcStyleShipDetail> atcStyleShipDetails { get; set; }

        public DataTable FabData { get; set; }

        public DataTable TrimData { get; set; }
    }
    public class AtcmasterData
    {
        public String AtcNum { get; set; }
        public String OurStyle { get; set; }
        public String Buyer { get; set; }
        public String Factory { get; set; }
        public String Country { get; set; }
        public DateTime PCD { get; set; }
        public String Stylename { get; set; }
        public String OrderQty { get; set; }
        public DateTime DateofProduction { get; set; }
    }





    public class AtcStyleShipDetail
    {

        public String OurStyle { get; set; }
        public String Color { get; set; }
        public String Fob { get; set; }
        public String OrderQty { get; set; }
        public String OrderValue { get; set; }
        public String ShippedQty { get; set; }
        public String ShippedValue { get; set; }
    }



    public class FCRStatusReportDataModel
    {
        public string ReportName { get; set; }

        public DataTable FabricdataData { get; set; }
    }
}