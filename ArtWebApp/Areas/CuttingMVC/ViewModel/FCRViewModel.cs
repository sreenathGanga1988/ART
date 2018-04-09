using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.CuttingMVC.ViewModel
{

    public class FullFCRModelData
    {

        public List<FCRViewModel> FCRViewModelDatalist { get; set; }
        public Decimal SkuDetPK { get; set; }
        public Decimal LocationPk { get; set; }

        public Decimal GiventoFactory { get; set; }
        public Decimal GivenBackToStore { get; set; }
        public Decimal ToBeonLocation { get; set; }
        public Decimal TotalFabricLayed { get; set; }
        public Decimal TotalShortage { get; set; }
        public Decimal MarkMissedQty { get; set; }
        public Decimal TotalBalanceQty { get; set; }
        public Decimal MissingQty { get; set; }
        public Decimal RejectionrecutQty { get; set; }
        public String Isclosebuttonvisible { get; set; }

    }




    public class FCRViewModel
    {
        public FcrMasterData fcrMasterData { get; set; }

        public DataTable CutData { get; set; }
        public DataTable LayshortagereqData { get; set; }
        public DataTable RejectionReqData { get; set; }
        public DataTable SamplingCutOrderData { get; set; }
        public DataTable DeliveryData { get; set; }

        


        public String TotalFabQty { get; set; }
        public String TotalPlies { get; set; }
        public String TotalLayedQty { get; set; }
        public String TotalFabricLayed { get; set; }
        public String TotalEndbit { get; set; }
        public String TotaCutorderQty { get; set; }
        public String Rejectionrecut { get; set; }
        public String TotalBalanceQty { get; set; }

        public String ActualFCRConsumtion { get; set; }
        public String OverConsumed { get; set; }
        public String OverConsumedPer { get; set; }
        


        public String TotalShortage { get; set; }
        public String TotalNonusableEndbit { get; set; }
        public String TotalSampleYardage { get; set; }

        public String Isclosebuttonvisible { get; set; }
        public String IsClosed { get; set; }

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


    public class FCRSummary
    {
        public String Factory { get; set; }
        public String AtcNum { get; set; }
        public String Generateddate { get; set; }
        public DataTable FCRDatatable { get; set; }
    }
    }