using ArtWebApp.BLL.AccountsBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Accounts.Debitnote
{
    public partial class DebitSupplierAgainstASNShortage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BTN_SHOWasn_Click(object sender, EventArgs e)
        {

        }

        protected void btn_sumbit_Click(object sender, EventArgs e)
        {


            DebitNoteAgainstASNShortage asnshortdebit = new DebitNoteAgainstASNShortage();

            asnshortdebit.supplier_Pk = int.Parse(drp_supplier.SelectedValue.ToString());
            asnshortdebit.DebitNoteAgainstASNShortageDetailsCollection = DebitDetailsData();
            asnshortdebit.InsertDebitNoteforSupplier();



        }





        public List<BLL.AccountsBLL.DebitNoteAgainstASNShortageDetails> DebitDetailsData()
        {

            List<BLL.AccountsBLL.DebitNoteAgainstASNShortageDetails> rk = new List<BLL.AccountsBLL.DebitNoteAgainstASNShortageDetails>();


            foreach (GridViewRow di in tbl_podata.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("Chk_select");

                if (chkBx != null && chkBx.Checked)
                {




                    Decimal lbl_SYard = Decimal.Parse(((di.FindControl("lbl_SYard") as Label).Text.ToString()));
                    Decimal lbl_ayard = Decimal.Parse(((di.FindControl("lbl_ayard") as Label).Text.ToString()));
                    Decimal lbl_PoPK = Decimal.Parse(((di.FindControl("lbl_PoPK") as Label).Text.ToString()));
                    Decimal lbl_supplierDoc_PK = Decimal.Parse(((di.FindControl("lbl_supplierDoc_PK") as Label).Text.ToString()));
                    Decimal lbl_Shortage = Decimal.Parse(((di.FindControl("lbl_Shortage") as Label).Text.ToString()));



                    BLL.AccountsBLL.DebitNoteAgainstASNShortageDetails lsdetdata = new BLL.AccountsBLL.DebitNoteAgainstASNShortageDetails();

                    lsdetdata.Asn_PK = lbl_supplierDoc_PK;

                    lsdetdata.Po_PK = lbl_PoPK;

                    lsdetdata.syard = lbl_SYard;

                    lsdetdata.ayard = lbl_ayard;

                    lsdetdata.shortageyard = lbl_Shortage;






                    rk.Add(lsdetdata);
                }
            }

            return rk;


        }








    }
}