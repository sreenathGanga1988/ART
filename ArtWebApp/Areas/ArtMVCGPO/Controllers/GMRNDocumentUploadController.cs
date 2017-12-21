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
           
            ConfigureViewModel(model);
            return View(model);
        }


        [HttpGet]
        public ActionResult Index (GMRNViewModal model = null)
        {

            ConfigureViewModel(model);
            return View(model);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Show")]
        public ActionResult ShowMRN(GMRNViewModal model)
        {
           
           
           
            return RedirectToAction("GMRNDocumentUpload",model);
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
        [MultipleButton(Name = "action", Argument = "Save")]
        public ActionResult GMRNDocumentUpload(FileUpload upload, HttpPostedFileBase file, GMRNViewModal model)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var guid = Guid.NewGuid().ToString();
                string filenametext = guid + fileName;
                var path = Path.Combine(Server.MapPath("~/Uploads/"), filenametext);
                file.SaveAs(path);
                string fl = path.Substring(path.LastIndexOf("\\"));
                string[] split = fl.Split('\\');
                string newpath = split[1];
                string imagepath = "~/Uploads/" + newpath;

                MrnFileUpload mrnFileUpload = new MrnFileUpload();
                mrnFileUpload.Mrn_PK = model.MrnID;
                mrnFileUpload.StringLength = imagepath;
                mrnFileUpload.AddedBy = HttpContext.Session["Username"].ToString();
                mrnFileUpload.AddedDate = DateTime.Now;
                mrnFileUpload.Isdeleted = "N";
                mrnFileUpload.MrnType = "GMRN";
                mrnFileUpload.Filename = filenametext;
                db.MrnFileUploads.Add(mrnFileUpload);
                db.SaveChanges();
            }
            TempData["Success"] = "Upload successful";
            return RedirectToAction("GMRNDocumentUpload", model);
        }



        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Complete")]
        public ActionResult MarkMRNComplete( GMRNViewModal model)
        {
            if (model.MrnID!=null)
            {
                var q = from gmrnmaster in db.StockMrnMasters
                        where gmrnmaster.SMrn_PK == model.MrnID
                        select gmrnmaster;

              foreach(var element in q)
                {
                    element.IsCompleted = "Y";
                    element.MarkedCompletedBy = HttpContext.Session["Username"].ToString();
                    element.MarkCompletedDate = DateTime.Now;
                }
                db.SaveChanges();
            }
            TempData["Success"] = "Upload successful";
            return RedirectToAction("GMRNDocumentUpload", model);
        }








        public FileResult Download(int id)
        {
            int fid = Convert.ToInt32(id);
            
            string filename = (from f in db.MrnFileUploads
                               where f.FileUploadID == id
                               select f.StringLength).First();
            string contentType = "application/pdf";
            //Parameters to file are
            //1. The File Path on the File Server
            //2. The connent type MIME type
            //3. The paraneter for the file save asked by the browser
            return File(filename, contentType, "Report.pdf");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}