using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.ProductionMVC.Viewmodel
{
    public class AsqTrackerModel
    {
        public string ReportName { get; set; }
        public DataTable AsqData { get; set; }
    }

    public class ReportDataModel
    {
        public string ReportName { get; set; }

        public DataTable AsqData { get; set; }
    }

    public class PcdAlertModel
    {
        public string WeekNo { get; set; }
        public string LineNo { get; set; }
        public string Atcid{ get; set; }
        public string Atcnum { get; set; }
        public string CutStartDate{ get; set; }
        public string ApprovalStatus { get; set; }
        public string SewingMaterialIssue { get; set; }
        public string BoRemarksSewing { get; set; }
        public string BoPlanSewingAccpet { get; set; }
        public string SewingAction { get; set; }
        public string PackingMaterialIssue { get; set; }
        public string BoRemarksPacking { get; set; }
        public string BoPlanPackingAccept { get; set; }
        public string PackingAction { get; set; }
        public string Location_pk { get; set; }
        public string Pcdalert_pk{ get; set; }
        public string OldPcdalert_pk { get; set; }

    }


}