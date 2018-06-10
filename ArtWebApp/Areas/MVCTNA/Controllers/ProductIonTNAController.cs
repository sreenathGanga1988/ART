using ArtWebApp.Areas.MVCTNA.TNAREpo;
using ArtWebApp.Areas.MVCTNA.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Net.Mail;
using System.IO;
using System.Web.Routing;
using System.Text;
using Rotativa.MVC;

namespace ArtWebApp.Areas.MVCTNA.Controllers
{
    public class ProductIonTNAController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: MVCTNA/ProductIonTNA
        public ActionResult Index(ProductionTNAVModelMaster model=null)
        {


            if (model == null)
            {
                ProductionTNARepo productionTNARepo = new ProductionTNARepo();

                model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAData(0,0);
            }
            else
            {

            }
           
            return View(model);
        }


        public ActionResult FacPCDChanged(ProductionTNAVModelMaster model = null)
        {


            ProductionTNARepo productionTNARepo = new ProductionTNARepo();

            DateTime tempdate = DateTime.Now.Date;
            tempdate = tempdate.AddMonths(12);

            model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAData(tempdate, tempdate, 0);

            return View(model);
        }

        public ActionResult PrintIndex(Decimal id)
        {
            var report = new Rotativa.MVC.ActionAsPdf("PendingGateReceipt", new { id = id });
            return report;
        }


