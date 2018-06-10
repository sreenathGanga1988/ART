using ArtWebApp.Areas.CuttingMVC.ViewModel;
using ArtWebApp.Areas.Repository;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.CuttingMVC.Controllers
{

    public class FCRController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew();

        // GET: CuttingMVC/FCR
        public ActionResult Index(int id, int ourStyleid, int locationpk)
        {
            List<decimal?> list = Session["ApprovedLocationlist"] as List<decimal?>;
            FCRViewModel fCRViewModel = new FCRViewModel();
            FcrLoaderrepo fcrLoaderrepo = new FcrLoaderrepo();
            fCRViewModel = fcrLoaderrepo.LoadFcrData(id, ourStyleid, locationpk);
            if (list.Contains(locationpk)) { fCRViewModel.Isclosebuttonvisible = "Y"; } else { fCRViewModel.Isclosebuttonvisible = "N"; }
            return View(fCRViewModel);


        }



        public ActionResult FullSkuIndex(int id, int locationpk)
        {
            List<decimal?> list = Session["ApprovedLocationlist"] as List<decimal?>;
            FcrLoaderrepo fcrLoaderrepo = new FcrLoaderrepo();
            FullFCRModelData fullFCRModelData = fcrLoaderrepo.LoadFcrofSkuLocation(id, locationpk);
            fullFCRModelData.SkuDetPK = id;
            fullFCRModelData.LocationPk = locationpk;
            if (list.Contains(locationpk)) { fullFCRModelData.Isclosebuttonvisible = "Y"; } else { fullFCRModelData.Isclosebuttonvisible = "N"; }

            return View(fullFCRModelData);


        }






        public String IsClosed(int skudet_pk, int ourStyleid, int locationpk)
        {
            String status = "Closed";

            var q = from fabdet in enty.FabricInLocation_tbl
                    where fabdet.SkuDet_PK == skudet_pk && fabdet.OurStyleId == ourStyleid && fabdet.Location_pk == locationpk
                    select fabdet;

            foreach (var element in q)
            {
                status = element.Status;
            }

            return status;

        }




        public ActionResult FCRIndex()
        {
            ViewBag.AtcID = new SelectList(enty.AtcMasters, "AtcId", "AtcNum");
            ViewBag.Locid = new SelectList(enty.LocationMasters, "location_pk", "locationname");
            return View();
        }

        public ActionResult ReportIndex()
        {
            ViewBag.AtcID = new SelectList(enty.AtcMasters, "AtcId", "AtcNum");
            ViewBag.Locid = new SelectList(enty.LocationMasters, "location_pk", "locationname");
            return View();

        }

        [HttpGet]
        public PartialViewResult Getdatewisefabricconsume(DateTime fromdate, DateTime todate, int locid, int atcid)
        {
            DatewiseFabricConsume model = new DatewiseFabricConsume();

            FcrRepo fcrRepo = new FcrRepo();
            DataTable dt = fcrRepo.Getdatewisefabricconsume(fromdate,todate,locid,atcid);
            model.Getdatewisefabricconsume = dt;
            
         //   model.ReportName = "Datewise Details";
            return PartialView("DatewiseFabricConsume_P", model);
        }

        [HttpGet]
        public PartialViewResult GetATCwisefabricconsume( int locid, int atcid)
        {
            ATCwiseFabricConsume model = new ATCwiseFabricConsume();
            FcrRepo fcrRepo = new FcrRepo();
            DataTable dt = fcrRepo.GetATCwisefabricconsume(locid, atcid);
            model.GetATCwisefabricconsume= dt;

            //   model.ReportName = "Datewise Details";
            return PartialView("ATCwiseFabricConsume_P", model);
        }


        public ActionResult FCRLoader(int id)
        {

            FabricInLocation_tbl fabricInLocation_Tbl = enty.FabricInLocation_tbl.Find(id);


            int SkuDet_PK = int.Parse(fabricInLocation_Tbl.SkuDet_PK.ToString());
            int ourStyleid = int.Parse(fabricInLocation_Tbl.OurStyleId.ToString());
            int locationpk = int.Parse(fabricInLocation_Tbl.Location_pk.ToString());

            return RedirectToAction("Index", new
            {
                id = SkuDet_PK,
                ourStyleid = ourStyleid,
                locationpk = locationpk,
            });


        }


        public ActionResult FCRMasterLoader(int id)
        {

            FabricInLocationAtcMaster_tbl fabricInLocation_Tbl = enty.FabricInLocationAtcMaster_tbl.Find(id);


            int SkuDet_PK = int.Parse(fabricInLocation_Tbl.SkuDet_PK.ToString());

            int locationpk = int.Parse(fabricInLocation_Tbl.Location_pk.ToString());

            return RedirectToAction("FullSkuIndex", new
            {
                id = SkuDet_PK,

                locationpk = locationpk,
            });


        }




        public ActionResult FullFcrIndex(int id, int locationpk)
        {


            return View();
        }







        [HttpGet]
        public PartialViewResult GetFCRStatus(int Id)
        {
            FCRStatusReportDataModel model = new FCRStatusReportDataModel();

            FcrRepo fcrRepo = new FcrRepo();
            DataTable dt = fcrRepo.GetFabriclocationGroup(Id);
            model.FabricdataData = dt;

            model.ReportName = "FCR Status";
            return PartialView("FCRStatus", model);
        }



        [HttpGet]
        public PartialViewResult GetFCRAtcStatus(int Id)
        {
            FCRStatusReportDataModel model = new FCRStatusReportDataModel();

            FcrRepo fcrRepo = new FcrRepo();
            DataTable dt = fcrRepo.GetFabricoflocationByAtc(Id);
            model.FabricdataData = dt;

            model.ReportName = "FCR Status";
            return PartialView("FcrMasterStatus", model);
        }






        //        public FcrMasterData GetMasterData(int skudetpk = 0, int ourstyleid = 0, int location_pk = 0, FCRViewModel fCRViewModel = null)
        //        {











        //            String Stylename = "";
        //            String stylenum = "";
        //            int atcid = 0;

        //            var q = from s in enty.SkuRawmaterialDetails
        //                    where s.SkuDet_PK == skudetpk
        //                    select s;

        //            FcrMasterData fcrMasterData = new FcrMasterData();
        //            fcrMasterData.LocPK = location_pk;
        //            fcrMasterData.OurStyleID = ourstyleid;
        //            foreach (var element in q)
        //            {
        //                fcrMasterData.Atc = element.SkuRawMaterialMaster.AtcMaster.AtcNum;
        //                fcrMasterData.Fabric = element.SkuRawMaterialMaster.RMNum + " / " + element.SkuRawMaterialMaster.Composition + " / " + element.SkuRawMaterialMaster.Composition + " / " + element.SkuRawMaterialMaster.Composition
        //                    + " / " + element.ColorCode + " / " + element.ItemColor;

        //                fcrMasterData.Buyer = element.SkuRawMaterialMaster.AtcMaster.BuyerMaster.BuyerName;
        //                fcrMasterData.Dateofproduction = element.SkuRawMaterialMaster.AtcMaster.ShipDate.ToString();
        //                fcrMasterData.Color = element.ColorCode;

        //                fcrMasterData.SkuDetPK = element.SkuDet_PK;
        //                atcid = int.Parse(element.SkuRawMaterialMaster.Atc_id.ToString());
        //            }


        //            try
        //            {

        //                var missingqty = enty.FabricMissings.Where(u => u.OurStyleID == ourstyleid &&
        //                 u.SkuDetPK == skudetpk && u.Location_Pk == location_pk && u.IsApproved == "Y").Select(u => u.MissingQty).Sum();

        //                if (missingqty == null)
        //                {
        //                    fcrMasterData.MarkMissedQty = "0";
        //                }
        //                else
        //                {
        //                    fcrMasterData.MarkMissedQty = missingqty.ToString();
        //                }

        //            }
        //            catch (Exception)
        //            {
        //                fcrMasterData.MarkMissedQty = "0";

        //            }

        //            if (ourstyleid != 0)
        //            {
        //                var q1 = from atcDetail in enty.AtcDetails
        //                         where atcDetail.OurStyleID == ourstyleid
        //                         select atcDetail;
        //                foreach (var element in q1)
        //                {

        //                    Stylename += element.OurStyle;
        //                    stylenum += element.BuyerStyle;
        //                }
        //            }
        //            else
        //            {
        //                var q1 = from atcDetail in enty.AtcDetails
        //                         where atcDetail.AtcId == atcid
        //                         select atcDetail;
        //                foreach (var element in q1)
        //                {

        //                    Stylename += element.OurStyle;
        //                    stylenum += element.BuyerStyle;
        //                }
        //            }

        //            fcrMasterData.Style = Stylename;




        //            var q2 = (from popack in enty.POPackDetails
        //                      where popack.PoPackMaster.AtcId == atcid
        //                      select popack.PoPackMaster.SeasonName).Distinct();
        //            foreach (var popackdet in q2)
        //            {
        //                fcrMasterData.Season += " / " + popackdet;
        //            }
        //            var q3 = (from popack in enty.PoPackMasters
        //                      join
        //lctnmastermstr in enty.LocationMasters on popack.ExpectedLocation_PK equals lctnmastermstr.Location_PK
        //                      where popack.AtcId == atcid && popack.ExpectedLocation_PK == location_pk
        //                      select lctnmastermstr.LocationPrefix).Distinct();
        //            foreach (var popackdet in q3)
        //            {
        //                fcrMasterData.Factory += " / " + popackdet;
        //            }

        //            if (fcrMasterData.Color == "")
        //            {
        //                var orderqty = enty.POPackDetails.Where(u => u.OurStyleID == ourstyleid && u.PoPackMaster.ExpectedLocation_PK == location_pk).Select(u => u.PoQty).Sum();
        //                fcrMasterData.Order = orderqty.ToString();
        //            }
        //            else
        //            {
        //                var orderqty = enty.POPackDetails.Where(u => u.OurStyleID == ourstyleid && u.PoPackMaster.ExpectedLocation_PK == location_pk && u.ColorCode == fcrMasterData.Color).Select(u => u.PoQty).Sum();
        //                fcrMasterData.Order = orderqty.ToString();
        //            }



        //            var skupk = enty.SkuRawmaterialDetails.Where(u => u.SkuDet_PK == skudetpk).Select(u => u.Sku_PK).FirstOrDefault();




        //            int sku_pk = int.Parse(skupk.ToString());
        //            var q4 = (from stylmstr in enty.StyleCostingMasters
        //                      join styldet in enty.StyleCostingDetails
        //                      on stylmstr.Costing_PK equals styldet.Costing_PK
        //                      where styldet.Sku_PK == sku_pk && stylmstr.IsApproved == "A" && stylmstr.AtcDetail.OurStyleID == ourstyleid
        //                      select styldet.Consumption).Max();


        //            fcrMasterData.Consumption = q4.ToString();

        //            decimal totalqty = 0;
        //            decimal totalweightedqty = 0;
        //            var q5 = (from cutorder in enty.CutOrderMasters
        //                      where cutorder.SkuDet_pk == skudetpk && cutorder.OurStyleID == ourstyleid
        //                      select new { cutorder.Color, cutorder.CutQty, cutorder.ActualConsumption }).ToList();

        //            foreach (var element in q5)
        //            {

        //                try
        //                {
        //                    fcrMasterData.Fabdescription = element.Color;
        //                    if (element.ActualConsumption != null)
        //                    {
        //                        totalqty += decimal.Parse(element.CutQty.ToString());

        //                        decimal tryqty = decimal.Parse(element.CutQty.ToString()) * decimal.Parse(element.ActualConsumption.ToString());
        //                        totalweightedqty += tryqty;
        //                    }
        //                }
        //                catch (Exception)
        //                {


        //                }

        //            }
        //            try
        //            {
        //                fcrMasterData.ApprovedConsumption = (totalweightedqty / totalqty).ToString();
        //            }
        //            catch (Exception)
        //            {

        //                fcrMasterData.ApprovedConsumption = "0";
        //            }


        //            decimal giventofacotorydemo = 0;
        //            decimal givenback = 0;
        //            decimal totalgiven = 0;


        //            foreach (DataRow row in fCRViewModel.DeliveryData.Rows)
        //            {
        //                totalgiven += Decimal.Parse(row["RollYard"].ToString());
        //                if (Decimal.Parse(row["RollYard"].ToString()) > 0)
        //                {
        //                    giventofacotorydemo += Decimal.Parse(row["RollYard"].ToString());
        //                }
        //                else
        //                {
        //                    givenback += Decimal.Parse(row["RollYard"].ToString());
        //                }

        //            }

        //            fcrMasterData.GiventoFactory = giventofacotorydemo.ToString();
        //            fcrMasterData.GivenBackToStore = givenback.ToString();
        //            fcrMasterData.TotalGiven = (totalgiven + givenback).ToString();
        //            fcrMasterData.ToBeonLocation = totalgiven.ToString();













        //            return fcrMasterData;
        //        }




        public ActionResult Print(int Id, int Locid)
        {
            var report = new Rotativa.MVC.ActionAsPdf("GetFCRSummary", new { id = Id, Locid = Locid });
            return report;

        }

        [HttpGet]
        public PartialViewResult GetFCRSummary(int Id ,int Locid)
        {
            FCRStatusReportDataModel model = new FCRStatusReportDataModel();

            FcrRepo fcrRepo = new FcrRepo();
            FCRSummary fCRSummary = fcrRepo.GetFCRSummary(Id, Locid);
           

           
            return PartialView("FCRSummaryView_P", fCRSummary);
        }







        [HttpGet]
        public ActionResult MarkMissing(int skudetpk, int locationpk, Decimal Missingqty)
        {
            bool status = false;

            FabricMissingMaster fabricMissing = new FabricMissingMaster();
            fabricMissing.SkuDetPK = skudetpk;
            //  fabricMissing.OurStyleID = ourStyleid;

            fabricMissing.Location_Pk = locationpk;

            fabricMissing.MissingQty = Missingqty;
            fabricMissing.AddedBy = HttpContext.Session["Username"].ToString();

            fabricMissing.AddedDate = DateTime.Now;


            fabricMissing.IsLevel1Approved = "N";
            fabricMissing.IsApproved = "N";

            enty.FabricMissingMasters.Add(fabricMissing);
            enty.SaveChanges();
            status = true;


            return RedirectToAction("index", new
            {
                id = skudetpk,

                locationpk = locationpk
            });

        }




        [HttpGet]
        public ActionResult CloseFCR(int skudetpk, int ourStyleid, int locationpk, Decimal Missingqty, Decimal OrderQty, Decimal CutQty, Decimal TotalFabricConsumed, Decimal BomConsumption, Decimal MarkerConsumption, Decimal ActualFCRConsumtion, Decimal OverConsumed, Decimal OverConsumedPer)
        {
            bool status = false;

            FabricInLocation_tbl fabricInLocation_Tbl = enty.FabricInLocation_tbl.Where(u => u.SkuDet_PK == skudetpk && u.OurStyleId == ourStyleid && u.Location_pk == locationpk).FirstOrDefault();
            fabricInLocation_Tbl.Status = "Closed";
            fabricInLocation_Tbl.IsClosed = "Y";
            fabricInLocation_Tbl.ClosedBy = HttpContext.Session["Username"].ToString();
            fabricInLocation_Tbl.ClosedDate = DateTime.Now;
            fabricInLocation_Tbl.OrderQty = OrderQty;
            fabricInLocation_Tbl.CutQty = CutQty;
            fabricInLocation_Tbl.TotalFabConsumed = TotalFabricConsumed;
            fabricInLocation_Tbl.BomConsumption = BomConsumption;
            fabricInLocation_Tbl.MarkerConsumption = MarkerConsumption;
            fabricInLocation_Tbl.ActualConsumption = ActualFCRConsumtion;
            fabricInLocation_Tbl.OverConsumed = OverConsumed;
            fabricInLocation_Tbl.OverConsumedPer = OverConsumedPer;
            enty.SaveChanges();
            return RedirectToAction("FCRIndex");

        }



        [HttpGet]
        public ActionResult CloseFullFCR(int skudetpk, int locationpk)
        {
            bool status = false;

            FabricInLocationAtcMaster_tbl fabricInLocation_Tbl = enty.FabricInLocationAtcMaster_tbl.Where(u => u.SkuDet_PK == skudetpk && u.Location_pk == locationpk).FirstOrDefault();
            fabricInLocation_Tbl.Status = "Closed";
            fabricInLocation_Tbl.IsClosed = "Y";
            fabricInLocation_Tbl.ClosedBy = HttpContext.Session["Username"].ToString();
            fabricInLocation_Tbl.ClosedDate = DateTime.Now;
            enty.SaveChanges();
            return RedirectToAction("FCRIndex");

        }






        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                enty.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}