using ArtWebApp.Areas.CuttingMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.CuttingMVC.Controllers
{
    public class RejectionPanelController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: CuttingMVC/RejectionPanel
        public ActionResult RejectionPanelAccept()
        {
            RejectionPanelViewModal mdl = new RejectionPanelViewModal();

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
        [HttpPost]
        public ActionResult RejectionPanelAccept(RejectionPanelViewModal Model)
        {
            Model.AddedBy = HttpContext.Session["Username"].ToString();

            Model.AddedDate = DateTime.Now;
            var Number = (from n
                   in Model.FabreqDetails
                          where n.IsSelected == true
                          select n).ToList();
            Model.FabreqDetails = Number;
            Model.Type = "LayShortage";
            Rejectionfabricrepository lyipores = new Rejectionfabricrepository();
            String code = lyipores.InsertLaysheetShortageRoll(Model);
            TempData["shortMessage"] = "Extra Fabric Request Added Sucessfull Ref#" + code.ToString();

            return RedirectToAction("RejectionPanelAccept");
        }
        private void ConfigureViewModel(RejectionPanelViewModal model)
        {
            int atcid = 0;

            model.AtcList = new SelectList(db.AtcMasters.Where(o => o.IsClosed == "N"), "AtcId", "AtcNum");
            model.LocaionList = new SelectList(db.LocationMasters.Where(o => o.IsActive == "A" && o.LocType=="F"), "Location_Pk", "Locationname");
            if (model.AtcID.HasValue)
            {
                SelectList ourstyleitem = new SelectList(db.AtcDetails.Where(o => o.AtcId == model.AtcID), "OurStyleID", "OurStyle");

                model.OurStyleList = ourstyleitem;
                atcid = int.Parse(model.AtcID.ToString());
              
            }
            else
            {
                model.OurStyleOptions = new SelectList(Enumerable.Empty<SelectListItem>());
                
            }

        }

        [HttpGet]
        public JsonResult PopulateRequest(int ourstyleid, int locationid)
        {


            List<String> list = db.RejectionPanelExtraFabbReqs.
                Where(u => u.POPackDetail.OurStyleID == ourstyleid && u.Location_PK == locationid).Select(U => U.Fabreqno).Distinct().ToList();

            //SelectList Cutorderlist = new SelectList(db.RejectionPanelExtraFabbReqs.
            //    Where(u => u.POPackDetail.OurStyleID == ourstyleid && u.Location_PK == locationid).Select(U=>U.Fabreqno).Distinct().ToList(), "Fabreqno", "Fabreqno");


            var Cutorderlist = list.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
            //&& !(from layshortdet in db.RejectReqDetails where layshortdet.RejectionType == "P" && layshortdet.Location_PK == locationid select layshortdet.RejFabReqID).Contains(laydet.RejFabPanelReqID)

            JsonResult jsd = Json(Cutorderlist, JsonRequestBehavior.AllowGet);

            return jsd;

        }

        [HttpGet]
        public PartialViewResult GetRequestDetailView(int ourstyleid,int locationid,String ReqID)
        {
            RejectionPanelViewModal model = new RejectionPanelViewModal();
            Rejectionfabricrepository lyipores = new Rejectionfabricrepository();
            model.FabreqDetails = lyipores.GetRejectionpanelrequestdata(ourstyleid, locationid, ReqID);
            return PartialView("GetRequestDetailView", model);
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