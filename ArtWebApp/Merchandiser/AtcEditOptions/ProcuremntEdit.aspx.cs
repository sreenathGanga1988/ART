using ArtWebApp.BLL.MerchandsingBLL.ProcurementBLL;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser
{
    public partial class ProcuremntEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAtcCombo();
            }
        }


        public void FillAtcCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                entty.Configuration.AutoDetectChangesEnabled = false;
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

        public void FillPOCombo(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.ProcurementMasters
                        where ponmbr.AtcId == atcid
                        where ponmbr.IsApproved=="N"
                        select new
                        {
                            name = ponmbr.PONum,
                            pk = ponmbr.PO_Pk
                        };

                drp_PO.DataSource = q.ToList();
                drp_PO.DataBind();




            }
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




        public void fillPoData(int popk)
        {
            BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData pmmstrdata = new BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData();

            pmmstrdata = pmmstrdata.GetPodata(popk);
            
            drp_supplier.SelectedValue = pmmstrdata.Supplier_Pk.ToString();
            drp_currency.SelectedValue = pmmstrdata.CurrencyID.ToString ();
            drp_deliveryterm.SelectedValue = pmmstrdata.DeliveryTerms_Pk.ToString();
            drp_deliverymethod.SelectedValue = pmmstrdata.DeliveryMethod_Pk.ToString();
            drp_paymentterm.SelectedValue = pmmstrdata.PaymentTermID.ToString();
            drp_deliverydestination.SelectedValue = pmmstrdata.Location_PK.ToString ();
            drp_potype.SelectedValue = pmmstrdata.PoType.ToString().Trim();
            // = pmmstrdata.PoType.ToString();
            dtp_deliverydate.Value = pmmstrdata.Deliverydate;


            txt_freightcharges.Text =pmmstrdata.freightcharge.ToString();
            drp_freightChargetype.Text = pmmstrdata.FreightType;
            try
            {
                txtarea.Text = pmmstrdata.Remark.ToString();
            }
            catch (Exception)
            {
                
               
            }

            tbl_podetails.DataSource = pmmstrdata.POdetails;
       
            tbl_podetails.DataBind();
            upd_paymentterm.Update();
            upd_supplier.Update();

            ViewState["convfact"] = convfact.Value = (convdatagenerator(int.Parse(pmmstrdata.CurrencyID.ToString()))).ToString();

        }



        public String UpdatePodata()
        {
           

            BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData POmstr = new BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData();
            POmstr.PO_Pk = int.Parse(drp_PO.SelectedValue.ToString());
            POmstr.Supplier_Pk = int.Parse(drp_supplier.SelectedValue.ToString());
            POmstr.DeliveryTerms_Pk = int.Parse(drp_deliveryterm.SelectedValue.ToString());
            POmstr.PaymentTermID = int.Parse(drp_paymentterm.SelectedValue.ToString());
            POmstr.DeliveryMethod_Pk = int.Parse(drp_deliverymethod.SelectedValue.ToString());
            POmstr.CurrencyID = int.Parse(drp_currency.SelectedValue.ToString());
            POmstr.freightcharge = Decimal.Parse(txt_freightcharges.Text);
            POmstr.FreightType = drp_freightChargetype.SelectedItem.Text;

            try
            {
                POmstr.CurrencyID = int.Parse(drp_currency.SelectedValue.ToString());
            }
            catch (Exception)
            {

                updateStatus("Select Currency");
                throw;
            }

            POmstr.Location_PK = int.Parse(drp_deliverydestination.SelectedValue.ToString());
           
            POmstr.AddedBy = Session["Username"].ToString().Trim();
            POmstr.Deliverydate = DateTime.Parse(dtp_deliverydate.Date.ToString());
            POmstr.PoType = drp_potype.SelectedValue.ToString();
            POmstr.IsApproved = "N";
            POmstr.IsDeleted = "N";
            POmstr.Remark = txtarea.Text;

            POmstr.ProcurementDetailsCollection = GetPODetailsData();
      //      POmstr.ProcurementDetailsCollection = GetPODetailsData();
            POmstr.updateProcurementData(POmstr);
            String Msg = "Supplier Po is updated Sucessfully";

            return Msg;
        }



        public void updateStatus(String msg)
        {
            lbl_mssg.Text = msg;
            
        }

        protected void drp_currency_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
        {

           
        }





        protected void Button2_Click(object sender, EventArgs e)
        {
            fillPoData(int.Parse(drp_PO.SelectedItem.Value.ToString()));
        }

        public List<BLL.MerchandsingBLL.ProcurementBLL.ProcurementDetails> GetPODetailsData()
        {

            List<BLL.MerchandsingBLL.ProcurementBLL.ProcurementDetails> rk = new List<BLL.MerchandsingBLL.ProcurementBLL.ProcurementDetails>();

            convfact.Value = (convdatagenerator(int.Parse(drp_currency.SelectedItem.Value.ToString()))).ToString();
           
            for (int i = 0; i < tbl_podetails.Rows.Count; i++)
            {
                int skudet_pk = int.Parse(((tbl_podetails.Rows[i].FindControl("lbl_skudetpk") as Label).Text.ToString()));
                int podet_pk = int.Parse(((tbl_podetails.Rows[i].FindControl("lbl_podetpk") as Label).Text.ToString()));
                float unitrate = float.Parse((tbl_podetails.Rows[i].FindControl("txt_unitrate") as TextBox).Text.ToString());
                decimal poqty = decimal.Parse(((tbl_podetails.Rows[i].FindControl("txt_poQty") as TextBox).Text.ToString()));
                // converted usd rate
                float CURate = 0;
                try
                {

                    CURate = unitrate / float.Parse(ViewState["convfact"].ToString());
                }
                catch (Exception)
                {
                    CURate = unitrate;

                }


                String Suppliercolor = ((tbl_podetails.Rows[i].FindControl("ddl_Supcolor") as DropDownList).SelectedItem.Text.Trim());

                String SupplierSize = ((tbl_podetails.Rows[i].FindControl("ddl_SupSize") as DropDownList).SelectedItem.Text.Trim());

               

                BLL.MerchandsingBLL.ProcurementBLL.ProcurementDetails pddetails = new BLL.MerchandsingBLL.ProcurementBLL.ProcurementDetails();
                pddetails.SkuDet_PK = skudet_pk;
                pddetails.POQty = poqty;
                pddetails.PODet_PK = podet_pk;
                pddetails.POUnitRate = unitrate;
                pddetails.CURate = CURate;
                pddetails.SupplierColor = Suppliercolor;
                pddetails.SupplierSize = SupplierSize;
       
                pddetails.CURate = CURate;


                rk.Add(pddetails);
            }





            return rk;


        }



        public float convdatagenerator(int currencypk)
        {
            float convvalue = 1;
            DBTransaction.ProcurementTransaction pctrans = new DBTransaction.ProcurementTransaction(); ;
            convvalue = pctrans.Getconversionfact(currencypk);
            return convvalue;
        }



        protected void Btn_submit_Click(object sender, EventArgs e)
        {
            String ponum = UpdatePodata();
            updateStatus(ponum);

        }

        protected void tbl_podetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList ddl_supcolor = (e.Row.FindControl("ddl_Supcolor") as DropDownList);
            DropDownList ddl_supSize = (e.Row.FindControl("ddl_SupSize") as DropDownList);
           


            try
            { Label supcolor=(e.Row.FindControl("lbl_suppliercolor") as Label);
            string itemcolor = supcolor.Text.Trim();
                ddl_supcolor.Items.FindByText(itemcolor).Selected = true;
                supcolor.Visible = false;
            }
            catch (Exception)
            {
              
            }
            try
            {
                Label supsize = (e.Row.FindControl("lbl_suppliersize") as Label);
                string itemsize = supsize.Text.Trim();
                ddl_supSize.Items.FindByText(itemsize).Selected = true;
                supsize.Visible = false;
            }
            catch (Exception)
            {
               
            }
        }

        protected void drp_currency_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["convfact"] = convfact.Value = (convdatagenerator(int.Parse(drp_currency.SelectedItem.Value.ToString()))).ToString();
        }

        protected void tbl_podetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = tbl_podetails.Rows[index];
            if (e.CommandName == "Delete")         {



                BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData podetdata = new BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData();
                int podet_pk = int.Parse(((row.FindControl("lbl_podetpk") as Label).Text.ToString()));
                String msg = podetdata.DeletePODetailsPK(podet_pk);
                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);


            }
        }


        protected void drp_supplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcurementMasterData procurementMasterData = new ProcurementMasterData();

            int selectedindex = procurementMasterData.GetSupplierPaymentFixed(int.Parse(drp_supplier.SelectedItem.Value.ToString()));
            if (selectedindex != 0)
            {
                drp_paymentterm.SelectedValue = selectedindex.ToString();
                drp_paymentterm.Enabled = false;
            }
            else
            {
                drp_paymentterm.SelectedValue = selectedindex.ToString();
                drp_paymentterm.Enabled = true;
            }

            upd_paymentterm.Update();
        }




    }
}