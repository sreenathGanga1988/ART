using ArtWebApp.Areas.Quality.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Quality
{
    public class QualityRepo
    {
    }


    public class QualityDashboardRepo
    {

        public QualityDashBoardViewmodel GetQualityDashBoardViewmodel()
        {
            QualityDashBoardViewmodel reportDataModel = new QualityDashBoardViewmodel();

            reportDataModel.PendingvalidationData = GetPendingvalidation(0);
            reportDataModel.PendingInspection = GetPendingInspection(0);
            reportDataModel.PendingGrouping = GetPendingGrouping(0);

            return reportDataModel;
        }



        public DataTable GetPendingvalidation(int atcid = 0)
        {
            DataTable dt = new DataTable();






            using (SqlCommand cmd = new SqlCommand(@"SELECT SupplierDocumentMaster.Containernum, SupplierDocumentMaster.AtracotrackingNum, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS itemDescription, SupplierDocumentMaster.SupplierDocnum, ProcurementMaster.PONum, COUNT(FabricRollmaster.Roll_PK) AS PendingRolls FROM SkuRawmaterialDetail INNER JOIN SkuRawMaterialMaster ON 
                                SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN FabricRollmaster INNER JOIN SupplierDocumentMaster 
                                ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK
                                INNER JOIN ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN ProcurementMaster ON 
                                ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId
                                WHERE (FabricRollmaster.IsSaved = 'N') AND (SupplierDocumentMaster.SupplierETA >CONVERT (DATETIME, '2017-08-08 00:00:00', 102)) 
                                GROUP BY SupplierDocumentMaster.Containernum, SupplierDocumentMaster.AtracotrackingNum,
                                AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' '), SupplierDocumentMaster.SupplierDocnum, ProcurementMaster.PONum
                           "))
            {
               
               
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
            }













            return dt;
        }


        public DataTable GetPendingInspection(int atcid = 0)
        {
            DataTable dt = new DataTable();






            using (SqlCommand cmd = new SqlCommand(@"   SELECT SupplierDocumentMaster.Containernum, SupplierDocumentMaster.AtracotrackingNum, AtcMaster.AtcNum, 
						   SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + 
						   ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + 
						   ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS itemDescription, 
						   SupplierDocumentMaster.SupplierDocnum, ProcurementMaster.PONum, COUNT(FabricRollmaster.Roll_PK) AS PendingRolls FROM SkuRawmaterialDetail INNER JOIN SkuRawMaterialMaster
						    ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN FabricRollmaster INNER JOIN SupplierDocumentMaster ON
							 FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
							  ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk 
							  INNER JOIN AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId WHERE (FabricRollmaster.IsSaved!= N'N') AND (FabricRollmaster.IsApproved != N'A') AND 
							  (SupplierDocumentMaster.SupplierETA > CONVERT (DATETIME, '2016-12-20 00:00:00', 102)) GROUP BY SupplierDocumentMaster.Containernum, SupplierDocumentMaster.AtracotrackingNum, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + 
							  ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' +
							   ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + 
							   ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' '), SupplierDocumentMaster.SupplierDocnum, ProcurementMaster.PONum
                           "))
            {

               
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
            }













            return dt;
        }


        public DataTable GetPendingGrouping(int atcid = 0)
        {
            DataTable dt = new DataTable();






            using (SqlCommand cmd = new SqlCommand(@"   	   SELECT        SupplierDocumentMaster.Containernum, SupplierDocumentMaster.AtracotrackingNum, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS itemDescription, 
                         SupplierDocumentMaster.SupplierDocnum, ProcurementMaster.PONum, count (FabricRollmaster.Roll_PK) AS PendingRolls
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         FabricRollmaster INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId
WHERE        (FabricRollmaster.IsSaved != N'N') AND (FabricRollmaster.IsApproved!= N'N')  AND ((FabricRollmaster.WidthGroup IS NULL) OR (FabricRollmaster.ShadeGroup IS NULL) OR 
                         (FabricRollmaster.ShrinkageGroup IS NULL))AND (SupplierDocumentMaster.SupplierETA > CONVERT(DATETIME, '2016-12-20 00:00:00', 102))
GROUP BY SupplierDocumentMaster.Containernum, SupplierDocumentMaster.AtracotrackingNum, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' '), SupplierDocumentMaster.SupplierDocnum, 
                         ProcurementMaster.PONum "))
            {


                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
            }













            return dt;
        }

    }

}