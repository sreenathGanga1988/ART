using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtWebApp.DataModels;

namespace ArtWebApp.Areas.Shipping.Controllers
{
    public class ShippingDocumentMastersController : Controller
    {
        private ArtEntitiesnew db = new ArtEntitiesnew();

        // GET: Shipping/ShippingDocumentMasters
        public ActionResult Index()
        {
            return View(db.ShippingDocumentMasters.ToList());
        }



        public ActionResult GateRecieptIndex()
        {
            return View(db.ShippingDocumentMasters.Where (u=>u.IsGateReceived==null).ToList());
        }


        // GET: Shipping/ShippingDocumentMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingDocumentMaster shippingDocumentMaster = db.ShippingDocumentMasters.Find(id);
            if (shippingDocumentMaster == null)
            {
                return HttpNotFound();
            }
            return View(shippingDocumentMaster);
        }


        // GET: Shipping/ShippingDocumentMasters/Details/5
        public ActionResult GatereceiptDetails(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingDocumentMaster shippingDocumentMaster = db.ShippingDocumentMasters.Find(id);
            if (shippingDocumentMaster == null)
            {
                return HttpNotFound();
            }
            return View(shippingDocumentMaster);
        }

        public ActionResult Receive(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingDocumentMaster shippingDocumentMaster = db.ShippingDocumentMasters.Find(id);

            shippingDocumentMaster.GateReceivedBy= HttpContext.Session["Username"].ToString();
            shippingDocumentMaster.IsGateReceived = true;
            shippingDocumentMaster.GateReceiptDate = DateTime.Now.Date;
            if (shippingDocumentMaster == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("GateRecieptIndex");
        }

        // GET: Shipping/ShippingDocumentMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shipping/ShippingDocumentMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShipingDoc_PK,AddedDate,AddedBY,ShipperName,ExporterName,ShipperInv,Description,NOofctnRoll,Packagetype,Weight,Type,InvoiceValue,Vessel,Conatianer,ContsainerType,ETA,ShipDocNum,BL,Mode,DocType,IsReceived,ReceivedBy,TentativeETA,ReceivedTime,GateReceiptDate,GateReceivedBy,IsGateReceived,Berth,ManifestNO,ManifestNoDate,ManifestNoAddedDate,ManifestNoAddedBy,CopyDocumentsReceivedDate,CopyDocumentsReceivedAddedDate,CopyDocumentsReceivedAddedBy,OrginalDocumentsReceivedDate,OrginalDocumentsReceivedAddedDate,OrginalDocumentsReceivedAddedBy,PortEntry,PortEntryAddedDate,PortEntrydAddedBy,LodgeDate,LodgeAddedDate,LodgeAddedBy,PassedDate,PassedDateAddedDate,PassedDateAddedBy,DocLodgeDate,DocLodgeAddedDate,DocLodgeAddedBy,DocRcvdDate,DocRcvdAddedDate,DocRcvdAddedBy,LodgePortDate,LodgePortAddedDate,LodgePortAddedBy,InhouseDate,InhouseAddedDate,InhouseAddedBy,KPACFSCHGS,KPACFSCHGSAddedDate,KPACFSCHGSAddedBy,HCharges,HChargesAddedDate,HChargesAddedBy,StockCharges,StockChargesAddedDate,StockChargesAddedBy,TransportCharges,TransportChargesAddedDate,TransportChargesAddedBy,TruckHaltingCharges,TruckHaltingAddedDate,TruckHaltingAddedBy,EsealCharges,EsealAddedDate,EsealAddedBy,Transporter,TransporterAddedDate,TransporterAddedBy,EntrylevyCharges,EntrylevyChargesAddedDate,EntrylevyChargesAddedBy,BondCancelledDate,BondCancelledDateAddedDate,BondCancelledDateAddedBy,InhouseFTY,InhouseFTYAddedDate,InhouseFTYAddedBy")] ShippingDocumentMaster shippingDocumentMaster)
        {
            if (ModelState.IsValid)
            {
                db.ShippingDocumentMasters.Add(shippingDocumentMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shippingDocumentMaster);
        }

        // GET: Shipping/ShippingDocumentMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {


                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingDocumentMaster shippingDocumentMaster = db.ShippingDocumentMasters.Find(id);
            ShippingRepo shippingRepo = new ShippingRepo();

           

            if (shippingDocumentMaster == null)
            {
                return HttpNotFound();

                
            }
            ViewBag.IsReceived = "N";
            if (shippingRepo.IsGateReceived(int.Parse( id.ToString())))
            {
                ViewBag.IsReceived = "Y";
            }
            else
            {
                ViewBag.IsReceived = "N";
            }
           
            return View(shippingDocumentMaster);
        }

        // POST: Shipping/ShippingDocumentMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShipingDoc_PK,AddedDate,AddedBY,ShipperName,ExporterName,ShipperInv,Description,NOofctnRoll,Packagetype,Weight,Type,InvoiceValue,Vessel,Conatianer,ContsainerType,ETA,ShipDocNum,BL,Mode,DocType,IsReceived,ReceivedBy,TentativeETA,ReceivedTime,GateReceiptDate,GateReceivedBy,IsGateReceived,Berth,ManifestNO,ManifestNoDate,ManifestNoAddedDate,ManifestNoAddedBy,CopyDocumentsReceivedDate,CopyDocumentsReceivedAddedDate,CopyDocumentsReceivedAddedBy,OrginalDocumentsReceivedDate,OrginalDocumentsReceivedAddedDate,OrginalDocumentsReceivedAddedBy,PortEntry,PortEntryAddedDate,PortEntrydAddedBy,LodgeDate,LodgeAddedDate,LodgeAddedBy,PassedDate,PassedDateAddedDate,PassedDateAddedBy,DocLodgeDate,DocLodgeAddedDate,DocLodgeAddedBy,DocRcvdDate,DocRcvdAddedDate,DocRcvdAddedBy,LodgePortDate,LodgePortAddedDate,LodgePortAddedBy,InhouseDate,InhouseAddedDate,InhouseAddedBy,KPACFSCHGS,KPACFSCHGSAddedDate,KPACFSCHGSAddedBy,HCharges,HChargesAddedDate,HChargesAddedBy,StockCharges,StockChargesAddedDate,StockChargesAddedBy,TransportCharges,TransportChargesAddedDate,TransportChargesAddedBy,TruckHaltingCharges,TruckHaltingAddedDate,TruckHaltingAddedBy,EsealCharges,EsealAddedDate,EsealAddedBy,Transporter,TransporterAddedDate,TransporterAddedBy,EntrylevyCharges,EntrylevyChargesAddedDate,EntrylevyChargesAddedBy,BondCancelledDate,BondCancelledDateAddedDate,BondCancelledDateAddedBy,InhouseFTY,InhouseFTYAddedDate,InhouseFTYAddedBy")] ShippingDocumentMaster shippingDocumentMaster)
        {
            if (ModelState.IsValid)
            {
                UpdateShippingDocument(shippingDocumentMaster);
                return RedirectToAction("Index");
            }
            return View(shippingDocumentMaster);
        }



        public void UpdateShippingDocument(ShippingDocumentMaster shippingDocumentMaster)
        {

            var q = from shpmstr in db.ShippingDocumentMasters
                    where shpmstr.ShipingDoc_PK == shippingDocumentMaster.ShipingDoc_PK
                    select shpmstr;

            foreach(var element in q)
            {
                String AddedbY = HttpContext.Session["Username"].ToString();










                if (shippingDocumentMaster.ManifestNoDate != null)
                {
                    element.ManifestNoDate = shippingDocumentMaster.ManifestNoDate;
                    element.ManifestNO = shippingDocumentMaster.ManifestNO;
                    
                    element.ManifestNoAddedBy = AddedbY;
                    element.ManifestNoAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.CopyDocumentsReceivedDate != null)
                {
                    element.CopyDocumentsReceivedDate = shippingDocumentMaster.CopyDocumentsReceivedDate;
                    element.CopyDocumentsReceivedAddedBy = AddedbY;
                    element.CopyDocumentsReceivedAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.OrginalDocumentsReceivedDate != null)
                {
                    element.OrginalDocumentsReceivedDate = shippingDocumentMaster.OrginalDocumentsReceivedDate;
                    element.OrginalDocumentsReceivedAddedBy = AddedbY;
                    element.OrginalDocumentsReceivedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.PortEntry != null)
                {
                    element.PortEntry = shippingDocumentMaster.PortEntry;
                    element.PortEntrydAddedBy = AddedbY;
                    element.PortEntryAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.LodgeDate != null)
                {
                    element.LodgeDate = shippingDocumentMaster.LodgeDate;
                    element.LodgeAddedBy = AddedbY;
                    element.LodgeAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.PassedDate != null)
                {
                    element.PassedDate = shippingDocumentMaster.PassedDate;
                    element.PassedDateAddedBy = AddedbY;
                    element.PassedDateAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.DocLodgeDate != null)
                {
                    element.DocLodgeDate = shippingDocumentMaster.DocLodgeDate;
                    element.DocLodgeAddedBy = AddedbY;
                    element.DocLodgeAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.DocRcvdDate != null)
                {
                    element.DocRcvdDate = shippingDocumentMaster.DocRcvdDate;
                    element.DocRcvdAddedBy = AddedbY;
                    element.DocRcvdAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.LodgePortDate != null)
                {


                    element.LodgePortDate = shippingDocumentMaster.LodgePortDate;
                    element.LodgePortAddedBy = AddedbY;
                    element.LodgePortAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.InhouseDate != null)
                {
                    

                    element.InhouseDate = shippingDocumentMaster.InhouseDate;
                    element.InhouseAddedBy = AddedbY;
                    element.InhouseAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.KPACFSCHGS != null)
                {
                    

                    element.KPACFSCHGS = shippingDocumentMaster.KPACFSCHGS;
                    element.KPACFSCHGSAddedBy = AddedbY;
                    element.KPACFSCHGSAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.HCharges != null)
                {
                    

                    element.HCharges = shippingDocumentMaster.HCharges;
                    element.HChargesAddedBy = AddedbY;
                    element.HChargesAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.StockCharges != null)
                {
                    
  element.StockCharges = shippingDocumentMaster.StockCharges;
                    element.StockChargesAddedBy = AddedbY;
                    element.StockChargesAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.TransportCharges != null)
                {
                    


                    element.TransportCharges = shippingDocumentMaster.TransportCharges;
                    element.TransportChargesAddedBy = AddedbY;
                    element.TransportChargesAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.TruckHaltingCharges != null)
                {
                    


                    element.TruckHaltingCharges = shippingDocumentMaster.TruckHaltingCharges;
                    element.TruckHaltingAddedBy = AddedbY;
                    element.TruckHaltingAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.EsealCharges != null)
                {
                    

                    element.EsealCharges = shippingDocumentMaster.EsealCharges;
                    element.EsealAddedBy = AddedbY;
                    element.EsealAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.Transporter != null)
                {
                    

                    element.Transporter = shippingDocumentMaster.Transporter;
                    element.TransporterAddedBy = AddedbY;
                    element.TransporterAddedDate = DateTime.Now;
                }
                
                
                 if (shippingDocumentMaster.EntrylevyCharges != null)
                {


                    element.EntrylevyCharges = shippingDocumentMaster.EntrylevyCharges;
                    element.EntrylevyChargesAddedBy = AddedbY;
                    element.EntrylevyChargesAddedDate = DateTime.Now;
                }
                if (shippingDocumentMaster.BondCancelledDate != null)
                {


                    element.BondCancelledDate = shippingDocumentMaster.BondCancelledDate;
                    element.BondCancelledDateAddedBy = AddedbY;
                    element.BondCancelledDateAddedDate = DateTime.Now;
                }

                
            }

            db.SaveChanges();

        }
        // GET: Shipping/ShippingDocumentMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingDocumentMaster shippingDocumentMaster = db.ShippingDocumentMasters.Find(id);
            if (shippingDocumentMaster == null)
            {
                return HttpNotFound();
            }
            return View(shippingDocumentMaster);
        }

        // POST: Shipping/ShippingDocumentMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ShippingDocumentMaster shippingDocumentMaster = db.ShippingDocumentMasters.Find(id);
            db.ShippingDocumentMasters.Remove(shippingDocumentMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
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
