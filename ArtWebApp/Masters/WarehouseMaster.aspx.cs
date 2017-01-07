using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DataModels;
namespace ArtWebApp.Masters
{
    public partial class Warehouse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void cmb_locationtype_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
        {
            subcomboload();
        }

        public void subcomboload()
        {
            hd_type.Value = cmb_locationtype.SelectedValue.ToString();
            cmb_subtype.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            addlocation();
        }




        public void addlocation()
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                if (!enty.LocationMasters.Any(f => f.LocationPrefix.Trim() == txt_prefix.Text.Trim() || f.LocationName.Trim() == txt_name.Text.Trim()))
                {

                    LocationMaster tmpmstr = new LocationMaster();
                    tmpmstr.LocationPrefix = txt_prefix.Text.Trim().ToUpper ();
                    tmpmstr.LocationName = txt_name.Text.Trim().ToUpper();
                    tmpmstr.LocationAddress = txta_address.Value.Trim();
                    tmpmstr.LocType = cmb_locationtype.SelectedValue.ToString();
                    tmpmstr.LocationType_PK = int.Parse(cmb_subtype.SelectedValue.ToString());
                    tmpmstr.Telephone = txt_telephone.Text.Trim();

                    tmpmstr.Fax = txt_fax.Text.Trim();
                    tmpmstr.Email = txt_email.Text.Trim();
                    tmpmstr.ContactPerson = txt_contactperson.Text.Trim(); ;
                    tmpmstr.CurrencyID = int.Parse(cmb_currency.SelectedValue.ToString());
                    tmpmstr.CountryID = int.Parse(cmb_country.SelectedValue.ToString());
                    tmpmstr.PaymentModeID = int.Parse(cmb_paymentmode.SelectedValue.ToString());
                    tmpmstr.IsActive = "A";
                    tmpmstr.AddedBy = Session["Username"].ToString().Trim();
                    tmpmstr.AddedDate = DateTime.Now;

                    enty.LocationMasters.Add(tmpmstr);

                    enty.SaveChanges();
                }
            }
        }
    }
}