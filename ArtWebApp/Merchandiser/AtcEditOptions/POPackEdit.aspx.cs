using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser
{
    public partial class POPackEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            updatePOPackmaster(int.Parse(cmb_po.SelectedItem.Value.ToString()));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            fillcontrol();
            UpdatePanel3.Update();
        }





        public void filldetails(int popackid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var queryselecct = from pkmstr in enty.PoPackMasters
                                   where pkmstr.PoPackId == popackid
                                   select pkmstr;


                foreach (var element in queryselecct)
                {
                    lbl_popackid.Text = element.PoPackId.ToString ();
                    txt_buyerpo.Text = (element.BuyerPO == null? "" : element.BuyerPO);
                    txt_packdetail.Text = (element.PackingInstruction == null ? "" : element.PackingInstruction);
                    dtp_deliverydate.Value = (element.DeliveryDate == null ? DateTime.Now.Date : element.DeliveryDate);
                    dtp_inhousedate.Value = (element.Inhousedate == null ? DateTime.Now.Date : element.Inhousedate);

                    try
                    {
                        drp_dest.SelectedValue = element.BuyerDestination_PK.ToString();
                    }
                    catch (Exception)
                    {


                    }

                    try
                    {
                        drp_channel.SelectedValue = element.ChannelID.ToString();
                    }
                    catch (Exception)
                    {


                    }
                    try
                    {
                        drp_pogroup.SelectedValue = (element.PoGroup == null ? "" : element.PoGroup);
                    }
                    catch (Exception)
                    {
                        
                        
                    }

                    try
                    {
                        drp_taggroup.SelectedValue= (element.TagGroup == null ? "" : element.TagGroup); 
                    }
                    catch (Exception)
                    {


                    }
                    try
                    {


                      //  drp_season.Items.FindByTex(element.SeasonName.ToString ()).Selected = true;
                        drp_season.SelectedItem.Text= (element.SeasonName == null ? "" : element.SeasonName);
                    }
                    catch (Exception)
                    {


                    }
                   
                 
                }




            }
        }





        public void updatePOPackmaster(int popackid)
        {
            BLL.PoPackMasterData pomstrdata = new BLL.PoPackMasterData ();
            
            pomstrdata.PoPackId = int.Parse(lbl_popackid.Text. ToString());
            pomstrdata.BuyerPO = txt_buyerpo.Text.Trim();
            pomstrdata.DeliveryDate = dtp_deliverydate.Date;
            pomstrdata.Inhousedate = dtp_inhousedate.Date;
            pomstrdata.PackingInstruction = txt_packdetail.Text.Trim();
            pomstrdata.POGroup = drp_pogroup.SelectedItem.Text;
            pomstrdata.POTag = drp_taggroup.SelectedItem.Text;
            pomstrdata.seasonName = drp_season.SelectedItem.Text;
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
            pomstrdata.updatePOpAck(pomstrdata);
            String Msg = "POPack # Updated";
            lbl_errordisplayer.Text = Msg;

            clearControl();
        }
        public void clearControl()
        {
            txt_buyerpo.Text = "";
            lbl_popackid.Text = "";
            



        }



        public void fillcontrol()
        {
            int atcid = int.Parse(cmb_atc.SelectedItem.Value.ToString ());

            ArtEntitiesnew enty = new ArtEntitiesnew();
            var PoQuery = from pckmst in enty.PoPackMasters
                          where pckmst.AtcId == atcid
                          select new
                          {
                              name = pckmst.PoPacknum,
                              pk = pckmst.PoPackId
                          };

          


            cmb_po.DataSource = PoQuery.ToList();
            cmb_po.DataBind();

         



            //showAllPoPackATC();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            filldetails(int.Parse(cmb_po.SelectedItem.Value.ToString()));
        }
    }
}