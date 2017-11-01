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

            if (productionTNAVModel.Location_PK == 0)
            {


                productionTNAVModel = SelectproductionTNABasedonMaster(productionTNAVModel);




            }



            return productionTNAVModel;


        }




        public ProductionTNAVModel  SelectproductionTNABasedonMaster(ProductionTNAVModel productionTNAVModel)
        {

            

            int alloweddays = 0;
            int alertdays = 0;


            var q = from productiontnacomp in db.ProductionTNAComponents
                    select productiontnacomp;

            foreach (var element in q)
            {
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
                }
                else if (element.CompName.Trim() == "FC1")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.FC1 = DateTime.Parse(productionTNAVModel.FINALMARKER.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");

                }
                else if (element.CompName.Trim() == "PP MEETING")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PPMEETING = DateTime.Parse(productionTNAVModel.FC1.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");

                }
                else if (element.CompName.Trim() == "SIZE SET")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.SIZESET = DateTime.Parse(productionTNAVModel.PPMEETING.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy"); ;

                }
                else if (element.CompName.Trim() == "SEWING TRIM")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.SEWINGTRIM = DateTime.Parse(productionTNAVModel.SIZESET.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");

                }
                else if (element.CompName.Trim() == "BULK FABRIC")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.BULKFABRIC = DateTime.Parse(productionTNAVModel.SEWINGTRIM.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");

                }
                else if (element.CompName.Trim() == "RECEIPT OF ORGINAL DOCUMENT")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.RECEIPTOFORGINALDOCUMENT = DateTime.Parse(productionTNAVModel.BULKFABRIC.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                }
                else if (element.CompName.Trim() == "GRADDED PATTERN")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.GRADDEDPATTERN = DateTime.Parse(productionTNAVModel.BULKFABRIC.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");

                }
                else if (element.CompName.Trim() == "SAMPLE YARDAGES")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.SAMPLEYARDAGES = DateTime.Parse(productionTNAVModel.GRADDEDPATTERN.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");

                }
                else if (element.CompName.Trim() == "PP APPROVAL")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PPAPPROVAL = DateTime.Parse(productionTNAVModel.SAMPLEYARDAGES.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");

                }
                else if (element.CompName.Trim() == "PP SUBMISSION DATE MERCHANT")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PPSUBMISSIONDATEMERCHANT = DateTime.Parse(productionTNAVModel.PPAPPROVAL.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");

                }
              

                else if (element.CompName.Trim() == "INPUT")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.INPUT = DateTime.Parse(productionTNAVModel.PCD.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");
                }
                else if (element.CompName.Trim() == "PACKING TRIMS")
                {
                    alloweddays = int.Parse(element.DaystoAdjust.ToString());
                    alertdays = int.Parse(element.Alertdays.ToString());
                    productionTNAVModel.PACKINGTRIMS = DateTime.Parse(productionTNAVModel.PCD.ToString()).AddDays(alloweddays).ToString("MMMM dd,yyyy");

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