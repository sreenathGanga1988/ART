using ArtWebApp.Areas.ArtMVCGPO.ViewModal;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVCGPO.Controllers
{
    public class SalesDoDocumentController : Controller
    {
        ArtEntitiesnew db = new ArtEntitiesnew();
        // GET: ArtMVCGPO/SalesDoDocument
        public ActionResult Index(SalesDoModel model=null)
        {
            ConfigureViewModel(model);
           
            return View(model);
        }

        private void ConfigureViewModel(SalesDoModel model)
        {


            model.SDOList = new SelectList(db.InventorySalesMasters, "SalesDO_PK", "SalesDONum");
            if (model.SalesDO_PK.HasValue)
            {

                model.MrnFileUploads = getmrnList(int.Parse (model.SalesDO_PK.ToString()));


            }
            else
            {
                model.MrnFileUploads = null;


            }

        }




        public List<MrnFileUpload> getmrnList(int salesdoPk)
        {
            List<MrnFileUpload> mrnfileuploadlist = null;


            mrnfileuploadlist = (from inventorysalesdetails in db.InventorySalesDetails
                                 join
inventorymaster in db.StockInventoryMasters on inventorysalesdetails.SInventoryItem_PK equals inventorymaster.SInventoryItem_PK
                                 join stockmrndetails in db.StockMRNDetails on inventorymaster.SMRNDet_Pk equals stockmrndetails.SMRNDet_Pk
                                 join mrnfileupload in db.MrnFileUploads
on stockmrndetails.SMRN_Pk equals mrnfileupload.Mrn_PK
                                 where mrnfileupload.MrnType == "GMRN" && inventorysalesdetails.SalesDO_PK == salesdoPk
                                 select mrnfileupload).ToList();

            return mrnfileuploadlist;

        }



        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Show")]
        public ActionResult ShowMRN(GMRNViewModal model)
        {



            return RedirectToAction("Index", model);
        }




    }
}