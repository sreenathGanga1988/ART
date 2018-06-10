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

            atcClosingModelList= ConfigureViewModel1(atcClosingModelList);
            return View(atcClosingModelList);
        }

        [HttpGet]
        public ActionResult AddToBEofMonth()
        {
            AtcClosingModelList atcClosingModelList = new AtcClosingModelList();

            atcClosingModelList = ConfigureViewModel(atcClosingModelList);
            return View(atcClosingModelList);
        }
        [HttpGet]
        public ActionResult AtcOfmonth(AtcClosingModelList model)
        {
            AtcClosingRepo repo = new AtcClosingRepo();

           if( model.Type == "Atcclosing")
            {
                model.ClosedAtc = repo.AtcofMonth(model.Month);
            }
            else
            {
                model.ClosedAtc = repo.GetBEofMonthAtc(model.Month);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult BEofMonth(AtcClosingModelList model)
        {
            AtcClosingRepo repo = new AtcClosingRepo();

          
                model.ClosedAtc = repo.GetBEofMonthAtc(model.Month);
            
            return View(model);
        }






        private AtcClosingModelList ConfigureViewModel(AtcClosingModelList model)
        {
            AtcClosingRepo repo = new AtcClosingRepo();
            model.atcClosingModels = repo.GetNonclosedatclist();

            return model;
        }
        private AtcClosingModelList ConfigureViewModel1(AtcClosingModelList model)
        {
            AtcClosingRepo repo = new AtcClosingRepo();
            model.atcClosingModels = repo.GetNonclosedatclistforshipmentclose();

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
        [MultipleButton(Name = "action", Argument = "AddToBEofMonth")]
        public ActionResult AddToBEofMonth(AtcClosingModelList model)
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
            repo.BEofMonth(model);


            return RedirectToAction("AddToBEofMonth");
        }






        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Show")]
        public ActionResult ShowAtc(AtcClosingModelList model)
        {
            model.Type = "Atcclosing";

            return RedirectToAction("AtcOfmonth", model);
        }
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "ShowBE")]
        public ActionResult ShowAtcBE(AtcClosingModelList model)
        {
            model.Type = "BE";

            return RedirectToAction("BEofMonth", model);
        }
        

        [HttpGet]
        public ActionResult RemoveAtcBE(String Month)
        {
            AtcClosingRepo repo = new AtcClosingRepo();
            repo.RemoveMonth(Month);


            return RedirectToAction("AddToBEofMonth");
        }

    }
}