using ArtWebApp.Areas.CuttingMVC.ViewModel;
using ArtWebApp.Areas.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.CuttingMVC.Controllers
{
    public class AtcPerformanceController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew();
       
        // GET: AtcPerformance
        public ActionResult Index(int id)
        {

            AtcPerformanceRepo repo = new AtcPerformanceRepo();

            AtcPerformanceMasterVM atcPerformanceMasterVM = new AtcPerformanceMasterVM();

            atcPerformanceMasterVM.atcmasterData = GetMasterData(id);
            atcPerformanceMasterVM.atcStyleShipDetails = GetAtcStyleShipData(id);

            atcPerformanceMasterVM.FabData = repo.GetAtcTemplateData(id, 1);
            atcPerformanceMasterVM.TrimData = repo.GetAtcTemplateData(id, 2);

            return View(atcPerformanceMasterVM);
        }



        public AtcmasterData GetMasterData(int atcid = 0)
        {



            String Stylename = "";
            String stylenum = "";


            var q = from s in enty.AtcMasters
                    where s.AtcId == atcid
                    select s;

            AtcmasterData atcmasterData = new AtcmasterData();
            foreach (var element in q)
            {
                atcmasterData.AtcNum = element.AtcNum;
                atcmasterData.PCD = DateTime.Parse(element.HouseDate.ToString());
                atcmasterData.Buyer = element.BuyerMaster.BuyerName;
                atcmasterData.Country = element.CountryMaster1.ShortName;

            }

            var q1 = from atcDetail in enty.AtcDetails
                     where atcDetail.AtcId == atcid
                     select atcDetail;
            foreach (var element in q1)
            {

                Stylename += element.OurStyle;
                stylenum += element.BuyerStyle;
            }
            atcmasterData.OurStyle = Stylename;
            atcmasterData.Stylename = stylenum;


            var orderqty = enty.POPackDetails.Where(u => u.PoPackMaster.AtcId == atcid).Select(u => u.PoQty).Sum();

            atcmasterData.OrderQty = orderqty.ToString();
            return atcmasterData;
        }


        public List<AtcStyleShipDetail> GetAtcStyleShipData(int atcid = 0)
        {
            Decimal Shippedvalue = 0;
            List<AtcStyleShipDetail> atcStyleShipDetails = new List<AtcStyleShipDetail>();
            var q = from atcDetail in enty.AtcDetails
                    where atcDetail.AtcId == atcid
                    select atcDetail;

            foreach (var element in q)
            {
                AtcStyleShipDetail atcStyleShipDetail = new AtcStyleShipDetail();
                atcStyleShipDetail.OurStyle = element.OurStyle;
                atcStyleShipDetail.Fob = element.FOB.ToString();

                var orderqty = enty.POPackDetails.Where(u => u.OurStyleID == element.OurStyleID).Select(u => u.PoQty).Sum();
                var ShippedQty = enty.InvoiceDetails.Where(u => u.OurStyleID == element.OurStyleID).Select(u => u.InvoiceQty).Sum();




                atcStyleShipDetail.OrderQty = orderqty.ToString();

                atcStyleShipDetail.OrderValue = (Decimal.Parse(orderqty.ToString()) * Decimal.Parse(atcStyleShipDetail.Fob)).ToString();


                atcStyleShipDetail.ShippedQty = ShippedQty.ToString();



                var inv = from invdet in enty.InvoiceDetails
                          where invdet.OurStyleID == element.OurStyleID
                          select invdet;
                foreach (var element123 in inv)
                {
                    Shippedvalue = Shippedvalue + Decimal.Parse(element123.InvoiceQty.ToString()) * Decimal.Parse(element123.FOB.ToString());
                }



                var ourstylecolor = (from invdet in enty.InvoiceDetails
                                     join shipmentdet in enty.ShipmentHandOverDetails on invdet.ShipmentHandOver_PK equals shipmentdet.ShipmentHandOver_PK
                                     join popackdet in enty.POPackDetails on shipmentdet.CombinationCode equals popackdet.CombinationCode

                                     where invdet.OurStyleID == element.OurStyleID

                                     select new { popackdet.ColorName }).Distinct();

                foreach (var element1234556677 in ourstylecolor)
                {
                    atcStyleShipDetail.Color += " / " + element1234556677.ColorName;
                }



                atcStyleShipDetail.ShippedValue = Shippedvalue.ToString();

                atcStyleShipDetails.Add(atcStyleShipDetail);

            }

            return atcStyleShipDetails;
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