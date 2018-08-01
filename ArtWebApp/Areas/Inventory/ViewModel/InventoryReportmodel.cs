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


    public class AtcwiseFabricInventory
    {
      
        public string AtcId{ get; set; }
        public string Location{ get; set; }
        public DataTable InventoryDetails { get; set; }
        public DataTable TrimsInventoryDetails { get; set; }
        

    }


    public class GetMcrInventory
    {

        public string AtcId { get; set; }
        public string Location { get; set; }
        public DataTable InventoryDetails { get; set; }
        public DataTable TrimsInventoryDetails { get; set; }


    }

    public class FabricInventoryList
    {
        public string AtcId { get; set; }
        public string Location { get; set; }
        public string InventoryItem_PK { get; set; }
        public string RMNum { get; set; }
        public string Description { get; set; }
        public string ItemColor { get; set; }
        public string SupplierColor { get; set; }
        public string UomCode { get; set; }
        public string ReceivedQty { get; set; }
        public string DeliveredQty { get; set; }
        public string OnhandQty { get; set; }
        public string PhysicalQty { get; set; }
        public string DiffQty { get; set; }
        public string Type { get; set; }
        public string MCRDetails_Pk { get; set; }
        public string ActualCURate { get; set; }
        public string CURate { get; set; }
        public string Template_Pk { get; set; }
        public string Skudet_Pk{ get; set; }
    }

    public class AllocateRack
    {
        public string Mrn_PK { get; set; }
        public string Rack_PK { get; set; }
        public DataTable GetMrnlist { get; set; }

    }
    public class Approvedata
    {
        public string InventoryItem_PK { get; set; }
        public string Rack_PK { get; set; }
        public string NewRack_PK { get; set; }
        public string AllocateQty { get; set; }
        public string mrnqty { get; set; }
        public string BalanceQty { get; set; }

    }

    public class DeliveryReceipt
    {
        public int AtcId { get; set; }
        public DateTime DoDate { get; set; }
        public string dotype { get; set; }
        public string containerno { get; set; }
        public int toloc { get; set; }
        public string boe { get; set; }
        public string deliverymethod { get; set; }
        public DataTable DeliveryDetails{ get; set; }
        public DataTable DeliverFabricDetails { get; set; }
    }
    public class DeliveryDetails
    {
        public int AtcId { get; set; }
        public string DoDate { get; set; }
        public string dotype { get; set; }
        public string containerno { get; set; }
        public int toloc { get; set; }
        public string boe { get; set; }        
        public int InventoryItem_pk { get; set; }
        public Decimal DeliveryQty { get; set; }
        public int DO_Pk { get; set; }
        public int rack_pk { get; set; }
        public int RackInventory_PK { get; set; }
    }

}
