using ArtWebApp.Areas.ArtMVC.Models.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVC.Controllers
{
    public class IPOController : Controller
    {
        ArtEntitiesnew db = new ArtEntitiesnew();
        // GET: IPO


        // GET: ArtMVC/IPO
        public ActionResult ShowIPOHistory(int Poid = 0)
        {
            IPORepository iprep = new IPORepository();

            IPOListViewModel ipoviewlisttmp = new IPOListViewModel();
            if (Poid != 0)
            {
                ipoviewlisttmp = iprep.GetIPOMasterData(Poid);
            }
            ViewBag.Poid = new SelectList(db.ODOOGPOMasters.Select(x => new { x.PONum, x.POId }).Distinct(), "POId", "PONum");


            return View(ipoviewlisttmp);
        }

    }
}