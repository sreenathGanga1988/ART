using ArtWebApp.Areas.Accounts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.Accounts.Controllers
{
    public class AccountReportController : Controller
    {
        // GET: Accounts/AccountReport
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult GetDashBoard()
        {
            AccountdashBoard model = new AccountdashBoard();
            
            AccountRepo accountRepo = new AccountRepo();
            model = accountRepo.GetAccountDashBoard();          

            
            return PartialView("AccontDashboardview", model);
        }
    }
}