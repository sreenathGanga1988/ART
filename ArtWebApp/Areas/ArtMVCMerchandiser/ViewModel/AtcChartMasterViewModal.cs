using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel
{
    public class AtcChartMasterViewModal
    {
        public int? AtcID { get; set; }

        public SelectList AtcList { get; set; }

        public SelectList OurStyleList { get; set; }

        public SelectList SkuList { get; set; }

        public SelectList SeasonList { get; set; }

        public SelectList ASQList { get; set; }

        public IEnumerable<int> OurStyleID { get; set; }
        public IEnumerable<int> SkuID { get; set; }
        public IEnumerable<int> POPACKID { get; set; }

        public AtcChartmaster atcChartmaster { get; set; }
        [AllowHtml]
        public string Csv { get; set; }
    }

    public class AtcChartmaster
    {
        public DataTable atcdetail { get; set; }
        public DataTable asqtable { get; set; }
        public DataTable sizedata { get; set; }

        public DataTable BomData { get; set; }
        public DataTable procurementplandata { get; set; }
        public DataTable Inbounddata { get; set; }
        public DataTable Podataofatc { get; set; }
        public DataTable GoodsinTransit { get; set; }
        public DataTable onhandqty { get; set; }

        public DataTable cutorderofatc { get; set; }
        public DataTable recptofAtc { get; set; }
        public DataTable AdnofATC { get; set; }
        public DataTable RemarkofATC { get; set; }
        public DataTable OurStyledata { get; set; }
        





        public List<BomData> BomDataList { get; set; }

    }


    public class BomData
    {
        
        
        public String RMNum { get; set; }
        public String Description { get; set; }
        public String ColorName { get; set; }
        public String SizeName { get; set; }
        public String ItemColor { get; set; }
        public String ItemSize { get; set; }
        public String UnitRate { get; set; }
        public String GarmentQty { get; set; }
        public String Consumption { get; set; }
        public String WastagePercentage { get; set; }
        public String RqdQty { get; set; }
        public String UomCode { get; set; }
        public String PlannedQty { get; set; }
        public String PlannedDetails { get; set; }
        public String balanceToPlan { get; set; }
        public String PoIssuedQty { get; set; }
        public String PODetails { get; set; }
        public String BalanceQty { get; set; }
        public String ADNDetails { get; set; }
        public String ShippingDetails { get; set; }
        public String OnhandDetails { get; set; }
        public String PendingtoRecieve { get; set; }
        public String TransistDetails { get; set; }
        public String CutorderDetails { get; set; }
        public String ReceiptDetails { get; set; }
        public String Remark { get; set; }
        public String Styles { get; set; }
        public String SkuDet_PK { get; set; }
        public String SkuPK { get; set; }
        public List<PlannedDetails> PlannedDetailsList { get; set; }
        public List<PODetails> PODetailsList { get; set; }
        public List<ADNDetails> ADNDetailsList { get; set; }
        public List<InBoundDetails> InBoundDetailsList { get; set; }
        public List<OrderOnHand> OrderOnHandList { get; set; }
        public List<RemarkDetails> RemarkDetailsList { get; set; }
        public List<OurStyleDetails> OurStyleDetailsList { get; set; }
        public List<ReceiptDetails> ReceiptDetailsList { get; set; }
    }


    public class PlannedDetails
    {
        public String Qty { get; set; }
        public String ETADate { get; set; }
    }

    public class PODetails
    {
        public String PONum { get; set; }
        public String POQty { get; set; }
        public String UomCode { get; set; }
        public String SupplierName { get; set; }
        public String BaseUOMQty { get; set; }

    }

    public class ADNDetails
    {
        public String DocNum { get; set; }
        public String ContainerNum { get; set; }
        public String BOENum { get; set; }
        public String PONum { get; set; }
        public String Qty { get; set; }
        public String ExtraQty { get; set; }      
        public String ADNType { get; set; }
        
    }
    public class InBoundDetails
    {
        public String ShipperInv { get; set; }
        public String ETA { get; set; }
        public String Conatianer { get; set; }
        public String Qty { get; set; }
        

    }
    public class OrderOnHand
    {

        public String LocationPrefix { get; set; }
        public Decimal OnhandQty { get; set; }
        public Decimal BaseUOMQty { get; set; }
        public String Qty { get; set; }
        public String LocType { get; set; }


    }
    public class RemarkDetails
    {
        public String Remark { get; set; }
        public String AddedDate { get; set; }
        public String AddedBy { get; set; }
       


    }
    public class OurStyleDetails
    {
        public String OurStyle { get; set; }
        public String Consumption { get; set; }
        
    }
    public class ReceiptDetails
    { 
        public String MrnNum { get; set; }
        public Decimal Qty { get; set; }
        public String UomCode { get; set; }
        public String PONum { get; set; }
        public String LocationPrefix { get; set; }
       

    }
    

}