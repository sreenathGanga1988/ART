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
using System.Text;
using Rotativa.MVC;
using System.Data;

namespace ArtWebApp.Areas.MVCTNA.Controllers
{
    public class ProductIonTNAController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        // GET: MVCTNA/ProductIonTNA
        public ActionResult Index(ProductionTNAVModelMaster model=null)
        {
            ViewBag.Location_pk = new SelectList(db.LocationMasters.Where (u=> u.LocType=="F"), "Location_PK", "LocationName");

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
        public ActionResult TNAListIndex(ProductionTNAVModelMaster model = null)
        {
            ViewBag.Location_pk = new SelectList(db.LocationMasters.Where(u => u.LocType == "F"), "Location_PK", "LocationName");
            ViewBag.AtcID = new SelectList(db.AtcMasters.Where(u => u.IsShipmentCompleted != "Y").ToList(), "AtcId", "AtcNum");

            if (model == null)
            {
                ProductionTNARepo productionTNARepo = new ProductionTNARepo();

                model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAData(0, 0);
            }
            else
            {

            }

            return View(model);
        }


        [HttpPost]
        public JsonResult DeleteRoll(List<ProductionTNAATCList> things)
        {
            bool status = false;
            ProductionTNARepo tnarepo = new ProductionTNARepo();
            int count = 0;
            string username = HttpContext.Session["Username"].ToString();
            var per = from user in db.UserMasters where user.UserName == username && user.Is_Tnalistadd == "Y" select user;
            foreach (var cin in per)
            {

                count = count + 1;
            }
            if (count > 0)
            {

            
            foreach (ProductionTNAATCList roll in things)
            {
                int ourstyle= int.Parse(roll.ourstyle.ToString());
                var q1 = from ourlist in db.TNA_OurstlyeList where ourlist.Ourstyle_id == ourstyle select ourlist;
                foreach (var element in q1)
                {
                    element.IsDeleted = "Y";
                }
                

                try
                {
                    db.SaveChanges();
                    status = true;
                }
                catch (Exception exp)
                {
                    status = false;
                    throw;
                }

            }
                return Json(new { isok = true, message = "Succesfully Removed"});
            }
            else
            {
                return Json(new { isok = false, message = "Not Allowed" });
            }
            
        }
        

        [HttpGet]
        public JsonResult PopulateOurStyle(int Id=0,int locpk=0)
        {
            
            ProductionTNARepo tnarept = new ProductionTNARepo();
            
            DataTable mylist = tnarept.get_oursylelist(Id,locpk);            

            SelectList ourstyleitem = MVCControls.DataTabletoSelectList("OurStyleID", "OurStyle", mylist, "");

            JsonResult jsd = Json(ourstyleitem, JsonRequestBehavior.AllowGet);

            return jsd;

        }


        
       
        [HttpPost]
        public ActionResult update_ourstyle(decimal[] SelectedOurStyle,int locpk)

        {
            int atc_id = 0;
            int count = 0;
            string mes = "";
            string ourstylenum = "";
            string username= HttpContext.Session["Username"].ToString();
            var per = from user in db.UserMasters where user.UserName == username && user.Is_Tnalistadd=="Y" select user;
            foreach(var cin in per)
            {
                
                count = count + 1;
            }
            if (count>0)
            {

            
            foreach ( var  oursytle in SelectedOurStyle)
            {
                int ourstyleid = int.Parse(oursytle.ToString());
                var q = from atcdet in db.AtcDetails where atcdet.OurStyleID == ourstyleid  select atcdet;
                    foreach (var element in q)
                    {
                        atc_id = int.Parse(element.AtcId.ToString());
                    ourstylenum = element.OurStyle;

                    }

                if (!db.TNA_OurstlyeList.Any(f => f.Ourstyle_id== ourstyleid && f.location_pk== locpk))
                { 
                    TNA_OurstlyeList tnalist= new TNA_OurstlyeList();
                tnalist.Atc_id = atc_id;
                tnalist.Ourstyle_id = int.Parse(oursytle.ToString());
                tnalist.Ourstyle = ourstylenum;
                tnalist.location_pk = locpk;
               tnalist.AddedBy= HttpContext.Session["Username"].ToString();
                tnalist.AddedDate = DateTime.Now;
                tnalist.IsDeleted = "N";
                db.TNA_OurstlyeList.Add(tnalist);
                db.SaveChanges();
                }
            }
                mes = "Ourstyle details updated#" ;
                return Json(new { isok = true, message = mes });

            }
            else
            {
                mes= "You are not allowed";
                return Json(new { isok = false, message = mes });
            }
            //return RedirectToAction("TNAListIndex");
            
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

        [HttpGet]
        public PartialViewResult TNAOurstylewise(DateTime fromdate, DateTime todate, decimal[] SelectedOurStyle, int locpk)
        {
            
            int Ourstyle_id = 0;
            ProductionTNARepo productionTNARepo = new ProductionTNARepo();
            ProductionTNAVModelMaster model = new ProductionTNAVModelMaster();
            
            model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAOurstyleData(fromdate, todate, locpk, SelectedOurStyle);
            
            return PartialView("TNAListview", model);
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
                        mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        mailMessage.To.Add("jinil_raj@atraco.ae");
                        mailMessage.To.Add("Kotwal_Kiran@atraco.ae");
                        mailMessage.To.Add("kristel_tabao@atraco.ae");
                        mailMessage.To.Add("vijesh@ashton-apparel.com");
                        mailMessage.To.Add("bargesh@ashton-apparel.com");
                        mailMessage.To.Add("vijeesh_aandiyan@atraco.ae");
                        mailMessage.To.Add("cathy_joy@atraco.ae");
                        mailMessage.To.Add("sanjay.pandita@ashton-apparel.com");
                        mailMessage.To.Add("raghavendra.g@ashton-apparel.com");
                        mailMessage.To.Add("sathish@ashton-apparel.com");
                        mailMessage.To.Add("mannan_kapasi@atraco.ae");
                        mailMessage.To.Add("Sathyapalan_mk@atraco.ae");
                        mailMessage.To.Add("sandeep.kalathil@ashton-apparel.com");
                        mailMessage.To.Add("karan_jain@atraco.ae");

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
                        mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        mailMessage.To.Add("jinil_raj@atraco.ae");
                        mailMessage.To.Add("Kotwal_Kiran@atraco.ae");
                        mailMessage.To.Add("karan_jain@atraco.ae");
                        mailMessage.To.Add("devaraju_n@atraco.ae");
                        mailMessage.To.Add("vijeesh_aandiyan@atraco.ae");
                        mailMessage.To.Add("bargesh@ashton-apparel.com");
                        mailMessage.To.Add("vijesh@ashton-apparel.com");
                        mailMessage.To.Add("cathy_joy@atraco.ae");
                        mailMessage.To.Add("sathish@ashton-apparel.com");
                        mailMessage.To.Add("mannan_kapasi@atraco.ae");
                        mailMessage.To.Add("Sathyapalan_mk@atraco.ae");
                        mailMessage.To.Add("suresh.tk@mombasa-apparel.com");
                        mailMessage.To.Add("rajith@ashton-apparel.com");
                        mailMessage.To.Add("rhey_uson@ashton-apparel.com");
                        mailMessage.To.Add("siddu.hs@mombasa-apparel.com");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" + body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        smtp.Send(mailMessage);
                        result = true;
                    }
                }
                else if (location == 12 && merchant == "VIJEESH")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        mailMessage.To.Add("jinil_raj@atraco.ae");
                        mailMessage.To.Add("Kotwal_Kiran@atraco.ae");
                        mailMessage.To.Add("karan_jain@atraco.ae");
                        mailMessage.To.Add("jyoti_t@atraco.ae");
                        mailMessage.To.Add("vijeesh_aandiyan@atraco.ae");
                        mailMessage.To.Add("cathy_joy@atraco.ae");
                        mailMessage.To.Add("vijesh@ashton-apparel.com");
                        mailMessage.To.Add("mahesh.subrayamaiya@mombasa-apparel.com");
                        mailMessage.To.Add("bhupesh.mallappa@mombasa-apparel.com");
                        mailMessage.To.Add("bargesh@ashton-apparel.com");
                        mailMessage.To.Add("sathish@ashton-apparel.com");
                        mailMessage.To.Add("mannan_kapasi@atraco.ae");
                        mailMessage.To.Add("Sathyapalan_mk@atraco.ae");
                        mailMessage.To.Add("mohan@mombasa-apparel.com");


                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" + body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        smtp.Send(mailMessage);
                        result = true;
                    }
                }
                else if (location == 14 && merchant == "VIJEESH")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        mailMessage.To.Add("jinil_raj@atraco.ae");
                        mailMessage.To.Add("Kotwal_Kiran@atraco.ae");
                        mailMessage.To.Add("karan_jain@atraco.ae");
                        mailMessage.To.Add("kristel_tabao@atraco.ae");
                        mailMessage.To.Add("vijeesh_aandiyan@atraco.ae");
                        mailMessage.To.Add("cathy_joy@atraco.ae");
                        mailMessage.To.Add("sathish@ashton-apparel.com");
                        mailMessage.To.Add("mannan_kapasi@atraco.ae");
                        mailMessage.To.Add("Sathyapalan_mk@atraco.ae");
                        mailMessage.To.Add("bobby_charakanam@ashton-ethiopia.com");                        
                        mailMessage.To.Add("ronald.martis@ashton-ethiopia.com");
                        mailMessage.To.Add("mithilesh@ashton-ethiopia.com");
                        mailMessage.To.Add("avnish_choudhary@ashton-ethiopia.com");
                        mailMessage.To.Add("amarnath.karthick@ashton-ethiopia.com");

                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" + body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        smtp.Send(mailMessage);
                        result = true;
                    }
                }
                else if (location == 8 && merchant == "MAHENDRA")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        mailMessage.To.Add("jinil_raj@atraco.ae");
                        mailMessage.To.Add("Kotwal_Kiran@atraco.ae");
                        mailMessage.To.Add("karan_jain@atraco.ae");
                        mailMessage.To.Add("kristel_tabao@atraco.ae");
                        mailMessage.To.Add("mahendra_goswami@atraco.ae");
                        mailMessage.To.Add("jinu_rajan@atraco.ae");
                        mailMessage.To.Add("vijesh@ashton-apparel.com");
                        mailMessage.To.Add("bargesh@ashton-apparel.com");
                        mailMessage.To.Add("sanjay.pandita@ashton-apparel.com");
                        mailMessage.To.Add("raghavendra.g@ashton-apparel.com");
                        mailMessage.To.Add("sathish@ashton-apparel.com");
                        mailMessage.To.Add("mannan_kapasi@atraco.ae");
                        mailMessage.To.Add("sandeep.kalathil@ashton-apparel.com");
                        mailMessage.To.Add("Sathyapalan_mk@atraco.ae");
                        mailMessage.To.Add("rahul_powar@atraco.ae");

                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" + body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        smtp.Send(mailMessage);
                        result = true;
                    }
                }
                else if (location == 11 && merchant == "MAHENDRA")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        mailMessage.To.Add("jinil_raj@atraco.ae");
                        mailMessage.To.Add("Kotwal_Kiran@atraco.ae");
                        mailMessage.To.Add("karan_jain@atraco.ae");
                        mailMessage.To.Add("devaraju_n@atraco.ae");
                        mailMessage.To.Add("mahendra_goswami@atraco.ae");
                        mailMessage.To.Add("bargesh@ashton-apparel.com");
                        mailMessage.To.Add("vijesh@ashton-apparel.com");
                        mailMessage.To.Add("jinu_rajan@atraco.ae");
                        mailMessage.To.Add("sathish@ashton-apparel.com");
                        mailMessage.To.Add("mannan_kapasi@atraco.ae");
                        mailMessage.To.Add("Sathyapalan_mk@atraco.ae");
                        mailMessage.To.Add("suresh.tk@mombasa-apparel.com");
                        mailMessage.To.Add("rajith@ashton-apparel.com");
                        mailMessage.To.Add("rhey_uson@ashton-apparel.com");
                        mailMessage.To.Add("siddu.hs@mombasa-apparel.com");
                        mailMessage.To.Add("rahul_powar@atraco.ae");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" + body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        smtp.Send(mailMessage);
                        result = true;
                    }
                }
                else if (location == 12 && merchant == "MAHENDRA")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        mailMessage.To.Add("jinil_raj@atraco.ae");
                        mailMessage.To.Add("Kotwal_Kiran@atraco.ae");
                        mailMessage.To.Add("karan_jain@atraco.ae");
                        mailMessage.To.Add("jyoti_t@atraco.ae");
                        mailMessage.To.Add("mahendra_goswami@atraco.ae");
                        mailMessage.To.Add("jinu_rajan@atraco.ae");
                        mailMessage.To.Add("vijesh@ashton-apparel.com");
                        mailMessage.To.Add("mahesh.subrayamaiya@mombasa-apparel.com");
                        mailMessage.To.Add("bhupesh.mallappa@mombasa-apparel.com");
                        mailMessage.To.Add("bargesh@ashton-apparel.com");
                        mailMessage.To.Add("sathish@ashton-apparel.com");
                        mailMessage.To.Add("mannan_kapasi@atraco.ae");
                        mailMessage.To.Add("Sathyapalan_mk@atraco.ae");
                        mailMessage.To.Add("mohan@mombasa-apparel.com");
                        mailMessage.To.Add("rahul_powar@atraco.ae");

                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" + body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        smtp.Send(mailMessage);
                        result = true;
                    }
                }
                else if (location == 14 && merchant == "MAHENDRA")
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {

                        mailMessage.To.Add("zohair_kamdar@atraco.ae");
                        mailMessage.To.Add("jinil_raj@atraco.ae");
                        mailMessage.To.Add("Kotwal_Kiran@atraco.ae");
                        mailMessage.To.Add("karan_jain@atraco.ae");
                        mailMessage.To.Add("kristel_tabao@atraco.ae");
                        mailMessage.To.Add("mahendra_goswami@atraco.ae");
                        mailMessage.To.Add("jinu_rajan@atraco.ae");
                        mailMessage.To.Add("sathish@ashton-apparel.com");
                        mailMessage.To.Add("mannan_kapasi@atraco.ae");
                        mailMessage.To.Add("Sathyapalan_mk@atraco.ae");
                        mailMessage.To.Add("bobby_charakanam@ashton-ethiopia.com");
                        mailMessage.To.Add("ronald.martis@ashton-ethiopia.com");
                        mailMessage.To.Add("mithilesh@ashton-ethiopia.com");
                        mailMessage.To.Add("avnish_choudhary@ashton-ethiopia.com");
                        mailMessage.To.Add("amarnath.karthick@ashton-ethiopia.com");
                        mailMessage.To.Add("rahul_powar@atraco.ae");


                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "<div><b>" + locationname + " " + merchant + " TNA upto Merchant PCD " + startdate.ToString("dd/mm/yyyy") + "</b></div><br/>" +  body;
                        mailMessage.Subject = subject;
                        mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                        smtp.Send(mailMessage);
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
            else if (weekday == "Saturday")
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