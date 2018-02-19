using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Inventory.ViewModel
{
    public class InventoryReportmodel
    {
    }
    public class AsqTrackerModel
    {
        public DataTable AsqData { get; set; }
    }

    public class InventoryReportDataModel
    {
        public string ReportName { get; set; }

        public DataTable AsqData { get; set; }
    }
}