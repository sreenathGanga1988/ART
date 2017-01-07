using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DataModels;
namespace ArtWebApp.Masters
{
    public partial class SuppierForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            addSupplier();
        }

        public void MessgeboxUpdate(String Messagetype, String Messg)
        {
            if (Messagetype == "sucess")
            {
                Messaediv.Attributes["class"] = "success";
                Messaediv.InnerText = "Supplier Added";
            }
            else
            {
                  Messaediv.Attributes["class"] = "error-message ";
                Messaediv.InnerText = "Supplier Not Added";
              
            }
        }
        public void addSupplier()
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                if (!enty.SupplierMasters.Any(f => f.SupplierName.Trim() == txt_name.Text.Trim() || f.SupplierPrefix.Trim() == txt_prefix.Text.Trim()))
                {
                    SupplierMaster spmstr = new SupplierMaster();
                    spmstr.SupplierPrefix = txt_prefix.Text.Trim();
                    spmstr.SupplierName = txt_name.Text.Trim();
                    spmstr.SupplierAddress = txta_address.Value.Trim();
                    spmstr.SupplierType = cmb_suppliertype.SelectedValue.ToString();

                    spmstr.Telephone = txt_telephone.Text.Trim();

                    spmstr.Fax = txt_fax.Text.Trim();
                    spmstr.Email = txt_email.Text.Trim();
                    spmstr.ContactPerson = txt_contactperson.Text.Trim(); ;
                    spmstr.CurrencyID = int.Parse(cmb_currency.SelectedValue.ToString());
                    spmstr.PaymentTermID = int.Parse(cmb_paymentterm.SelectedValue.ToString());
                    spmstr.CountryID = int.Parse(cmb_country.SelectedValue.ToString());
                    spmstr.PaymentModeID = int.Parse(cmb_paymentmode.SelectedValue.ToString());
                    spmstr.IsActive = "A";
                    spmstr.AddedBY= Session["Username"].ToString().Trim();
                    spmstr.AddedDate = DateTime.Now;
                    enty.SupplierMasters.Add(spmstr);

                    enty.SaveChanges();
                    MessgeboxUpdate("sucess", "Supplier Added");
                 
                }
            }
        }
    }
}