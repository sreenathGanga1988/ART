using ArtWebApp.Areas.ProductionMVC.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ProductionMVC.Controllers
{
    public class ASQTrackerController : Controller
    {
        // GET: ProductionMVC/ASQTracker
        public ActionResult Index()
        {
            AsqTrackerModel asqTrackerModel = new AsqTrackerModel();

            ProductionRepo productionRepo = new ProductionRepo();
            asqTrackerModel = productionRepo.GetProductionTNAData();


            return View(asqTrackerModel);
        }
    }
}