using ArtWebApp.Areas.Accounts.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Accounts
{
    public class AccountRepo
    {


        public AccountdashBoard GetAccountDashBoard()
        {
            AccountdashBoard accountdashBoard = new AccountdashBoard();
            accountdashBoard.PendingGeneralPoforPayable = GetSpoPending();
            accountdashBoard.PendingPOforPayable = GetPoPending();
            accountdashBoard.PendingservicePoforPosting = GetServicePoPending();
            accountdashBoard.PendingLocalExternalSalesforPosting = GetlocalExternalsalesPending();
            
            accountdashBoard.PendingInternalSalesForDebiting = GetPendingInternalSalesForDebiting();

            return accountdashBoard;
    }


        public DataTable GetSpoPending()
        {
            DataTable dt = new DataTable();


            String query = @"SELECT        SPO_Pk, SupplierName, SPONum, POQty, ReceivedQty, ExtraQty, InvoicedQty, ReceivedQty - InvoicedQty AS BancetoInvoice
FROM      (SELECT        StockPOMaster.SPO_Pk, StockPOMaster.SPONum, SupplierMaster.SupplierName, SUM(StockPODetails_1.POQty) AS POQty, ISNULL
                             ((SELECT        SUM(ReceivedQty) AS Expr1
                                 FROM            StockMRNDetails
                                 WHERE        (SPO_PK = StockPOMaster.SPO_Pk)), 0) AS ReceivedQty, ISNULL
                             ((SELECT        SUM(ExtraQty) AS Expr1
                                 FROM            StockMRNDetails AS StockMRNDetails_1
                                 WHERE        (SPO_PK = StockPOMaster.SPO_Pk)), 0) AS ExtraQty, ISNULL
                             ((SELECT        SUM(SupplierStockInvoiceDetail.InvoiceQty) AS Expr1
                                 FROM            StockPODetails INNER JOIN
                                                          SupplierStockInvoiceDetail ON StockPODetails.SPODetails_PK = SupplierStockInvoiceDetail.SPODetails_PK AND StockPODetails.SPODetails_PK = SupplierStockInvoiceDetail.SPODetails_PK
                                 GROUP BY StockPODetails.SPO_PK
                                 HAVING        (StockPODetails.SPO_PK = StockPOMaster.SPO_Pk)), 0) AS InvoicedQty, StockPOMaster.AddedDate
FROM            StockPOMaster INNER JOIN
                         SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         StockPODetails AS StockPODetails_1 ON StockPOMaster.SPO_Pk = StockPODetails_1.SPO_PK
GROUP BY StockPOMaster.SPO_Pk, StockPOMaster.SPONum, SupplierMaster.SupplierName, StockPOMaster.IsApproved, StockPOMaster.AddedDate
HAVING        (StockPOMaster.IsApproved = N'Y')) AS tt
WHERE        (ReceivedQty - InvoicedQty > 0)";
            using (SqlCommand cmd = new SqlCommand())
            {



               
                cmd.CommandText = query;
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            }
            return dt;
        }




        public DataTable GetPoPending()
        {
            DataTable dt = new DataTable();


            String query = @"SELECT        TT.PO_Pk, TT.PONum, SupplierMaster.SupplierName, TT.POQty, TT.ReceiptQty, TT.invoiced, TT.LastMRNDATE, TT.ReceiptQty - TT.invoiced AS BalToInvoice, PaymentTermMaster.PaymentTermCode
FROM            (SELECT        ProcurementMaster.PO_Pk, SUM(ProcurementDetails_1.POQty) AS POQty, ISNULL
                                                        ((SELECT        SUM(MrnDetails.ReceiptQty) AS Expr1
                                                            FROM            MrnDetails INNER JOIN
                                                                                     ProcurementDetails ON MrnDetails.PODet_PK = ProcurementDetails.PODet_PK
                                                            WHERE        (ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk)), 0) AS ReceiptQty, ISNULL
                                                        ((SELECT        SUM(SupplierInvoiceDetail.InvoiceQty) AS Expr1
                                                            FROM            SupplierInvoiceDetail INNER JOIN
                                                                                     ProcurementDetails AS ProcurementDetails_2 ON SupplierInvoiceDetail.PODet_PK = ProcurementDetails_2.PODet_PK
                                                            GROUP BY ProcurementDetails_2.PO_Pk
                                                            HAVING        (ProcurementDetails_2.PO_Pk = ProcurementMaster.PO_Pk)), 0) AS invoiced,
                                                        (SELECT        MAX(MrnMaster.AddedDate) AS Expr1
                                                          FROM            MrnMaster INNER JOIN
                                                                                    MrnDetails AS MrnDetails_1 ON MrnMaster.Mrn_PK = MrnDetails_1.Mrn_PK
                                                          GROUP BY MrnMaster.Po_PK
                                                          HAVING         (MrnMaster.Po_PK = ProcurementMaster.PO_Pk)) AS LastMRNDATE, ProcurementMaster.IsNormal, ProcurementMaster.IsApproved, ProcurementMaster.IsDeleted, 
                                                    ProcurementMaster.MarkCompleted, ProcurementMaster.PONum, ProcurementMaster.Supplier_Pk, ProcurementMaster.PaymentTermID
                          FROM            ProcurementMaster INNER JOIN
                                                    ProcurementDetails AS ProcurementDetails_1 ON ProcurementMaster.PO_Pk = ProcurementDetails_1.PO_Pk
                          GROUP BY ProcurementMaster.PO_Pk, ProcurementMaster.IsNormal, ProcurementMaster.IsApproved, ProcurementMaster.IsDeleted, ProcurementMaster.MarkCompleted, ProcurementMaster.PONum, 
                                                    ProcurementMaster.Supplier_Pk, ProcurementMaster.PaymentTermID,ProcurementMaster.AddedDate
                          HAVING         (ProcurementMaster.IsNormal = N'Y') AND (ProcurementMaster.IsApproved = N'Y') AND (ProcurementMaster.IsDeleted = N'N') AND (ProcurementMaster.Supplier_Pk <> 1113)AND (ProcurementMaster.MarkCompleted = N'N') AND (ProcurementMaster.AddedDate > CONVERT(DATETIME, '2017-07-07 00:00:00', 102))) AS TT INNER JOIN
                         SupplierMaster ON TT.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         PaymentTermMaster ON TT.PaymentTermID = PaymentTermMaster.PaymentTermID
WHERE        (TT.ReceiptQty - TT.invoiced >0)";
            using (SqlCommand cmd = new SqlCommand())
            {



               
                cmd.CommandText = query;
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            }
            return dt;
        }




        public DataTable GetServicePoPending()
        {
            DataTable dt = new DataTable();


            String query = @"SELECT ServicePOMaster.ServicePO_PK, ServicePOMaster.ServicePOnumber, ServicePOMaster.Description, ServicePOMaster.DebitFrom, ServicePOMaster.DebitName, ServiceTypeMaster.ServiceType, 
                         ServicePOMaster.Amount, CurrencyMaster.CurrencyCode, ServicePOMaster.IsApproved
FROM            ServicePOMaster INNER JOIN
                         ServiceTypeMaster ON ServicePOMaster.ServiceType_Pk = ServiceTypeMaster.ServiceType_Pk INNER JOIN
                         CurrencyMaster ON ServicePOMaster.CurrencyID = CurrencyMaster.CurrencyID
WHERE(ServicePOMaster.IsApproved = N'Y') AND(ServicePOMaster.IsPosted = N'N')";
            using (SqlCommand cmd = new SqlCommand())
            {



              
                cmd.CommandText = query;
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            }
            return dt;
        }



        public DataTable GetlocalExternalsalesPending()
        {
            DataTable dt = new DataTable();


            String query = @"SELECT        InventorySalesMaster.SalesDO_PK, InventorySalesMaster.SalesDONum, InventorySalesMaster.SalesDate, InventorySalesMaster.SalesDODate, LocationMaster.LocationName AS FromLocation, 
                         LocalBuyerMaster.LocalBuyerName AS Buyer
FROM            InventorySalesMaster INNER JOIN
                         LocationMaster ON InventorySalesMaster.FromLocation_PK = LocationMaster.Location_PK INNER JOIN
                         LocalBuyerMaster ON InventorySalesMaster.ToLocation_PK = LocalBuyerMaster.LocalBuyer_PK
WHERE        (InventorySalesMaster.DoType = N'External') AND (InventorySalesMaster.IsDebited = N'N')";
            using (SqlCommand cmd = new SqlCommand())
            {



              
                cmd.CommandText = query;
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            }
            return dt;
        }

        public DataTable GetPendingInternalSalesForDebiting()
        {
            DataTable dt = new DataTable();


            String query = @"SELECT        InventorySalesMaster.SalesDO_PK, InventorySalesMaster.SalesDONum, InventorySalesMaster.SalesDate, InventorySalesMaster.SalesDODate, LocationMaster.LocationName AS FromLocation, 
                         LocationMaster_1.LocationName AS ToLocation, SUM(InventorySalesDetail.DeliveryQty * InventorySalesDetail.CuRate) AS TotalValueinUSD 
FROM            InventorySalesMaster INNER JOIN
                         LocationMaster ON InventorySalesMaster.FromLocation_PK = LocationMaster.Location_PK INNER JOIN
                         LocationMaster AS LocationMaster_1 ON InventorySalesMaster.FromLocation_PK = LocationMaster_1.Location_PK INNER JOIN
                         InventorySalesDetail ON InventorySalesMaster.SalesDO_PK = InventorySalesDetail.SalesDO_PK
WHERE        (InventorySalesMaster.DoType = N'Internal') AND (InventorySalesMaster.IsDebited = N'N')
GROUP BY InventorySalesMaster.SalesDO_PK, InventorySalesMaster.SalesDONum, InventorySalesMaster.SalesDate, InventorySalesMaster.SalesDODate, LocationMaster.LocationName, LocationMaster_1.LocationName";
            using (SqlCommand cmd = new SqlCommand())
            {




                cmd.CommandText = query;
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            }
            return dt;
        }


       
    }
}