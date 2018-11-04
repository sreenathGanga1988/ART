using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.DBTransaction
{
    public class ProcurementTransaction
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        /// <summary>
        /// get base Rate for currency against doloor
        /// </summary>
        /// <param name="currencyid"></param>
        /// <returns></returns>
        public float Getconversionfact(int currencyid)
        {
            float conv = 1;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                enty.Configuration.AutoDetectChangesEnabled = false;
                var CONVFACT = enty.POCurrExRates.Where(u => u.CurrencyID == currencyid).Select(u => u.Convrate).FirstOrDefault();
                if (CONVFACT != null)
                {
                    conv = float.Parse(CONVFACT.ToString());
                }
                else
                {
                    conv = 0;
                }
            }
            return conv;
        }


        /// <summary>
        /// Get all altuom for base Uom
        /// </summary>
        /// <param name="baseuomcode"></param>
        /// <returns></returns>
        public DataTable GetAltUOM(String baseuomcode)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        UOMMaster_1.Uom_PK, UOMMaster_1.UomCode
FROM            AltUOMMaster INNER JOIN
                         UOMMaster AS UOMMaster_1 ON AltUOMMaster.AltUom_PK = UOMMaster_1.Uom_PK INNER JOIN
                         UOMMaster ON AltUOMMaster.Uom_PK = UOMMaster.Uom_PK
