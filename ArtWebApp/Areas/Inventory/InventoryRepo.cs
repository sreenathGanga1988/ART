using ArtWebApp.Areas.Inventory.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace ArtWebApp.Areas.Inventory
{
    public class InventoryRepo
    {

        public RollPropertyViewModelMaster GetRollData(int id)
        {

            RollPropertyViewModelMaster rollPropertyViewModelMaster = new RollPropertyViewModelMaster();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand(@"RollTracker_SP");


            cmd.Parameters.AddWithValue("@supplierdock_pk", 0);
            cmd.Parameters.AddWithValue("@Skudetpk", 0);
            cmd.Parameters.AddWithValue("@RollPk", id);
            cmd.Parameters.AddWithValue("@cutplanPk", 0);
            cmd.Parameters.AddWithValue("@docnum", "NA");

            cmd.CommandType = CommandType.StoredProcedure;


            dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd); ;
            rollPropertyViewModelMaster.RollPropertyViewModellist = GetRollPropertyViewModellist(dt);

            return rollPropertyViewModelMaster;


        }
        public static DataTable GetFactoryDetails(int locpk)
        {
            DataTable dt = new DataTable();
           
                
                SqlCommand cmd = new SqlCommand();
                
            cmd.CommandText = @"SELECT        LocationMaster.Location_PK, LocationMaster.LocationName
                    FROM            FactWareLinkMaster INNER JOIN
                                             LocationMaster ON FactWareLinkMaster.ToLoc_PK = LocationMaster.Location_PK
                    WHERE        (FactWareLinkMaster.FromLoc_pk = @locpk)";

                cmd.Parameters.AddWithValue("@locpk", locpk);
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                return dt;
            }

        public DataTable GetATCwiseFabricInventory(int Locid, int Atcid,int ToLocid)
        {
            DataTable dt = new DataTable();
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        InventoryMaster.InventoryItem_PK, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum, InventoryMaster.SkuDet_Pk,SkuRawMaterialMaster.Template_pk,
                SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                 ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, InventoryMaster.OnhandQty, InventoryMaster.OnhandQty as PhysicalQty,
                LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value,AtcMaster.AtcID,LocationMaster.Location_PK,
                0 as DiffQty,InventoryMaster.CURate as ActualRate,0 as Packages, @ToLocid as ToLocid
                FROM            MrnMaster INNER JOIN
                MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                Template_Master INNER JOIN
                AtcMaster INNER JOIN
                SkuRawMaterialMaster INNER JOIN
                SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id ON 
                Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                LocationMaster INNER JOIN
                UOMMaster INNER JOIN
                InventoryMaster INNER JOIN
                ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON LocationMaster.Location_PK = InventoryMaster.Location_PK ON 
                SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk ON MrnDetails.MrnDet_PK = InventoryMaster.MrnDet_PK WHERE AtcMaster.AtcId=@Atcid AND LocationMaster.Location_PK =@Locid and (ItemGroupMaster.ItemGroupName = N'Fabric')
                 AND (InventoryMaster.OnhandQty > 0) and InventoryMaster.InventoryItem_PK not in(select InventoryItem_PK from MCRDetails)
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize,
                  ProcurementDetails.SupplierColor, UOMMaster.UomCode";
            cmd.Parameters.AddWithValue("@Locid", Locid);
            cmd.Parameters.AddWithValue("@Atcid", Atcid);
            cmd.Parameters.AddWithValue("@ToLocid", ToLocid);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }
        public DataTable GetATCwiseTrimsInventory(int Locid, int Atcid,int ToLocid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, InventoryMaster.SkuDet_Pk,SkuRawMaterialMaster.Template_pk,
                SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, InventoryMaster.OnhandQty,
InventoryMaster.OnhandQty as PhysicalQty,
                AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value,
AtcMaster.AtcID,LocationMaster.Location_PK,0 as DiffQty,InventoryMaster.CURate as ActualRate,0 as Packages,@ToLocid as ToLocid
                FROM            MrnMaster INNER JOIN
                MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                Template_Master INNER JOIN
                AtcMaster INNER JOIN
                SkuRawMaterialMaster INNER JOIN
                SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id ON 
                Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                LocationMaster INNER JOIN
                UOMMaster INNER JOIN
                InventoryMaster INNER JOIN
                ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON LocationMaster.Location_PK = InventoryMaster.Location_PK ON 
                SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk ON MrnDetails.MrnDet_PK = InventoryMaster.MrnDet_PK WHERE AtcMaster.AtcId=@Atcid AND LocationMaster.Location_PK =@Locid and (ItemGroupMaster.ItemGroupName = N'Trims')
                 AND (InventoryMaster.OnhandQty > 0) and InventoryMaster.InventoryItem_PK not in(select InventoryItem_PK from MCRDetails) ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize,
                  ProcurementDetails.SupplierColor, UOMMaster.UomCode";
            cmd.Parameters.AddWithValue("@Locid", Locid);
            cmd.Parameters.AddWithValue("@Atcid", Atcid);
            cmd.Parameters.AddWithValue("@ToLocid", ToLocid);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }

        public DataTable FabricInventoryEdit(int Locid, int Atcid,int mcr_pk)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        InventoryMaster.InventoryItem_PK, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum, InventoryMaster.SkuDet_Pk,SkuRawMaterialMaster.Template_pk,
                SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                 ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, InventoryMaster.OnhandQty, InventoryMaster.OnhandQty as PhysicalQty,
                LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value,AtcMaster.AtcID,LocationMaster.Location_PK,
                0 as DiffQty,InventoryMaster.CURate as ActualRate,0 as Packages,@mcr_pk as mcr_pk
                FROM            MrnMaster INNER JOIN
                MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                Template_Master INNER JOIN
                AtcMaster INNER JOIN
                SkuRawMaterialMaster INNER JOIN
                SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id ON 
                Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                LocationMaster INNER JOIN
                UOMMaster INNER JOIN
                InventoryMaster INNER JOIN
                ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON LocationMaster.Location_PK = InventoryMaster.Location_PK ON 
                SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk ON MrnDetails.MrnDet_PK = InventoryMaster.MrnDet_PK WHERE AtcMaster.AtcId=@Atcid AND LocationMaster.Location_PK =@Locid and (ItemGroupMaster.ItemGroupName = N'Fabric')
                 AND (InventoryMaster.OnhandQty > 0) 
and InventoryMaster.InventoryItem_PK not in(select InventoryItem_PK from MCRDetails where Mcr_pk =@mcr_pk)
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize,
                  ProcurementDetails.SupplierColor, UOMMaster.UomCode";
            cmd.Parameters.AddWithValue("@Locid", Locid);
            cmd.Parameters.AddWithValue("@Atcid", Atcid);
            cmd.Parameters.AddWithValue("@mcr_pk", mcr_pk);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }

        public DataTable TrimsInventoryEdit(int Locid, int Atcid,int mcr_pk)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, InventoryMaster.SkuDet_Pk,SkuRawMaterialMaster.Template_pk,
                SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, InventoryMaster.OnhandQty,
InventoryMaster.OnhandQty as PhysicalQty,
                AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value,
