using Infragistics.Web.UI.ListControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser.Atc_Chart
{
    public partial class StockADN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_confirmPO_Click(object sender, EventArgs e)
        {
            Adddata();
        }


        public void Adddata()
        {
            List<DropDownItem> items = drp_po.SelectedItems;
            ArtWebApp.BLL.MerchandsingBLL.AtcChartBLL atcbll = new BLL.MerchandsingBLL.AtcChartBLL();
            tbl_Podetails.DataSource = atcbll.getMultiSelectedStockPoData(items);
            tbl_Podetails.DataBind();
            upd_grid.Update();
        }

        protected void btn_savercpt_Click(object sender, EventArgs e)
        {
            String msg = InsertDocRecieptmasterData();
            lbl_errordisplayer.Text = msg;
            drp_rcpt.DataBind();
            udp_drprcpt.Update();
        }

        /// <summary>
        /// insert the Docreciept
        /// </summary>

        public String InsertDocRecieptmasterData()
        {
            String rcptnum = "";
            BLL.MerchandsingBLL.StockDocumentReceiptdata rcptmstrdata = new BLL.MerchandsingBLL.StockDocumentReceiptdata();


            rcptmstrdata.ContainerNum = txt_container.Text.Trim();
            rcptmstrdata.Supplier_PK = int.Parse(drp_supplier.SelectedValue.ToString());
            rcptmstrdata.BOENum = txt_boe.Text.Trim();
            rcptmstrdata.ETADate = DateTime.Parse(dtp_deliverydate.Value.ToString());
            rcptmstrdata.Remark = txta_remark.Value.ToString();
            rcptmstrdata.InhouseDate = DateTime.Parse(dtp_deliverydate.Value.ToString());
            rcptmstrdata.AddedBy = Session["Username"].ToString().Trim();
            rcptmstrdata.IsCompleted = "N";
            rcptmstrdata.Location_PK = int.Parse(Session["UserLoc_pk"].ToString());
            rcptmstrdata.currency_Pk = int.Parse(drp_supplier.SelectedValue.ToString());
            rcptmstrdata.docvalue = decimal.Parse(txt_docvalue.Text);
            rcptnum = rcptmstrdata.InsertReciptMstr(rcptmstrdata);


            String msg = "Doc # : " + rcptnum + " is generated Sucessfully";
            //       MessageBoxShow(msg);

            return msg;
        }





        protected void btn_confirmRcpt_Click(object sender, EventArgs e)
        {
            hdn_rcptnum.Value = drp_rcpt.SelectedValue.ToString();
            BLL.MerchandsingBLL.StockDocumentReceiptdata rcptmstrdata = new BLL.MerchandsingBLL.StockDocumentReceiptdata();

            //drp_po.DataBind();
            //udp_drppo.Update();

            podata.DataBind();
            drp_po.DataBind();
            dtp_eta.Value = rcptmstrdata.geteta(int.Parse(drp_rcpt.SelectedValue.ToString()));
            upd_eta.Update();
        }

        protected void btn_saveMrn_Click(object sender, EventArgs e)
        {


            if (checkdatagridValue(tbl_Podetails, "lbl_bal", "txt_qty"))
            {
                InsertPodetails();
                tbl_Podetails.DataSource = null;
                tbl_Podetails.DataBind();
                upd_grid.Update();
                lbl_errordisplayer.Text = "Details Added";
            }
            else
            {
                string msg = "Entered CM is greater than Approved CM Please Revise the Costing";
                lbl_errordisplayer.Text = msg;
            }


        }
        public Boolean checkdatagridValue(GridView tblgrid, String lbl_Qty1, String txt_Qty2)
        {

            Boolean isQtyok = true;
            for (int i = 0; i < tblgrid.Rows.Count; i++)
            {
                GridViewRow currentRow = tblgrid.Rows[i];
                CheckBox chkBx = (CheckBox)currentRow.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    try
                    {

                        float AllowedQty = float.Parse(((tblgrid.Rows[i].FindControl(lbl_Qty1) as Label).Text.ToString()));
                        float Enterqty = float.Parse(((tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).Text.ToString()));
                        if (!QuantityValidator.ISFloatQuantityLesser(AllowedQty, Enterqty))
                        {
                            isQtyok = false;
                            (tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).BackColor = System.Drawing.Color.Red;


                        }
                        else
                        {
                            (tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).BackColor = System.Drawing.Color.White;
                        }

                    }
                    catch (Exception)
                    {
                        isQtyok = false;
                        (tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).BackColor = System.Drawing.Color.Red;

                    }
                }







            }
            return isQtyok;
        }


        public void InsertPodetails()
        {
            BLL.MerchandsingBLL.DocumentReceiptdata rcptmstrdata = new BLL.MerchandsingBLL.DocumentReceiptdata();
            rcptmstrdata.DocumentDetailsDataCollection = GetPODetailsData();
            rcptmstrdata.insertPoEtaData(rcptmstrdata);
        }

        public void MessageBoxShow(String msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + msg + "');", true);
        }

        protected void dtp_eta_ValueChanged(object sender, Infragistics.Web.UI.EditorControls.TextEditorValueChangedEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    Infragistics.Web.UI.EditorControls.WebDatePicker wbdt = di.FindControl("wdp_etadate") as Infragistics.Web.UI.EditorControls.WebDatePicker;


                    wbdt.Date = DateTime.Parse(dtp_eta.Date.ToString());
                }
            }
            upd_grid.Update();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    TextBox wbdt = di.FindControl("txt_do") as TextBox;


                    wbdt.Text = txt_deliverynote.Text.Trim();
                }
            }
            upd_grid.Update();
        }








        public List<BLL.MerchandsingBLL.DocPodetaildata> GetPODetailsData()
        {

            List<BLL.MerchandsingBLL.DocPodetaildata> rk = new List<BLL.MerchandsingBLL.DocPodetaildata>();
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {


                    int podet_pk = int.Parse(((di.FindControl("lbl_podet_pk") as Label).Text.ToString()));
                    decimal recieptqty = decimal.Parse((di.FindControl("txt_qty") as TextBox).Text.ToString());
                    decimal txt_newExcessqty = decimal.Parse((di.FindControl("txt_newExcessqty") as TextBox).Text.ToString());
                    String invnum = (di.FindControl("txt_do") as TextBox).Text;
                    Infragistics.Web.UI.EditorControls.WebDatePicker wbdt = di.FindControl("wdp_etadate") as Infragistics.Web.UI.EditorControls.WebDatePicker;
                    BLL.MerchandsingBLL.DocPodetaildata podetdata = new BLL.MerchandsingBLL.DocPodetaildata();

                    podetdata.Doc_Pk = int.Parse(drp_rcpt.SelectedItem.Value.ToString());
                    podetdata.podet_PK = podet_pk;
                    podetdata.Qty = recieptqty;
                    podetdata.InvNum = invnum;
                    podetdata.ETADate = wbdt.Date;
                    podetdata.eXCESSQty = txt_newExcessqty;

                    podetdata.AddedDate = DateTime.Now;
                    podetdata.AddedBy = Session["Username"].ToString().Trim();
                    rk.Add(podetdata);
                }
            }



            return rk;


        }

        protected void btn_confirmatc_Click(object sender, EventArgs e)
        {
           

            drp_po.DataBind();
            udp_drppo.Update();
        }
    }
}