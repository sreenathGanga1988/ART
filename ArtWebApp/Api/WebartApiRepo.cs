using ArtWebApp.Areas.MVCTNA.TNAREpo;
using ArtWebApp.Areas.MVCTNA.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.Api
{
    public  static class WebartApiRepo
    {


        public static List<ArtTaskModel> GetPendingTask(int id)
        {

            ProductionTNAVModelMaster model = new ProductionTNAVModelMaster();
            ProductionTNARepo productionTNARepo = new ProductionTNARepo();
            DateTime tempdate = DateTime.Now.Date;
            tempdate = tempdate.AddDays(30);
            model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAData(tempdate, tempdate, 0);

            List<ArtTaskModel> ArtTaskModels = new List<ArtTaskModel>();

            try
            {
                #region Final marker
                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDdFINALMARKER == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_dFINALMARKER == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_dFINALMARKER == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "Final Marker";
                    ArtTaskModels.Add(ArtTaskModel);

                }
                #endregion




            }
            catch (Exception)
            {
              
            }

            try
            {
                #region FC1


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDFC1 == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_FC1 == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_FC1 == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "FC1";
                    ArtTaskModels.Add(ArtTaskModel);

                }


                #endregion

            }
            catch (Exception)
            {
               
            }
            try
            {



                #region PPMeeting


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDPPMEETING == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_PPMEETING == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_PPMEETING == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "PP Meeting";
                    ArtTaskModels.Add(ArtTaskModel);

                }




#endregion


            }
            catch (Exception)
            {
               

            }
            try
            {
               




                #region Size Set


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDSIZESET == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_SIZESET == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_SIZESET == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "Size Set";
                    ArtTaskModels.Add(ArtTaskModel);

                }




                #endregion
            }
            catch (Exception)
            {

               
            }


            try
            {
              





                #region Sewing Trim


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDSEWINGTRIM == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_SEWINGTRIM == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_SEWINGTRIM == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "Sewing trim";
                    ArtTaskModels.Add(ArtTaskModel);

                }




                #endregion





            }
            catch (Exception)
            {


              
            }

            try
            {
             


                #region Bulk Fabric


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDBULKFABRIC == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_BULKFABRIC == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_BULKFABRIC == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "Bulk Fabric";
                    ArtTaskModels.Add(ArtTaskModel);

                }




                #endregion


            }
            catch (Exception)
            {

              
            }
            try
            {
               




                #region RECEIPT OF ORGINAL DOCUMENT


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDRECEIPTOFORGINALDOCUMENT == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_RECEIPTOFORGINALDOCUMENT == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.RECEIPTOFORGINALDOCUMENT == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "Orginal Document";
                    ArtTaskModels.Add(ArtTaskModel);

                }




                #endregion


            }
            catch (Exception)
            {

                
            }
            try
            {
                



                #region Gradeed pattern


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDGRADDEDPATTERN == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_GRADDEDPATTERN == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_GRADDEDPATTERN == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "Gradded Pattern";
                    ArtTaskModels.Add(ArtTaskModel);

                }




                #endregion






            }
            catch (Exception)
            {


               


            }
            try
            {
            





                #region Sample Yard


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDSAMPLEYARDAGES == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_SAMPLEYARDAGES == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_SAMPLEYARDAGES == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "Sample Yard";
                    ArtTaskModels.Add(ArtTaskModel);

                }




                #endregion







            }
            catch (Exception)
            {


               
            }
            try
            {
              




                #region PP Approval


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDPPAPPROVAL == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_PPAPPROVAL == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_PPAPPROVAL == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "PP Approval";
                    ArtTaskModels.Add(ArtTaskModel);

                }




                #endregion






            }
            catch (Exception)
            {


              
            }
            try
            {
                


                #region PP Submission


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDPPSUBMISSIONDATEMERCHANT == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_PPSUBMISSIONDATEMERCHANT == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_PPSUBMISSIONDATEMERCHANT == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "PP Submission";
                    ArtTaskModels.Add(ArtTaskModel);

                }




                #endregion





            }
            catch (Exception)
            {


              
            }
            try
            {
               








                #region Input Date


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDINPUT == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_INPUT == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_INPUT == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "Input";
                    ArtTaskModels.Add(ArtTaskModel);

                }




                #endregion
            }
            catch (Exception)
            {


               
            }



            try
            {
               





                #region Packing trim 


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDPACKINGTRIMS == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_PACKINGTRIMS == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_PACKINGTRIMS == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "Packing trim";
                    ArtTaskModels.Add(ArtTaskModel);

                }




                #endregion






            }
            catch (Exception)
            {


               
            }
            
            try
            {
              




                #region System File 


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDSYSTEMFILES == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_SYSTEMFILES == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_SYSTEMFILES == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "System File";
                    ArtTaskModels.Add(ArtTaskModel);

                }




                #endregion




            }
            catch (Exception)
            {


              
            }
            try
            {
            



                #region Shrinkage


                var LightCoralstatuscount = 0;
                var Orangestatuscount = 0;
                List<ProductionTNAVModel> Bulkfabriclist = model.ProductionTNAVModelList.Where(u => u.User_IDSHRINKAGE == id).ToList();
                try
                {
                    LightCoralstatuscount = Bulkfabriclist.Where(u => u.status_SHRINKAGE == "LightCoral").Count();
                }
                catch (Exception)
                {
                    LightCoralstatuscount = 0;
                }

                try
                {
                    Orangestatuscount = Bulkfabriclist.Where(u => u.status_SHRINKAGE == "Orange").Count();
                }
                catch (Exception)
                {
                    Orangestatuscount = 0;
                }

                if (LightCoralstatuscount != 0 || Orangestatuscount != 0)
                {
                    ArtTaskModel ArtTaskModel = new ArtTaskModel();
                    ArtTaskModel.OrangeStatus = Orangestatuscount.ToString();
                    ArtTaskModel.LightColorStatus = LightCoralstatuscount.ToString();
                    ArtTaskModel.TaskName = "Shrinkage";
                    ArtTaskModels.Add(ArtTaskModel);

                }




                #endregion







            }
            catch (Exception)
            {

              
            }






            return ArtTaskModels;

        }





        public static List<ArtTaskModelLoccation> GetPendingTask()
        {

            ProductionTNAVModelMaster model = new ProductionTNAVModelMaster();
            ProductionTNARepo productionTNARepo = new ProductionTNARepo();
            DateTime tempdate = DateTime.Now.Date;
            tempdate = tempdate.AddDays(30);
            model.ProductionTNAVModelList = productionTNARepo.GetProductionTNAData(tempdate, tempdate, 0);

            List<ArtTaskModelLoccation> ArtTaskModelLoccations = new List<ArtTaskModelLoccation>();





var idlist = model.ProductionTNAVModelList.Select(i => new { i.Location_PK, i.OurStyleID }).Distinct();

            DeleteRow();






            foreach (var element in idlist)
            {

                int id = int.Parse(element.Location_PK.ToString());
                int OurStyleID = int.Parse(element.OurStyleID.ToString());


                List<ProductionTNAVModel> pendinglist = model.ProductionTNAVModelList.Where(u => u.Location_PK == id && u.OurStyleID== OurStyleID).ToList();
                try
                {
                    #region Final marker
                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
                  

                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_dFINALMARKER == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }

                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_dFINALMARKER == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }

                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_dFINALMARKER == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }

                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "Final Marker";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id;
                        ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);

                    }
                    #endregion




                }
                catch (Exception)
                {

                }
                try
                {
                    #region FC1


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
                
                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_FC1 == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_FC1 == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_FC1 == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }

                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "FC1";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id;
                        ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);
                    }


                    #endregion

                }
                catch (Exception)
                {

                }
                try
                {



                    #region PPMeeting


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
                    
                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_PPMEETING == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_PPMEETING == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_PPMEETING == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }

                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "PP Meeting";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id; ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);
                    }




                    #endregion


                }
                catch (Exception)
                {


                }
                try
                {





                    #region Size Set


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
                  
                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_SIZESET == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }

                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_SIZESET == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_SIZESET == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "Size Set";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id; ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);
                    }




                    #endregion
                }
                catch (Exception)
                {


                }
                try
                {






                    #region Sewing Trim


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
                  
                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_SEWINGTRIM == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_SEWINGTRIM == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_SEWINGTRIM == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }

                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "Sewing trim";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id; ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);
                    }




                    #endregion





                }
                catch (Exception)
                {



                }
                try
                {



                    #region Bulk Fabric


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;

                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_BULKFABRIC == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }

                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_BULKFABRIC == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_BULKFABRIC == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "Bulk Fabric";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id; ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);
                    }




                    #endregion


                }
                catch (Exception)
                {


                }
                try
                {





                    #region RECEIPT OF ORGINAL DOCUMENT


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
                
                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_RECEIPTOFORGINALDOCUMENT == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_RECEIPTOFORGINALDOCUMENT == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.RECEIPTOFORGINALDOCUMENT == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }

                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "Orginal Document";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id; ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);
                    }




                    #endregion


                }
                catch (Exception)
                {


                }
                try
                {




                    #region Gradeed pattern


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
                   
                   
                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_GRADDEDPATTERN == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_GRADDEDPATTERN == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_GRADDEDPATTERN == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }

                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "Gradded Pattern";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id; ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);
                    }




                    #endregion






                }
                catch (Exception)
                {





                }
                try
                {






                    #region Sample Yard


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
    
                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_SAMPLEYARDAGES == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }

                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_SAMPLEYARDAGES == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_SAMPLEYARDAGES == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "Sample Yard";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id; ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);
                    }




                    #endregion







                }
                catch (Exception)
                {



                }
                try
                {





                    #region PP Approval


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
                   
                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_PPAPPROVAL == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_PPAPPROVAL == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_PPAPPROVAL == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }

                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "PP Approval";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id; ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);
                    }




                    #endregion






                }
                catch (Exception)
                {



                }
                try
                {



                    #region PP Submission


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
                 
                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_PPSUBMISSIONDATEMERCHANT == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_PPSUBMISSIONDATEMERCHANT == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_PPSUBMISSIONDATEMERCHANT == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }

                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "PP Submission";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id;
                        ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);
                    }




                    #endregion





                }
                catch (Exception)
                {



                }
                try
                {









                    #region Input Date


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
                   
                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_INPUT == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_INPUT == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_INPUT == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }

                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "Input";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id;
                        ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);
                    }




                    #endregion
                }
                catch (Exception)
                {



                }
                try
                {






                    #region Packing trim 


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
                   
                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_PACKINGTRIMS == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }

                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_PACKINGTRIMS == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_PACKINGTRIMS == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "Packing trim";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id;
                        ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);
                    }




                    #endregion






                }
                catch (Exception)
                {



                }
                try
                {





                    #region System File 


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
                  
                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_SYSTEMFILES == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }

                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_SYSTEMFILES == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_SYSTEMFILES == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "System File";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id;
                        ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);
                        UpdateData(ArtTaskModelLoccation);
                    }




                    #endregion




                }
                catch (Exception)
                {



                }
                try
                {




                    #region Shrinkage


                    var LightCoralstatuscount = 0;
                    var Orangestatuscount = 0;
                    
                    try
                    {
                        LightCoralstatuscount = pendinglist.Where(u => u.status_SHRINKAGE == "LightCoral").Count();
                    }
                    catch (Exception)
                    {
                        LightCoralstatuscount = 0;
                    }

                    try
                    {
                        Orangestatuscount = pendinglist.Where(u => u.status_SHRINKAGE == "Orange").Count();
                    }
                    catch (Exception)
                    {
                        Orangestatuscount = 0;
                    }
                    var Greenstatuscount = 0;
                    try
                    {
                        Greenstatuscount = pendinglist.Where(u => u.status_SHRINKAGE == "greenyellow").Count();
                    }
                    catch (Exception)
                    {
                        Greenstatuscount = 0;
                    }
                    if (LightCoralstatuscount != 0 || Orangestatuscount != 0|| Greenstatuscount != 0)
                    {
                        ArtTaskModelLoccation ArtTaskModelLoccation = new ArtTaskModelLoccation();
                        ArtTaskModelLoccation.OrangeStatus = Orangestatuscount.ToString();
                        ArtTaskModelLoccation.LightColorStatus = LightCoralstatuscount.ToString();
                        ArtTaskModelLoccation.TaskName = "Shrinkage";
                        ArtTaskModelLoccation.GreenStatus = Greenstatuscount.ToString();
                        ArtTaskModelLoccation.Location_pk = id;
                        ArtTaskModelLoccation.OurStyleID = OurStyleID;
                        ArtTaskModelLoccations.Add(ArtTaskModelLoccation);

                        UpdateData(ArtTaskModelLoccation);



                    }




                    #endregion







                }
                catch (Exception)
                {


                }

            }


           






            return ArtTaskModelLoccations;

        }


        public static void UpdateData(ArtTaskModelLoccation artTaskModelLoccation)
        {
            using (ArtWebApp.DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {
                ProductionTNAPending productionTNAPending = new ProductionTNAPending();
                productionTNAPending.CompName = artTaskModelLoccation.TaskName;
                productionTNAPending.Green = Decimal.Parse(artTaskModelLoccation.GreenStatus);
                productionTNAPending.Red = Decimal.Parse(artTaskModelLoccation.LightColorStatus);
                productionTNAPending.Yellow = Decimal.Parse(artTaskModelLoccation.OrangeStatus);
                productionTNAPending.FactoryID = artTaskModelLoccation.Location_pk;
                productionTNAPending.OurStyleID = artTaskModelLoccation.OurStyleID;
                enty.ProductionTNAPendings.Add(productionTNAPending);

                enty.SaveChanges();
            }
        }


        public static void DeleteRow()
        {
            using (ArtWebApp.DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {
                var rows = from o in enty.ProductionTNAPendings
                           select o;
                foreach (var row in rows)
                {
                    enty.ProductionTNAPendings.Remove(row);
                }
                enty.SaveChanges();
            }
        }


    }
}