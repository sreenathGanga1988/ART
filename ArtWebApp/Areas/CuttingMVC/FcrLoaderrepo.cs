using ArtWebApp.Areas.CuttingMVC.ViewModel;
using ArtWebApp.Areas.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.CuttingMVC
{
    public class FcrLoaderrepo
    {




        public FullFCRModelData LoadFcrofSkuLocation(int id, int locationpk)
        {
            Decimal GiventoFactory = 0;
            Decimal GivenBackToStore = 0;
            Decimal ToBeonLocation = 0;
            Decimal TotalFabricLayed = 0;
            Decimal TotalShortage = 0;
            Decimal MarkMissedQty = 0;
            Decimal TotalBalanceQty = 0;
            Decimal MissingQty = 0;
            FullFCRModelData fullFCRModelData = new FullFCRModelData();
            List<FCRViewModel> fcrMasterDatalist = new List<FCRViewModel>();

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var k = from fabdet in enty.FabricInLocation_tbl
                        where fabdet.SkuDet_PK == id && fabdet.Location_pk == locationpk
                        select fabdet;
                foreach (var element in k)
                {



                    FCRViewModel fCRViewModel = LoadFcrData(int.Parse(element.SkuDet_PK.ToString()), int.Parse(element.OurStyleId.ToString()), int.Parse(element.Location_pk.ToString()));


                    GiventoFactory = GiventoFactory + decimal.Parse(fCRViewModel.fcrMasterData.GiventoFactory);
                    GivenBackToStore = GivenBackToStore + decimal.Parse(fCRViewModel.fcrMasterData.GivenBackToStore);
                    ToBeonLocation = ToBeonLocation + decimal.Parse(fCRViewModel.fcrMasterData.ToBeonLocation);
                    TotalFabricLayed = TotalFabricLayed + decimal.Parse(fCRViewModel.TotalFabricLayed);
                    TotalShortage = TotalShortage + decimal.Parse(fCRViewModel.TotalShortage);
                    MarkMissedQty = MarkMissedQty + decimal.Parse(fCRViewModel.fcrMasterData.MarkMissedQty);
                    TotalBalanceQty = TotalBalanceQty + decimal.Parse(fCRViewModel.TotalBalanceQty.ToString());
                    MissingQty = MissingQty + decimal.Parse(fCRViewModel.fcrMasterData.MissingQty.ToString());


                    fcrMasterDatalist.Add(fCRViewModel);

                }
            }

            fullFCRModelData.FCRViewModelDatalist = fcrMasterDatalist;

            fullFCRModelData.GiventoFactory = GiventoFactory;
            fullFCRModelData.GivenBackToStore = GivenBackToStore;
            fullFCRModelData.ToBeonLocation = ToBeonLocation;
            fullFCRModelData.TotalFabricLayed = TotalFabricLayed;
            fullFCRModelData.TotalShortage = TotalShortage;
            fullFCRModelData.MarkMissedQty = MarkMissedQty;
            fullFCRModelData.TotalBalanceQty = TotalBalanceQty;
            fullFCRModelData.MissingQty = MissingQty;
            return fullFCRModelData;
        }






        public FCRViewModel LoadFcrData(int id, int ourStyleid, int locationpk)
        {

            FcrRepo fcrRepo = new FcrRepo();
            FCRViewModel fCRViewModel = new FCRViewModel();
            fCRViewModel.CutData = fcrRepo.GetAtcTemplateData(id);
            fCRViewModel.LayshortagereqData = fcrRepo.GetLayShortageData(id);
            fCRViewModel.RejectionReqData = fcrRepo.GetRejectionData(id);
            fCRViewModel.SamplingCutOrderData = fcrRepo.GetSampleAndExtraCutorder(id, ourStyleid, locationpk);
            fCRViewModel.DeliveryData = fcrRepo.GetDeliveryData(id);


            if (ourStyleid != 0)
            {
                try
                {
                    fCRViewModel.LayshortagereqData = fCRViewModel.LayshortagereqData.Select("OurStyleID = " + ourStyleid + "").CopyToDataTable();
                }
                catch (Exception)
                {
                    fCRViewModel.LayshortagereqData = null;


                }
                try
                {
                    fCRViewModel.RejectionReqData = fCRViewModel.RejectionReqData.Select("OurStyleID = " + ourStyleid + "").CopyToDataTable();
                }
                catch (Exception)
                {

                    fCRViewModel.RejectionReqData = null;
                }
                try
                {
                    fCRViewModel.CutData = fCRViewModel.CutData.Select("OurStyleID = " + ourStyleid + "").CopyToDataTable();
                }
                catch (Exception)
                {
                    fCRViewModel.CutData = null;

                }
                try
                {
                    fCRViewModel.DeliveryData = fCRViewModel.DeliveryData.Select("OurStyleID = " + ourStyleid + "").CopyToDataTable();
                }
                catch (Exception)
                {

                    fCRViewModel.DeliveryData = null;
                }
            }

            if (locationpk != 0)
            {

                try
                {
                    fCRViewModel.LayshortagereqData = fCRViewModel.LayshortagereqData.Select("ToLoc = " + locationpk + "").CopyToDataTable();
                }
                catch (Exception)
                {
                    fCRViewModel.LayshortagereqData = null;

                }
                try
                {
                    fCRViewModel.RejectionReqData = fCRViewModel.RejectionReqData.Select("ToLoc = " + locationpk + "").CopyToDataTable();
                }
                catch (Exception)
                {
                    fCRViewModel.RejectionReqData = null;

                }
                try
                {
                    fCRViewModel.CutData = fCRViewModel.CutData.Select("ToLoc = " + locationpk + "").CopyToDataTable();
                }
                catch (Exception)
                {
                    fCRViewModel.CutData = null;

                }
                try
                {
                    fCRViewModel.DeliveryData = fCRViewModel.DeliveryData.Select("ToLoc = " + locationpk + "").CopyToDataTable();
                }
                catch (Exception)
                {
                    fCRViewModel.DeliveryData = null;

                }

            }


            fCRViewModel.fcrMasterData = GetMasterData(id, ourStyleid, locationpk, fCRViewModel);


            fCRViewModel.TotalSampleYardage = "0";

            fCRViewModel.TotaCutorderQty = fCRViewModel.CutData.Compute("Sum(Qty)", "").ToString();
            fCRViewModel.TotalFabricLayed = fCRViewModel.CutData.Compute("Sum(layedFabric)", "").ToString();
            fCRViewModel.TotalLayedQty = fCRViewModel.CutData.Compute("Sum(CutQty)", "").ToString();
            fCRViewModel.TotalShortage = fCRViewModel.CutData.Compute("Sum(ExcessOrShort)", "").ToString();
            fCRViewModel.TotalNonusableEndbit = fCRViewModel.CutData.Compute("Sum(NonReusableEndbit)", "").ToString();
            fCRViewModel.TotalBalanceQty = (Decimal.Parse(fCRViewModel.fcrMasterData.ToBeonLocation.ToString()) - Decimal.Parse(fCRViewModel.TotalFabricLayed.ToString()) - Decimal.Parse(fCRViewModel.fcrMasterData.MarkMissedQty.ToString())).ToString();
            fCRViewModel.TotalSampleYardage = fCRViewModel.SamplingCutOrderData.Compute("Sum(FabQty)", "").ToString();

            if (fCRViewModel.TotalSampleYardage == "")
            {
                fCRViewModel.TotalSampleYardage = "0";
            }
            fCRViewModel.TotalBalanceQty = (Decimal.Parse(fCRViewModel.TotalBalanceQty) - Decimal.Parse(fCRViewModel.TotalShortage) - Decimal.Parse(fCRViewModel.TotalSampleYardage)).ToString();


            try
            {

                fCRViewModel.ActualFCRConsumtion = (Decimal.Parse(fCRViewModel.TotalFabricLayed.ToString()) / Decimal.Parse(fCRViewModel.TotalLayedQty.ToString())).ToString();

            }
            catch (Exception)
            {
                fCRViewModel.ActualFCRConsumtion = "0";


            }
            fCRViewModel.OverConsumed = (Decimal.Parse(fCRViewModel.ActualFCRConsumtion.ToString()) - Decimal.Parse(fCRViewModel.fcrMasterData.ApprovedConsumption.ToString())).ToString();


            fCRViewModel.IsClosed = IsClosed(id, ourStyleid, locationpk);













            return fCRViewModel;

        }

        public String IsClosed(int skudet_pk, int ourStyleid, int locationpk)
        {
            String status = "Closed";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from fabdet in enty.FabricInLocation_tbl
                        where fabdet.SkuDet_PK == skudet_pk && fabdet.OurStyleId == ourStyleid && fabdet.Location_pk == locationpk
                        select fabdet;

                foreach (var element in q)
                {
                    status = element.Status;
                }
            }
            return status;

        }

        public FcrMasterData GetMasterData(int skudetpk = 0, int ourstyleid = 0, int location_pk = 0, FCRViewModel fCRViewModel = null)
        {

            FcrMasterData fcrMasterData = new FcrMasterData();


            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {






                String Stylename = "";
                String stylenum = "";
                int atcid = 0;

                var q = from s in enty.SkuRawmaterialDetails
                        where s.SkuDet_PK == skudetpk
                        select s;


                fcrMasterData.LocPK = location_pk;
                fcrMasterData.OurStyleID = ourstyleid;
                foreach (var element in q)
                {
                    fcrMasterData.Atc = element.SkuRawMaterialMaster.AtcMaster.AtcNum;
                    fcrMasterData.Fabric = element.SkuRawMaterialMaster.RMNum + " / " + element.SkuRawMaterialMaster.Composition + " / " + element.SkuRawMaterialMaster.Composition + " / " + element.SkuRawMaterialMaster.Composition
                        + " / " + element.ColorCode + " / " + element.ItemColor;

                    fcrMasterData.Buyer = element.SkuRawMaterialMaster.AtcMaster.BuyerMaster.BuyerName;
                    fcrMasterData.Dateofproduction = element.SkuRawMaterialMaster.AtcMaster.ShipDate.ToString();
                    fcrMasterData.Color = element.ColorCode;

                    fcrMasterData.SkuDetPK = element.SkuDet_PK;
                    atcid = int.Parse(element.SkuRawMaterialMaster.Atc_id.ToString());
                }


                try
                {

                    var missingqty = enty.FabricMissings.Where(u => u.OurStyleID == ourstyleid &&
                     u.SkuDetPK == skudetpk && u.Location_Pk == location_pk && u.IsApproved == "Y").Select(u => u.MissingQty).Sum();

                    if (missingqty == null)
                    {
                        fcrMasterData.MarkMissedQty = "0";
                    }
                    else
                    {
                        fcrMasterData.MarkMissedQty = missingqty.ToString();
                    }

                }
                catch (Exception)
                {
                    fcrMasterData.MarkMissedQty = "0";

                }

                if (ourstyleid != 0)
                {
                    var q1 = from atcDetail in enty.AtcDetails
                             where atcDetail.OurStyleID == ourstyleid
                             select atcDetail;
                    foreach (var element in q1)
                    {

                        Stylename += element.OurStyle;
                        stylenum += element.BuyerStyle;
                    }
                }
                else
                {
                    var q1 = from atcDetail in enty.AtcDetails
                             where atcDetail.AtcId == atcid
                             select atcDetail;
                    foreach (var element in q1)
                    {

                        Stylename += element.OurStyle;
                        stylenum += element.BuyerStyle;
                    }
                }

                fcrMasterData.Style = Stylename;




                var q2 = (from popack in enty.POPackDetails
                          where popack.PoPackMaster.AtcId == atcid
                          select popack.PoPackMaster.SeasonName).Distinct();
                foreach (var popackdet in q2)
                {
                    fcrMasterData.Season += " / " + popackdet;
                }
                var q3 = (from popack in enty.PoPackMasters
                          join
    lctnmastermstr in enty.LocationMasters on popack.ExpectedLocation_PK equals lctnmastermstr.Location_PK
                          where popack.AtcId == atcid && popack.ExpectedLocation_PK == location_pk
                          select lctnmastermstr.LocationPrefix).Distinct();
                foreach (var popackdet in q3)
                {
                    fcrMasterData.Factory += " / " + popackdet;
                }

                if (fcrMasterData.Color == "")
                {
                    var orderqty = enty.POPackDetails.Where(u => u.OurStyleID == ourstyleid && u.PoPackMaster.ExpectedLocation_PK == location_pk).Select(u => u.PoQty).Sum();
                    fcrMasterData.Order = orderqty.ToString();
                }
                else
                {
                    var orderqty = enty.POPackDetails.Where(u => u.OurStyleID == ourstyleid && u.PoPackMaster.ExpectedLocation_PK == location_pk && u.ColorCode == fcrMasterData.Color).Select(u => u.PoQty).Sum();
                    fcrMasterData.Order = orderqty.ToString();
                }



                var skupk = enty.SkuRawmaterialDetails.Where(u => u.SkuDet_PK == skudetpk).Select(u => u.Sku_PK).FirstOrDefault();




                int sku_pk = int.Parse(skupk.ToString());
                var q4 = (from stylmstr in enty.StyleCostingMasters
                          join styldet in enty.StyleCostingDetails
                          on stylmstr.Costing_PK equals styldet.Costing_PK
                          where styldet.Sku_PK == sku_pk && stylmstr.IsApproved == "A" && stylmstr.AtcDetail.OurStyleID == ourstyleid
                          select styldet.Consumption).Max();


                fcrMasterData.Consumption = q4.ToString();

                decimal totalqty = 0;
                decimal totalweightedqty = 0;
                var q5 = (from cutorder in enty.CutOrderMasters
                          where cutorder.SkuDet_pk == skudetpk && cutorder.OurStyleID == ourstyleid
                          select new { cutorder.Color, cutorder.CutQty, cutorder.ActualConsumption }).ToList();

                foreach (var element in q5)
                {

                    try
                    {
                        fcrMasterData.Fabdescription = element.Color;
                        if (element.ActualConsumption != null)
                        {
                            totalqty += decimal.Parse(element.CutQty.ToString());

                            decimal tryqty = decimal.Parse(element.CutQty.ToString()) * decimal.Parse(element.ActualConsumption.ToString());
                            totalweightedqty += tryqty;
                        }
                    }
                    catch (Exception)
                    {


                    }

                }
                try
                {
                    fcrMasterData.ApprovedConsumption = (totalweightedqty / totalqty).ToString();
                }
                catch (Exception)
                {

                    fcrMasterData.ApprovedConsumption = "0";
                }


                decimal giventofacotorydemo = 0;
                decimal givenback = 0;
                decimal totalgiven = 0;


                foreach (DataRow row in fCRViewModel.DeliveryData.Rows)
                {
                    totalgiven += Decimal.Parse(row["RollYard"].ToString());
                    if (Decimal.Parse(row["RollYard"].ToString()) > 0)
                    {
                        giventofacotorydemo += Decimal.Parse(row["RollYard"].ToString());
                    }
                    else
                    {
                        givenback += Decimal.Parse(row["RollYard"].ToString());
                    }

                }

                fcrMasterData.GiventoFactory = giventofacotorydemo.ToString();
                fcrMasterData.GivenBackToStore = givenback.ToString();
                fcrMasterData.TotalGiven = (totalgiven + givenback).ToString();
                fcrMasterData.ToBeonLocation = totalgiven.ToString();











            }

            return fcrMasterData;
        }
    }
}