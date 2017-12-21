using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.CuttingMVC.ViewModel
{
    public class FCRViewModel
    {
        public FcrMasterData fcrMasterData { get; set; }

        public DataTable CutData { get; set; }
        public DataTable LayshortagereqData { get; set; }
        public DataTable RejectionReqData { get; set; }


        public String TotalFabQty { get; set; }
        public String TotalPlies { get; set; }
        public String TotalLayedQty { get; set; }
        public String TotalFabricLayed { get; set; }
        public String TotalEndbit { get; set; }
        public String TotaCutorderQty { get; set; }

        public String ActualFCRConsumtion { get; set; }
        public String OverConsumed { get; set; }

       

    }

    public class FcrMasterData
    {
        public String Factory { get; set; }
        public String Season { get; set; }
        public String Color { get; set; }
        public String Atc { get; set; }
        public String Style { get; set; }
        public String Consumption { get; set; }
        public String ApprovedConsumption { get; set; }
        public String Order { get; set; }
        public String Fabric { get; set; }
        public String Buyer { get; set; }
        public String Fabdescription { get; set; }
        public String Dateofproduction { get; set; }
        public String GiventoFactory { get; set; }
        public String GivenBackToStore { get; set; }
        public String TotalGiven { get; set; }
        public String ToBeonLocation { get; set; }
        public String ToTransfer { get; set; }
        public String MarkMissedQty { get; set; }

        public Decimal SkuDetPK { get; set; }
        public Decimal OurStyleID { get; set; }
        public int LocPK { get; set; }
        public Decimal MissingQty { get; set; }

    }
}