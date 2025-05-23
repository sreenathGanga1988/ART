﻿using ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using ArtWebApp.DataModelAtcWorld;
namespace ArtWebApp.Areas.Repository
{
    public class MerchandiserRepo
    {











    }

    public class FreightChargeRepo
    {

        String connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;

        public String InsertFreightCharges(FreightRequestMasterViewModel order)
        {

            String reqnum = "";
            using (ArtEntitiesnew db = new ArtEntitiesnew())
            {
                FreightRequestMaster mstr = new FreightRequestMaster();
                mstr.FromParty = order.FromParty;
                mstr.ToParty = order.ToParty;
                mstr.Shipper = order.Shipper;
                mstr.Weight = order.Weight;
                mstr.ContentofPackage = order.ContentofPackage;
                mstr.DebitTo = order.DebitTo;
                mstr.Reason = order.Reason;
                mstr.Merchandiser = order.Merchandiser;
                mstr.ForwarderDetails = order.ForwarderDetails;
                mstr.ApproximateCharges = order.ApproximateCharges;
                mstr.Remark = order.Remark;
                mstr.IsApproved = "N";
                mstr.IsDeleted = "N";
                mstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
                mstr.AddedDate = DateTime.Now;
                mstr.ShipementType = order.ShipmentType;
                db.FreightRequestMasters.Add(mstr);

                db.SaveChanges();


                if (order.ShipmentType == "Sea")
                {
                    reqnum = mstr.FreightRequestNum = "SFRQ-" + mstr.FreightRequestID;
                }
                else
                {
                    reqnum = mstr.FreightRequestNum = "FRQ-" + mstr.FreightRequestID;
                }


                foreach (FreightChargeDetailViewMoodel element in order.FreightChargeDetails)
                {

                    FreightChargeDetail frightchargedetail = new FreightChargeDetail();
                    frightchargedetail.AtcID = element.AtcID;
                    frightchargedetail.FreightCharge = element.FreightCharge;
                    frightchargedetail.FreightRequestID = mstr.FreightRequestID;
                    frightchargedetail.FirstFreightCharge = element.FreightCharge;
                    db.FreightChargeDetails.Add(frightchargedetail);
                }

                db.SaveChanges();
            }

            return reqnum;
        }
        public String InsertStocFreightCharges(FreightRequestMasterViewModel order)
        {

            String reqnum = "";
            using (ArtEntitiesnew db = new ArtEntitiesnew())
            {
                StockFreightRequestMaster mstr = new StockFreightRequestMaster();
                mstr.FromParty = order.FromParty;
                mstr.ToParty = order.ToParty;
                mstr.Shipper = order.Shipper;
                mstr.Weight = order.Weight;
                mstr.ContentofPackage = order.ContentofPackage;
                mstr.DebitTo = order.DebitTo;
                mstr.Reason = order.Reason;
                mstr.Merchandiser = order.Merchandiser;
                mstr.ForwarderDetails = order.ForwarderDetails;
                mstr.ApproximateCharges = order.ApproximateCharges;
                mstr.Remark = order.Remark;
                mstr.IsApproved = "N";
                mstr.IsDeleted = "N";
                mstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
                mstr.AddedDate = DateTime.Now;
                mstr.ShipementType = order.ShipmentType;
                db.StockFreightRequestMasters.Add(mstr);

                db.SaveChanges();

                reqnum = mstr.StockFreightRequestNum = "STFRQ-" + mstr.StockFreightRequestID;

                foreach (StockFreightChargeDetailViewMoodel element in order.stockFreightChargeDetails)
                {

                    StockFreightChargeDetail frightchargedetail = new StockFreightChargeDetail();
                    frightchargedetail.SPO_PK = int.Parse(element.SpoPK.ToString());
                    frightchargedetail.SPODetails_PK = int.Parse(element.SPODetails_PK.ToString());
                    frightchargedetail.StockFreightCharge = element.FreightCharge;
                    frightchargedetail.StockFreightRequestID = mstr.StockFreightRequestID;
                    frightchargedetail.FirstStockFreightCharge = element.FreightCharge;
                    db.StockFreightChargeDetails.Add(frightchargedetail);
                }

                db.SaveChanges();
            }

            return reqnum;
        }
        public String InsertLabCharges(LabChargeMasterViewModel order)
        {

            String reqnum = "";
            using (ArtEntitiesnew db = new ArtEntitiesnew())
            {
                LabRequestMaster mstr = new LabRequestMaster();
                mstr.Supplier_pk = order.SupplierPK;

                mstr.Reason = order.Reason;
                mstr.Merchandiser = order.Merchandiser;

                mstr.ApproximateCharges = order.ApproximateCharges;
                mstr.Remark = order.Remark;
                mstr.IsApproved = "N";

                mstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
                mstr.AddedDate = DateTime.Now;

                db.LabRequestMasters.Add(mstr);

                db.SaveChanges();

                reqnum = mstr.LabRequestNum = "LRQ-" + mstr.LabRequestID;

                foreach (FreightChargeDetailViewMoodel element in order.FreightChargeDetails)
                {

                    LabChargeDetail frightchargedetail = new LabChargeDetail();
                    frightchargedetail.AtcID = element.AtcID;
                    frightchargedetail.LabCharge = element.FreightCharge;
                    frightchargedetail.FirstLabCharge = element.FreightCharge;
                    frightchargedetail.LabRequestID = mstr.LabRequestID;
                    db.LabChargeDetails.Add(frightchargedetail);
                }

                db.SaveChanges();
            }

            return reqnum;
        }

