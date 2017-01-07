using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.Qualityreports
{
    public partial class PendingReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }




        protected void btn_showPO_Click(object sender, EventArgs e)
        {
            showreport();
        }


        public void showreport()
        {
            //GetasnData

            DBTransaction.ReportTransactions.QualityReportTran RPTRN = new DBTransaction.ReportTransactions.QualityReportTran();

            DataTable dt = RPTRN.GetASNREport(int.Parse(drp_asn.SelectedValue.ToString()), int.Parse(drp_color.SelectedValue.ToString()));





            //ReportDataSource datasource = new ReportDataSource("DataSet2", dt);
            //this.ReportViewer1.LocalReport.DataSources.Clear();
            //this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            //this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\ASNREPORT.rdlc";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (drp_atc.SelectedValue != null)
                {
                    FillAsnCombo(int.Parse(drp_atc.SelectedValue.ToString()));

                }
            }
            catch (Exception exp)
            {


            }



        }


        public void FillAsnCombo(int atcid)
        {
            BLL.InventoryBLL.InspectionData fbrolldet = new BLL.InventoryBLL.InspectionData();

            drp_asn.DataSource = fbrolldet.GetDocumentnumber(int.Parse(drp_atc.SelectedValue.ToString()));
            drp_asn.DataValueField = "pk";
            drp_asn.DataTextField = "name";
            drp_asn.DataBind();
            UPD_ASN.Update();




        }

        public void FillFabricombo(int asn_pk, int atcid)
        {

            drp_color.DataSource = BLL.InventoryBLL.RollTransactionBLL.getfabricinsideASN(asn_pk, atcid);

            drp_color.DataValueField = "SkuDet_PK";
            drp_color.DataTextField = "ItemDescription";
            drp_color.DataBind();
            upd_color.Update();



        }














        protected void btn_Asn_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_asn.SelectedValue != null)
                {
                    FillFabricombo(int.Parse(drp_asn.SelectedValue.ToString()), int.Parse(drp_atc.SelectedValue.ToString()));
                }
            }
            catch (Exception exp)
            {


            }
        }


        protected void btn_fabric_Click1(object sender, EventArgs e)
        {
            showreport();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DBTransaction.ReportTransactions.QualityReportTran RPTRN = new DBTransaction.ReportTransactions.QualityReportTran();

            DataTable dt = RPTRN.GetRollsActionPending("Validation");
            //WebDataGrid1.DataSource = dt;
            //WebDataGrid1.DataBind();

            HiddenField1.Value = "Inspection";
            WebDataGrid1.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            HiddenField1.Value = "Validation";
            WebDataGrid1.DataBind();
        }
    }
}