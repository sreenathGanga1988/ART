using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.ProductionMVC.Viewmodel
{
    public class AsqTrackerModel
    {
        public DataTable AsqData { get; set; }
    }

    public class ReportDataModel
    {
        public string ReportName { get; set; }

        public DataTable AsqData { get; set; }
    }
}