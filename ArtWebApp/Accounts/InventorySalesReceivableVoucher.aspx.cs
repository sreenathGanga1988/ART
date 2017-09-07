using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Accounts
{
    public partial class InventorySalesReceivableVoucher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            filltowarehouses();
        }

        public void filltowarehouses()
        {
            using (DataModels.ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.LocalBuyerMasters

                        select new
                        {
                            name = order.LocalBuyerName,
                            pk = order.LocalBuyer_PK
                        };

                drp_ToWarehouse.DataSource = q.ToList();
                drp_ToWarehouse.DataBind();


                // Bind the table to a System.Windows.Forms.BindingSource object, 
                // which acts as a proxy for a System.Windows.Forms.DataGridView object.

            }
        }

        protected void Button3_Click1(object sender, EventArgs e)
        {

            int year = int.Parse(cmb_year.SelectedItem.Text);

            int month = int.Parse(cmb_Month.SelectedValue.ToString());

            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            lbl_fromdate.Text = startDate.ToString();
            lbl_todate.Text = endDate.ToString();

            tbl_podata.DataSource = SalesDOData;
            tbl_podata.DataBind();
        }


       public void loadSDO()
       {

       }

        protected void btn_sumbit_Click(object sender, EventArgs e)
        {
            string msg = "";
            String num = "";
            BLL.AccountsBLL.DebitNoteAgainstSales dbslsmstr = new BLL.AccountsBLL.DebitNoteAgainstSales();
            dbslsmstr.Year = int.Parse(cmb_year.SelectedValue.ToString());
            dbslsmstr.Month = cmb_Month.SelectedItem.Text.ToString();
            dbslsmstr.toLocationPK_pk = int.Parse(drp_ToWarehouse.SelectedValue.ToString());
            dbslsmstr.fromLocationPK_pk = int.Parse(Session["UserLoc_pk"].ToString());
            dbslsmstr.DebitNoteAgainstSalesDetailsDataCollection = DebitDetailsData();
            dbslsmstr.InsertSPOInvoice();
            msg = num + " is generated Successfully";
            tbl_podata.DataSource = null;
            tbl_podata.DataBind();
            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
        }


        public List<BLL.AccountsBLL.DebitNoteAgainstSalesDetails> DebitDetailsData()
        {

            List<BLL.AccountsBLL.DebitNoteAgainstSalesDetails> rk = new List<BLL.AccountsBLL.DebitNoteAgainstSalesDetails>();


            foreach (GridViewRow di in tbl_podata.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("Chk_select");

                if (chkBx != null && chkBx.Checked)
                {




                    int lbl_sdopk = int.Parse(((di.FindControl("lbl_sdopk") as Label).Text.ToString()));



                    BLL.AccountsBLL.DebitNoteAgainstSalesDetails lsdetdata = new BLL.AccountsBLL.DebitNoteAgainstSalesDetails();


                    lsdetdata.SDO_PK = lbl_sdopk;

                    rk.Add(lsdetdata);
                }
            }
            return rk;


        }
    }
}