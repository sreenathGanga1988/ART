using ArtWebApp.Areas.MVCTNA.TNAREpo;
using ArtWebApp.Areas.MVCTNA.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ArtWebApp.Areas.MVCTNA.Controllers
{
    public class ProductIonTNAController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: MVCTNA/ProductIonTNA
        public ActionResult Index(ProductionTNAVModelMaster model=null)
        {


            if (model == null)
            {
                ProductionTNARepo productionTNARepo = new ProductionTNARepo();

                model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAData(0,0);
            }
            else
            {

            }
           
            return View(model);
        }










        public ActionResult FacPCDChanged(ProductionTNAVModelMaster model = null)
        {


            ProductionTNARepo productionTNARepo = new ProductionTNARepo();

            DateTime tempdate = DateTime.Now.Date;
            tempdate = tempdate.AddMonths(12);

            model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAData(tempdate, tempdate, 0);

            return View(model);
        }







        [HttpGet]
        public PartialViewResult BetweenDateTNA(DateTime fromdate, DateTime todate)
        {

            ProductionTNARepo productionTNARepo = new ProductionTNARepo();
            ProductionTNAVModelMaster model = new ProductionTNAVModelMaster();
            model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAData(fromdate, todate,0);
            //  return RedirectToAction("Index", new RouteValueDictionary(model));
            return PartialView("TnaView", model);
        }

      
        public ActionResult Create(ProductionTNAVModelMaster model = null)
        {
            ConfigureViewModel(model);
            return View(model);
        }


        private void ConfigureViewModel(ProductionTNAVModelMaster model)
        {
            int atcid = 0;

            int userid = int.Parse(HttpContext.Session["User_PK"].ToString());
          
            model.tnaUserRights = db.TnaUserRights.Where(u => u.User_PK == userid).ToList();
            model.AtcList = new SelectList(db.AtcMasters.Where(o => o.IsClosed == "N"), "AtcId", "AtcNum");
            if (model.AtcID.HasValue)
            {

                ProductionTNARepo productionTNARepo = new ProductionTNARepo();

                model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAData(model.AtcID,0);

              
            }
            else
            {
                model.ProductionTNAVModelList = null;
            }

        }




        [HttpGet]
        public JsonResult ChangeFactoryDate(String MerchantPCD ,String FactoryPCD ,String CurrentFactoryPCD,int ourstyleid ,int locationpk)

        {
            bool status = false;


            var olddet = db.ProductionTNADetails.Where(u => u.OurStyleID == ourstyleid && u.Location_PK == locationpk && u.ProductionTNACompID == 15 && u.IsDeleted == "N").ToList();

            foreach(var element in olddet)
            {
                element.IsDeleted = "Y";
               
            }


            ProductionTNADetail productionTNADetail = new ProductionTNADetail();
            productionTNADetail.Location_PK = locationpk;
            productionTNADetail.OurStyleID = ourstyleid;
            productionTNADetail.ProductionTNACompID = 15;
            productionTNADetail.Actionname = "IsFactoryPlannedPCDDate";
            productionTNADetail.MarkedBy = HttpContext.Session["Username"].ToString();
            productionTNADetail.MarkedDate = DateTime.Now;
            productionTNADetail.IsDeleted = "N";
            db.ProductionTNADetails.Add(productionTNADetail);







            db.SaveChanges();


            status = true;




            JsonResult jsd = Json(new { status = status }, JsonRequestBehavior.AllowGet);
            return jsd;
        }




        [HttpGet]
        public JsonResult Mark(int? CompId, int? Ourstyleid , int? location_Pk, String Id)

        {
            bool status = false;
            int CutPlan_PK = 0;



            if (!db.ProductionTNADetails.Any(f => f.ProductionTNACompID == CompId && f.OurStyleID == Ourstyleid && f.Location_PK == location_Pk && f.IsDeleted=="N"))
            {


                ProductionTNADetail productionTNADetail = new ProductionTNADetail();
                productionTNADetail.Location_PK = location_Pk;
                productionTNADetail.OurStyleID = Ourstyleid;
                productionTNADetail.ProductionTNACompID = CompId;
                productionTNADetail.Actionname = Id;
                productionTNADetail.MarkedBy = HttpContext.Session["Username"].ToString();
                productionTNADetail.MarkedDate= DateTime.Now;
                productionTNADetail.IsDeleted = "N";
                db.ProductionTNADetails.Add(productionTNADetail);

            }




            db.SaveChanges();


            status = true;


           

            JsonResult jsd = Json(new { status = status }, JsonRequestBehavior.AllowGet);
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