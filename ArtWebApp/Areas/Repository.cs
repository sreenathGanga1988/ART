using ArtWebApp.Areas.ArtMVC.Models.ViewModel;
using ArtWebApp.Areas.CuttingMVC.Models;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtWebApp.Areas
{
    

    public class IPORepository
    {
        ArtEntitiesnew db = new ArtEntitiesnew();





        public IPOListViewModel GetIPOMasterData(int POID)
        {
            IPOListViewModel ipolistview = new IPOListViewModel();

            var IPOViewModellist = ((from oodogpomstr in db.ODOOGPOMasters
                                     where oodogpomstr.POId == POID
                                     select new IPOViewModel { IPONumber = oodogpomstr.PONum, OODoLocation = oodogpomstr.OdooLocation, Poid = oodogpomstr.POId }).Distinct()).ToList();




            foreach (IPOViewModel element in IPOViewModellist)
            {
                int poid = int.Parse(element.Poid.ToString());

                element.IPODetailsViewModellist = GetIPoDetailsData(poid);





            }





            ipolistview.IPOMasterlist = IPOViewModellist;

            return ipolistview;


        }






        public List<IPODetailsViewModel> GetIPoDetailsData(int POID)
        {

            IPOListViewModel ipolistview = new IPOListViewModel();

            var q = ((from oodogpomstr in db.ODOOGPOMasters
                      where oodogpomstr.POId == POID
                      select new IPODetailsViewModel { ItemDescription = oodogpomstr.Description, RequiredQty = oodogpomstr.Qty, POLIneID = oodogpomstr.POLineID, OODOUOM = oodogpomstr.Odoo_UOM }).Distinct()).ToList();

            foreach (IPODetailsViewModel element in q)
            {
                int POLIneID = int.Parse(element.POLIneID.ToString());

                element.SpoDetailsList = GetSPoDetailsData(POLIneID);





            }
            return q;
        }




        public List<SpoDetails> GetSPoDetailsData(int POLineID)
        {



            var q = ((from oodogpomstr in db.StocPOForODOOs
                      join stockpo in db.StockPOMasters on oodogpomstr.Spo_PK equals stockpo.SPO_Pk
                      where oodogpomstr.POLineID == POLineID
                      select new SpoDetails { SPOnum = stockpo.SPONum, Qty = oodogpomstr.POQty, Suppplier = stockpo.SupplierMaster.SupplierName, SPOUOM = oodogpomstr.UOMMaster.UomName, Spodet_pk = oodogpomstr.SPoDet_PK }).Distinct()).ToList();

            foreach (SpoDetails element in q)
            {
                int Spodetail_PK = int.Parse(element.Spodet_pk.ToString());


                element.SMRNDetailsList = GetMRNDetailsData(Spodetail_PK);


            }
            return q;
        }



        public List<SMRNDetails> GetMRNDetailsData(int spodetil_pk)
        {



            var q = ((from oodogpomstr in db.StockMrnMasters
                      join stockpo in db.StockMRNDetails on oodogpomstr.SMrn_PK equals stockpo.SMRN_Pk
                      where stockpo.SPODetails_PK == spodetil_pk
                      select new SMRNDetails { SMRNNUM = oodogpomstr.SMrnNum, Qty = stockpo.ReceivedQty, ExtraQty = stockpo.ExtraQty, SMRNDate = oodogpomstr.AddedDate, invoice = oodogpomstr.DoNumber, smrndet_pk = stockpo.SMRNDet_Pk }).Distinct()).ToList();

            foreach (SMRNDetails element in q)
            {
                int smrndet_PK = int.Parse(element.smrndet_pk.ToString());

                element.SDODetailsList = GetSDODetails(smrndet_PK);


            }
            return q;
        }

        public List<SDODetails> GetSDODetails(int smrndet_PK)
        {



            var q = ((from oodogpomstr in db.InventorySalesDetails
                      join stockinv in db.StockInventoryMasters on oodogpomstr.SInventoryItem_PK equals stockinv.SInventoryItem_PK
                      where stockinv.SMRNDet_Pk == smrndet_PK
                      select new SDODetails
                      { SDONUM = oodogpomstr.InventorySalesMaster.SalesDONum, Qty = oodogpomstr.DeliveryQty, AddedBy = oodogpomstr.InventorySalesMaster.AddedBy, AddedDate = oodogpomstr.InventorySalesMaster.AddedDate }).Distinct()).ToList();

            foreach (SDODetails element in q)
            {





            }
            return q;
        }
    }




    public class LaysheetRollRepository
    {
        ArtEntitiesnew db = new ArtEntitiesnew();

     public  List<ApprovelaysheetModel> getlaysheetRollData(decimal[] laysheetpkarry)
    {


            var q = (from laydet in db.LaySheetDetails
                     where laysheetpkarry.Contains(laydet.LaySheet_PK ?? 0) && !(from layshortdet in db.LayShortageDetails  where layshortdet.LaySheetDet_PK==laydet.LaySheetDet_PK select layshortdet.Roll_PK).Contains(laydet.Roll_PK)
                     select new ApprovelaysheetModel
                     {


                         LaySheetDet_PK = laydet.LaySheetDet_PK,
                         IsSelected = false,
                         LaySheet_PK = laydet.LaySheet_PK??0,
                         LayCutNum = laydet.LaySheetMaster.LaySheetNum,
                         RollNum = laydet.FabricRollmaster.RollNum,
       NoOfPlies=laydet.NoOfPlies??0,
       FabUtilized=laydet.FabUtilized??0,
       EndBit=laydet.EndBit??0,
       BalToCut=laydet.BalToCut??0,
       ExcessOrShort=laydet.ExcessOrShort??0,
       IsRecuttable=laydet.IsRecuttable,
       Roll_PK=laydet.Roll_PK??0,
       ShadeGroup= laydet.FabricRollmaster.RollNum,
                         ShrinkageGroup = laydet.FabricRollmaster.ShrinkageGroup,
                         WidthGroup =laydet.FabricRollmaster.WidthGroup,
                         MarkerType = laydet.FabricRollmaster.MarkerType,





                     }).ToList();



   








            return q;
    }



      public String InsertLaysheetShortageRoll(LaySheetShortageViewModel model)
        {
            String msg = "";


            LayShortageReqMaster layreqmstr = new DataModels.LayShortageReqMaster();
            layreqmstr.AtcID = model.AtcID;
            layreqmstr.AddedBY = model.AddedBy;
            layreqmstr.AddedDate = model.AddedDate;
            layreqmstr.Type = model.Type;
            layreqmstr.IsEndBit = false;
            layreqmstr.IsLayShortage = model.IsLayShortage;
            layreqmstr.IsApproved = false;
            layreqmstr.SkuDet_PK = model.SkuID;
            db.LayShortageReqMasters.Add(layreqmstr);

            db.SaveChanges();
            msg= layreqmstr.LayShortageReqCode= "LSH" + layreqmstr.LayShortageMasterID.ToString().PadLeft(6, '0');
            foreach (ApprovelaysheetModel rollmodel in model.RollDetails)
            {

               

                LayShortageDetail layrolldet = new DataModels.LayShortageDetail();
                layrolldet.Roll_PK = rollmodel.Roll_PK;
                layrolldet.EndBit = rollmodel.EndBit;
                layrolldet.LaySheetDet_PK = rollmodel.LaySheetDet_PK;
                layrolldet.ExcessOrShort = rollmodel.ExcessOrShort;
                layrolldet.AddedBy = model.AddedBy;
                layrolldet.LayShortageMasterID = layreqmstr.LayShortageMasterID;
                layrolldet.AddedDate = model.AddedDate;
                db.LayShortageDetails.Add(layrolldet);
            }

            db.SaveChanges();
            




            return msg;
        }



      public string InsertCutOrderAdjust(LayShortageCutorderAdjustmentViewModal model)
        {
            String msg = "";
            int cutplanpk = 0;
            decimal newconsumption = 0;
            LayAdjustDetail layadjstdet = new DataModels.LayAdjustDetail();
            layadjstdet.CutID = model.CutID;
            layadjstdet.LayShortageMasterID = model.LayShortageMasterID;
            layadjstdet.AddedDate = model.AddedDate;
            layadjstdet.AddedBy = model.AddedBy;
            layadjstdet.Qty = model.ToAddQty;
        
            db.LayAdjustDetails.Add(layadjstdet);


            var q = from cutord in db.CutOrderMasters
                    where cutord.CutID == model.CutID
                    select cutord;
                 foreach(var element in q)
            {
                element.FabQty = element.FabQty + model.ToAddQty;
             
                element.ActualConsumption =  element.FabQty / element.CutQty;
                newconsumption = decimal.Parse(element.ActualConsumption.ToString());
                cutplanpk =int.Parse ( element.CutPlan_Pk.ToString());
            }

            var q1=from cutplnmstr in db.CutPlanMasters
                   where cutplnmstr.CutPlan_PK == cutplanpk
                   select cutplnmstr;
            foreach (var element in q1)
            {
                element.CutOrderConsumption = newconsumption;
             
            }


            db.SaveChanges();
          
            
            




            return msg;
        }


        public string InsertRejectionCutOrderAdjust(RejectionCutorderAdjustmentViewModal model)
        {
            String msg = "";
            int cutplanpk = 0;
            decimal newconsumption = 0;
            RejectionAdjustDetail layadjstdet = new DataModels.RejectionAdjustDetail();
            layadjstdet.CutID = model.CutID;
            layadjstdet.RejReqMasterIDID = model.LayShortageMasterID;
            layadjstdet.AddedDate = model.AddedDate;
            layadjstdet.AddedBy = model.AddedBy;
            layadjstdet.Qty = model.ToAddQty;

            db.RejectionAdjustDetails.Add(layadjstdet);


            var q = from cutord in db.CutOrderMasters
                    where cutord.CutID == model.CutID
                    select cutord;
            foreach (var element in q)
            {
                element.FabQty = element.FabQty + model.ToAddQty;
                element.ActualConsumption = element.FabQty / element.CutQty;
                cutplanpk = int.Parse(element.CutPlan_Pk.ToString());
                newconsumption = decimal.Parse(element.ActualConsumption.ToString());
            }
            var q1 = from cutplnmstr in db.CutPlanMasters
                     where cutplnmstr.CutPlan_PK == cutplanpk
                     select cutplnmstr;
            foreach (var element in q1)
            {
                element.CutOrderConsumption = newconsumption;

            }
            db.SaveChanges();







            return msg;
        }





        public LayShortageCutorderAdjustmentViewModal PopulateLayRequestModel(int CutID = 0, int LayShortageMasterID = 0)
        {
            LayShortageCutorderAdjustmentViewModal model = new LayShortageCutorderAdjustmentViewModal();
          
          
            
            var requestqtyvar = db.LayShortageDetails.Where(u => u.LayShortageMasterID == LayShortageMasterID).Sum(u => u.ExcessOrShort);
            Decimal requestqty = Decimal.Parse( requestqtyvar.ToString());
            model.RequestQty = requestqty;

            var alreadyallocated = db.LayAdjustDetails.Where(U => U.LayShortageMasterID == LayShortageMasterID).Sum(U => U.Qty);
            if (alreadyallocated != null) { model.AllocatedQty = Decimal.Parse(alreadyallocated.ToString()); } else { model.AllocatedQty = 0; };
            model.BalanceQty = model.RequestQty - model.AllocatedQty;

            model.ToAddQty = model.BalanceQty;




            var q = from cutorder in db.CutOrderMasters
                    where cutorder.CutID == CutID
                    select new { cutorder.FabQty, cutorder.MarkerType, cutorder.Shrinkage, cutorder.CutWidth, cutorder.CutQty };
            foreach (var element in q)
            {
                model.CutOrderQty = element.FabQty;
                model.MarkerType = element.MarkerType;
                model.Shrinkage = element.Shrinkage;
                model.CutWidth = element.CutWidth;
                model.CutQty = element.CutQty.ToString();
            }

            var DeliveryQty = db.CutOrderDOes.Where(U => U.CutID == CutID).Sum(U => U.DeliveryQty);

            if (DeliveryQty != null) { model.DeliveredQty = DeliveryQty.ToString(); } else { model.DeliveredQty = "0"; };
       

           
    
          

            return model;


            

        }










    }


    public class Rejectionfabricrepository
    {
        ArtEntitiesnew db = new ArtEntitiesnew();

        public List<FabreqDetails> GetRejectionpanelrequestdata(int  ourstyleid, int locationid,String ReqID)
        {


            var q = (from laydet in db.RejectionPanelExtraFabbReqs
                     where laydet.POPackDetail.OurStyleID ==ourstyleid && laydet.Location_PK == locationid && laydet.Fabreqno == ReqID
                     && !(from layshortdet in db.RejectReqDetails where layshortdet.RejectionType=="P"  && layshortdet.Location_PK== locationid select layshortdet.RejFabReqID).Contains(laydet.RejFabPanelReqID)
                     select new FabreqDetails
                     {


                         RejFabPanelReqID = laydet.RejFabPanelReqID,
                         IsSelected = false,
                         Fabreqno = laydet.Fabreqno ,
                         Reqdate = laydet.Reqdate,
                         DepartmentName = laydet.DepartmentName,
                         ReqQty = laydet.ReqQty ?? 0,
                         ColorName = laydet.POPackDetail.ColorCode ,
                         OurStyle = laydet.POPackDetail.AtcDetail.OurStyle ,
                         LocationName = laydet.LocationMaster.LocationName ,
                         Allowedfabric = 0,
                         





                     }).ToList();












            return q;
        }


        public RejectionCutorderAdjustmentViewModal PopulateRejectionRequestModel(int CutID = 0, int rejionid = 0)
        {
            RejectionCutorderAdjustmentViewModal model = new RejectionCutorderAdjustmentViewModal();



            var requestqtyvar = db.RejectReqDetails.Where(u => u.RejReqMasterID == rejionid).Sum(u => u.AllowedQty);
            Decimal requestqty = Decimal.Parse(requestqtyvar.ToString());
            model.RequestQty = requestqty;

            var alreadyallocated = db.RejectionAdjustDetails.Where(U => U.RejReqMasterIDID == rejionid).Sum(U => U.Qty);
            if (alreadyallocated != null) { model.AllocatedQty = Decimal.Parse(alreadyallocated.ToString()); } else { model.AllocatedQty = 0; };

            model.BalanceQty = model.RequestQty - model.AllocatedQty;

            model.ToAddQty = model.BalanceQty;




            var q = from cutorder in db.CutOrderMasters
                    where cutorder.CutID == CutID
                    select new { cutorder.FabQty, cutorder.MarkerType, cutorder.Shrinkage, cutorder.CutWidth, cutorder.CutQty };
            foreach (var element in q)
            {
                model.CutOrderQty = element.FabQty;
                model.MarkerType = element.MarkerType;
                model.Shrinkage = element.Shrinkage;
                model.CutWidth = element.CutWidth;
                model.CutQty = element.CutQty.ToString();
            }

            var DeliveryQty = db.CutOrderDOes.Where(U => U.CutID == CutID).Sum(U => U.DeliveryQty);

            if (DeliveryQty != null) { model.DeliveredQty = DeliveryQty.ToString(); } else { model.DeliveredQty = "0"; };






            return model;




        }

        public String InsertLaysheetShortageRoll(RejectionPanelViewModal model)
        {
            String msg = "";


            RejectReqMaster lsmstr = new RejectReqMaster();
            lsmstr.AtcID = model.AtcID;
            lsmstr.Location_PK = model.LocationID;


            lsmstr.AddedBY = HttpContext.Current.Session["Username"].ToString();
            lsmstr.AddedDate = DateTime.Now;
            lsmstr.IsAdjusted = false;
            lsmstr.RejectionType = "P";
            db.RejectReqMasters.Add(lsmstr);
            db.SaveChanges();
            msg = "PR" + lsmstr.RejReqMasterID;
            lsmstr.Reqnum = msg;




            db.SaveChanges();





            foreach (FabreqDetails di in model.FabreqDetails)
            {
                RejectReqDetail lcdet = new RejectReqDetail();
                lcdet.RejFabReqID = di.RejFabPanelReqID;
                lcdet.AllowedQty = di.Allowedfabric;
                lcdet.RejReqMasterID = lsmstr.RejReqMasterID;
                lcdet.RejectionType = "P";
                lcdet.Location_PK = model.LocationID;
                lcdet.FabreqNum = di.Fabreqno;
                db.RejectReqDetails.Add(lcdet);



                var qlayroll = from rlldata in db.RejectionPanelExtraFabbReqs
                               where rlldata.RejFabPanelReqID == di.RejFabPanelReqID
                               select rlldata;
                foreach (var element1 in qlayroll)
                {
                    element1.IsApproved = true;
                }





            }


            db.SaveChanges();





            return msg;
        }


    }

    public class SubConfabricrepository
    {
        ArtEntitiesnew db = new ArtEntitiesnew();

        public String InsertSubConShortage(int  id)
        {
            String msg = "";
            int cutplanpk = 0;
            decimal newconsumption = 0;
            SubConExtraRequest subConExtraRequest = db.SubConExtraRequests.Find(id);
            subConExtraRequest.IsApproved = "Y";
            subConExtraRequest.ApprovedDate= DateTime.Now;
            subConExtraRequest.ApprovedBy = HttpContext.Current.Session["Username"].ToString();

            SubConAdjustDetail lsmstr = new SubConAdjustDetail();
            lsmstr.CutID = subConExtraRequest.CutOrderID;
            lsmstr.Qty = subConExtraRequest.RequestQty;
            lsmstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
            lsmstr.AddedDate = DateTime.Now;
            db.SubConAdjustDetails.Add(lsmstr);
            db.SaveChanges();
            var q = from cutord in db.CutOrderMasters
                    where cutord.CutID == subConExtraRequest.CutOrderID
                    select cutord;
            foreach (var element in q)
            {
                element.FabQty = element.FabQty + lsmstr.Qty;
                element.ActualConsumption = element.FabQty / element.CutQty;
                cutplanpk = int.Parse(element.CutPlan_Pk.ToString());
                newconsumption = decimal.Parse(element.ActualConsumption.ToString());
            }
            var q1 = from cutplnmstr in db.CutPlanMasters
                     where cutplnmstr.CutPlan_PK == cutplanpk
                     select cutplnmstr;
            foreach (var element in q1)
            {
                element.CutOrderConsumption = newconsumption;

            }


            db.SaveChanges();











            return msg;
        }


    }

    public static class  ComboRepository
    {
        /// <summary>
        /// get the fabric and Skudetpk of the same of a atc
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public static SelectList fillFabColorofAtc(int atcid)
        {
            DataTable dt = DBTransaction.Productiontransaction.CutPlanTransaction.GetfabricofATC(atcid);

            SelectList skulist  = Areas.MVCControls.DataTabletoSelectList("SkuDet_PK", "ItemDescription", dt, "Select fabric");

            return skulist;

        }

    }


}