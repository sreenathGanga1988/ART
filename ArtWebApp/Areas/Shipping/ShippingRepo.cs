using ArtWebApp.Areas.Shipping.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Shipping
{
    public class ShippingRepo
    { ArtWebApp.DataModels.ArtEntitiesnew db = new DataModels.ArtEntitiesnew();
        public ImportViewModelMaster loadIMPreport(int Importid)
        {
            ImportViewModelMaster importViewModelMaster = new ImportViewModelMaster();
            importViewModelMaster.ID = Importid;
            List<ImportViewModel> list = new List<ImportViewModel>();
            importViewModelMaster.shippingDocument = db.ShippingDocumentMasters.Find(Importid);
            DBTransaction.ReportTransactions.ShippingReportTran prdtran = new DBTransaction.ReportTransactions.ShippingReportTran();
            //  adapt.Connection.ConnectionString = Program.ConnStr;
            DataTable dt = prdtran.GetIMP(Importid);






            foreach(DataRow drow in dt.Rows)
            {
                ImportViewModel importViewModel = new ImportViewModel();
                
                    importViewModel.AtcNum = drow["AtcNum"].ToString();
                importViewModel.RMNum =drow["RMNum"].ToString();
        importViewModel.Description =drow["Description"].ToString();
        importViewModel.ItemColor =drow["ItemColor"].ToString();
        importViewModel.ItemSize =drow["ItemSize"].ToString();
        importViewModel.SupplierColor =drow["SupplierColor"].ToString();
        importViewModel.SupplierSize =drow["SupplierSize"].ToString();
        importViewModel.UomCode =drow["UomCode"].ToString();
        importViewModel.PONum =drow["PONum"].ToString();
        importViewModel.DocNum =drow["DocNum"].ToString();
        importViewModel.Qty =drow["Qty"].ToString();
        importViewModel.Donumber =drow["Donumber"].ToString();
        importViewModel.AddedBy =drow["AddedBy"].ToString();
        importViewModel.AddedDate =drow["AddedDate"].ToString();
        importViewModel.DocAddedBy =drow["DocAddedBy"].ToString();
        importViewModel.DocAddedDate =drow["DocAddedDate"].ToString();
        importViewModel.SupplierName =drow["SupplierName"].ToString();
        importViewModel.ImpDocumentNUm =drow["ImpDocumentNUm"].ToString();
        importViewModel.ShippingDet_PK = drow["ShippingDet_PK"].ToString();
         importViewModel.DocDet_Pk = drow["DocDet_Pk"].ToString();
                importViewModel.Locationname = drow["Locationname"].ToString();
                importViewModel.Location_PK = drow["LocationID"].ToString();
                importViewModel.isReceived = drow["isReceived"].ToString();
                

                importViewModel.Description = importViewModel.RMNum + " " + importViewModel.Description + " " + importViewModel.ItemColor + " " + importViewModel.ItemSize
              + " " + importViewModel.SupplierColor + " " + importViewModel.SupplierSize;



                //    try
                //{
                //    importViewModel.Location_PK = drow["Location_PK"].ToString();
                //}
                //catch (Exception)
                //{
                //    importViewModel.Location_PK = drow["LocationID"].ToString();

                //}
                list.Add(importViewModel);


    }

            importViewModelMaster.ImportViewModels = list;


            try
            {
                var pendingcount = importViewModelMaster.ImportViewModels.Where(u => u.Location_PK == "0").Count();
                importViewModelMaster.PendingCount = int.Parse(pendingcount.ToString());
            }
            catch (Exception)
            {

                importViewModelMaster.PendingCount = 0;
            }


            return importViewModelMaster;
        } 


        public void InsertShippingDocumentlocation(ImportViewModelMaster importViewModelMaster)
        {

            foreach (ImportViewModel imp in importViewModelMaster.ImportViewModels)
            {

                if (imp.IsSelected == true)
                {
                    int ShippingDet_PK= int.Parse(imp.ShippingDet_PK.ToString());
                    int DocDet_Pk = int.Parse(imp.DocDet_Pk.ToString());

                    var Q = from TEMPshippingDocumentDetailLocation in db.ShippingDocumentDetailLocations
                            where TEMPshippingDocumentDetailLocation.DocDet_Pk == DocDet_Pk && TEMPshippingDocumentDetailLocation.ShippingDet_PK == ShippingDet_PK
                            select TEMPshippingDocumentDetailLocation;
                    foreach (var element in Q)
                    {

                        element.IsDeleted = "Y";

                    }



                    ShippingDocumentDetailLocation shippingDocumentDetailLocation = new ShippingDocumentDetailLocation();
                    shippingDocumentDetailLocation.ShippingDet_PK = ShippingDet_PK;
                    shippingDocumentDetailLocation.Location_PK = Decimal.Parse(importViewModelMaster.Location_PK.ToString());
                    shippingDocumentDetailLocation.DocDet_Pk = DocDet_Pk;
                    shippingDocumentDetailLocation.AddedBy = HttpContext.Current.Session["Username"].ToString();
                    shippingDocumentDetailLocation.AddedDate = DateTime.Now;
                    shippingDocumentDetailLocation.IsDeleted ="N";
                    shippingDocumentDetailLocation.IsReceived = "N";
                    db.ShippingDocumentDetailLocations.Add(shippingDocumentDetailLocation);

                }
                
            }


            if(importViewModelMaster.PendingCount==0)
            {
                var q = from shippingDocumentMaster in db.ShippingDocumentMasters
                        where shippingDocumentMaster.ShipingDoc_PK == importViewModelMaster.ID
                        select shippingDocumentMaster;


                foreach (var element in q)
                {

                    element.IsAssigned = "Y";
                  

                }
            }
            db.SaveChanges();
        }

        public void ReceiveDocumentlocation(ImportViewModelMaster importViewModelMaster)
        {

            foreach (ImportViewModel imp in importViewModelMaster.ImportViewModels)
            {

               
                    int ShippingDet_PK = int.Parse(imp.ShippingDet_PK.ToString());
                    int DocDet_Pk = int.Parse(imp.DocDet_Pk.ToString());

                    var Q = from TEMPshippingDocumentDetailLocation in db.ShippingDocumentDetailLocations
                            where TEMPshippingDocumentDetailLocation.DocDet_Pk == DocDet_Pk && 
                            TEMPshippingDocumentDetailLocation.ShippingDet_PK == ShippingDet_PK
                             &&
                            TEMPshippingDocumentDetailLocation.IsDeleted == "Y"
                            select TEMPshippingDocumentDetailLocation;
                    foreach (var element in Q)
                    {

                    element.ReceivedBy = HttpContext.Current.Session["Username"].ToString();
                    element.ReceivedDate = DateTime.Now;
                    element.IsReceived = "N";

                }









            }
            db.SaveChanges();
        }




        public void gateIn(int id,Decimal NoPackages)
        {


            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var q = from shippingDocumentMaster in enty.ShippingDocumentMasters
                        where shippingDocumentMaster.ShipingDoc_PK == id
                        select shippingDocumentMaster;


                foreach(var element in q)
                {


                    element.IsReceived = "Y"; 
                    element.LastReceivedLocationPK = HttpContext.Current.Session["UserLoc_pk"].ToString();
                    element.IsDelivered = "N";

                }


                ShippingDocumentActionDetail shippingDocumentActionDetail = new ShippingDocumentActionDetail();
                shippingDocumentActionDetail.AddedBy = HttpContext.Current.Session["Username"].ToString();
                shippingDocumentActionDetail.AddedDate = DateTime.Now;
                shippingDocumentActionDetail.ActionType = "Receipt";
                shippingDocumentActionDetail.ShipingDoc_PK = id;
                
                shippingDocumentActionDetail.NoPackages = NoPackages.ToString();
                enty.ShippingDocumentActionDetails.Add(shippingDocumentActionDetail);

                enty.SaveChanges();


            }

        }



        public void gateOut(int id,Decimal qty,Boolean setlocation,Decimal toloc)
        {


            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var q = from shippingDocumentMaster in enty.ShippingDocumentMasters
                        where shippingDocumentMaster.ShipingDoc_PK == id
                        select shippingDocumentMaster;


                foreach (var element in q)
                {


                    element.IsReceived = "N";
                    element.LastReceivedLocationPK = HttpContext.Current.Session["UserLoc_pk"].ToString();
                    element.IsDelivered = "Y";
                    element.DeliveredPackage = qty;
                    if (setlocation==true)
                    {
                        element.ToLoc_Pk = toloc;
                    }
                    else
                    {
                        element.ToLoc_Pk = null;
                    }
                }


                ShippingDocumentActionDetail shippingDocumentActionDetail = new ShippingDocumentActionDetail();
                shippingDocumentActionDetail.AddedBy = HttpContext.Current.Session["Username"].ToString();
                shippingDocumentActionDetail.AddedDate = DateTime.Now;
                shippingDocumentActionDetail.ActionType = "Delivery";
                shippingDocumentActionDetail.NoPackages = qty.ToString();
                shippingDocumentActionDetail.ShipingDoc_PK = id;
                enty.ShippingDocumentActionDetails.Add(shippingDocumentActionDetail);

                enty.SaveChanges();


            }

        }


        public Boolean IsGateReceived(int id)
        {
            Boolean IsgateReceipt = false;

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                if (!enty.ShippingDocumentActionDetails.Any(f => f.ShipingDoc_PK == id))
                {

                    IsgateReceipt = false;


                }
                else
                {
                    IsgateReceipt = true;
                }

            }
            return IsgateReceipt;
        }


        public DataTable GetContainer(String container, String bl)
        {
            DataTable dt = new DataTable();


            using (SqlCommand cmd = new SqlCommand(@"ContainerWise_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@container", container);
                cmd.Parameters.AddWithValue("@bl", bl);

                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }





            return dt;
        }
       public DataTable GetShipmentshortclosedetails(int atcid)
        {
            DataTable dt = new DataTable();


            using (SqlCommand cmd = new SqlCommand(@"GetShipmentClosedDetails"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@atcid", atcid);

                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }





            return dt;
        }
       
    }
}