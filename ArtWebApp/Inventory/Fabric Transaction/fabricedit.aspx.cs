using ArtWebApp.DataModels;
using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class fabricedit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //FillPOCombo();
            }

        }

        protected void btn_confirmAtc_Click(object sender, EventArgs e)
        {

        }






        



     





        public void FillFabricombo(int asn_pk, int atcid)
        {

            drp_color.DataSource = BLL.InventoryBLL.RollTransactionBLL.getfabricinsideASN(asn_pk, atcid);

            drp_color.DataValueField = "SkuDet_PK";
            drp_color.DataTextField = "ItemDescription";
            drp_color.DataBind();
            upd_color.Update();



        }







        protected void btn_color_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollmasterDataDetails rolldata = new BLL.InventoryBLL.FabricRollmasterDataDetails();

            int asnpk =int.Parse ( drp_asn.SelectedValue.ToString());
            int atcid = int.Parse(drp_atc.SelectedValue.ToString());
            FillFabricombo(asnpk, atcid);
              

                
            
        }






        public List<BLL.InventoryBLL.FabricRollmasterDataDetails> GetRollDetailsData()
        {

            List<BLL.InventoryBLL.FabricRollmasterDataDetails> rk = new List<BLL.InventoryBLL.FabricRollmasterDataDetails>();
            for (int i = 0; i < tbl_InverntoryDetails.Rows.Count; i++)
            {
                CheckBox chkBx = (CheckBox)tbl_InverntoryDetails.Rows[i].FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    String rollpk = ((tbl_InverntoryDetails.Rows[i].FindControl("lbl_rollpk") as Label).Text.ToString());

                    String txt_rollnum = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_rollnum") as TextBox).Text.ToString());
                    //Decimal txt_qty = Decimal.Parse((tbl_InverntoryDetails.Rows[i].FindControl("txt_qty") as TextBox).Text.ToString());
                    String txt_remark = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_remark") as TextBox).Text.ToString());
                 
                    String txt_syard = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_syard") as TextBox).Text.ToString());
                    String txt_sshrinkage = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_sshrinkage") as TextBox).Text.ToString());
                    String txt_sWidth = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_sWidth") as TextBox).Text.ToString());
                    String txt_Sshade = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_Sshade") as TextBox).Text.ToString());
                    String txt_sgsm = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_sgsm") as TextBox).Text.ToString());
                    String txt_sweight = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_sweight") as TextBox).Text.ToString());

                    BLL.InventoryBLL.FabricRollmasterDataDetails rolldata = new BLL.InventoryBLL.FabricRollmasterDataDetails();

                    //   rolldata.Po_PK = int.Parse(ddl_po.SelectedValue.ToString());
                
                    rolldata.RollNum = txt_rollnum;
                    rolldata.Qty = Decimal.Parse(txt_syard.ToString());
                    rolldata.Roll_PK = int.Parse(rollpk);
                    rolldata.Remark = txt_remark;
                    rolldata.SShrink = txt_sshrinkage;
                    rolldata.SYard = txt_syard;
                    rolldata.SShade = txt_Sshade;
                    rolldata.SWidth = txt_sWidth;
                    rolldata.Sweight = txt_sweight;
                    rolldata.SGSM = txt_sgsm;
                   


                    rk.Add(rolldata);
                }
            }




            return rk;


        }


        public BLL.InventoryBLL.RollInventoryData getmstrdetails()
        {
            BLL.InventoryBLL.RollInventoryData rlinvdata = new BLL.InventoryBLL.RollInventoryData();

            rlinvdata.Addeddate = DateTime.Now;
            rlinvdata.DocumentNum = "NA";
            rlinvdata.AddedVia = "MR";
            rlinvdata.AddedBy = Session["SupplierUsername"].ToString().Trim(); ;



            return rlinvdata;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollEntryMRN mrnrolldata = new BLL.InventoryBLL.FabricRollEntryMRN();
         
            mrnrolldata.Rolldatacollection = GetRollDetailsData();
            mrnrolldata.UpdateRollSupplierdata();
            tbl_InverntoryDetails.DataSource = null;
            tbl_InverntoryDetails.DataBind();
            upd_grid.Update();
            MessgeboxUpdate("sucess", "Roll Detail Sucessfully Added ");

        }


        public void MessgeboxUpdate(String Messagetype, String Messg)
        {
            if (Messagetype == "sucess")
            {
                Messaediv.Attributes["class"] = "success";
                Messaediv.InnerText = Messg;
            }
            else
            {
                Messaediv.Attributes["class"] = "error-message";
                Messaediv.InnerText = Messg;
            }
        }

        protected void chk_selectall_CheckedChanged(object sender, EventArgs e)
        {
            selectall();
        }


        public void selectall()
        {
            if (chk_selectall.Checked == true)
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

        protected void btn_remark_Click(object sender, EventArgs e)
        {
            string Value = txt_remark.Text;

            updatedata("txt_remark", Value);


        }

        protected void btn_yard_Click(object sender, EventArgs e)
        {
            string Value = txt_yardage.Text;

            updatedata("txt_syard", Value);



        }

        protected void btn_shade_Click(object sender, EventArgs e)
        {
            string Value = txt_shade.Text;

            updatedata("txt_Sshade", Value);


        }

        protected void btn_shrinkage_Click(object sender, EventArgs e)
        {
            string Value = txt_Shrinkage.Text;
            updatedata("txt_sshrinkage", Value);


        }

        protected void btn_width_Click(object sender, EventArgs e)
        {
            string Value = txt_width.Text;
            updatedata("txt_sWidth", Value);


        }

        protected void btn_gsm_Click(object sender, EventArgs e)
        {
            string Value = txt_gsm.Text;

            updatedata("txt_sgsm", Value);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollmasterDataDetails   rlinvdata = new BLL.InventoryBLL.FabricRollmasterDataDetails();
            int asnpk = int.Parse(drp_asn.SelectedValue.ToString());
            tbl_InverntoryDetails.DataSource = rlinvdata.getRollDetailsAsnandFabric(asnpk,int.Parse (drp_color.SelectedValue.ToString ()));

            tbl_InverntoryDetails.DataBind();
        }
    }
}