using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class InspectionRollEntry : System.Web.UI.Page
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
                    FillPOCombo(int.Parse(drp_atc.SelectedValue.ToString()));
                }
            }
            catch (Exception exp)
            {


            }
        }
        public void FillPOCombo(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.ProcurementMasters
                        where ponmbr.AtcId == atcid && ponmbr.POType == "F"
                        select new
                        {
                            name = ponmbr.PONum,
                            pk = ponmbr.PO_Pk
                        };

                ddl_po.DataSource = q.ToList();
                ddl_po.DataValueField = "pk";
                ddl_po.DataTextField = "name";
                ddl_po.DataBind();
                upd_po.Update();



            }
        }

        public void FillMRNCombo(int poid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.MrnMasters
                        where ponmbr.Po_PK == poid
                        select new
                        {
                            name = ponmbr.MrnNum,
                            pk = ponmbr.Mrn_PK
                        };

                drp_mrn.DataSource = q.ToList();
                drp_mrn.DataValueField = "pk";
                drp_mrn.DataTextField = "name";
                drp_mrn.DataBind();
                upd_mrn.Update();



            }
        }
        protected void btn_po_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_po.SelectedValue != null)
                {
                    FillMRNCombo(int.Parse(ddl_po.SelectedValue.ToString()));
                }
            }
            catch (Exception exp)
            {


            }
        }

        protected void btn_mrn_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollmasterDataDetails fbrolldet = new BLL.InventoryBLL.FabricRollmasterDataDetails();
            tbl_InverntoryDetails.DataSource = fbrolldet.getRollDetailsofMRN(int.Parse(drp_mrn.SelectedValue.ToString()));
            tbl_InverntoryDetails.DataBind();
            upd_grid.Update();
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
                    rolldata.SGSM = txt_agsm.Trim();


                    rk.Add(rolldata);
                }
            }




            return rk;


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

                   

                    
                        else if  (chk_woven.Checked == false)
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

        
    }
}