        public String UpdateFreightCharges(FreightRequestMasterViewModel order)
        {

            String reqnum = "";
            using (ArtEntitiesnew db = new ArtEntitiesnew())
            {


                var q = from frgtmstr in db.FreightRequestMasters
                        where frgtmstr.FreightRequestID == order.FreightRequestID
                        select frgtmstr;

                foreach (var element in q)
                {
                    element.FromParty = order.FromParty;
                    element.ToParty = order.ToParty;
                    element.Shipper = order.Shipper;
                    element.Weight = order.Weight;
                    element.ContentofPackage = order.ContentofPackage;
                    element.DebitTo = order.DebitTo;
                    element.Reason = order.Reason;
                    element.Merchandiser = order.Merchandiser;
                    element.ForwarderDetails = order.ForwarderDetails;
                    element.ApproximateCharges = order.ApproximateCharges;
                    element.Remark = order.Remark;

                    try
                    {
                        var tot = order.FreightChargeDetails.Sum(u => u.FreightCharge);
                        element.ApproximateCharges = tot.ToString();
                    }
                    catch (Exception)
                    {
                        element.ApproximateCharges = order.ApproximateCharges;

                    }
                    if (element.IsApproved == "Y")
                    {
                        element.IsApproved = order.IsApproved;
                    }
                    element.IsDeleted = "N";
                    element.AddedBy = HttpContext.Current.Session["Username"].ToString();
                    element.AddedDate = DateTime.Now;

                }







                foreach (FreightChargeDetailViewMoodel element in order.FreightChargeDetails)
                {

                    var q1 = from frgtdet in db.FreightChargeDetails
                             where frgtdet.FreightReqDetID == element.FreightReqDetID
                             select frgtdet;

                    foreach (var frightchargedetail in q1)
                    {
                        frightchargedetail.FreightCharge = element.FreightCharge;
                        frightchargedetail.Remark = element.Remark;
                    }



                }

                db.SaveChanges();
            }

            return reqnum;
        }
        public string UpdateLabCharge(LabChargeMasterViewModel order)
        {
            String reqnum = "";
            using (ArtEntitiesnew db = new ArtEntitiesnew())
            {
                var q = from labchargemaster in db.LabRequestMasters
                        where labchargemaster.LabRequestID == order.LabRequestID
                        select labchargemaster;
                foreach (var element in q)
                {

                    element.Merchandiser = order.Merchandiser;
                    element.Reason = order.Reason;
                    element.Remark = order.Remark;
                    element.Supplier_pk = order.SupplierPK;
                    element.IsApproved = order.IsApproved;
                    element.AddedBy = HttpContext.Current.Session["Username"].ToString();
                    element.AddedDate = DateTime.Now;
                }
                foreach (FreightChargeDetailViewMoodel element in order.FreightChargeDetails)
                {
                    var q1 = from labdet in db.LabChargeDetails
                             where labdet.LabReqDetID == element.FreightReqDetID
                             select labdet;

                    foreach (var labdetails in q1)
                    {
                        labdetails.LabCharge = element.FreightCharge;
                        labdetails.Remark = element.Remark;
                    }



                }
                db.SaveChanges();

            }
            return reqnum;
        }
        /// <summary>
        /// get the last costing
        /// </summary>
        /// <param name="ourstyle"></param>
        /// <returns></returns>
        public FreightChargeDetail GetAllowedFreightCharges(FreightChargeDetail freightChargeDetailnew)
        {
            Decimal costingpk = 0;
            Decimal alreadyUsed = 0;


            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT      sum(  Allowedvalue) As AllowedValue
FROM            (SELECT        OurStyleID, Qty * Compvalue AS Allowedvalue, AtcId
                          FROM            (SELECT        OurStyleID, ISNULL
                             ((SELECT        SUM(PoQty) AS Expr1
                                 FROM            POPackDetails
                                 WHERE        (OurStyleID = AtcDetails.OurStyleID)), 0) AS Qty, ISNULL
                             ((SELECT        MAX(StyleCostingComponentDetails.CompValue) AS Expr1
                                 FROM            StyleCostingComponentDetails INNER JOIN
                                                          StyleCostingMaster ON StyleCostingComponentDetails.Costing_PK = StyleCostingMaster.Costing_PK
                                 WHERE        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingComponentDetails.CostComp_PK = 8) AND (StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID)), 0) AS Compvalue, AtcId
FROM            AtcDetails
WHERE        (AtcId = @atcid)) AS tt) AS ttt
GROUP BY  AtcId", con);

