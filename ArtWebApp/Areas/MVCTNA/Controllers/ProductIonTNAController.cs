using ArtWebApp.Areas.MVCTNA.TNAREpo;
using ArtWebApp.Areas.MVCTNA.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.MVCTNA.Controllers
{
    public class ProductIonTNAController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: MVCTNA/ProductIonTNA
        public ActionResult Index(ProductionTNAVModelMaster model)
        {

            ProductionTNARepo productionTNARepo = new ProductionTNARepo();

            model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAData(0);
            return View(model);
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
            TnaUserRight tnaUserRight = db.TnaUserRights.Where(u=>u.User_PK==userid).FirstOrDefault();
            model.tnaUserRight = tnaUserRight;
            model.AtcList = new SelectList(db.AtcMasters.Where(o => o.IsClosed == "N"), "AtcId", "AtcNum");
            if (model.AtcID.HasValue)
            {

                ProductionTNARepo productionTNARepo = new ProductionTNARepo();

                model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAData(model.AtcID);

              
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