AtcMaster.AtcID,LocationMaster.Location_PK,0 as DiffQty,InventoryMaster.CURate as ActualRate,0 as Packages,@mcr_pk as mcr_pk
                FROM            MrnMaster INNER JOIN
                MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                Template_Master INNER JOIN
                AtcMaster INNER JOIN
                SkuRawMaterialMaster INNER JOIN
                SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id ON 
                Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                LocationMaster INNER JOIN
                UOMMaster INNER JOIN
                InventoryMaster INNER JOIN
                ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON LocationMaster.Location_PK = InventoryMaster.Location_PK ON 
                SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk ON MrnDetails.MrnDet_PK = InventoryMaster.MrnDet_PK WHERE AtcMaster.AtcId=@Atcid AND LocationMaster.Location_PK =@Locid and (ItemGroupMaster.ItemGroupName = N'Trims')
                 AND (InventoryMaster.OnhandQty > 0) and InventoryMaster.InventoryItem_PK not in(select InventoryItem_PK from MCRDetails where Mcr_pk =@mcr_pk) ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize,
                  ProcurementDetails.SupplierColor, UOMMaster.UomCode";
            cmd.Parameters.AddWithValue("@Locid", Locid);
            cmd.Parameters.AddWithValue("@Atcid", Atcid);
            cmd.Parameters.AddWithValue("@mcr_pk", mcr_pk);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }

        public DataTable GetDeliveryItemDetails(int Locid, int Atcid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        InventoryItem_PK, RMNum,Rack_name,Rack_PK,RackInventory_PK, Description, ItemColor, ItemSize, SupplierSize, SupplierColor, UomCode, ReceivedQty, DeliveredQty, OnhandQty as TotalOnhand, BlockedQty,(OnhandQty-BlockedQty) as OnhandQty,Refnum
FROM            (SELECT        RackInventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, RackInventoryMaster.Rack_PK,RackInventoryMaster.RackInventory_PK,RackMaster.Rack_name,
                                                    SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, 
                                                    SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, RackInventoryMaster.ReceivedQty, 
                                                    RackInventoryMaster.DeliveredQty, RackInventoryMaster.OnhandQty, ISNULL
                                                        ((SELECT        SUM(RequestOrderDetails.Qty) AS Expr1
                                                            FROM            RequestOrderDetails INNER JOIN
                                                                                     RequestOrderMaster ON RequestOrderDetails.RO_Pk = RequestOrderMaster.RO_Pk
                                                            WHERE        (RequestOrderDetails.InventoryItem_PK = RackInventoryMaster.InventoryItem_PK) AND (RequestOrderMaster.IsDeleted = N'N') AND (RequestOrderMaster.IsCompleted = N'N')), 0) 
                                                    + ISNULL
                                                        ((SELECT        SUM(LoanQty) AS Expr1
                                                            FROM            InventoryLoanMaster
                                                            WHERE        (FromIIT_Pk = RackInventoryMaster.InventoryItem_PK) AND (IsApproved = N'N')), 0) AS BlockedQty,RackInventoryMaster.Refnum
                          FROM            UOMMaster INNER JOIN
                                                    RackInventoryMaster INNER JOIN
                                                    ProcurementDetails ON RackInventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK INNER JOIN
                                                    SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON RackInventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK
inner join RackMaster on RackMaster.Rack_pk=RackInventoryMaster.Rack_PK
inner join Template_Master  on Template_Master.Template_PK =SkuRawMaterialMaster.Template_pk
 WHERE(SkuRawMaterialMaster.Atc_id = @Atcid) AND (RackInventoryMaster.Location_PK = @Locid) and (Template_Master.ItemGroup_PK=2))AS tt
ORDER BY RMNum, Description, ItemColor, ItemSize, SupplierSize, SupplierColor, UomCode";
            cmd.Parameters.AddWithValue("@Locid", Locid);
            cmd.Parameters.AddWithValue("@Atcid", Atcid);            


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }
        public  DataTable getFabricRollofAItemPK(string Condition, int location_pk, int mcrpk)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        tt.Roll_PK, tt.RollNum, tt.ASN, tt.PONum, tt.itemDescription, tt.WidthGroup, tt.ShadeGroup, tt.ShrinkageGroup, tt.AYard, tt.AtcNum, tt.InventoryItem_PK, tt.Location_PK, RollInventoryMaster.IsPresent, 
                         RollInventoryMaster.Location_Pk AS Expr1, tt.MarkerType, tt.AWidth, tt.AShrink, tt.AShade,ISNULL( tt.SWeight,'NA') as SWeight,RACK_NAME,@mcrpk as mcrpk
FROM            (SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, ProcurementMaster.PONum, 
                         ISNULL(SkuRawMaterialMaster.Composition, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, N' ') 
                         + ' ' + ISNULL(ProcurementDetails.SupplierSize, N' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, N' ') AS itemDescription, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup, FabricRollmaster.AYard, AtcMaster.AtcNum, InventoryMaster.InventoryItem_PK, InventoryMaster.Location_PK, FabricRollmaster.MarkerType, FabricRollmaster.AWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.SWeight, ISNULL( (SELECT        RackMaster.Rack_name
FROM            RackMaster INNER JOIN
                         RollInventoryMaster ON RackMaster.Rack_pk = RollInventoryMaster.Rack_PK
WHERE        (RollInventoryMaster.Roll_PK = FabricRollmaster.Roll_PK ) and  (RollInventoryMaster.Location_Pk = @location_pk ) and  (RollInventoryMaster.IsPresent ='Y') ) ,0) AS RACK_NAME
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         SupplierMaster ON SupplierDocumentMaster.Supplier_pk = SupplierMaster.Supplier_PK INNER JOIN
                         InventoryMaster ON FabricRollmaster.SkuDet_PK = InventoryMaster.SkuDet_Pk INNER JOIN
                         CurrencyMaster ON SupplierMaster.CurrencyID = CurrencyMaster.CurrencyID 
WHERE         " + Condition+ " ) AS tt INNER JOIN RollInventoryMaster ON tt.Roll_PK = RollInventoryMaster.Roll_PK WHERe (RollInventoryMaster.IsPresent = N'Y') AND (RollInventoryMaster.Location_PK = @location_pk) and tt.Roll_PK not in(select Roll_PK from MCRRollDetails where Mcr_pk =@mcrpk)  ORDER BY tt.InventoryItem_PK ";
                cmd.Parameters.AddWithValue("@Condition", Condition);
                cmd.Parameters.AddWithValue("@location_pk", location_pk);
                cmd.Parameters.AddWithValue("@mcrpk", mcrpk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }

        public DataTable GetFabricStockDetails(int Locid, int Atcid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        InventoryItem_PK, RMNum, Description, ItemColor, ItemSize, SupplierSize, SupplierColor, UomCode, ReceivedQty, DeliveredQty, OnhandQty as TotalOnhand, BlockedQty,(OnhandQty-BlockedQty) as OnhandQty,Refnum
FROM            (SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, 
                                                    SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, 
                                                    SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, 
                                                    InventoryMaster.DeliveredQty, InventoryMaster.OnhandQty, ISNULL
                                                        ((SELECT        SUM(RequestOrderDetails.Qty) AS Expr1
                                                            FROM            RequestOrderDetails INNER JOIN
                                                                                     RequestOrderMaster ON RequestOrderDetails.RO_Pk = RequestOrderMaster.RO_Pk
                                                            WHERE        (RequestOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK) AND (RequestOrderMaster.IsDeleted = N'N') AND (RequestOrderMaster.IsCompleted = N'N')), 0) 
                                                    + ISNULL
                                                        ((SELECT        SUM(LoanQty) AS Expr1
                                                            FROM            InventoryLoanMaster
                                                            WHERE        (FromIIT_Pk = InventoryMaster.InventoryItem_PK) AND (IsApproved = N'N')), 0) AS BlockedQty,InventoryMaster.Refnum
                          FROM            UOMMaster INNER JOIN
                                                    InventoryMaster INNER JOIN
                                                    ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK INNER JOIN
                                                    SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK
inner join Template_Master  on Template_Master.Template_PK =SkuRawMaterialMaster.Template_pk
 WHERE        (SkuRawMaterialMaster.Atc_id = @Atcid) AND (InventoryMaster.Location_PK = @Locid) and (Template_Master.ItemGroup_PK=1) AND (InventoryMaster.OnhandQty > 0)) AS tt
ORDER BY RMNum, Description, ItemColor, ItemSize, SupplierSize, SupplierColor, UomCode";
            cmd.Parameters.AddWithValue("@Locid", Locid);
            cmd.Parameters.AddWithValue("@Atcid", Atcid);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }
        public DataTable GetDetailsRackShuffle(int Rack_pk)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        InventoryItem_PK, RMNum,Rack_name,Rack_PK,RackInventory_PK, Description, ItemColor, ItemSize, SupplierSize, SupplierColor, UomCode, ReceivedQty, DeliveredQty, OnhandQty as TotalOnhand, Refnum,AtcNum
