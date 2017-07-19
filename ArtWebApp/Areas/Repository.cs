using ArtWebApp.Areas.ArtMVC.Models.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas
{
    public class Repository
    {
    }

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
                int Spodetail_PK = int.Parse(element.smrndet_pk.ToString());




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


}