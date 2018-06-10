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

        public DataTable GetATCwiseFabricInventory(int Locid, int Atcid)
        {
            DataTable dt = new DataTable();
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        InventoryMaster.InventoryItem_PK, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum, InventoryMaster.SkuDet_Pk,SkuRawMaterialMaster.Template_pk,
                SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                 ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, InventoryMaster.OnhandQty, InventoryMaster.OnhandQty as PhysicalQty,
                LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value,AtcMaster.AtcID,LocationMaster.Location_PK,
                0 as DiffQty,InventoryMaster.CURate as ActualRate
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
and AtcMaster.AtcId not in(select Atcid from MCRDetails where Location_pk =@Locid)
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize,
                  ProcurementDetails.SupplierColor, UOMMaster.UomCode";
            cmd.Parameters.AddWithValue("@Locid", Locid);
            cmd.Parameters.AddWithValue("@Atcid", Atcid);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }
        public DataTable GetATCwiseTrimsInventory(int Locid, int Atcid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, InventoryMaster.SkuDet_Pk,SkuRawMaterialMaster.Template_pk,
                SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, InventoryMaster.OnhandQty,
InventoryMaster.OnhandQty as PhysicalQty,
                AtcMaster.AtcNum, LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value,
AtcMaster.AtcID,LocationMaster.Location_PK,0 as DiffQty,InventoryMaster.CURate as ActualRate
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
                 AND (InventoryMaster.OnhandQty > 0) and AtcMaster.AtcId not in(select Atcid from MCRDetails where Location_pk =@Locid) ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize,
                  ProcurementDetails.SupplierColor, UOMMaster.UomCode";
            cmd.Parameters.AddWithValue("@Locid", Locid);
            cmd.Parameters.AddWithValue("@Atcid", Atcid);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }
        public DataTable GetMrnDetailsforAllocation(int MRN_PK,int Rack_PK)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        InventoryMaster.InventoryItem_PK, SkuRawMaterialMaster.RMNum, InventoryMaster.SkuDet_Pk,SkuRawMaterialMaster.Template_pk,
                SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty,
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
				and MrnMaster.Mrn_PK =@MRN_PK and MrnDetails.IsRackAllocateDone is NULL
                 ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize,
                  ProcurementDetails.SupplierColor, UOMMaster.UomCode ";
            cmd.Parameters.AddWithValue("@MRN_PK", MRN_PK);
            cmd.Parameters.AddWithValue("@Rack_PK", Rack_PK);
            


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }
        public DataTable MCRFabricInventory(int Locid, int Atcid)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        Atcid,InventoryItem_pk, Location_pk, ReceivedQty, McrDetails_pk, DeliveredQty, Onhandqty, PhysicalQty, DiffQty, AddedDate, Addedby, ApprovedBy, ApprovedDate, RMNum, Description, 
                         ItemColor, SupplierColor, UOM, CU_Rate, ActualCU_Rate, type
FROM            MCRDetails
WHERE        (Atcid = @Atcid) AND (Location_pk = @Locid) AND (type = 'F')";
            cmd.Parameters.AddWithValue("@Locid", Locid);
            cmd.Parameters.AddWithValue("@Atcid", Atcid);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }

        public DataTable MCRTrimsInventory(int Locid, int Atcid)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        Atcid,InventoryItem_pk, Location_pk, ReceivedQty, McrDetails_pk, DeliveredQty, Onhandqty, PhysicalQty, DiffQty, AddedDate, Addedby, ApprovedBy, ApprovedDate, RMNum, Description, 
                         ItemColor, SupplierColor, UOM, CU_Rate, ActualCU_Rate, type
FROM            MCRDetails
WHERE        (Atcid = @Atcid) AND (Location_pk = @Locid) AND (type = 'T')";
            cmd.Parameters.AddWithValue("@Locid", Locid);
            cmd.Parameters.AddWithValue("@Atcid", Atcid);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }

        public DataTable ApprovedMCRFabricInventory(int Locid, int Atcid)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        Atcid,InventoryItem_pk, Location_pk, ReceivedQty, McrDetails_pk, DeliveredQty, Onhandqty, PhysicalQty, DiffQty, AddedDate, Addedby, ApprovedBy, ApprovedDate, RMNum, Description, 
                         ItemColor, SupplierColor, UOM, CU_Rate, ActualCU_Rate, type
FROM            MCRDetails
WHERE        (Atcid = @Atcid) AND (Location_pk = @Locid) AND (type = 'F') and (Isapproved='Y')";
            cmd.Parameters.AddWithValue("@Locid", Locid);
            cmd.Parameters.AddWithValue("@Atcid", Atcid);


            dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            return dt;
        }

        public DataTable ApprovedMCRTrimsInventory(int Locid, int Atcid)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        Atcid,InventoryItem_pk, Location_pk, ReceivedQty, McrDetails_pk, DeliveredQty, Onhandqty, PhysicalQty, DiffQty, AddedDate, Addedby, ApprovedBy, ApprovedDate, RMNum, Description, 
                         ItemColor, SupplierColor, UOM, CU_Rate, ActualCU_Rate, type
FROM            MCRDetails
WHERE        (Atcid = @Atcid) AND (Location_pk = @Locid) AND (type = 'T')  and (Isapproved='Y')";
            cmd.Parameters.AddWithValue("@Locid", Locid);
            cmd.Parameters.AddWithValue("@Atcid", Atcid);


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


        public void insertRollProperty(RollPropertyJson Model)
        {
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
            }

        }







        public List<RollPropertApprovalModel> GetRollPropertApprovalModellist(DataTable datatable)
        {
            List<RollPropertApprovalModel> list = new List<RollPropertApprovalModel>();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                RollPropertApprovalModel rollPropertApprovalModel = new RollPropertApprovalModel();
                rollPropertApprovalModel.Roll_PK = System.Decimal.Parse(datatable.Rows[i]["Roll_PK"].ToString());
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