using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser.ASQ
{
    public partial class ASQShuffle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAtcCombo();

            }
            else
            {

            }
        }


        public void FillAtcCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from atcorder in entty.AtcMasters
                        select new
                        {
                            name = atcorder.AtcNum,
                            pk = atcorder.AtcId
                        };

                // Create a table from the query.


                drp_atc.DataSource = q.ToList();
                drp_atc.DataBind();
                upd_atc.Update();




            }
        }

        public void FillOurStyleCombo(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.AtcDetails
                        where ponmbr.AtcId == atcid
                        select new
                        {
                            name = ponmbr.OurStyle,
                            pk = ponmbr.OurStyleID
                        };

                drp_ourstyle.DataSource = q.ToList();
                drp_ourstyle.DataBind();
                upd_ourstyle.Update();



            }
        }


        public void fillpopackdetails(int ourstyleid)
        {


            ArtEntitiesnew enty = new ArtEntitiesnew();
            var PoQuery = from pckmst in enty.PoPackMasters
                          join pckmstdet in enty.POPackDetails on
                           pckmst.PoPackId equals pckmstdet.POPackId
                          where pckmstdet.OurStyleID == ourstyleid

                          select new
                          {
                              poname = pckmst.PoPacknum,
                              popk = pckmst.PoPackId
                          } into x
                          group x by new { x.poname, x.popk } into g
                          select new
                          {
                              name = g.Key.poname,
                              pk = g.Key.popk

                          };



            cmb_po.DataSource = PoQuery.ToList();
            cmb_po.DataBind();

            drp_popack.DataSource = PoQuery.ToList();
            drp_popack.DataBind();


            //showAllPoPackATC();
        }
        protected void btn_atc_Click(object sender, EventArgs e)
        {
            FillOurStyleCombo(int.Parse(drp_atc.SelectedValue.ToString()));
        }

        protected void cmb_po_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_OURSTYLE_Click(object sender, EventArgs e)
        {
            fillpopackdetails(int.Parse(drp_ourstyle.SelectedValue.ToString()));
            lbl_ourstyleid.Text = drp_ourstyle.SelectedValue.ToString();
        }

        protected void btn_OURSTYLE0_Click(object sender, EventArgs e)
        {
            BLL.MerchandsingBLL.AsqShuffleBLL asqshuffle = new BLL.MerchandsingBLL.AsqShuffleBLL();

            tbl_fromPOdetails.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(cmb_po.SelectedValue.ToString()), int.Parse(drp_ourstyle.SelectedValue.ToString()));
            tbl_fromPOdetails.DataBind();
            updgrid1.Update();
        }

        protected void btn_OURSTYLE1_Click(object sender, EventArgs e)
        {
            String Group = drp_atc.SelectedItem.Text + "-0";
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_popack.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int popackid = int.Parse(item.Value.ToString());
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {
                BLL.MerchandsingBLL.AsqShuffleBLL asqshuffle = new BLL.MerchandsingBLL.AsqShuffleBLL();


              String condition=  GetGroup(int.Parse(cmb_po.SelectedValue.ToString()), popaklist);

               
                tbl_topodetails.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(drp_ourstyle.SelectedValue.ToString()), popaklist);
                tbl_topodetails.DataBind();
                updgrid2.Update();
                if (condition != "")
                {

                    String newstrin = asqshuffle.GetASQGroupNumber(condition, int.Parse(drp_atc.SelectedValue.ToString()), drp_atc.SelectedItem.Text);

                    lbl_group.Text = newstrin;
                    upd_groupname.Update();
                }
            }
        }





        public String  GetGroup(int popackid=0 ,ArrayList Popackdetlist=null)
        {

         


            String condition = "";
            if (popackid!=0)
            {
                Popackdetlist.Add(popackid);
            }

            for (int i = 0; i < Popackdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition +  Popackdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "," + Popackdetlist[i].ToString().Trim();
                }



            }


            return condition;

        }





        protected void lbl_newQty_TextChanged(object sender, EventArgs e)
        {

            TextBox newqty = (TextBox)sender;






            GridViewRow currentRow = newqty.ClosestContainer<GridViewRow>();
            Label lbl_poQty = (currentRow.FindControl("lbl_poQty") as Label);
            Label lbl_adjusterQty = (currentRow.FindControl("lbl_adjusterQty") as Label);

            if (float.Parse(lbl_poQty.Text) >= float.Parse(newqty.Text))
            {
                float bal = float.Parse(newqty.Text) - float.Parse(lbl_poQty.Text);

                lbl_adjusterQty.Text = bal.ToString();


                UpdatePanel upd_adjusterQty = (currentRow.FindControl("upd_adjusterQty") as UpdatePanel);
                upd_adjusterQty.Update();
            }
            else
            {
                //   String Msg = " ";
                string Msg = "alert('Cannot Increase ASQ Qty .Inform HO')";
                newqty.Text = lbl_poQty.Text;
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
            }


        }

        public void fromasqvaluechanged(GridViewRow currentRow, object sender)
        {
            TextBox newqty = (TextBox)sender;
            Label lbl_poQty = (currentRow.FindControl("lbl_poQty") as Label);
            Label lbl_adjusterQty = (currentRow.FindControl("lbl_adjusterQty") as Label);

            if (float.Parse(lbl_poQty.Text) > float.Parse(newqty.Text))
            {
                float bal = float.Parse(newqty.Text) - float.Parse(lbl_poQty.Text);

                lbl_adjusterQty.Text = bal.ToString();


                UpdatePanel upd_adjusterQty = (currentRow.FindControl("upd_adjusterQty") as UpdatePanel);
                upd_adjusterQty.Update();
            }
            else
            {
                //   String Msg = " ";
                string Msg = "alert('Cannot Increase ASQ Qty .Inform HO')";
                newqty.Text = lbl_poQty.Text;
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
            }
        }


        public void toasqvaluechange(GridViewRow currentRow, object sender)
        {
            TextBox newqty = (TextBox)sender;
            Label lbl_poQty = (currentRow.FindControl("lbl_poQty") as Label);
            Label lbl_adjusterQty = (currentRow.FindControl("lbl_toadjusterQty") as Label);

            if (float.Parse(lbl_poQty.Text) <= float.Parse(newqty.Text))
            {
                float bal = float.Parse(newqty.Text) - float.Parse(lbl_poQty.Text);

                lbl_adjusterQty.Text = bal.ToString();


                UpdatePanel upd_adjusterQty = (currentRow.FindControl("upd_toadjusterQty") as UpdatePanel);
                upd_adjusterQty.Update();
                String lbl_colorcode = (currentRow.FindControl("lbl_colorcode") as Label).Text.Trim();
                String lbl_sizename = (currentRow.FindControl("lbl_sizename") as Label).Text.Trim();
                String lbl_colorname = (currentRow.FindControl("lbl_colorname") as Label).Text.Trim();
                String lbl_sizecoode = (currentRow.FindControl("lbl_sizecoode") as Label).Text.Trim();
                Label lbl_frmpopack = (currentRow.FindControl("lbl_frmpopack") as Label);
                UpdatePanel upd_frmpopack = (currentRow.FindControl("upd_frmpopack") as UpdatePanel);

                String lbl_asq = (currentRow.FindControl("lbl_asq") as Label).Text.Trim();
                float totqt = getbalancedQty(lbl_colorcode, lbl_colorname, lbl_sizecoode, lbl_sizename, sender);

                string frmpopack = updatefromASQ(lbl_colorcode, lbl_colorname, lbl_sizecoode, lbl_sizename, totqt, lbl_asq, sender);
                lbl_frmpopack.Text = frmpopack;
                upd_frmpopack.Update();
            }
            else
            {
                //   String Msg = " ";
                string Msg = "alert('Cannot Reduce ASQ Qty .Inform HO')";
                newqty.Text = lbl_poQty.Text;
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
            }
        }


        protected void txt_newQty_TextChanged(object sender, EventArgs e)
        {
            TextBox newqty = (TextBox)sender;






            GridViewRow currentRow = newqty.ClosestContainer<GridViewRow>();

            Label lbl_poQty = (currentRow.FindControl("lbl_poQty") as Label);
            Label lbl_adjusterQty = (currentRow.FindControl("lbl_toadjusterQty") as Label);

            if (float.Parse(lbl_poQty.Text) < float.Parse(newqty.Text))
            {
                float bal = float.Parse(newqty.Text) - float.Parse(lbl_poQty.Text);

                lbl_adjusterQty.Text = bal.ToString();


                UpdatePanel upd_adjusterQty = (currentRow.FindControl("upd_toadjusterQty") as UpdatePanel);
                upd_adjusterQty.Update();
                String lbl_colorcode = (currentRow.FindControl("lbl_colorcode") as Label).Text.Trim();
                String lbl_sizename = (currentRow.FindControl("lbl_sizename") as Label).Text.Trim();
                String lbl_colorname = (currentRow.FindControl("lbl_colorname") as Label).Text.Trim();
                String lbl_sizecoode = (currentRow.FindControl("lbl_sizecoode") as Label).Text.Trim();
                Label lbl_frmpopack = (currentRow.FindControl("lbl_frmpopack") as Label);
                UpdatePanel upd_frmpopack = (currentRow.FindControl("upd_frmpopack") as UpdatePanel);

                String lbl_asq = (currentRow.FindControl("lbl_asq") as Label).Text.Trim();
                float totqt = getbalancedQty(lbl_colorcode, lbl_colorname, lbl_sizecoode, lbl_sizename, sender);

                string frmpopack = updatefromASQ(lbl_colorcode, lbl_colorname, lbl_sizecoode, lbl_sizename, totqt, lbl_asq, sender);
                lbl_frmpopack.Text = frmpopack;
                upd_frmpopack.Update();
            }
            else
            {
                //   String Msg = " ";
                string Msg = "alert('Cannot Reduce ASQ Qty .Inform HO')";
                newqty.Text = lbl_poQty.Text;
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
            }
        }







        public float getbalancedQty(String colorcode, String Colorname, String sizecode, String Sizename, object sender)
        {
            float sum = 0;
            for (int i = 0; i < tbl_topodetails.Rows.Count; i++)
            {
                GridViewRow currentRow = tbl_topodetails.Rows[i];

                CheckBox chkBx = (CheckBox)currentRow.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    Label lbl_adjusterQty = (currentRow.FindControl("lbl_toadjusterQty") as Label);
                    Label lbl_colorcode = (currentRow.FindControl("lbl_colorcode") as Label);
                    Label lbl_sizename = (currentRow.FindControl("lbl_sizename") as Label);
                    Label lbl_colorname = (currentRow.FindControl("lbl_colorname") as Label);
                    Label lbl_sizecoode = (currentRow.FindControl("lbl_sizecoode") as Label);





                    if (lbl_colorcode.Text.Trim() == colorcode && lbl_colorname.Text.Trim() == Colorname && lbl_sizename.Text.Trim() == Sizename && lbl_sizecoode.Text.Trim() == sizecode)
                    {
                        sum = sum + float.Parse(lbl_adjusterQty.Text);
                    }

                }







            }
            return sum;
        }

        public string updatefromASQ(String colorcode, String Colorname, String sizecode, String Sizename, float qty, String asq, object sender)
        {
            string frmpopack = "0";

            try
            {
                for (int i = 0; i < tbl_fromPOdetails.Rows.Count; i++)
                {
                    GridViewRow currentRow = tbl_fromPOdetails.Rows[i];


                    Label lbl_poQty = (currentRow.FindControl("lbl_poQty") as Label);
                    Label lbl_adjusterQty = (currentRow.FindControl("lbl_toadjusterQty") as Label);
                    Label lbl_colorcode = (currentRow.FindControl("lbl_colorcode") as Label);
                    Label lbl_sizename = (currentRow.FindControl("lbl_sizename") as Label);
                    Label lbl_colorname = (currentRow.FindControl("lbl_colorname") as Label);
                    Label lbl_sizecoode = (currentRow.FindControl("lbl_sizecoode") as Label);
                    Label lbl_asq = (currentRow.FindControl("lbl_asq") as Label);
                    Label lbl_bal = (currentRow.FindControl("lbl_bal") as Label);
                    Label lbl_PoDet_PK = (currentRow.FindControl("lbl_PoDet_PK") as Label);
                    if (lbl_asq.Text.Trim() == asq)
                    {

                        string Msg = "alert('TO ASQ Cannot be same as from ASQ')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
                    }
                    else
                    {

                        if (lbl_colorcode.Text.Trim() == colorcode && lbl_colorname.Text.Trim() == Colorname && lbl_sizename.Text.Trim() == Sizename && lbl_sizecoode.Text.Trim() == sizecode)
                        {
                            lbl_bal.Text = qty.ToString();
                            UpdatePanel upd_bal = (currentRow.FindControl("upd_bal") as UpdatePanel);
                            upd_bal.Update();
                            frmpopack = lbl_PoDet_PK.Text;
                        }


                    }



                }
            }
            catch (Exception)
            {

                frmpopack = "0";
            }
            return frmpopack;
        }





        protected void Button1_Click(object sender, EventArgs e)
        {

        }





        public void validationofASQ()
        {
            Boolean Isok = true;

            for (int i = 0; i < tbl_fromPOdetails.Rows.Count; i++)
            {

                GridViewRow currentRow = tbl_fromPOdetails.Rows[i];
                CheckBox chkBx = (CheckBox)currentRow.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    TextBox txt_newQty = (currentRow.FindControl("txt_newQty") as TextBox);


                    fromasqvaluechanged(currentRow, txt_newQty);
                }



            }
            for (int i = 0; i < tbl_topodetails.Rows.Count; i++)
            {
                GridViewRow currentRow = tbl_topodetails.Rows[i];
                CheckBox chkBx = (CheckBox)currentRow.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    TextBox txt_newQty = (currentRow.FindControl("txt_newQty") as TextBox);
                    toasqvaluechange(currentRow, txt_newQty);
                }



            }

            for (int i = 0; i < tbl_fromPOdetails.Rows.Count; i++)
            {
                GridViewRow currentRow = tbl_fromPOdetails.Rows[i];
                CheckBox chkBx = (CheckBox)currentRow.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    try
                    {
                        TextBox txt_newQty = (currentRow.FindControl("txt_newQty") as TextBox);
                        Label lbl_bal = (currentRow.FindControl("lbl_bal") as Label);
                        Label lbl_adjusterQty = (currentRow.FindControl("lbl_adjusterQty") as Label);

                        float bal = float.Parse(lbl_bal.Text) + float.Parse(lbl_adjusterQty.Text);
                        if (bal != 0)
                        {
                            string Msg = "alert('Please match the Reduced Qty and Added Qty ')";

                            ScriptManager.RegisterClientScriptBlock((txt_newQty as Control), this.GetType(), "alert", Msg, true);
                            Isok = false;
                        }
                        else
                        {

                        }
                    }
                    catch (Exception)
                    {

                        Isok = false;
                    }
                }

            }

            if (Isok == true)
            {
                btn_submit.Enabled = true;
            }
            else
            {
                btn_submit.Enabled = false;
            }

        }


        protected void btn_confirmshuffle_Click(object sender, EventArgs e)
        {
            validationofASQ();
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            BLL.MerchandsingBLL.AsqShuffleMasterData asshfl = new BLL.MerchandsingBLL.AsqShuffleMasterData();
            asshfl.ourstyleid = int.Parse(drp_ourstyle.SelectedValue.ToString());
            asshfl.FromPOPackID = int.Parse(cmb_po.SelectedValue.ToString());
            asshfl.asqgroup = lbl_group.Text;

            String fromourstyleid = (tbl_fromPOdetails.Rows[0].FindControl("lbl_ourstyleid") as Label).Text;
            String toourstyleid = (tbl_topodetails.Rows[0].FindControl("lbl_ourstyleid") as Label).Text;
            String frompackid = (tbl_fromPOdetails.Rows[0].FindControl("lbl_poapckid") as Label).Text;
            if ((asshfl.ourstyleid.ToString() == fromourstyleid) && (fromourstyleid == toourstyleid) && (asshfl.FromPOPackID.ToString() == frompackid))
            {
                asshfl.ASQShuffleDetailsDataCollection = GetPODetailsData();
                asshfl.insertasqshufflemaster();

                tbl_fromPOdetails.DataSource = null;
                tbl_topodetails.DataSource = null;
                tbl_topodetails.DataBind();
                tbl_fromPOdetails.DataBind();
            }
            else
            {
                string Msg = "alert('Selected Dropdowns and grid values didnt match renterdetails')";

                ScriptManager.RegisterClientScriptBlock((tbl_fromPOdetails as Control), this.GetType(), "alert", Msg, true);
            }
        }





        public List<BLL.MerchandsingBLL.ASQShuffleDetailsData> GetPODetailsData()
        {


            List<BLL.MerchandsingBLL.ASQShuffleDetailsData> rk = new List<BLL.MerchandsingBLL.ASQShuffleDetailsData>();
            for (int i = 0; i < tbl_topodetails.Rows.Count; i++)
            {
                GridViewRow currentRow = tbl_topodetails.Rows[i];

                CheckBox chkBx = (CheckBox)(currentRow.FindControl("chk_select"));

                if (chkBx != null && chkBx.Checked)
                {




                    Label lbl_ourstyleid = (currentRow.FindControl("lbl_ourstyleid") as Label);
                    Label lbl_PoDet_PK = (currentRow.FindControl("lbl_PoDet_PK") as Label);
                    Label lbl_colorcode = (currentRow.FindControl("lbl_colorcode") as Label);
                    Label lbl_sizename = (currentRow.FindControl("lbl_sizename") as Label);
                    Label lbl_colorname = (currentRow.FindControl("lbl_colorname") as Label);
                    Label lbl_sizecoode = (currentRow.FindControl("lbl_sizecoode") as Label);
                    Label lbl_frmpopack = (currentRow.FindControl("lbl_frmpopack") as Label);
                    Label txt_newQty = (currentRow.FindControl("lbl_toadjusterQty") as Label);




                    if (lbl_frmpopack.Text != "0")
                    {
                        BLL.MerchandsingBLL.ASQShuffleDetailsData pddetails = new BLL.MerchandsingBLL.ASQShuffleDetailsData();
                        pddetails.OurStyleID = int.Parse(lbl_ourstyleid.Text);

                        pddetails.ToPOPackDet_PK = int.Parse(lbl_PoDet_PK.Text);
                        pddetails.AddedQty = int.Parse(txt_newQty.Text); ;
                        pddetails.colorcode = lbl_colorcode.Text.Trim();
                        pddetails.colorname = lbl_colorname.Text.Trim();
                        pddetails.sizecode = lbl_sizecoode.Text.Trim();
                        pddetails.sizename = lbl_sizename.Text.Trim();
                        pddetails.FromPOPackDet_PK = int.Parse(lbl_frmpopack.Text);




                        rk.Add(pddetails);
                    }
                }



            }




            return rk;


        }

    }
}