                cmd.Parameters.AddWithValue("@atcid", freightChargeDetailnew.AtcID);
                try
                {
                    costingpk = Decimal.Parse(cmd.ExecuteScalar().ToString());
                }
                catch (Exception)
                {

                    costingpk = 0;
                }



            }
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from frgdet in enty.FreightChargeDetails
                         where frgdet.AtcID == freightChargeDetailnew.AtcID
                         select new { frgdet.FreightCharge }).ToList();

                foreach (var element in q)
                {

                    try
                    {
                        if (decimal.Parse(element.FreightCharge.ToString()) > 0)
                        {
                            alreadyUsed = alreadyUsed + decimal.Parse(element.FreightCharge.ToString());
                        }

                    }
                    catch (Exception)
                    {


                    }
                }

                var q1 = (from frgdet in enty.ProcurementMasters
                          where frgdet.AtcId == freightChargeDetailnew.AtcID && frgdet.IsDeleted == "N"
                          select new { frgdet.FreightCharge }).ToList();

                foreach (var element in q1)
                {

                    try
                    {
                        if (decimal.Parse(element.FreightCharge.ToString()) > 0)
                        {
                            alreadyUsed = alreadyUsed + decimal.Parse(element.FreightCharge.ToString());
                        }

                    }
                    catch (Exception)
                    {


                    }
                }


            }




            freightChargeDetailnew.AllowedValue = costingpk.ToString();
            freightChargeDetailnew.UsedValue = alreadyUsed.ToString();
            freightChargeDetailnew.BalanceValue = (Decimal.Parse(costingpk.ToString()) - Decimal.Parse(alreadyUsed.ToString())).ToString();
            costingpk = costingpk - alreadyUsed;


            return freightChargeDetailnew;
        }
        public LabChargeDetail GetAllowedLabCharges(LabChargeDetail freightChargeDetailnew)
        {
            Decimal costingpk = 0;
            Decimal alreadyUsed = 0;


            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT      sum(  Allowedvalue) As AllowedValue
FROM            (SELECT        OurStyleID, Qty * Compvalue AS Allowedvalue, AtcId
                          FROM            (SELECT        OurStyleID, ISNULL
                             ((SELECT        SUM(PoQty) AS Expr1
                                 FROM            POPackDetails
                                 WHERE        (OurStyleID = AtcDetails.OurStyleID)), 0) AS Qty, ISNULL
                             ((SELECT        MAX(StyleCostingComponentDetails.CompValue) AS Expr1
                                 FROM            StyleCostingComponentDetails INNER JOIN
                                                          StyleCostingMaster ON StyleCostingComponentDetails.Costing_PK = StyleCostingMaster.Costing_PK
                                 WHERE        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingComponentDetails.CostComp_PK = 10) AND (StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID)), 0) AS Compvalue, AtcId
FROM            AtcDetails
WHERE        (AtcId = @atcid)) AS tt) AS ttt
GROUP BY  AtcId", con);

                cmd.Parameters.AddWithValue("@atcid", freightChargeDetailnew.AtcID);
                try
                {
                    costingpk = Decimal.Parse(cmd.ExecuteScalar().ToString());
                }
                catch (Exception)
                {

                    costingpk = 0;
                }



            }
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from frgdet in enty.LabChargeDetails
                         where frgdet.AtcID == freightChargeDetailnew.AtcID
                         select new { frgdet.LabCharge }).ToList();

                foreach (var element in q)
                {

                    try
                    {
                        if (decimal.Parse(element.LabCharge.ToString()) > 0)
                        {
                            alreadyUsed = alreadyUsed + decimal.Parse(element.LabCharge.ToString());
                        }

                    }
                    catch (Exception)
                    {


                    }
                }



            }




            freightChargeDetailnew.AllowedValue = costingpk.ToString();
            freightChargeDetailnew.UsedValue = alreadyUsed.ToString();
            freightChargeDetailnew.BalanceValue = (Decimal.Parse(costingpk.ToString()) - Decimal.Parse(alreadyUsed.ToString())).ToString();
            costingpk = costingpk - alreadyUsed;


            return freightChargeDetailnew;
        }


    }


    public class AtcperformanceRepo
    {
        public DataTable GetGarmentdetails(int Atcid)
        {
           SqlCommand cmd =new SqlCommand ( @"select Atc_id, GoodPcs, RejectPcs from OCRATCWise_VW where atc_id=@Atcid");
            cmd.Parameters.AddWithValue("@AtcId", Atcid);
            return QueryFunctions.ReturnQueryResultDatatablefromAtcWorldkENYA(cmd);
        }

        public DataTable getShippedQty(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@"select InvoiceQty,( InvoiceQty *FOB ) as price from InvoiceDetail where OurStyleID in(select OurStyleID  from AtcDetails  where AtcId =@AtcId)");

            SqlCommand cmd2 = new SqlCommand(@" 
            SELECT        AtcDetails.AtcId, ShipmentHandOverDetails.ShippedQty, ShipmentHandOverDetails.FCM
            FROM            ShipmentHandOverDetails INNER JOIN
                                     AtcDetails ON ShipmentHandOverDetails.OurStyleID = AtcDetails.OurStyleID
            WHERE        (AtcDetails.AtcId = @AtcId)");


            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }

        public DataTable getfabricdetails(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@" SELECT        AtcMaster.AtcNum, ProcurementDetails.CURate, MrnDetails.ReceiptQty, MrnDetails.ExtraQty, ProcurementMaster.POType, MrnDetails.ReceiptQty + MrnDetails.ExtraQty AS TotalQTY, 
                         (MrnDetails.ReceiptQty + MrnDetails.ExtraQty) * ProcurementDetails.CURate AS PurchaseValue, Template_Master.ItemGroup_PK
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         MrnMaster INNER JOIN
                         MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                         ProcurementDetails ON MrnDetails.PODet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId ON SkuRawmaterialDetail.SkuDet_PK = MrnDetails.SkuDet_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (AtcMaster.AtcId = @AtcId) and Template_Master.ItemGroup_PK =1");


            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }

        public DataTable getTrimsdetails(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@" SELECT        AtcMaster.AtcNum, ProcurementDetails.CURate, MrnDetails.ReceiptQty, MrnDetails.ExtraQty, ProcurementMaster.POType, MrnDetails.ReceiptQty + MrnDetails.ExtraQty AS TotalQTY, 
                         (MrnDetails.ReceiptQty + MrnDetails.ExtraQty) * ProcurementDetails.CURate AS PurchaseValue, Template_Master.ItemGroup_PK
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         MrnMaster INNER JOIN
                         MrnDetails ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK INNER JOIN
                         ProcurementDetails ON MrnDetails.PODet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId ON SkuRawmaterialDetail.SkuDet_PK = MrnDetails.SkuDet_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (AtcMaster.AtcId = @AtcId) and Template_Master.ItemGroup_PK =2");


            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }
        public DataTable getFreightCharge(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@" select isnull(sum(FreightCharge ),0)as FreightCharge from FreightChargeDetail a , FreightRequestMaster b where
    a.FreightRequestID =b.FreightRequestID and b.IsApproved ='Y' and a.AtcID =@AtcId");

            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }
        public DataTable getEmbroideryCharge(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@" select b.shippedqty,  (b.ShippedQty*a.EmbroidaryPrinting )as charge  from JobContractOptionalDetail a, ShipmentHandOverDetails  b ,AtcDetails c
where a.OurStyleID =b.OurStyleID and c.AtcId =@AtcId and c.OurStyleID =b.OurStyleID and a.EmbroidaryPrinting  >0");

            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }
        public DataTable getWashingCharge(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@" select b.shippedqty,  (b.ShippedQty*a.Wash )as charge  from JobContractOptionalDetail a, ShipmentHandOverDetails  b ,AtcDetails c
where a.OurStyleID =b.OurStyleID and c.AtcId =@AtcId and c.OurStyleID =b.OurStyleID and a.Wash  >0");

            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }
        public DataTable getPrintingCharge(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@" select b.shippedqty,  (b.ShippedQty*a.Printing )as charge  from JobContractOptionalDetail a, ShipmentHandOverDetails  b ,AtcDetails c
where a.OurStyleID =b.OurStyleID and c.AtcId =@AtcId and c.OurStyleID =b.OurStyleID and a.Printing  >0");

            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }
        public DataTable getFactoryLogisticCharge(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@" select b.shippedqty,  (b.ShippedQty*a.FactoryLogistic )as charge  from JobContractOptionalDetail a, ShipmentHandOverDetails  b ,AtcDetails c
where a.OurStyleID =b.OurStyleID and c.AtcId =@AtcId and c.OurStyleID =b.OurStyleID and a.FactoryLogistic  >0");

            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }

        public DataTable getCMCharge(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@"select b.shippedqty,  (b.ShippedQty*a.CM  )as charge  from JobContractMaster a, ShipmentHandOverDetails  b ,AtcDetails c
where a.OurStyleID =b.OurStyleID and c.AtcId =@AtcId and c.OurStyleID =b.OurStyleID and a.CM   >0");

            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }


        public DataTable getInventoryMisplaced(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@" 
SELECT        InventoryMissingDetails.Qty, InventoryMaster.CURate
FROM            InventoryMissingDetails INNER JOIN
                         InventoryMissingRequest ON InventoryMissingDetails.MisplaceApp_PK = InventoryMissingRequest.MisplaceApp_pk INNER JOIN
                         InventoryMaster ON InventoryMissingDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK
WHERE        (InventoryMissingRequest.Atc_id = @AtcId) and  (InventoryMissingRequest.IsApproved ='Y')");


            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }
        public DataTable getShortShipment(int AtcId)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd1 = new SqlCommand(@"select sum(a.PoQty )as POQty, sum(a.PoQty*b.FOB  )as POValue,0 as shipqty,0 as shipvalue from POPackDetails a, AtcDetails b where a.OurStyleID =b.OurStyleID and b.AtcId =@AtcId group by b.FOB 
union 
select 0 as POQty, 0 as POValue, sum(a.invoiceqty) as shipqty, sum(a.invoiceqty*a.fob)as shipvalue from InvoiceDetail a, AtcDetails b where a.OurStyleID =b.OurStyleID and b.AtcId =@AtcId group by a.FOB");

            SqlCommand cmd = new SqlCommand(@" SELECT 
                MAX(CASE WHEN poqty>0 THEN poqty ELSE 0 END) as POQty,
                MAX(CASE WHEN POValue>0 THEN POValue ELSE 0 END) as POValue,
                MAX(CASE WHEN shipqty>0 THEN shipqty ELSE 0 END) as shipqty,
                MAX(CASE WHEN shipvalue>0 THEN shipvalue ELSE 0 END) as shipvalue
                from(
                select sum(a.PoQty )as POQty, sum(a.PoQty*b.FOB  )as POValue,0 as shipqty,0 as shipvalue from POPackDetails a, AtcDetails b where a.OurStyleID =b.OurStyleID and b.AtcId =@AtcId
                union 
                select 0 as POQty,0 as POValue, sum(a.ShippedQty )as shipqty, sum(a.ShippedQty*b.FOB ) as shipvalue From ShipmentHandOverDetails a, AtcDetails b where a.OurStyleID =b.OurStyleID and b.AtcId =@AtcId 
                )as t");


            cmd1.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd1);
        }
        public DataTable GetFabricLeftover(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@" 
SELECT        InventoryMaster.InventoryItem_PK, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum, InventoryMaster.SkuDet_Pk,SkuRawMaterialMaster.Template_pk,
                SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                 ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, InventoryMaster.OnhandQty, InventoryMaster.OnhandQty as PhysicalQty,
                LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value,AtcMaster.AtcID,LocationMaster.Location_PK,
                0 as DiffQty,InventoryMaster.CURate as ActualRate,0 as Packages
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
                SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk ON MrnDetails.MrnDet_PK = InventoryMaster.MrnDet_PK WHERE AtcMaster.AtcId=@AtcId  and (ItemGroupMaster.ItemGroupName = N'Fabric')
                 AND (InventoryMaster.OnhandQty > 0) and LocationMaster. Location_PK in(2,3,4,5,6,10,13,15,16,17,22,30,37)
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize,
                  ProcurementDetails.SupplierColor, UOMMaster.UomCode");


            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }
        public DataTable GetTrimsLeftover(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@" 
SELECT        InventoryMaster.InventoryItem_PK, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum, InventoryMaster.SkuDet_Pk,SkuRawMaterialMaster.Template_pk,
                SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                 ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.ReceivedQty, InventoryMaster.DeliveredQty, InventoryMaster.OnhandQty, InventoryMaster.OnhandQty as PhysicalQty,
                LocationMaster.LocationName, InventoryMaster.ReceivedVia, InventoryMaster.Refnum, InventoryMaster.CURate, InventoryMaster.CURate * InventoryMaster.OnhandQty AS Value,AtcMaster.AtcID,LocationMaster.Location_PK,
                0 as DiffQty,InventoryMaster.CURate as ActualRate,0 as Packages
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
                SkuRawmaterialDetail.SkuDet_PK = InventoryMaster.SkuDet_Pk ON MrnDetails.MrnDet_PK = InventoryMaster.MrnDet_PK WHERE AtcMaster.AtcId=@AtcId  and (ItemGroupMaster.ItemGroupName = N'Trims')
                 AND (InventoryMaster.OnhandQty > 0) and LocationMaster .Location_PK in(2,3,4,5,6,10,13,15,16,17,22,30,37)
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize,
                  ProcurementDetails.SupplierColor, UOMMaster.UomCode");


            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }
    }
    public class AtcClosingRepo
    {

        public DataTable GetNonClosedAtc()
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@"NonClosedAtcList_SP");

            cmd.CommandType = CommandType.StoredProcedure;


            return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }

        public DataTable GetClosedATCList(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@"  SELECT        PoPackId, PoPacknum, BuyerPO, OurStyle, BuyerStyle, POQty, ShipedQty, OurStyleID, FirstDeliveryDate, DeliveryDate, HandoverDate, AtcId
FROM            (SELECT        PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, SUM(POPackDetails.PoQty) AS POQty, ISNULL
                                                        ((SELECT        SUM(ShippedQty) AS Expr1
                                                            FROM            ShipmentHandOverDetails
                                                            GROUP BY POPackId, OurStyleID
                                                            HAVING        (POPackId = PoPackMaster.PoPackId) AND (OurStyleID = POPackDetails.OurStyleID)), 0) AS ShipedQty, AtcDetails.OurStyleID, PoPackMaster.FirstDeliveryDate, PoPackMaster.DeliveryDate, 
                                                    PoPackMaster.AtcId, PoPackMaster.HandoverDate, MAX(POPackDetails.IsShortClosed) AS Expr1, AtcDetails.AtcId AS Expr2
                          FROM            PoPackMaster INNER JOIN
                                                    POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
                                                    AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID
                          GROUP BY PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, POPackDetails.OurStyleID, AtcDetails.OurStyleID, PoPackMaster.FirstDeliveryDate, 
                                                    PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, AtcDetails.AtcId
                          HAVING   (AtcDetails.AtcId=@AtcId)   and  (MAX(POPackDetails.IsShortClosed) <> N'Y') ) AS tt
WHERE        (POQty - ShipedQty > 0)");


            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }


        public DataTable GetClosedAtc(string month)
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@"SELECT        AtcMaster.AtcId, AtcMaster.AtcNum, BuyerMaster.BuyerName, CountryMaster.ShortName, AtcAction.ActionDoneBy, AtcAction.ActionDoneDate, AtcAction.ActionType, AtcAction.Month, AtcAction.Year
FROM            AtcMaster INNER JOIN
                         AtcAction ON AtcMaster.AtcId = AtcAction.AtcID INNER JOIN
                         BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID INNER JOIN
                         CountryMaster ON AtcMaster.ProductionCountryID = CountryMaster.CountryID
