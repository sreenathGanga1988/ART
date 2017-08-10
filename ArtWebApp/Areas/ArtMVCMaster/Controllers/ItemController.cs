using ArtWebApp.ViewModel.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVCMaster.Controllers
{
    public class ItemController : Controller
    {
        // GET: ArtMVCMaster/Item
        public  ActionResult Index()
        {
         return   RedirectToAction("ItemMenuIndex");
        }


        public ActionResult ItemMenuIndex()
        {
         Itemmenu itmmenu = new Itemmenu();
            itmmenu.itemmenupk = 1;
            itmmenu.profilepk = 1;
            
             SubMenusiewModal asd = itmmenu.SubMenusiewModal;

     



            return View(itmmenu);
        }
    }






}