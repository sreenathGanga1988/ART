using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.DBTransaction.ShippingTransaction
{
    public class DocumentTransaction
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;


        public DataTable GetDOCData(int docpk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        DocDetails.DocDet_Pk, DocMaster.ContainerNum, DocMaster.BOENum, DocMaster.Remark, DocDetails.Donumber, ProcurementMaster.PONum, SkuRawMaterialMaster.RMNum, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + ' ' + ProcurementDetails.SupplierSize + ' ' + ProcurementDetails.SupplierColor + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width
                          AS ItemDescription, UOMMaster.UomCode, DocDetails.Qty, DocDetails.Eta
FROM            DocDetails INNER JOIN
                         DocMaster ON DocDetails.Doc_Pk = DocMaster.Doc_Pk INNER JOIN
                         ProcurementDetails ON DocDetails.PODet_Pk = ProcurementDetails.PODet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON UOMMaster.Uom_PK = SkuRawMaterialMaster.Uom_PK AND SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
WHERE        (DocDetails.Doc_Pk = @Param1)", con);


                cmd.Parameters.AddWithValue("@atcid", docpk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public DataTable GetDOCData(ArrayList shpdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";
            String query = "";
            for (int i = 0; i < shpdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " Where  DocMaster.Doc_Pk= " + shpdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or DocMaster.Doc_Pk =" + shpdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                query = @"SELECT        DocMaster.Doc_Pk, DocMaster.DocNum, DocMaster.ContainerNum AS Supplierinvoice, DocMaster.BOENum, DocMaster.Remark, DocMaster.InhouseDate, DocMaster.ETADate, 
                         SupplierMaster.SupplierName
FROM            DocMaster INNER JOIN
                         SupplierMaster ON DocMaster.Supplier_PK = SupplierMaster.Supplier_PK" + condition + "";
               
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            
            return QueryFunctions.ReturnQueryResultDatatable(cmd);

        }


        public DataTable GetDOData(ArrayList shpdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";
            String query = "";
            for (int i = 0; i < shpdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " Where  DeliveryOrderMaster.DO_PK = " + shpdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or DeliveryOrderMaster.DO_PK  =" + shpdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                query = @"SELECT        DeliveryOrderMaster.DO_PK, DeliveryOrderMaster.DONum, DeliveryOrderMaster.DeliveryDate, AtcMaster.AtcNum, DeliveryOrderMaster.ExportContainer, DeliveryOrderMaster.ContainerNumber, 
                         LocationMaster.LocationName AS [From], LocationMaster_1.LocationName AS [TO Location], DeliveryOrderMaster.AddedBy, DeliveryOrderMaster.AddedDate, DeliveryOrderMaster.DoType
FROM            DeliveryOrderMaster INNER JOIN
                         AtcMaster ON DeliveryOrderMaster.AtcID = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON DeliveryOrderMaster.FromLocation_PK = LocationMaster.Location_PK INNER JOIN
                         LocationMaster AS LocationMaster_1 ON DeliveryOrderMaster.ToLocation_PK = LocationMaster_1.Location_PK" + condition + "";

            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;

            return QueryFunctions.ReturnQueryResultDatatable(cmd);

        }


        public DataTable GetSDOCData(ArrayList shpdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";
            String query = "";
            for (int i = 0; i < shpdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " Where  SDocMaster.SDoc_Pk= " + shpdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or SDocMaster.SDoc_Pk =" + shpdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                query = @"SELECT SDocMaster.SDoc_Pk, SDocMaster.SDocNum, SDocMaster.ContainerNum, SDocMaster.BOENum, SDocMaster.Remark, SDocMaster.InhouseDate, SDocMaster.ETADate, SupplierMaster.SupplierName FROM SDocMaster INNER JOIN SupplierMaster ON SDocMaster.Supplier_PK = SupplierMaster.Supplier_PK" + condition + "";

            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;

            return QueryFunctions.ReturnQueryResultDatatable(cmd);

        }


        public DataTable GetSDOData(ArrayList shpdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "";
            String query = "";
            for (int i = 0; i < shpdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " Where  InventorySalesMaster.SalesDO_PK = " + shpdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or InventorySalesMaster.SalesDO_PK  =" + shpdetlist[i].ToString().Trim();
                }



            }

            if (condition != "where")
            {
                query = @"SELECT InventorySalesMaster.SalesDO_PK, InventorySalesMaster.SalesDONum, InventorySalesMaster.SalesDate, LocationMaster.LocationName, InventorySalesMaster.SalesDODate, InventorySalesMaster.ContainerNumber, InventorySalesMaster.BoeNum FROM InventorySalesMaster INNER JOIN LocationMaster ON InventorySalesMaster.ToLocation_PK = LocationMaster.Location_PK" + condition + "";

            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;

            return QueryFunctions.ReturnQueryResultDatatable(cmd);

        }
    }


    public  class lctransaction
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;


        public DataTable GetLCData(int docpk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        LCDetails.LCDet_PK, LCMaster.LCNum, LCDetails.POType, LCDetails.ATCnum, LCDetails.PONUM, LCDetails.LCValue, LCDetails.InvoiceNUM, SupplierMaster.SupplierName, BankMaster.BankName, 
                         LCMaster.LC_PK
FROM            LCDetails INNER JOIN
                         LCMaster ON LCDetails.LC_PK = LCMaster.LC_PK INNER JOIN
                         BankMaster ON LCMaster.Bank_PK = BankMaster.Bank_PK INNER JOIN
                         SupplierMaster ON LCMaster.Supplier_pk = SupplierMaster.Supplier_PK
WHERE        (LCMaster.LC_PK = @Param1)", con);


                cmd.Parameters.AddWithValue("@Param1", docpk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }
    }

}