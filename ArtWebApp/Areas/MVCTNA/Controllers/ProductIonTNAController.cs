using ArtWebApp.Areas.MVCTNA.TNAREpo;
using ArtWebApp.Areas.MVCTNA.ViewModel;
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

    }
}