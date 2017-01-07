using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.BLL;
namespace ArtWebApp.Merchandiser
{
    public partial class ServicePO : System.Web.UI.Page
    {
        BLL.ProcurementBLL.ServicePOData spdata=null;

    
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack )
            {
                spdata = new BLL.ProcurementBLL.ServicePOData();
                spdata.loadserviceTypeandCurrency(drp_serviceType,drp_currency);
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            insertServicePo();
        }


        public void insertServicePo()
        {
            spdata = new BLL.ProcurementBLL.ServicePOData();
            spdata.DebitFrom = ddl_debitfrom.SelectedItem.Text.Trim();
            spdata.DebitName = drp__name.SelectedItem.Text.Trim();
            spdata.ServiceType = drp_serviceType.SelectedItem.Text.Trim();
            spdata.ServiceType_PK = int.Parse(drp_serviceType.SelectedValue.ToString());
            spdata.Amount = decimal.Parse(txt_amount.Text.ToString());
            spdata.Description = txt_description.Text.ToString();
            spdata.CurrencyID = int.Parse(drp_currency.SelectedValue.ToString());

            String servicePonum = spdata.insertServicePO(spdata);
            lbl_errordisplayer.Text = "Service PO " + servicePonum +  " Generated Successfully";
            clearcontrols();
        }



        public void clearcontrols()
        {
            txt_amount.Text = "";
            txt_description.Text = "";
           
        }

     

        protected void ddl_debitfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            spdata = new BLL.ProcurementBLL.ServicePOData();

            if (ddl_debitfrom.SelectedItem.Text.Trim() == "Buyer")
            {
                spdata.LoadCombo("Buyer", drp__name);
            }
            else if (ddl_debitfrom.SelectedItem.Text.Trim() == "ATC")
            {
                spdata.LoadCombo("ATC", drp__name);
            }
            else if (ddl_debitfrom.SelectedItem.Text.Trim() == "Department")
            {

            }
            else if (ddl_debitfrom.SelectedItem.Text.Trim() == "Individual")
            {
                spdata.LoadCombo("Individual", drp__name);
            }
            else if (ddl_debitfrom.SelectedItem.Text.Trim() == "Factory")
            {
                spdata.LoadCombo("Factory", drp__name);
            }
        }
       

        
    }
}