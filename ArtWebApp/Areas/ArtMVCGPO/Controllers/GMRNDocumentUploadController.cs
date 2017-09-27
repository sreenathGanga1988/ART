using ArtWebApp.Areas.ArtMVCGPO.ViewModal;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ArtWebApp.Areas.ArtMVCGPO.Controllers
{
    public class GMRNDocumentUploadController : Controller
    {
        ArtEntitiesnew db = new ArtEntitiesnew();
        // GET: ArtMVCGPO/GMRNDocumentUpload
        [HttpGet]
        public ActionResult GMRNDocumentUpload(GMRNViewModal model=null)
        {
            GMRNViewModal grvmdl = new GMRNViewModal();
            ConfigureViewModel(grvmdl);
            return View(grvmdl);
        }


        private void ConfigureViewModel(GMRNViewModal model)
        {
          

            model.MrnList = new SelectList(db.StockMrnMasters, "SMrn_PK", "SMrnNum");
            if (model.MrnID.HasValue)
            {

                model.MrnFileUploads = db.MrnFileUploads.Where(u => u.Mrn_PK == model.MrnID).ToList();


            }
            else
            {
                model.MrnFileUploads = null;


            }

        }


        [HttpPost]
        public ActionResult GMRNDocumentUpload(FileUpload upload, HttpPostedFileBase file, GMRNViewModal model)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var guid = Guid.NewGuid().ToString();
                var path = Path.Combine(Server.MapPath("~/Uploads/Gmrn"), guid + fileName);
                file.SaveAs(path);
                string fl = path.Substring(path.LastIndexOf("\\"));
                string[] split = fl.Split('\\');
                string newpath = split[1];
                string imagepath = "~/uploads/" + newpath;

                MrnFileUpload mrnFileUpload = new MrnFileUpload();
                mrnFileUpload.Mrn_PK = model.MrnID;
                mrnFileUpload.StringLength = imagepath;
                db.MrnFileUploads.Add(mrnFileUpload);
                db.SaveChanges();
            }
            TempData["Success"] = "Upload successful";
            return RedirectToAction("GMRNDocumentUpload", model);
        }
    }
}