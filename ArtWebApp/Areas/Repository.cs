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
                     where laysheetpkarry.Contains(laydet.LaySheet_PK ?? 0)
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
            layreqmstr.IsEndBit = model.IsEndBIT;
            layreqmstr.IsLayShortage = model.IsLayShortage;
            layreqmstr.IsApproved = false;
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