FROM            (SELECT        RackInventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, RackInventoryMaster.Rack_PK,RackInventoryMaster.RackInventory_PK,RackMaster.Rack_name,
                                                    SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, 
                                                    SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, RackInventoryMaster.ReceivedQty, 
                                                    RackInventoryMaster.DeliveredQty, RackInventoryMaster.OnhandQty,RackInventoryMaster.Refnum,AtcMaster.AtcNum 
                          FROM            UOMMaster INNER JOIN
                                                    RackInventoryMaster INNER JOIN
                                                    ProcurementDetails ON RackInventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK INNER JOIN
                                                    SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON RackInventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK
inner join RackMaster on RackMaster.Rack_pk=RackInventoryMaster.Rack_PK
inner join Template_Master  on Template_Master.Template_PK =SkuRawMaterialMaster.Template_pk inner join
AtcMaster on AtcMaster.AtcId =SkuRawMaterialMaster.Atc_id 
WHERE(RackInventoryMaster.Rack_PK=@Rack_pk) and (Template_Master.ItemGroup_PK=2))AS tt
ORDER BY RMNum, Description, ItemColor, ItemSize, SupplierSize, SupplierColor, UomCode";
            cmd.Parameters.AddWithValue("@Rack_pk", Rack_pk);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }
        public DataTable GetRollDetailsRackShuffle(int LocPk,int Atcid,int rackpk)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        tt.Roll_PK, tt.RollNum, tt.ASN, tt.PONum, tt.Description, tt.AYard, tt.AtcNum, tt.InventoryItem_PK, tt.Location_PK, RollInventoryMaster.Location_Pk AS location_pk, tt.AWidth, tt.AShrink, tt.AShade, ISNULL(tt.SWeight, 'NA') 
                         AS SWeight, tt.SYard, tt.Qty, tt.SWidth, tt.SShade, tt.ItemColor,tt.RMNum,tt.ColorCode,RollInventoryMaster.Rack_PK,RackMaster.Rack_name,tt.AtcId  
FROM            (SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, ProcurementMaster.PONum, 
                                                    ISNULL(SkuRawMaterialMaster.Composition, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, N' ') 
                                                    + ' ' + ISNULL(ProcurementDetails.SupplierSize, N' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, N' ') AS Description, AtcMaster.AtcNum, InventoryMaster.InventoryItem_PK, InventoryMaster.Location_PK, 
                                                    FabricRollmaster.MarkerType, FabricRollmaster.AWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.SWeight, FabricRollmaster.SYard, FabricRollmaster.Qty, FabricRollmaster.SWidth, 
                                                    FabricRollmaster.SShade, FabricRollmaster.AYard, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ColorCode, SkuRawMaterialMaster.RMNum,
													AtcMaster.AtcId
                          FROM            Template_Master INNER JOIN
                                                    SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                                                    ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                                                    FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                                                    ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                                                    SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                                                    ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                                                    AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                                                    InventoryMaster ON FabricRollmaster.SkuDet_PK = InventoryMaster.SkuDet_Pk AND FabricRollmaster.SkuDet_PK = InventoryMaster.SkuDet_Pk AND 
                                                    FabricRollmaster.MRnDet_PK = InventoryMaster.MrnDet_PK 
                          WHERE        (ItemGroupMaster.ItemGroupName = N'Fabric') AND (InventoryMaster.OnhandQty > 0) AND (FabricRollmaster.IsDelivered <> N'Y') AND (FabricRollmaster.RackAllocated ='Y')) AS tt INNER JOIN
                         RollInventoryMaster ON tt.Roll_PK = RollInventoryMaster.Roll_PK inner join
						 RackMaster on RackMaster.Rack_pk=RollInventoryMaster.Rack_PK
WHERE        (RollInventoryMaster.IsPresent = N'Y') AND (tt.Location_PK = @LocPk) AND (tt.AYard > 0) and (tt.AtcId =@Atcid) and (RackMaster.Rack_pk =@rackpk)
ORDER BY tt.Roll_PK ";
            cmd.Parameters.AddWithValue("@LocPk", LocPk);
            cmd.Parameters.AddWithValue("@Atcid", Atcid);
            cmd.Parameters.AddWithValue("@rackpk", rackpk);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }
        public DataTable GetMrnDetailsforAllocation(int MRN_PK,int Rack_PK,int locpk)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            if(MRN_PK > 0)
            {
                cmd.CommandText = @"select InventoryItem_PK,RMNum,SkuDet_Pk,Template_pk,Description,ItemColor,ItemSize,SupplierSize,SupplierColor,UomCode,ReceivedQty,AllocatedQty,AtcNum,LocationName,ReceivedVia,Refnum,
CURate,AtcID,Location_PK,MrnNum,Rack_PK,OnhandQty
 from (SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, InventoryMaster.SkuDet_Pk,SkuRawMaterialMaster.Template_pk,
                SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty,InventoryMaster.OnhandQty, 
isnull((select sum(onhandqty) from RackInventoryMaster where InventoryMaster.InventoryItem_PK =RackInventoryMaster.Inventoryitem_PK),0)as AllocatedQty,
                AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate,
AtcMaster.AtcID,LocationMaster.Location_PK,MrnMaster.MrnNum,@Rack_PK as Rack_PK
                FROM            MrnMaster INNER JOIN
                MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                Template_Master INNER JOIN
                AtcMaster INNER JOIN
                SkuRawMaterialMaster INNER JOIN
                SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id ON 
                Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                LocationMaster INNER JOIN
                UOMMaster INNER JOIN
                InventoryMaster INNER JOIN
                ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON LocationMaster.Location_PK = InventoryMaster.Location_PK ON 
                SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk ON MrnDetails.MrnDet_PK = InventoryMaster.MrnDet_PK WHERE (ItemGroupMaster.ItemGroupName = N'Trims')
				and (InventoryMaster.ReceivedVia='MR') and MrnMaster.Mrn_PK =@MRN_PK and MrnDetails.IsRackAllocateDone is NULL)as tt where ((OnhandQty-AllocatedQty )>0) 
                 ORDER BY RMNum, Description, ItemColor, ItemSize, SupplierSize,SupplierColor,UomCode ";
                cmd.Parameters.AddWithValue("@MRN_PK", MRN_PK);
                cmd.Parameters.AddWithValue("@Rack_PK", Rack_PK);
            }
            else
            {
                cmd.CommandText = @"select InventoryItem_PK,RMNum,SkuDet_Pk,Template_pk,Description,ItemColor,ItemSize,SupplierSize,SupplierColor,UomCode,ReceivedQty,AllocatedQty,AtcNum,LocationName,ReceivedVia,Refnum,
CURate,AtcID,Location_PK,MrnNum,Rack_PK,OnhandQty
 from (SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, InventoryMaster.SkuDet_Pk,SkuRawMaterialMaster.Template_pk,
                SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty,InventoryMaster.OnhandQty, 
isnull((select sum(onhandqty) from RackInventoryMaster where InventoryMaster.InventoryItem_PK =RackInventoryMaster.Inventoryitem_PK),0)as AllocatedQty,
                AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate,
AtcMaster.AtcID,LocationMaster.Location_PK,MrnMaster.MrnNum,@Rack_PK as Rack_PK
                FROM            MrnMaster INNER JOIN
                MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                Template_Master INNER JOIN
                AtcMaster INNER JOIN
                SkuRawMaterialMaster INNER JOIN
                SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id ON 
                Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                LocationMaster INNER JOIN
                UOMMaster INNER JOIN
                InventoryMaster INNER JOIN
                ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK ON UOMMaster.Uom_PK = ProcurementDetails.Uom_PK ON LocationMaster.Location_PK = InventoryMaster.Location_PK ON 
                SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk ON MrnDetails.MrnDet_PK = InventoryMaster.MrnDet_PK WHERE (ItemGroupMaster.ItemGroupName = N'Trims')
				and (InventoryMaster.ReceivedVia='MR') and MrnDetails.IsRackAllocateDone is NULL and LocationMaster.Location_PK=@locpk)as tt where ((OnhandQty-AllocatedQty )>0) 
                 ORDER BY RMNum, Description, ItemColor, ItemSize, SupplierSize,SupplierColor,UomCode ";                
                cmd.Parameters.AddWithValue("@Rack_PK", Rack_PK);
                cmd.Parameters.AddWithValue("@locpk", locpk);
            }
            
            


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }
        public DataTable GetBodyParts()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        BodyPartName, BodyPart_PK
