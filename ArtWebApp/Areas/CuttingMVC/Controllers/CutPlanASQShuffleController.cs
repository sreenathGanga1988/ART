using ArtWebApp.Areas.CuttingMVC.Models;
using ArtWebApp.DataModelAtcWorld;
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
    public class CutPlanASQShuffleController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();

     
        // GET: CuttingMVC/CutPlanASQShuffle
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.AtcID = new SelectList(db.AtcMasters, "AtcId", "AtcNum");

            return View();
        }

        //[HttpGet]
        ////[MultipleButton(Name = "action", Argument = "EditCut")]
        //public ActionResult EditCut(FormCollection formCollection)
        //{


        //    int cutplanpk = int.Parse(Request.Form["CutPlanPK"].ToString());

        //    char[] AllStrings = Request.Form["POPACKID"].ToArray();

        //    return RedirectToAction("CutplanAsqShuffle", new { id = cutplanpk });


        //}

        public ActionResult EditCut(decimal[] SelectedOurStyle, int Id = 0)
        {



            return RedirectToAction("CutplanAsqShuffle", new { id = Id ,SelectedPopackID= SelectedOurStyle});


        }


       
        [HttpGet]
        public ActionResult CutplanAsqShuffle(decimal? id, decimal[] SelectedOurStyle)
        {
            CutplanViewModel cutplanViewModel = new CutplanViewModel(int.Parse(id.ToString()));
            cutplanViewModel.SelectedPopackID = SelectedOurStyle;
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
               

            }






           


            return cutplanViewModel;
        }

        [HttpGet]
        public JsonResult PopulateCutOrderofStyle( int Id = 0)
        {
            CuttingRepository repo = new CuttingRepository();

           DataTable myasq = repo.CutplanASQNotPresent(Id);

            SelectList PopackList = MVCControls.DataTabletoSelectList("PoPackId", "PoPacknum",myasq,"");

            JsonResult jsd = Json(PopackList, JsonRequestBehavior.AllowGet);

            return jsd;

        }





        [HttpPost]
        public JsonResult Create(List<AsqDetCollection> things, int Id = 0)

        {
            
            bool status = false;


            int actualcutplanpk = 0;
            int actualskudetpk = 0;
            ArtWebApp.DataModelAtcWorld.AtcWorldEntities kenyadb = new DataModelAtcWorld.AtcWorldEntities();

            foreach (AsqDetCollection asqCollection in things)
            {
                 int CutPlan_PK = int.Parse(asqCollection.CutPlan_PK.ToString());
                int PoPack_Detail_PK = int.Parse(asqCollection.PoPackdetID.ToString());
                Decimal Qty = Decimal.Parse(asqCollection.Qty.ToString());
                int ActualCutPlan_PK=int.Parse(asqCollection.ActualCutPlan_PK.ToString());
                POPackDetail pddetpk = db.POPackDetails.Find(PoPack_Detail_PK);

                if (CutPlan_PK == 0)
                {
                    if (!db.CutPlanASQDetails.Any(f => f.CutPlan_PK == ActualCutPlan_PK && f.PoPack_Detail_PK== PoPack_Detail_PK))
                    {
                        CutPlanASQDetail cddetail = new CutPlanASQDetail();
                        cddetail.CutPlan_PK = ActualCutPlan_PK;
                        cddetail.PoPackId = pddetpk.POPackId;
                        cddetail.PoPack_Detail_PK = PoPack_Detail_PK;
                        cddetail.CutQty = Qty;
                        cddetail.ColorName = pddetpk.ColorName;
                        cddetail.SizeName = pddetpk.SizeName;
                        cddetail.Skudet_PK = actualskudetpk;
                        cddetail.OurStyleId = pddetpk.OurStyleID;
                        cddetail.IsDeleted = "N";
                        cddetail.AddedVia = "Edit";
                        cddetail.AddedDate = DateTime.Now;
                        cddetail.AddedBy = HttpContext.Session["Username"].ToString();
                        db.CutPlanASQDetails.Add(cddetail);

                        db.SaveChanges();
                        if (kenyadb.ArtCutPlanASQDets.Any(f => f.CutPlan_PK == ActualCutPlan_PK))
                        {

                            ArtCutPlanASQDet asqdet = new DataModelAtcWorld.ArtCutPlanASQDet();

                            asqdet.PoPackId = pddetpk.POPackId;
                            asqdet.PoPack_Detail_PK = PoPack_Detail_PK;
                            asqdet.ColorName = pddetpk.ColorName;
                            asqdet.SizeName = pddetpk.SizeName;
                            asqdet.CutQty = Qty;
                            asqdet.CutPlan_PK = actualcutplanpk;
                            asqdet.CutPlanASQDetails_PK = cddetail.CutPlanASQDetails_PK;

                            kenyadb.ArtCutPlanASQDets.Add(asqdet);

                        }


                        kenyadb.SaveChanges();
                    }

                  


                   






                }
                else
                {

                    actualcutplanpk = CutPlan_PK;
                    if (db.CutPlanASQDetails.Any(f => f.CutPlan_PK == CutPlan_PK && f.PoPack_Detail_PK == PoPack_Detail_PK ))
                    {
                        var q = from cutplanasqdert in db.CutPlanASQDetails
                                where cutplanasqdert.CutPlan_PK == CutPlan_PK && cutplanasqdert.PoPack_Detail_PK == PoPack_Detail_PK
                                select cutplanasqdert;

                        foreach (var element in q)
                        {
                            actualskudetpk =int.Parse( element.Skudet_PK.ToString());
                            element.OldQty = element.CutQty;
                            element.CutQty = Qty;
                        }



                        var q1 = from cutplanasqdert in kenyadb.ArtCutPlanASQDets
                                 where cutplanasqdert.CutPlan_PK == CutPlan_PK && cutplanasqdert.PoPack_Detail_PK == PoPack_Detail_PK
                                select cutplanasqdert;

                        foreach (var element in q1)
                        {
                           
                            element.CutQty = Qty;
                        }


                    }
                }

                kenyadb.SaveChanges();
                db.SaveChanges();

             




            }
            return new JsonResult { Data = new { status = status } };
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