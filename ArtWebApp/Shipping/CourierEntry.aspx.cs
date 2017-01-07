using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Shipping
{
    public partial class CourierEntry : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private void ClearData()
        {
            txt_atcstyle.Text = "";
            txt_buyer.Text = "";
            txt_typeofsample.Text = "";
            txt_courierdate.Text = "";
            txt_otheraccount.Text = "";
            txt_qty.Text = "";
            txt_destination.Text = "";
            txt_apprcost.Text = "";
            txt_addeddate.Text = "";
            txt_accountnum.Text = "";
            txt_remark.Text = "";
            txt_isouraccount.Text = "";
            txt_isnormalcourier.Text = "";
            txt_reciver.Text = "";
            txt_sender.Text = "";
            txt_weight.Text = "";
            txt_awbnum.Text = "";



        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
          try

    {
                BLL.ShippingBLL.CourierTableData cde = new BLL.ShippingBLL.CourierTableData();

                //   cde.CourierDate = DateTime.Parse(Request.Form[txt_courierdate.UniqueID].ToString());
                string s = DateTime.Parse(Request.Form[txt_courierdate.UniqueID].ToString()).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                string s1 = DateTime.Parse(Request.Form[txt_addeddate.UniqueID].ToString()).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
               


                cde.CourierDate = DateTime.Parse(s);

                cde.TypeofSample = txt_typeofsample.Text.Trim();
                cde.Buyer = txt_buyer.Text.Trim();
                cde.AtcOrStyle = txt_atcstyle.Text.Trim();
                cde.Quantity = txt_qty.Text.Trim();
                cde.Reciever = txt_reciver.Text.Trim();
                cde.Sender = txt_sender.Text.Trim();
                cde.Destination = txt_destination.Text.Trim();
                cde.AWBnum = txt_awbnum.Text.Trim();
                cde.ISourAccount = txt_isouraccount.Text.Trim();
                cde.OtherAccount = txt_otheraccount.Text.Trim();
                cde.IsnormalCourier = txt_isnormalcourier.Text.Trim();
                cde.Weight = txt_weight.Text.Trim();
                cde.ApprCost = txt_apprcost.Text.Trim();
                cde.Remark = txt_remark.Text.Trim();
                // cde.Addeddate = DateTime.Parse(Request.Form[txt_addeddate.UniqueID].ToString());
               
                cde.Addeddate = DateTime.Parse(s1);
             
                cde.InsertCourierData();
                lbl_msg.Text = "Added Successsfuly";
                ClearData();

            }

            catch (Exception)
            {

                Response.Write("courierdate" + Request.Form[txt_courierdate.UniqueID].ToString());
                Response.Write("AddedDate" + Request.Form[txt_addeddate.UniqueID].ToString());




                try
                {
                    string s = DateTime.Parse(Request.Form[txt_courierdate.UniqueID].ToString()).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                    Response.Write("converted courierdate" + s);
                }
                catch (Exception)
                {

                    
                }


                try
                {
                    string s1 = DateTime.Parse(Request.Form[txt_addeddate.UniqueID].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    Response.Write("converted AddedDate" + s1);
                }
                catch (Exception)
                {

                    
                }
                







            }


        }

    }
}