FROM            BodyPartMaster where IsActive = 1";


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }
        public DataTable GetRollMrnDetailsforAllocation(int MRN_PK, int Rack_PK,int locpk)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            if (MRN_PK > 0)
            {                            
            cmd.CommandText = @"SELECT        tt.Roll_PK, tt.RollNum, tt.ASN, tt.PONum, tt.Description, tt.AYard, tt.AtcNum, tt.InventoryItem_PK, tt.Location_PK, RollInventoryMaster.Location_Pk AS location_pk, tt.AWidth, tt.AShrink, tt.AShade, ISNULL(tt.SWeight, 'NA') 
                         AS SWeight, tt.SYard, tt.Qty, tt.SWidth, tt.SShade, tt.ItemColor,@Rack_PK as Rack_PK,tt.RMNum,tt.ColorCode
FROM            (SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, ProcurementMaster.PONum, 
                                                    ISNULL(SkuRawMaterialMaster.Composition, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, N' ') 
                                                    + ' ' + ISNULL(ProcurementDetails.SupplierSize, N' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, N' ') AS Description, AtcMaster.AtcNum, InventoryMaster.InventoryItem_PK, InventoryMaster.Location_PK, 
                                                    FabricRollmaster.MarkerType, FabricRollmaster.AWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.SWeight, FabricRollmaster.SYard, FabricRollmaster.Qty, FabricRollmaster.SWidth, 
                                                    FabricRollmaster.SShade, FabricRollmaster.AYard, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ColorCode, SkuRawMaterialMaster.RMNum
                          FROM            Template_Master INNER JOIN
                                                    SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                                                    ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                                                    FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                                                    ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                                                    SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                                                    ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                                                    AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                                                    InventoryMaster ON FabricRollmaster.SkuDet_PK = InventoryMaster.SkuDet_Pk AND FabricRollmaster.SkuDet_PK = InventoryMaster.SkuDet_Pk AND 
                                                    FabricRollmaster.MRnDet_PK = InventoryMaster.MrnDet_PK inner join
													MrnDetails on MrnDetails.MrnDet_PK=FabricRollmaster.MRnDet_PK and MrnDetails.Mrn_PK =@MRN_PK
                          WHERE        (ItemGroupMaster.ItemGroupName = N'Fabric') AND (InventoryMaster.OnhandQty > 0) AND (FabricRollmaster.IsDelivered <> N'Y') AND (FabricRollmaster.RackAllocated ='N')) AS tt INNER JOIN
                         RollInventoryMaster ON tt.Roll_PK = RollInventoryMaster.Roll_PK
WHERE        (RollInventoryMaster.IsPresent = N'Y') AND (tt.Location_PK = @locpk)  and RollInventoryMaster.Location_Pk =@locpk
ORDER BY tt.RollNum ";
            cmd.Parameters.AddWithValue("@MRN_PK", MRN_PK);
            cmd.Parameters.AddWithValue("@Rack_PK", Rack_PK);
            cmd.Parameters.AddWithValue("@locpk", locpk);
            }
            else
            {

            cmd.CommandText = @"SELECT        tt.Roll_PK, tt.RollNum, tt.ASN, tt.PONum, tt.Description, tt.AYard, tt.AtcNum, tt.InventoryItem_PK, tt.Location_PK, RollInventoryMaster.Location_Pk AS location_pk, tt.AWidth, tt.AShrink, tt.AShade, ISNULL(tt.SWeight, 'NA') 
                         AS SWeight,  tt.SYard, tt.Qty, tt.SWidth, tt.SShade, tt.ItemColor,@Rack_PK as Rack_PK,tt.RMNum,tt.ColorCode
FROM            (SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, ProcurementMaster.PONum, 
                                                    ISNULL(SkuRawMaterialMaster.Composition, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, N' ') 
                                                    + ' ' + ISNULL(ProcurementDetails.SupplierSize, N' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, N' ') AS Description, AtcMaster.AtcNum, InventoryMaster.InventoryItem_PK, InventoryMaster.Location_PK, 
                                                    FabricRollmaster.MarkerType, FabricRollmaster.AWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.SWeight, FabricRollmaster.SYard, FabricRollmaster.Qty, FabricRollmaster.SWidth, 
                                                    FabricRollmaster.SShade, FabricRollmaster.AYard, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ColorCode, SkuRawMaterialMaster.RMNum
                          FROM            Template_Master INNER JOIN
                                                    SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK ON Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                                                    ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                                                    FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                                                    ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                                                    SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                                                    ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                                                    AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                                                    InventoryMaster ON FabricRollmaster.SkuDet_PK = InventoryMaster.SkuDet_Pk  AND 
                                                    FabricRollmaster.MRnDet_PK = InventoryMaster.MrnDet_PK
                          WHERE        (ItemGroupMaster.ItemGroupName = N'Fabric') AND (InventoryMaster.OnhandQty > 0) AND (FabricRollmaster.IsDelivered <> N'Y') AND (FabricRollmaster.RackAllocated='N')) AS tt INNER JOIN
                         RollInventoryMaster ON tt.Roll_PK = RollInventoryMaster.Roll_PK
WHERE        (RollInventoryMaster.IsPresent = N'Y') AND (tt.Location_PK = @locpk)  and RollInventoryMaster.Location_Pk =@locpk
ORDER BY tt.RollNum
 ";
            
            cmd.Parameters.AddWithValue("@Rack_PK", Rack_PK);
            cmd.Parameters.AddWithValue("@locpk", locpk);

            }
            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }
        public DataTable GetStockInventoryItems(int MRN_PK, int Rack_PK,int locpk)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            if (MRN_PK > 0)
            {                            
            cmd.CommandText = @"SELECT        SInventoryItem_PK, Description, Composition, Construct, TemplateColor, TemplateSize, width, Unitprice, ReceivedQty, OnHandQty, UomName, Location_Pk, deliveryqty, CuRate, AllocatedQty,
SMRNDet_Pk,SPODetails_PK,Template_PK,MRNQty,ReceivedVia,Refnum,@Rack_PK as Rack_PK
FROM            (SELECT        StockInventoryMaster.SInventoryItem_PK, Template_Master.Description, StockInventoryMaster.Composition, StockInventoryMaster.Construct, StockInventoryMaster.TemplateColor, 
                    StockInventoryMaster.TemplateSize, StockInventoryMaster.TemplateWidth + ' ' + StockInventoryMaster.TemplateWeight AS width, StockInventoryMaster.Unitprice, StockInventoryMaster.ReceivedQty, 
                    StockInventoryMaster.OnHandQty, UOMMaster.UomName, StockInventoryMaster.Location_Pk, 0.0 AS deliveryqty, StockInventoryMaster.CuRate,
					isnull((select sum(onhandqty) from StockRackInventoryMaster where StockInventoryMaster.SInventoryItem_PK=StockRackInventoryMaster.SInventoryitem_PK),0)as AllocatedQty ,
					StockMRNDetails.SMRNDet_Pk ,StockPODetails.SPODetails_PK,StockInventoryMaster.Template_PK,(StockMRNDetails.ReceivedQty+ISNULL (StockMRNDetails.ExtraQty ,0)) as MRNQty,
					StockInventoryMaster.ReceivedVia,StockInventoryMaster.Refnum 								

                          FROM            StockInventoryMaster INNER JOIN
                                                    Template_Master ON StockInventoryMaster.Template_PK = Template_Master.Template_PK INNER JOIN
                                                    UOMMaster ON StockInventoryMaster.Uom_PK = UOMMaster.Uom_PK inner join
													StockMRNDetails on StockMRNDetails .SMRNDet_Pk =StockInventoryMaster.SMRNDet_Pk inner join
													StockPODetails on StockPODetails .SPODetails_PK =StockInventoryMaster.SPODetails_PK 
                          WHERE        (StockInventoryMaster.Location_Pk = @locpk) AND (StockInventoryMaster.OnHandQty > 0)) AS tt where (OnHandQty -AllocatedQty )>0 ";
            cmd.Parameters.AddWithValue("@MRN_PK", MRN_PK);
            cmd.Parameters.AddWithValue("@Rack_PK", Rack_PK);
            cmd.Parameters.AddWithValue("@locpk", locpk);
            }
            else
            {

            cmd.CommandText = @"SELECT        SInventoryItem_PK, Description, Composition, Construct, TemplateColor, TemplateSize, width, Unitprice, ReceivedQty, OnHandQty, UomName, Location_Pk, deliveryqty, CuRate, AllocatedQty,
SMRNDet_Pk,SPODetails_PK,Template_PK,MRNQty,ReceivedVia,Refnum,@Rack_PK as Rack_PK
FROM            (SELECT        StockInventoryMaster.SInventoryItem_PK, Template_Master.Description, StockInventoryMaster.Composition, StockInventoryMaster.Construct, StockInventoryMaster.TemplateColor, 
                    StockInventoryMaster.TemplateSize, StockInventoryMaster.TemplateWidth + ' ' + StockInventoryMaster.TemplateWeight AS width, StockInventoryMaster.Unitprice, StockInventoryMaster.ReceivedQty, 
                    StockInventoryMaster.OnHandQty, UOMMaster.UomName, StockInventoryMaster.Location_Pk, 0.0 AS deliveryqty, StockInventoryMaster.CuRate,
					isnull((select sum(onhandqty) from StockRackInventoryMaster where StockInventoryMaster.SInventoryItem_PK=StockRackInventoryMaster.SInventoryitem_PK),0)as AllocatedQty ,
					StockMRNDetails.SMRNDet_Pk ,StockPODetails.SPODetails_PK,StockInventoryMaster.Template_PK,(StockMRNDetails.ReceivedQty+ISNULL (StockMRNDetails.ExtraQty ,0)) as MRNQty,
					StockInventoryMaster.ReceivedVia,StockInventoryMaster.Refnum 								

                          FROM            StockInventoryMaster INNER JOIN
                                                    Template_Master ON StockInventoryMaster.Template_PK = Template_Master.Template_PK INNER JOIN
                                                    UOMMaster ON StockInventoryMaster.Uom_PK = UOMMaster.Uom_PK inner join
													StockMRNDetails on StockMRNDetails .SMRNDet_Pk =StockInventoryMaster.SMRNDet_Pk inner join
													StockPODetails on StockPODetails .SPODetails_PK =StockInventoryMaster.SPODetails_PK 
                          WHERE        (StockInventoryMaster.Location_Pk = @locpk) AND (StockInventoryMaster.OnHandQty > 0)) AS tt where (OnHandQty -AllocatedQty )>0
 ";
            
            cmd.Parameters.AddWithValue("@Rack_PK", Rack_PK);
            cmd.Parameters.AddWithValue("@locpk", locpk);

            }
            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }

        public DataTable MCRFabricInventory(int Mcr_pk)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        Atcid,InventoryItem_pk, Location_pk, ReceivedQty, McrDetails_pk, DeliveredQty, Onhandqty, PhysicalQty, DiffQty, AddedDate, Addedby, ApprovedBy, ApprovedDate, RMNum, Description, 
                         ItemColor, SupplierColor, UOM, CU_Rate, ActualCU_Rate, type,Mcr_pk
