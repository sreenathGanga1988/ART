using ArtWebApp.Areas.ProductionMVC.Viewmodel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.ProductionMVC
{
    public class ProductionRepo
    {


        public AsqTrackerModel GetProductionTNAData()
        {
            AsqTrackerModel asqTrackerModel = new AsqTrackerModel();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"ASQTracker_SP";
                cmd.CommandType = CommandType.StoredProcedure;

               

                DataTable dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {






                        asqTrackerModel.AsqData = dt;










                    }
                }




            }

            return asqTrackerModel;
        }

        public AsqTrackerModel GetProductionTNAData(DateTime fromdate,DateTime todate)
        {
            AsqTrackerModel asqTrackerModel = new AsqTrackerModel();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"ASQTracker_SP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);


                DataTable dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {






                        asqTrackerModel.AsqData = dt;










                    }
                }




            }

            return asqTrackerModel;
        }

    }


    public class ProductionReportRepo
    {


        public ReportDataModel GetBEpercentReport(String Month)
        {
            ReportDataModel reportDataModel = new ReportDataModel();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"BEpercentReport_SP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Month", Month);


                DataTable dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {






                        reportDataModel.AsqData = dt;










                    }
                }




            }

            return reportDataModel;
        }





        public ReportDataModel GetBEpercentRatioReport(String Month)
        {
            ReportDataModel reportDataModel = new ReportDataModel();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"BERatioReport_SP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Month", Month);


                DataTable dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {






                        reportDataModel.AsqData = dt;










                    }
                }




            }

            return reportDataModel;
        }

        /// <summary>
        /// Get the CSFA Reports background data
        /// </summary>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="atcid"></param>
        /// <param name="MyType"></param> wich typ if day report or total atc base report
        /// <returns></returns>
        public DataTable GetCSFAWithAtcAndRatio(DateTime fromdate, DateTime todate, int atcid = 0,String MyType=null)
        {



            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(@"CSFAData"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);
                dt = QueryFunctions. ReturnQueryResultDatatableforSP(cmd);
            }
            //  dt = dt.Select("OurStyleId=802").CopyToDataTable();





            dt.Columns.Add("CRatio");
            dt.Columns.Add("SRatio");
            dt.Columns.Add("FRatio");
            dt.Columns.Add("ARatio");


            dt.Columns.Add("Locked_CRatio");
            dt.Columns.Add("Locked_SRatio");
            dt.Columns.Add("Locked_FRatio");
            dt.Columns.Add("Locked_ARatio");


            dt.Columns.Add("Locked_TotValue", typeof(Decimal));




            foreach (DataRow row in dt.Rows)
            {
                Decimal cval = 0, sval = 0, fval = 0, aval = 0, ratiotot = 0, CRatio = 0, SRatio = 0, FRatio = 0, ARatio = 0,
                    Locked_CRatio = 0, Locked_SRatio = 0, Locked_FRatio = 0, Locked_ARatio = 0, LockedCM = 0,
                Locked_CValue = 0, Locked_SValue = 0, Locked_FValue = 0, Locked_AValue = 0, TTLSAM = 0; ;
                int ourstyleid = int.Parse(row["OurStyleId"].ToString());

                if (ourstyleid == 802)
                {
                    int k = ourstyleid;
                }

                try
                {
                    cval = Decimal.Parse(row["C"].ToString());
                }
                catch (Exception)
                {


                }
                try
                {
                    sval = Decimal.Parse(row["S"].ToString());
                }
                catch (Exception)
                {


                }
                try
                {
                    fval = Decimal.Parse(row["F"].ToString());
                }
                catch (Exception)
                {


                }
                try { aval = Decimal.Parse(row["A"].ToString()); } catch (Exception) { }
                try { LockedCM = Decimal.Parse(row["LockedCM"].ToString()); } catch (Exception) { }

                try { TTLSAM = Decimal.Parse(row["TTLSAM"].ToString()); } catch (Exception) { }

                ratiotot = TTLSAM;
                //ratiotot = aval + fval + sval + cval;
                try { CRatio = (cval / ratiotot) * 100; } catch (Exception) { }
                try { SRatio = (sval / ratiotot) * 100; } catch (Exception) { }
                try { FRatio = (fval / ratiotot) * 100; } catch (Exception) { }
                try { ARatio = (aval / ratiotot) * 100; } catch (Exception) { }


                try { Locked_CRatio = (LockedCM / 100) * CRatio; } catch (Exception) { }
                try { Locked_SRatio = (LockedCM / 100) * SRatio; } catch (Exception) { }
                try { Locked_FRatio = (LockedCM / 100) * FRatio; } catch (Exception) { }
                try { Locked_ARatio = (LockedCM / 100) * ARatio; } catch (Exception) { }









                row["CRatio"] = CRatio;
                row["SRatio"] = SRatio;
                row["FRatio"] = FRatio;
                row["ARatio"] = ARatio;
                row["Locked_CRatio"] = Math.Round(Decimal.Parse(Locked_CRatio.ToString()), 5);
                row["Locked_SRatio"] = Math.Round(Decimal.Parse(Locked_SRatio.ToString()), 5);
                row["Locked_FRatio"] = Math.Round(Decimal.Parse(Locked_FRatio.ToString()), 5);
                row["Locked_ARatio"] = Math.Round(Decimal.Parse(Locked_ARatio.ToString()), 5);



                Decimal ProducedGarment = Decimal.Parse(row["ProducedGarment"].ToString());
                Decimal ReveneuVal = 0;

                int DeptID = int.Parse(row["DeptID"].ToString());
                Decimal multratio = 0;
                if (DeptID == 2) { multratio = Locked_CRatio; }
                else if (DeptID == 5) { multratio = Locked_FRatio; }
                else if (DeptID == 6) { multratio = Locked_ARatio; } else if (DeptID == 3) { multratio = Locked_SRatio; }
                ReveneuVal = multratio * ProducedGarment;

                row["Locked_TotValue"] = ReveneuVal;

            }





            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, "LocationName", "Location_pk", "OurStyle", "OurStyleID", "LockedCM", "CRatio", "SRatio", "FRatio", "ARatio", "Locked_CRatio", "Locked_SRatio", "Locked_FRatio", "Locked_ARatio");

            distinctValues.Columns.Add("JCM", typeof(Decimal));
            distinctValues.Columns.Add("CutQty", typeof(Decimal));
            distinctValues.Columns.Add("CutValue", typeof(Decimal));
            distinctValues.Columns.Add("CutJCMValue", typeof(Decimal));



            distinctValues.Columns.Add("SewQty", typeof(Decimal));
            distinctValues.Columns.Add("SewValue", typeof(Decimal));
            distinctValues.Columns.Add("SewJCMValue", typeof(Decimal));


            distinctValues.Columns.Add("FinishingQty", typeof(Decimal));
            distinctValues.Columns.Add("Finishingvalue", typeof(Decimal));
            distinctValues.Columns.Add("FinishingJCMValue", typeof(Decimal));


            distinctValues.Columns.Add("AirPortQty", typeof(Decimal));
            distinctValues.Columns.Add("AirportValue", typeof(Decimal));
            distinctValues.Columns.Add("AirportJCMValue", typeof(Decimal));

            distinctValues.Columns.Add("Total", typeof(Decimal));
            distinctValues.Columns.Add("JCMtotalvalue", typeof(Decimal));
            distinctValues.Columns.Add("Diff", typeof(Decimal));
            foreach (DataRow row in distinctValues.Rows)
            {
                int Location_pk = int.Parse(row["Location_pk"].ToString());
                //DateTime effDate = DateTime.Parse(row["effDate"].ToString());
                int OurStyleID = int.Parse(row["OurStyleID"].ToString());



                try
                {
                    var JCM = dt.Compute("max(JCM)", " Location_pk=" + Location_pk + " and DeptID=2  and OurStyleID=" + OurStyleID + "");
                    row["JCM"] = Decimal.Parse(JCM.ToString());
                }
                catch (Exception)
                {

                    row["JCM"] = 0;
                }






                #region Cut

                try
                {
                    var cutQtyqry = dt.Compute("Sum(ProducedGarment)", " Location_pk=" + Location_pk + " and DeptID=2  and OurStyleID=" + OurStyleID + "");
                    row["CutQty"] = Decimal.Parse(cutQtyqry.ToString());
                }
                catch (Exception)
                {

                    row["CutQty"] = 0;
                }
                try
                {
                    var cutValqry = dt.Compute("Sum(Locked_TotValue)", "Location_pk=" + Location_pk + " and DeptID=2 and OurStyleID=" + OurStyleID + "");
                    row["CutValue"] = Decimal.Parse(cutValqry.ToString());
                }
                catch (Exception ex)
                {

                    row["CutValue"] = 0;
                }
                try
                {
                    var cutValqry = dt.Compute("Sum(Revenue)", "Location_pk=" + Location_pk + " and DeptID=2 and OurStyleID=" + OurStyleID + "");
                    row["CutJCMValue"] = Decimal.Parse(cutValqry.ToString());
                }
                catch (Exception ex)
                {

                    row["CutJCMValue"] = 0;
                }

                try
                {
                    var cutValqry = dt.Compute("Max(DeptCM)", "Location_pk=" + Location_pk + " and DeptID=2 and OurStyleID=" + OurStyleID + "");
                    row["CRatio"] = Decimal.Parse(cutValqry.ToString());
                }
                catch (Exception ex)
                {

                    row["CRatio"] = 0;
                }
                #endregion

                #region Sew

                try
                {
                    var SewQtyqry = dt.Compute("Sum(ProducedGarment)", "Location_pk=" + Location_pk + " and DeptID=3 and OurStyleID=" + OurStyleID + "");
                    row["SewQty"] = Decimal.Parse(SewQtyqry.ToString());
                }
                catch (Exception)
                {

                    row["SewQty"] = 0;
                }
                try
                {
                    var SewValqry = dt.Compute("Sum(Locked_TotValue)", "Location_pk=" + Location_pk + " and DeptID=3 and OurStyleID=" + OurStyleID + "");
                    row["SewValue"] = Decimal.Parse(SewValqry.ToString());
                }
                catch (Exception ex)
                {

                    row["SewValue"] = 0;
                }
                try
                {
                    var cutValqry = dt.Compute("Sum(Revenue)", " Location_pk=" + Location_pk + " and DeptID=3 and OurStyleID=" + OurStyleID + "");
                    row["SewJCMValue"] = Decimal.Parse(cutValqry.ToString());
                }
                catch (Exception ex)
                {

                    row["SewJCMValue"] = 0;
                }
                try
                {
                    var cutValqry = dt.Compute("Max(DeptCM)", " Location_pk=" + Location_pk + " and DeptID=3 and OurStyleID=" + OurStyleID + "");
                    row["SRatio"] = Decimal.Parse(cutValqry.ToString());
                }
                catch (Exception ex)
                {

                    row["SRatio"] = 0;
                }
                #endregion


                #region AirPort

                try
                {
                    var AirPortQtyqry = dt.Compute("Sum(ProducedGarment)", " Location_pk=" + Location_pk + " and DeptID=6 and OurStyleID=" + OurStyleID + "");
                    row["AirPortQty"] = Decimal.Parse(AirPortQtyqry.ToString());
                }
                catch (Exception)
                {

                    row["AirPortQty"] = 0;
                }
                try
                {
                    var AirPortValqry = dt.Compute("Sum(Locked_TotValue)", " Location_pk=" + Location_pk + " and DeptID=6 and OurStyleID=" + OurStyleID + "");
                    row["AirPortValue"] = Decimal.Parse(AirPortValqry.ToString());
                }
                catch (Exception ex)
                {

                    row["AirPortValue"] = 0;
                }
                try
                {
                    var cutValqry = dt.Compute("Sum(Revenue)", "Location_pk=" + Location_pk + " and DeptID=6 and OurStyleID=" + OurStyleID + "");
                    row["AirportJCMValue"] = Decimal.Parse(cutValqry.ToString());
                }
                catch (Exception ex)
                {

                    row["AirportJCMValue"] = 0;
                }
                try
                {
                    var cutValqry = dt.Compute("Max(DeptCM)", " Location_pk=" + Location_pk + " and DeptID=6 and OurStyleID=" + OurStyleID + "");
                    row["ARatio"] = Decimal.Parse(cutValqry.ToString());
                }
                catch (Exception ex)
                {

                    row["ARatio"] = 0;
                }
                #endregion






                #region Finishing

                try
                {
                    var Finishingqry = dt.Compute("Sum(ProducedGarment)", "Location_pk=" + Location_pk + " and DeptID=5 and OurStyleID=" + OurStyleID + "");
                    row["FinishingQty"] = Decimal.Parse(Finishingqry.ToString());
                }
                catch (Exception)
                {

                    row["FinishingQty"] = 0;
                }
                try
                {
                    var FinishingValqry = dt.Compute("Sum(Locked_TotValue)", " Location_pk=" + Location_pk + " and DeptID=5 and OurStyleID=" + OurStyleID + "");
                    row["FinishingValue"] = Decimal.Parse(FinishingValqry.ToString());
                }
                catch (Exception ex)
                {

                    row["FinishingValue"] = 0;
                }


                try
                {
                    var cutValqry = dt.Compute("Sum(Revenue)", " Location_pk=" + Location_pk + " and DeptID=5 and OurStyleID=" + OurStyleID + "");
                    row["FinishingJCMValue"] = Decimal.Parse(cutValqry.ToString());
                }
                catch (Exception ex)
                {

                    row["FinishingJCMValue"] = 0;
                }
                try
                {
                    var cutValqry = dt.Compute("Max(DeptCM)", " Location_pk=" + Location_pk + " and DeptID=5 and OurStyleID=" + OurStyleID + "");
                    row["FRatio"] = Decimal.Parse(cutValqry.ToString());
                }
                catch (Exception ex)
                {

                    row["FRatio"] = 0;
                }
                #endregion


                #region JCMValue

                try
                {
                    var jcmvalue = dt.Compute("Sum(Revenue)", "Location_pk=" + Location_pk + "and OurStyleID=" + OurStyleID + "");
                    row["JCMtotalvalue"] = Decimal.Parse(jcmvalue.ToString());
                }
                catch (Exception ex)
                {

                    row["JCMtotalvalue"] = 0;
                }


                #endregion

                Decimal total = Decimal.Parse(row["FinishingValue"].ToString()) + Decimal.Parse(row["AirPortValue"].ToString())
                    + Decimal.Parse(row["SewValue"].ToString()) + Decimal.Parse(row["CutValue"].ToString());

                row["Total"] = Decimal.Parse(total.ToString());


                row["Diff"] = Decimal.Parse(row["Total"].ToString()) - Decimal.Parse(row["JCMtotalvalue"].ToString());


            }





            distinctValues.Columns["Location_pk"].SetOrdinal(0);
            distinctValues.Columns["OurStyleID"].SetOrdinal(1);
            distinctValues.Columns["LocationName"].SetOrdinal(2);
            distinctValues.Columns["OurStyle"].SetOrdinal(3);
            distinctValues.Columns["jcm"].SetOrdinal(4);
            distinctValues.Columns["LockedCM"].SetOrdinal(5);
            distinctValues.Columns["CutQty"].SetOrdinal(6);
            distinctValues.Columns["CRatio"].SetOrdinal(7);
            distinctValues.Columns["CutJCMValue"].SetOrdinal(8);
            distinctValues.Columns["Locked_CRatio"].SetOrdinal(9);
            distinctValues.Columns["CutValue"].SetOrdinal(10);
            distinctValues.Columns["SewQty"].SetOrdinal(11);
            distinctValues.Columns["SRatio"].SetOrdinal(12);
            distinctValues.Columns["SewJCMValue"].SetOrdinal(13);
            distinctValues.Columns["Locked_SRatio"].SetOrdinal(14);
            distinctValues.Columns["SewValue"].SetOrdinal(15);
            distinctValues.Columns["FinishingQty"].SetOrdinal(16);
            distinctValues.Columns["FRatio"].SetOrdinal(17);
            distinctValues.Columns["FinishingJCMValue"].SetOrdinal(18);
            distinctValues.Columns["Locked_FRatio"].SetOrdinal(19);
            distinctValues.Columns["Finishingvalue"].SetOrdinal(20);
            distinctValues.Columns["AirPortQty"].SetOrdinal(21);
            distinctValues.Columns["ARatio"].SetOrdinal(22);
            distinctValues.Columns["AirportJCMValue"].SetOrdinal(23);
            distinctValues.Columns["Locked_ARatio"].SetOrdinal(24);
            distinctValues.Columns["AirportValue"].SetOrdinal(25);
            distinctValues.Columns["Total"].SetOrdinal(26);
            distinctValues.Columns["JCMtotalvalue"].SetOrdinal(27);
            distinctValues.Columns["Diff"].SetOrdinal(28);



            if (MyType == "Day") { return dt; } else { return distinctValues; }

           
        }







        public DataTable GetShipmentofAtc( int atcid=0)
        {
            DataTable dt = new DataTable();

         




                using (SqlCommand cmd = new SqlCommand(@"ShipmentofAtc_SP"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@atcid", atcid);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }

              




              

               



           
            return dt;
        }



        public DataTable GetRejectionRequest(int atcid = 0)
        {
            DataTable dt = new DataTable();






            using (SqlCommand cmd = new SqlCommand(@"RejectionRequest_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@atcid", atcid);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }













            return dt;
        }



        public DataTable GetJobContractMaster(int atcid = 0)
        {
            DataTable dt = new DataTable();






            using (SqlCommand cmd = new SqlCommand(@"JobcontractList_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@atcid", atcid);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }













            return dt;
        }


        public DataTable GetCSFA(DateTime fromdate, DateTime todate,int atcid = 0)
        {
           


            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(@"CSFAData"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }


          


            
            dt.Columns.Add("CRatio");
            dt.Columns.Add("SRatio");
            dt.Columns.Add("FRatio");
            dt.Columns.Add("ARatio");


            dt.Columns.Add("Locked_CRatio");
            dt.Columns.Add("Locked_SRatio");
            dt.Columns.Add("Locked_FRatio");
            dt.Columns.Add("Locked_ARatio");

            dt.Columns.Add("Locked_Cvalue");
            dt.Columns.Add("Locked_SValue");
            dt.Columns.Add("Locked_FValue");
            dt.Columns.Add("Locked_AValue");
            dt.Columns.Add("Locked_TotValue");
            dt.Columns.Add("JCMtotalvalue");




            foreach (DataRow row in dt.Rows)
            {
                Decimal cval = 0, sval = 0, fval = 0, aval = 0, ratiotot = 0, CRatio = 0, SRatio = 0, FRatio = 0, ARatio = 0,
                    Locked_CRatio = 0, Locked_SRatio = 0, Locked_FRatio = 0, Locked_ARatio = 0, LockedCM = 0,
                Locked_CValue = 0, Locked_SValue = 0, Locked_FValue = 0, Locked_AValue = 0;

                try
                {
                    cval = Decimal.Parse(row["C"].ToString());
                }
                catch (Exception)
                {


                }
                try
                {
                    sval = Decimal.Parse(row["S"].ToString());
                }
                catch (Exception)
                {


                }
                try
                {
                    fval = Decimal.Parse(row["F"].ToString());
                }
                catch (Exception)
                {


                }
                try { aval = Decimal.Parse(row["A"].ToString()); } catch (Exception) { }
                try { LockedCM = Decimal.Parse(row["LockedCM"].ToString()); } catch (Exception) { }
                ratiotot = aval + fval + sval + cval;
                try { CRatio = (cval / ratiotot) * 100; } catch (Exception) { }
                try { SRatio = (sval / ratiotot) * 100; } catch (Exception) { }
                try { FRatio = (fval / ratiotot) * 100; } catch (Exception) { }
                try { ARatio = (aval / ratiotot) * 100; } catch (Exception) { }


                try { Locked_CRatio = (LockedCM / 100) * CRatio; } catch (Exception) { }
                try { Locked_SRatio = (LockedCM / 100) * SRatio; } catch (Exception) { }
                try { Locked_FRatio = (LockedCM / 100) * FRatio; } catch (Exception) { }
                try { Locked_ARatio = (LockedCM / 100) * ARatio; } catch (Exception) { }









                row["CRatio"] = CRatio;
                row["SRatio"] = SRatio;
                row["FRatio"] = FRatio;
                row["ARatio"] = ARatio;
                row["Locked_CRatio"] = Locked_CRatio;
                row["Locked_SRatio"] = Locked_SRatio;
                row["Locked_FRatio"] = Locked_FRatio;
                row["Locked_ARatio"] = Locked_ARatio;





           
                try { Locked_CValue = Decimal.Parse(row["CQty"].ToString()); } catch (Exception) { }
                try { Locked_SValue = Decimal.Parse(row["SQty"].ToString()); } catch (Exception) { }
                try { Locked_FValue = Decimal.Parse(row["FQty"].ToString()); } catch (Exception) { }
                try { Locked_AValue = Decimal.Parse(row["AQty"].ToString()); } catch (Exception) { }
                row["Locked_Cvalue"] = Locked_CValue* Locked_CRatio;
                row["Locked_SValue"] = Locked_SValue * Locked_SRatio;
                row["Locked_FValue"] = Locked_FValue * Locked_FRatio;
                row["Locked_AValue"] = Locked_FValue * Locked_ARatio;

                row["Locked_TotValue"]    = Decimal.Parse(row["Locked_Cvalue"].ToString()) + Decimal.Parse(row["Locked_SValue"].ToString()) + Decimal.Parse(row["Locked_FValue"].ToString()) + Decimal.Parse(row["Locked_AValue"].ToString());

            }
            return dt;
        }


        public DataTable GetCSFANew(DateTime fromdate, DateTime todate, int atcid = 0)
        {



            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(@"CSFAData"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }






            dt.Columns.Add("CRatio");
            dt.Columns.Add("SRatio");
            dt.Columns.Add("FRatio");
            dt.Columns.Add("ARatio");


            dt.Columns.Add("Locked_CRatio");
            dt.Columns.Add("Locked_SRatio");
            dt.Columns.Add("Locked_FRatio");
            dt.Columns.Add("Locked_ARatio");

            dt.Columns.Add("multiratio");

            dt.Columns.Add("Locked_TotValue", typeof(Decimal));




            foreach (DataRow row in dt.Rows)
            {
                Decimal cval = 0, sval = 0, fval = 0, aval = 0, ratiotot = 0, CRatio = 0, SRatio = 0, FRatio = 0, ARatio = 0,
                    Locked_CRatio = 0, Locked_SRatio = 0, Locked_FRatio = 0, Locked_ARatio = 0, LockedCM = 0,
                Locked_CValue = 0, Locked_SValue = 0, Locked_FValue = 0, Locked_AValue = 0 ,TTLSAM = 0;

                try
                {
                    cval = Decimal.Parse(row["C"].ToString());
                }
                catch (Exception)
                {


                }
                try
                {
                    sval = Decimal.Parse(row["S"].ToString());
                }
                catch (Exception)
                {


                }
                try
                {
                    fval = Decimal.Parse(row["F"].ToString());
                }
                catch (Exception)
                {


                }
                try { aval = Decimal.Parse(row["A"].ToString()); } catch (Exception) { }
                try { LockedCM = Decimal.Parse(row["LockedCM"].ToString()); } catch (Exception) { }
                try { TTLSAM = Decimal.Parse(row["TTLSAM"].ToString()); } catch (Exception) { }

                ratiotot = TTLSAM;
                //ratiotot = aval + fval + sval + cval;
                try { CRatio = (cval / ratiotot) * 100; } catch (Exception) { }
                try { SRatio = (sval / ratiotot) * 100; } catch (Exception) { }
                try { FRatio = (fval / ratiotot) * 100; } catch (Exception) { }
                try { ARatio = (aval / ratiotot) * 100; } catch (Exception) { }


                try { Locked_CRatio = (LockedCM / 100) * CRatio; } catch (Exception) { }
                try { Locked_SRatio = (LockedCM / 100) * SRatio; } catch (Exception) { }
                try { Locked_FRatio = (LockedCM / 100) * FRatio; } catch (Exception) { }
                try { Locked_ARatio = (LockedCM / 100) * ARatio; } catch (Exception) { }









                row["CRatio"] = CRatio;
                row["SRatio"] = SRatio;
                row["FRatio"] = FRatio;
                row["ARatio"] = ARatio;
                row["Locked_CRatio"] = Locked_CRatio;
                row["Locked_SRatio"] = Locked_SRatio;
                row["Locked_FRatio"] = Locked_FRatio;
                row["Locked_ARatio"] = Locked_ARatio;



                Decimal ProducedGarment = Decimal.Parse(row["ProducedGarment"].ToString());
                Decimal ReveneuVal = 0;

                int DeptID= int.Parse(row["DeptID"].ToString());
                Decimal multratio = 0;
                if (DeptID == 2) { multratio = Locked_CRatio; } else if (DeptID == 5) { multratio = Locked_FRatio; }
                else if (DeptID == 6) { multratio = Locked_ARatio; } else if (DeptID == 3) { multratio = Locked_SRatio; }
                ReveneuVal = multratio * ProducedGarment;

                row["multiratio"] = multratio;
                
                row["Locked_TotValue"] = ReveneuVal;

            }

            //using (ArtEntitiesnew artenty = new DataModels.ArtEntitiesnew())
            //{
            //    foreach (DataRow row in dt.Rows)
            //{
            //    int Location_pk = int.Parse(row["Location_pk"].ToString());
            //    int OurStyleID = int.Parse(row["OurStyleID"].ToString());
            //    DateTime effDate = DateTime.Parse(row["effDate"].ToString());
            //    int DeptID = int.Parse(row["DeptID"].ToString());
            //        Decimal jcm = 0;
            //        Decimal acm = 0;
            //        int k = 0;
            //        Decimal LockedCM = 0;
            //        try
            //        {
            //            jcm = Math.Round(Decimal.Parse(row["JCM"].ToString()), 4);
            //        }
            //        catch (Exception)
            //        {

                       
            //        }

            //        try
            //        {
            //            acm = Math.Round(Decimal.Parse(row["multiratio"].ToString()), 4);
            //        }
            //        catch (Exception)
            //        {

                       
            //        }
            //        try
            //        {
            //            LockedCM = Math.Round(Decimal.Parse(row["LockedCM"].ToString()), 4);
            //        }
            //        catch (Exception)
            //        {


            //        }
            //        if (!artenty.CSFA_Component.Any(f => f.Ourstyle_id == OurStyleID && f.Department_pk == DeptID && f.Location_pk == Location_pk && f.Eff_Date == effDate))
            //        {

            //            try
            //            {
            //                k = 0;
            //                CSFA_Component ourStyleDeptCM = new CSFA_Component();
            //                ourStyleDeptCM.Eff_Date = effDate;
            //                ourStyleDeptCM.Ourstyle = row["OurStyle"].ToString();
            //                ourStyleDeptCM.Department = row["DeptName"].ToString();
            //                ourStyleDeptCM.Qty = Decimal.Parse(row["ProducedGarment"].ToString());
            //                ourStyleDeptCM.Location = row["LocationName"].ToString();

            //                k = 1;
            //                ourStyleDeptCM.Locked_cm = LockedCM;
            //                ourStyleDeptCM.Jcm = jcm;
            //                ourStyleDeptCM.Component_Acm = acm;                          
            //                ourStyleDeptCM.Location_pk = Location_pk;                          
                           
            //                ourStyleDeptCM.Ourstyle_id = OurStyleID;
            //                ourStyleDeptCM.Department_pk = DeptID;



            //                artenty.CSFA_Component.Add(ourStyleDeptCM);
            //                artenty.SaveChanges();
            //            }
            //            catch (Exception exp)
            //            {

            //                k = k;
                            
            //            }

            //        }
            //    }
            //}


                DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, "LocationName", "effDate", "Location_pk");
            distinctValues.Columns.Add("CutQty");
            distinctValues.Columns.Add("CutValue");
            distinctValues.Columns.Add("SewQty");
            distinctValues.Columns.Add("SewValue");
           

            distinctValues.Columns.Add("FinishingQty");
            distinctValues.Columns.Add("Finishingvalue");
            distinctValues.Columns.Add("AirPortQty");
            distinctValues.Columns.Add("AirportValue");


            distinctValues.Columns.Add("Total");
            distinctValues.Columns.Add("JCMtotalvalue");
            distinctValues.Columns.Add("Diff");
            foreach (DataRow row in distinctValues.Rows)
            {
                int Location_pk = int.Parse(row["Location_pk"].ToString());
                DateTime effDate = DateTime.Parse(row["effDate"].ToString());


                #region Cut

                try
                {
                    var cutQtyqry = dt.Compute("Sum(ProducedGarment)", "effDate ='" + effDate + "' and Location_pk=" + Location_pk + " and DeptID=2 ");
                    row["CutQty"] = Decimal.Parse(cutQtyqry.ToString()).ToString("F");
                }
                catch (Exception)
                {

                    row["CutQty"] = 0;
                }
                try
                {
                    var cutValqry = dt.Compute("Sum(Locked_TotValue)", "effDate ='" + effDate + "' and Location_pk=" + Location_pk + " and DeptID=2 ");
                    row["CutValue"] = Decimal.Parse(cutValqry.ToString()).ToString("F");
                }
                catch (Exception ex)
                {

                    row["CutValue"] = 0;
                }
                #endregion



                #region Sew

                try
                {
                    var SewQtyqry = dt.Compute("Sum(ProducedGarment)", "effDate ='" + effDate + "' and Location_pk=" + Location_pk + " and DeptID=3 ");
                    row["SewQty"] = Decimal.Parse(SewQtyqry.ToString()).ToString("F");
                }
                catch (Exception)
                {

                    row["SewQty"] = 0;
                }
                try
                {
                    var SewValqry = dt.Compute("Sum(Locked_TotValue)", "effDate ='" + effDate + "' and Location_pk=" + Location_pk + " and DeptID=3 ");
                    row["SewValue"] = Decimal.Parse(SewValqry.ToString()).ToString("F");
                }
                catch (Exception ex)
                {

                    row["SewValue"] = 0;
                }
                #endregion


                #region AirPort

                try
                {
                    var AirPortQtyqry = dt.Compute("Sum(ProducedGarment)", "effDate ='" + effDate + "' and Location_pk=" + Location_pk + " and DeptID=6 ");
                    row["AirPortQty"] = Decimal.Parse(AirPortQtyqry.ToString()).ToString("F");
                }
                catch (Exception)
                {

                    row["AirPortQty"] = 0;
                }
                try
                {
                    var AirPortValqry = dt.Compute("Sum(Locked_TotValue)", "effDate ='" + effDate + "' and Location_pk=" + Location_pk + " and DeptID=6");
                    row["AirPortValue"] = Decimal.Parse(AirPortValqry.ToString()).ToString("F");
                }
                catch (Exception ex)
                {

                    row["AirPortValue"] = 0;
                }
                #endregion






                #region Finishing

                try
                {
                    var Finishingqry = dt.Compute("Sum(ProducedGarment)", "effDate ='" + effDate + "' and Location_pk=" + Location_pk + " and DeptID=5 ");
                    row["FinishingQty"] = Decimal.Parse(Finishingqry.ToString()).ToString("F");
                }
                catch (Exception)
                {

                    row["FinishingQty"] = 0;
                }
                try
                {
                    var FinishingValqry = dt.Compute("Sum(Locked_TotValue)", "effDate ='" + effDate + "' and Location_pk=" + Location_pk + " and DeptID=5 ");
                    row["FinishingValue"] = Decimal.Parse(FinishingValqry.ToString()).ToString("F");
                }
                catch (Exception ex)
                {

                    row["FinishingValue"] = 0;
                }
                #endregion


                #region JCMValue

                try
                {
                    var jcmvalue = dt.Compute("Sum(Revenue)", "effDate ='" + effDate + "' and Location_pk=" + Location_pk + "");
                    row["JCMtotalvalue"] = Decimal.Parse(jcmvalue.ToString()).ToString("F");
                }
                catch (Exception ex)
                {

                    row["JCMtotalvalue"] = 0;
                }


                #endregion

                Decimal total = Decimal.Parse(row["FinishingValue"].ToString()) + Decimal.Parse(row["AirPortValue"].ToString())
                    + Decimal.Parse(row["SewValue"].ToString()) + Decimal.Parse(row["CutValue"].ToString());

                row["Total"]=total.ToString("F");


                row["Diff"] = Decimal.Parse(row["Total"].ToString()) - Decimal.Parse(row["JCMtotalvalue"].ToString());


            }





            return distinctValues;
        }



        public DataTable GetDObetweenDate(DateTime fromdate, DateTime todate, String dotype)
        {
            DataTable dt = new DataTable();






            using (SqlCommand cmd = new SqlCommand(@"GetDeliveryDosBetweenDates_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);
                cmd.Parameters.AddWithValue("@dotype", dotype);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }













            return dt;
        }




        public DataTable GETADNWISE(DateTime fromdate, DateTime todate)
        {
            DataTable dt = new DataTable();






            using (SqlCommand cmd = new SqlCommand(@"GetADNWISE_SP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);

                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }













            return dt;
        }


















        public DataTable GetCriticalAtc(int atcid = 0)
        {
            DataTable dt = new DataTable();






            using (SqlCommand cmd = new SqlCommand(@"CriticalReportMaster_Sp"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@atcid", atcid);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }





            return dt;
        }


    }







    public class PcdAlertRepo
    {


        public String insertpcdaLERT(PcdAlertModel things)
        {
            string msg = "";

            using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {
                int Pcdalert_pk = 0;
                if (things.Pcdalert_pk != null)
                {
                    Pcdalert_pk = int.Parse(things.Pcdalert_pk);
                }

                DateTime weekdate = DateTime.Parse(things.WeekNo);
                var a = enty.YearWeekMasters.Where(U => U.Fromdate <= weekdate && U.Todate >= weekdate).Select(u => u.Week_no).FirstOrDefault();
                var weekofdate = Decimal.Parse(a.ToString());

                if (!enty.PcdAlertMasters.Any(f => f.PcdAlert_Pk == Pcdalert_pk))
                {


                    PcdAlertMaster pcdAlertMaster = new PcdAlertMaster();
                    pcdAlertMaster.Atc_id = int.Parse(things.Atcid);
                    pcdAlertMaster.WeekNum = weekofdate;
                    pcdAlertMaster.Line_no = int.Parse(things.LineNo);
                    pcdAlertMaster.Cut_Start_date = DateTime.Parse(things.CutStartDate);
                    pcdAlertMaster.Approval_status = things.ApprovalStatus;
                    pcdAlertMaster.Sewing_Material_Issue = things.SewingMaterialIssue;
                    pcdAlertMaster.BO_remarks_sewing = things.BoRemarksSewing;
                    pcdAlertMaster.Sewing_Bo_plan_accept = things.BoPlanSewingAccpet;
                    pcdAlertMaster.Sewing_action = things.SewingAction;
                    pcdAlertMaster.Location_pk = int.Parse(things.Location_pk);
                    pcdAlertMaster.Addedby = HttpContext.Current.Session["Username"].ToString();
                    pcdAlertMaster.Addeddate = DateTime.Now;
                    pcdAlertMaster.IsActive = "Y";
                    pcdAlertMaster.OldPcdAlert_Pk = 0;
                    pcdAlertMaster.type = "Sewing";



                    enty.PcdAlertMasters.Add(pcdAlertMaster);

                }
                else
                {


                    var q = from pcdalert in enty.PcdAlertMasters
                            where pcdalert.PcdAlert_Pk == Pcdalert_pk
                            select pcdalert;
                    foreach (var element in q)
                    {


                        element.Atc_id = int.Parse(things.Atcid);
                        element.WeekNum = weekofdate;
                        element.Line_no = int.Parse(things.LineNo);
                        element.Cut_Start_date = DateTime.Parse(things.CutStartDate);
                        element.Approval_status = things.ApprovalStatus;
                        element.Sewing_Material_Issue = things.SewingMaterialIssue;
                        element.BO_remarks_sewing = things.BoRemarksSewing;
                        element.Sewing_Bo_plan_accept = things.BoPlanSewingAccpet;
                        element.Sewing_action = things.SewingAction;
                        element.Location_pk = int.Parse(things.Location_pk);

                    }



                }

                enty.SaveChanges();
                msg = "sucessfully UPDATED";


            }
            return msg;
        }

        public String insertpcdalertPacking(PcdAlertModel things)
        {
            string msg = "";

            using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {
                int Pcdalert_pk = 0;
                if (things.Pcdalert_pk != null)
                {
                    Pcdalert_pk = int.Parse(things.Pcdalert_pk);
                }

                DateTime weekdate = DateTime.Parse(things.WeekNo);
                var a = enty.YearWeekMasters.Where(U => U.Fromdate <= weekdate && U.Todate >= weekdate).Select(u => u.Week_no).FirstOrDefault();
                var weekofdate = Decimal.Parse(a.ToString());

                if (!enty.PcdAlertMasters.Any(f => f.PcdAlert_Pk == Pcdalert_pk))
                {


                    PcdAlertMaster pcdAlertMaster = new PcdAlertMaster();
                    pcdAlertMaster.Atc_id = int.Parse(things.Atcid);
                    pcdAlertMaster.WeekNum = weekofdate;
                    pcdAlertMaster.Line_no = int.Parse(things.LineNo);
                    pcdAlertMaster.Cut_Start_date = DateTime.Parse(things.CutStartDate);
                    pcdAlertMaster.Approval_status = things.ApprovalStatus;
                    pcdAlertMaster.Packing_Material_Issue = things.PackingMaterialIssue;
                    pcdAlertMaster.BO_remarks_Packing = things.BoRemarksPacking;
                    pcdAlertMaster.Packing_bo_plan_accept = things.BoPlanPackingAccept;
                    pcdAlertMaster.Packing_action = things.PackingAction;
                    pcdAlertMaster.Location_pk = int.Parse(things.Location_pk);
                    pcdAlertMaster.Addedby = HttpContext.Current.Session["Username"].ToString();
                    pcdAlertMaster.Addeddate = DateTime.Now;
                    pcdAlertMaster.IsActive = "Y";
                    pcdAlertMaster.OldPcdAlert_Pk = 0;
                    pcdAlertMaster.type = "Packing";
                    enty.PcdAlertMasters.Add(pcdAlertMaster);

                }
                else
                {


                    var q = from pcdalert in enty.PcdAlertMasters
                            where pcdalert.PcdAlert_Pk == Pcdalert_pk
                            select pcdalert;
                    foreach (var element in q)
                    {


                        element.Atc_id = int.Parse(things.Atcid);
                        element.WeekNum = weekofdate;
                        element.Line_no = int.Parse(things.LineNo);
                        element.Cut_Start_date = DateTime.Parse(things.CutStartDate);
                        element.Approval_status = things.ApprovalStatus;
                        element.Packing_Material_Issue = things.PackingMaterialIssue;
                        element.BO_remarks_Packing = things.BoRemarksPacking;
                        element.Packing_bo_plan_accept = things.BoPlanPackingAccept;
                        element.Packing_action = things.PackingAction;
                        element.Location_pk = int.Parse(things.Location_pk);
                    }

                }

                enty.SaveChanges();
                msg = "sucessfully UPDATED";

            }
            return msg;
        }

        public List<PcdAlertModel> get_pcdalert(DateTime weekno, int location_pk)
        {

            List<PcdAlertModel> rk = new List<PcdAlertModel>();
            using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {

                DateTime curweek = DateTime.Now.Date;
                var a1 = enty.YearWeekMasters.Where(U => U.Fromdate <= curweek && U.Todate >= curweek).Select(u => u.Week_no).FirstOrDefault();
                insertPending(int.Parse(a1.ToString()));


                var a = enty.YearWeekMasters.Where(U => U.Fromdate <= weekno && U.Todate >= weekno).Select(u => u.Week_no).FirstOrDefault();
                Decimal datetimeofcountryindiais = Decimal.Parse(a.ToString());

                var q = from pcddetails in enty.PcdAlertMasters
                        where pcddetails.WeekNum == datetimeofcountryindiais && pcddetails.Location_pk == location_pk && pcddetails.IsActive == "Y" && pcddetails.type == "Sewing"
                        select pcddetails;
                foreach (var element in q)
                {
                    PcdAlertModel pcdAlertMaster = new PcdAlertModel();
                    pcdAlertMaster.WeekNo = element.WeekNum.ToString();
                    pcdAlertMaster.LineNo = element.Line_no.ToString();
                    pcdAlertMaster.Atcnum = element.AtcMaster.AtcNum;
                    pcdAlertMaster.Atcid = element.AtcMaster.AtcId.ToString();
                    pcdAlertMaster.CutStartDate = element.Cut_Start_date.ToString();
                    pcdAlertMaster.ApprovalStatus = element.Approval_status.ToString();
                    pcdAlertMaster.SewingMaterialIssue = element.Sewing_Material_Issue == null ? "" : element.Sewing_Material_Issue;
                    pcdAlertMaster.BoRemarksSewing = element.BO_remarks_sewing == null ? "" : element.BO_remarks_sewing;
                    pcdAlertMaster.BoPlanSewingAccpet = element.Sewing_Bo_plan_accept == null ? "" : element.Sewing_Bo_plan_accept;
                    pcdAlertMaster.SewingAction = element.Sewing_action == null ? "" : element.Sewing_action;
                    pcdAlertMaster.Location_pk = element.Location_pk.ToString();
                    pcdAlertMaster.Pcdalert_pk = element.PcdAlert_Pk.ToString();
                    rk.Add(pcdAlertMaster);

                }


            }
            return rk;
        }

        public List<PcdAlertModel> get_pcdalert_packing(DateTime weekno, int location_pk)
        {

            List<PcdAlertModel> rk = new List<PcdAlertModel>();
            using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {

                DateTime curweek = DateTime.Now.Date;
                var a1 = enty.YearWeekMasters.Where(U => U.Fromdate <= curweek && U.Todate >= curweek).Select(u => u.Week_no).FirstOrDefault();
                insertPendingPacking(int.Parse(a1.ToString()));


                var a = enty.YearWeekMasters.Where(U => U.Fromdate <= weekno && U.Todate >= weekno).Select(u => u.Week_no).FirstOrDefault();
                Decimal datetimeofcountryindiais = Decimal.Parse(a.ToString());

                var q = from pcddetails in enty.PcdAlertMasters
                        where pcddetails.WeekNum == datetimeofcountryindiais && pcddetails.Location_pk == location_pk && pcddetails.IsActive == "Y" && pcddetails.type == "Packing"
                        select pcddetails;
                foreach (var element in q)
                {
                    PcdAlertModel pcdAlertMaster = new PcdAlertModel();
                    pcdAlertMaster.WeekNo = element.WeekNum.ToString();
                    pcdAlertMaster.LineNo = element.Line_no.ToString();
                    pcdAlertMaster.Atcnum = element.AtcMaster.AtcNum;
                    pcdAlertMaster.Atcid = element.AtcMaster.AtcId.ToString();
                    pcdAlertMaster.CutStartDate = element.Cut_Start_date.ToString();
                    pcdAlertMaster.ApprovalStatus = element.Approval_status.ToString();
                    pcdAlertMaster.PackingMaterialIssue = element.Packing_Material_Issue == null ? "" : element.Packing_Material_Issue;
                    pcdAlertMaster.BoRemarksPacking = element.BO_remarks_Packing == null ? "" : element.BO_remarks_Packing;
                    pcdAlertMaster.BoPlanPackingAccept = element.Packing_bo_plan_accept == null ? "" : element.Packing_bo_plan_accept;
                    pcdAlertMaster.PackingAction = element.Packing_action == null ? "" : element.Packing_action;
                    pcdAlertMaster.Location_pk = element.Location_pk.ToString();
                    pcdAlertMaster.Pcdalert_pk = element.PcdAlert_Pk.ToString();
                    rk.Add(pcdAlertMaster);

                }


            }
            return rk;
        }
        public void insertPending(int newweeknum)
        {
            string msg = "";
            using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {

                var q = from pcddetails in enty.PcdAlertMasters
                        where pcddetails.WeekNum < newweeknum && pcddetails.Sewing_action == "Pending" && pcddetails.IsActive == "Y"
                        select pcddetails;
                foreach (var element in q)
                {
                    PcdAlertMaster pcdAlertMaster = new PcdAlertMaster();
                    pcdAlertMaster.WeekNum = newweeknum;
                    pcdAlertMaster.Line_no = element.Line_no;
                    pcdAlertMaster.Atc_id = element.AtcMaster.AtcId;
                    pcdAlertMaster.Cut_Start_date = element.Cut_Start_date;
                    pcdAlertMaster.Approval_status = element.Approval_status.ToString();
                    pcdAlertMaster.Sewing_Material_Issue = element.Sewing_Material_Issue == null ? "" : element.Sewing_Material_Issue;
                    pcdAlertMaster.BO_remarks_sewing = element.BO_remarks_sewing == null ? "" : element.BO_remarks_sewing;
                    pcdAlertMaster.Sewing_Bo_plan_accept = element.Sewing_Bo_plan_accept == null ? "" : element.Sewing_Bo_plan_accept;
                    pcdAlertMaster.Sewing_action = element.Sewing_action == null ? "" : element.Sewing_action;
                    pcdAlertMaster.Location_pk = element.Location_pk;
                    pcdAlertMaster.Addedby = HttpContext.Current.Session["Username"].ToString();
                    pcdAlertMaster.Addeddate = DateTime.Now;
                    pcdAlertMaster.IsActive = "Y";
                    pcdAlertMaster.OldPcdAlert_Pk = element.PcdAlert_Pk;
                    pcdAlertMaster.type = "Sewing";
                    element.IsActive = "N";
                    enty.PcdAlertMasters.Add(pcdAlertMaster);
                }
                enty.SaveChanges();
            }

        }
        public void insertPendingPacking(int newweeknum)
        {
            string msg = "";
            using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {

                var q = from pcddetails in enty.PcdAlertMasters
                        where pcddetails.WeekNum < newweeknum && pcddetails.Packing_action == "Pending" && pcddetails.IsActive == "Y"
                        select pcddetails;
                foreach (var element in q)
                {
                    PcdAlertMaster pcdAlertMaster = new PcdAlertMaster();
                    pcdAlertMaster.WeekNum = newweeknum;
                    pcdAlertMaster.Line_no = element.Line_no;
                    pcdAlertMaster.Atc_id = element.AtcMaster.AtcId;
                    pcdAlertMaster.Cut_Start_date = element.Cut_Start_date;
                    pcdAlertMaster.Approval_status = element.Approval_status.ToString();
                    pcdAlertMaster.Sewing_Material_Issue = element.Packing_Material_Issue == null ? "" : element.Packing_Material_Issue;
                    pcdAlertMaster.BO_remarks_sewing = element.BO_remarks_Packing == null ? "" : element.BO_remarks_Packing;
                    pcdAlertMaster.Sewing_Bo_plan_accept = element.Packing_bo_plan_accept == null ? "" : element.Packing_bo_plan_accept;
                    pcdAlertMaster.Sewing_action = element.Packing_action == null ? "" : element.Packing_action;
                    pcdAlertMaster.Location_pk = element.Location_pk;
                    pcdAlertMaster.Addedby = HttpContext.Current.Session["Username"].ToString();
                    pcdAlertMaster.Addeddate = DateTime.Now;
                    pcdAlertMaster.IsActive = "Y";
                    pcdAlertMaster.OldPcdAlert_Pk = element.PcdAlert_Pk;
                    pcdAlertMaster.type = "Packing";
                    element.IsActive = "N";
                    enty.PcdAlertMasters.Add(pcdAlertMaster);
                }
                enty.SaveChanges();
            }

        }

        public List<PcdAlertModel> GetPCDSewingDetails(DateTime weekno, int location_pk)
        {

            List<PcdAlertModel> rk = new List<PcdAlertModel>();
            using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {

                var a = enty.YearWeekMasters.Where(U => U.Fromdate <= weekno && U.Todate >= weekno).Select(u => u.Week_no).FirstOrDefault();
                Decimal datetimeofcountryindiais = Decimal.Parse(a.ToString());

                var q = from pcddetails in enty.PcdAlertMasters
                        where pcddetails.WeekNum >= datetimeofcountryindiais && pcddetails.Location_pk == location_pk && pcddetails.IsActive == "Y" && pcddetails.type == "Sewing"
                        select pcddetails;
                foreach (var element in q)
                {
                    PcdAlertModel pcdAlertMaster = new PcdAlertModel();
                    pcdAlertMaster.WeekNo = element.WeekNum.ToString();
                    pcdAlertMaster.LineNo = element.Line_no.ToString();
                    pcdAlertMaster.Atcnum = element.AtcMaster.AtcNum;
                    pcdAlertMaster.Atcid = element.AtcMaster.AtcId.ToString();
                    pcdAlertMaster.CutStartDate = element.Cut_Start_date.ToString();
                    pcdAlertMaster.ApprovalStatus = element.Approval_status.ToString();
                    pcdAlertMaster.SewingMaterialIssue = element.Sewing_Material_Issue == null ? "" : element.Sewing_Material_Issue;
                    pcdAlertMaster.BoRemarksSewing = element.BO_remarks_sewing == null ? "" : element.BO_remarks_sewing;
                    pcdAlertMaster.BoPlanSewingAccpet = element.Sewing_Bo_plan_accept == null ? "" : element.Sewing_Bo_plan_accept;
                    pcdAlertMaster.SewingAction = element.Sewing_action == null ? "" : element.Sewing_action;
                    pcdAlertMaster.Location_pk = element.LocationMaster.LocationName;
                    pcdAlertMaster.Pcdalert_pk = element.PcdAlert_Pk.ToString();
                    rk.Add(pcdAlertMaster);

                }


            }
            return rk;
        }

        public List<PcdAlertModel> GetPCDPackingDetails(DateTime weekno, int location_pk)
        {

            List<PcdAlertModel> rk = new List<PcdAlertModel>();
            using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {

                var a = enty.YearWeekMasters.Where(U => U.Fromdate <= weekno && U.Todate >= weekno).Select(u => u.Week_no).FirstOrDefault();
                Decimal datetimeofcountryindiais = Decimal.Parse(a.ToString());

                var q = from pcddetails in enty.PcdAlertMasters
                        where pcddetails.WeekNum >= datetimeofcountryindiais && pcddetails.Location_pk == location_pk && pcddetails.IsActive == "Y" && pcddetails.type == "Packing"
                        select pcddetails;
                foreach (var element in q)
                {
                    PcdAlertModel pcdAlertMaster = new PcdAlertModel();
                    pcdAlertMaster.WeekNo = element.WeekNum.ToString();
                    pcdAlertMaster.LineNo = element.Line_no.ToString();
                    pcdAlertMaster.Atcnum = element.AtcMaster.AtcNum;
                    pcdAlertMaster.Atcid = element.AtcMaster.AtcId.ToString();
                    pcdAlertMaster.CutStartDate = element.Cut_Start_date.ToString();
                    pcdAlertMaster.ApprovalStatus = element.Approval_status.ToString();
                    pcdAlertMaster.PackingMaterialIssue = element.Packing_Material_Issue == null ? "" : element.Packing_Material_Issue;
                    pcdAlertMaster.BoRemarksSewing = element.BO_remarks_Packing == null ? "" : element.BO_remarks_Packing;
                    pcdAlertMaster.BoPlanSewingAccpet = element.Packing_bo_plan_accept == null ? "" : element.Packing_bo_plan_accept;
                    pcdAlertMaster.SewingAction = element.Packing_action == null ? "" : element.Packing_action;
                    pcdAlertMaster.Location_pk = element.LocationMaster.LocationName;
                    pcdAlertMaster.Pcdalert_pk = element.PcdAlert_Pk.ToString();
                    rk.Add(pcdAlertMaster);

                }


            }
            return rk;
        }
    }
}