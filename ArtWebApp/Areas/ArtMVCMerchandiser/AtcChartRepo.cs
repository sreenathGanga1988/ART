using ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.ArtMVCMerchandiser
{
    public static class AtcChartRepo
    {

        public static AtcChartmaster fillAtcChartViewModel(int atcid, String type, int ourstyleid)
        {
            string onhandtype = "A";

            AtcChartmaster atcChartmaster = new AtcChartmaster();


            atcChartmaster.BomData = BLL.FactoryAtcChart.ShowBOM(atcid, type, ourstyleid);



            atcChartmaster.procurementplandata = BLL.FactoryAtcChart.GetProcurementPlan(atcid);
            atcChartmaster.Inbounddata = BLL.FactoryAtcChart.GetInboundData(atcid);
            atcChartmaster.Podataofatc = BLL.FactoryAtcChart.GetPODataofAtc(atcid);
            atcChartmaster.GoodsinTransit = BLL.FactoryAtcChart.GetTransistQty(atcid);
            atcChartmaster.onhandqty = BLL.FactoryAtcChart.GetOnhandQty(atcid, onhandtype);
            atcChartmaster .cutorderofatc = BLL.FactoryAtcChart.GetCutOrderDetails(atcid);
            atcChartmaster. recptofAtc = BLL.InventoryBLL.FactoryInventory.GetallReceipt(atcid);
            atcChartmaster. AdnofATC = BLL.FactoryAtcChart.GetADNDetails(atcid);
            atcChartmaster. RemarkofATC = BLL.FactoryAtcChart.GetPlanningRemark(atcid);
            atcChartmaster. OurStyledata = BLL.FactoryAtcChart.GetOurStyleConsumption(atcid);





            atcChartmaster.BomDataList = ConverrttoBOMObject(atcChartmaster);


            atcChartmaster.procurementplandata =null;
            atcChartmaster.Inbounddata = null;
            atcChartmaster.Podataofatc = null;
            atcChartmaster.GoodsinTransit = null;
            atcChartmaster.onhandqty = null;
            atcChartmaster.cutorderofatc = null;
            atcChartmaster.recptofAtc = null;
            atcChartmaster.AdnofATC = null;
            atcChartmaster.RemarkofATC = null;
            atcChartmaster.OurStyledata = null;






            return atcChartmaster;
        }


        public static List<BomData> ConverrttoBOMObject(AtcChartmaster atcChartmaster)
        {
            List<BomData> BomDatalist = new List<BomData>();

            foreach (DataRow row in atcChartmaster.BomData.Rows)
            {
                List<PlannedDetails> plannedDetailslist = new List<PlannedDetails>();
                List<PODetails> pOdetailslist = new List<PODetails>();
                List<ADNDetails> aDNDetailsList = new List<ADNDetails>();
                List<InBoundDetails> inBoundDetailsList = new List<InBoundDetails>();
                List<OrderOnHand> orderOnHandList = new List<OrderOnHand>();
                List<RemarkDetails> remarkDetailsList = new List<RemarkDetails>();
                List<OurStyleDetails> OurStyleDetailsList = new List<OurStyleDetails>();
                 List<ReceiptDetails> ReceiptDetailsList= new List<ReceiptDetails>();

                BomData bomData = new BomData();
                bomData.RMNum = row["RMNum"].ToString();
                bomData.Description = row["Description"].ToString();
                bomData.ColorName = row["ColorName"].ToString();
                bomData.SizeName = row["SizeName"].ToString();
                bomData.ItemColor = row["ItemColor"].ToString();
                bomData.ItemSize = row["ItemSize"].ToString();
                bomData.UnitRate = row["UnitRate"].ToString();
                bomData.GarmentQty = row["GarmentQty"].ToString();
                bomData.Consumption = row["Consumption"].ToString();
                bomData.WastagePercentage = row["WastagePercentage"].ToString();
                bomData.RqdQty = row["RqdQty"].ToString();
                bomData.UomCode = row["UomCode"].ToString();
                bomData.PlannedQty = "0";
                bomData.PlannedDetails = "";
                bomData.balanceToPlan = "";
                bomData.PoIssuedQty = row["PoIssuedQty"].ToString();
                bomData.PODetails = "";
                bomData.BalanceQty = row["BalanceQty"].ToString();
                bomData.ADNDetails = "";
                bomData.ShippingDetails = "";
                bomData.OnhandDetails = "";
                bomData.PendingtoRecieve = "";
                bomData.TransistDetails = "";
                bomData.CutorderDetails = "";
                bomData.ReceiptDetails = "";
                bomData.Remark = "";
                bomData.Styles = "";
                bomData.SkuDet_PK = row["SkuDet_PK"].ToString();
                bomData.SkuPK = row["Sku_Pk"].ToString();

                #region PlannDetail
                try
                {
                    using (DataTable tempdt = atcChartmaster.procurementplandata.Select("Skudet_Pk=" + bomData.SkuDet_PK).CopyToDataTable())
                    {

                        bomData.PlannedQty = tempdt.Compute("Sum(Qty)", "").ToString();

                        foreach (DataRow dt in tempdt.Rows)
                        {
                            PlannedDetails plannedDetails = new PlannedDetails();
                            plannedDetails.ETADate = dt["ETADate"].ToString();
                            plannedDetails.Qty = dt["Qty"].ToString();

                            plannedDetailslist.Add(plannedDetails);
                        }


                    }
                }
                catch (Exception)
                {


                }

                #endregion

                #region PoDetails
                try
                {


                    using (DataTable tempdt = atcChartmaster.Podataofatc.Select("Skudet_Pk=" + bomData.SkuDet_PK).CopyToDataTable())
                    {



                        foreach (DataRow dt in tempdt.Rows)
                        {
                            PODetails pODetails = new PODetails();
                            pODetails.PONum = dt["PONum"].ToString();
                            pODetails.POQty = dt["POQty"].ToString();
                            pODetails.UomCode = dt["UomCode"].ToString();
                            pODetails.SupplierName = dt["SupplierName"].ToString();
                            pODetails.BaseUOMQty = dt["BaseUOMQty"].ToString();

                            pOdetailslist.Add(pODetails);

                        }


                    }
                }
                catch (Exception ex)
                {


                }
                #endregion

                #region InBoundDetails
                try
                {


                    using (DataTable tempdt = atcChartmaster.Inbounddata.Select("Skudet_Pk=" + bomData.SkuDet_PK).CopyToDataTable())
                    {



                        foreach (DataRow dt in tempdt.Rows)
                        {
                            InBoundDetails inBoundDetails = new InBoundDetails();
                            inBoundDetails.Conatianer = dt["Conatianer"].ToString();
                            inBoundDetails.ETA = dt["ETA"].ToString();
                            inBoundDetails.Qty = dt["Qty"].ToString();
                            inBoundDetails.ShipperInv = dt["ShipperInv"].ToString();


                            inBoundDetailsList.Add(inBoundDetails);

                        }


                    }
                }
                catch (Exception ex)
                {


                }
                #endregion


                #region ADNDetails
                try
                {


                    using (DataTable tempdt = atcChartmaster.AdnofATC.Select("Skudet_Pk=" + bomData.SkuDet_PK).CopyToDataTable())
                    {



                        foreach (DataRow dt in tempdt.Rows)
                        {
                            ADNDetails aDNDetails = new ADNDetails();
                            aDNDetails.DocNum = dt["DocNum"].ToString();
                            aDNDetails.ContainerNum = dt["ContainerNum"].ToString();
                            aDNDetails.BOENum = dt["BOENum"].ToString();
                            aDNDetails.PONum = dt["PONum"].ToString();

                            aDNDetails.Qty = dt["Qty"].ToString();
                            aDNDetails.ExtraQty = dt["ExtraQty"].ToString();
                            aDNDetails.ADNType = dt["ADNType"].ToString();
                            aDNDetailsList.Add(aDNDetails);

                        }


                    }
                }
                catch (Exception ex)
                {


                }
                #endregion


                #region Order OnHand
                try
                {


                    using (DataTable tempdt = atcChartmaster.onhandqty.Select("Skudet_Pk=" + bomData.SkuDet_PK).CopyToDataTable())
                    {



                        foreach (DataRow dt in tempdt.Rows)
                        {
                            OrderOnHand orderOnHand = new OrderOnHand();
                            orderOnHand.LocationPrefix = dt["LocationPrefix"].ToString();
                            orderOnHand.OnhandQty = Decimal.Parse(dt["OnhandQty"].ToString());
                            orderOnHand.BaseUOMQty = Decimal.Parse( dt["BaseUOMQty"].ToString());
                            orderOnHand.LocType = dt["LocType"].ToString();

                            
                            orderOnHandList.Add(orderOnHand);

                        }


                    }
                }
                catch (Exception ex)
                {


                }
                #endregion

                #region Remark
                try
                {


                    using (DataTable tempdt = atcChartmaster.RemarkofATC.Select("Skudet_Pk=" + bomData.SkuDet_PK).CopyToDataTable())
                    {



                        foreach (DataRow dt in tempdt.Rows)
                        {
                            RemarkDetails remarkDetails = new RemarkDetails();
                            remarkDetails.Remark = dt["Remark"].ToString();
                            remarkDetails.AddedDate = DateTime.Parse(dt["AddedDate"].ToString()).ToString("MM/dd/yyyy");
                            remarkDetails.AddedBy = dt["AddedBy"].ToString();
                           


                            remarkDetailsList.Add(remarkDetails);

                        }


                    }
                }
                catch (Exception ex)
                {


                }
                #endregion

                #region OurStyle 
                try
                {


                    using (DataTable tempdt = atcChartmaster.OurStyledata.Select("Skudet_Pk=" + bomData.SkuDet_PK).CopyToDataTable())
                    {



                        foreach (DataRow dt in tempdt.Rows)
                        {
                            OurStyleDetails ourStyleDetails = new OurStyleDetails();
                            ourStyleDetails.OurStyle = dt["OurStyle"].ToString();
                            ourStyleDetails.Consumption = dt["Consumption"].ToString();
                          



                            OurStyleDetailsList.Add(ourStyleDetails);

                        }


                    }
                }
                catch (Exception ex)
                {


                }
                #endregion

                #region Receipt Details
                try
                {


                    using (DataTable tempdt = atcChartmaster.recptofAtc.Select("Skudet_Pk=" + bomData.SkuDet_PK).CopyToDataTable())
                    {

                        foreach (DataRow dt in tempdt.Rows)
                        {
                            ReceiptDetails receiptDetails = new ReceiptDetails();
                            receiptDetails.MrnNum = dt["MrnNum"].ToString();
                            receiptDetails.Qty = Decimal.Parse(dt["Qty"].ToString());
                            receiptDetails.UomCode = dt["UomCode"].ToString();
                            receiptDetails.PONum = dt["PONum"].ToString();
                            receiptDetails.LocationPrefix = dt["LocationPrefix"].ToString();
                           



                            ReceiptDetailsList.Add(receiptDetails);

                        }


                    }
                }
                catch (Exception ex)
                {


                }
                #endregion

                bomData.PODetailsList = pOdetailslist;
                bomData.PlannedDetailsList = plannedDetailslist;
                bomData.ADNDetailsList = aDNDetailsList;
                bomData.InBoundDetailsList = inBoundDetailsList;
                bomData.OrderOnHandList = orderOnHandList;
                bomData.RemarkDetailsList = remarkDetailsList;
                bomData.OurStyleDetailsList = OurStyleDetailsList;
                bomData.ReceiptDetailsList = ReceiptDetailsList;
                BomDatalist.Add(bomData);

            }

            return BomDatalist;
        }




    }
}