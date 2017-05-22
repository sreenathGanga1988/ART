using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.DBTransaction.ReportTransactions
{
    public class ReporterTrans
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        /// <summary>
        /// Get All the details of a PO
        /// </summary>
        /// <param name="POnum"></param>
        /// <returns></returns>
        public DataTable GetDORData(int dor_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        SkuRawmaterialDetail.SupplierSize, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemColor, SkuRawMaterialMaster.Composition, 
                         SkuRawMaterialMaster.Construction, SkuRawMaterialMaster.Weight, SkuRawMaterialMaster.Width, UOMMaster.UomName, DeliveryOrderMaster.DONum, DeliveryOrderMaster.DeliveryDate, 
                         DeliveryOrderMaster.ContainerNumber, AtcMaster.AtcNum, DeliveryOrderMaster.BoeNum, LocationMaster.LocationName, LocationMaster.LocationAddress, DeliveryReceiptMaster.DORNum, 
                         DeliveryReceiptMaster.AddedBy, DeliveryReceiptMaster.AddedDate, DeliveryReceiptDetail.ReceivedQty, DeliveryOrderMaster.AddedBy AS Addedby, DeliveryOrderMaster.AddedDate AS DAte, 
                         DeliveryReceiptMaster.DOR_PK, DeliveryReceiptDetail.DORDet_Pk
FROM            UOMMaster INNER JOIN
                         DeliveryReceiptDetail INNER JOIN
                         DeliveryReceiptMaster ON DeliveryReceiptDetail.DOR_PK = DeliveryReceiptMaster.DOR_PK INNER JOIN
                         LocationMaster ON DeliveryReceiptMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         DeliveryOrderMaster INNER JOIN
                         AtcMaster ON DeliveryOrderMaster.AtcID = AtcMaster.AtcId ON DeliveryReceiptMaster.DO_PK = DeliveryOrderMaster.DO_PK INNER JOIN
                         InventoryMaster INNER JOIN
                         SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK ON 
                         DeliveryReceiptDetail.InventoryItem_PK = InventoryMaster.InventoryItem_PK ON UOMMaster.Uom_PK = InventoryMaster.Uom_Pk
WHERE        (DeliveryReceiptMaster.DOR_PK = @Param1)", con);
                cmd.Parameters.AddWithValue("@Param1", dor_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }





        /// <summary>
        /// Get All the details of a PO
        /// </summary>
        /// <param name="POnum"></param>
        /// <returns></returns>
        public DataTable GetDOData(int dopk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        SkuRawmaterialDetail.SupplierSize, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemColor, SkuRawMaterialMaster.Composition, 
                         SkuRawMaterialMaster.Construction, SkuRawMaterialMaster.Weight, SkuRawMaterialMaster.Width, UOMMaster.UomName, DeliveryOrderMaster.DONum, DeliveryOrderMaster.AddedDate, 
                         DeliveryOrderMaster.ContainerNumber, AtcMaster.AtcNum, DeliveryOrderDetails.DeliveryQty, DeliveryOrderDetails.Remark, DeliveryOrderMaster.BoeNum, LocationMaster.LocationName AS FRMLOC, 
                         LocationMaster.LocationAddress AS FRMADD, LocationMaster_1.LocationName AS TOLOC, LocationMaster_1.LocationAddress AS TOADD, DeliveryOrderMaster.AddedBy, 
                         Template_Master.Description AS Tempname, DeliveryOrderMaster.DO_PK, isnull( DeliveryMethodMaster.DeliveryMethod,'')as DeliveryMethod
FROM            SkuRawmaterialDetail INNER JOIN
                         AtcMaster INNER JOIN
                         SkuRawMaterialMaster ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         DeliveryOrderMaster ON AtcMaster.AtcId = DeliveryOrderMaster.AtcID INNER JOIN
                         DeliveryOrderDetails ON DeliveryOrderMaster.DO_PK = DeliveryOrderDetails.DO_PK INNER JOIN
                         InventoryMaster ON SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk AND DeliveryOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         LocationMaster ON DeliveryOrderMaster.FromLocation_PK = LocationMaster.Location_PK INNER JOIN
                         LocationMaster AS LocationMaster_1 ON DeliveryOrderMaster.ToLocation_PK = LocationMaster_1.Location_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         UOMMaster ON InventoryMaster.Uom_Pk = UOMMaster.Uom_PK LEFT OUTER JOIN
                         DeliveryMethodMaster ON DeliveryOrderMaster.Deliverymethod_Pk = DeliveryMethodMaster.Deliverymethod_Pk
WHERE        (DeliveryOrderMaster.DO_PK = @param1)", con);
                cmd.Parameters.AddWithValue("@Param1", dopk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public DataTable GetDORollData(int dopk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.MarkerType, 
                         FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, DORollDetails.DO_PK
FROM            DORollDetails INNER JOIN
                         FabricRollmaster ON DORollDetails.Roll_PK = FabricRollmaster.Roll_PK
WHERE        (DORollDetails.DO_PK = @Param1)
", con);
                cmd.Parameters.AddWithValue("@Param1", dopk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        public DataTable GetDORollDataofSelectedRoll(int dopk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand("GetRollDetail_sp", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Do_Pk", dopk);

                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);



            }
            return dt;
        }




        /// <summary>
        /// Get All the details of a SPO
        /// </summary>
        /// <param name="POnum"></param>
        /// <returns></returns>
        public DataTable GetSmrnData(int Smrn_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"GetStockMRN_SP", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SMrn_pk", Smrn_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        /// <summary>
        /// Get All the details of a SPO
        /// </summary>
        /// <param name="POnum"></param>
        /// <returns></returns>
        public DataTable GetSDOData(int Sdo_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT DeliveryOrderStockMaster.SDONum, DeliveryOrderStockMaster.DeliveryDate, LocationMaster.LocationName AS frmloc, LocationMaster.LocationAddress AS frmadd,
                      LocationMaster_1.LocationName AS toloc, LocationMaster_1.LocationAddress AS toadd, DeliveryOrderStockMaster.ContainerNumber,
                      DeliveryOrderStockMaster.BoeNum, DeliveryOrderStockMaster.AddedBy, DeliveryOrderStockMaster.AddedDate, DeliveryOrderStockMaster.SDO_PK,
                      UOMMaster.UomName, Template_Master.Description AS TEMPLATE, DeliveryOrderStockDetails.DeliveryQty, ISNULL(DeliveryOrderStockDetails.Remark, ' ')
                      AS REMARK, ISNULL(StockPODetails.Composition, '') AS Composition, ISNULL(StockPODetails.Construct, ' ') AS CONSTRUCT, ISNULL(StockPODetails.TemplateColor,
                      ' ') AS TEMPCOLOR, ISNULL(StockPODetails.TemplateSize, ' ') AS TEMPSIZE, ISNULL(StockPODetails.TemplateWidth, ' ') AS TEMPWIDTH,
                      ISNULL(StockPODetails.TemplateWeight, ' ') AS TEMPWEIGHT
FROM DeliveryOrderStockMaster INNER JOIN
                      DeliveryOrderStockDetails ON DeliveryOrderStockMaster.SDO_PK = DeliveryOrderStockDetails.SDO_PK INNER JOIN
                      LocationMaster ON DeliveryOrderStockMaster.FromLocation_PK = LocationMaster.Location_PK INNER JOIN
                      LocationMaster AS LocationMaster_1 ON DeliveryOrderStockMaster.ToLocation_PK = LocationMaster_1.Location_PK INNER JOIN
                      StockInventoryMaster ON DeliveryOrderStockDetails.SInventoryItem_PK = StockInventoryMaster.SInventoryItem_PK INNER JOIN
                      UOMMaster ON StockInventoryMaster.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                      Template_Master ON StockInventoryMaster.Template_PK = Template_Master.Template_PK INNER JOIN
                      StockPODetails ON StockInventoryMaster.SPODetails_PK = StockPODetails.SPODetails_PK
WHERE     (DeliveryOrderStockMaster.SDO_PK = @Param1)",con);


                cmd.Parameters.AddWithValue("@Param1", Sdo_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        

        /// <summary>
        /// Get All the details of a Jobcontract
        /// </summary>
        /// <param name="POnum"></param>
        /// <returns></returns>
        public DataTable GetJCData(int JC_PK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        POPackDetails.POPackId, SUM(POPackDetails.PoQty) AS PoQty, CountryMaster.Description, LocationMaster.LocationName, LocationMaster.LocationAddress, AtcMaster.AtcNum, GarmentCategory.CategoryName, 
                         JobContractMaster.JOBContractNUM, JobContractMaster.AddedDate, JobContractMaster.AddedBy, JobContractDetail.CMvalue, AtcDetails.OurStyle, PoPackMaster.PoPacknum, PoPackMaster.DeliveryDate, 
                         PoPackMaster.BuyerPO, ROUND(SUM(POPackDetails.PoQty) * (JobContractDetail.CMvalue / 12), 2) AS POVAL, isnull( JobContractMaster.Remark,'') as Remark
FROM            POPackDetails INNER JOIN
                         PoPackMaster ON POPackDetails.POPackId = PoPackMaster.PoPackId INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON PoPackMaster.AtcId = AtcMaster.AtcId AND AtcDetails.AtcId = AtcMaster.AtcId INNER JOIN
                         JobContractMaster ON AtcMaster.AtcId = JobContractMaster.AtcID INNER JOIN
                         JobContractDetail ON PoPackMaster.PoPackId = JobContractDetail.PoPackID AND AtcDetails.OurStyleID = JobContractDetail.OurStyleID AND 
                         JobContractMaster.JobContract_pk = JobContractDetail.JobContract_pk INNER JOIN
                         LocationMaster ON JobContractMaster.Location_Pk = LocationMaster.Location_PK INNER JOIN
                         GarmentCategory ON AtcDetails.CategoryID = GarmentCategory.CategoryID INNER JOIN
                         CountryMaster ON LocationMaster.CountryID = CountryMaster.CountryID
GROUP BY POPackDetails.POPackId, CountryMaster.Description, LocationMaster.LocationName, LocationMaster.LocationAddress, AtcMaster.AtcNum, GarmentCategory.CategoryName, JobContractMaster.JOBContractNUM, 
                         JobContractMaster.AddedDate, JobContractMaster.AddedBy, JobContractDetail.CMvalue, AtcDetails.OurStyle, PoPackMaster.PoPacknum, PoPackMaster.DeliveryDate, PoPackMaster.BuyerPO, 
                         JobContractMaster.JobContract_pk, JobContractMaster.Remark
HAVING      (JobContractMaster.JobContract_pk =@Param1 )", con);
                cmd.Parameters.AddWithValue("@Param1", JC_PK);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public DataTable GetSalesDO(int sdo_PK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand("GetSalesDO_SP", con);
                cmd.Parameters.AddWithValue("@SalesDO_PK", sdo_PK);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public DataTable GetLoanReport(int loan_PK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand("GetLoanReportData_sp", con);
                cmd.Parameters.AddWithValue("@Loan_pk", loan_PK);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

    }


    public class AccountReportrans
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        /// <summary>
        ///GEt all detail of a supplierInvoice
        /// </summary>
        /// <param name="supinv_PK"></param>
        /// <returns></returns>
        public DataTable GetSUPInvData(int supinv_PK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        SupplierInvoiceMaster.SupplierInvoiceNum, SupplierInvoiceMaster.AddedBy, SupplierInvoiceMaster.AddedDate, ISNULL(SupplierInvoiceMaster.Remark, '') AS Remark, 
                         ISNULL(SupplierInvoiceMaster.InvoiceDate, '') AS InvoiceDate, ISNULL(SupplierInvoiceMaster.AccountDate, '') AS AccountDate, ISNULL(SupplierInvoiceMaster.IsAdvance, '') AS IsAdvance, 
                         ISNULL(SupplierInvoiceMaster.SupInvnum, '') AS SupInvnum, AtcMaster.AtcNum, ProcurementMaster.PONum, ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, 
                         ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') 
                         + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS itemDescription, ProcurementDetails.POUnitRate, ProcurementDetails.POQty, UOMMaster.UomCode, 
                         CurrencyMaster.CurrencyCode, SupplierInvoiceMaster.SupplierInvoice_PK, SupplierInvoiceDetail.InvoiceQty, SupplierInvoiceDetail.InvoiceQty * SupplierInvoiceDetail.Unitrate AS InvoiceValue, 
                         SupplierMaster.SupplierName
FROM            SupplierInvoiceMaster INNER JOIN
                         SupplierInvoiceDetail ON SupplierInvoiceMaster.SupplierInvoice_PK = SupplierInvoiceDetail.SupplierInvoice_PK INNER JOIN
                         ProcurementDetails ON SupplierInvoiceDetail.PODet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         CurrencyMaster ON SupplierInvoiceMaster.Currency_PK = CurrencyMaster.CurrencyID INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id AND SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         SupplierMaster ON SupplierInvoiceMaster.Supplier_Pk = SupplierMaster.Supplier_PK
WHERE        (SupplierInvoiceMaster.SupplierInvoice_PK = @Param1)
", con);
                cmd.Parameters.AddWithValue("@Param1", supinv_PK);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }




        /// <summary>
        ///GEt all detail of a supplierInvoice for stockpo
        /// </summary>
        /// <param name="spurinv_PK"></param>
        /// <returns></returns>
        public DataTable GetSPURInvData(int spurinv_PK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        SupplierStockInvoiceMaster.SupplierStockInvoice_PK, SupplierStockInvoiceMaster.SupplierStockInvoiceNum, SupplierStockInvoiceMaster.AddedBy, SupplierStockInvoiceMaster.AddedDate, 
                         SupplierMaster.SupplierName, ISNULL(SupplierStockInvoiceMaster.Remark, '') AS Expr1, SupplierStockInvoiceDetail.InvoiceQty, SupplierStockInvoiceDetail.Unitrate, SupplierStockInvoiceDetail.InvCurrency, 
                         CurrencyMaster.CurrencyID, 
                         StockPODetails.Composition + '' + StockPODetails.Construct + '' + StockPODetails.TemplateColor + '' + StockPODetails.TemplateSize + '' + StockPODetails.TemplateWidth + '' + StockPODetails.TemplateWeight AS
                          Description, StockPOMaster.SPONum
FROM            SupplierStockInvoiceMaster INNER JOIN
                         SupplierStockInvoiceDetail ON SupplierStockInvoiceMaster.SupplierStockInvoice_PK = SupplierStockInvoiceDetail.SupplierStockInvoice_PK INNER JOIN
                         CurrencyMaster ON SupplierStockInvoiceMaster.Currency_PK = CurrencyMaster.CurrencyID INNER JOIN
                         SupplierMaster ON SupplierStockInvoiceMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         StockPODetails ON SupplierStockInvoiceDetail.SPODetails_PK = StockPODetails.SPODetails_PK INNER JOIN
                         Template_Master ON StockPODetails.Template_PK = Template_Master.Template_PK INNER JOIN
                         StockPOMaster ON StockPODetails.SPO_PK = StockPOMaster.SPO_Pk
WHERE        (SupplierStockInvoiceMaster.SupplierStockInvoice_PK = @Param1)
", con);
                cmd.Parameters.AddWithValue("@Param1", spurinv_PK);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }







        public DataTable GetBuyerInvData(int inv_PK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        InvoiceMaster.InvoiceNum, BuyerMaster.BuyerName, LocationMaster.LocationName, BankMaster.BankName, ShipmentHandOverMaster.ShipmentHandOverCode, AtcMaster.AtcNum, 
                         PoPackMaster.PoPacknum + '/' + PoPackMaster.BuyerPO AS PoPacknum, InvoiceDetail.FOB, ShipmentHandOverDetails.ShippedQty, AtcDetails.OurStyle, InvoiceDetail.InvoiceQty, InvoiceDetail.CartonNum, 
                         InvoiceMaster.Invoice_PK
FROM            PoPackMaster INNER JOIN
                         AtcMaster INNER JOIN
                         BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID INNER JOIN
                         AtcDetails ON AtcMaster.AtcId = AtcDetails.AtcId INNER JOIN
                         JobContractDetail INNER JOIN
                         InvoiceMaster INNER JOIN
                         InvoiceDetail ON InvoiceMaster.Invoice_PK = InvoiceDetail.Invoice_PK INNER JOIN
                         BankMaster ON InvoiceMaster.Bank_PK = BankMaster.Bank_PK INNER JOIN
                         ShipmentHandOverDetails ON InvoiceDetail.ShipmentHandOver_PK = ShipmentHandOverDetails.ShipmentHandOver_PK INNER JOIN
                         ShipmentHandOverMaster ON ShipmentHandOverDetails.ShipmentHandMaster_PK = ShipmentHandOverMaster.ShipmentHandMaster_PK ON 
                         JobContractDetail.JobContractDetail_pk = ShipmentHandOverDetails.JobContractDetail_pk INNER JOIN
                         LocationMaster ON InvoiceMaster.Location_PK = LocationMaster.Location_PK ON AtcDetails.OurStyleID = JobContractDetail.OurStyleID ON PoPackMaster.PoPackId = JobContractDetail.PoPackID
WHERE        (InvoiceMaster.Invoice_PK =@Param1)
", con);
                cmd.Parameters.AddWithValue("@Param1", inv_PK);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }








        //public DataTable GetCostingData()
        //{
        //    DataTable dt = new DataTable();

        //    using (SqlConnection con = new SqlConnection(connStr))
        //    {
        //        con.Open();




        //        SqlCommand cmd = new SqlCommand(@"GetCostingData_SP", con);

        //        cmd.CommandType = CommandType.StoredProcedure;



        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        dt.Load(rdr);



        //    }
        //    return dt;
        //}


        public DataTable GetCostingData(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"GetCostingDataOFATC_SP", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@atcid", atcid);


                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }








    } 



    public class ProductionReportsTrans
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        /// <summary>
        /// get shipment thandover data of all atc of a shipment
        /// </summary>
        /// <param name="shipmenthandover_pk"></param>
        /// <returns></returns>
        public DataTable GetShipmentHandoverData( int shipmenthandover_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = @"SELECT        ShipmentHandOverMaster.ShipmentHandOverCode, LocationMaster.LocationName, ShipmentHandOverDetails.ShipmentHandOverDate, JobContractMaster.JobContract_pk, 
                         PoPackMaster.PoPacknum + ' /' + PoPackMaster.BuyerPO AS PoPacknum, AtcMaster.AtcNum, AtcDetails.OurStyle, ShipmentHandOverDetails.ShippedQty, ShipmentHandOverDetails.AddedBy, 
                         ShipmentHandOverDetails.AddedDate, ShipmentHandOverDetails.ShipmentHandOver_PK, JobContractMaster.JOBContractNUM, PoPackMaster.BuyerPO, PoPackMaster.AtcId
FROM            AtcMaster INNER JOIN
                         AtcDetails ON AtcMaster.AtcId = AtcDetails.AtcId INNER JOIN
                         JobContractDetail INNER JOIN
                         JobContractMaster ON JobContractDetail.JobContract_pk = JobContractMaster.JobContract_pk INNER JOIN
                         ShipmentHandOverMaster INNER JOIN
                         LocationMaster ON ShipmentHandOverMaster.Location_Pk = LocationMaster.Location_PK INNER JOIN
                         ShipmentHandOverDetails ON ShipmentHandOverMaster.ShipmentHandMaster_PK = ShipmentHandOverDetails.ShipmentHandMaster_PK ON 
                         JobContractDetail.JobContractDetail_pk = ShipmentHandOverDetails.JobContractDetail_pk INNER JOIN
                         PoPackMaster ON JobContractDetail.PoPackID = PoPackMaster.PoPackId ON AtcDetails.OurStyleID = JobContractDetail.OurStyleID
WHERE        (ShipmentHandOverMaster.ShipmentHandMaster_PK = @Param1)


";


                cmd.CommandText = Query1;
                cmd.Parameters.AddWithValue("@Param1", shipmenthandover_pk);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        /// <summary>
        /// gets the production report of a atc 
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public DataTable GetShipmentHandoverofAtc(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = @"SELECT        Distinct ShipmentHandOverMaster.ShipmentHandOverCode, ShipmentHandOverMaster.ShipmentHandMaster_PK
FROM            ShipmentHandOverMaster INNER JOIN
                         ShipmentHandOverDetails ON ShipmentHandOverMaster.ShipmentHandMaster_PK = ShipmentHandOverDetails.ShipmentHandMaster_PK INNER JOIN
                         JobContractDetail ON ShipmentHandOverDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk INNER JOIN
                         PoPackMaster ON JobContractDetail.PoPackID = PoPackMaster.PoPackId
WHERE        (PoPackMaster.AtcId = @Param1)";


                cmd.CommandText = Query1;
                cmd.Parameters.AddWithValue("@Param1", atcid);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }











        /// <summary>
        /// gets the production report of a atc 
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public DataTable GetProductionReportofAtc(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = @"SELECT        AtcMaster.AtcNum, ProductionReportDetails.WashedQty, ProductionReportDetails.SewnQty, JobContractMaster.JOBContractNUM, JobContractDetail.OurStyleID, LocationMaster.LocationName, 
                         PoPackMaster.PoPacknum, AtcDetails.OurStyle, PoPackMaster.BuyerPO, ISNULL(ProductionReportDetails.ShipmentHandOverDate, '') AS ProductionDate, ProductionReportDetails.PackedQty, 
                         ProductionReportDetails.ShippedQty, ProductionReportDetails.AddedBy, ProductionReportDetails.AddedDate, PoPackMaster.AtcId
FROM            PoPackMaster INNER JOIN
                         JobContractDetail INNER JOIN
                         ProductionReportDetails ON JobContractDetail.JobContractDetail_pk = ProductionReportDetails.JobContractDetail_pk INNER JOIN
                         JobContractMaster ON JobContractDetail.JobContract_pk = JobContractMaster.JobContract_pk INNER JOIN
                         LocationMaster ON JobContractMaster.Location_Pk = LocationMaster.Location_PK ON PoPackMaster.PoPackId = JobContractDetail.PoPackID INNER JOIN
                         AtcDetails ON JobContractDetail.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId
WHERE        (PoPackMaster.AtcId = @Param1)


";


                cmd.CommandText = Query1;
                cmd.Parameters.AddWithValue("@Param1", atcid);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        /// <summary>
        /// Get the details of job contract
        /// </summary>
        /// <param name="jc_pk"></param>
        /// <returns></returns>

        public DataTable GetJOBContractCM(int jc_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = "GetJobContractCMData_SP";


                cmd.CommandText = Query1;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@jc_pk", jc_pk);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        /// Get the details of job contract Others
        /// </summary>
        /// <param name="jc_pk"></param>
        /// <returns></returns>

        public DataTable GetJOBContractOthers(int jc_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = "GetJobContractOptional_SP";


                cmd.CommandText = Query1;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("jobcontractoptional_pk", jc_pk);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


    }


    public class QualityReportTran
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        
        public DataTable GetASNREport(int asn_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = "GetASNReport_SP";


                cmd.CommandText = Query1;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                cmd.Parameters.AddWithValue("@skudet_PK", 0);
                
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public DataTable GetASNREport(int asn_pk,int skudet_PK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = "GetASNReport_SP";


                cmd.CommandText = Query1;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@asn_pk", asn_pk);
                cmd.Parameters.AddWithValue("@skudet_PK", skudet_PK);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }








        public DataTable GetRollsActionPending(String pendingstage)
        {

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = "GetQualityPendingReport_SP";


                cmd.CommandText = Query1;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Pending", pendingstage);
                

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;


        }
















    }

    public class ShippingReportTran
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;

        public DataTable GetDOC(int doc_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = @"SELECT        ProcurementDetails.PODet_PK, ProcurementDetails.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierColor, ProcurementDetails.SupplierSize, UOMMaster.UomCode, ProcurementDetails.POQty, ProcurementMaster.PONum, ProcurementMaster.PO_Pk, 
                         DocMaster.DocNum, DocMaster.ContainerNum, DocMaster.BOENum, DocMaster.Remark, DocMaster.InhouseDate, DocMaster.ETADate, DocDetails.Qty, DocDetails.Eta, DocDetails.Donumber, 
                         DocDetails.AddedBy, DocDetails.AddedDate, DocMaster.AddedBy AS DocAddedBy, DocMaster.AddedDate AS DocAddedDate, SupplierMaster.SupplierName, AtcMaster.AtcNum, DocDetails.ExtraQty, 
                         DocMaster.ADNType
FROM            ProcurementDetails INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         DocDetails ON ProcurementDetails.PODet_PK = DocDetails.PODet_Pk INNER JOIN
                         DocMaster ON DocDetails.Doc_Pk = DocMaster.Doc_Pk INNER JOIN
                         SupplierMaster ON DocMaster.Supplier_PK = SupplierMaster.Supplier_PK INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId
WHERE        (DocMaster.Doc_Pk = @doc_pk)";


                cmd.CommandText = Query1;
                cmd.Parameters.AddWithValue("@doc_pk", doc_pk);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }
        public DataTable GetIMP(int IMPPK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = "GetImportDocument_SP";
                cmd.CommandText = Query1;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IMPPK", IMPPK);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public DataTable GetCourierDetailBetween(DateTime  fromdate , DateTime todate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = "CourierData_SP";
                cmd.CommandText = Query1;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate",fromdate);
                cmd.Parameters.AddWithValue("@TODate",todate);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }






    }


    public class GeneralReportTrans
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;

        public DataTable GetCuttingDetails(int doc_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = "GetCuttingTicket_SP";


                cmd.CommandText = Query1;
                
                cmd.Parameters.AddWithValue("@cutid", doc_pk);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        public DataTable GetASQShuffle(int ashufle_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = "GetASQShuffledetails_SP";


                cmd.CommandText = Query1;

                cmd.Parameters.AddWithValue("@asqshufflePK", ashufle_pk);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }
    }





}