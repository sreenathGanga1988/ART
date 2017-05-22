using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.BLL;
namespace ArtWebApp.Merchandiser
{
    public partial class AddPOPack : System.Web.UI.Page
    {
        BLL.PoPackMasterData pomstrdata= null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
        if(!IsPostBack)
        {
            txt_atcnum.Text = Request.QueryString["atcnum"].ToString();
           int atcid  =int.Parse(Request.QueryString["atcid"].ToString());
        }

        }



        //public void filltowarehouses()
        //{
        //    using (ArtEntitiesnew entty = new ArtEntitiesnew())
        //    {
        //        var q = from order in entty.LocationMasters
        //                where order.LocType == "W"
        //                select new
        //                {
        //                    name = order.LocationName,
        //                    pk = order.Location_PK
        //                };
               
              

        //        drp_deliverymode.DataSource = q1.ToList();
        //        drp_deliverymode.DataBind();

        //        // Bind the table to a System.Windows.Forms.BindingSource object, 
        //        // which acts as a proxy for a System.Windows.Forms.DataGridView object.

        //    }
        //}
       


        public void Insertdata()
        {
            pomstrdata = new PoPackMasterData();
            pomstrdata.AtcId = int.Parse(Request.QueryString["atcid"].ToString());
            pomstrdata.Atcnum = txt_atcnum.Text;
            pomstrdata.BuyerPO = txt_buyerpo.Text.Trim();
            pomstrdata.DeliveryDate = dtp_rsd.Date;
            pomstrdata.HandoverDate = dtp_hd.Date;
            pomstrdata.firstDeliverydate = dtp_deliverydate.Date;
            pomstrdata.Inhousedate = dtp_inhousedate.Date;
            pomstrdata.PackingInstruction = txt_packdetail.Text.Trim();
            pomstrdata.POGroup = drp_pogroup.SelectedItem.Text;
            pomstrdata.POTag = drp_taggroup.SelectedItem.Text;
            pomstrdata.seasonName = drp_season.SelectedItem.Text;
            pomstrdata.location_PK = int.Parse(drp_loc.SelectedValue.ToString());
            try
            {
                pomstrdata.ChannelID = int.Parse(drp_channel.SelectedItem.Value.ToString());
            }
            catch (Exception)
            {

                pomstrdata.ChannelID = 1;
            }


            try
            {
                pomstrdata.BuyerDestination_PK = int.Parse(drp_dest.SelectedItem.Value.ToString());
            }
            catch (Exception)
            {
                pomstrdata.BuyerDestination_PK = 3;

            }


            String ponum = pomstrdata.insertpopack(pomstrdata);
             String Msg = "ASQ # : " + ponum + " is generated Successfully";
             lbl_errordisplayer.Text = Msg;
             MessageBoxShow(Msg);
             clearcontrol();
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            Insertdata();
        }

        public void clearcontrol()
        {
            txt_buyerpo.Text = "";
            txt_packdetail.Text = "";
        }

         public void MessageBoxShow(String Msg)
        {                         
            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);
        }

         protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
         {


             if(CheckBox1.Checked==true)
             {
                 DropDownListChosen1.Visible = true;
                 DropDownListChosen1.Enabled = true;
                 txt_buyerpo.Enabled = false;
             }
             else
             {
                  DropDownListChosen1.Visible = false;
                 DropDownListChosen1.Enabled = false;
                 txt_buyerpo.Enabled = true;
                     
             }
             }

         protected void DropDownListChosen1_SelectedIndexChanged(object sender, EventArgs e)
         {
             txt_buyerpo.Text = DropDownListChosen1.Text;
         }

        protected void dtp_deliverydate_ValueChanged(object sender, Infragistics.Web.UI.EditorControls.TextEditorValueChangedEventArgs e)
        {
            dtp_rsd.Value = dtp_deliverydate.Value;
            dtp_hd.Value = dtp_deliverydate.Value;
        }
    }
    
}