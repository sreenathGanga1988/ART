using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Quality
{
    public partial class InsPectionCompleting : System.Web.UI.Page
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

        public void FillFabricombo(int asn_pk, int atcid)
        {

            drp_color.DataSource = BLL.InventoryBLL.RollTransactionBLL.getfabricinsideASN(asn_pk, atcid);

            drp_color.DataValueField = "SkuDet_PK";
            drp_color.DataTextField = "ItemDescription";
            drp_color.DataBind();
            upd_color.Update();



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
            DataTable dt = fbrolldet.getfullRollDetailsofASNandSKUDetPK(int.Parse(drp_asn.SelectedValue.ToString()), int.Parse(drp_color.SelectedValue.ToString()));
            tbl_InverntoryDetails.DataSource = dt;
            tbl_InverntoryDetails.DataBind();
            fillthecombo(dt);
            upd_grid.Update();
        }

        public void fillthecombo(DataTable dt)
        {



            DataView view = new DataView(dt);


            DataTable Shrinktable = view.ToTable(true, "AShrink");
            drp_shrinkage.DataSource = Shrinktable;
            //drp_shrinkage.DataValueField = "AShrink";
            //drp_shrinkage.DataTextField = "AShrink";
            drp_shrinkage.DataBind();
            upd_shrinkage.Update();



            DataTable Widthtable = view.ToTable(true, "AWidth");
            drp_cutwidth.DataSource = Widthtable;
            //drp_cutwidth.DataValueField = "AWidth";
            //drp_cutwidth.DataTextField = "AWidth";
            drp_cutwidth.DataBind();
            upd_width.Update();





            DataTable shadetable = view.ToTable(true, "AShade");
            drp_shade.DataSource = shadetable;
            //drp_shade.DataValueField = "AShade";
            //drp_shade.DataTextField = "AShade";
            drp_shade.DataBind();
            upd_shade.Update();



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





        protected void chk_selectall_CheckedChanged(object sender, EventArgs e)
        {
            selectall();
        }



        protected void Button3_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollEntryMRN mrnrolldata = new BLL.InventoryBLL.FabricRollEntryMRN();
            mrnrolldata.Rolldatacollection = GetRollDetailsData();
            mrnrolldata.UpproveRollInspectionGroup();

            tbl_InverntoryDetails.DataSource = null;
            tbl_InverntoryDetails.DataBind();
            upd_grid.Update();

        }



        public List<BLL.InventoryBLL.FabricRollmasterDataDetails> GetRollDetailsData()
        {

            List<BLL.InventoryBLL.FabricRollmasterDataDetails> rk = new List<BLL.InventoryBLL.FabricRollmasterDataDetails>();
            for (int i = 0; i < tbl_InverntoryDetails.Rows.Count; i++)
            {


                CheckBox chkBx = (CheckBox)tbl_InverntoryDetails.Rows[i].FindControl("Chk_select");

                if (chkBx.Checked == true)
                {

                    String lbl_rollpk = ((tbl_InverntoryDetails.Rows[i].FindControl("lbl_rollpk") as Label).Text.ToString());




                    BLL.InventoryBLL.FabricRollmasterDataDetails rolldata = new BLL.InventoryBLL.FabricRollmasterDataDetails();


                    rolldata.Roll_PK = int.Parse(lbl_rollpk);



                    String txt_widthgroup = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_widthgroup") as TextBox).Text.ToString());
                    String txt_shadegroup = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_shadegroup") as TextBox).Text.ToString());
                    String txt_lblshrinkagegroup = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_lblshrinkagegroup") as TextBox).Text.ToString());

                    rolldata.widthgroup = txt_widthgroup.Trim();
                    rolldata.shadegroup = txt_shadegroup.Trim();
                    rolldata.shrinkagegroup = txt_lblshrinkagegroup.Trim();

                    rk.Add(rolldata);
                }
            }




            return rk;


        }

        protected void btn_width_Click(object sender, EventArgs e)
        {
            ArrayList widthlist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_cutwidth.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String popackid = item.Value.ToString();
                widthlist.Add(popackid);
            }


            if (widthlist.Count > 0 && widthlist != null)
            {
                BLL.InventoryBLL.FabricRollmasterDataDetails fbrolldet = new BLL.InventoryBLL.FabricRollmasterDataDetails();
                DataTable dt = fbrolldet.getfullRollDetailsofASNandSKUDetPKofaWidth(int.Parse(drp_asn.SelectedValue.ToString()), int.Parse(drp_color.SelectedValue.ToString()), widthlist);

                tbl_InverntoryDetails.DataSource = dt;
                tbl_InverntoryDetails.DataBind();
            }
        }

        protected void btn_shrinkage_Click(object sender, EventArgs e)
        {
            ArrayList shrinkagegroup = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_shrinkage.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String popackid = item.Value.ToString();
                shrinkagegroup.Add(popackid);
            }


            if (shrinkagegroup.Count > 0 && shrinkagegroup != null)
            {
                BLL.InventoryBLL.FabricRollmasterDataDetails fbrolldet = new BLL.InventoryBLL.FabricRollmasterDataDetails();
                DataTable dt = fbrolldet.getfullRollDetailsofASNandSKUDetPKofShrinkage(int.Parse(drp_asn.SelectedValue.ToString()), int.Parse(drp_color.SelectedValue.ToString()), shrinkagegroup);

                tbl_InverntoryDetails.DataSource = dt;
                tbl_InverntoryDetails.DataBind();
            }
        }

        protected void btn_shade_Click(object sender, EventArgs e)
        {
            ArrayList shadegroup = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_shade.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String popackid = item.Value.ToString();
                shadegroup.Add(popackid);
            }


            if (shadegroup.Count > 0 && shadegroup != null)
            {
                BLL.InventoryBLL.FabricRollmasterDataDetails fbrolldet = new BLL.InventoryBLL.FabricRollmasterDataDetails();
                DataTable dt = fbrolldet.getfullRollDetailsofASNandSKUDetPKofShade(int.Parse(drp_asn.SelectedValue.ToString()), int.Parse(drp_color.SelectedValue.ToString()), shadegroup);

                tbl_InverntoryDetails.DataSource = dt;
                tbl_InverntoryDetails.DataBind();
            }
        }



        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void btnGroupByshrinkage_Click(object sender, EventArgs e)
        {

        }

        protected void btn_groupbyshade_Click(object sender, EventArgs e)
        {

        }

        protected void btn_copywidth_Click(object sender, EventArgs e)
        {

            string Value = txt_widthgroup.Text;

            updatedata("txt_widthgroup", Value);
        }

        protected void btn_copyshrinkage_Click(object sender, EventArgs e)
        {
            string Value = txt_shrinkgroup.Text;

            updatedata("txt_lblshrinkagegroup", Value);
        }

        protected void btn_copyshade_Click(object sender, EventArgs e)
        {
            string Value = txt_shadegroup.Text;

            updatedata("txt_shadegroup", Value);
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



        public void copyshade()
        {
            for (int i = 0; i < tbl_InverntoryDetails.Rows.Count; i++)
            {
                Label lbl_ashade = (tbl_InverntoryDetails.Rows[i].FindControl("lbl_ashade") as Label);
                TextBox txt_shadegroup = (tbl_InverntoryDetails.Rows[i].FindControl("txt_shadegroup") as TextBox);
                txt_shadegroup.Text = lbl_ashade.Text.Trim();
            }
        }

        protected void btn_inheritshade_Click(object sender, EventArgs e)
        {
            copyshade();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {

        }

        protected void Button4_Click1(object sender, EventArgs e)
        {

        }
    }
}










