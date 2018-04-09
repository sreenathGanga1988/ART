using ArtWebApp.Areas.Quality.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.Quality.Controllers
{
    public class QualityDashBoardController : Controller
    {
        // GET: Quality/QualityDashBoard
        public ActionResult QualityDashBoard()
        {
            QualityDashboardRepo qualityDashboardRepo = new QualityDashboardRepo();
               QualityDashBoardViewmodel reportDataModel = new QualityDashBoardViewmodel();
            reportDataModel = qualityDashboardRepo.GetQualityDashBoardViewmodel();
            
            return View(reportDataModel);
        }
    }
}