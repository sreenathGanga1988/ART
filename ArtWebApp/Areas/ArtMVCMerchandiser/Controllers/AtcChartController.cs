using ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVCMerchandiser.Controllers
{
    public class AtcChartController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: ArtMVCMerchandiser/AtcChart
        public ActionResult Index(AtcChartMasterViewModal mdl=null)
        {
           

            ConfigureViewModel(mdl);
            //   ViewBag.AtcID = new SelectList(db.AtcMasters.Where(o => o.IsClosed == "N"), "AtcId", "AtcNum");
            try
            {
                ViewBag.SuccessMessage = TempData["shortMessage"].ToString();
            }
            catch (Exception)
            {


            }
            return View(mdl);
        }
        [HttpPost]
        public ActionResult Export(AtcChartMasterViewModal model)
        {
            var cd = new ContentDisposition
            {
                FileName = "YourFileName.csv",
                Inline = false
            };
            Response.AddHeader("Content-Disposition", cd.ToString());
            return Content(model.Csv, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        private void ConfigureViewModel(AtcChartMasterViewModal model)
        {
            int atcid = 0;

            model.AtcList = new SelectList(db.AtcMasters.Where(o => o.IsClosed == "N"), "AtcId", "AtcNum");
            if (model.AtcID.HasValue)
            {

                model.atcChartmaster = AtcChartRepo.fillAtcChartViewModel(int.Parse(model.AtcID.ToString()), "atc", 0);
                SelectList ourstyleitem = new SelectList(db.AtcDetails.Where(o => o.AtcId == model.AtcID), "OurStyleID", "OurStyle");

                model.OurStyleList = ourstyleitem;
                atcid = int.Parse(model.AtcID.ToString());
                model.SkuList = ComboRepository.fillFabColorofAtc(atcid);
            }
            else
            {
                model.OurStyleList = new SelectList(Enumerable.Empty<SelectListItem>());
                model.SkuList = new SelectList(Enumerable.Empty<SelectListItem>());
                model.SeasonList = new SelectList(Enumerable.Empty<SelectListItem>());
                model.ASQList = new SelectList(Enumerable.Empty<SelectListItem>());
            }

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}