        [HttpGet]
        public PartialViewResult BetweenDateTNA(DateTime fromdate, DateTime todate)
        {

            ProductionTNARepo productionTNARepo = new ProductionTNARepo();
            ProductionTNAVModelMaster model = new ProductionTNAVModelMaster();
            model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAData(fromdate, todate, 0);
            
                        
            return PartialView("TnaView", model);
        }
        public Boolean SendTnaMail(DateTime startdate, String merchant, int location)
        {
            var result = false;
            var subject = "";
            var body = "";
            var locationname = "";
            var fromAddress = new MailAddress("atcdailyexpress@gmail.com", "TNA Details Report");
            const string fromPassword = "8812686atc";
            ProductionTNARepo productionTNARepo = new ProductionTNARepo();
            ProductionTNAVModelMaster model = new ProductionTNAVModelMaster();
            var q = from LocationMaster in db.LocationMasters where LocationMaster.Location_PK== location select LocationMaster;
            foreach(var element in q)
            {
                locationname = element.LocationName;
            }
            model.ProductionTNAVModelList = productionTNARepo.MerchantwiseData(startdate, startdate, merchant, location);
            if (model.ProductionTNAVModelList.Count > 0)
            {
                body = RenderPartialViewToString("TnaView", model);
                subject = merchant +" / " + locationname + " TNA Details";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
                };
                if (location == 8 && merchant=="VIJEESH")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        //mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        //mailMessage.To.Add("jinil_raj@atraco.ae");
                        //mailMessage.To.Add("kristel_tabao@atraco.ae");
                        //mailMessage.To.Add("vijeesh_aandiyan@atraco.ae");
                        //mailMessage.To.Add("cathy_joy@atraco.ae");
                        mailMessage.To.Add("prakash_rethinam@atraco.ae");
                        //mailMessage.To.Add("abhishek_gupta@atraco.ae");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body ="<div><b>"+ locationname + " "+ merchant +" TNA upto Merchant PCD "+ startdate.ToString("dd/mm/yyyy") + "</b></div><br/>"+ body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        smtp.Send(mailMessage);
                        result = true;
                    }
                }
                else if (location == 11 && merchant == "VIJEESH")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        //mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        //mailMessage.To.Add("jinil_raj@atraco.ae");
                        //mailMessage.To.Add("devaraju_n@atraco.ae");
                        //mailMessage.To.Add("vijeesh_aandiyan@atraco.ae");
                        //mailMessage.To.Add("cathy_joy@atraco.ae");
                        mailMessage.To.Add("prakash_rethinam@atraco.ae");
                        mailMessage.To.Add("mannan_kapasi@atraco.ae");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" + body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        //smtp.Send(mailMessage);
                        result = true;
                    }
                }
                else if (location == 12 && merchant == "VIJEESH")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        //mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        //mailMessage.To.Add("jinil_raj@atraco.ae");
                        //mailMessage.To.Add("jyoti_t@atraco.ae");
                        //mailMessage.To.Add("vijeesh_aandiyan@atraco.ae");
                        //mailMessage.To.Add("cathy_joy@atraco.ae");
                        mailMessage.To.Add("prakash_rethinam@atraco.ae");
                        mailMessage.To.Add("rethinam.prakash@gmail.com");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" + body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        //smtp.Send(mailMessage);
                        result = true;
                    }
                }
                else if (location == 14 && merchant == "VIJEESH")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        //mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        //mailMessage.To.Add("jinil_raj@atraco.ae");
                        //mailMessage.To.Add("kristel_tabao@atraco.ae");
                        //mailMessage.To.Add("vijeesh_aandiyan@atraco.ae");
                        //mailMessage.To.Add("cathy_joy@atraco.ae");
                        mailMessage.To.Add("prakash_rethinam@atraco.ae");
                        mailMessage.To.Add("rethinam.prakash@gmail.com");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" + body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        //smtp.Send(mailMessage);
                        result = true;
                    }
                }
                else if (location == 8 && merchant == "MAHENDRA")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        //mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        //mailMessage.To.Add("jinil_raj@atraco.ae");
                        //mailMessage.To.Add("kristel_tabao@atraco.ae");
                        //mailMessage.To.Add("mahendra_goswami@atraco.ae");
                        //mailMessage.To.Add("cathy_joy@atraco.ae");
                        mailMessage.To.Add("prakash_rethinam@atraco.ae");
                        mailMessage.To.Add("abhishek_gupta@atraco.ae");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" + body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        //smtp.Send(mailMessage);
                        result = true;
                    }
                }
                else if (location == 11 && merchant == "MAHENDRA")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        //mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        //mailMessage.To.Add("jinil_raj@atraco.ae");
                        //mailMessage.To.Add("devaraju_n@atraco.ae");
                        //mailMessage.To.Add("mahendra_goswami@atraco.ae");
                        //mailMessage.To.Add("cathy_joy@atraco.ae");
                        mailMessage.To.Add("prakash_rethinam@atraco.ae");
                        mailMessage.To.Add("rethinam.prakash@gmail.com");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" + body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        //smtp.Send(mailMessage);
                        result = true;
                    }
                }
                else if (location == 12 && merchant == "MAHENDRA")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        //mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        //mailMessage.To.Add("jinil_raj@atraco.ae");
                        //mailMessage.To.Add("jyoti_t@atraco.ae");
                        //mailMessage.To.Add("mahendra_goswami@atraco.ae");
                        //mailMessage.To.Add("cathy_joy@atraco.ae");
                        mailMessage.To.Add("prakash_rethinam@atraco.ae");
                        mailMessage.To.Add("rethinam.prakash@gmail.com");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" + body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        //smtp.Send(mailMessage);
                        result = true;
                    }
                }
                else if (location == 14 && merchant == "MAHENDRA")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        //mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        //mailMessage.To.Add("jinil_raj@atraco.ae");
                        //mailMessage.To.Add("kristel_tabao@atraco.ae");
                        //mailMessage.To.Add("mahendra_goswami@atraco.ae");
                        //mailMessage.To.Add("cathy_joy@atraco.ae");
                        mailMessage.To.Add("prakash_rethinam@atraco.ae");
                        mailMessage.To.Add("rethinam.prakash@gmail.com");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" +  body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        //smtp.Send(mailMessage);
                        result = true;
                    }
                }

            }

            return result;
        }

        [HttpGet]
        public PartialViewResult Sendmail(DateTime fromdate, DateTime todate)
        {

            ProductionTNARepo productionTNARepo = new ProductionTNARepo();
            ProductionTNAVModelMaster model = new ProductionTNAVModelMaster();          

            var result = false;            
            var q = from Merchandiser in db.MerchandiserMasters select Merchandiser;
            
            DateTime startdate = DateTime.Now.AddDays(14);
            var weekday=startdate.ToString("dddd");

            if (weekday == "Wednesday")
            {
                foreach (var element in q)
                {
                    result = SendTnaMail(startdate, element.MerchandiserName, 8);
                    result = SendTnaMail(startdate, element.MerchandiserName, 11);
                    result = SendTnaMail(startdate, element.MerchandiserName, 12);
                    result = SendTnaMail(startdate, element.MerchandiserName, 14);
                }
            }
            else if (weekday == "Sunday")
            {
                foreach (var element in q)
                {
                    result = SendTnaMail(startdate, element.MerchandiserName, 8);
                    result = SendTnaMail(startdate, element.MerchandiserName, 11);
                    result = SendTnaMail(startdate, element.MerchandiserName, 12);
                    result = SendTnaMail(startdate, element.MerchandiserName, 14);
                }
            }
                    
            
            return PartialView(result);
        }


        

        public ActionResult Create(ProductionTNAVModelMaster model = null)
        {
            ConfigureViewModel(model);
            return View(model);
        }


        private void ConfigureViewModel(ProductionTNAVModelMaster model)
        {
            int atcid = 0;

            int userid = int.Parse(HttpContext.Session["User_PK"].ToString());
          
            model.tnaUserRights = db.TnaUserRights.Where(u => u.User_PK == userid).ToList();
            model.AtcList = new SelectList(db.AtcMasters.Where(o => o.IsClosed == "N"), "AtcId", "AtcNum");
            if (model.AtcID.HasValue)
            {

                ProductionTNARepo productionTNARepo = new ProductionTNARepo();

                model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAData(model.AtcID,0);

              
            }
            else
            {
                model.ProductionTNAVModelList = null;
            }

        }




        [HttpGet]
        public JsonResult ChangeFactoryDate(String MerchantPCD ,String FactoryPCD ,String CurrentFactoryPCD,int? ourstyleid ,int? locationpk)

        {
            bool status = false;


            var olddet = db.ProductionTNADetails.Where(u => u.OurStyleID == ourstyleid && u.Location_PK == locationpk && u.ProductionTNACompID == 15 && u.IsDeleted == "N").ToList();

            foreach(var element in olddet)
            {
                element.IsDeleted = "Y";
               
            }


            ProductionTNADetail productionTNADetail = new ProductionTNADetail();
            productionTNADetail.Location_PK = locationpk;
            productionTNADetail.OurStyleID = ourstyleid;
            productionTNADetail.ProductionTNACompID = 15;
            productionTNADetail.Actionname = "IsFactoryPlannedPCDDate";
            productionTNADetail.MarkedBy = HttpContext.Session["Username"].ToString();
            productionTNADetail.MarkedDate = DateTime.Now;
            productionTNADetail.IsDeleted = "N";
            db.ProductionTNADetails.Add(productionTNADetail);







            db.SaveChanges();


            status = true;




            JsonResult jsd = Json(new { status = status }, JsonRequestBehavior.AllowGet);
            return jsd;
        }




        [HttpGet]
        public JsonResult Mark(int? CompId, int? Ourstyleid , int? location_Pk, String Id)

        {
            bool status = false;
            int CutPlan_PK = 0;



            if (!db.ProductionTNADetails.Any(f => f.ProductionTNACompID == CompId && f.OurStyleID == Ourstyleid && f.Location_PK == location_Pk && f.IsDeleted=="N"))
            {


                ProductionTNADetail productionTNADetail = new ProductionTNADetail();
                productionTNADetail.Location_PK = location_Pk;
                productionTNADetail.OurStyleID = Ourstyleid;
                productionTNADetail.ProductionTNACompID = CompId;
                productionTNADetail.Actionname = Id;
                productionTNADetail.MarkedBy = HttpContext.Session["Username"].ToString();
                productionTNADetail.MarkedDate= DateTime.Now;
                productionTNADetail.IsDeleted = "N";
                db.ProductionTNADetails.Add(productionTNADetail);

            }




            db.SaveChanges();


            status = true;


           

            JsonResult jsd = Json(new { status = status }, JsonRequestBehavior.AllowGet);
            return jsd;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       

protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }


    }
}