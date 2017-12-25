using ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel;
using ArtWebApp.Areas.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVCMerchandiser.Controllers
{
    public class AtcClosingController : Controller
    {
        // GET: ArtMVCMerchandiser/AtcClosing
        [HttpGet]
        public ActionResult Index()
        {
            AtcClosingModelList atcClosingModelList = new AtcClosingModelList();

            atcClosingModelList= ConfigureViewModel(atcClosingModelList);
            return View(atcClosingModelList);
        }


        [HttpGet]
        public ActionResult AtcOfmonth(AtcClosingModelList model)
        {
            AtcClosingRepo repo = new AtcClosingRepo();


            model.ClosedAtc = repo.AtcofMonth(model.Month);
            return View(model);
        }
        private AtcClosingModelList ConfigureViewModel(AtcClosingModelList model)
        {
            AtcClosingRepo repo = new AtcClosingRepo();
            model.atcClosingModels = repo.GetNonclosedatclist();

            return model;
        }


        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Save")]
        public ActionResult Index(AtcClosingModelList model)
        {
            model.AddedBy = HttpContext.Session["Username"].ToString();

            model.Addeddate = DateTime.Now;
            var Number = (from n
                   in model.atcClosingModels
                          where n.IsSelected == true
                          select n).ToList();
            model.atcClosingModels = Number;
            model.Type = "AtcClosing";
            AtcClosingRepo repo = new AtcClosingRepo();
             repo.CloseAtc(model);
            

            return RedirectToAction("Index");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Show")]
        public ActionResult ShowAtc(AtcClosingModelList model)
        {
            

            return RedirectToAction("AtcOfmonth", model);
        }

    }
}