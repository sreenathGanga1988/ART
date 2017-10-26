using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.ArtMVCGPO.Controllers
{
    public class StockMrnMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: ArtMVCGPO/StockMrnMasters
        public ActionResult Index()
        {
            var stockMrnMasters = db.StockMrnMasters .Where(s=>s.IsCompleted==null).Include(s => s.LocationMaster).Include(s => s.StockPOMaster).Include(s=>s.StockPOMaster.SupplierMaster ).Include(s => s.StockRecieptMaster);
            return View(stockMrnMasters.ToList());
        }

  
      
    }
}
