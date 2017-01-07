using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class InspectionEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_atc_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_atc.SelectedValue != null)
                {
                    FillAsnCombo(int.Parse(drp_atc.SelectedValue.ToString()));
                   
                }
            }
            catch (Exception exp)
            {


            }
        }



        public void FillAsnCombo(int atcid)
        {
            BLL.InventoryBLL.InspectionData fbrolldet = new BLL.InventoryBLL.InspectionData();

            drp_asn.DataSource = fbrolldet.GetDocumentnumber(int.Parse(drp_atc.SelectedValue.ToString()));
            drp_asn.DataValueField = "pk";
                drp_asn.DataTextField = "name";
                drp_asn.DataBind();
                UPD_ASN.Update();



            
        }

        public void FillFabricombo(int asn_pk,int atcid)
        {

            drp_color.DataSource = BLL.InventoryBLL.RollTransactionBLL.getfabricinsideASN(asn_pk,atcid);

            drp_color.DataValueField = "SkuDet_PK";
            drp_color.DataTextField = "ItemDescription";
            drp_color.DataBind();
            upd_color.Update();



        }







        protected void btn_submitData_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollEntryMRN mrnrolldata = new BLL.InventoryBLL.FabricRollEntryMRN();
            mrnrolldata.Rolldatacollection = GetRollDetailsData();
            mrnrolldata.UpdateRollInspectiondata();

            tbl_InverntoryDetails.DataSource = null;
            tbl_InverntoryDetails.DataBind();
            upd_grid.Update();
        }




      


        public List<BLL.InventoryBLL.FabricRollmasterDataDetails> GetRollDetailsData()
        {

            List<BLL.InventoryBLL.FabricRollmasterDataDetails> rk = new List<BLL.InventoryBLL.FabricRollmasterDataDetails>();
            for (int i = 0; i < tbl_InverntoryDetails.Rows.Count; i++)
            {

                CheckBox chkBx = (CheckBox)tbl_InverntoryDetails.Rows[i].FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {

                    String lbl_rollpk = ((tbl_InverntoryDetails.Rows[i].FindControl("lbl_rollpk") as Label).Text.ToString());
                    String txt_ashrink = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_ashrink") as TextBox).Text.ToString());
                    String txt_ashade = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_ashade") as TextBox).Text.ToString());
                    String txt_awidth = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_awidth") as TextBox).Text.ToString());
                    String txt_ayard = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_ayard") as TextBox).Text.ToString());
                    String txt_agsm = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_agsm") as TextBox).Text.ToString());


                    BLL.InventoryBLL.FabricRollmasterDataDetails rolldata = new BLL.InventoryBLL.FabricRollmasterDataDetails();


                    rolldata.Roll_PK = int.Parse(lbl_rollpk);

                    rolldata.AShrink = txt_ashrink.Trim();
                    rolldata.AShade = txt_ashade.Trim();
                    rolldata.AWidth = txt_awidth.Trim();
                    rolldata.AYard = txt_ayard.Trim();
                    rolldata.AGSM = txt_agsm.Trim();


                    rk.Add(rolldata);
                }
            }




            return rk;


        }

      
            

       
        public void selectall()
        {
            if (chk_select.Checked == true)
            {
                foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
                {
                    CheckBox chkBx = (CheckBox)di.FindControl("Chk_select");
                    chkBx.Checked = true;

                }
            }
            else
            {
                foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
                {
                    CheckBox chkBx = (CheckBox)di.FindControl("Chk_select");
                    chkBx.Checked = false;

                }
            }
            upd_grid.Update();
        }

        protected void chk_woven_CheckedChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < tbl_InverntoryDetails.Rows.Count; i++)
            {

                CheckBox chkBx = (CheckBox)tbl_InverntoryDetails.Rows[i].FindControl("chk_select");

                if (chkBx.Checked == true)
                {
                    TextBox txt_ashrink = (tbl_InverntoryDetails.Rows[i].FindControl("txt_ashrink") as TextBox);
                    TextBox txt_ashade = (tbl_InverntoryDetails.Rows[i].FindControl("txt_ashade") as TextBox);
                    TextBox txt_awidth = (tbl_InverntoryDetails.Rows[i].FindControl("txt_awidth") as TextBox);
                    TextBox txt_ayard = (tbl_InverntoryDetails.Rows[i].FindControl("txt_ayard") as TextBox);
                    TextBox txt_agsm = (tbl_InverntoryDetails.Rows[i].FindControl("txt_agsm") as TextBox);



                    if (chk_woven.Checked == true)
                    {




                        txt_ashrink.Text = "0";
                        txt_ashade.Text = "0";
                        txt_awidth.Text = "0";
                        txt_ayard.Text = "0";


                        txt_ashrink.Enabled = false;
                        txt_ashade.Enabled = false;
                        txt_awidth.Enabled = false;
                        txt_ayard.Enabled = false;
                        txt_agsm.Enabled = true; ;



                    }




                    else if (chk_woven.Checked == false)
                    {


                        txt_agsm.Text = "0";
                        txt_ashrink.Enabled = true;
                        txt_ashade.Enabled = true;
                        txt_awidth.Enabled = true;
                        txt_ayard.Enabled = true;
                        txt_agsm.Enabled = false; ;
                    }
                }
            }
            upd_grid.Update();


        }

        protected void btn_fabric_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Asn_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_asn.SelectedValue != null)
                {
                    FillFabricombo(int.Parse(drp_asn.SelectedValue.ToString()), int.Parse(drp_atc.SelectedValue.ToString()));
                }
            }
            catch (Exception exp)
            {


            }
            
        }

        protected void btn_fabric_Click1(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollmasterDataDetails fbrolldet = new BLL.InventoryBLL.FabricRollmasterDataDetails();
            tbl_InverntoryDetails.DataSource = fbrolldet.getRollDetailsofASNandSKUDetPK(int.Parse(drp_asn.SelectedValue.ToString()), int.Parse(drp_color.SelectedValue.ToString()));
            tbl_InverntoryDetails.DataBind();
            upd_grid.Update();
        }

        protected void chk_selectall_CheckedChanged(object sender, EventArgs e)
        {
            selectall();
        }


        public void updatedata(String totextbox, String fromtextbox)
        {
            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    TextBox wbdt = di.FindControl(totextbox) as TextBox;


                    wbdt.Text = fromtextbox.Trim();
                }
            }
            upd_grid.Update();
        }



        public void updatedatafromsupplier(String totextbox, String fromlabel)
        {
            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    Label fromlabelbox = di.FindControl(fromlabel) as Label;
                    TextBox wbdt = di.FindControl(totextbox) as TextBox;

                    wbdt.Text = fromlabelbox.Text. Trim();
                }
            }
            upd_grid.Update();
        }






        protected void btn_remark_Click(object sender, EventArgs e)
        {
            string Value = txt_remark.Text;

            updatedata("txt_remark", Value);


        }

        protected void btn_yard_Click(object sender, EventArgs e)
        {
            string Value = txt_yardage.Text;

            updatedata("txt_ayard", Value);

            





        }

        protected void btn_shade_Click(object sender, EventArgs e)
        {
            string Value = txt_shade.Text;

            updatedata("txt_ashade", Value);


        }

        protected void btn_shrinkage_Click(object sender, EventArgs e)
        {
            string Value = txt_Shrinkage.Text;
            updatedata("txt_ashrink", Value);


        }

        protected void btn_width_Click(object sender, EventArgs e)
        {
            string Value = txt_width.Text;
            updatedata("txt_awidth", Value);


        }

        protected void btn_gsm_Click(object sender, EventArgs e)
        {
            string Value = txt_gsm.Text;

            updatedata("txt_agsm", Value);
        }

        protected void btn_yardage_Click(object sender, EventArgs e)
        {


            updatedatafromsupplier("txt_ayard", "lbl_syard");
        }

        protected void btn_shadage_Click(object sender, EventArgs e)
        {
            updatedatafromsupplier("txt_ashade", "lbl_sshade");
        }

       

        protected void btnSupshrink_Click(object sender, EventArgs e)
        {
            updatedatafromsupplier("txt_ashrink", "lbl_sshrinkage");
        }

        protected void btn_sswidth_Click(object sender, EventArgs e)
        {
            updatedatafromsupplier("txt_awidth", "lbl_swidth");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            updatedatafromsupplier("txt_agsm", "lbl_sgsm");
        }
    }
}