FROM            MCRDetails
WHERE        (Mcr_pk = @Mcr_pk) AND (type = 'F') ";
            cmd.Parameters.AddWithValue("@Mcr_pk", Mcr_pk);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }

        public DataTable MCRTrimsInventory(int Mcr_pk)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        Atcid,InventoryItem_pk, Location_pk, ReceivedQty, McrDetails_pk, DeliveredQty, Onhandqty, PhysicalQty, DiffQty, AddedDate, Addedby, ApprovedBy, ApprovedDate, RMNum, Description, 
                         ItemColor, SupplierColor, UOM, CU_Rate, ActualCU_Rate, type,Mcr_pk
FROM            MCRDetails
WHERE        (Mcr_pk = @Mcr_pk) AND (type = 'T') ";
            cmd.Parameters.AddWithValue("@Mcr_pk", Mcr_pk);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }


        public DataTable MCRTransferRollInventory(int mcr_pk)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, ProcurementMaster.PONum, 
                         ISNULL(SkuRawMaterialMaster.Composition, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, N' ') 
                         + ' ' + ISNULL(ProcurementDetails.SupplierSize, N' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, N' ') AS itemDescription, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup, FabricRollmaster.AYard, AtcMaster.AtcNum, InventoryMaster.InventoryItem_PK, InventoryMaster.Location_PK, FabricRollmaster.MarkerType, FabricRollmaster.AWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.SWeight,@mcr_pk as mcr_pk,0 as ReceiveYard,0 as ACT_R_Yard,0 as Mcr_kg
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         SupplierMaster ON SupplierDocumentMaster.Supplier_pk = SupplierMaster.Supplier_PK INNER JOIN
                         InventoryMaster ON FabricRollmaster.SkuDet_PK = InventoryMaster.SkuDet_Pk inner join
						 MCRRollDetails on MCRRollDetails.Mcr_pk =@mcr_pk and MCRRollDetails.Inventoryitem_pk=InventoryMaster.InventoryItem_PK 
WHERE MCRRollDetails.Mcr_pk =@mcr_pk  and MCRRollDetails.Roll_pk =FabricRollmaster.Roll_PK and MCRRollDetails.IsReceived ='N'";
            cmd.Parameters.AddWithValue("@mcr_pk", mcr_pk);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }
        public DataTable MCRTransferFabricInventory(int mcr_pk)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        Atcid,InventoryItem_pk, Location_pk, ReceivedQty, McrDetails_pk, DeliveredQty, Onhandqty, PhysicalQty, DiffQty, AddedDate, Addedby, ApprovedBy, ApprovedDate, RMNum, Description, 
                         ItemColor, SupplierColor, UOM, CU_Rate, ActualCU_Rate, type,Mcr_pk,PhysicalQty as ActualReceive,isnull(ActualDiffQty,0) as ActualDiffQty
