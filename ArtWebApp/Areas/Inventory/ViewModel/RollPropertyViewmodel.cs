using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Inventory.ViewModel
{
    public class RollPropertyViewModel
    {
        public System.Decimal Roll_PK { get; set; }
        public System.String RollNum { get; set; }
        public System.String LocationName { get; set; }
        public System.String DocumentNum { get; set; }
        public System.String AddedVia { get; set; }
        public System.String DeliveredVia { get; set; }
        public System.Decimal AYard { get; set; }
        public System.String MarkerType { get; set; }
        public System.String WidthGroup { get; set; }
        public System.String ShadeGroup { get; set; }
        public System.String ShrinkageGroup { get; set; }
        public System.String IsCut { get; set; }
        public System.String IsPresent { get; set; }
        public System.Decimal Location_Pk { get; set; }
        public System.Decimal SkuDet_PK { get; set; }
        public System.String IsDelivered { get; set; }
        public System.String ASN { get; set; }
        public System.String LaysheetNUM { get; set; }
        public System.String CutPlanNUM { get; set; }
        public System.String itemDescription { get; set; }
    }

    public class RollPropertyViewModelMaster
    {
        public List<RollPropertyViewModel> RollPropertyViewModellist { get; set; }
    }

    public class RollPropertyJson
    {
        public System.Decimal Roll_PK { get; set; }
        public System.String MarkerType { get; set; }
        public System.String WidthGroup { get; set; }
        public System.String ShadeGroup { get; set; }
        public System.String ShrinkageGroup { get; set; }
        public System.String Reason { get; set; }
    }

    public class RollPropertApprovalModel
    {
        
             public System.Decimal FabricRollChangeID { get; set; }
        public System.Decimal Roll_PK { get; set; }
        public System.String RollNum { get; set; }
        public System.String LocationName { get; set; }
        public System.String DocumentNum { get; set; }
        public System.String AddedVia { get; set; }
        public System.String DeliveredVia { get; set; }
        public System.Decimal AYard { get; set; }
        public System.String MarkerType { get; set; }
        public System.String WidthGroup { get; set; }
        public System.String ShadeGroup { get; set; }
        public System.String ShrinkageGroup { get; set; }
        public System.String IsCut { get; set; }
        public System.String IsPresent { get; set; }
        public System.Decimal Location_Pk { get; set; }
        public System.Decimal SkuDet_PK { get; set; }
        public System.String IsDelivered { get; set; }
        public System.String ASN { get; set; }
        public System.String itemDescription { get; set; }
        public System.String NewMarkerType { get; set; }
        public System.String NewShrinkageGroup { get; set; }
        public System.String NewShadeGroup { get; set; }
        public System.String NewWidthGroup { get; set; }
        public System.String IsApproved { get; set; }
        public System.String AddedBy { get; set; }
        public System.DateTime AddedDate { get; set; }
        public System.String LaysheetNUM { get; set; }
        public System.String CutPlanNUM { get; set; }
        public System.String itemDescription1 { get; set; }
    }


    public class RollPropertApprovalModelMaster
    {
        public List<RollPropertApprovalModel> RollPropertApprovalModellist { get; set; }
    }

    public class RollTransfertoGstockModel
    {
        public System.Boolean IsSelected { get; set; }
        public System.Decimal Roll_PK { get; set; }
        public System.String RollNum { get; set; }
        public System.String itemDescription { get; set; }
        public System.String UOM { get; set; }
        public System.String Remark { get; set; }
        public System.String AShrink { get; set; }
        public System.String AShade { get; set; }
        public System.String AWidth { get; set; }
        public System.Decimal AYard { get; set; }
        public System.String SupplierDocnum { get; set; }
        public System.Decimal Atc_id { get; set; }
        public System.String DocumentNum { get; set; }
        public System.Decimal Location_Pk { get; set; }
        public System.String IsPresent { get; set; }
    }


    public class RollTransfertoGstockModelMaster
    {
        public int TransferToGSTock_PK { get; set; }
        public int SkuDet_PK { get; set; }
        public List<RollTransfertoGstockModel> RollTransfertoGstockModellist { get; set; }
    }

  


}