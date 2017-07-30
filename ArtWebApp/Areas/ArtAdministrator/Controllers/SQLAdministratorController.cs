using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtAdministrator.Controllers
{
    public class SQLAdministratorController : Controller
    {
        // GET: ArtAdministrator/SQLAdministrator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BackUPDatabase()
        {


            return View();
        }


    }
}