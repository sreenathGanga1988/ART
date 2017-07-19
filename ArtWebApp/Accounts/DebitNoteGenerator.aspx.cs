using ArtWebApp.BLL.AccountsBLL;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Accounts
{
    public partial class DebitNoteGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadCombo(drp_creditfor.SelectedItem.Text.Trim(), cmb_credit);

            //if (drp_creditfor.SelectedValue.ToString ().Trim()=="Buyer")
            //{
            //    LoadCombo("Buyer" ,cmb_credit);
            //}
            //else if (drp_creditfor.SelectedValue.ToString().Trim() == "Supplier")
            //{
            //    LoadCombo("Buyer", cmb_credit);
            //}
            //else if (drp_creditfor.SelectedValue.ToString().Trim() == "Supplier")
            //{
              
            //}
        }



        /// <summary>
        /// Load The DebitfromCombo based on user
        /// </summary>
        /// <param name="Debitfrom"></param>
        /// <param name="cmb_debit"></param>
        public void LoadCombo(String Debitfrom,CustomDropDown.DropDownListChosen cmb_debit)
        {


            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                if (Debitfrom == "Buyer")
                {

                    var PoQuery = from bmstr in enty.BuyerMasters
                                  where bmstr.BuyerName != ""
                                  select new
                                  {
                                      name = bmstr.BuyerName,
                                      pk = bmstr.BuyerID
                                  };
                    cmb_debit.DataSource = PoQuery.ToList();
                    cmb_debit.DataBind();
                }
                else if (Debitfrom == "ATC")
                {
                    var PoQuery = from atcmstr in enty.AtcMasters
                                  select new
                                  {
                                      name = atcmstr.AtcNum,
                                      pk = atcmstr.AtcId
                                  };
                    cmb_debit.DataSource = PoQuery.ToList();
                    cmb_debit.DataBind();
                }
                else if (Debitfrom == "Individual")
                {

                    var PoQuery = from atcmstr in enty.MerchandiserMasters
                                  select new
                                  {
                                      name = atcmstr.MerchandiserName,
                                      pk = atcmstr.MerChandiser_Pk
                                  };
                    cmb_debit.DataSource = PoQuery.ToList();
                    cmb_debit.DataBind();
                }

                else if (Debitfrom == "Factory")
                {

                    var PoQuery = from order in enty.LocationMasters
                                  where order.LocType == "F"
                                  select new
                                  {
                                      name = order.LocationName,
                                      pk = order.Location_PK
                                  };
                    cmb_debit.DataSource = PoQuery.ToList();
                    cmb_debit.DataBind();
                }

                else if (Debitfrom == "Supplier")
                {

                    var PoQuery = from order in enty.SupplierMasters
                                
                                  select new
                                  {
                                      name = order.SupplierName,
                                      pk = order.Supplier_PK
                                  };
                    cmb_debit.DataSource = PoQuery.ToList();
                    cmb_debit.DataBind();
                }
                else if (Debitfrom == "Atc")
                {

                    var PoQuery = from order in enty.AtcMasters

                                  select new
                                  {
                                      name = order.AtcNum,
                                      pk = order.AtcId
                                  };
                    cmb_debit.DataSource = PoQuery.ToList();
                    cmb_debit.DataBind();
                }
            }

            //showAllPoPackATC();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            LoadCombo(cmb_debitfrom.SelectedItem.Text.Trim(), cmb_debitorname);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {


                CreditNoteMasterData cdmdata = new BLL.AccountsBLL.CreditNoteMasterData();

                cdmdata.CreidtFor = drp_creditfor.SelectedItem.Text.Trim();
                cdmdata.CreditName = cmb_credit.SelectedItem.Text.Trim();
                cdmdata.DebitFor = cmb_debitfrom.SelectedItem.Text.Trim();
                cdmdata.  DebitorName = cmb_debitorname.SelectedItem.Text.Trim();
                cdmdata.Amount = Decimal.Parse(txt_amount.Text);
                cdmdata.Message = txt_message.InnerText;
                cdmdata.AddedBy = Session["Username"].ToString().Trim();
                cdmdata.AddedDate = DateTime.Now;
                cdmdata.AmountUsed = 0;
                cdmdata.IsApproved = "N";
              
                String invnum = cdmdata.InsertCreditNote();

                invnum = "Credit Note " + invnum + "Generated Sucessfully";
                MessgeboxUpdate("sucess", invnum);

                txt_amount.Text = "0";


    }
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




    }
}