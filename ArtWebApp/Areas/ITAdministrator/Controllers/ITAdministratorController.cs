using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ITAdministrator.Controllers
{
    public class ITAdministratorController : Controller
    {
        // GET: ITAdministrator/ITAdministrator
        public ActionResult ShowItTasks()
        {
            return View();
        }



        public ActionResult Simple()
        {
            List<SubMenuMaster> all = new List<SubMenuMaster>();
            using (ArtEntitiesnew dc = new ArtEntitiesnew())
            {
                all = dc.SubMenuMasters.OrderBy(a => a.ParentID).ToList();
            }
            return View(all);
        }



    }
}