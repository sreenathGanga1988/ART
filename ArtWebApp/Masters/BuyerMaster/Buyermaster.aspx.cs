using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_Buyermaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        addBuyer();
    }


    public void addBuyer()
    {
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
            BuyerMaster mstr = new BuyerMaster();
            mstr.BuyerName = txt_buyername.Text.Trim();
            mstr.Prefix = txt_prefix.Text.Trim();
            mstr.Adress = txta_address.Value.Trim();
            mstr.Telephone = txt_telephone.Text.Trim();
            mstr.Fax = txt_fax.Text.Trim();
            mstr.Email = txt_email.Text.Trim();
            mstr.ContactPerson = txt_contactperson.Text.Trim();
            mstr.Agent = txt_agent.Text.Trim();
            mstr.Department = txt_dept.Text.ToString();
            mstr.PaymentModeCode = int.Parse(cmb_payment.SelectedValue.ToString());
            mstr.CurrencyCode = int.Parse(cmb_currency.SelectedValue.ToString());
            enty.BuyerMasters.Add(mstr);
            enty.SaveChanges();
        }
    }
}