FROM            MCRDetails
WHERE       (Mcr_pk = @mcr_pk) AND (type = 'F')";
            cmd.Parameters.AddWithValue("@mcr_pk", mcr_pk);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }

        public DataTable MCRTransferTrimsInventory(int mcr_pk)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        Atcid,InventoryItem_pk, Location_pk, ReceivedQty, McrDetails_pk, DeliveredQty, Onhandqty, PhysicalQty, DiffQty, AddedDate, Addedby, ApprovedBy, ApprovedDate, RMNum, Description, 
                         ItemColor, SupplierColor, UOM, CU_Rate, ActualCU_Rate, type,Mcr_pk,PhysicalQty as ActualReceive,isnull(ActualDiffQty,0) as ActualDiffQty
FROM            MCRDetails
WHERE        (Mcr_pk = @mcr_pk) AND (type = 'T')";
            cmd.Parameters.AddWithValue("@mcr_pk", mcr_pk);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }

       
        public DataTable ApprovedMCRFabricInventory(int Mcr_pk)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        Atcid,InventoryItem_pk, Location_pk, ReceivedQty, McrDetails_pk, DeliveredQty, Onhandqty, PhysicalQty, DiffQty, AddedDate, Addedby, ApprovedBy, ApprovedDate, RMNum, Description, 
                         ItemColor, SupplierColor, UOM, CU_Rate, ActualCU_Rate, type,Mcr_pk
FROM            MCRDetails
WHERE        (Mcr_pk = @Mcr_pk) AND (type = 'F') ";
            cmd.Parameters.AddWithValue("@Mcr_pk", Mcr_pk);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }

        public DataTable ApprovedMCRTrimsInventory(int Mcr_pk)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        Atcid,InventoryItem_pk, Location_pk, ReceivedQty, McrDetails_pk, DeliveredQty, Onhandqty, PhysicalQty, DiffQty, AddedDate, Addedby, ApprovedBy, ApprovedDate, RMNum, Description, 
                         ItemColor, SupplierColor, UOM, CU_Rate, ActualCU_Rate, type,Mcr_pk
