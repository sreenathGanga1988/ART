using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Quality
{
    public partial class QualityInspection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (drp_atc.SelectedValue != null)
                {
                    FillAsnCombo(int.Parse(drp_atc.SelectedValue.ToString()));

                }
            }
            catch (Exception )
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

        public void FillFabricombo(int asn_pk, int atc)
        {

            drp_color.DataSource = BLL.InventoryBLL.RollTransactionBLL.getfabricinsideASN(asn_pk,atc);

            drp_color.DataValueField = "SkuDet_PK";
            drp_color.DataTextField = "ItemDescription";
            drp_color.DataBind();
            upd_color.Update();



        }






        protected void Button2_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollEntryMRN mrnrolldata = new BLL.InventoryBLL.FabricRollEntryMRN();
            mrnrolldata.Rolldatacollection = GetRollDetailsData();
            mrnrolldata.UpproveRollInspectiondata();
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




                    BLL.InventoryBLL.FabricRollmasterDataDetails rolldata = new BLL.InventoryBLL.FabricRollmasterDataDetails();


                    rolldata.Roll_PK = int.Parse(lbl_rollpk);

                    DropDownList drp_markerType = (tbl_InverntoryDetails.Rows[i].FindControl("drp_markerType") as DropDownList);
                    DropDownList drp_acceptable = (tbl_InverntoryDetails.Rows[i].FindControl("drp_acceptable") as DropDownList);

                    String txt_defect = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_defect") as TextBox).Text.ToString());
                    String txt_defectper100 = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_defectper100") as TextBox).Text.ToString());
                    String txt_point = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_point") as TextBox).Text.ToString());
                    String txt_pointper100 = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_pointper100") as TextBox).Text.ToString());

                    rolldata.MarkerType = drp_markerType.SelectedItem.Text.Trim();
                    rolldata.IsAccepted = drp_acceptable.SelectedItem.Text.Trim();
                    rolldata.IsAccepted = drp_acceptable.SelectedItem.Text.Trim();
                    rolldata.TotalDefect = txt_defect;
                    rolldata.TotalDefecton100 = txt_defectper100;
                    rolldata.TotalPoint = txt_point;
                    rolldata.TotalPointon100yard = txt_pointper100;

                    rk.Add(rolldata);
                }
            }




            return rk;


        }

        protected void btn_Asn_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_asn.SelectedValue != null)
                {
                    FillFabricombo(int.Parse(drp_asn.SelectedValue.ToString()),int.Parse (drp_atc.SelectedValue.ToString ()));
                }
            }
            catch (Exception exp)
            {


            }
        }

        protected void btn_fabric_Click1(object sender, EventArgs e)
        {
            BLL.InventoryBLL.RollInventoryData fbrolldet = new BLL.InventoryBLL.RollInventoryData();
            tbl_InverntoryDetails.DataSource = fbrolldet.getRollDetailsofASN(int.Parse(drp_asn.SelectedValue.ToString()), int.Parse(drp_color.SelectedValue.ToString()));
            tbl_InverntoryDetails.DataBind();
            upd_grid.Update();
        }



        protected void chk_selectall_CheckedChanged(object sender, EventArgs e)
        {
            selectall();
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



        public void updatedropdata(String todropdown, String fromdropdown)
        {
            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    DropDownList wbdt = di.FindControl(todropdown) as DropDownList;


                    wbdt.Text = fromdropdown.Trim();
                }
            }
            upd_grid.Update();
        }







        protected void btn_remark_Click(object sender, EventArgs e)
        {
            string Value = txt_defect.Text;

            updatedata("txt_defect", Value);


        }

        protected void btn_yard_Click(object sender, EventArgs e)
        {
            string Value = txt_defectper100.Text;

            updatedata("txt_defectper100", Value);







        }

        protected void btn_shade_Click(object sender, EventArgs e)
        {
            string Value = txt_point.Text;

            updatedata("txt_point", Value);


        }

        protected void btn_shrinkage_Click(object sender, EventArgs e)
        {
            string Value = txt_pointper100.Text;
            updatedata("txt_pointper100", Value);


        }

        protected void btn_width_Click(object sender, EventArgs e)
        {
            string Value = drp_acceptable.Text;
            updatedropdata("drp_acceptable", Value);


        }

        protected void btn_gsm_Click(object sender, EventArgs e)
        {
            string Value = drp_markerType.Text;

            updatedropdata("drp_markerType", Value);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            searchgridview();
        }




        public void searchgridview()
        {
            for(int i=0;i<tbl_InverntoryDetails.Rows.Count;i++)
            {
                if(txt_roll.Text.Trim ()==tbl_InverntoryDetails.Rows[i].Cells[3].ToString ().Trim ())
                {
                    tbl_InverntoryDetails.Rows[i].Cells[12].Focus();
                    tbl_InverntoryDetails.Rows[i].BackColor = System.Drawing.Color.IndianRed; 
                }

            }
            upd_grid.Update();
            
        }
    }
}