WHERE        (AtcAction.Month = @Param1)");


            cmd.Parameters.AddWithValue("@Param1", month);
            

            return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }



        public DataTable GetBEofMonthAtc(string month)
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@" SELECT AtcMaster.AtcId, AtcMaster.AtcNum, BuyerMaster.BuyerName, CountryMaster.ShortName, BEOfMonth.AddedBy as ActionDoneBy, BEOfMonth.AddedDate as ActionDoneDate,'BE' as ActionType, BEOfMonth.month,'0' as Year
FROM            AtcMaster INNER JOIN
                         BEOfMonth ON AtcMaster.AtcId = BEOfMonth.AtcID INNER JOIN
                         BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID INNER JOIN
                         CountryMaster ON AtcMaster.ProductionCountryID = CountryMaster.CountryID
WHERE        (BEOfmonth.Month = @Param1)");


            cmd.Parameters.AddWithValue("@Param1", month);


            return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }








       
        public List<AtcClosingModel> GetNonclosedatclist()
        {
            List<AtcClosingModel> ls = new List<AtcClosingModel>();

            DataTable dt = GetNonClosedAtc();
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow drow in dt.Rows)
                    {

                        AtcClosingModel atcClosingModel = new AtcClosingModel();
                        atcClosingModel.IsSelected = false;
                        atcClosingModel.AtcNum = drow["AtcNum"].ToString();
                        atcClosingModel.BuyerName = drow["BuyerName"].ToString();
                        atcClosingModel.NoofStyles = drow["NoofStyles"].ToString();
                        atcClosingModel.IsCompleted = drow["IsCompleted"].ToString();
                        atcClosingModel.IsClosed = drow["IsClosed"].ToString();
                        atcClosingModel.Description = drow["Description"].ToString();
                        atcClosingModel.ProjectionQty = drow["ProjectionQty"].ToString();
                        atcClosingModel.AtcId = int.Parse(drow["AtcId"].ToString());
                        ls.Add(atcClosingModel);
                    }
                }

            }

            return ls;


        }
        public List<AtcClosingModel> GetNonclosedatclistforshipmentclose()
        {
            List<AtcClosingModel> ls = new List<AtcClosingModel>();
            using (ArtEntitiesnew db = new ArtEntitiesnew())
            {
                var q1 = from AtcMaster in db.AtcMasters
                         where AtcMaster.IsShipmentCompleted != "Y"
                         select AtcMaster;
                foreach (var element in q1)
                {

                    DataTable atc = GetClosedATCList(int.Parse(element.AtcId.ToString()));

                    if (atc.Rows.Count == 0)
                    {
                        AtcClosingModel atcClosingModel = new AtcClosingModel();
                        atcClosingModel.IsSelected = false;
                        atcClosingModel.AtcNum = element.AtcNum;
                        var buyer = from BuyerMaster in db.BuyerMasters
                                    where BuyerMaster.BuyerID == element.Buyer_ID
                                    select BuyerMaster;
                        foreach (var buy in buyer)
                        {
                            atcClosingModel.BuyerName = buy.BuyerName;
                        }
                        var country = from CountryMaster in db.CountryMasters
                                      where CountryMaster.CountryID == element.ProductionCountryID
                                      select CountryMaster;
                        foreach (var procount in country)
                        {
                            atcClosingModel.Description = procount.Description;
                        }
                        atcClosingModel.NoofStyles = element.NoofStyles.ToString();
                        atcClosingModel.IsClosed = element.IsShipmentCompleted;
                        atcClosingModel.AtcId = int.Parse(element.AtcId.ToString());
                        ls.Add(atcClosingModel);
                    }
                }

            }

            return ls;


        }
        public DataTable AtcofMonth(string month)
        {
           
            DataTable dt = GetClosedAtc(month);
            if (dt != null)
            {

                

            }




            return dt;


        }
        public void CloseAtc(AtcClosingModelList atcClosingModelList)
        {


            foreach(AtcClosingModel atcClosingModel in atcClosingModelList.atcClosingModels)
            {
                
                using (AtcWorldEntities atcenty = new ArtWebApp.DataModelAtcWorld.AtcWorldEntities())
                {
                    using (ArtEntitiesnew enty = new ArtEntitiesnew())
                    {

                        Decimal atcid = 0;
                        var q = from atcmstr in enty.AtcMasters
                                where atcmstr.AtcId == atcClosingModel.AtcId
                                select atcmstr;
                        foreach (var element in q)
                        {
                            //element.IsClosed = "Y";
                            element.IsShipmentCompleted = "Y";
                            atcid = Decimal.Parse(element.AtcId.ToString());

                        }

                        AtcAction atcAction = new AtcAction();
                        atcAction.AtcID = atcClosingModel.AtcId;
                        atcAction.ActionType = atcClosingModelList.Type;
                        atcAction.ActionDoneDate = atcClosingModelList.Addeddate;
                        atcAction.ActionDoneBy = atcClosingModelList.AddedBy;
                        atcAction.Month = atcClosingModelList.Month;
                        enty.AtcActions.Add(atcAction);
                        enty.SaveChanges();
                        var ourstyleids = from AtcDetail in enty.AtcDetails
                                          where AtcDetail.AtcId==atcid
                                          select AtcDetail;
                        foreach(var ourstyle in ourstyleids)
                        {
                            ArtAtcClosingMaster artAtcClosingMaster = new ArtAtcClosingMaster();
                            artAtcClosingMaster.AtcId = atcid;
                            artAtcClosingMaster.AtcNum = ourstyle.AtcMaster.AtcNum;
                            artAtcClosingMaster.OurstyleId = ourstyle.OurStyleID;
                            artAtcClosingMaster.Ourstyle = ourstyle.OurStyle.Trim();
                            artAtcClosingMaster.IsClosed = "Y";
                            artAtcClosingMaster.AddedBy = HttpContext.Current.Session["Username"].ToString();
                            artAtcClosingMaster.AddedDate = DateTime.Now;
                            atcenty.ArtAtcClosingMasters.Add(artAtcClosingMaster);
                            atcenty.SaveChanges();
                        }
                    }
                }


            }


        }



        public void CloseAtcETH(AtcClosingModelList atcClosingModelList)
        {


            foreach (AtcClosingModel atcClosingModel in atcClosingModelList.atcClosingModels)
            {
                
                using (AtcWorldEntities atcenty = new ArtWebApp.DataModelAtcWorld.AtcWorldEntities("Ethiopia"))
                {
                    using (ArtEntitiesnew enty = new ArtEntitiesnew())
                    {

                        Decimal atcid = 0;
                        var q = from atcmstr in enty.AtcMasters
                                where atcmstr.AtcId == atcClosingModel.AtcId
                                select atcmstr;
                        foreach (var element in q)
                        {
                            //element.IsClosed = "Y";
                            element.IsShipmentCompleted = "Y";
                            atcid = Decimal.Parse(element.AtcId.ToString());

                        }

                        AtcAction atcAction = new AtcAction();
                        atcAction.AtcID = atcClosingModel.AtcId;
                        atcAction.ActionType = atcClosingModelList.Type;
                        atcAction.ActionDoneDate = atcClosingModelList.Addeddate;
                        atcAction.ActionDoneBy = atcClosingModelList.AddedBy;
                        atcAction.Month = atcClosingModelList.Month;
                        enty.AtcActions.Add(atcAction);
                        enty.SaveChanges();
                        var ourstyleids = from AtcDetail in enty.AtcDetails
                                          where AtcDetail.AtcId == atcid
                                          select AtcDetail;
                        foreach (var ourstyle in ourstyleids)
                        {
                            ArtAtcClosingMaster artAtcClosingMaster = new ArtAtcClosingMaster();
                            artAtcClosingMaster.AtcId = atcid;
                            artAtcClosingMaster.AtcNum = ourstyle.AtcMaster.AtcNum;
                            artAtcClosingMaster.OurstyleId = ourstyle.OurStyleID;
                            artAtcClosingMaster.Ourstyle = ourstyle.OurStyle.Trim();
                            artAtcClosingMaster.IsClosed = "Y";
                            artAtcClosingMaster.AddedBy = HttpContext.Current.Session["Username"].ToString();
                            artAtcClosingMaster.AddedDate = DateTime.Now;
                            atcenty.ArtAtcClosingMasters.Add(artAtcClosingMaster);
                            atcenty.SaveChanges();
                        }
                    }
                }


            }


        }


        public void BEofMonth(AtcClosingModelList atcClosingModelList)
        {


            foreach (AtcClosingModel atcClosingModel in atcClosingModelList.atcClosingModels)
            {
               
                    using (ArtEntitiesnew enty = new ArtEntitiesnew())
                    {



                        BEOfMonth bEOfMonth = new BEOfMonth();
                        bEOfMonth.AtcID = atcClosingModel.AtcId;
                        bEOfMonth.AddedDate = atcClosingModelList.Addeddate;
                        bEOfMonth.AddedBy = atcClosingModelList.AddedBy;
                        bEOfMonth.Month = atcClosingModelList.Month;
                        enty.BEOfMonths.Add(bEOfMonth);

                        enty.SaveChanges();
                    }
                }

        


        }


        public void RemoveMonth(String Month)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from bedet in enty.BEOfMonths
                        where bedet.Month == Month
                        select bedet;
                foreach(var element in q)
                {
                    enty.BEOfMonths.Remove(element);


                }

                enty.SaveChanges();
            }
            }
        }


}