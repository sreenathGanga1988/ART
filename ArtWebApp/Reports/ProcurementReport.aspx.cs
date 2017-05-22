using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using ArtWebApp.DataModels;
using ArtWebApp.Reports.Dataset.ReportDataSetTableAdapters;
namespace ArtWebApp.Reports
{
    public partial class ProcurementReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack )
            {
                FillAtcCombo();
            }

        }

        protected void Btn_show_Click(object sender, EventArgs e)
        {
           

            try
            {
                if (drp_Atc.SelectedItem.Value != null)
                {
                    loadPOreport();
                }
            }
            catch (Exception exp)
            {

               
            }
        }


        public void FillAtcCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from atcorder in entty.AtcMasters
                       select new
                        {
                            name = atcorder.AtcNum,
                            pk = atcorder.AtcId
                        };

                // Create a table from the query.
                drp_Atc.DataSource = q.ToList();
                drp_Atc.DataBind();


               

            }
        }

        public void FillPOCombo(int  atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.ProcurementMasters
                        where ponmbr.AtcId==atcid
                        select new
                        {
                            name = ponmbr.PONum,
                            pk = ponmbr.PO_Pk
                        };
            
                drp_PO.DataSource = q.ToList();
                drp_PO.DataBind();




            }
        }

        public void loadPOreport()
        {
       
          
            
            DBTransaction.ProcurementTransaction potrsans = new DBTransaction.ProcurementTransaction();

          //  DataTable dt = potrsans.GetPOData(drp_PO.SelectedItem.Text.Trim());
            DataTable dt = potrsans.GetPOData(int.Parse( drp_PO.SelectedValue.ToString().Trim()));
            
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            if (dt.Rows[0]["POType"].ToString().Trim() == "F")
            {
                this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\FPO.rdlc";
            }
            else
            {
                this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\APO.rdlc";
            }



            this.ReportViewer1.LocalReport.DisplayName = drp_PO.SelectedItem.ToString ();

        }



        public void loaDSPOreport()
        {



            DBTransaction.ProcurementTransaction potrsans = new DBTransaction.ProcurementTransaction();

            DataTable dt = potrsans.GetsPOData(int.Parse ( drp_spo.SelectedItem.Value.ToString()));


            var potype= getIPOofSpo(int.Parse(drp_spo.SelectedItem.Value.ToString()));
            ReportParameter rp1 = new ReportParameter("Potype", potype);

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = drp_spo.SelectedItem.ToString();
           
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\sPO.rdlc";
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });

        }


        public String getIPOofSpo(int spopk)
        {
            string potype = "";
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {

                var sizedetails = (from stoforoodo in entty.StocPOForODOOs 
                                   join oodogpo in entty.ODOOGPOMasters on stoforoodo.POId equals oodogpo.POId

                                   where stoforoodo.Spo_PK == spopk
                                   select new
                                   {
                                       oodogpo.PONum
                                   }).Distinct();

               foreach (var element in sizedetails)
               {

                    if (potype == "")
                    {
                        potype = potype +  element.PONum;
                    }
                    else
                    {
                        potype = potype + "/" + element.PONum;
                    }
                   
               }



            }

            if(potype=="")
            {
                potype = "Local";
            }

            return potype;
        }


        public void loaDServicePOreport()
        {



            DBTransaction.ProcurementTransaction potrsans = new DBTransaction.ProcurementTransaction();

            DataTable dt = potrsans.GetServicepo(int.Parse(drp_servicepo.SelectedItem.Value.ToString()));

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.DisplayName = drp_servicepo.SelectedItem.ToString();
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\servicepo.rdlc";


        }


        public void loadPOStatusreport()
        {
            
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\POStatus.rdlc";
            this.ReportViewer1.LocalReport.DataSources.Clear();

            //ProcurementDataTableAdapter padapt = new ProcurementDataTableAdapter();
          
            //RecieptDataTableAdapter adapt = new RecieptDataTableAdapter();
            //DBTransaction.ProcurementTransaction potrsans = new DBTransaction.ProcurementTransaction();
            DBTransaction.ProcurementReports poreptrsans = new DBTransaction.ProcurementReports();
            DataTable dt = poreptrsans.GetPOStatusByPO(drp_PO.SelectedItem.Text.Trim());
            DataTable dt1 = new DataTable();
            dt1 = poreptrsans.GetReciptDetailsByPO(int.Parse(drp_PO.SelectedItem.Value.ToString()));
            DataTable dt2 = new DataTable();
            dt2 = poreptrsans.GetInvoiceDetailsByPO(int.Parse(drp_PO.SelectedItem.Value.ToString()));

            DataTable dt3 = new DataTable();
            dt3 = poreptrsans.GetDocumentByPO(int.Parse(drp_PO.SelectedItem.Value.ToString()));



            
            //DataTable dt = padapt.GetDataBy(drp_PO.SelectedItem.Text.Trim());
            //dt1 = adapt.GetDataByPO(int.Parse(drp_PO.SelectedItem.Value.ToString()));
            //Microsoft.Reporting.WebForms.ReportDataSource DataSet1= new ReportDataSource () ;
            //DataSet1.Value = dt;
            //Microsoft.Reporting.WebForms.ReportDataSource DataSet2 = new ReportDataSource();
            //DataSet2.Value = dt1;
            // this.ReportViewer1.LocalReport.DataSources.Add(DataSet1);
            // this.ReportViewer1.LocalReport.DataSources.Add(DataSet2);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rd1 = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Add(rd1);
            if (dt1 != null)
            {
                ReportDataSource rd2 = new ReportDataSource("DataSet2", dt1);
                ReportViewer1.LocalReport.DataSources.Add(rd2);
            }
            if (dt2 != null)
            {
                ReportDataSource rd3 = new ReportDataSource("DataSet3", dt2);
                ReportViewer1.LocalReport.DataSources.Add(rd3);
            }
            if (dt3 != null)
            {
                ReportDataSource rd4 = new ReportDataSource("DataSet4", dt3);
                ReportViewer1.LocalReport.DataSources.Add(rd4);
            }
            ReportViewer1.LocalReport.Refresh();

        }


        public void loadsPOStatusreport()
        {

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\SPOstatus.rdlc";
            this.ReportViewer1.LocalReport.DataSources.Clear();

            //ProcurementDataTableAdapter padapt = new ProcurementDataTableAdapter();

            //RecieptDataTableAdapter adapt = new RecieptDataTableAdapter();
            //DBTransaction.ProcurementTransaction potrsans = new DBTransaction.ProcurementTransaction();

            DBTransaction.ProcurementTransaction potrsans = new DBTransaction.ProcurementTransaction();
            DataTable dt = potrsans.GetsPOData(int.Parse(drp_spo.SelectedItem.Value.ToString()));


            DataTable dt1 = new DataTable();
            dt1 = potrsans.Getspomrn(int.Parse(drp_spo.SelectedItem.Value.ToString()));


            DataTable dt2 = new DataTable();
            dt2 = potrsans.GetSpoiNVOICEDetails(int.Parse(drp_spo.SelectedItem.Value.ToString()));

            //DataTable dt = padapt.GetDataBy(drp_PO.SelectedItem.Text.Trim());
            //dt1 = adapt.GetDataByPO(int.Parse(drp_PO.SelectedItem.Value.ToString()));
            //Microsoft.Reporting.WebForms.ReportDataSource DataSet1= new ReportDataSource () ;
            //DataSet1.Value = dt;
            //Microsoft.Reporting.WebForms.ReportDataSource DataSet2 = new ReportDataSource();
            //DataSet2.Value = dt1;
            // this.ReportViewer1.LocalReport.DataSources.Add(DataSet1);
            // this.ReportViewer1.LocalReport.DataSources.Add(DataSet2);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rd1 = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Add(rd1);
            if (dt1 != null)
            {
                ReportDataSource rd2 = new ReportDataSource("DataSet2", dt1);
                ReportViewer1.LocalReport.DataSources.Add(rd2);
            }
            if (dt2 != null)
            {
                ReportDataSource rd3 = new ReportDataSource("DataSet3", dt2);
                ReportViewer1.LocalReport.DataSources.Add(rd3);
            }

            ReportViewer1.LocalReport.Refresh();

        }


        public void loadPOreport(string ponum)
        {



            DBTransaction.ProcurementTransaction potrsans = new DBTransaction.ProcurementTransaction();

            //  DataTable dt = potrsans.GetPOData(drp_PO.SelectedItem.Text.Trim());
            DataTable dt = potrsans.GetPOData(ponum);

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            if (dt.Rows[0]["POType"].ToString().Trim() == "F")
            {
                this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\FPO.rdlc";
            }
            else
            {
                this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\APO.rdlc";
            }

              ;

        }







        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
            if (drp_Atc.SelectedItem.Value != null)
            {
                FillPOCombo(int.Parse(drp_Atc.SelectedValue.ToString()));
            }
            }
            catch (Exception exp)
            {

                Response.Write(exp.ToString());
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            loaDSPOreport();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            loadPOStatusreport();
        }

        protected void btn_cuttingticketbom_Click(object sender, EventArgs e)
        {
            BLL.MerchandsingBLL.BOMData bmdata = new BLL.MerchandsingBLL.BOMData();
            DataTable BomData = bmdata.ShowBOM(int.Parse(drp_Atc.SelectedValue.ToString()));
            ReportDataSource datasource = new ReportDataSource("DataSet1", BomData);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\CuttingBOM.rdlc";
        }

        public void ShowBOM()
        {

          
         
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                loadPOreport(txt_ponum.Text.Trim());
            }
            catch (Exception)
            {

                
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            loadsPOStatusreport();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            loaDServicePOreport();
        }
    }
}