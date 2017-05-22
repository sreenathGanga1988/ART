using ArtWebApp.DataModels;
using ArtWebApp.Reports.Dataset.ReportDataSetTableAdapters;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ArtWebApp.Sampling
{
    public partial class ShowSamplingstatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String Message = "Cutreq assigned to Mr " + drp_master.SelectedItem.Text + " From " + dtp_fromdate.Value.ToString() + "to " + dtp_todate.Value.ToString();

            loadDORReport(getAlldataofmasterOfPeriod(), Message); ;
        }



        public void loadDORReport(DataTable dt,String reporthead)
        {

            int assignedtotal = 0;

            int receivedtotal = 0;

            int SignedbyMaster = 0;

            int MarkCompleted = 0;
          



          




            if (dt!=null)
            {
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        receivedtotal = dt.Rows.Count;
                    }
                    catch (Exception)
                    {
                        receivedtotal = 0;


                    }


                    try
                    {
                        DataTable newresult = dt.Select("PaternMasterName is not null").CopyToDataTable();
                        assignedtotal = newresult.Rows.Count;
                    }
                    catch (Exception)
                    {
                        assignedtotal = 0;


                    }

                    try
                    {
                        DataTable newresult = dt.Select("SignedBYMaster = 1").CopyToDataTable();
                        SignedbyMaster = newresult.Rows.Count;
                    }
                    catch (Exception)
                    {
                        SignedbyMaster = 0;


                    }

                    try
                    {
                        DataTable newresult = dt.Select("MarkCompleted = 1").CopyToDataTable();
                        MarkCompleted = newresult.Rows.Count;
                    }
                    catch (Exception)
                    {
                        MarkCompleted = 0;


                    }
                    
                }




            }
            

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "D:\\report1.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\SampCutreqReport.rdlc";
            ReportParameter Heading = new ReportParameter("Heading", reporthead);
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Heading });
            
            ReportParameter Recieved = new ReportParameter("Recieved", receivedtotal.ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Recieved });

            ReportParameter Assigned = new ReportParameter("Assigned", assignedtotal.ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Assigned });

            ReportParameter Signed = new ReportParameter("Signed", SignedbyMaster.ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Signed });

            ReportParameter Completed = new ReportParameter("Completed", MarkCompleted.ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Completed });
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            
                 String Message = "Cutreq  Received in sampling From " + dtp_fromdate.Value.ToString() + "to " + dtp_todate.Value.ToString();

            loadDORReport(getAlldataofperiod(), Message); ;
        }



        public DataTable getAlldata()
        {

            string Qry = @"SELECT        SampCutReqMaster.ReqNum, BuyerMaster.BuyerName, PatterRefMaster.PatterRefNum, SampCutReqMaster.Fabric, SampleType.SampleType, SampCutReqMaster.SampleRequiredDate, 
                         SamCutAssignmentMaster.PatternReqDate, SampCutReqMaster.SizeDetail, SampCutReqMaster.Qty, SamCutAssignmentMaster.ReceivedDate, SamCutAssignmentMaster.ReceivedBy, 
                         SamCutAssignmentMaster.AssignedDate, SamCutAssignmentMaster.SignedDate, SamCutAssignmentMaster.CompletedDate, SamCutAssignmentMaster.Remark, SamCutAssignmentMaster.PatternCompletedDate, 
                         PatternMaster.PaternMasterName, SamCutAssignmentMaster.SignedBYMaster, SampCutReqMaster.MarkCompleted, PatternMaster.PatternMasterID
FROM            SamCutAssignmentMaster INNER JOIN
                         SampCutReqMaster ON SamCutAssignmentMaster.SampCutreqID = SampCutReqMaster.SampCutreqID INNER JOIN
                         SampleType ON SampCutReqMaster.SampleTypeID = SampleType.SampleTypeID INNER JOIN
                         BuyerMaster ON SampCutReqMaster.BuyerID = BuyerMaster.BuyerID INNER JOIN
                         PatterRefMaster ON SampCutReqMaster.PatternRefID = PatterRefMaster.PatternRefID LEFT OUTER JOIN
                         PatternMaster ON SamCutAssignmentMaster.PatternMasterID = PatternMaster.PatternMasterID";



return QueryFunctions.ReturnQueryResultDatatable(Qry);
        }

        public DataTable getAlldataofperiod()
        {


            string Qry = @"SELECT        SampCutReqMaster.ReqNum, BuyerMaster.BuyerName, PatterRefMaster.PatterRefNum, SampCutReqMaster.Fabric, SampleType.SampleType, SampCutReqMaster.SampleRequiredDate, 
                         SamCutAssignmentMaster.PatternReqDate, SampCutReqMaster.SizeDetail, SampCutReqMaster.Qty, SamCutAssignmentMaster.ReceivedDate, SamCutAssignmentMaster.ReceivedBy, 
                         SamCutAssignmentMaster.AssignedDate, SamCutAssignmentMaster.SignedDate, SamCutAssignmentMaster.CompletedDate, SamCutAssignmentMaster.Remark, SamCutAssignmentMaster.PatternCompletedDate, 
                         PatternMaster.PaternMasterName, SamCutAssignmentMaster.SignedBYMaster, SampCutReqMaster.MarkCompleted, PatternMaster.PatternMasterID
FROM            SamCutAssignmentMaster INNER JOIN
                         SampCutReqMaster ON SamCutAssignmentMaster.SampCutreqID = SampCutReqMaster.SampCutreqID INNER JOIN
                         SampleType ON SampCutReqMaster.SampleTypeID = SampleType.SampleTypeID INNER JOIN
                         BuyerMaster ON SampCutReqMaster.BuyerID = BuyerMaster.BuyerID INNER JOIN
                         PatterRefMaster ON SampCutReqMaster.PatternRefID = PatterRefMaster.PatternRefID LEFT OUTER JOIN
                         PatternMaster ON SamCutAssignmentMaster.PatternMasterID = PatternMaster.PatternMasterID
WHERE         (SamCutAssignmentMaster.ReceivedDate BETWEEN '" + DateTime.Parse(dtp_fromdate.Value.ToString()) + "' AND '" + DateTime.Parse(dtp_todate.Value.ToString()) + "' )";





            return QueryFunctions.ReturnQueryResultDatatable(Qry);
        }

        public DataTable getAlldataofmasterOfPeriod()
        {

            string Qry = @"SELECT        SampCutReqMaster.ReqNum, BuyerMaster.BuyerName, PatterRefMaster.PatterRefNum, SampCutReqMaster.Fabric, SampleType.SampleType, SampCutReqMaster.SampleRequiredDate, 
                         SamCutAssignmentMaster.PatternReqDate, SampCutReqMaster.SizeDetail, SampCutReqMaster.Qty, SamCutAssignmentMaster.ReceivedDate, SamCutAssignmentMaster.ReceivedBy, 
                         SamCutAssignmentMaster.AssignedDate, SamCutAssignmentMaster.SignedDate, SamCutAssignmentMaster.CompletedDate, SamCutAssignmentMaster.Remark, SamCutAssignmentMaster.PatternCompletedDate, 
                         PatternMaster.PaternMasterName, SamCutAssignmentMaster.SignedBYMaster, SampCutReqMaster.MarkCompleted, PatternMaster.PatternMasterID
FROM            SamCutAssignmentMaster INNER JOIN
                         SampCutReqMaster ON SamCutAssignmentMaster.SampCutreqID = SampCutReqMaster.SampCutreqID INNER JOIN
                         SampleType ON SampCutReqMaster.SampleTypeID = SampleType.SampleTypeID INNER JOIN
                         BuyerMaster ON SampCutReqMaster.BuyerID = BuyerMaster.BuyerID INNER JOIN
                         PatterRefMaster ON SampCutReqMaster.PatternRefID = PatterRefMaster.PatternRefID LEFT OUTER JOIN
                         PatternMaster ON SamCutAssignmentMaster.PatternMasterID = PatternMaster.PatternMasterID
WHERE        (PatternMaster.PatternMasterID = "+int.Parse (drp_master.SelectedValue.ToString ())+") AND (SamCutAssignmentMaster.ReceivedDate BETWEEN '" + DateTime.Parse(dtp_fromdate.Value.ToString())+ "' AND '" + DateTime.Parse(dtp_todate.Value.ToString()) + "' )";



            return QueryFunctions.ReturnQueryResultDatatable(Qry);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            loadDORReport(getAlldata(), "Total Report of sampling");
        }
    }
}