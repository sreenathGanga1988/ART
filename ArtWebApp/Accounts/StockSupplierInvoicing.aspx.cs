using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Accounts
{
    public partial class StockSupplierInvoicing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            loadPO(int.Parse(drp_supplier.SelectedItem.Value.ToString()));
        }

       
           
        




        public void loadPO(int supplierpk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.StockPOMasters
                        where ponmbr.Supplier_Pk == supplierpk 
                        select new
                        {
                            name = ponmbr.SPONum,
                            pk = ponmbr.SPO_Pk
                        };

                drp_po.DataSource = q.ToList();
                drp_po.DataBind();
                udp_drppo.Update();
            }
        }

        public void loadPodetailgrid()
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_po.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int popackid = int.Parse(item.Value.ToString());
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {
                BLL.PoPackMasterData pkmstrdata = new BLL.PoPackMasterData();

                DataTable dt= BLL.AccountsBLL.InvoicingBLL.GetSPOInvoicedDetails(popaklist);
                tbl_Podetails.DataSource = dt;
                tbl_Podetails.DataBind();

                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("BalanceQty"));
                tbl_Podetails.FooterRow.Cells[11].Text = total.ToString("N2");
                upd_Grid.Update();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            loadPodetailgrid();
        }

        protected void btn_confirminvdet_Click(object sender, EventArgs e)
        {
            lbl_invValue.Text = "0";
            if (checkdatagridValue(tbl_Podetails, "lbl_balncetoinvqty", "txt_invQty"))
            {

                lbl_invValue.Text = calculategridValue().ToString();
                upd_invvalue.Update();

            }
        }

        public Boolean CheckCurrency()
        {

            Boolean isQtyok = true;
            for (int i = 0; i < tbl_Podetails.Rows.Count; i++)
            {
                GridViewRow currentRow = tbl_Podetails.Rows[i];
                CheckBox chkBx = (CheckBox)currentRow.FindControl("chk_select");
                if (chkBx != null && chkBx.Checked)
                {
                    try
                    {
                        String currecnycode = drp_currency.SelectedItem.Text.Trim();

                        String lbl_Currency = ((currentRow.FindControl("lbl_Currency") as Label).Text);
                        if (lbl_Currency.Trim() != currecnycode.Trim())
                        {
                            isQtyok = false;
                            (tbl_Podetails.Rows[i].FindControl("lbl_Currency") as Label).BackColor = System.Drawing.Color.Red;
                            drp_currency.BackColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            (tbl_Podetails.Rows[i].FindControl("lbl_Currency") as Label).BackColor = System.Drawing.Color.White;

                        }




                    }
                    catch (Exception)
                    {
                        isQtyok = false;
                        (tbl_Podetails.Rows[i].FindControl("lbl_Currency") as Label).BackColor = System.Drawing.Color.Red;

                    }
                }
            }
            upd_Grid.Update();
            return isQtyok;
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
            upd_Grid.Update();
            return isQtyok;
        }




        public float calculategridValue()
        {


            float invvalue = 0;
            for (int i = 0; i < tbl_Podetails.Rows.Count; i++)
            {
                GridViewRow currentRow = tbl_Podetails.Rows[i];
                CheckBox chkBx = (CheckBox)currentRow.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    try
                    {

                        float Invqty = float.Parse(((tbl_Podetails.Rows[i].FindControl("txt_invQty") as TextBox).Text.ToString()));
                        float pounitrate = float.Parse(((tbl_Podetails.Rows[i].FindControl("lbl_porate") as Label).Text.ToString()));

                        invvalue = invvalue + (Invqty * pounitrate);
                    }
                    catch (Exception)
                    {

                        (tbl_Podetails.Rows[i].FindControl("txt_invQty") as TextBox).BackColor = System.Drawing.Color.Red;

                    }
                }



            }

            return invvalue;
        }

        protected void drp_currency_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
        {
            String currencycode = drp_currency.SelectedValue.ToString();
        }

        protected void drp_currency_DataBound(object sender, EventArgs e)
        {

        }

        public void MessgeboxUpdate(String Messagetype, String Messg)
        {
            if (Messagetype == "sucess")
            {
                Messaediv.Attributes["class"] = "success";
                Messaediv.InnerText = Messg;
            }
            else
            {

            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkdatagridValue(tbl_Podetails, "lbl_balncetoinvqty", "txt_invQty"))
            {
                if (CheckCurrency())
                {


                    String invnum = InsertInvoice();

                    invnum = "Payable Voucher " + invnum + "Generated Sucessfully";
                    MessgeboxUpdate("sucess", invnum);



                    tbl_Podetails.DataSource = null;
                    tbl_Podetails.DataBind();
                    upd_Grid.Update();
                }
            }
        }

        public String InsertInvoice()
        {

            BLL.AccountsBLL.InvoiceMasterData mrnmstr = new BLL.AccountsBLL.InvoiceMasterData();

            mrnmstr.Remark = txt_remark.Text;
            mrnmstr.AddedDate = DateTime.Now;
            mrnmstr.AddedBy = Session["Username"].ToString().Trim();
            mrnmstr.Supplier_PK = int.Parse(drp_supplier.SelectedItem.Value.ToString());
            mrnmstr.LocationPK_pk = int.Parse(Session["UserLoc_pk"].ToString());
            mrnmstr.Currency_PK = int.Parse(drp_currency.SelectedItem.Value.ToString());
            mrnmstr.AccountDate = DateTime.Parse(dtp_accountdate.Value.ToString());
            mrnmstr.Invoicedate = DateTime.Parse(dtp_invdate.Value.ToString());
            mrnmstr.Supinvnum = txt_suppinvoicenum.Text.Trim();
            if (chk_advance.Checked == false)
            { mrnmstr.IsAdvance = "N"; }
            else
            {
                mrnmstr.IsAdvance = "Y";
            }

            mrnmstr.StockInvoiceDetDataCollection = GetInvoiceDetData();

            String Invnum = mrnmstr.InsertSPOInvoice(mrnmstr);

            return Invnum;
        }
        public List<BLL.AccountsBLL.StockInvoiceDetData> GetInvoiceDetData()
        {

            List<BLL.AccountsBLL.StockInvoiceDetData> rk = new List<BLL.AccountsBLL.StockInvoiceDetData>();


            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int spodetpk = int.Parse(((di.FindControl("lbl_podet_pk") as Label).Text.ToString()));

                    Decimal invqty = Decimal.Parse(((di.FindControl("txt_invQty") as TextBox).Text.ToString()));
                    Decimal unitrate = Decimal.Parse(((di.FindControl("lbl_porate") as Label).Text.ToString()));

                    String lbl_Currency = ((di.FindControl("lbl_Currency") as Label).Text);



                    BLL.AccountsBLL.StockInvoiceDetData shpdet = new BLL.AccountsBLL.StockInvoiceDetData();
                    shpdet.SpoPodet_Pk = spodetpk;
                    shpdet.invoiceQty = invqty;
                    shpdet.Unitrate = unitrate;
                    shpdet.InvCurrency = lbl_Currency;


                    rk.Add(shpdet);
                }
            }
            return rk;


        }
    }
}