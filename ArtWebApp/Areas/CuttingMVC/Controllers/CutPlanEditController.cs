﻿using ArtWebApp.Areas.CuttingMVC.Models;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.CuttingMVC.Controllers
{
    public class CutPlanEditController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: CuttingMVC/CutPlanEdit
        [HttpGet]
        public ActionResult Index()
        {
           
            ViewBag.AtcID = new SelectList(db.AtcMasters, "AtcId", "AtcNum");

            return View();
        }
        // GET: CuttingMVC/Edit/5
        [HttpGet]
        public ActionResult Edit(decimal? id)
        {
            CutplanViewModel cutplanViewModel = new CutplanViewModel(int.Parse ( id.ToString()));

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                cutplanViewModel = ConfigureModel(int.Parse(id.ToString()), cutplanViewModel);
            }
             if (cutplanViewModel.cutPlanMaster == null)
            {
                return HttpNotFound();
            }
         
                 return View(cutplanViewModel);
        }



        public CutplanViewModel ConfigureModel(int id, CutplanViewModel cutplanViewModel)
        {
            CutPlanMaster cutPlanMaster = db.CutPlanMasters.Find(id);

            cutplanViewModel.cutPlanMaster = cutPlanMaster;
            if (cutplanViewModel.cutPlanMaster != null)
            {
                var CutplanMarkerType = db.CutPlanMarkerTypes.Where(u => u.CutPlan_PK == id).Select(u => u.CutPlanmarkerTypeName).ToArray();
                cutplanViewModel.CutPlanMarkerTypes = CutplanMarkerType;

            }

          




            ViewBag.OurStyleID = new SelectList(db.AtcDetails, "OurStyleID", "OurStyle", cutPlanMaster.OurStyleID);
            ViewBag.Location_PK = new SelectList(db.LocationMasters, "Location_PK", "LocationName", cutPlanMaster.Location_PK);
            ViewBag.SkuDet_PK = new SelectList(db.SkuRawmaterialDetails, "SkuDet_PK", "ColorCode", cutPlanMaster.SkuDet_PK);

            ViewBag.MarkerMade = new SelectList(db.MarkerMadeMasters, "MarkerMade", "MarkerMade", cutPlanMaster.MarkerMade);
            ViewBag.CutType = new SelectList(db.CutTypeMasters, "CutType", "CutType", cutPlanMaster.CutType);
            ViewBag.Fabrication = new SelectList(db.BodyPartMasters, "BodyPartName", "BodyPartName", cutPlanMaster.Fabrication);
           
            var categories = db.MarkerDirectionMasters.Select(c => new
            {
                MarkerDirectionID = c.MarkerDirection,
                MarkerDirectionName = c.MarkerDirection
            }).ToList();

            ViewBag.categories = new MultiSelectList(categories, "MarkerDirectionID", "MarkerDirectionName");



            return cutplanViewModel;
        }




        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Save")]
        public ActionResult UpdateMaster(CutplanViewModel cutplanViewModel=null)
        {
            TempData["Error"] = null;

            try
            {
                if (cutplanViewModel.cutplanPk != null || cutplanViewModel.cutplanPk != 0)
                {
                    CuttingRepository cuttingRepository = new CuttingRepository();

                    cuttingRepository.EditCutplanMaster(cutplanViewModel);
                    TempData["MasterSucess"] = "Master Data Upload successful";
                }
            }
            catch (Exception)
            {

                TempData["Error"] = "Error Occured";
            }



            return RedirectToAction("Edit", new { id = cutplanViewModel.cutPlanMaster.CutPlan_PK });

          
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "EditCut")]
        public ActionResult EditCut(FormCollection formCollection)
        {


            int cutplanpk = int.Parse(Request.Form["CutPlanPK"].ToString());

            return RedirectToAction("Edit", new { id = cutplanpk });


        }


        [HttpPost]
        public JsonResult Create(List<AsqCollection> things)

        {
            bool status = false;
            int CutPlan_PK = 0;
            foreach (AsqCollection asqCollection in things)
            {
                CutPlan_PK = int.Parse(asqCollection.CutPlan_PK.ToString());
                int OurStyleId = int.Parse(asqCollection.OurStyleID.ToString());
                int PoPackId = int.Parse(asqCollection.PoPackId.ToString());
                Decimal Qty = Decimal.Parse(asqCollection.Qty.ToString());
                if (!db.CutPlanASQDetails.Any(f => f.CutPlan_PK == CutPlan_PK && f.OurStyleId == OurStyleId && f.PoPackId == PoPackId && f.SizeName == asqCollection.SizeName
                        && f.ColorName == asqCollection.ColorName))
                {

                    var popackdets_pk = db.POPackDetails.Where(u => u.SizeName == asqCollection.SizeName && u.ColorName == asqCollection.ColorName && u.OurStyleID == OurStyleId && u.POPackId == PoPackId).Select(u => u.PoPack_Detail_PK).FirstOrDefault();
                    var skudetpk= db.CutPlanMasters.Where(u => u.CutPlan_PK==CutPlan_PK).Select(u => u.SkuDet_PK).FirstOrDefault();
                    CutPlanASQDetail cddetail = new CutPlanASQDetail();
                    cddetail.CutPlan_PK = CutPlan_PK;
                    cddetail.PoPackId = PoPackId;
                    cddetail.PoPack_Detail_PK = int.Parse(popackdets_pk.ToString());
                    cddetail.CutQty = Qty;
                    cddetail.ColorName = asqCollection.ColorName;
                    cddetail.SizeName = asqCollection.SizeName;
                    cddetail.Skudet_PK = int.Parse(skudetpk.ToString());
                    cddetail.OurStyleId = OurStyleId;
                    cddetail.IsDeleted = "N";
                    cddetail.AddedVia = "Edit";
                    cddetail.AddedDate = DateTime.Now;
                    cddetail.AddedBy= HttpContext.Session["Username"].ToString();
                    db.CutPlanASQDetails.Add(cddetail);

                }
                else
                {
                    var q = from cutplanasqdert in db.CutPlanASQDetails
                            where cutplanasqdert.CutPlan_PK == CutPlan_PK && cutplanasqdert.OurStyleId == OurStyleId
                            && cutplanasqdert.PoPackId == PoPackId
                            && cutplanasqdert.SizeName == asqCollection.SizeName
                            && cutplanasqdert.ColorName == asqCollection.ColorName
                            select cutplanasqdert;

                    foreach (var element in q)
                    {
                        element.OldQty = element.CutQty;
                        element.CutQty = Qty;
                    }
                   
                }

            }


            var q1 = from cutplnmstr in db.CutPlanMasters
                     where cutplnmstr.CutPlan_PK == CutPlan_PK
                     select cutplnmstr;


                     foreach(var element in q1)
            {
                element.IsApproved = "N";
                element.IsRatioAdded = "N";
                element.IsRollAdded = "N";
                element.IsPatternAdded = "N";
                element.IsCutorderGiven = "N";
            }
            var q2 = from cutordermaster in db.CutOrderMasters
                     where cutordermaster.CutPlan_Pk == CutPlan_PK
                     select cutordermaster;
            foreach (var element in q2)
            {
                element.IsDeleted = "Y";
                
            }



            db.SaveChanges();


            status = true;


            return new JsonResult { Data = new { status = status } };
        }

      

        [HttpGet]
        public JsonResult PopulateCutPlan(int Id = 0)
        {
            List<decimal?> list = Session["ApprovedLocationlist"] as List<decimal?>;

            SelectList ourstyleitem = new SelectList(db.CutPlanMasters.Where(o => o.OurStyleID==Id && list.Contains(o.Location_PK)), "CutPlan_PK", "CutPlanNUM");

            JsonResult jsd = Json(ourstyleitem, JsonRequestBehavior.AllowGet);

            return jsd;

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