using ArtWebApp.Areas.CuttingMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.CuttingMVC.Controllers
{
    public class RejectpanelAdjustController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: CuttingMVC/RejectpanelAdjust
        public ActionResult RejectpanelAdjust()
        {

            LayShortageCutorderAdjustmentViewModal mdl = new LayShortageCutorderAdjustmentViewModal();

            ConfigureViewModel(mdl);
            try
            {
                ViewBag.SuccessMessage = TempData["shortMessage"].ToString();
            }
            catch (Exception)
            {


            }
            return View(mdl);
        }


        [HttpGet]
        public JsonResult PopulateLayShortagerequest(int Id = 0)
        {


            SelectList LayShortageMasterreqlist = new SelectList(db.RejectReqMasters.Where(o => o.AtcID == Id), "RejReqMasterID", "Reqnum");


            JsonResult jsd = Json(LayShortageMasterreqlist, JsonRequestBehavior.AllowGet);

            return jsd;

        }


        [HttpGet]
        public JsonResult PopulateCutorderList(int Id = 0)
        {

            var atcid = db.RejectReqMasters.Where(u => u.RejReqMasterID == Id).Select(u => u.AtcID).FirstOrDefault();
            int skuid = int.Parse(atcid.ToString());


            SelectList Cutorderlist = new SelectList(db.CutOrderMasters.Where(o => o.AtcID == atcid), "CutID", "Cut_NO");


            JsonResult jsd = Json(Cutorderlist, JsonRequestBehavior.AllowGet);

            return jsd;

        }



        [HttpGet]
        public JsonResult PopulateLayRequestModel(int CutID = 0, int LayShortageMasterID = 0)
        {
            LayShortageCutorderAdjustmentViewModal model = new LayShortageCutorderAdjustmentViewModal();
            LaysheetRollRepository lyipores = new LaysheetRollRepository();
            model = lyipores.PopulateLayRequestModel(CutID, LayShortageMasterID);
            //String cutorderQty = "0";
            //String MarkerType = "0";
            //String Shrinkage = "0";
            //String CutWidth = "0";
            //String CutQty = "0";
            //String DeliveredQty = "0";
            //var requestqtyvar = db.LayShortageDetails.Where(u => u.LayShortageMasterID == LayShortageMasterID).Sum(u => u.ExcessOrShort);
            //String requestqty = requestqtyvar.ToString();


            //var q = from cutorder in db.CutOrderMasters
            //        where cutorder.CutID == CutID
            //        select new { cutorder.FabQty, cutorder.MarkerType, cutorder.Shrinkage, cutorder.CutWidth,cutorder.CutQty };
            //foreach(var element in q)
            //{
            //    cutorderQty = element.FabQty.ToString();
            //    MarkerType = element.MarkerType;
            //    Shrinkage = element.Shrinkage;
            //    CutWidth = element.CutWidth;
            //    CutQty = element.CutQty.ToString();
            //}

            //var cutdoQTY = db.CutOrderDOes.Where(U => U.CutID == CutID).Sum(U => U.DeliveryQty);

            //DeliveredQty = CutQty.ToString();



            //return Json(new { requestqty, cutorderQty , MarkerType , Shrinkage, CutWidth , CutQty , DeliveredQty }, JsonRequestBehavior.AllowGet);


            //   string output = JsonConvert.SerializeObject(model);


            //SelectList Cutorderlist = new SelectList(db.CutOrderMasters.Where(o => o.SkuDet_pk == skuid), "CutID", "Cut_NO");


            //JsonResult jsd = Json(Cutorderlist, JsonRequestBehavior.AllowGet);

            //return jsd;

            return Json(model, JsonRequestBehavior.AllowGet);

        }


        private void ConfigureViewModel(LayShortageCutorderAdjustmentViewModal model)
        {
            int atcid = 0;

            model.AtcList = new SelectList(db.AtcMasters.Where(o => o.IsClosed == "N"), "AtcId", "AtcNum");
            if (model.AtcID.HasValue)
            {
                SelectList LayShortageMasterreqlist = new SelectList(db.RejectReqMasters.Where(o => o.AtcID == model.AtcID), "RejReqMasterID", "Reqnum");

                model.LayShortageMasterreqlist = LayShortageMasterreqlist;


                SelectList Cutorderlist = new SelectList(db.CutOrderMasters.Where(o => o.AtcID == model.AtcID), "CutID", "Cut_NO");

                model.Cutorderlist = Cutorderlist;


            }
            else
            {
                model.LayShortageMasterreqlist = new SelectList(Enumerable.Empty<SelectListItem>());
                model.Cutorderlist = new SelectList(Enumerable.Empty<SelectListItem>());
            }

        }





        [HttpPost]
        public ActionResult RejectpanelAdjust(LayShortageCutorderAdjustmentViewModal mdl)
        {
            mdl.AddedBy = HttpContext.Session["Username"].ToString();

            mdl.AddedDate = DateTime.Now;

            LaysheetRollRepository lyipores = new LaysheetRollRepository();
            String code = lyipores.InsertRejectionCutOrderAdjust(mdl);
            TempData["shortMessage"] = "Updated";

            return RedirectToAction("RejectpanelAdjust");
        }











    }
}
