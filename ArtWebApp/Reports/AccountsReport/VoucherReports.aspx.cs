using ArtWebApp.DataModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.AccountsReport
{
    public partial class VoucherReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillVoucherCombo();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (drp_vouchertype.SelectedValue.ToString() == "PUR")
            {
                showreport();
            }
            else if (drp_vouchertype.SelectedValue.ToString() == "INV")
            {
                showInvreport();
            }
            else if (drp_vouchertype.SelectedValue.ToString() == "SPUR")
            {
                showSPURInvreport();
            }

        }


        public void FillVoucherCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from atcorder in entty.SupplierInvoiceMasters
                        select new
                        {
                            name = atcorder.SupplierInvoiceNum,
                            pk = atcorder.SupplierInvoice_PK
                        };

                // Create a table from the query.
                drp_voucher.DataSource = q.ToList();
                drp_voucher.DataBind();




            }
        }

        public void FillSPURVoucherCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from atcorder in entty.SupplierStockInvoiceMasters
                        select new
                        {
                            name = atcorder.SupplierStockInvoiceNum,
                            pk = atcorder.SupplierStockInvoice_PK
                        };

                // Create a table from the query.
                drp_voucher.DataSource = q.ToList();
                drp_voucher.DataBind();




            }
        }

        public void FillBuyerVoucherCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from atcorder in entty.InvoiceMasters
                        select new
                        {
                            name = atcorder.InvoiceNum,
                            pk = atcorder.Invoice_PK
                        };

                // Create a table from the query.
                drp_voucher.DataSource = q.ToList();
                drp_voucher.DataBind();




            }
        }



        public void showreport()
        {
            DBTransaction.ReportTransactions.AccountReportrans  acttrsans = new DBTransaction.ReportTransactions.AccountReportrans();

            System.Data.DataTable dt = acttrsans.GetSUPInvData(int.Parse(drp_voucher.SelectedValue.ToString().Trim()));


            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\PUR.rdlc";
        }


        public void showInvreport()
        {
            DBTransaction.ReportTransactions.AccountReportrans acttrsans = new DBTransaction.ReportTransactions.AccountReportrans();

            System.Data.DataTable dt = acttrsans.GetBuyerInvData(int.Parse(drp_voucher.SelectedValue.ToString().Trim()));


            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\InvoiceReport.rdlc";
        }


        public void showSPURInvreport()
        {
            DBTransaction.ReportTransactions.AccountReportrans acttrsans = new DBTransaction.ReportTransactions.AccountReportrans();

            System.Data.DataTable dt = acttrsans.GetSPURInvData(int.Parse(drp_voucher.SelectedValue.ToString().Trim()));


            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\SPURREPORT.rdlc";
        }

        protected void DropDownListChosen1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_vouchertype.SelectedValue.ToString() == "PUR")
            {
                FillVoucherCombo();
            }
            else if (drp_vouchertype.SelectedValue.ToString() == "INV")
            {
                FillBuyerVoucherCombo();
            }
            else if (drp_vouchertype.SelectedValue.ToString() == "SPUR")
            {
                FillSPURVoucherCombo();
            }
        }
    }
}