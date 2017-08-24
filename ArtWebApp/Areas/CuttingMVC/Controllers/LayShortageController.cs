using ArtWebApp.Areas.CuttingMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.CuttingMVC.Controllers
{
    public class LayShortageController : Controller
    {
        // GET: CuttingMVC/LayShortage
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: ArtMVC/ApproveLaysheet


        [HttpGet]
        public ActionResult LaysheetShortage()
        {
            LaySheetShortageViewModel mdl = new LaySheetShortageViewModel();

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
        public ActionResult LaysheetShortage(LaySheetShortageViewModel Model)
        {
            Model.AddedBy = HttpContext.Session["Username"].ToString();

            Model.AddedDate = DateTime.Now;
            var Number = (from n
                   in Model.RollDetails
                          where n.IsSelected == true
                          select n).ToList();
            Model.RollDetails = Number;
            Model.Type = "LayShortage";
            LaysheetRollRepository lyipores = new LaysheetRollRepository();
            String code = lyipores.InsertLaysheetShortageRoll(Model);
            TempData["shortMessage"] = "Extra Fabric Request Added Sucessfull Ref#" + code.ToString();

            return RedirectToAction("LaysheetShortage");
        }

        [HttpGet]
        public JsonResult PopulateOurStyle(int Id = 0)
        {


            SelectList ourstyleitem = new SelectList(db.AtcDetails.Where(o => o.AtcId == Id), "OurStyleID", "OurStyle");


            JsonResult jsd = Json(ourstyleitem, JsonRequestBehavior.AllowGet);

            return jsd;

        }
        [HttpGet]
        public JsonResult PopulateOFabric(int Id = 0)
        {


            SelectList SkuList = ComboRepository.fillFabColorofAtc(Id); ;


            JsonResult jsd = Json(SkuList, JsonRequestBehavior.AllowGet);

            return jsd;

        }

        [HttpGet]
        public JsonResult PopulateLaysheetSelectionlist(decimal[] SelectedOurStyle, int Id=0)
        {


            SelectList ourstyleitem = new SelectList(db.LaySheetMasters.Where(o => SelectedOurStyle.Contains(o.OustyleID ?? 0) && o.CutOrderDetail.CutOrderMaster.SkuDet_pk==Id), "LaySheet_PK", "LaySheetNum");

            JsonResult jsd = Json(ourstyleitem, JsonRequestBehavior.AllowGet);

            return jsd;

        }

    

        [HttpGet]
        public PartialViewResult GetRollView(decimal[] SelectedOurStyle)
        {
            LaySheetShortageViewModel model = new LaySheetShortageViewModel();
            LaysheetRollRepository lyipores = new LaysheetRollRepository();
            model.RollDetails = lyipores.getlaysheetRollData(SelectedOurStyle);
            return PartialView("LaySheetRollView", model);
        }



        private void ConfigureViewModel(LaySheetShortageViewModel model)
        {
            int atcid = 0;
          
            model.AtcList = new SelectList(db.AtcMasters.Where(o => o.IsClosed == "N"), "AtcId", "AtcNum");
            if (model.AtcID.HasValue)
            {
                SelectList ourstyleitem = new SelectList(db.AtcDetails.Where(o => o.AtcId == model.AtcID), "OurStyleID", "OurStyle");

                model.OurStyleList = ourstyleitem;
                atcid = int.Parse(model.AtcID.ToString ());
                model.SkuList = ComboRepository.fillFabColorofAtc(atcid);
            }
            else
            {
                model.OurStyleOptions = new SelectList(Enumerable.Empty<SelectListItem>());
                model.SkuList = new SelectList(Enumerable.Empty<SelectListItem>());
            }
          
        }
    



    }
}