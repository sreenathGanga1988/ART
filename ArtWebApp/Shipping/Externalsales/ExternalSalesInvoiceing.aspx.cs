using ArtWebApp.BLL.AccountsBLL;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Shipping.Externalsales
{
    public partial class ExternalSalesInvoiceing : System.Web.UI.Page
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


                

            }
        }

      

        public void loadSDO()
        {

            int buyerid = int.Parse(drp_ToWarehouse.SelectedValue.ToString());
            using (DataModels.ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.InventorySalesMasters
                        where order.ToLocation_PK == buyerid
                        select new
                        {
                            name = order.SalesDONum,
                            pk = order.SalesDO_PK
                        };

                dro_sdo.DataSource = q.ToList();
                dro_sdo.DataBind();


              
            }
        }


        public void LoadSdoDetails(int sdo_pk)
        {

        }

        protected void btn_sumbit_Click(object sender, EventArgs e)
        {
           



            string msg = "";
            String num = "";
            ExternalDoInvoiceMaster extrnlmstr = new ExternalDoInvoiceMaster();
            extrnlmstr.ExternalDoInvoiceDetailsCollection = DebitDetailsData();
            num = extrnlmstr.Updateinvoicedetails();

           
            msg ="Invoice # " +num + " is generated Successfully";
            tbl_podata.DataSource = null;
            tbl_podata.DataBind();
            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);




        }


        public List<BLL.AccountsBLL.ExternalDoInvoiceDetails> DebitDetailsData()
        {

            List<BLL.AccountsBLL.ExternalDoInvoiceDetails> rk = new List<BLL.AccountsBLL.ExternalDoInvoiceDetails>();


            foreach (GridViewRow di in tbl_podata.Rows)
            {
               



                    int lbl_SalesDODet_PK = int.Parse(((di.FindControl("lbl_SalesDODet_PK") as Label).Text.ToString()));
                       Decimal txt_agreedcurate = Decimal.Parse(((di.FindControl("txt_agreedcurate") as TextBox).Text.ToString()));
                

                    BLL.AccountsBLL.ExternalDoInvoiceDetails lsdetdata = new BLL.AccountsBLL.ExternalDoInvoiceDetails();


                    lsdetdata.SDODet_PK = lbl_SalesDODet_PK;
                     lsdetdata.AgreedCurate = txt_agreedcurate;

                rk.Add(lsdetdata);
              
            }
            return rk;


        }

        protected void btn_Buyer_Click(object sender, EventArgs e)
        {
            loadSDO();
        }

        protected void btn_details_Click(object sender, EventArgs e)
        {
            DBTransaction.ReportTransactions.ReporterTrans repotrans = new DBTransaction.ReportTransactions.ReporterTrans();

            String dotypestrng = "";
            int sdopk = int.Parse(dro_sdo.SelectedItem.Value.ToString());
            using (DataModels.ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var dotype = entty.InventorySalesMasters.Where(u => u.SalesDO_PK == sdopk).Select(u => u.DoType).FirstOrDefault();
                dotypestrng = dotype.ToString().Trim();
            }

            DataTable dt = repotrans.GetSalesDO(sdopk, dotypestrng);
            tbl_podata.DataSource = dt;
            tbl_podata.DataBind();
        }
        float totalvalue = 0;
        protected void tbl_podata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Check if the current row is datarow or not
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Add the value of column
                totalvalue += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalValue"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Find the control label in footer 
                Label lblamount = (Label)e.Row.FindControl("txt_actualtotal");
                //Assign the total value to footer label control
                lblamount.Text = totalvalue.ToString();
            }
        }
    }
}