FROM            MCRDetails
WHERE        (Mcr_pk = @Mcr_pk) AND (type = 'T') ";
            cmd.Parameters.AddWithValue("@Mcr_pk", Mcr_pk);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }


        public string MCRTrimsInventory(AtcwiseFabricInventory order) {
            string MCRNum = "";
            using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {

              
                


            }
                return MCRNum;

        }

        
        public List<RollPropertyViewModel> GetRollPropertyViewModellist(DataTable datatable)
        {
            List<RollPropertyViewModel> list = new List<RollPropertyViewModel>();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                RollPropertyViewModel rollPropertyViewModel = new RollPropertyViewModel();

                rollPropertyViewModel.Roll_PK = System.Decimal.Parse(datatable.Rows[i]["Roll_PK"].ToString());
                rollPropertyViewModel.RollNum = datatable.Rows[i]["RollNum"].ToString();
                rollPropertyViewModel.LocationName = datatable.Rows[i]["LocationName"].ToString();
                rollPropertyViewModel.DocumentNum = datatable.Rows[i]["DocumentNum"].ToString();
                rollPropertyViewModel.AddedVia = datatable.Rows[i]["AddedVia"].ToString();
                rollPropertyViewModel.DeliveredVia = datatable.Rows[i]["DeliveredVia"].ToString();
                rollPropertyViewModel.AYard = System.Decimal.Parse(datatable.Rows[i]["AYard"].ToString());
                rollPropertyViewModel.MarkerType = datatable.Rows[i]["MarkerType"].ToString();
                rollPropertyViewModel.WidthGroup = datatable.Rows[i]["WidthGroup"].ToString();
                rollPropertyViewModel.ShadeGroup = datatable.Rows[i]["ShadeGroup"].ToString();
                rollPropertyViewModel.ShrinkageGroup = datatable.Rows[i]["ShrinkageGroup"].ToString();
                rollPropertyViewModel.IsCut = datatable.Rows[i]["IsCut"].ToString();
                rollPropertyViewModel.IsPresent = datatable.Rows[i]["IsPresent"].ToString();
                rollPropertyViewModel.Location_Pk = System.Decimal.Parse(datatable.Rows[i]["Location_Pk"].ToString());
                rollPropertyViewModel.SkuDet_PK = System.Decimal.Parse(datatable.Rows[i]["SkuDet_PK"].ToString());
                rollPropertyViewModel.IsDelivered = datatable.Rows[i]["IsDelivered"].ToString();
                rollPropertyViewModel.ASN = datatable.Rows[i]["ASN"].ToString();
                rollPropertyViewModel.LaysheetNUM = datatable.Rows[i]["LaysheetNUM"].ToString();
                rollPropertyViewModel.CutPlanNUM = datatable.Rows[i]["CutPlanNUM"].ToString();
                rollPropertyViewModel.itemDescription = datatable.Rows[i]["itemDescription"].ToString();

                list.Add(rollPropertyViewModel);
            }
            return list;
        }


        public Boolean   insertRollProperty(RollPropertyJson Model)
        {
            bool status = false;
            using (ArtWebApp.DataModels.ArtEntitiesnew entymew = new DataModels.ArtEntitiesnew())
            {
                String OldMarkerType = "";
                String OldShadeGroup = "";
                String OldShrinkageGroup = "";
                String OldWidthGroup = "";
                var q = from rollmstr in entymew.FabricRollmasters
                        where rollmstr.Roll_PK == Model.Roll_PK
                        select rollmstr;
                foreach (var element in q)
                {
                    OldMarkerType = element.MarkerType;
                    OldShadeGroup = element.ShadeGroup;
                    OldShrinkageGroup = element.ShrinkageGroup;
                    OldWidthGroup = element.WidthGroup;

                    //element.MarkerType = "";
                    //element.ShadeGroup = "";
                    //element.ShrinkageGroup = "";
                    //element.WidthGroup = "";
                }


                var location = entymew.RollInventoryMasters.Where(u => u.Roll_PK == Model.Roll_PK && u.IsPresent == "Y").Select(u => u.Location_Pk).FirstOrDefault();

                RollPropertyChangeMaster rollPropertyChangeMaster = new DataModels.RollPropertyChangeMaster();
                rollPropertyChangeMaster.Roll_PK = Model.Roll_PK;
                rollPropertyChangeMaster.NewMarkerType = Model.MarkerType;
                rollPropertyChangeMaster.NewShadeGroup = Model.ShadeGroup;
                rollPropertyChangeMaster.NewShrinkageGroup = Model.ShrinkageGroup;
                rollPropertyChangeMaster.NewWidthGroup = Model.WidthGroup;
                var fabroll = entymew.FabricRollmasters.Find(Model.Roll_PK);
                rollPropertyChangeMaster.ShadeGroup = OldShadeGroup;
                rollPropertyChangeMaster.ShrinkageGroup = OldShrinkageGroup;
                rollPropertyChangeMaster.NewMarkerType = OldMarkerType;
                rollPropertyChangeMaster.ShadeGroup = OldShadeGroup;
                rollPropertyChangeMaster.Location_PK = int.Parse(location.ToString());
                rollPropertyChangeMaster.AddedDate = DateTime.Now;
                rollPropertyChangeMaster.AddedBy = HttpContext.Current.Session["Username"].ToString();
                rollPropertyChangeMaster.IsApproved = "N";
                entymew.RollPropertyChangeMasters.Add(rollPropertyChangeMaster);
                entymew.SaveChanges();
                status = true;
            }

            return status;

        }







        public List<RollPropertApprovalModel> GetRollPropertApprovalModellist(DataTable datatable)
        {
            List<RollPropertApprovalModel> list = new List<RollPropertApprovalModel>();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                RollPropertApprovalModel rollPropertApprovalModel = new RollPropertApprovalModel();
                rollPropertApprovalModel.Roll_PK = System.Decimal.Parse(datatable.Rows[i]["Roll_PK"].ToString());
                rollPropertApprovalModel.FabricRollChangeID = System.Decimal.Parse(datatable.Rows[i]["FabricRollChangeID"].ToString());
                rollPropertApprovalModel.RollNum = datatable.Rows[i]["RollNum"].ToString();
                rollPropertApprovalModel.LocationName = datatable.Rows[i]["LocationName"].ToString();
                rollPropertApprovalModel.DocumentNum = datatable.Rows[i]["DocumentNum"].ToString();
                rollPropertApprovalModel.AddedVia = datatable.Rows[i]["AddedVia"].ToString();
                rollPropertApprovalModel.DeliveredVia = datatable.Rows[i]["DeliveredVia"].ToString();
                rollPropertApprovalModel.AYard = System.Decimal.Parse(datatable.Rows[i]["AYard"].ToString());
                rollPropertApprovalModel.MarkerType = datatable.Rows[i]["MarkerType"].ToString();
                rollPropertApprovalModel.WidthGroup = datatable.Rows[i]["WidthGroup"].ToString();
                rollPropertApprovalModel.ShadeGroup = datatable.Rows[i]["ShadeGroup"].ToString();
                rollPropertApprovalModel.ShrinkageGroup = datatable.Rows[i]["ShrinkageGroup"].ToString();
                rollPropertApprovalModel.IsCut = datatable.Rows[i]["IsCut"].ToString();
                rollPropertApprovalModel.IsPresent = datatable.Rows[i]["IsPresent"].ToString();
                rollPropertApprovalModel.Location_Pk = System.Decimal.Parse(datatable.Rows[i]["Location_Pk"].ToString());
                rollPropertApprovalModel.SkuDet_PK = System.Decimal.Parse(datatable.Rows[i]["SkuDet_PK"].ToString());
                rollPropertApprovalModel.IsDelivered = datatable.Rows[i]["IsDelivered"].ToString();
                rollPropertApprovalModel.ASN = datatable.Rows[i]["ASN"].ToString();
                rollPropertApprovalModel.itemDescription = datatable.Rows[i]["itemDescription"].ToString();
                rollPropertApprovalModel.NewMarkerType = datatable.Rows[i]["NewMarkerType"].ToString();
                rollPropertApprovalModel.NewShrinkageGroup = datatable.Rows[i]["NewShrinkageGroup"].ToString();
                rollPropertApprovalModel.NewShadeGroup = datatable.Rows[i]["NewShadeGroup"].ToString();
                rollPropertApprovalModel.NewWidthGroup = datatable.Rows[i]["NewWidthGroup"].ToString();
                rollPropertApprovalModel.IsApproved = datatable.Rows[i]["IsApproved"].ToString();
                rollPropertApprovalModel.AddedBy = datatable.Rows[i]["AddedBy"].ToString();
                rollPropertApprovalModel.AddedDate = System.DateTime.Parse(datatable.Rows[i]["AddedDate"].ToString());
                rollPropertApprovalModel.LaysheetNUM = datatable.Rows[i]["LaysheetNUM"].ToString();
                rollPropertApprovalModel.CutPlanNUM = datatable.Rows[i]["CutPlanNUM"].ToString();
                rollPropertApprovalModel.itemDescription1 = datatable.Rows[i]["itemDescription1"].ToString();
                list.Add(rollPropertApprovalModel);
            }
            return list;
        }
        public RollPropertApprovalModelMaster GetRollPropertApprovalModelMasterData(int id)
        {
            RollPropertApprovalModelMaster rollPropertApprovalModelMaster = new RollPropertApprovalModelMaster();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RollPropertyApprovalPending_SP";
            cmd.Parameters.AddWithValue("@location_pk", id);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.CommandType = CommandType.StoredProcedure;
            dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            rollPropertApprovalModelMaster.RollPropertApprovalModellist = GetRollPropertApprovalModellist(dt);
            return rollPropertApprovalModelMaster;
        }



        public void ApproveRollPropertyChange(int id)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                RollPropertyChangeMaster rollPropertyChangeMaster = enty.RollPropertyChangeMasters.Find(id);




                if (rollPropertyChangeMaster == null)
                {

                }
                else
                {


                    if (rollPropertyChangeMaster.IsApproved == "N")
                    {
                        rollPropertyChangeMaster.IsApproved = "Y";
                        rollPropertyChangeMaster.ApprovedBy = "";
                        rollPropertyChangeMaster.ApprovedDate = DateTime.Now;



                        var q = from rollmstr in enty.FabricRollmasters
                                where rollmstr.Roll_PK == rollPropertyChangeMaster.Roll_PK
                                select rollmstr;
                        foreach (var element in q)
                        {
                            element.MarkerType = rollPropertyChangeMaster.NewMarkerType;
                            element.ShadeGroup = rollPropertyChangeMaster.NewShadeGroup;
                            element.ShrinkageGroup = rollPropertyChangeMaster.NewShrinkageGroup;
                            element.WidthGroup = rollPropertyChangeMaster.NewWidthGroup;


                        }
                    }

                }

                enty.SaveChanges();
            }

        }



        public void CancelRollPropertyChange(int id)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                RollPropertyChangeMaster rollPropertyChangeMaster = enty.RollPropertyChangeMasters.Find(id);




                if (rollPropertyChangeMaster == null)
                {

                }
                else
                {


                    if (rollPropertyChangeMaster.IsApproved == "N")
                    {
                        rollPropertyChangeMaster.IsApproved = "Y";
                        rollPropertyChangeMaster.ApprovedBy = "";
                        rollPropertyChangeMaster.ApprovedDate = DateTime.Now;



                        var q = from rollmstr in enty.FabricRollmasters
                                where rollmstr.Roll_PK == rollPropertyChangeMaster.Roll_PK
                                select rollmstr;
                        foreach (var element in q)
                        {
                            element.MarkerType = rollPropertyChangeMaster.MarkerType;
                            element.ShadeGroup = rollPropertyChangeMaster.ShadeGroup;
                            element.ShrinkageGroup = rollPropertyChangeMaster.ShrinkageGroup;
                            element.WidthGroup = rollPropertyChangeMaster.WidthGroup;


                        }
                    }

                }

                enty.SaveChanges();
            }

        }




    }




    public class RollTransfertoGstockRepo
    {
        public List<RollTransfertoGstockModel> GetRollTransfertoGstockModellist(DataTable datatable)
        {
            List<RollTransfertoGstockModel> list = new List<RollTransfertoGstockModel>();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                RollTransfertoGstockModel rollTransfertoGstockModel = new RollTransfertoGstockModel();
                rollTransfertoGstockModel.Roll_PK = System.Decimal.Parse(datatable.Rows[i]["Roll_PK"].ToString());
                rollTransfertoGstockModel.RollNum = datatable.Rows[i]["RollNum"].ToString();
                rollTransfertoGstockModel.itemDescription = datatable.Rows[i]["itemDescription"].ToString();
                rollTransfertoGstockModel.UOM = datatable.Rows[i]["UOM"].ToString();
                rollTransfertoGstockModel.Remark = datatable.Rows[i]["Remark"].ToString();
                rollTransfertoGstockModel.AShrink = datatable.Rows[i]["AShrink"].ToString();
                rollTransfertoGstockModel.AShade = datatable.Rows[i]["AShade"].ToString();
                rollTransfertoGstockModel.AWidth = datatable.Rows[i]["AWidth"].ToString();
                rollTransfertoGstockModel.AYard = System.Decimal.Parse(datatable.Rows[i]["AYard"].ToString());
                rollTransfertoGstockModel.SupplierDocnum = datatable.Rows[i]["SupplierDocnum"].ToString();
                rollTransfertoGstockModel.Atc_id = System.Decimal.Parse(datatable.Rows[i]["Atc_id"].ToString());
                rollTransfertoGstockModel.DocumentNum = datatable.Rows[i]["DocumentNum"].ToString();
                rollTransfertoGstockModel.Location_Pk = System.Decimal.Parse(datatable.Rows[i]["Location_Pk"].ToString());
                rollTransfertoGstockModel.IsPresent = datatable.Rows[i]["IsPresent"].ToString();
                list.Add(rollTransfertoGstockModel);
            }
            return list;
        }
        public RollTransfertoGstockModelMaster GetRollTransfertoGstockModelMasterData(int location_PK, int Skudet_PK)
        {
            RollTransfertoGstockModelMaster rollTransfertoGstockModelMaster = new RollTransfertoGstockModelMaster();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetTransferRollToStock_SP";
            cmd.Parameters.AddWithValue("@Location_PK", location_PK);
            cmd.Parameters.AddWithValue("@SkuDet_PK", Skudet_PK);
            cmd.CommandType = CommandType.StoredProcedure;
           
            dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            rollTransfertoGstockModelMaster.RollTransfertoGstockModellist = GetRollTransfertoGstockModellist(dt);
            return rollTransfertoGstockModelMaster;
        }
        public DataTable GetfabricinsideTransferToGstock(int Id)
        {
            RollTransfertoGstockModelMaster rollTransfertoGstockModelMaster = new RollTransfertoGstockModelMaster();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ISNULL(SkuRawMaterialMaster.RMNum, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' (' + ISNULL(SkuRawmaterialDetail.ColorCode, ' ')+ ' )' AS itemDescription, SkuRawmaterialDetail.SkuDet_PK, 
                         TransferToGstockDetails.TransferToGSTock_PK
FROM            TransferToGstockDetails INNER JOIN
                         SkuRawmaterialDetail ON TransferToGstockDetails.FromSkudet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (Template_Master.ItemGroup_PK = 1)
GROUP BY ISNULL(SkuRawMaterialMaster.RMNum, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' (' + ISNULL(SkuRawmaterialDetail.ColorCode, ' ')+ ' )', SkuRawmaterialDetail.SkuDet_PK, 
                         TransferToGstockDetails.TransferToGSTock_PK
HAVING(TransferToGstockDetails.TransferToGSTock_PK = @TransferToGSTock_PK)";
            cmd.Parameters.AddWithValue("@TransferToGSTock_PK", Id);
           
     
            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
          
            return dt;
        }

       

        public String InsertGstockRoll(RollTransfertoGstockModelMaster model)
        {
            String msg = "";
            String documentnum = "";
            decimal locationpk = 0;
          
                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                {
                    var q1 = from domaster in enty.TransferToGstockMasters
                             where domaster.TransferToGSTock_PK == model.TransferToGSTock_PK
                             select new
                             {
                                 domaster.Location_Pk,
                                 domaster.TransferNumber
                             };
                    foreach(var element in q1)
                    {
                    documentnum = element.TransferNumber;
                    locationpk= decimal.Parse( element.Location_Pk.ToString());
                }

              
                foreach (RollTransfertoGstockModel rollmodel in model.RollTransfertoGstockModellist)
            {


                if (rollmodel.IsSelected == true)
                {

                    if (!enty.RollInventoryMasters.Any(f => f.DocumentNum == documentnum.ToString() && f.Location_Pk == locationpk && f.Roll_PK == rollmodel.Roll_PK))
                    {


                        //creates a roll on the new location with is present as N

                        RollInventoryMaster rvinvmstr = new RollInventoryMaster();

                        rvinvmstr.Addeddate = DateTime.Now;
                        rvinvmstr.DocumentNum = documentnum;
                        rvinvmstr.AddedVia = "GT";
                        rvinvmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                        rvinvmstr.Location_Pk = locationpk;
                        rvinvmstr.Roll_PK = rollmodel.Roll_PK;
                        rvinvmstr.IsPresent = "W";
                            enty.RollInventoryMasters.Add(rvinvmstr);
                            enty.SaveChanges();

                        var q = from rllinvdata in enty.RollInventoryMasters
                                where rllinvdata.Roll_PK == rollmodel.Roll_PK && rllinvdata.IsPresent == "Y"
                                select rllinvdata;
                        foreach (var element in q)
                        {
                            element.IsPresent = "N";
                            element.DeliveredVia = documentnum;
                            element.NewRollInventory_PK = rvinvmstr.RollInventory_PK;

                        }
                       enty.SaveChanges();

                    }


                }

            }
            }

            return msg;
        }

        

    }










}