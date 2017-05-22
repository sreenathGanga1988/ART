﻿using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.AccountsBLL
{
    public static class InvoicingBLL
    {

        /// <summary>
        /// Get All the ATc Against which PO has been raised for a supplier
        /// </summary>
        /// <param name="supplier_pk"></param>
        /// <returns></returns>
        public static DataTable GetAtcofSupllier(int supplier_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        AtcMaster.AtcId, AtcMaster.AtcNum
FROM            AtcMaster INNER JOIN
                         ProcurementMaster ON AtcMaster.AtcId = ProcurementMaster.AtcId
WHERE        (ProcurementMaster.Supplier_Pk = @supplier_pk)
GROUP BY AtcMaster.AtcId, AtcMaster.AtcNum";
                cmd.Parameters.AddWithValue("@supplier_pk", supplier_pk);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }

        /// <summary>
        /// Get all POs given By the Supplier Against a the list of Atc
        /// </summary>
        /// <param name="Atclist"></param>
        /// <param name="supplier_pk"></param>
        /// <returns></returns>
        public static DataTable GetPOsOfATCSupplier(ArrayList Atclist, int supplier_pk)
        {
            DataTable dt = new DataTable();
            string condition = "AND";

            for (int i = 0; i < Atclist.Count; i++)
            {
                if (i == 0)
                {
                    condition = " AtcId =" + Atclist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or AtcId=" + Atclist[i].ToString().Trim();
                }



            }

            if (condition != "AND")
            {
                String query = @"SELECT        PO_Pk, PONum
FROM            ProcurementMaster AS ProcurementMaster_1
WHERE       (Supplier_Pk = @Param3) AND(" + condition + ")";
                //DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
                //dt = pktrans.getPodetails(query);


                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Param3", supplier_pk);

                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
                }


            }
            return dt;

        }


        public static DataTable GetPOInvoicedDetails(ArrayList polist)
        {
            DataTable dt = new DataTable();
            string condition = "where";

            for (int i = 0; i < polist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " ProcurementDetails.PO_Pk=" + polist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or ProcurementDetails.PO_Pk=" + polist[i].ToString().Trim();
                }



            }

            if (condition.Trim () != "where")
            {
//                String query = @"SELECT        SkuDet_PK, PODet_PK,PONum, RMNum, Description, ItemColor, ItemSize, SupplierColor, SupplierSize, UomCode, POQty, ReceivedQty , ExtraQty ,    (CASE POQty WHEN 0 THEN 0   ELSE ((ExtraQty/POQty)*100)  END) AS ExtraPer,    InvQty  , ReceivedQty - InvQty AS BaltoINV, POUnitRate, Uom_PK,CurrencyCode,TT.POType
//FROM           ( SELECT        ProcurementDetails.PODet_PK,ProcurementMaster.PONum, ProcurementDetails.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
//                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
//                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierColor, ProcurementDetails.SupplierSize, UOMMaster.UomCode, ProcurementDetails.POQty, ProcurementDetails.POUnitRate,  CurrencyMaster.CurrencyID, CurrencyMaster.CurrencyCode,
//                         ProcurementDetails.CURate, ProcurementDetails.Uom_PK,
//                             (SELECT        ISNULL(SUM(ReceiptQty), 0) AS Expr1
//                               FROM            MrnDetails
//                               WHERE        (PODet_PK = ProcurementDetails.PODet_PK) AND (SkuDet_PK = ProcurementDetails.SkuDet_PK)) AS ReceivedQty,
//							   (SELECT        ISNULL(SUM(ExtraQty), 0) AS Expr1
//                               FROM            MrnDetails
//                               WHERE        (PODet_PK = ProcurementDetails.PODet_PK) AND (SkuDet_PK = ProcurementDetails.SkuDet_PK)) AS ExtraQty,
//                             (SELECT        ISNULL(SUM(InvoiceQty), 0) AS Expr1
//                               FROM            SupplierInvoiceDetail
//                               WHERE        (PODet_PK = ProcurementDetails.PODet_PK)) AS InvQty,ProcurementMaster.POType
//FROM            ProcurementDetails INNER JOIN
//                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
//                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
//                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
//                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
//                         CurrencyMaster ON ProcurementMaster.CurrencyID = CurrencyMaster.CurrencyID " + condition + " ) AS tt";



                String query = @"SELECT        SkuDet_PK, PODet_PK,PONum, RMNum, Description, ItemColor, ItemSize, SupplierColor, SupplierSize, UomCode, POQty, ReceivedQty , ExtraQty ,    (CASE POQty WHEN 0 THEN 0   ELSE ((ExtraQty/POQty)*100)  END) AS ExtraPer,    InvQty  , ReceivedQty - InvQty AS BaltoINV, POUnitRate, Uom_PK,CurrencyCode,TT.POType,tt.LastMRNDATE
FROM           ( SELECT        ProcurementDetails.PODet_PK,ProcurementMaster.PONum, ProcurementDetails.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierColor, ProcurementDetails.SupplierSize, UOMMaster.UomCode, ProcurementDetails.POQty, ProcurementDetails.POUnitRate,  CurrencyMaster.CurrencyID, CurrencyMaster.CurrencyCode,
                         ProcurementDetails.CURate, ProcurementDetails.Uom_PK,
                             (SELECT        ISNULL(SUM(ReceiptQty), 0) AS Expr1
                               FROM            MrnDetails
                               WHERE        (PODet_PK = ProcurementDetails.PODet_PK) AND (SkuDet_PK = ProcurementDetails.SkuDet_PK)) AS ReceivedQty,
							   (SELECT        ISNULL(SUM(ExtraQty), 0) AS Expr1
                               FROM            MrnDetails
                               WHERE        (PODet_PK = ProcurementDetails.PODet_PK) AND (SkuDet_PK = ProcurementDetails.SkuDet_PK)) AS ExtraQty,
                             (SELECT        ISNULL(SUM(InvoiceQty), 0) AS Expr1
                               FROM            SupplierInvoiceDetail
                               WHERE        (PODet_PK = ProcurementDetails.PODet_PK)) AS InvQty,ProcurementMaster.POType,(SELECT        MAX(MrnMaster.AddedDate) 
FROM            MrnMaster INNER JOIN
                         MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK
WHERE        (MrnDetails.PODet_PK = ProcurementDetails.PODet_PK)) as LastMRNDATE
FROM            ProcurementDetails INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         CurrencyMaster ON ProcurementMaster.CurrencyID = CurrencyMaster.CurrencyID " + condition + " ) AS tt";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    

                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
                }
            }
            return dt;

        }



        public static DataTable GetSPOInvoicedDetails(ArrayList polist)
        {
            DataTable dt = new DataTable();
            string condition = "where";

            for (int i = 0; i < polist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " StockPODetails.SPO_PK=" + polist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or StockPODetails.SPO_PK=" + polist[i].ToString().Trim();
                }



            }

            if (condition.Trim() != "where")
            {
                String query = @"SELECT        SPODetails_PK, SPONum, ItemDescription, POQty, UomCode, Unitprice, isnull(ReceivedQty,0) AS RecievedQty, (POQty-isnull(ReceivedQty,0)) as BalancetoReceiveQty,isnull(Invoicedqty,0) as InvoicedQty,(isnull(ReceivedQty,0)-isnull(Invoicedqty,0)) as BalanceQty, CurrencyCode
FROM            (SELECT        StockPODetails.SPODetails_PK, StockPODetails.Template_PK, 
                         StockPODetails.Composition + ' ' + StockPODetails.Construct + ' ' + StockPODetails.TemplateColor + ' ' + StockPODetails.TemplateSize + ' ' + StockPODetails.TemplateWidth + ' ' + StockPODetails.TemplateWeight
                          AS ItemDescription, StockPODetails.POQty, UOMMaster.UomCode, StockPODetails.Unitprice,
                             (SELECT        SUM(ReceivedQty +isnull( ExtraQty,0)) 
FROM            StockMRNDetails
WHERE        (SPODetails_PK = StockPODetails.SPODetails_PK)) AS ReceivedQty, 0 AS BalanceQty, CurrencyMaster.CurrencyCode, StockPOMaster.SPONum  ,isnull((				 SELECT SUM(InvoiceQty)
FROM SupplierStockInvoiceDetail
WHERE        (SPODetails_PK = StockPODetails.SPODetails_PK)),0)  as Invoicedqty
FROM            StockPODetails INNER JOIN
                         UOMMaster ON StockPODetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         StockPOMaster ON StockPODetails.SPO_PK = StockPOMaster.SPO_Pk INNER JOIN
                         CurrencyMaster ON StockPOMaster.CurrencyID = CurrencyMaster.CurrencyID " + condition + " ) AS tt";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;


                    dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
                }
            }
            return dt;

        }
    
    
    
    
    }








    public class InvoiceMasterData
    {
        public int LocationPK_pk { get; set; }

         public int Currency_PK { get; set; }
         public int Supplier_PK { get; set; }
         public string SupplierInvoiceNum { get; set; }
         public string Remark { get; set; }
         public DateTime AddedDate { get; set; }

         public DateTime AccountDate { get; set; }
         public DateTime Invoicedate { get; set; }

         public string IsAdvance { get; set; }
        public string AddedBy { get; set; }
        public string Supinvnum { get; set; }
        public List<InvoiceDetData> InvoiceDetDataCollection { get; set; }

        public List<StockInvoiceDetData> StockInvoiceDetDataCollection { get; set; }
        public String InsertPOInvoice(InvoiceMasterData sinvmstrdata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                SupplierInvoiceMaster Spinmstr = new SupplierInvoiceMaster();
                Spinmstr.Location_PK = sinvmstrdata.LocationPK_pk;
                 Spinmstr.Supplier_Pk = sinvmstrdata.Supplier_PK;
                Spinmstr.AddedBy = sinvmstrdata.AddedBy;
                Spinmstr.AddedDate = sinvmstrdata.AddedDate;
                Spinmstr.Currency_PK = sinvmstrdata.Currency_PK;
                Spinmstr.InvoiceDate = sinvmstrdata.Invoicedate;
                Spinmstr.AccountDate = sinvmstrdata.AccountDate;
                Spinmstr.IsAdvance = sinvmstrdata.IsAdvance;
                Spinmstr.Remark = sinvmstrdata.Remark;
                Spinmstr.IsPosted = "N";
                Spinmstr.SupInvnum = sinvmstrdata.Supinvnum;
                Spinmstr.Year = int.Parse(sinvmstrdata.AccountDate.Year.ToString ());
               Donum= Spinmstr.SupplierInvoiceNum = CreateINVnum(int.Parse(sinvmstrdata.AccountDate.Year.ToString()));
                enty.SupplierInvoiceMasters.Add(Spinmstr);


                enty.SaveChanges();

             




                foreach (InvoiceDetData di in sinvmstrdata.InvoiceDetDataCollection)
                {
                    //Add the delivery details
                    SupplierInvoiceDetail shpdert = new SupplierInvoiceDetail();
                    shpdert.SupplierInvoice_PK = Spinmstr.SupplierInvoice_PK;
                    shpdert.PODet_PK = di.Podet_Pk;
                    shpdert.InvoiceQty = di.invoiceQty;
                    shpdert.Unitrate = di.Unitrate;
                    shpdert.InvCurrency = di.InvCurrency;

                    enty.SupplierInvoiceDetails.Add(shpdert);





                }
                enty.SaveChanges();

            }


            return Donum;
        }


        public String InsertSPOInvoice(InvoiceMasterData sinvmstrdata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                SupplierStockInvoiceMaster Spinmstr = new SupplierStockInvoiceMaster();
                Spinmstr.Location_PK = sinvmstrdata.LocationPK_pk;
                Spinmstr.Supplier_Pk = sinvmstrdata.Supplier_PK;
                Spinmstr.AddedBy = sinvmstrdata.AddedBy;
                Spinmstr.AddedDate = sinvmstrdata.AddedDate;
                Spinmstr.Currency_PK = sinvmstrdata.Currency_PK;

                Spinmstr.InvoiceDate = sinvmstrdata.Invoicedate;
                Spinmstr.AccountDate = sinvmstrdata.AccountDate;
                Spinmstr.IsAdvance = sinvmstrdata.IsAdvance;
                Spinmstr.IsPosted = "N";
                Spinmstr.SupInvnum = sinvmstrdata.Supinvnum;
                Spinmstr.Remark = sinvmstrdata.Remark;
                Spinmstr.Year = int.Parse(sinvmstrdata.AccountDate.Year.ToString());

                Donum = Spinmstr.SupplierStockInvoiceNum = CreateSTOCKINVnum(int.Parse(sinvmstrdata.AccountDate.Year.ToString()));
                enty.SupplierStockInvoiceMasters.Add(Spinmstr);


                enty.SaveChanges();






                foreach (StockInvoiceDetData di in sinvmstrdata.StockInvoiceDetDataCollection)
                {
                    //Add the delivery details
                    SupplierStockInvoiceDetail shpdert = new SupplierStockInvoiceDetail();
                    shpdert.SupplierStockInvoice_PK = Spinmstr.SupplierStockInvoice_PK;
                    shpdert.SPODetails_PK = di.SpoPodet_Pk;
                    shpdert.InvoiceQty = di.invoiceQty;
                    shpdert.Unitrate = di.Unitrate;
                    shpdert.InvCurrency = di.InvCurrency;
                    enty.SupplierStockInvoiceDetails.Add(shpdert);





                }
                enty.SaveChanges();

            }


            return Donum;
        }


        /// <summary>
        /// Create PUR num
        /// </summary>
        /// <returns></returns>
        public String CreateINVnum( int year)
        {
            String invnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var count = (from o in enty.SupplierInvoiceMasters

                             where o.Year==year
                             select o).Count();

                var result = year.ToString ().Substring(year.ToString().Length - 2);

                invnum = "PUR" + (int.Parse(count.ToString()) + 50000).ToString().PadLeft(6, '0')+"/"+result.ToString();

               

            }

            return invnum;
        }

        /// <summary>
        /// Create SPUR num
        /// </summary>
        /// <returns></returns>
        public String CreateSTOCKINVnum(int year)
        {
            String invnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var count = (from o in enty.SupplierStockInvoiceMasters

                             where o.Year == year
                             select o).Count();


              
                var result = year.ToString().Substring(year.ToString().Length - 2);
                invnum = "SPUR" + (int.Parse(count.ToString()) + 50000).ToString().PadLeft(6, '0') + "/" + result.ToString();

            }

            return invnum;
        }

    }

    public class InvoiceDetData
    {
        public int invoiedetdata { get; set; }

        public int Podet_Pk { get; set; }
        public Decimal invoiceQty { get; set; }
        public Decimal Unitrate { get; set; }

        public String    InvCurrency { get; set; }


    }




    public class DebitNoteAgainstSales
    {
        public List<DebitNoteAgainstSalesDetails> DebitNoteAgainstSalesDetailsDataCollection { get; set; }
        public int fromLocationPK_pk { get; set; }
        public int toLocationPK_pk { get; set; }

        public int Year { get; set; }
        public String Month { get; set; }
        public String InsertSPOInvoice()
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

               SalesDebitNoteMaster Spinmstr = new SalesDebitNoteMaster();
                Spinmstr.Month = this.Month;
                Spinmstr.Year = this.Year;
                Spinmstr.AddedBy = HttpContext.Current.Session["Username"].ToString ();
                Spinmstr.AddedDate = DateTime.Now;
                Spinmstr.FromLocation_PK = int.Parse(HttpContext.Current.Session["UserLoc_pk"].ToString());

                Spinmstr.ToLocationPK = this.toLocationPK_pk;
                Spinmstr.Remark = "NA";
                Spinmstr.IsCompleted = "N";

               
                ////Spinmstr.IsPosted = "N";
                ////Spinmstr.SupInvnum = sinvmstrdata.Supinvnum;

                ////Spinmstr.Year = int.Parse(sinvmstrdata.AccountDate.Year.ToString());

                //Donum = Spinmstr.SupplierStockInvoiceNum = CreateSTOCKINVnum(int.Parse(sinvmstrdata.AccountDate.Year.ToString()));
                enty.SalesDebitNoteMasters.Add(Spinmstr);
                enty.SaveChanges();
                Spinmstr.SalesDebitNUM = "DBS" + Spinmstr.SalesDebitMasterID.ToString ().PadLeft(6, '0');
              






                foreach (DebitNoteAgainstSalesDetails di in this.DebitNoteAgainstSalesDetailsDataCollection)
                {
                    //Add the delivery details
                    SalesDebitNoteDetail shpdert = new SalesDebitNoteDetail();
                    shpdert.SalesDebitMasterID = Spinmstr.SalesDebitMasterID;
                    shpdert.SDO_PK = di.SDO_PK;
                   
                    enty.SalesDebitNoteDetails.Add(shpdert);





                }
                enty.SaveChanges();

            }


            return Donum;
        }

    }

    public class DebitNoteAgainstSalesDetails
    {
        public int SDO_PK { get; set; }
        public int SalesDebitMasterID { get; set; }
    }

    public class StockInvoiceDetData
    {
        public int invoiedetdata { get; set; }

        public int SpoPodet_Pk { get; set; }
        public Decimal invoiceQty { get; set; }
        public Decimal Unitrate { get; set; }

        public String InvCurrency { get; set; }


    }
     






        



 








}