WHERE        (UOMMaster.UomCode = @baseuomcode)", con);
                cmd.Parameters.AddWithValue("@baseuomcode", @baseuomcode);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        /// <summary>
        /// Get the conversion factor and Operator for altuom conversion
        /// </summary>
        /// <param name="baseuom_pk"></param>
        /// <param name="altuom_pk"></param>
        /// <returns></returns>

        public DataTable getAltuomdata(int baseuom_pk, int altuom_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        Conv_fact, Operator
FROM            AltUOMMaster
WHERE        (Uom_PK = @baseuom) AND (AltUom_PK = @altuom)", con);
                cmd.Parameters.AddWithValue("@baseuom", baseuom_pk);
                cmd.Parameters.AddWithValue("@altuom", altuom_pk);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }




        /// <summary>
        /// Get All the non deleted PO of a SKUPK
        /// </summary>
        /// <param name="sku_pk"></param>
        /// <returns></returns>
        public DataTable GetPOofSkuid(int sku_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT ProcurementMaster.PONum AS PO
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         ProcurementDetails ON SkuRawmaterialDetail.SkuDet_PK = ProcurementDetails.SkuDet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk
WHERE        (ProcurementMaster.IsDeleted <> N'D') AND (SkuRawMaterialMaster.Sku_Pk = @Param1)", con);
                cmd.Parameters.AddWithValue("@Param1", sku_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        /// <summary>
        /// Get All the Non deleted PO of a SKUDET_PK
        /// </summary>
        /// <param name="skudet_pk"></param>
        /// <returns></returns>
        public DataTable GetPOofSkuDetPK(int skudet_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT ProcurementMaster.PONum AS PO
FROM            SkuRawmaterialDetail INNER JOIN
                         ProcurementDetails ON SkuRawmaterialDetail.SkuDet_PK = ProcurementDetails.SkuDet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk
WHERE        (ProcurementMaster.IsDeleted <> N'D') AND (SkuRawmaterialDetail.SkuDet_PK = @Param2)", con);
                cmd.Parameters.AddWithValue("@Param2", skudet_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

/// <summary>
/// Get All the details of a PO ny ponum
/// </summary>
/// <param name="POnum"></param>
/// <returns></returns>
        public DataTable GetPOData(String POnum)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        ProcurementMaster.PONum, ProcurementDetails.POQty, AtcMaster.AtcNum, CurrencyMaster.CurrencyCode, DeliveryMethodMaster.DeliveryMethod, LocationMaster.LocationName, 
                         LocationMaster.LocationAddress, SupplierMaster.SupplierName, SupplierMaster.SupplierAddress, SupplierMaster.Telephone, PaymentTermMaster.PaymentCodeDescription, SkuRawMaterialMaster.RMNum, 
                         ProcurementDetails.POUnitRate, UOMMaster.UomCode, SkuRawMaterialMaster.Construction, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Width, SkuRawMaterialMaster.Weight, 
                         ProcurementMaster.AddedDate, CAST(ProcurementDetails.POQty * ProcurementDetails.POUnitRate AS decimal(18, 2)) AS POvalue, CountryMaster.Description, AtcMaster.MerchandiserName, 
                         DeliveryTermMaster.DeliveryTerm, ProcurementMaster.PO_Pk, (CASE ProcurementDetails.SupplierColor WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE ProcurementDetails.SupplierColor END) AS SupplierColor, 
                         (CASE ProcurementDetails.SupplierSize WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE ProcurementDetails.SupplierSize END) AS SupplierSize, 
                         (CASE SkuRawmaterialDetail.ItemSize WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE SkuRawmaterialDetail.ItemSize END) AS ItemSize, 
                         (CASE SkuRawmaterialDetail.ItemColor WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE SkuRawmaterialDetail.ItemColor END) AS ItemColor, ProcurementMaster.DeliveryDate, 
                         Template_Master.Description AS ItemName, ProcurementMaster.POType, ProcurementMaster.Remark, ProcurementMaster.IsApproved, ProcurementMaster.IsDeleted, ProcurementMaster.DeletedBy, 
                         ProcurementMaster.ApprovedBy, ProcurementMaster.DeletedDate, ProcurementMaster.Approveddate
FROM            ProcurementMaster INNER JOIN
                         ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         CurrencyMaster ON ProcurementMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                         DeliveryMethodMaster ON ProcurementMaster.DeliveryMethod_Pk = DeliveryMethodMaster.Deliverymethod_Pk INNER JOIN
                         LocationMaster ON ProcurementMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         PaymentTermMaster ON ProcurementMaster.PaymentTermID = PaymentTermMaster.PaymentTermID INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk AND ProcurementMaster.AtcId = SkuRawMaterialMaster.Atc_id INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         DeliveryTermMaster ON ProcurementMaster.DeliveryTerms_Pk = DeliveryTermMaster.DeliveryTerms_Pk INNER JOIN
                         CountryMaster ON SupplierMaster.CountryID = CountryMaster.CountryID INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (ProcurementMaster.PONum = @param1)", con);
                cmd.Parameters.AddWithValue("@Param1", POnum);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        public DataTable GetPOData(int  po_PK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {

                con.Open();




                SqlCommand cmd = new SqlCommand(@"GetAPO_SP", con);
                cmd.Parameters.AddWithValue("@PO_Pk", po_PK);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public DataTable GetMCRData(int @Mcr_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {

                con.Open();




                SqlCommand cmd = new SqlCommand(@"GetMCR_SP", con);
                cmd.Parameters.AddWithValue("@Mcr_pk", @Mcr_pk);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        /// <summary>
        /// Get the items withion a PO
        /// </summary>
        /// <param name="PO_PK"></param>
        /// <returns></returns>

        public DataTable GetPODetailsData(int  PO_PK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        ProcurementDetails.PODet_PK, SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, (Template_Master.Description +' ' +SkuRawMaterialMaster.Construction +' ' + 
                         SkuRawMaterialMaster.Composition +' ' + SkuRawMaterialMaster.Width +' ' + SkuRawMaterialMaster.Weight) as Description, 
                         (CASE ProcurementDetails.SupplierColor WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE ProcurementDetails.SupplierColor END) AS SupplierColor, 
                         (CASE ProcurementDetails.SupplierSize WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE ProcurementDetails.SupplierSize END) AS SupplierSize, 
                         (CASE SkuRawmaterialDetail.ItemSize WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE SkuRawmaterialDetail.ItemSize END) AS ItemSize, 
                         (CASE SkuRawmaterialDetail.ItemColor WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE SkuRawmaterialDetail.ItemColor END) AS ItemColor, ProcurementDetails.POQty, UOMMaster.UomCode, 
                         ProcurementDetails.POUnitRate, CAST(ProcurementDetails.POQty * ProcurementDetails.POUnitRate AS decimal(18, 2)) AS POvalue, ProcurementMaster.PO_Pk,ProcurementDetails.CURate
FROM            ProcurementMaster INNER JOIN
                         ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (ProcurementMaster.PO_Pk = @Param1)", con);
                cmd.Parameters.AddWithValue("@Param1", PO_PK);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }












        # region Approval


        public void ApprovePO(int costingPK)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var datatoapprove = from pomstr in enty.ProcurementMasters
                                    where pomstr.PO_Pk == costingPK
                                    select pomstr;

                foreach (var element123 in datatoapprove)
                {
                    element123.IsApproved = "Y";
                    element123.ApprovedBy = HttpContext.Current.Session["Username"].ToString();
                    element123.Approveddate = DateTime.Now;
                }

                enty.SaveChanges();
            }


        }






        public void DeletePO(int costingPK)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var datatoapprove = from pomstr in enty.ProcurementMasters
                                    where pomstr.PO_Pk == costingPK
                                    select pomstr;

                foreach (var element123 in datatoapprove)
                {
                    element123.IsDeleted = "Y";
                    element123.DeletedBy = HttpContext.Current.Session["Username"].ToString();
                    element123.DeletedDate = DateTime.Now;
                }

                enty.SaveChanges();
            }


        }


        public void ForwardApprovePO(int PO_PK)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                POApproval apprl = new POApproval();
                apprl.PO_PK = PO_PK;
                apprl.ForwardedBy = HttpContext.Current.Session["Username"].ToString();
                apprl.IApproved = "N";
                enty.POApprovals.Add(apprl);

                enty.SaveChanges();
            }


        }












        # endregion


        # region RO

        /// <summary>
        /// Get the list of Available Item Against the Template from 
        /// Same Warehouse
        /// </summary>
        /// <param name="atcid"></param>
        /// <param name="temp_pk"></param>
        /// <returns></returns>

        public DataTable GetDetailforRO(int atcid, int temp_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        InventoryMaster.InventoryItem_PK,InventoryMaster.SkuDet_Pk, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.OnhandQty, ProcurementDetails_1.POUnitRate, 
                         SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.Template_pk, LocationMaster.LocationPrefix,ProcurementDetails.CURate
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         ProcurementDetails AS ProcurementDetails_1 ON InventoryMaster.PoDet_PK = ProcurementDetails_1.PODet_PK INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK
WHERE        (SkuRawMaterialMaster.Atc_id = @atcid) AND (SkuRawMaterialMaster.Template_pk = @temp_pk)
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode", con);
                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@temp_pk", temp_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }







        /// <summary>
        /// Get detail of an RO
        /// </summary>
        /// <param name="ro_pk"></param>
        /// <returns></returns>
        public DataTable GetRoDetails(int ro_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




//                SqlCommand cmd = new SqlCommand(@"SELECT        RequestOrderDetails.RODet_Pk, SkuRawMaterialMaster.RMNum, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Construction, SkuRawmaterialDetail.SupplierColor, 
//                         SkuRawmaterialDetail.SupplierSize, RequestOrderDetails.Qty, RequestOrderDetails.CUnitPrice, RequestOrderDetails.FromSkuDet_PK, RequestOrderDetails.ToSkuDet_PK, 
//                         RequestOrderDetails.InventoryItem_PK
//FROM            SkuRawMaterialMaster INNER JOIN
//                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
//                         RequestOrderDetails ON SkuRawmaterialDetail.SkuDet_PK = RequestOrderDetails.ToSkuDet_PK
//WHERE        (RequestOrderDetails.RO_Pk = @ropk)", con);





                SqlCommand cmd = new SqlCommand(@"SELECT        RequestOrderDetails.RODet_Pk, SkuRawMaterialMaster.RMNum, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Construction, SkuRawmaterialDetail.SupplierColor, 
                         SkuRawmaterialDetail.SupplierSize, RequestOrderDetails.Qty, RequestOrderDetails.CUnitPrice, RequestOrderDetails.FromSkuDet_PK, RequestOrderDetails.ToSkuDet_PK, RequestOrderDetails.InventoryItem_PK,
                          InventoryMaster.OnhandQty, InventoryMaster.Refnum
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         RequestOrderDetails ON SkuRawmaterialDetail.SkuDet_PK = RequestOrderDetails.ToSkuDet_PK INNER JOIN
                         InventoryMaster ON RequestOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK
WHERE        (RequestOrderDetails.RO_Pk = @ropk)", con);


                cmd.Parameters.AddWithValue("@ropk", ro_pk);


                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        /// <summary>
        /// Get Details of a stock RO
        /// </summary>
        /// <param name="ro_pk"></param>
        /// <returns></returns>

        public DataTable GetStockRoDetails(int ro_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        SkuRawMaterialMaster.RMNum, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Construction, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.SupplierSize,
                         RequestOrderStockDetails.RODet_Pk, RequestOrderStockDetails.Qty , RequestOrderStockDetails.CUnitPrice , RequestOrderStockDetails.SInventoryItem_PK,
                       (LocationMaster.LocationName + '  GStock ') as fromLocation, RequestOrderStockDetails.ToSkuDet_PK
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         RequestOrderStockDetails ON SkuRawmaterialDetail.SkuDet_PK = RequestOrderStockDetails.ToSkuDet_PK INNER JOIN
                         StockInventoryMaster ON RequestOrderStockDetails.SInventoryItem_PK = StockInventoryMaster.SInventoryItem_PK INNER JOIN
                         LocationMaster ON StockInventoryMaster.Location_Pk = LocationMaster.Location_PK
WHERE        (RequestOrderStockDetails.SRO_Pk = @ropk)", con);
                cmd.Parameters.AddWithValue("@ropk", ro_pk);


                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }





        public DataTable GetROData(int ro_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        RequestOrderMaster.RONum, RequestOrderMaster.CreatedDate, RequestOrderMaster.AddedBy, AtcMaster.AtcNum AS FRMATC, AtcMaster_1.AtcNum AS TOATC, Template_Master.Description AS TOTEMP, 
                         Template_Master_1.Description AS FRMTEMP, RequestOrderDetails.Qty, SkuRawMaterialMaster.Composition AS FRMCOMP, SkuRawMaterialMaster.Construction AS FRMCONS, 
                         SkuRawMaterialMaster.Weight AS FRMWEIG, SkuRawMaterialMaster.Width AS FROMWID, SkuRawmaterialDetail.ItemColor AS FRMITEMCOLOR, SkuRawmaterialDetail.SupplierColor AS FRMSUPPCOLOR, 
                         SkuRawmaterialDetail.ItemSize AS FRMITEMSIZE, SkuRawmaterialDetail.SupplierSize AS FRMSUPPSIZE, RequestOrderDetails.CUnitPrice AS RATE, UOMMaster.UomName AS UOM, 
                         SkuRawMaterialMaster_1.Composition, SkuRawMaterialMaster_1.Construction, SkuRawMaterialMaster_1.Weight, SkuRawMaterialMaster_1.Width, SkuRawmaterialDetail_1.ItemColor, 
                         SkuRawmaterialDetail_1.SupplierColor, SkuRawmaterialDetail_1.ItemSize, SkuRawmaterialDetail_1.SupplierSize, LocationMaster.LocationName, LocationMaster.LocationAddress, 
                         RequestOrderMaster.RO_Pk
FROM            SkuRawmaterialDetail INNER JOIN
                         RequestOrderMaster INNER JOIN
                         RequestOrderDetails ON RequestOrderMaster.RO_Pk = RequestOrderDetails.RO_Pk ON SkuRawmaterialDetail.SkuDet_PK = RequestOrderDetails.FromSkuDet_PK INNER JOIN
                         SkuRawmaterialDetail AS SkuRawmaterialDetail_1 ON RequestOrderDetails.ToSkuDet_PK = SkuRawmaterialDetail_1.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         SkuRawMaterialMaster AS SkuRawMaterialMaster_1 ON SkuRawmaterialDetail_1.Sku_PK = SkuRawMaterialMaster_1.Sku_Pk INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         AtcMaster AS AtcMaster_1 ON SkuRawMaterialMaster_1.Atc_id = AtcMaster_1.AtcId INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         Template_Master AS Template_Master_1 ON SkuRawMaterialMaster_1.Template_pk = Template_Master_1.Template_PK INNER JOIN
                         InventoryMaster ON RequestOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         UOMMaster ON InventoryMaster.Uom_Pk = UOMMaster.Uom_PK
WHERE        (RequestOrderMaster.RO_Pk = @ro_pk)", con);
                cmd.Parameters.AddWithValue("@ro_pk", ro_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }








        public DataTable GetStockROData(int sro_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"  SELECT RequestOrderStockMaster.RONum, Template_Master.Description AS TEmplate, UOMMaster.UomName, ISNULL(StockInventoryMaster.Composition, ' ') AS Composition, ISNULL(StockInventoryMaster.Construct,
                  ' ') AS Construct, ISNULL(StockInventoryMaster.TemplateColor, ' ') AS Color, ISNULL(StockInventoryMaster.TemplateSize, ' ') AS Size, ISNULL(StockInventoryMaster.TemplateWidth, ' ') AS width,
                  ISNULL(StockInventoryMaster.TemplateWeight, ' ') AS Tweight, ISNULL(RequestOrderStockDetails.Qty, '0') AS QTY, RequestOrderStockMaster.CreatedDate, RequestOrderStockMaster.AddedBy, 
                         RequestOrderStockMaster.SRO_Pk, AtcMaster.AtcNum, ISNULL(Template_Master_1.Description, N' ') AS ToTEmp, ISNULL(SkuRawMaterialMaster.Composition, N' ') AS ToComp,
                         ISNULL(SkuRawMaterialMaster.Construction, N' ') AS ToCons, ISNULL(SkuRawMaterialMaster.Weight, N' ') AS ToWEIGHT, ISNULL(SkuRawMaterialMaster.Width, N' ') AS TOWIDTH,
                         UOMMaster_1.UomName AS TOUOM, ISNULL(SkuRawmaterialDetail.ItemColor, N' ') AS toITCOL, ISNULL(SkuRawmaterialDetail.SupplierColor, N' ') AS tosupcol, LocationMaster.LocationName
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master INNER JOIN
                         RequestOrderStockDetails INNER JOIN
                         RequestOrderStockMaster ON RequestOrderStockDetails.SRO_Pk = RequestOrderStockMaster.SRO_Pk INNER JOIN
                         AtcMaster ON RequestOrderStockMaster.AtcID = AtcMaster.AtcId INNER JOIN
                         StockInventoryMaster ON RequestOrderStockDetails.SInventoryItem_PK = StockInventoryMaster.SInventoryItem_PK ON Template_Master.Template_PK = StockInventoryMaster.Template_PK INNER JOIN
                         UOMMaster ON StockInventoryMaster.Uom_PK = UOMMaster.Uom_PK ON SkuRawmaterialDetail.SkuDet_PK = RequestOrderStockDetails.ToSkuDet_PK INNER JOIN
                         Template_Master AS Template_Master_1 ON SkuRawMaterialMaster.Template_pk = Template_Master_1.Template_PK INNER JOIN
                         UOMMaster AS UOMMaster_1 ON SkuRawMaterialMaster.Uom_PK = UOMMaster_1.Uom_PK INNER JOIN
                         LocationMaster ON StockInventoryMaster.Location_Pk = LocationMaster.Location_PK
WHERE        (RequestOrderStockMaster.SRO_Pk = @sro_pk)", con);
                cmd.Parameters.AddWithValue("@sro_pk", sro_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

      











        #endregion



        #region ServicePO

        # region ServicePOApproval

        public DataTable GetServicePoForApproval()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        ServicePOMaster.ServicePO_PK, ServicePOMaster.ServicePOnumber, ServicePOMaster.DebitFrom, ServicePOMaster.DebitName, ServiceTypeMaster.ServiceType, ServicePOMaster.Amount, 
                         CurrencyMaster.CurrencyCode, ServicePOMaster.Description, ServicePOMaster.AddedBy, ServicePOMaster.AddedDate
FROM            ServicePOMaster INNER JOIN
                         ServiceTypeMaster ON ServicePOMaster.ServiceType_Pk = ServiceTypeMaster.ServiceType_Pk INNER JOIN
                         CurrencyMaster ON ServicePOMaster.CurrencyID = CurrencyMaster.CurrencyID
WHERE        (ServicePOMaster.IsApproved = 'N') AND (ServicePOMaster.IsDeleted ='N' )", con);




                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public void ApproveServicePO(int servicepo_pk)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var datatoapprove = from pomstr in enty.ServicePOMasters
                                    where pomstr.ServicePO_PK == servicepo_pk
                                    select pomstr;

                foreach (var element123 in datatoapprove)
                {
                    element123.IsApproved = "Y";
                    element123.ApprovedBy = HttpContext.Current.Session["Username"].ToString();
                    element123.ApprovedDate = DateTime.Now;
                }

                enty.SaveChanges();
            }


        }

        public void DeleteServicePO(int servicepo_pk)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {



                var datatoapprove = from pomstr in enty.ServicePOMasters
                                    where pomstr.ServicePO_PK == servicepo_pk
                                    select pomstr;

                foreach (var element123 in datatoapprove)
                {
                    element123.IsDeleted = "Y";
                    element123.DeletedBy = HttpContext.Current.Session["Username"].ToString();
                    element123.DeletedDate = DateTime.Now;
                }

                enty.SaveChanges();
            }


        }



        public DataTable GetServicepo(int PO_PK)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @" SELECT        ServicePOMaster.ServicePO_PK, ServicePOMaster.ServicePOnumber, ServicePOMaster.DebitFrom, ServicePOMaster.DebitName, ServicePOMaster.Amount, ServicePOMaster.Description, 
                         ServicePOMaster.AddedBy, ServicePOMaster.AddedDate, ServicePOMaster.ApprovedBy, ServicePOMaster.ApprovedDate, ServiceTypeMaster.ServiceType, CurrencyMaster.CurrencyCode, 
                         ServicePOMaster.IsApproved, ServicePOMaster.Remark
FROM            ServicePOMaster INNER JOIN
                         CurrencyMaster ON ServicePOMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                         ServiceTypeMaster ON ServicePOMaster.ServiceType_Pk = ServiceTypeMaster.ServiceType_Pk
WHERE        (ServicePOMaster.ServicePO_PK = @Param1)";



                cmd.Parameters.AddWithValue("@Param1", PO_PK);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }





        #endregion

        #endregion





        #region Stock PO

        public DataTable GetsPOData(int SPOPK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"GetSPO_SP", con);




                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SPO_Pk", SPOPK);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }
        public DataTable GetSpoDetails(int spo_pk,int adn_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        SPODetails_PK, Template_PK, Composition, Construct, TemplateColor, TemplateSize, TemplateWidth, TemplateWeight, POQty, UomCode, Unitprice, ReceivedQty, (ADNQTY-ReceivedQty) as BalanceQty
FROM            (SELECT        StockPODetails.SPODetails_PK, StockPODetails.Template_PK, StockPODetails.Composition, StockPODetails.Construct, StockPODetails.TemplateColor, StockPODetails.TemplateSize, StockPODetails.TemplateWidth, 
                         StockPODetails.TemplateWeight, StockPODetails.POQty, UOMMaster.UomCode, StockPODetails.Unitprice, ISNULL
                             ((SELECT        SUM(ReceivedQty) AS Expr1
                                 FROM            StockMRNDetails
                                 GROUP BY SPODetails_PK
                                 HAVING        (SPODetails_PK = StockPODetails.SPODetails_PK)), 0) AS ReceivedQty, 0 AS BalanceQty, SDocDetails.SDoc_Pk, SDocDetails.Qty AS ADNQTY
FROM            StockPODetails INNER JOIN
                         UOMMaster ON StockPODetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         SDocDetails ON StockPODetails.SPODetails_PK = SDocDetails.SPODet_Pk
                          WHERE      SDocDetails.SDoc_Pk =@adn_pk and StockPODetails.SPO_PK=@spo_pk ) AS tt", con);


                cmd.Parameters.AddWithValue("@spo_pk", spo_pk);
                cmd.Parameters.AddWithValue("@adn_pk", adn_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public DataTable GetSpoItemList(int spo_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        Template_Master.Description + ISNULL(StockPODetails.Composition, '') + ISNULL(StockPODetails.Construct, '') + ISNULL(StockPODetails.TemplateColor, '') AS Item, StockPODetails.SPODetails_PK
FROM            StockPODetails INNER JOIN
                         Template_Master ON StockPODetails.Template_PK = Template_Master.Template_PK
WHERE        (StockPODetails.SPO_PK = @spo_pk)", con);


                cmd.Parameters.AddWithValue("@spo_pk", spo_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public DataTable GetSpoiNVOICEDetails(int spo_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"	 SELECT        SupplierStockInvoiceMaster.SupplierStockInvoice_PK, SupplierStockInvoiceMaster.SupplierStockInvoiceNum, SupplierStockInvoiceMaster.AddedBy, SupplierStockInvoiceMaster.AddedDate, 
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
						 WHERE        (StockPOMaster.SPO_Pk = @spo_pk)", con);


                cmd.Parameters.AddWithValue("@spo_pk", spo_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }
        public DataTable Getspomrn(int spo_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        LocationMaster.LocationName, LocationMaster.LocationAddress, StockRecieptMaster.StockRecieptNum, StockRecieptMaster.ContainerNum, 
                         StockRecieptMaster.BOENum, StockRecieptMaster.Remark, StockRecieptMaster.InhouseDate, StockRecieptMaster.Deliverydate, StockRecieptMaster.AddedBy, 
                         LocationMaster.Telephone, SupplierMaster.SupplierName, SupplierMaster.SupplierAddress, SupplierMaster.Telephone AS TEL, 
                         CountryMaster.Description AS COUNTRY, StockMrnMaster.SMrnNum, StockMrnMaster.AddedBY AS Addby, StockMrnMaster.AddedDate, 
                         StockPOMaster.SPONum AS PO, StockMRNDetails.ReceivedQty, StockMRNDetails.ExtraQty, UOMMaster.UomName, Template_Master.Description AS TEMPLATE, 
                         StockPODetails.Composition, StockPODetails.Construct, StockPODetails.TemplateColor, StockPODetails.TemplateSize, StockPODetails.TemplateWidth, 
                         StockPODetails.TemplateWeight
FROM            StockRecieptMaster INNER JOIN
                         StockMrnMaster ON StockRecieptMaster.SReciept_Pk = StockMrnMaster.SReciept_Pk INNER JOIN
                         StockMRNDetails ON StockMrnMaster.SMrn_PK = StockMRNDetails.SMRN_Pk INNER JOIN
                         StockPODetails ON StockMRNDetails.SPODetails_PK = StockPODetails.SPODetails_PK INNER JOIN
                         StockPOMaster ON StockPODetails.SPO_PK = StockPOMaster.SPO_Pk INNER JOIN
                         CountryMaster INNER JOIN
                         LocationMaster ON CountryMaster.CountryID = LocationMaster.CountryID ON StockPOMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         UOMMaster ON StockPODetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         Template_Master ON StockPODetails.Template_PK = Template_Master.Template_PK
                          WHERE        (StockPODetails.SPO_PK = @spo_pk)", con);


                cmd.Parameters.AddWithValue("@spo_pk", spo_pk);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        #  endregion
    }


    public class ProcurementReports
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        public DataTable GetReciptDetailsByPO(int PO_PK)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        RecieptMaster.RecieptNum, RecieptMaster.ContainerNum, RecieptMaster.BOENum, RecieptMaster.Remark, RecieptMaster.InhouseDate, RecieptMaster.Deliverydate, MrnDetails.ExtraQty, 
                         MrnDetails.Remark AS Expr1, MrnDetails.ReceiptQty, MrnMaster.MrnNum, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName, SkuRawmaterialDetail.RqdQty, 
                         SkuRawmaterialDetail.SupplierSize, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemColor, SkuRawMaterialMaster.Composition, 
                         SkuRawMaterialMaster.Construction, SkuRawMaterialMaster.Weight, SkuRawMaterialMaster.Width, UOMMaster.UomName, MrnMaster.AddedDate, LocationMaster.LocationName, 
                         LocationMaster.LocationAddress, LocationMaster.Telephone, LocationMaster.Email, LocationMaster.Fax, MrnMaster.AddedBY
FROM            RecieptMaster INNER JOIN
                         MrnMaster ON RecieptMaster.Reciept_Pk = MrnMaster.Reciept_Pk INNER JOIN
                         MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                         ProcurementMaster ON MrnMaster.Po_PK = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         SupplierMaster ON RecieptMaster.Supplier_PK = SupplierMaster.Supplier_PK INNER JOIN
                         SkuRawMaterialMaster ON AtcMaster.AtcId = SkuRawMaterialMaster.Atc_id INNER JOIN
                         SkuRawmaterialDetail ON MrnDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK AND SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         UOMMaster ON SkuRawMaterialMaster.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         LocationMaster ON MrnMaster.Location_Pk = LocationMaster.Location_PK
WHERE        (ProcurementMaster.PO_Pk = @Param1)";


                
                cmd.Parameters.AddWithValue("@Param1", PO_PK);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }


         public DataTable GetPOStatusByPO(String POnum)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        ProcurementMaster.PONum, ProcurementDetails.POQty, AtcMaster.AtcNum, CurrencyMaster.CurrencyCode, DeliveryMethodMaster.DeliveryMethod, LocationMaster.LocationName, 
                         LocationMaster.LocationAddress, SupplierMaster.SupplierName, SupplierMaster.SupplierAddress, SupplierMaster.Telephone, PaymentTermMaster.PaymentCodeDescription, SkuRawMaterialMaster.RMNum, 
                         ProcurementDetails.POUnitRate, UOMMaster.UomCode, SkuRawMaterialMaster.Construction, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Width, SkuRawMaterialMaster.Weight, 
                         ProcurementMaster.AddedDate, CAST(ProcurementDetails.POQty * ProcurementDetails.POUnitRate AS decimal(18, 2)) AS POvalue, CountryMaster.Description, AtcMaster.MerchandiserName, 
                         DeliveryTermMaster.DeliveryTerm, ProcurementMaster.PO_Pk, (CASE ProcurementDetails.SupplierColor WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE ProcurementDetails.SupplierColor END) AS SupplierColor, 
                         (CASE ProcurementDetails.SupplierSize WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE ProcurementDetails.SupplierSize END) AS SupplierSize, 
                         (CASE SkuRawmaterialDetail.ItemSize WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE SkuRawmaterialDetail.ItemSize END) AS ItemSize, 
                         (CASE SkuRawmaterialDetail.ItemColor WHEN 'NA' THEN '' WHEN 'N/A' THEN '' ELSE SkuRawmaterialDetail.ItemColor END) AS ItemColor, ProcurementMaster.DeliveryDate, 
                         Template_Master.Description AS ItemName, ProcurementMaster.POType, ProcurementMaster.Remark, ProcurementMaster.IsApproved, ProcurementMaster.IsDeleted, ProcurementMaster.DeletedBy, 
                         ProcurementMaster.ApprovedBy, ProcurementMaster.DeletedDate, ProcurementMaster.Approveddate
FROM            ProcurementMaster INNER JOIN
                         ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         CurrencyMaster ON ProcurementMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                         DeliveryMethodMaster ON ProcurementMaster.DeliveryMethod_Pk = DeliveryMethodMaster.Deliverymethod_Pk INNER JOIN
                         LocationMaster ON ProcurementMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         PaymentTermMaster ON ProcurementMaster.PaymentTermID = PaymentTermMaster.PaymentTermID INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk AND ProcurementMaster.AtcId = SkuRawMaterialMaster.Atc_id INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         DeliveryTermMaster ON ProcurementMaster.DeliveryTerms_Pk = DeliveryTermMaster.DeliveryTerms_Pk INNER JOIN
                         CountryMaster ON SupplierMaster.CountryID = CountryMaster.CountryID INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (ProcurementMaster.PONum = @param1)";



                cmd.Parameters.AddWithValue("@Param1", POnum);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }


        public DataTable GetInvoiceDetailsByPO(int PO_PK)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"SELECT        SupplierInvoiceMaster.SupplierInvoiceNum, SupplierInvoiceMaster.AddedBy, SupplierInvoiceMaster.AddedDate, ISNULL(SupplierInvoiceMaster.Remark, '') AS Remark, 
                         ISNULL(SupplierInvoiceMaster.InvoiceDate, '') AS InvoiceDate, ISNULL(SupplierInvoiceMaster.AccountDate, '') AS AccountDate, ISNULL(SupplierInvoiceMaster.IsAdvance, '') AS IsAdvance, 
                         ISNULL(SupplierInvoiceMaster.SupInvnum, '') AS SupInvnum, AtcMaster.AtcNum, ProcurementMaster.PONum, 
                       (ISNULL( SkuRawMaterialMaster.Composition,'') + ' ' +ISNULL( SkuRawMaterialMaster.Construction,'') + ' ' +ISNULL( SkuRawMaterialMaster.Weight,'') + ' ' + ISNULL(SkuRawMaterialMaster.Width,'') + ' ' +ISNULL( SkuRawmaterialDetail.ItemColor,'') + ' ' +ISNULL( SkuRawmaterialDetail.ItemSize,'')
                          + ' ' + ISNULL(ProcurementDetails.SupplierSize,'') + ' ' + ISNULL( ProcurementDetails.SupplierColor,'')) AS itemDescription, ProcurementDetails.POUnitRate, ProcurementDetails.POQty, UOMMaster.UomCode, 
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
                         SupplierMaster ON SupplierInvoiceMaster.Supplier_Pk = SupplierMaster.Supplier_PK WHERE        (ProcurementDetails.PO_Pk = @Param1)";



                cmd.Parameters.AddWithValue("@Param1", PO_PK);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }

   


              public DataTable GetDocumentByPO(int PO_PK)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = @"     SELECT DocMaster.DocNum, ISNULL(SkuRawMaterialMaster.Composition, '') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, '') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, '')
                         + ' ' + ISNULL(SkuRawMaterialMaster.Width, '') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, '') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, '') + ' ' + ISNULL(ProcurementDetails.SupplierSize, '')
                         + ' ' + ISNULL(ProcurementDetails.SupplierColor, '') AS itemDescription, DocDetails.Donumber, DocDetails.Qty, ISNULL(DocDetails.ExtraQty, 0) AS ExtraQty, DocDetails.Eta, DocDetails.AddedDate, 
                         DocDetails.AddedBy, DocMaster.Doc_Pk
FROM            ProcurementDetails INNER JOIN
                         DocDetails ON ProcurementDetails.PODet_PK = DocDetails.PODet_Pk INNER JOIN
                         DocMaster ON DocDetails.Doc_Pk = DocMaster.Doc_Pk INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
WHERE        (ProcurementDetails.PO_Pk = @Param1)";



                cmd.Parameters.AddWithValue("@Param1", PO_PK);

                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);



            }
            return dt;
        }



    }
}