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
        public ActionResult Index(int id,int ourStyleid,int locationpk)
        {
            FcrRepo fcrRepo = new FcrRepo();
            FCRViewModel fCRViewModel = new FCRViewModel();
            fCRViewModel.fcrMasterData = GetMasterData(id, ourStyleid, locationpk);
            fCRViewModel.CutData = fcrRepo.GetAtcTemplateData(id);
            fCRViewModel.LayshortagereqData = fcrRepo.GetLayShortageData(id);
            fCRViewModel.RejectionReqData = fcrRepo.GetRejectionData(id);


            try
            {

                fCRViewModel.ActualFCRConsumtion = (Decimal.Parse(fCRViewModel.TotalFabricLayed.ToString()) / Decimal.Parse(fCRViewModel.TotalLayedQty.ToString())).ToString();

            }
            catch (Exception)
            {
                fCRViewModel.ActualFCRConsumtion = "0";


            }
            fCRViewModel.OverConsumed= (Decimal.Parse(fCRViewModel.ActualFCRConsumtion.ToString()) - Decimal.Parse(fCRViewModel.fcrMasterData.ApprovedConsumption.ToString())).ToString();
            if (ourStyleid != 0)
            {
                try
                {
                    fCRViewModel.LayshortagereqData = fCRViewModel.LayshortagereqData.Select("OurStyleID = " + ourStyleid + "").CopyToDataTable();
                }
                catch (Exception)
                {

                  
                }
                try
                {
                    fCRViewModel.RejectionReqData = fCRViewModel.RejectionReqData.Select("OurStyleID = " + ourStyleid + "").CopyToDataTable();
                }
                catch (Exception)
                {


                }
                try
                {
                    fCRViewModel.CutData = fCRViewModel.CutData.Select("OurStyleID = " + ourStyleid + "").CopyToDataTable();
                }
                catch (Exception)
                {


                }
                
            }

            if (locationpk != 0)
            {

                try
                {
                    fCRViewModel.LayshortagereqData = fCRViewModel.LayshortagereqData.Select("ToLoc = "+ locationpk + "").CopyToDataTable();
                }
                catch (Exception)
                {


                }
                try
                {
                    fCRViewModel.RejectionReqData = fCRViewModel.RejectionReqData.Select("ToLoc = " + locationpk + "").CopyToDataTable();
                }
                catch (Exception)
                {


                }
                try
                {
                    fCRViewModel.CutData = fCRViewModel.CutData.Select("ToLoc = " + locationpk + "").CopyToDataTable();
                }
                catch (Exception)
                {


                }

            }


            fCRViewModel.TotaCutorderQty = fCRViewModel.CutData.Compute("Sum(Qty)", "").ToString();

            fCRViewModel.TotalFabricLayed = fCRViewModel.CutData.Compute("Sum(layedFabric)", "").ToString();
            fCRViewModel.TotalLayedQty = fCRViewModel.CutData.Compute("Sum(CutQty)", "").ToString();
            fCRViewModel.TotalBalanceQty = (Decimal.Parse(fCRViewModel.fcrMasterData.ToBeonLocation.ToString()) - Decimal.Parse(fCRViewModel.TotalFabricLayed.ToString()) - Decimal.Parse(fCRViewModel.fcrMasterData.MarkMissedQty.ToString())).ToString();


            return View(fCRViewModel);
           

        }





        public ActionResult FCRIndex()
        {
            ViewBag.AtcID = new SelectList(enty.AtcMasters, "AtcId", "AtcNum");
            return View();


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










        public FcrMasterData GetMasterData(int skudetpk = 0 ,int ourstyleid=0,int location_pk=0 )
        {



            String Stylename = "";
            String stylenum = "";
            int atcid = 0;

            var q = from s in enty.SkuRawmaterialDetails
                    where s.SkuDet_PK == skudetpk 
                    select s;

            FcrMasterData fcrMasterData = new FcrMasterData();
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

            if (ourstyleid != 0) {
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
            var q3 = (from popack in enty.PoPackMasters  join
                      lctnmastermstr in enty.LocationMasters on popack.ExpectedLocation_PK equals lctnmastermstr.Location_PK
                      where popack.AtcId == atcid &&popack.ExpectedLocation_PK==location_pk
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
                var orderqty = enty.POPackDetails.Where(u => u.OurStyleID == ourstyleid && u.PoPackMaster.ExpectedLocation_PK == location_pk && u.ColorCode ==fcrMasterData.Color).Select(u => u.PoQty).Sum();
                fcrMasterData.Order = orderqty.ToString();
            }

            

            var skupk = enty.SkuRawmaterialDetails.Where(u => u.SkuDet_PK == skudetpk).Select(u => u.Sku_PK).FirstOrDefault();




            int sku_pk = int.Parse(skupk.ToString());
            var q4 = (from stylmstr in enty.StyleCostingMasters
                     join styldet in enty.StyleCostingDetails
                     on stylmstr.Costing_PK equals styldet.Costing_PK
                     where styldet.Sku_PK == sku_pk && stylmstr.IsApproved == "A" && stylmstr.AtcDetail.OurStyleID == ourstyleid
                     select  styldet.Consumption ).Max();
            

                fcrMasterData.Consumption = q4.ToString();

            decimal totalqty = 0;
            decimal totalweightedqty = 0;
            var q5 = (from cutorder in enty.CutOrderMasters
                     where cutorder.SkuDet_pk == skudetpk && cutorder.OurStyleID==ourstyleid
                     select new { cutorder.Color, cutorder.CutQty, cutorder.ActualConsumption }).ToList();

            foreach(var element in q5)
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

            var giventofacotory= (from cutorderdo in enty.CutOrderDOes
                                  where cutorderdo.CutOrderMaster.SkuDet_pk == skudetpk && cutorderdo.CutOrderMaster.ToLoc == location_pk
                                  select new { cutorderdo.DeliveryQty }).ToList();

             foreach (var element in giventofacotory)
            {
                totalgiven += Decimal.Parse(element.DeliveryQty.ToString());
                if (Decimal.Parse(element.DeliveryQty.ToString()) > 0)
                {
                    giventofacotorydemo += Decimal.Parse(element.DeliveryQty.ToString());
                }
                else
                {
                    givenback += Decimal.Parse(element.DeliveryQty.ToString());
                }
            }
            fcrMasterData.GiventoFactory = giventofacotorydemo.ToString();
            fcrMasterData.GivenBackToStore = givenback.ToString();
            fcrMasterData.TotalGiven = totalgiven.ToString();

            fcrMasterData.ToBeonLocation = (totalgiven - givenback).ToString();





            







            return fcrMasterData;
        }

        [HttpGet]
        public ActionResult MarkMissing(int skudetpk, int ourStyleid, int locationpk, Decimal Missingqty)
        {
            bool status = false;

            FabricMissing fabricMissing = new FabricMissing();
            fabricMissing.SkuDetPK = skudetpk;
            fabricMissing.OurStyleID = ourStyleid;

            fabricMissing.Location_Pk = locationpk;

            fabricMissing.MissingQty = Missingqty;
            fabricMissing.AddedBy = HttpContext.Session["Username"].ToString();

            fabricMissing.AddedDate = DateTime.Now;


            fabricMissing.IsLevel1Approved = "N";
            fabricMissing.IsApproved = "N";

            enty.FabricMissings.Add(fabricMissing);
            enty.SaveChanges();
            status = true;


            return RedirectToAction("index", new
            {
                id = skudetpk,
                ourStyleid = ourStyleid,
                locationpk = locationpk
            });

        }




        [HttpGet]
        public ActionResult CloseFCR(int skudetpk, int ourStyleid, int locationpk, Decimal Missingqty)
        {
            bool status = false;

            FabricInLocation_tbl fabricInLocation_Tbl = enty.FabricInLocation_tbl.Where(u=>u.SkuDet_PK== skudetpk && u.OurStyleId == ourStyleid && u.Location_pk == locationpk).FirstOrDefault();

            fabricInLocation_Tbl.IsClosed = "Y";
            fabricInLocation_Tbl.ClosedBy = HttpContext.Session["Username"].ToString();
            fabricInLocation_Tbl.ClosedDate = DateTime.Now;
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