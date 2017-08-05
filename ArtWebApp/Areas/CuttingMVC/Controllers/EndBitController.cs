using ArtWebApp.Areas.CuttingMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.CuttingMVC.Controllers
{
    public class EndBitController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: CuttingMVC/EndBit
        public ActionResult EndBit()
        {
            LaySheetShortageViewModel mdl = new LaySheetShortageViewModel();
            ViewBag.AtcID = new SelectList(db.AtcMasters.Where(o => o.IsClosed == "N"), "AtcId", "AtcNum");
            return View(mdl);
        }

        [HttpPost]
        public ActionResult EndBit(LaySheetShortageViewModel Model)
        {
            Model.AddedBy = HttpContext.Session["Username"].ToString();

            Model.AddedDate = DateTime.Now;
            var Number = (from n
                   in Model.RollDetails
                          where n.IsSelected == true
                          select n).ToList();
            Model.RollDetails = Number;
            Model.Type = "EndBit";
            LaysheetRollRepository lyipores = new LaysheetRollRepository();
            String code = lyipores.InsertLaysheetShortageRoll(Model);

            return View();
        }

    }
}