using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ArtWebApp.DataModels;
using System.Collections;

namespace ArtWebApp.BLL.InventoryBLL
{
    public static class RollTransactionBLL
    {
        /// <summary>
        /// get all the fanbric details of a mrn
        /// </summary>
        /// <param name="mrn_pk"></param>
        /// <returns></returns>
        public static DataTable getfabricdetailsofMRN(int mrn_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"
SELECT        SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + '  ' + SkuRawMaterialMaster.Weight + SkuRawMaterialMaster.Width + '  ' + ProcurementDetails.SupplierColor
                          + '  ' + ProcurementDetails.SupplierSize AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK, ProcurementDetails.PODet_PK, MrnDetails.MrnDet_PK
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         ProcurementDetails ON SkuRawmaterialDetail.SkuDet_PK = ProcurementDetails.SkuDet_PK INNER JOIN
                         MrnDetails ON ProcurementDetails.PODet_PK = MrnDetails.PODet_PK
WHERE        (ItemGroupMaster.ItemGroupName = N'Fabric') AND (MrnDetails.Mrn_PK = @mrn_pk)";
                cmd.Parameters.AddWithValue("@mrn_pk", mrn_pk);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }






        /// <summary>
        /// Get All Fabric DO of Atc
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static DataTable getFabricDoDetails(int atcid)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        DeliveryOrderMaster.DO_PK, DeliveryOrderMaster.DONum
FROM            DeliveryOrderDetails INNER JOIN
                         InventoryMaster ON DeliveryOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         DeliveryOrderMaster ON DeliveryOrderDetails.DO_PK = DeliveryOrderMaster.DO_PK
WHERE        (ItemGroupMaster.ItemGroupName = 'Fabric') AND (DeliveryOrderMaster.AtcID = @atcid)
GROUP BY DeliveryOrderMaster.DO_PK, DeliveryOrderMaster.DONum";
                cmd.Parameters.AddWithValue("@atcid", atcid);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }

        /// <summary>
        /// Get all types of fabric inside a ASN
        /// </summary>
        /// <param name="mrn_pk"></param>
        /// <returns></returns>
        public static DataTable getfabricinsideASN(int asn, int atcid)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"
SELECT DISTINCT 
                         SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + '  ' + SkuRawMaterialMaster.Weight + SkuRawMaterialMaster.Width + '  ' + ProcurementDetails.SupplierColor
                          + '   ' + ProcurementDetails.SupplierSize AS ItemDescription, SkuRawmaterialDetail.SkuDet_PK
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ItemGroupMaster ON Template_Master.ItemGroup_PK = ItemGroupMaster.ItemGroupID INNER JOIN
                         ProcurementDetails ON SkuRawmaterialDetail.SkuDet_PK = ProcurementDetails.SkuDet_PK INNER JOIN
                         FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK AND ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk
WHERE        (ItemGroupMaster.ItemGroupName = N'Fabric') AND (FabricRollmaster.SupplierDoc_pk = @asn) and   (SkuRawMaterialMaster.Atc_id = @atcid)
GROUP BY SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + '  ' + SkuRawMaterialMaster.Weight + SkuRawMaterialMaster.Width + '  ' + ProcurementDetails.SupplierColor
                          + '   ' + ProcurementDetails.SupplierSize, SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.Atc_id";
                cmd.Parameters.AddWithValue("@asn", asn);
                cmd.Parameters.AddWithValue("@atcid", atcid);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }



        public static DataTable CreateRollRows(int numofrows)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("RollNum", typeof(String));
            dt.Columns.Add("Qty", typeof(String));
            dt.Columns.Add("UOM", typeof(String));
            dt.Columns.Add("Remark", typeof(String));


            for (int i = 1; i <= numofrows; i++)
            {


                dt.Rows.Add("Roll#" + i.ToString(), "0", "YDS", "Good Condition");

            }

            return dt;


        }




        /// <summary>
        /// Get All Fabric Rolls of A Item based on the sskudetpk of that item
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static DataTable getFabricRollofAItemPK(int iitemPK)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, ProcurementMaster.PONum, 
                         ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                         + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS itemDescription, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup, FabricRollmaster.AYard, FabricRollmaster.SkuDet_PK, FabricRollmaster.IsDelivered
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         SupplierMaster ON SupplierDocumentMaster.Supplier_pk = SupplierMaster.Supplier_PK INNER JOIN
                         InventoryMaster ON FabricRollmaster.SkuDet_PK = InventoryMaster.SkuDet_Pk
