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
            ViewBag.AtcID = new SelectList(db.AtcMasters.Where(o => o.IsClosed == "N"), "AtcId", "AtcNum");
            return View(mdl);
        }
        [HttpPost]
        public ActionResult LaysheetShortage(LaySheetShortageViewModel Model)
        {
            Model.AddedBy = HttpContext.Session["Username"].ToString();

            Model.AddedDate = DateTime.Now;
            var Number = (from n
                   in Model.rolldetailcollection
                          where n.IsSelected == true
                          select n).ToList();
            Model.rolldetailcollection = Number;
            Model.Type = "LayShortage";
            LaysheetRollRepository lyipores = new LaysheetRollRepository();
            String code = lyipores.InsertLaysheetShortageRoll(Model);

            return View();
        }

        [HttpGet]
        public JsonResult PopulateOurStyle(int Id = 0)
        {


            SelectList ourstyleitem = new SelectList(db.AtcDetails.Where(o => o.AtcId == Id), "OurStyleID", "OurStyle");


            JsonResult jsd = Json(ourstyleitem, JsonRequestBehavior.AllowGet);

            return jsd;

        }


        [HttpGet]
        public JsonResult PopulateLaysheetSelectionlist(decimal[] SelectedOurStyle)
        {


            SelectList ourstyleitem = new SelectList(db.LaySheetMasters.Where(o => SelectedOurStyle.Contains(o.OustyleID ?? 0)), "LaySheet_PK", "LaySheetNum");

            JsonResult jsd = Json(ourstyleitem, JsonRequestBehavior.AllowGet);

            return jsd;

        }

        [HttpGet]
        public PartialViewResult GetRollView(decimal[] SelectedOurStyle)
        {
            LaySheetShortageViewModel model = new LaySheetShortageViewModel();
            LaysheetRollRepository lyipores = new LaysheetRollRepository();
            model.rolldetailcollection = lyipores.getlaysheetRollData(SelectedOurStyle);
            return PartialView("LaySheetRollView", model);
        }
    }
}