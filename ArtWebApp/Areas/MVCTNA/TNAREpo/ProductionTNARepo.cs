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

        List<TnaUserRight> tnaUserrightlist = null;

        public List<ProductionTNAVModel> GetProductionTNAData(int? atcid)
        {
            List<ProductionTNAVModel> ProductionTNAVModelList = new List<ProductionTNAVModel>();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"ProductionTNAofAtc_SP";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AtcId", atcid);

                DataTable dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {


                            ProductionTNAVModel productionTNAVModel = new ProductionTNAVModel();


                            productionTNAVModel.OurStyleID = Decimal.Parse(row["OurStyleID"].ToString());
                            try
                            {
                                productionTNAVModel.Location_PK = Decimal.Parse(row["ExpectedLocation_PK"].ToString());
                            }
                            catch (Exception)
                            {


                                productionTNAVModel.Location_PK = 0;
                            }
                            productionTNAVModel.PCD = DateTime.Parse(row["MerchantPCD"].ToString());
                            productionTNAVModel.LocationName = row["LocationName"].ToString();
                            productionTNAVModel.OurStyle = row["OurStyle"].ToString();
                            productionTNAVModel.BuyerStyle = row["BuyerStyle"].ToString();
                            productionTNAVModel.AtcNum = row["AtcNum"].ToString();
                            productionTNAVModel.AtcID = int.Parse(row["AtcId"].ToString());
                            productionTNAVModel.ShortName = row["ShortName"].ToString();
                            productionTNAVModel = calculateProductionTNA(productionTNAVModel);
                            ProductionTNAVModelList.Add(productionTNAVModel);


                        }
















                    }
                }




            }

            return ProductionTNAVModelList;
        }

        public List<ProductionTNAVModel> GetProductionTNAData(DateTime fromdate, DateTime todate)
        {
            List<ProductionTNAVModel> ProductionTNAVModelList = new List<ProductionTNAVModel>();

            tnaUserrightlist = db.TnaUserRights.ToList();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"ProductionTNABetweendate_sp";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);

                DataTable dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {


                            ProductionTNAVModel productionTNAVModel = new ProductionTNAVModel();


                            productionTNAVModel.OurStyleID = Decimal.Parse(row["OurStyleID"].ToString());
                            try
                            {
                                productionTNAVModel.Location_PK = Decimal.Parse(row["ExpectedLocation_PK"].ToString());
                            }
                            catch (Exception)
                            {


                                productionTNAVModel.Location_PK = 0;
                            }
                            productionTNAVModel.PCD = DateTime.Parse(row["MerchantPCD"].ToString());
                            productionTNAVModel.LocationName = row["LocationName"].ToString();
                            productionTNAVModel.OurStyle = row["OurStyle"].ToString();
                            productionTNAVModel.BuyerStyle = row["BuyerStyle"].ToString();
                            productionTNAVModel.AtcNum = row["AtcNum"].ToString();
                            productionTNAVModel.AtcID = int.Parse(row["AtcId"].ToString());
                            productionTNAVModel.ShortName = row["ShortName"].ToString();
                            productionTNAVModel = calculateProductionTNA(productionTNAVModel);
                            ProductionTNAVModelList.Add(productionTNAVModel);


                        }
















                    }
                }




            }

            return ProductionTNAVModelList;
        }



        public ProductionTNAVModel calculateProductionTNA(ProductionTNAVModel productionTNAVModel)
        {

            if (productionTNAVModel.Location_PK != 0)
            {


                productionTNAVModel = SelectproductionTNABasedonMaster(productionTNAVModel);


                productionTNAVModel = SelectUserTNABasedonMaster(productionTNAVModel);

            }



            return productionTNAVModel;


        }


        public String CalculateFactoryPCD(int ourstyleid, int locationpk, String merchantpcd)
        {
            String FactoryPCD = merchantpcd;

            try
            {
                var FactoryPCDdata = db.ProductionTNADetails.Where(u => u.OurStyleID == ourstyleid && u.Location_PK == locationpk && u.ProductionTNACompID == 15 && u.IsDeleted == "N").First();
                FactoryPCD = DateTime.Parse(FactoryPCDdata.MarkedDate.ToString()).ToString("MMMM dd,yyyy");
            }
            catch (Exception)
            {

                FactoryPCD = merchantpcd;
            }

            return FactoryPCD;

        }
        public String CalculateStatus(String ActualDate, DateTime TnaDate , int alertdays)
        {
            String Status = "Pending";


            if(ActualDate.Trim()=="" && ActualDate!=null)
            {
                try
                {
                    int dayspending = int.Parse( (TnaDate - DateTime.Now.Date).TotalDays.ToString());
                   if(dayspending > 0)
                    {

                        if (dayspending > alertdays)
                        {

                        }
                        else
                        {
                            Status = "Orange";
                        }

                    }
                    else
                    {
                        Status = "Red";
                    }

                }
                catch (Exception)
                {
                    Status= "Pending";


                }


            }
            else
            {

                Status = "Green";

            }

            return Status;

        }
        public ProductionTNAVModel SelectproductionTNABasedonMaster(ProductionTNAVModel productionTNAVModel)
        {


            DateTime daytody = DateTime.Now.Date;
            int alloweddays = 0;
            int alertdays = 0;

            productionTNAVModel.FACTORYPLANNEDPCD = CalculateFactoryPCD(int.Parse(productionTNAVModel.OurStyleID.ToString()), int.Parse(productionTNAVModel.Location_PK.ToString()), productionTNAVModel.PCD.ToString());

            var ActualDates = db.ProductionTNADetails.Where(u => u.OurStyleID == productionTNAVModel.OurStyleID && u.IsDeleted == "N").ToList();
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
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PCD = productionTNAVModel.PCD.AddDays(alloweddays);




                }
                else if (element.CompName.Trim() == "FINAL MARKER")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.FINALMARKER = DateTime.Parse(productionTNAVModel.PCD.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdFINALMARKER = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualFINALMARKER = actualdate;
                    productionTNAVModel.status_dFINALMARKER =CalculateStatus(productionTNAVModel.ActualFINALMARKER, DateTime.Parse(productionTNAVModel.FINALMARKER.ToString()), alertdays);
                }
                else if (element.CompName.Trim() == "FC1")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.FC1 = DateTime.Parse(productionTNAVModel.FINALMARKER.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdFC1 = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualFC1 = actualdate;
                    productionTNAVModel.status_FC1 = CalculateStatus(productionTNAVModel.ActualFC1, DateTime.Parse(productionTNAVModel.FC1.ToString()), alertdays);
                
            }
                else if (element.CompName.Trim() == "PP MEETING")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PPMEETING = DateTime.Parse(productionTNAVModel.FC1.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdPPMEETING = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualPPMEETING = actualdate;
                    productionTNAVModel.status_PPMEETING = CalculateStatus(productionTNAVModel.ActualPPMEETING, DateTime.Parse(productionTNAVModel.PPMEETING.ToString()), alertdays);

                }
                else if (element.CompName.Trim() == "SIZE SET")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.SIZESET = DateTime.Parse(productionTNAVModel.PPMEETING.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy"); ;
                    productionTNAVModel.IdSIZESET = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualSIZESET = actualdate;
                    productionTNAVModel.status_SIZESET = CalculateStatus(productionTNAVModel.ActualSIZESET, 
                        DateTime.Parse(productionTNAVModel.SIZESET.ToString()), alertdays);

                }
                else if (element.CompName.Trim() == "SEWING TRIM")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.SEWINGTRIM = DateTime.Parse(productionTNAVModel.SIZESET.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdSEWINGTRIM = int.Parse(element.ProductionTNACompID.ToString());

                    productionTNAVModel.ActualSEWINGTRIM = actualdate;
                    
                         productionTNAVModel.status_SEWINGTRIM = CalculateStatus(productionTNAVModel.ActualSEWINGTRIM,
                        DateTime.Parse(productionTNAVModel.SEWINGTRIM.ToString()), alertdays);
                }
                else if (element.CompName.Trim() == "BULK FABRIC")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.BULKFABRIC = DateTime.Parse(productionTNAVModel.SEWINGTRIM.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdBULKFABRIC = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualBULKFABRIC = actualdate;

                    productionTNAVModel.status_BULKFABRIC = CalculateStatus(productionTNAVModel.ActualBULKFABRIC,
                     DateTime.Parse(productionTNAVModel.BULKFABRIC.ToString()), alertdays);
                }
                else if (element.CompName.Trim() == "RECEIPT OF ORGINAL DOCUMENT")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.RECEIPTOFORGINALDOCUMENT = DateTime.Parse(productionTNAVModel.BULKFABRIC.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdRECEIPTOFORGINALDOCUMENT = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualRECEIPTOFORGINALDOCUMENT = actualdate;

                    productionTNAVModel.status_RECEIPTOFORGINALDOCUMENT = CalculateStatus(productionTNAVModel.ActualRECEIPTOFORGINALDOCUMENT,
                     DateTime.Parse(productionTNAVModel.RECEIPTOFORGINALDOCUMENT.ToString()), alertdays);
                }
                else if (element.CompName.Trim() == "GRADDED PATTERN")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.GRADDEDPATTERN = DateTime.Parse(productionTNAVModel.BULKFABRIC.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdGRADDEDPATTERN = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualGRADDEDPATTERN = actualdate;

                    productionTNAVModel.status_GRADDEDPATTERN = CalculateStatus(productionTNAVModel.ActualGRADDEDPATTERN,
                   DateTime.Parse(productionTNAVModel.GRADDEDPATTERN.ToString()), alertdays);
                }
                else if (element.CompName.Trim() == "SAMPLE YARDAGES")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.SAMPLEYARDAGES = DateTime.Parse(productionTNAVModel.GRADDEDPATTERN.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdSAMPLEYARDAGES = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualSAMPLEYARDAGES = actualdate;

                    productionTNAVModel.status_SAMPLEYARDAGES = CalculateStatus(productionTNAVModel.ActualSAMPLEYARDAGES,
                 DateTime.Parse(productionTNAVModel.SAMPLEYARDAGES.ToString()), alertdays);
                }
                else if (element.CompName.Trim() == "PP APPROVAL")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PPAPPROVAL = DateTime.Parse(productionTNAVModel.SAMPLEYARDAGES.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdPPAPPROVAL = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualPPAPPROVAL = actualdate;

                    productionTNAVModel.status_PPAPPROVAL = CalculateStatus(productionTNAVModel.ActualPPAPPROVAL,
                 DateTime.Parse(productionTNAVModel.PPAPPROVAL.ToString()), alertdays);
                }
                else if (element.CompName.Trim() == "PP SUBMISSION DATE MERCHANT")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PPSUBMISSIONDATEMERCHANT = DateTime.Parse(productionTNAVModel.PPAPPROVAL.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdPPSUBMISSIONDATEMERCHANT = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualPPSUBMISSIONDATEMERCHANT = actualdate;

                    productionTNAVModel.status_PPSUBMISSIONDATEMERCHANT = CalculateStatus(productionTNAVModel.ActualPPSUBMISSIONDATEMERCHANT,
               DateTime.Parse(productionTNAVModel.PPSUBMISSIONDATEMERCHANT.ToString()), alertdays);
                }


                else if (element.CompName.Trim() == "INPUT")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.INPUT = DateTime.Parse(productionTNAVModel.PCD.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdINPUT = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualINPUT = actualdate;
                    productionTNAVModel.status_INPUT = CalculateStatus(productionTNAVModel.ActualINPUT,
           DateTime.Parse(productionTNAVModel.INPUT.ToString()), alertdays);
                }
                else if (element.CompName.Trim() == "PACKING TRIMS")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PACKINGTRIMS = DateTime.Parse(productionTNAVModel.PCD.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdPACKINGTRIMS = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualPACKINGTRIMS = actualdate;

                    productionTNAVModel.status_PACKINGTRIMS = CalculateStatus(productionTNAVModel.ActualPACKINGTRIMS,
          DateTime.Parse(productionTNAVModel.PACKINGTRIMS.ToString()), alertdays);
                }
                else if (element.CompName.Trim() == "SYSTEM FILES")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.SYSTEMFILES = DateTime.Parse(productionTNAVModel.PCD.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdSYSTEMFILES = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualSYSTEMFILES = actualdate;
                    productionTNAVModel.status_SYSTEMFILES = CalculateStatus(productionTNAVModel.ActualSYSTEMFILES,
      DateTime.Parse(productionTNAVModel.SYSTEMFILES.ToString()), alertdays);
                }
                else if (element.CompName.Trim() == "SHRINKAGE")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.SHRINKAGE = DateTime.Parse(productionTNAVModel.PCD.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                    productionTNAVModel.IdSHRINKAGE = int.Parse(element.ProductionTNACompID.ToString());
                    productionTNAVModel.ActualSHRINKAGE = actualdate;
                    productionTNAVModel.status_SHRINKAGE = CalculateStatus(productionTNAVModel.ActualSHRINKAGE,
    DateTime.Parse(productionTNAVModel.SHRINKAGE.ToString()), alertdays);
                }
                else if (element.CompName.Trim() == " FACTORY PLANNED PCD")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                }
            }







            return productionTNAVModel;

        }

        public ProductionTNAVModel SelectUserTNABasedonMaster(ProductionTNAVModel productionTNAVModel)
        {
            try
            {
                List<TnaUserRight> tnaUserrightlisttemp = tnaUserrightlist.Where(u => u.Location_PK == productionTNAVModel.Location_PK).ToList();
                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsBulkFabricDate == true).LastOrDefault();
                    productionTNAVModel.User_IDdFINALMARKER = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDdFINALMARKER = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                    productionTNAVModel.UsrDdFINALMARKER = "";
                }

                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsFC1Date == true).LastOrDefault();
                    productionTNAVModel.User_IDFC1 = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDFC1 = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                    productionTNAVModel.UsrDFC1 = "";
                }
                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsPPMeetingDate == true).LastOrDefault();
                    productionTNAVModel.User_IDPPMEETING = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDPPMEETING = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                    productionTNAVModel.UsrDPPMEETING = "";
                   
                }
                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsSizeSetDate == true).LastOrDefault();
                    productionTNAVModel.User_IDSIZESET = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDSIZESET = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                
                    productionTNAVModel.UsrDSIZESET = "";
                }


                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsSewingTrimDate == true).LastOrDefault();
                    productionTNAVModel.User_IDSEWINGTRIM = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDSEWINGTRIM = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                  

                    productionTNAVModel.UsrDSEWINGTRIM = "";
                }

                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsBulkFabricDate == true).LastOrDefault();
                    productionTNAVModel.User_IDBULKFABRIC = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDBULKFABRIC = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                   
                    productionTNAVModel.UsrDBULKFABRIC = "";
                }
                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsBulkFabricDate == true).LastOrDefault();
                    productionTNAVModel.User_IDRECEIPTOFORGINALDOCUMENT = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDRECEIPTOFORGINALDOCUMENT = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                   
                    productionTNAVModel.UsrDRECEIPTOFORGINALDOCUMENT = "";
                }
                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsGradedPatternDate == true).LastOrDefault();
                    productionTNAVModel.User_IDGRADDEDPATTERN = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDGRADDEDPATTERN = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                

                    productionTNAVModel.UsrDGRADDEDPATTERN = "";


                }
                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsSampleYardagesDate == true).LastOrDefault();
                    productionTNAVModel.User_IDSAMPLEYARDAGES = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDSAMPLEYARDAGES = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                

                    productionTNAVModel.UsrDSAMPLEYARDAGES = "";
                }
                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsPPApprovalDate == true).LastOrDefault();
                    productionTNAVModel.User_IDPPAPPROVAL = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDPPAPPROVAL = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                  

                    productionTNAVModel.UsrDPPAPPROVAL = "";
                }
                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsPPSumbissionDate == true).LastOrDefault();
                    productionTNAVModel.User_IDPPSUBMISSIONDATEMERCHANT = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDPPSUBMISSIONDATEMERCHANT = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                   

                    productionTNAVModel.UsrDPPSUBMISSIONDATEMERCHANT = "";
                }
                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsInputDate == true).LastOrDefault();
                    productionTNAVModel.User_IDINPUT = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDINPUT = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                   

                    productionTNAVModel.UsrDINPUT = "";
                }



                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsPackingTrimDate == true).LastOrDefault();
                    productionTNAVModel.User_IDPACKINGTRIMS = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDPACKINGTRIMS = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                 

                    productionTNAVModel.UsrDPACKINGTRIMS = "";
                }
                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsFactoryPlannedPCDDate == true).LastOrDefault();
                    productionTNAVModel.User_IDFACTORYPLANNEDPCD = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDFACTORYPLANNEDPCD = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                    

                    productionTNAVModel.UsrDFACTORYPLANNEDPCD = "";
                }
                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsSystemFile == true).LastOrDefault();
                    productionTNAVModel.User_IDSYSTEMFILES = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDSYSTEMFILES = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                    productionTNAVModel.UsrDFC1 = "";
                    productionTNAVModel.UsrDPPMEETING = "";
                    productionTNAVModel.UsrDSIZESET = "";
                    productionTNAVModel.UsrDSEWINGTRIM = "";
                    productionTNAVModel.UsrDBULKFABRIC = "";
                    productionTNAVModel.UsrDRECEIPTOFORGINALDOCUMENT = "";
                    productionTNAVModel.UsrDGRADDEDPATTERN = "";
                    productionTNAVModel.UsrDSAMPLEYARDAGES = "";
                    productionTNAVModel.UsrDPPAPPROVAL = "";
                    productionTNAVModel.UsrDPPSUBMISSIONDATEMERCHANT = "";
                    productionTNAVModel.UsrDINPUT = "";
                    productionTNAVModel.UsrDPACKINGTRIMS = "";
                    productionTNAVModel.UsrDFACTORYPLANNEDPCD = "";
                    productionTNAVModel.UsrDSYSTEMFILES = "";

                    productionTNAVModel.UsrDSYSTEMFILES = "";
                }
                try
                {
                    var q = tnaUserrightlist.Where(u => u.IsShrinkage == true).LastOrDefault();
                    productionTNAVModel.User_IDSHRINKAGE = int.Parse(q.User_PK.ToString());
                    productionTNAVModel.UsrDSHRINKAGE = q.UserMaster.UserName;
                }
                catch (Exception)
                {
                   
                    productionTNAVModel.UsrDSHRINKAGE = "";
                }




            }
            catch (Exception)
            {
                productionTNAVModel.UsrDFC1 = "";
                productionTNAVModel.UsrDPPMEETING = "";
                productionTNAVModel.UsrDSIZESET = "";
                productionTNAVModel.UsrDSEWINGTRIM = "";
                productionTNAVModel.UsrDBULKFABRIC = "";
                productionTNAVModel.UsrDRECEIPTOFORGINALDOCUMENT = "";
                productionTNAVModel.UsrDGRADDEDPATTERN = "";
                productionTNAVModel.UsrDSAMPLEYARDAGES = "";
                productionTNAVModel.UsrDPPAPPROVAL = "";
                productionTNAVModel.UsrDPPSUBMISSIONDATEMERCHANT = "";
                productionTNAVModel.UsrDINPUT = "";
                productionTNAVModel.UsrDPACKINGTRIMS = "";
                productionTNAVModel.UsrDFACTORYPLANNEDPCD = "";
                productionTNAVModel.UsrDSYSTEMFILES = "";
                productionTNAVModel.UsrDSHRINKAGE = "";
            }
          





            return productionTNAVModel;
        }

    }


}