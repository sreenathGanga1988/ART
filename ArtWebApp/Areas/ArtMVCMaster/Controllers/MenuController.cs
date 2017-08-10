
using ArtWebApp.ViewModel.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVCMaster.Controllers
{
    public class MenuController : Controller
    {
        // GET: ArtMVCMaster/Menu
        public ActionResult MenuIndex(int Id = 0)
        {
            UserMenu itmmenu = new UserMenu();
            itmmenu.itemmenupk = Id;
            itmmenu.profilepk = int.Parse(Session["UserProfile_Pk"].ToString());
            SubMenusiewModal asd = itmmenu.SubMenusiewModal;





            return View(itmmenu);
        }


        public ActionResult ItemMenuIndex(int menupk=0)
        {
            UserMenu itmmenu = new UserMenu();
            itmmenu.itemmenupk = 1;

            SubMenusiewModal asd = itmmenu.SubMenusiewModal;





            return View(itmmenu);
        }
    }
}