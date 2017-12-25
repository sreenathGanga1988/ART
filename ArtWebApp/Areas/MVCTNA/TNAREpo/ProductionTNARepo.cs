using ArtWebApp.Areas.MVCTNA.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.MVCTNA.TNAREpo
{
   
    public class ProductionTNARepo
    {
        ArtEntitiesnew db = new ArtEntitiesnew();

        public List<ProductionTNAVModel> GetProductionTNAData(int? atcid)
        {
            List<ProductionTNAVModel> ProductionTNAVModelList = new List<ProductionTNAVModel> ();
           using (SqlCommand cmd= new SqlCommand())
           {
                cmd.CommandText = @"ProductionTNAofAtc_SP";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AtcId", atcid);

                DataTable dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

                if(dt != null){
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            

                            ProductionTNAVModel productionTNAVModel = new ProductionTNAVModel();


                            productionTNAVModel.OurStyleID = Decimal.Parse( row["OurStyleID"].ToString());
                            try
                            {
                                productionTNAVModel.Location_PK = Decimal.Parse(row["ExpectedLocation_PK"].ToString());
                            }
                            catch (Exception)
                            {


                                productionTNAVModel.Location_PK =0;
                            }
                            productionTNAVModel.PCD = DateTime.Parse( row["MerchantPCD"].ToString());
                            productionTNAVModel.LocationName = row["LocationName"].ToString();
                            productionTNAVModel.OurStyle = row["OurStyle"].ToString();
                            productionTNAVModel.BuyerStyle = row["BuyerStyle"].ToString();
                            productionTNAVModel.AtcNum = row["AtcNum"].ToString();
                            productionTNAVModel.AtcID = int.Parse (row["AtcId"].ToString());
                            productionTNAVModel.ShortName = row["ShortName"].ToString();
                            productionTNAVModel= calculateProductionTNA(productionTNAVModel);
                            ProductionTNAVModelList.Add(productionTNAVModel);

                            
                        }
                       















                    }
                }




            }

            return ProductionTNAVModelList;
        }




        public ProductionTNAVModel  calculateProductionTNA(ProductionTNAVModel productionTNAVModel)
        {

            if (productionTNAVModel.Location_PK != 0)
            {


                productionTNAVModel = SelectproductionTNABasedonMaster(productionTNAVModel);




            }



            return productionTNAVModel;


        }


        public String CalculateFactoryPCD( int ourstyleid ,int locationpk,String merchantpcd)
        {
            String FactoryPCD = merchantpcd;

            try
            {
                var FactoryPCDdata = db.ProductionTNADetails.Where(u => u.OurStyleID == ourstyleid && u.Location_PK == locationpk && u.ProductionTNACompID == 15).First();
                FactoryPCD = DateTime.Parse( FactoryPCDdata.MarkedDate.ToString()).ToString("MMMM dd,yyyy");
            }
            catch (Exception)
            {

                FactoryPCD = merchantpcd;
            }

            return FactoryPCD;

        }

        public ProductionTNAVModel  SelectproductionTNABasedonMaster(ProductionTNAVModel productionTNAVModel)
        {

            

            int alloweddays = 0;
            int alertdays = 0;

            productionTNAVModel.FACTORYPLANNEDPCD = CalculateFactoryPCD(int.Parse( productionTNAVModel.OurStyleID.ToString()), int.Parse(productionTNAVModel.Location_PK.ToString()), productionTNAVModel.PCD.ToString());

            var ActualDates = db.ProductionTNADetails.Where(u => u.OurStyleID == productionTNAVModel.OurStyleID).ToList();
            var q = (from productiontnacomp in db.ProductionTNAComponents
                    select productiontnacomp).ToList();

            foreach (var element in q)
            {
                String actualdate = "";

                try
                {
                    var q1 = ActualDates.Where(u => u.ProductionTNACompID == element.ProductionTNACompID).Select(u => u.MarkedDate).Max();
                    actualdate = q1.ToString();
                }
                catch (Exception)
                {
                    actualdate = "";

                }

                if (actualdate == null)
                {
                    actualdate = "";
                }


                if (element.CompName.Trim() == "PCD")
                {
                  alloweddays = int.Parse(element.DaystoAdjust.ToString());
                  alertdays= int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PCD = productionTNAVModel.PCD.AddDays(alloweddays);
                       

                    

                }
                else if (element.CompName.Trim() == "FINAL MARKER")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.FINALMARKER = DateTime.Parse(productionTNAVModel.PCD.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdFINALMARKER = int.Parse( element.ProductionTNACompID.ToString());

                    productionTNAVModel.ActualFINALMARKER = actualdate;
                }
                else if (element.CompName.Trim() == "FC1")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.FC1 = DateTime.Parse(productionTNAVModel.FINALMARKER.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdFC1 = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualFC1 = actualdate;
                }
                else if (element.CompName.Trim() == "PP MEETING")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PPMEETING = DateTime.Parse(productionTNAVModel.FC1.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdPPMEETING = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualPPMEETING = actualdate;
                }
                else if (element.CompName.Trim() == "SIZE SET")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.SIZESET = DateTime.Parse(productionTNAVModel.PPMEETING.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy"); ;
                    productionTNAVModel.IdSIZESET = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualSIZESET = actualdate;
                }
                else if (element.CompName.Trim() == "SEWING TRIM")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.SEWINGTRIM = DateTime.Parse(productionTNAVModel.SIZESET.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdSEWINGTRIM = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualSEWINGTRIM = actualdate;
                }
                else if (element.CompName.Trim() == "BULK FABRIC")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.BULKFABRIC = DateTime.Parse(productionTNAVModel.SEWINGTRIM.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdBULKFABRIC = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualBULKFABRIC = actualdate;
                }
                else if (element.CompName.Trim() == "RECEIPT OF ORGINAL DOCUMENT")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.RECEIPTOFORGINALDOCUMENT = DateTime.Parse(productionTNAVModel.BULKFABRIC.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdRECEIPTOFORGINALDOCUMENT = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualRECEIPTOFORGINALDOCUMENT = actualdate;
                }
                else if (element.CompName.Trim() == "GRADDED PATTERN")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.GRADDEDPATTERN = DateTime.Parse(productionTNAVModel.BULKFABRIC.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdGRADDEDPATTERN = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualGRADDEDPATTERN = actualdate;
                }
                else if (element.CompName.Trim() == "SAMPLE YARDAGES")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.SAMPLEYARDAGES = DateTime.Parse(productionTNAVModel.GRADDEDPATTERN.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdSAMPLEYARDAGES = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualSAMPLEYARDAGES = actualdate;
                }
                else if (element.CompName.Trim() == "PP APPROVAL")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PPAPPROVAL = DateTime.Parse(productionTNAVModel.SAMPLEYARDAGES.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdPPAPPROVAL = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualPPAPPROVAL = actualdate;
                }
                else if (element.CompName.Trim() == "PP SUBMISSION DATE MERCHANT")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PPSUBMISSIONDATEMERCHANT = DateTime.Parse(productionTNAVModel.PPAPPROVAL.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdPPSUBMISSIONDATEMERCHANT = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualPPSUBMISSIONDATEMERCHANT = actualdate;
                }
              

                else if (element.CompName.Trim() == "INPUT")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.INPUT = DateTime.Parse(productionTNAVModel.PCD.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdINPUT = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualINPUT = actualdate;
                }
                else if (element.CompName.Trim() == "PACKING TRIMS")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PACKINGTRIMS = DateTime.Parse(productionTNAVModel.PCD.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdPACKINGTRIMS = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualPACKINGTRIMS = actualdate;
                }
                else if (element.CompName.Trim() == " FACTORY PLANNED PCD")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                }
            }
          






            return productionTNAVModel;

        }


       

    }
}