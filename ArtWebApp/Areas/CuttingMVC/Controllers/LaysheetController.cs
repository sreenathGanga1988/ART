using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.CuttingMVC.Controllers
{
    public class LaysheetController : Controller
    {
        // GET: CuttingMVC/Laysheet
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult DeleteLaySheet()
        {
            return View();
        }
    }
}