WHERE        (InventoryMaster.InventoryItem_PK = @iitemPK) AND (FabricRollmaster.IsDelivered <> N'Y')";
                cmd.Parameters.AddWithValue("@iitemPK", iitemPK);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }









        /// <summary>
        /// Get All Fabric Rolls of A Item based on the sskudetpk of that item and will show those rolls which
        /// have same cutwidth and Shrinkage of that cutorder
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static DataTable getFabricRollofAItemPKandCutorder(int iitemPK ,int cutid)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, ProcurementMaster.PONum, 
                         ISNULL(SkuRawMaterialMaster.Composition, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, N' ') 
                         + ' ' + ISNULL(ProcurementDetails.SupplierSize, N' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, N' ') AS itemDescription, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup, FabricRollmaster.AYard, AtcMaster.AtcNum, InventoryMaster.InventoryItem_PK, InventoryMaster.Location_PK
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         SupplierMaster ON SupplierDocumentMaster.Supplier_pk = SupplierMaster.Supplier_PK INNER JOIN
                         InventoryMaster ON FabricRollmaster.SkuDet_PK = InventoryMaster.SkuDet_Pk INNER JOIN
                         CurrencyMaster ON SupplierMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                         CutOrderMaster ON FabricRollmaster.ShrinkageGroup = CutOrderMaster.Shrinkage AND FabricRollmaster.WidthGroup = CutOrderMaster.CutWidth
WHERE        (InventoryMaster.InventoryItem_PK = @iitemPK) AND (CutOrderMaster.CutID = @cutid)  AND (FabricRollmaster.IsDelivered <> N'Y')";
                cmd.Parameters.AddWithValue("@iitemPK", iitemPK);
                cmd.Parameters.AddWithValue("@cutid", cutid);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


    }




    public class FabricRollEntryMRN
    {
        public List<FabricRollmasterDataDetails> Rolldatacollection { get; set; }

        public RollInventoryData rollinvdata { get; set; }

        //RollInventoryData rollinvdata, List<FabricRollmasterDataDetails> FabricRollmasterDataDetails
        public void insertMrnRollData()
        {
            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {
                foreach (FabricRollmasterDataDetails rolldata in Rolldatacollection)
                {


                    FabricRollmaster fbmstr = new FabricRollmaster();
                    fbmstr.MRnDet_PK = rolldata.MRnDet_PK;
                    fbmstr.RollNum = rolldata.RollNum;
                    fbmstr.Qty = rolldata.Qty;
                    fbmstr.UOM = rolldata.UOM;
                    fbmstr.Remark = rolldata.Remark;
                    fbmstr.SShrink = rolldata.SShrink;
                    fbmstr.SYard = rolldata.SYard;
                    fbmstr.SShade = rolldata.SShade;
                    fbmstr.SWidth = rolldata.SWidth;
                    fbmstr.AShrink = rolldata.AShrink;
                    fbmstr.AShade = rolldata.AShade;
                    fbmstr.AWidth = rolldata.AWidth;
                    fbmstr.AYard = rolldata.AYard;
                    fbmstr.SkuDet_PK = rolldata.SkuDet_PK;
                    fbmstr.IsSaved = "N";
                    entry.FabricRollmasters.Add(fbmstr);
                    entry.SaveChanges();


                    RollInventoryMaster rvinvmstr = new RollInventoryMaster();

                    rvinvmstr.Addeddate = rollinvdata.Addeddate;
                    rvinvmstr.DocumentNum = rollinvdata.DocumentNum;
                    rvinvmstr.AddedVia = rollinvdata.AddedVia;
                    rvinvmstr.AddedBy = rollinvdata.AddedBy;
                    rvinvmstr.Location_Pk = rollinvdata.Location_Pk;
                    rvinvmstr.Roll_PK = fbmstr.Roll_PK;
                    rvinvmstr.IsPresent = "Y";

                    entry.SaveChanges();


                }


            }
        }



        public void UpdateRollMRNDetails()
        {
            foreach (FabricRollmasterDataDetails rolldata in Rolldatacollection)
            {
                using (ArtEntitiesnew entry = new ArtEntitiesnew())
                {
                    var q = from fbrmstr in entry.FabricRollmasters
                            where fbrmstr.Roll_PK == rolldata.Roll_PK
                            select fbrmstr;

                    foreach (var element in q)
                    {
                        element.MRnDet_PK = rolldata.MRnDet_PK;


                    }

                    entry.SaveChanges();
                }

            }
        }


        public void UpdateRollInspectiondata()
        {
            foreach (FabricRollmasterDataDetails rolldata in Rolldatacollection)
            {
                using (ArtEntitiesnew entry = new ArtEntitiesnew())
                {
                    var q = from fbrmstr in entry.FabricRollmasters
                            where fbrmstr.Roll_PK == rolldata.Roll_PK
                            select fbrmstr;

                    foreach (var element in q)
                    {
                        element.AShade = rolldata.AShade;
                        element.AShrink = rolldata.AShrink;
                        element.AWidth = rolldata.AWidth;
                        element.AYard = rolldata.AYard;
                        element.AGsm = rolldata.AGSM;
                        element.IsApproved = "N";
                        element.IsSaved = "Y";


                    }

                    entry.SaveChanges();
                }

            }
        }

        public void UpproveRollInspectiondata()
        {
            foreach (FabricRollmasterDataDetails rolldata in Rolldatacollection)
            {
                using (ArtEntitiesnew entry = new ArtEntitiesnew())
                {
                    var q = from fbrmstr in entry.FabricRollmasters
                            where fbrmstr.Roll_PK == rolldata.Roll_PK
                            select fbrmstr;

                    foreach (var element in q)
                    {

                        element.IsApproved = "A";

                        element.MarkerType = rolldata.MarkerType;
                        element.IsAcceptable = rolldata.IsAccepted;
                        element.TotalDefect = rolldata.TotalDefect;
                        element.TotalDefecton100 = rolldata.TotalDefecton100;
                        element.TotalPoint = rolldata.TotalPoint;
                        element.TotalPointon100yard = rolldata.TotalPointon100yard;
                    }

                    entry.SaveChanges();
                }

            }
        }



        public void UpproveRollInspectionGroup()
        {
            foreach (FabricRollmasterDataDetails rolldata in Rolldatacollection)
            {
                using (ArtEntitiesnew entry = new ArtEntitiesnew())
                {
                    var q = from fbrmstr in entry.FabricRollmasters
                            where fbrmstr.Roll_PK == rolldata.Roll_PK
                            select fbrmstr;

                    foreach (var element in q)
                    {

                        element.IsApproved = "A";

                        element.WidthGroup = rolldata.widthgroup;
                        element.ShadeGroup = rolldata.shadegroup;
                        element.ShrinkageGroup = rolldata.shrinkagegroup;
                    }

                    entry.SaveChanges();
                }

            }
        }

    }




    public class FabricRollmasterDataDetails
    {
        public int Roll_PK { get; set; }
        public string RollNum { get; set; }

        public int PoDet_PK { get; set; }
        public int MRnDet_PK { get; set; }
        public Decimal Qty { get; set; }
        public string UOM { get; set; }
        public string Remark { get; set; }
        public string SShrink { get; set; }
        public string SYard { get; set; }
        public string SShade { get; set; }
        public string SWidth { get; set; }
        public string AShrink { get; set; }
        public string AShade { get; set; }
        public string AWidth { get; set; }
        public string AYard { get; set; }
        public string AGSM { get; set; }
        public string SGSM { get; set; }


        public string TotalDefect { get; set; }
        public string TotalDefecton100 { get; set; }
        public string TotalPoint { get; set; }
        public string TotalPointon100yard { get; set; }
        //public string SGSM { get; set; }

        public string widthgroup { get; set; }
        public string shadegroup { get; set; }
        public string shrinkagegroup { get; set; }

        public string IsAccepted { get; set; }
        public string MarkerType { get; set; }


        public int SkuDet_PK
        {
            get { return getSKUDetPk(MRnDet_PK); }
            set { SkuDet_PK = value; }
        }

        public int getSKUDetPk(int mrndetpk)
        {
            int skudetpk = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from um in enty.MrnDetails
                         where um.MrnDet_PK == mrndetpk

                         select um.SkuDet_PK).FirstOrDefault();

                skudetpk = int.Parse(q.ToString());
            }

            return skudetpk;
        }


        /// <summary>
        /// get roll details of a mrn
        /// </summary>
        /// <param name="mrn_pk"></param>
        /// <returns></returns>
        public DataTable getRollDetailsofMRN(int mrn_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.Remark, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, 
                         FabricRollmaster.SWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard,FabricRollmaster.SGsm,FabricRollmaster.AGsm,FabricRollmaster.Lotnum
FROM            FabricRollmaster INNER JOIN
                         MrnDetails ON FabricRollmaster.MRnDet_PK = MrnDetails.MrnDet_PK
WHERE        (MrnDetails.Mrn_PK = @mrn_pk) and FabricRollmaster.IsSaved='N'";
                cmd.Parameters.AddWithValue("@mrn_pk", mrn_pk);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        public DataTable getRollDetailsofASNandSKUDetPK(int asn_pk,int skudet_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        Roll_PK, RollNum, Qty, UOM, Remark, SShrink, SYard, SShade, SWidth, AShrink, AShade, AWidth, AYard, SGsm, AGsm, SkuDet_PK, SupplierDoc_pk,Lotnum
FROM            FabricRollmaster
WHERE        (IsSaved = 'N') AND (SkuDet_PK = @skudet_pk) AND (SupplierDoc_pk = @asn_pk)";
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }






        public DataTable getRollDetailsofASNandMrnDetpk(int asn_pk, int mrndetpk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.Remark, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, 
                         FabricRollmaster.SWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.SGsm, FabricRollmaster.AGsm, FabricRollmaster.SkuDet_PK, 
                         FabricRollmaster.SupplierDoc_pk, FabricRollmaster.LOTnum, MrnDetails.MrnDet_PK, FabricRollmaster.MRnDet_PK AS Expr1
FROM            FabricRollmaster INNER JOIN
                         MrnDetails ON FabricRollmaster.SkuDet_PK = MrnDetails.SkuDet_PK
WHERE        (FabricRollmaster.SupplierDoc_pk = @asn_pk) AND (MrnDetails.MrnDet_PK = @mrndetpk) AND (FabricRollmaster.MRnDet_PK = 0)";
                cmd.Parameters.AddWithValue("@mrndetpk", mrndetpk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }













        /// <summary>
        /// show the roll details even if saved(used for edit)
        /// </summary>
        /// <param name="asn_pk"></param>
        /// <param name="skudet_pk"></param>
        /// <returns></returns>
        public DataTable getRollDetailsofASNandSKUDetPKForEdit(int asn_pk, int skudet_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        Roll_PK, RollNum, Qty, UOM, Remark, SShrink, SYard, SShade, SWidth, AShrink, AShade, AWidth, AYard, SGsm, AGsm, SkuDet_PK, SupplierDoc_pk,Lotnum
FROM            FabricRollmaster
WHERE        (SkuDet_PK = @skudet_pk) AND (SupplierDoc_pk = @asn_pk)";
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }








       /// <summary>
       /// get the MRN Value
       /// </summary>
       /// <param name="mrndet_pk"></param>
       /// <returns></returns>
        public DataTable GetMrnData(int mrndet_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        (MrnDetails.ReceiptQty+MrnDetails.ExtraQty) as receiptQty, UOMMaster.UomCode, MrnDetails.MrnDet_PK
FROM            MrnDetails INNER JOIN
                         UOMMaster ON MrnDetails.Uom_PK = UOMMaster.Uom_PK
WHERE        (MrnDetails.MrnDet_PK = @mrndet_pk)";
                cmd.Parameters.AddWithValue("@mrndet_pk", mrndet_pk);
               
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }






        public DataTable getfullRollDetailsofASNandSKUDetPK(int asn_pk, int skudet_pk)
        {








            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum, FabricRollmaster.SGsm, FabricRollmaster.AGsm, 
                         FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.TotalDefect, FabricRollmaster.TotalDefecton100, FabricRollmaster.TotalPoint, 
                         FabricRollmaster.TotalPointon100yard,FabricRollmaster.Lotnum
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE         (FabricRollmaster.IsSaved = N'Y') AND (FabricRollmaster.SupplierDoc_pk = @asn_pk) and  (FabricRollmaster.SkuDet_PK = @skudet_pk)
";
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }








        public DataTable getfullRollDetailsofASNandSKUDetPKofaWidth(int asn_pk, int skudet_pk,ArrayList widtharray)
        {

            String condition = "";
            for (int i = 0; i < widtharray.Count; i++)
            {
                if (i == 0)
                {
                    condition = " and ( FabricRollmaster.AWidth='" + widtharray[i].ToString().Trim()+"'";
                }
                else
                {
                    condition = condition + "  or FabricRollmaster.AWidth='" + widtharray[i].ToString().Trim() + "'";
                }



            }

          if(condition!="")
            {
                condition = condition + " )";
            }


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum, FabricRollmaster.SGsm, FabricRollmaster.AGsm, 
                         FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.TotalDefect, FabricRollmaster.TotalDefecton100, FabricRollmaster.TotalPoint, 
                         FabricRollmaster.TotalPointon100yard,FabricRollmaster.Lotnum
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE         (FabricRollmaster.IsSaved = N'Y') AND (FabricRollmaster.SupplierDoc_pk = @asn_pk) and  (FabricRollmaster.SkuDet_PK = @skudet_pk) 
"+condition;
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        public DataTable getfullRollDetailsofASNandSKUDetPKofShrinkage(int asn_pk, int skudet_pk, ArrayList widtharray)
        {

            String condition = "";
            for (int i = 0; i < widtharray.Count; i++)
            {
                if (i == 0)
                {
                    condition = " and ( FabricRollmaster.AShrink='" + widtharray[i].ToString().Trim() + "'";
                }
                else
                {
                    condition = condition + "  or FabricRollmaster.AShrink='" + widtharray[i].ToString().Trim() + "'";
                }



            }

            if (condition != "")
            {
                condition = condition + " )";
            }


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum, FabricRollmaster.SGsm, FabricRollmaster.AGsm, 
                         FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.TotalDefect, FabricRollmaster.TotalDefecton100, FabricRollmaster.TotalPoint, 
                         FabricRollmaster.TotalPointon100yard,FabricRollmaster.Lotnum
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE         (FabricRollmaster.IsSaved = N'Y') AND (FabricRollmaster.SupplierDoc_pk = @asn_pk) and  (FabricRollmaster.SkuDet_PK = @skudet_pk) 
" + condition;
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        public DataTable getfullRollDetailsofASNandSKUDetPKofShade(int asn_pk, int skudet_pk, ArrayList widtharray)
        {

            String condition = "";
            for (int i = 0; i < widtharray.Count; i++)
            {
                if (i == 0)
                {
                    condition = " and ( FabricRollmaster.AShade='" + widtharray[i].ToString().Trim() + "'";
                }
                else
                {
                    condition = condition + "  or FabricRollmaster.AShade='" + widtharray[i].ToString().Trim() + "'";
                }



            }

            if (condition != "")
            {
                condition = condition + " )";
            }


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum, FabricRollmaster.SGsm, FabricRollmaster.AGsm, 
                         FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.TotalDefect, FabricRollmaster.TotalDefecton100, FabricRollmaster.TotalPoint, 
                         FabricRollmaster.TotalPointon100yard,FabricRollmaster.Lotnum
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE         (FabricRollmaster.IsSaved = N'Y') AND (FabricRollmaster.SupplierDoc_pk = @asn_pk) and  (FabricRollmaster.SkuDet_PK = @skudet_pk) 
" + condition;
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


    }


    public class RollInventoryData
    {

        public int Location_Pk { get; set; }
        public string DocumentNum { get; set; }
        public string AddedVia { get; set; }
        public string IsPresent { get; set; }
        public string AddedBy { get; set; }
        public DateTime Addeddate { get; set; }



        public DataTable getRollDetailsofATC(int atcid)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, MrnMaster.MrnNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, 
                         FabricRollmaster.SWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, 
                         FabricRollmaster.IsAcceptable, FabricRollmaster.MarkerType, ProcurementMaster.PONum, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId,FabricRollmaster.Lotnum
FROM            MrnDetails INNER JOIN
                         MrnMaster ON MrnDetails.Mrn_PK = MrnMaster.Mrn_PK INNER JOIN
                         FabricRollmaster ON MrnDetails.MrnDet_PK = FabricRollmaster.MRnDet_PK INNER JOIN
                         ProcurementMaster ON MrnMaster.Po_PK = ProcurementMaster.PO_Pk INNER JOIN
                         SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId
WHERE        (AtcMaster.AtcId = @atc_id) and  (FabricRollmaster.IsApproved = N'N') and  (FabricRollmaster.IsSaved = N'Y')";
                cmd.Parameters.AddWithValue("@atc_id", atcid);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }



        public DataTable getRollDetailsofASN(int asn_pk ,int skudet_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum,FabricRollmaster.Lotnum
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE        (FabricRollmaster.IsApproved = N'N') AND (FabricRollmaster.IsSaved = N'Y') AND (FabricRollmaster.SupplierDoc_pk = @asn_pk)  AND (FabricRollmaster.SkuDet_PK = @skudet_pk)";
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }




        public DataTable getRollDetailsofASNforEdit(int asn_pk, int skudet_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum,FabricRollmaster.Lotnum, FabricRollmaster.TotalDefect, FabricRollmaster.TotalDefecton100, FabricRollmaster.TotalPoint , FabricRollmaster.TotalPointon100yard
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE        (FabricRollmaster.IsApproved = N'A') AND (FabricRollmaster.IsSaved = N'Y') AND (FabricRollmaster.SupplierDoc_pk = @asn_pk)  AND (FabricRollmaster.SkuDet_PK = @skudet_pk)";
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }



        public DataTable GetCompletedataofAsn(int asn_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, 
                         FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId, FabricRollmaster.SupplierDoc_pk, ProcurementMaster.PONum, FabricRollmaster.SGsm, FabricRollmaster.AGsm, 
                         FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.TotalDefect, FabricRollmaster.TotalDefecton100, FabricRollmaster.TotalPoint, 
                         FabricRollmaster.TotalPointon100yard,FabricRollmaster.Lotnum
FROM            AtcMaster INNER JOIN
                         SupplierMaster INNER JOIN
                         ProcurementDetails INNER JOIN
                         FabricRollmaster ON ProcurementDetails.PODet_PK = FabricRollmaster.podet_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk ON SupplierMaster.Supplier_PK = ProcurementMaster.Supplier_Pk ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE        (FabricRollmaster.IsApproved = N'N') AND (FabricRollmaster.IsSaved = N'Y') AND (FabricRollmaster.SupplierDoc_pk = @asn_pk)";
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }





    }




    public class InspectionData
    {
      public DataTable GetDocumentnumber(int atcid)
    {

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = @"SELECT       DISTINCT  (SupplierDocumentMaster.SupplierDocnum +' / '+SupplierDocumentMaster.AtracotrackingNum) AS name, SupplierDocumentMaster.SupplierDoc_pk as pk
FROM            SupplierDocumentMaster INNER JOIN
                         FabricRollmaster ON SupplierDocumentMaster.SupplierDoc_pk = FabricRollmaster.SupplierDoc_pk INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk
WHERE        (ProcurementMaster.AtcId = @Param1)";
            cmd.Parameters.AddWithValue("@Param1", atcid);

            return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }
    }


        public DataTable GetDocumentnumberByMRNDETPK(int mrndetpk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT DISTINCT SupplierDocumentMaster.SupplierDocnum + ' / ' + SupplierDocumentMaster.AtracotrackingNum AS name, SupplierDocumentMaster.SupplierDoc_pk AS pk, MrnDetails.MrnDet_PK
FROM            SupplierDocumentMaster INNER JOIN
                         FabricRollmaster ON SupplierDocumentMaster.SupplierDoc_pk = FabricRollmaster.SupplierDoc_pk INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         MrnDetails ON ProcurementDetails.PODet_PK = MrnDetails.PODet_PK
WHERE        (MrnDetails.MrnDet_PK = @Param2)";
                cmd.Parameters.AddWithValue("@Param2", mrndetpk);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


    }


}
namespace ArtWebApp.BLL.RollBLL
{




}