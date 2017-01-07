using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Masters
{
    public partial class UOMMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String navtype = Request.QueryString["navtype"];

            string v = Request.QueryString["navtype"];
            if (navtype == "UOMMaster")
            {
                MultiView1.ActiveViewIndex = 0;
            }
            else if (navtype == "AltUOM")
            {
                MultiView1.ActiveViewIndex = 1;
            }

            else if (navtype == "PORate")
            {
                MultiView1.ActiveViewIndex = 2;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AddUOMCOnVersion();
            dgv_altuom.DataBind();
        }

        public void AddUOMCOnVersion()
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                int baseUOm_pk = int.Parse(drp_baseuom.SelectedValue.ToString());
                int altUOm_pk = int.Parse(drp_altuom.SelectedValue.ToString());
                string operatorstring = drp_op.Text;
                float convertfact = float.Parse(txt_convfact.Text.ToString());
                if (!enty.AltUOMMasters.Any(f => f.Uom_PK == baseUOm_pk && f.AltUom_PK == altUOm_pk))
                {


                    AltUOMMaster almmstr = new AltUOMMaster();
                    almmstr.AltUom_PK = altUOm_pk;
                    almmstr.Uom_PK = baseUOm_pk;
                    almmstr.Operator = operatorstring;
                    almmstr.Conv_fact = convertfact;
                    enty.AltUOMMasters.Add(almmstr);
                    enty.SaveChanges();

                }
            }
        }

        protected void Btn_exrate_Click(object sender, EventArgs e)
        {
            InsertConversionrate();
            dgv_porate.DataBind();
        }



        public void InsertConversionrate()
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                int currencypk = int.Parse(drp_currency.SelectedValue.ToString());

                float convertfact = float.Parse(txt_rate.Text.ToString());

                if (!enty.POCurrExRates.Any(f => f.CurrencyID == currencypk && f.Convrate == convertfact))
                {
                    POCurrExRate porate = new POCurrExRate();
                    porate.CurrencyID = currencypk;
                    porate.Convrate = convertfact;
                    porate.AddedBy = Session["Username"].ToString().Trim();
                    porate.ExDate = DateTime.Now;

                    enty.POCurrExRates.Add(porate);
                    enty.SaveChanges();
                }

            }

        }

    }
}